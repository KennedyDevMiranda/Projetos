using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;

namespace Central_do_Educador.Services
{
    /// <summary>
    /// Gerencia contratos, parcelas e configurações financeiras.
    /// </summary>
    public static class FinanceiroService
    {
        // ──────────────────────────────────────────────
        //  Tipos auxiliares
        // ──────────────────────────────────────────────

        public class Contrato
        {
            public long Id { get; set; }
            public long AlunoId { get; set; }
            public string NomeAluno { get; set; } = "";
            public string Descricao { get; set; } = "";
            public decimal ValorTotal { get; set; }
            public string DataInicio { get; set; } = "";
            public string? DataFim { get; set; }
            public int QuantidadeParcelas { get; set; } = 1;
            public int DiaVencimento { get; set; } = 10;
            public string Status { get; set; } = "ATIVO";
            public string? Observacao { get; set; }
            public string? CriadoEm { get; set; }
        }

        public class Parcela
        {
            public long Id { get; set; }
            public long ContratoId { get; set; }
            public long AlunoId { get; set; }
            public string NomeAluno { get; set; } = "";
            public int NumeroParcela { get; set; }
            public decimal ValorParcela { get; set; }
            public string DataVencimento { get; set; } = "";
            public string? DataPagamento { get; set; }
            public decimal? ValorPago { get; set; }
            public string Status { get; set; } = "PENDENTE";
            public string? FormaPagamento { get; set; }
            public string? Observacao { get; set; }
            public string? NotificadoEm { get; set; }
            public string? DescricaoContrato { get; set; }
            public int TotalParcelas { get; set; }
        }

        public class ConfiguracaoFinanceiro
        {
            public int DiasAntecedenciaLembrete { get; set; } = 3;
            public int DiasAtrasoCobranca { get; set; } = 1;
            public bool EnviarEmail { get; set; } = true;
            public bool EnviarWhatsapp { get; set; } = true;
            public string HorarioEnvio { get; set; } = "09:00";
            public bool Ativo { get; set; } = true;
        }

        public class ResumoFinanceiro
        {
            public int TotalContratos { get; set; }
            public int ContratosAtivos { get; set; }
            public decimal ReceitaTotal { get; set; }
            public decimal TotalRecebido { get; set; }
            public decimal TotalPendente { get; set; }
            public decimal TotalVencido { get; set; }
            public int ParcelasVencidas { get; set; }
            public int ParcelasAVencer { get; set; }
        }

        // ──────────────────────────────────────────────
        //  Configuração Financeiro
        // ──────────────────────────────────────────────

        public static async Task<ConfiguracaoFinanceiro> ObterConfigAsync()
        {
            const string sql = @"
                SELECT dias_antecedencia_lembrete, dias_atraso_cobranca,
                       enviar_email, enviar_whatsapp, horario_envio, ativo
                FROM configuracao_financeiro
                WHERE id = 1;";

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqliteCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return new ConfiguracaoFinanceiro();

            return new ConfiguracaoFinanceiro
            {
                DiasAntecedenciaLembrete = Convert.ToInt32(reader["dias_antecedencia_lembrete"]),
                DiasAtrasoCobranca       = Convert.ToInt32(reader["dias_atraso_cobranca"]),
                EnviarEmail              = Convert.ToInt32(reader["enviar_email"]) == 1,
                EnviarWhatsapp           = Convert.ToInt32(reader["enviar_whatsapp"]) == 1,
                HorarioEnvio             = reader["horario_envio"]?.ToString() ?? "09:00",
                Ativo                    = Convert.ToInt32(reader["ativo"]) == 1,
            };
        }

        public static async Task SalvarConfigAsync(ConfiguracaoFinanceiro cfg)
        {
            const string sql = @"
                UPDATE configuracao_financeiro
                SET dias_antecedencia_lembrete = @dias_lembrete,
                    dias_atraso_cobranca       = @dias_atraso,
                    enviar_email               = @email,
                    enviar_whatsapp            = @whatsapp,
                    horario_envio              = @horario,
                    ativo                      = @ativo
                WHERE id = 1;";

            await Db.ExecuteNonQueryAsync(sql,
            [
                Db.P("@dias_lembrete", cfg.DiasAntecedenciaLembrete),
                Db.P("@dias_atraso",   cfg.DiasAtrasoCobranca),
                Db.P("@email",         cfg.EnviarEmail ? 1 : 0),
                Db.P("@whatsapp",      cfg.EnviarWhatsapp ? 1 : 0),
                Db.P("@horario",       cfg.HorarioEnvio),
                Db.P("@ativo",         cfg.Ativo ? 1 : 0),
            ]);
        }

