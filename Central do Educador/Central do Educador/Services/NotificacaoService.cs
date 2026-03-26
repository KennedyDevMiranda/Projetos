using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Central_do_Educador.Services
{
    /// <summary>
    /// Gera notificações a partir dos templates_mensagem,
    /// substituindo placeholders e gravando nas tabelas de notificação.
    /// </summary>
    public static class NotificacaoService
    {
        // Mapeia status do agendamento → código do template
        private static readonly Dictionary<string, string> _statusParaTemplate = new(StringComparer.OrdinalIgnoreCase)
        {
            ["CONFIRMADO"] = "CONFIRMACAO_AGENDAMENTO",
            ["CANCELADO"]  = "CANCELAMENTO_AGENDAMENTO",
            ["REMARCADO"]  = "LEMBRETE_AGENDAMENTO",
        };

        /// <summary>
        /// Gera notificações (e-mail + WhatsApp) para o agendamento informado,
        /// usando o template adequado ao status.
        /// Retorna a quantidade de notificações geradas.
        /// </summary>
        public static async Task<int> GerarNotificacoesAsync(int agendamentoId, string novoStatus)
        {
            if (!_statusParaTemplate.TryGetValue(novoStatus, out string? codigoTemplate))
                return 0; // Status sem template associado (ex.: PENDENTE, CONCLUIDO)

            // 1) Buscar dados do agendamento + aluno
            var dados = await ObterDadosAgendamentoAsync(agendamentoId);
            if (dados == null) return 0;

            // 2) Buscar templates ativos para o código (um por canal)
            var templates = await ObterTemplatesAsync(codigoTemplate);
            if (templates.Count == 0) return 0;

            int total = 0;

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            foreach (var tpl in templates)
            {
                // 3) Substituir placeholders
                string mensagem = SubstituirPlaceholders(tpl.Corpo, dados);
                string? titulo  = tpl.Assunto != null
                    ? SubstituirPlaceholders(tpl.Assunto, dados)
                    : null;

                // 4) Inserir na tabela notificacoes
                const string sqlNotif = @"
                    INSERT INTO notificacoes (agendamento_id, canal, template_id, titulo, mensagem, status)
                    VALUES (@agendamento_id, @canal, @template_id, @titulo, @mensagem, 'PENDENTE');
                    SELECT last_insert_rowid();";

                using var cmdNotif = new SqliteCommand(sqlNotif, conn);
                cmdNotif.Parameters.AddWithValue("@agendamento_id", agendamentoId);
                cmdNotif.Parameters.AddWithValue("@canal", tpl.Canal);
                cmdNotif.Parameters.AddWithValue("@template_id", tpl.Id);
                cmdNotif.Parameters.AddWithValue("@titulo", (object?)titulo ?? DBNull.Value);
                cmdNotif.Parameters.AddWithValue("@mensagem", mensagem);

                long notifId = (long)(await cmdNotif.ExecuteScalarAsync())!;

                // 5) Inserir destinatário(s): aluno + responsável (se houver)
                await InserirDestinatarioAsync(conn, notifId, "ALUNO",
                    dados.NomeAluno, dados.EmailAluno, dados.TelefoneAluno, tpl.Canal);

                if (!string.IsNullOrWhiteSpace(dados.NomeResponsavel))
                {
                    await InserirDestinatarioAsync(conn, notifId, "RESPONSAVEL",
                        dados.NomeResponsavel, null, dados.TelefoneResponsavel, tpl.Canal);
                }

                total++;
            }

            return total;
        }

        /// <summary>
        /// Permite visualizar a mensagem final antes de salvar (preview).
        /// </summary>
        public static async Task<List<NotificacaoPreview>> GerarPreviewAsync(int agendamentoId, string novoStatus)
        {
            var result = new List<NotificacaoPreview>();

            if (!_statusParaTemplate.TryGetValue(novoStatus, out string? codigoTemplate))
                return result;

            var dados = await ObterDadosAgendamentoAsync(agendamentoId);
            if (dados == null) return result;

            var templates = await ObterTemplatesAsync(codigoTemplate);

            foreach (var tpl in templates)
            {
                result.Add(new NotificacaoPreview
                {
                    Canal   = tpl.Canal,
                    Titulo  = tpl.Assunto != null ? SubstituirPlaceholders(tpl.Assunto, dados) : null,
                    Mensagem = SubstituirPlaceholders(tpl.Corpo, dados),
                });
            }

            return result;
        }

        // ──────────────────────────────────────────────
        //  Métodos privados
        // ──────────────────────────────────────────────

        private static string SubstituirPlaceholders(string texto, DadosAgendamento d)
        {
            return texto
                .Replace("{aluno}", d.NomeAluno)
                .Replace("{tipo}", d.Tipo)
                .Replace("{data_hora}", d.DataHoraFormatada)
                .Replace("{local}", string.IsNullOrWhiteSpace(d.Local) ? "—" : d.Local)
                .Replace("{observacao}", string.IsNullOrWhiteSpace(d.Observacao) ? "—" : d.Observacao)
                .Replace("{status}", d.Status);
        }

        private static async Task<DadosAgendamento?> ObterDadosAgendamentoAsync(int agendamentoId)
        {
            const string sql = @"
                SELECT
                    ag.id, ag.data_hora, ag.tipo, ag.local, ag.observacao, ag.status,
                    a.nome, a.email, a.numero_aluno,
                    a.NomeResponsavel, a.numero_responsavel
                FROM agendamentos ag
                INNER JOIN alunos a ON a.id = ag.aluno_id
                WHERE ag.id = @id;";

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", agendamentoId);

            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            string dataHoraRaw = reader["data_hora"]?.ToString() ?? "";
            string dataHoraFmt = DateTime.TryParse(dataHoraRaw, out var dt)
                ? dt.ToString("dd/MM/yyyy HH:mm")
                : dataHoraRaw;

            return new DadosAgendamento
            {
                AgendamentoId       = reader.GetInt32(0),
                DataHoraFormatada   = dataHoraFmt,
                Tipo                = reader["tipo"]?.ToString() ?? "",
                Local               = reader["local"]?.ToString() ?? "",
                Observacao          = reader["observacao"]?.ToString() ?? "",
                Status              = reader["status"]?.ToString() ?? "",
                NomeAluno           = reader["nome"]?.ToString() ?? "",
                EmailAluno          = reader["email"]?.ToString(),
                TelefoneAluno       = reader["numero_aluno"]?.ToString(),
                NomeResponsavel     = reader["NomeResponsavel"]?.ToString(),
                TelefoneResponsavel = reader["numero_responsavel"]?.ToString(),
            };
        }

        private static async Task<List<TemplateInfo>> ObterTemplatesAsync(string codigoTemplate)
        {
            const string sql = @"
                SELECT id, codigo, canal, assunto, corpo
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
                    Codigo  = reader.GetString(1),
                    Canal   = reader.GetString(2),
                    Assunto = reader["assunto"]?.ToString(),
                    Corpo   = reader.GetString(4),
                });
            }

            return lista;
        }

        private static async Task InserirDestinatarioAsync(
            SqliteConnection conn, long notificacaoId,
            string tipoPessoa, string? nome, string? email, string? telefone, string canal)
        {
            // Só insere se houver contato relevante para o canal
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

        // ──────────────────────────────────────────────
        //  Tipos auxiliares
        // ──────────────────────────────────────────────

        private class DadosAgendamento
        {
            public int AgendamentoId { get; set; }
            public string DataHoraFormatada { get; set; } = "";
            public string Tipo { get; set; } = "";
            public string Local { get; set; } = "";
            public string Observacao { get; set; } = "";
            public string Status { get; set; } = "";
            public string NomeAluno { get; set; } = "";
            public string? EmailAluno { get; set; }
            public string? TelefoneAluno { get; set; }
            public string? NomeResponsavel { get; set; }
            public string? TelefoneResponsavel { get; set; }
        }

        private class TemplateInfo
        {
            public int Id { get; set; }
            public string Codigo { get; set; } = "";
            public string Canal { get; set; } = "";
            public string? Assunto { get; set; }
            public string Corpo { get; set; } = "";
        }

        public class NotificacaoPreview
        {
            public string Canal { get; set; } = "";
            public string? Titulo { get; set; }
            public string Mensagem { get; set; } = "";
        }
    }
}
