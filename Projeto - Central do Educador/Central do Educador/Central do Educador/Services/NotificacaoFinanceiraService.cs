using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Central_do_Educador.Services
{
    /// <summary>
    /// Processa notificações automáticas financeiras:
    /// lembrete de vencimento, cobrança de parcelas vencidas e confirmação de pagamento.
    /// Integra com EmailService e WhatsAppService existentes.
    /// </summary>
    public static class NotificacaoFinanceiraService
    {
        // ──────────────────────────────────────────────
        //  Tipos auxiliares
        // ──────────────────────────────────────────────

        public class ResultadoProcessamento
        {
            public int LembretesGerados { get; set; }
            public int CobrancasGeradas { get; set; }
            public int EmailsEnviados { get; set; }
            public int EmailsFalhas { get; set; }
            public int WhatsAppEnviados { get; set; }
            public int WhatsAppFalhas { get; set; }
            public int WhatsAppLinks { get; set; }
            public List<string> Erros { get; set; } = [];
        }

        private class DadosParcela
        {
            public long ParcelaId { get; set; }
            public long AlunoId { get; set; }
            public string NomeAluno { get; set; } = "";
            public string? EmailAluno { get; set; }
            public string? TelefoneAluno { get; set; }
            public string? NomeResponsavel { get; set; }
            public string? TelefoneResponsavel { get; set; }
            public int NumeroParcela { get; set; }
            public int TotalParcelas { get; set; }
            public decimal ValorParcela { get; set; }
            public string DataVencimento { get; set; } = "";
            public string DescricaoContrato { get; set; } = "";
            public int DiasRestantes { get; set; }
            public int DiasAtraso { get; set; }
        }

        private class TemplateInfo
        {
            public int Id { get; set; }
            public string Canal { get; set; } = "";
            public string? Assunto { get; set; }
            public string Corpo { get; set; } = "";
        }

        // ──────────────────────────────────────────────
        //  Processamento principal
        // ──────────────────────────────────────────────

        /// <summary>
        /// Executa o ciclo completo: atualiza parcelas vencidas, gera lembretes,
        /// gera cobranças e envia tudo pelos canais configurados.
        /// </summary>
        public static async Task<ResultadoProcessamento> ProcessarAsync()
        {
            var resultado = new ResultadoProcessamento();

            var config = await FinanceiroService.ObterConfigAsync();
            if (!config.Ativo) return resultado;

            // 1) Marcar parcelas vencidas
            await FinanceiroService.AtualizarParcelasVencidasAsync();

            // 2) Gerar lembretes de vencimento próximo
            resultado.LembretesGerados = await GerarLembretesVencimentoAsync(config);

            // 3) Gerar cobranças de parcelas vencidas
            resultado.CobrancasGeradas = await GerarCobrancasVencidasAsync(config);

            // 4) Enviar notificações pendentes pelos canais
            if (config.EnviarEmail)
            {
                try
                {
                    var (enviados, falhas) = await EmailService.ProcessarPendentesAsync();
                    resultado.EmailsEnviados = enviados;
                    resultado.EmailsFalhas = falhas;
                }
                catch (Exception ex)
                {
                    resultado.Erros.Add($"Erro ao processar e-mails: {ex.Message}");
                }
            }

            if (config.EnviarWhatsapp)
            {
                try
                {
                    var (enviados, falhas, links) = await WhatsAppService.ProcessarPendentesAsync();
                    resultado.WhatsAppEnviados = enviados;
                    resultado.WhatsAppFalhas = falhas;
                    resultado.WhatsAppLinks = links;
                }
                catch (Exception ex)
                {
                    resultado.Erros.Add($"Erro ao processar WhatsApp: {ex.Message}");
                }
            }

            return resultado;
        }

        /// <summary>
        /// Gera notificação de confirmação de pagamento para uma parcela específica.
        /// </summary>
        public static async Task<int> GerarConfirmacaoPagamentoAsync(long parcelaId)
        {
            var parcela = await ObterDadosParcelaAsync(parcelaId);
            if (parcela == null) return 0;

            // Buscar dados adicionais do pagamento
            string? dataPagamento = null;
            string? formaPagamento = null;
            decimal valorPago = 0;

            using (var conn = new SqliteConnection(Db.ConnectionString))
            {
                await conn.OpenAsync();
                using var cmd = new SqliteCommand(
                    "SELECT data_pagamento, forma_pagamento, valor_pago FROM parcelas WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", parcelaId);

                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    dataPagamento = reader["data_pagamento"]?.ToString();
                    formaPagamento = reader["forma_pagamento"]?.ToString();
                    var vpObj = reader["valor_pago"];
                    if (vpObj is not DBNull && vpObj != null)
                        valorPago = Convert.ToDecimal(vpObj, CultureInfo.InvariantCulture);
                }
            }

            string dataPgtoFmt = "—";
            if (DateTime.TryParse(dataPagamento, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtPgto))
                dataPgtoFmt = dtPgto.ToString("dd/MM/yyyy");

            var placeholders = CriarPlaceholders(parcela);
            placeholders["{valor_pago}"] = valorPago.ToString("N2", new CultureInfo("pt-BR"));
            placeholders["{data_pagamento}"] = dataPgtoFmt;
            placeholders["{forma_pagamento}"] = string.IsNullOrWhiteSpace(formaPagamento) ? "—" : formaPagamento;

            var templates = await ObterTemplatesAsync("CONFIRMACAO_PAGAMENTO");
            return await CriarNotificacoesAsync(parcela, templates, placeholders);
        }

        // ──────────────────────────────────────────────
        //  Lembretes de vencimento
        // ──────────────────────────────────────────────

        private static async Task<int> GerarLembretesVencimentoAsync(FinanceiroService.ConfiguracaoFinanceiro config)
        {
            // Buscar parcelas PENDENTE que vencem nos próximos N dias
            // e que ainda não foram notificadas hoje
            const string sql = @"
                SELECT p.id, p.aluno_id, p.numero_parcela, p.valor_parcela, p.data_vencimento,
                       p.notificado_em,
                       c.descricao AS descricao_contrato, c.quantidade_parcelas AS total_parcelas,
                       a.nome AS nome_aluno, a.email, a.numero_aluno,
                       a.NomeResponsavel, a.numero_responsavel
                FROM parcelas p
                INNER JOIN contratos c ON c.id = p.contrato_id
                INNER JOIN alunos a ON a.id = p.aluno_id
                WHERE p.status = 'PENDENTE'
                  AND date(p.data_vencimento) BETWEEN date('now') AND date('now', @dias || ' days')
                  AND (p.notificado_em IS NULL OR date(p.notificado_em) < date('now'))
                ORDER BY p.data_vencimento ASC;";

            var parcelas = await BuscarParcelasParaNotificacaoAsync(sql,
                [Db.P("@dias", config.DiasAntecedenciaLembrete.ToString())]);

            if (parcelas.Count == 0) return 0;

            var templates = await ObterTemplatesAsync("LEMBRETE_VENCIMENTO");
            if (templates.Count == 0) return 0;

            int total = 0;
            foreach (var p in parcelas)
            {
                var placeholders = CriarPlaceholders(p);
                int criadas = await CriarNotificacoesAsync(p, templates, placeholders);
                if (criadas > 0)
                {
                    await MarcarNotificadoAsync(p.ParcelaId);
                    total += criadas;
                }
            }

            return total;
        }

        // ──────────────────────────────────────────────
        //  Cobranças de parcelas vencidas
        // ──────────────────────────────────────────────

        private static async Task<int> GerarCobrancasVencidasAsync(FinanceiroService.ConfiguracaoFinanceiro config)
        {
            // Buscar parcelas VENCIDA com atraso >= N dias
            // e que não foram notificadas hoje
            const string sql = @"
                SELECT p.id, p.aluno_id, p.numero_parcela, p.valor_parcela, p.data_vencimento,
                       p.notificado_em,
                       c.descricao AS descricao_contrato, c.quantidade_parcelas AS total_parcelas,
                       a.nome AS nome_aluno, a.email, a.numero_aluno,
                       a.NomeResponsavel, a.numero_responsavel
                FROM parcelas p
                INNER JOIN contratos c ON c.id = p.contrato_id
                INNER JOIN alunos a ON a.id = p.aluno_id
                WHERE p.status = 'VENCIDA'
                  AND julianday('now') - julianday(p.data_vencimento) >= @dias_atraso
                  AND (p.notificado_em IS NULL OR date(p.notificado_em) < date('now'))
                ORDER BY p.data_vencimento ASC;";

            var parcelas = await BuscarParcelasParaNotificacaoAsync(sql,
                [Db.P("@dias_atraso", config.DiasAtrasoCobranca)]);

            if (parcelas.Count == 0) return 0;

            var templates = await ObterTemplatesAsync("COBRANCA_VENCIDA");
            if (templates.Count == 0) return 0;

            int total = 0;
            foreach (var p in parcelas)
            {
                var placeholders = CriarPlaceholders(p);
                int criadas = await CriarNotificacoesAsync(p, templates, placeholders);
                if (criadas > 0)
                {
                    await MarcarNotificadoAsync(p.ParcelaId);
                    total += criadas;
                }
            }

            return total;
        }

        // ──────────────────────────────────────────────
        //  Métodos privados
        // ──────────────────────────────────────────────

        private static async Task<List<DadosParcela>> BuscarParcelasParaNotificacaoAsync(
            string sql, SqliteParameter[] parametros)
        {
            var lista = new List<DadosParcela>();

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqliteCommand(sql, conn);
            foreach (var p in parametros)
                cmd.Parameters.Add(p);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                string vencRaw = reader["data_vencimento"]?.ToString() ?? "";
                int diasRestantes = 0;
                int diasAtraso = 0;

                if (DateTime.TryParse(vencRaw, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtVenc))
                {
                    var diff = (dtVenc.Date - DateTime.Today).Days;
                    if (diff >= 0) diasRestantes = diff;
                    else diasAtraso = Math.Abs(diff);
                }

                lista.Add(new DadosParcela
                {
                    ParcelaId           = reader.GetInt64(reader.GetOrdinal("id")),
                    AlunoId             = reader.GetInt64(reader.GetOrdinal("aluno_id")),
                    NomeAluno           = reader["nome_aluno"]?.ToString() ?? "",
                    EmailAluno          = reader["email"]?.ToString(),
                    TelefoneAluno       = reader["numero_aluno"]?.ToString(),
                    NomeResponsavel     = reader["NomeResponsavel"]?.ToString(),
                    TelefoneResponsavel = reader["numero_responsavel"]?.ToString(),
                    NumeroParcela       = Convert.ToInt32(reader["numero_parcela"]),
                    TotalParcelas       = Convert.ToInt32(reader["total_parcelas"]),
                    ValorParcela        = Convert.ToDecimal(reader["valor_parcela"], CultureInfo.InvariantCulture),
                    DataVencimento      = vencRaw,
                    DescricaoContrato   = reader["descricao_contrato"]?.ToString() ?? "",
                    DiasRestantes       = diasRestantes,
                    DiasAtraso          = diasAtraso,
                });
            }

            return lista;
        }

        private static Dictionary<string, string> CriarPlaceholders(DadosParcela p)
        {
            string vencFmt = DateTime.TryParse(p.DataVencimento, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt)
                ? dt.ToString("dd/MM/yyyy")
                : p.DataVencimento;

            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["{aluno}"]             = p.NomeAluno,
                ["{valor_parcela}"]     = p.ValorParcela.ToString("N2", new CultureInfo("pt-BR")),
                ["{data_vencimento}"]   = vencFmt,
                ["{numero_parcela}"]    = p.NumeroParcela.ToString(),
                ["{total_parcelas}"]    = p.TotalParcelas.ToString(),
                ["{descricao_contrato}"]= p.DescricaoContrato,
                ["{dias_restantes}"]    = p.DiasRestantes.ToString(),
                ["{dias_atraso}"]       = p.DiasAtraso.ToString(),
            };
        }

        private static string SubstituirPlaceholders(string texto, Dictionary<string, string> placeholders)
        {
            foreach (var (chave, valor) in placeholders)
            {
                texto = texto.Replace(chave, valor, StringComparison.OrdinalIgnoreCase);
            }
            return texto;
        }

        private static async Task<List<TemplateInfo>> ObterTemplatesAsync(string codigoTemplate)
        {
            const string sql = @"
                SELECT id, canal, assunto, corpo
                FROM templates_mensagem
                WHERE codigo = @codigo AND ativo = 1;";

            var lista = new List<TemplateInfo>();

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@codigo", codigoTemplate);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new TemplateInfo
                {
                    Id      = reader.GetInt32(0),
                    Canal   = reader.GetString(1),
                    Assunto = reader["assunto"]?.ToString(),
                    Corpo   = reader.GetString(3),
                });
            }

            return lista;
        }

        private static async Task<DadosParcela?> ObterDadosParcelaAsync(long parcelaId)
        {
            const string sql = @"
                SELECT p.id, p.aluno_id, p.numero_parcela, p.valor_parcela, p.data_vencimento,
                       c.descricao AS descricao_contrato, c.quantidade_parcelas AS total_parcelas,
                       a.nome AS nome_aluno, a.email, a.numero_aluno,
                       a.NomeResponsavel, a.numero_responsavel
                FROM parcelas p
                INNER JOIN contratos c ON c.id = p.contrato_id
                INNER JOIN alunos a ON a.id = p.aluno_id
                WHERE p.id = @id;";

            var lista = await BuscarParcelasParaNotificacaoAsync(sql, [Db.P("@id", parcelaId)]);
            return lista.Count > 0 ? lista[0] : null;
        }

        /// <summary>
        /// Cria registros nas tabelas notificacoes + notificacao_destinatarios
        /// para todos os templates do código informado.
        /// </summary>
        private static async Task<int> CriarNotificacoesAsync(
            DadosParcela parcela, List<TemplateInfo> templates, Dictionary<string, string> placeholders)
        {
            int total = 0;

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            foreach (var tpl in templates)
            {
                string mensagem = SubstituirPlaceholders(tpl.Corpo, placeholders);
                string? titulo = tpl.Assunto != null
                    ? SubstituirPlaceholders(tpl.Assunto, placeholders)
                    : null;

                // Inserir notificação (agendamento_id = 0 para notificações financeiras)
                const string sqlNotif = @"
                    INSERT INTO notificacoes
                        (agendamento_id, canal, template_id, titulo, mensagem, status)
                    VALUES (0, @canal, @template_id, @titulo, @mensagem, 'PENDENTE');
                    SELECT last_insert_rowid();";

                using var cmdNotif = new SqliteCommand(sqlNotif, conn);
                cmdNotif.Parameters.AddWithValue("@canal", tpl.Canal);
                cmdNotif.Parameters.AddWithValue("@template_id", tpl.Id);
                cmdNotif.Parameters.AddWithValue("@titulo", (object?)titulo ?? DBNull.Value);
                cmdNotif.Parameters.AddWithValue("@mensagem", mensagem);

                long notifId = (long)(await cmdNotif.ExecuteScalarAsync())!;

                // Destinatário: aluno
                await InserirDestinatarioAsync(conn, notifId, "ALUNO",
                    parcela.NomeAluno, parcela.EmailAluno, parcela.TelefoneAluno, tpl.Canal);

                // Destinatário: responsável (se houver)
                if (!string.IsNullOrWhiteSpace(parcela.NomeResponsavel))
                {
                    await InserirDestinatarioAsync(conn, notifId, "RESPONSAVEL",
                        parcela.NomeResponsavel, null, parcela.TelefoneResponsavel, tpl.Canal);
                }

                total++;
            }

            return total;
        }

        private static async Task InserirDestinatarioAsync(
            SqliteConnection conn, long notificacaoId,
            string tipoPessoa, string? nome, string? email, string? telefone, string canal)
        {
            if (canal == "EMAIL" && string.IsNullOrWhiteSpace(email)) return;
            if (canal == "WHATSAPP" && string.IsNullOrWhiteSpace(telefone)) return;

            const string sql = @"
                INSERT INTO notificacao_destinatarios
                    (notificacao_id, tipo_pessoa, nome, email, telefone, status)
                VALUES
                    (@notificacao_id, @tipo_pessoa, @nome, @email, @telefone, 'PENDENTE');";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@notificacao_id", notificacaoId);
            cmd.Parameters.AddWithValue("@tipo_pessoa", tipoPessoa);
            cmd.Parameters.AddWithValue("@nome", (object?)nome ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object?)email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@telefone", (object?)telefone ?? DBNull.Value);
            await cmd.ExecuteNonQueryAsync();
        }

        private static async Task MarcarNotificadoAsync(long parcelaId)
        {
            await Db.ExecuteNonQueryAsync(@"
                UPDATE parcelas
                SET notificado_em = datetime('now','localtime')
                WHERE id = @id;",
                [Db.P("@id", parcelaId)]);
        }
    }
}