        // ──────────────────────────────────────────────
        //  CRUD Contratos
        // ──────────────────────────────────────────────

        public static async Task<long> InserirContratoAsync(Contrato c)
        {
            const string sql = @"
                INSERT INTO contratos
                    (aluno_id, descricao, valor_total, data_inicio, data_fim,
                     quantidade_parcelas, dia_vencimento, status, observacao)
                VALUES
                    (@aluno_id, @descricao, @valor_total, @data_inicio, @data_fim,
                     @qtd_parcelas, @dia_vencimento, @status, @observacao);
                SELECT last_insert_rowid();";

            var contratoId = await Db.ExecuteScalarAsync<long>(sql,
            [
                Db.P("@aluno_id",       c.AlunoId),
                Db.P("@descricao",      c.Descricao),
                Db.P("@valor_total",    c.ValorTotal),
                Db.P("@data_inicio",    c.DataInicio),
                Db.P("@data_fim",       c.DataFim),
                Db.P("@qtd_parcelas",   c.QuantidadeParcelas),
                Db.P("@dia_vencimento", c.DiaVencimento),
                Db.P("@status",         c.Status),
                Db.P("@observacao",     c.Observacao),
            ]);

            // Gerar parcelas automaticamente
            await GerarParcelasAsync(contratoId, c);

            return contratoId;
        }

        public static async Task AtualizarContratoAsync(Contrato c)
        {
            const string sql = @"
                UPDATE contratos
                SET descricao           = @descricao,
                    valor_total         = @valor_total,
                    data_inicio         = @data_inicio,
                    data_fim            = @data_fim,
                    quantidade_parcelas = @qtd_parcelas,
                    dia_vencimento      = @dia_vencimento,
                    status              = @status,
                    observacao          = @observacao,
                    atualizado_em       = datetime('now','localtime')
                WHERE id = @id;";

            await Db.ExecuteNonQueryAsync(sql,
            [
                Db.P("@id",             c.Id),
                Db.P("@descricao",      c.Descricao),
                Db.P("@valor_total",    c.ValorTotal),
                Db.P("@data_inicio",    c.DataInicio),
                Db.P("@data_fim",       c.DataFim),
                Db.P("@qtd_parcelas",   c.QuantidadeParcelas),
                Db.P("@dia_vencimento", c.DiaVencimento),
                Db.P("@status",         c.Status),
                Db.P("@observacao",     c.Observacao),
            ]);
        }

        public static async Task<List<Contrato>> ListarContratosAsync(string? filtroStatus = null, long? alunoId = null)
        {
            var sql = @"
                SELECT c.*, a.nome AS nome_aluno
                FROM contratos c
                INNER JOIN alunos a ON a.id = c.aluno_id
                WHERE 1=1";

            var parametros = new List<SqliteParameter>();

            if (!string.IsNullOrWhiteSpace(filtroStatus))
            {
                sql += " AND c.status = @status";
                parametros.Add(Db.P("@status", filtroStatus));
            }

            if (alunoId.HasValue)
            {
                sql += " AND c.aluno_id = @aluno_id";
                parametros.Add(Db.P("@aluno_id", alunoId.Value));
            }

            sql += " ORDER BY c.criado_em DESC;";

            var lista = new List<Contrato>();

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqliteCommand(sql, conn);
            foreach (var p in parametros)
                cmd.Parameters.Add(p);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(LerContrato(reader));
            }

            return lista;
        }

        public static async Task<Contrato?> ObterContratoPorIdAsync(long id)
        {
            const string sql = @"
                SELECT c.*, a.nome AS nome_aluno
                FROM contratos c
                INNER JOIN alunos a ON a.id = c.aluno_id
                WHERE c.id = @id;";

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            return LerContrato(reader);
        }

        public static async Task ExcluirContratoAsync(long id)
        {
            await Db.ExecuteInTransactionAsync(async tx =>
            {
                await Db.ExecuteNonQueryAsync(
                    "DELETE FROM parcelas WHERE contrato_id = @id;",
                    [Db.P("@id", id)], tx);

                await Db.ExecuteNonQueryAsync(
                    "DELETE FROM contratos WHERE id = @id;",
                    [Db.P("@id", id)], tx);
            });
        }

        // ──────────────────────────────────────────────
        //  CRUD Parcelas
        // ──────────────────────────────────────────────

        public static async Task<List<Parcela>> ListarParcelasAsync(
            long? contratoId = null, long? alunoId = null, string? filtroStatus = null)
        {
            var sql = @"
                SELECT p.*, a.nome AS nome_aluno, c.descricao AS descricao_contrato,
                       c.quantidade_parcelas AS total_parcelas
                FROM parcelas p
                INNER JOIN alunos a ON a.id = p.aluno_id
                INNER JOIN contratos c ON c.id = p.contrato_id
                WHERE 1=1";

            var parametros = new List<SqliteParameter>();

            if (contratoId.HasValue)
            {
                sql += " AND p.contrato_id = @contrato_id";
                parametros.Add(Db.P("@contrato_id", contratoId.Value));
            }

            if (alunoId.HasValue)
            {
                sql += " AND p.aluno_id = @aluno_id";
                parametros.Add(Db.P("@aluno_id", alunoId.Value));
            }

            if (!string.IsNullOrWhiteSpace(filtroStatus))
            {
                sql += " AND p.status = @status";
                parametros.Add(Db.P("@status", filtroStatus));
            }

            sql += " ORDER BY p.data_vencimento ASC;";

            var lista = new List<Parcela>();

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqliteCommand(sql, conn);
            foreach (var p in parametros)
                cmd.Parameters.Add(p);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(LerParcela(reader));
            }

            return lista;
        }

        /// <summary>
        /// Registra o pagamento de uma parcela e envia notificação de confirmação.
        /// </summary>
        public static async Task RegistrarPagamentoAsync(
            long parcelaId, decimal valorPago, string formaPagamento, string? observacao = null)
        {
            const string sql = @"
                UPDATE parcelas
                SET data_pagamento  = @data_pgto,
                    valor_pago      = @valor_pago,
                    status          = 'PAGA',
                    forma_pagamento = @forma,
                    observacao      = @obs,
                    atualizado_em   = datetime('now','localtime')
                WHERE id = @id;";

            string dataPagamento = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            await Db.ExecuteNonQueryAsync(sql,
            [
                Db.P("@id",         parcelaId),
                Db.P("@data_pgto",  dataPagamento),
                Db.P("@valor_pago", valorPago),
                Db.P("@forma",      formaPagamento),
                Db.P("@obs",        observacao),
            ]);

            // Verificar se todas as parcelas do contrato foram pagas
            await VerificarConclusaoContratoAsync(parcelaId);
        }

        /// <summary>
        /// Estorna o pagamento de uma parcela, voltando ao status PENDENTE ou VENCIDA.
        /// </summary>
        public static async Task EstornarPagamentoAsync(long parcelaId)
        {
            // Buscar data de vencimento para decidir status correto
            var vencimento = await Db.ExecuteScalarAsync<string>(
                "SELECT data_vencimento FROM parcelas WHERE id = @id;",
                [Db.P("@id", parcelaId)]);

            string novoStatus = "PENDENTE";
            if (DateTime.TryParse(vencimento, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtVenc)
                && dtVenc.Date < DateTime.Today)
            {
                novoStatus = "VENCIDA";
            }

            const string sql = @"
                UPDATE parcelas
                SET data_pagamento  = NULL,
                    valor_pago      = NULL,
                    status          = @status,
                    forma_pagamento = NULL,
                    atualizado_em   = datetime('now','localtime')
                WHERE id = @id;";

            await Db.ExecuteNonQueryAsync(sql,
            [
                Db.P("@id",     parcelaId),
                Db.P("@status", novoStatus),
            ]);
        }

        // ──────────────────────────────────────────────
        //  Resumo / Dashboard
        // ──────────────────────────────────────────────

        public static async Task<ResumoFinanceiro> ObterResumoAsync()
        {
            const string sql = @"
                SELECT
                    (SELECT COUNT(*) FROM contratos) AS total_contratos,
                    (SELECT COUNT(*) FROM contratos WHERE status = 'ATIVO') AS contratos_ativos,
                    (SELECT COALESCE(SUM(valor_total), 0) FROM contratos WHERE status = 'ATIVO') AS receita_total,
                    (SELECT COALESCE(SUM(valor_pago), 0) FROM parcelas WHERE status = 'PAGA') AS total_recebido,
                    (SELECT COALESCE(SUM(valor_parcela), 0) FROM parcelas WHERE status = 'PENDENTE') AS total_pendente,
                    (SELECT COALESCE(SUM(valor_parcela), 0) FROM parcelas WHERE status = 'VENCIDA') AS total_vencido,
                    (SELECT COUNT(*) FROM parcelas WHERE status = 'VENCIDA') AS parcelas_vencidas,
                    (SELECT COUNT(*) FROM parcelas WHERE status = 'PENDENTE'
                        AND date(data_vencimento) BETWEEN date('now') AND date('now','+7 days')) AS parcelas_a_vencer;";

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqliteCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return new ResumoFinanceiro();

            return new ResumoFinanceiro
            {
                TotalContratos  = Convert.ToInt32(reader["total_contratos"]),
                ContratosAtivos = Convert.ToInt32(reader["contratos_ativos"]),
                ReceitaTotal    = Convert.ToDecimal(reader["receita_total"], CultureInfo.InvariantCulture),
                TotalRecebido   = Convert.ToDecimal(reader["total_recebido"], CultureInfo.InvariantCulture),
                TotalPendente   = Convert.ToDecimal(reader["total_pendente"], CultureInfo.InvariantCulture),
                TotalVencido    = Convert.ToDecimal(reader["total_vencido"], CultureInfo.InvariantCulture),
                ParcelasVencidas = Convert.ToInt32(reader["parcelas_vencidas"]),
                ParcelasAVencer  = Convert.ToInt32(reader["parcelas_a_vencer"]),
            };
        }

        /// <summary>
        /// Atualiza parcelas PENDENTE cujo vencimento já passou para VENCIDA.
        /// Deve ser chamado periodicamente (ex.: ao abrir a tela financeira).
        /// </summary>
        public static async Task<int> AtualizarParcelasVencidasAsync()
        {
            const string sql = @"
                UPDATE parcelas
                SET status        = 'VENCIDA',
                    atualizado_em = datetime('now','localtime')
                WHERE status = 'PENDENTE'
                  AND date(data_vencimento) < date('now');";

            return await Db.ExecuteNonQueryAsync(sql);
        }

        // ──────────────────────────────────────────────
        //  Métodos privados
        // ──────────────────────────────────────────────

        /// <summary>
        /// Gera automaticamente as parcelas com base no contrato.
        /// </summary>
        private static async Task GerarParcelasAsync(long contratoId, Contrato c)
        {
            decimal valorParcela = Math.Round(c.ValorTotal / c.QuantidadeParcelas, 2);
            decimal soma = 0;

            if (!DateTime.TryParse(c.DataInicio, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dataBase))
                dataBase = DateTime.Today;

            for (int i = 1; i <= c.QuantidadeParcelas; i++)
            {
                // Última parcela absorve a diferença de arredondamento
                decimal valor = i == c.QuantidadeParcelas
                    ? c.ValorTotal - soma
                    : valorParcela;

                soma += valor;

                // Calcular data de vencimento: mês base + i, dia fixo
                var dataVencimento = CalcularDataVencimento(dataBase, i, c.DiaVencimento);

                string status = dataVencimento.Date < DateTime.Today ? "VENCIDA" : "PENDENTE";

                const string sql = @"
                    INSERT INTO parcelas
                        (contrato_id, aluno_id, numero_parcela, valor_parcela,
                         data_vencimento, status)
                    VALUES
                        (@contrato_id, @aluno_id, @numero, @valor,
                         @vencimento, @status);";

                await Db.ExecuteNonQueryAsync(sql,
                [
                    Db.P("@contrato_id", contratoId),
                    Db.P("@aluno_id",    c.AlunoId),
                    Db.P("@numero",      i),
                    Db.P("@valor",       valor),
                    Db.P("@vencimento",  dataVencimento.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                    Db.P("@status",      status),
                ]);
            }
        }

        private static DateTime CalcularDataVencimento(DateTime dataBase, int numeroParcela, int diaVencimento)
        {
            var mesAlvo = dataBase.AddMonths(numeroParcela);
            int diasNoMes = DateTime.DaysInMonth(mesAlvo.Year, mesAlvo.Month);
            int dia = Math.Min(diaVencimento, diasNoMes);
            return new DateTime(mesAlvo.Year, mesAlvo.Month, dia);
        }

        private static async Task VerificarConclusaoContratoAsync(long parcelaId)
        {
            var contratoId = await Db.ExecuteScalarAsync<long>(
                "SELECT contrato_id FROM parcelas WHERE id = @id;",
                [Db.P("@id", parcelaId)]);

            if (contratoId == 0) return;

            var pendentes = await Db.ExecuteScalarAsync<long>(
                "SELECT COUNT(*) FROM parcelas WHERE contrato_id = @cid AND status IN ('PENDENTE','VENCIDA');",
                [Db.P("@cid", contratoId)]);

            if (pendentes == 0)
            {
                await Db.ExecuteNonQueryAsync(@"
                    UPDATE contratos
                    SET status        = 'CONCLUIDO',
                        atualizado_em = datetime('now','localtime')
                    WHERE id = @id;",
                    [Db.P("@id", contratoId)]);
            }
        }

        private static Contrato LerContrato(SqliteDataReader reader)
        {
            return new Contrato
            {
                Id                 = reader.GetInt64(reader.GetOrdinal("id")),
                AlunoId            = reader.GetInt64(reader.GetOrdinal("aluno_id")),
                NomeAluno          = reader["nome_aluno"]?.ToString() ?? "",
                Descricao          = reader["descricao"]?.ToString() ?? "",
                ValorTotal         = Convert.ToDecimal(reader["valor_total"], CultureInfo.InvariantCulture),
                DataInicio         = reader["data_inicio"]?.ToString() ?? "",
                DataFim            = reader["data_fim"]?.ToString(),
                QuantidadeParcelas = Convert.ToInt32(reader["quantidade_parcelas"]),
                DiaVencimento      = Convert.ToInt32(reader["dia_vencimento"]),
                Status             = reader["status"]?.ToString() ?? "ATIVO",
                Observacao         = reader["observacao"]?.ToString(),
                CriadoEm           = reader["criado_em"]?.ToString(),
            };
        }

        private static Parcela LerParcela(SqliteDataReader reader)
        {
            return new Parcela
            {
                Id                = reader.GetInt64(reader.GetOrdinal("id")),
                ContratoId        = reader.GetInt64(reader.GetOrdinal("contrato_id")),
                AlunoId           = reader.GetInt64(reader.GetOrdinal("aluno_id")),
                NomeAluno         = reader["nome_aluno"]?.ToString() ?? "",
                NumeroParcela     = Convert.ToInt32(reader["numero_parcela"]),
                ValorParcela      = Convert.ToDecimal(reader["valor_parcela"], CultureInfo.InvariantCulture),
                DataVencimento    = reader["data_vencimento"]?.ToString() ?? "",
                DataPagamento     = reader["data_pagamento"]?.ToString(),
                ValorPago         = reader["valor_pago"] is DBNull ? null : Convert.ToDecimal(reader["valor_pago"], CultureInfo.InvariantCulture),
                Status            = reader["status"]?.ToString() ?? "PENDENTE",
                FormaPagamento    = reader["forma_pagamento"]?.ToString(),
                Observacao        = reader["observacao"]?.ToString(),
                NotificadoEm     = reader["notificado_em"]?.ToString(),
                DescricaoContrato = reader["descricao_contrato"]?.ToString(),
                TotalParcelas     = Convert.ToInt32(reader["total_parcelas"]),
            };
        }
    }
}
