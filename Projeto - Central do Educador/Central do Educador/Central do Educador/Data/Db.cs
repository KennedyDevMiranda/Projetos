//using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Central_do_Educador.Data
{
    public static class Db
    {
        // String de conexão para o arquivo do banco "central" 
        public static string DbPath { get; } = @"Data Source=\\100.104.17.8\ce\central.db";
        public static string ConnectionString => DbPath;

        public static async Task InitializeAsync()
        {
            // Em SQLite usamos arquivo local; garantir tabelas existentes.
            await CreateTablesIfNotExistsAsync();
        }

        public static async Task<SqliteConnection> OpenConnectionAsync()
        {
            var con = new SqliteConnection(ConnectionString);
            try
            {
                await con.OpenAsync();

                // Ativar verificação de chaves estrangeiras
                using var cmd = con.CreateCommand();
                cmd.CommandText = "PRAGMA foreign_keys = ON;";
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqliteException)
            {
                // Log mex.Message, mex.SqliteErrorCode, mex.InnerException?.Message, mex.StackTrace
                throw;
            }
            catch (OperationCanceledException)
            {
                // Log cancellation reason (likely connect timeout)
                throw;
            }
            catch (Exception)
            {
                // Generic log
                throw;
            }
            return con;
        }

        public static SqliteParameter P(string name, object? value)
            => new SqliteParameter(name, value ?? DBNull.Value);

        public static async Task<int> ExecuteNonQueryAsync(
            string sql,
            IEnumerable<SqliteParameter>? parameters = null,
            SqliteTransaction? tx = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL não pode ser vazio.", nameof(sql));

            if (tx != null)
            {
                using var cmd = tx.Connection!.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = sql;
                AddParameters(cmd, parameters);
                return await cmd.ExecuteNonQueryAsync();
            }

            using var con = await OpenConnectionAsync();
            using var cmd2 = con.CreateCommand();
            cmd2.CommandText = sql;
            AddParameters(cmd2, parameters);
            return await cmd2.ExecuteNonQueryAsync();
        }

        public static async Task<T?> ExecuteScalarAsync<T>(
            string sql,
            IEnumerable<SqliteParameter>? parameters = null,
            SqliteTransaction? tx = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL não pode ser vazio.", nameof(sql));

            object? result;

            if (tx != null)
            {
                using var cmd = tx.Connection!.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = sql;
                AddParameters(cmd, parameters);
                result = await cmd.ExecuteScalarAsync();
            }
            else
            {
                using var con = await OpenConnectionAsync();
                using var cmd2 = con.CreateCommand();
                cmd2.CommandText = sql;
                AddParameters(cmd2, parameters);
                result = await cmd2.ExecuteScalarAsync();
            }

            if (result == null || result == DBNull.Value)
                return default;

            return (T)Convert.ChangeType(result, typeof(T), CultureInfo.InvariantCulture);
        }

        public static async Task<DataTable> QueryDataTableAsync(
            string sql,
            IEnumerable<SqliteParameter>? parameters = null,
            SqliteTransaction? tx = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL não pode ser vazio.", nameof(sql));

            var dt = new DataTable();

            if (tx != null)
            {
                using var cmd = tx.Connection!.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = sql;
                AddParameters(cmd, parameters);

                using var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);
                return dt;
            }

            using var con = await OpenConnectionAsync();
            using var cmd2 = con.CreateCommand();
            cmd2.CommandText = sql;
            AddParameters(cmd2, parameters);

            using var reader2 = await cmd2.ExecuteReaderAsync();
            dt.Load(reader2);
            return dt;
        }

        public static async Task ExecuteInTransactionAsync(Func<SqliteTransaction, Task> action)
        {
            using var con = await OpenConnectionAsync();
            using var tx = con.BeginTransaction(); // BeginTransaction sync no Sqlite

            try
            {
                await action(tx);
                tx.Commit();
            }
            catch
            {
                try { tx.Rollback(); } catch { }
                throw;
            }
        }

        private static void AddParameters(SqliteCommand cmd, IEnumerable<SqliteParameter>? parameters)
        {
            if (parameters == null) return;
            foreach (var p in parameters)
                cmd.Parameters.Add(p);
        }

        private static async Task CreateTablesIfNotExistsAsync()
        {
            var sqlAlunos = @"
                CREATE TABLE IF NOT EXISTS alunos (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    nome TEXT NOT NULL,
                    email TEXT,
                    numero_aluno TEXT,
                    NomeResponsavel TEXT,
                    numero_responsavel TEXT,
                    ativo INTEGER NOT NULL DEFAULT 1
                );;";

            var sqlReposicao = @"
                CREATE TABLE IF NOT EXISTS reposicao (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    aluno TEXT,
                    aluno_id INTEGER,
                    UsuarioResponsavel TEXT,
                    usuario_id INTEGER,
                    data_falta TEXT NOT NULL,
                    data_registro TEXT NOT NULL,
                    Data_Reposicao TEXT NULL,
                    quantidade INTEGER NOT NULL DEFAULT 1,
                    lancado INTEGER NOT NULL DEFAULT 0,
                    Historico INTEGER NOT NULL DEFAULT 0,
                    Observacao TEXT,
                    valor_total NUMERIC NOT NULL DEFAULT 0,
                    FOREIGN KEY (aluno_id) REFERENCES alunos(id),
                    FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
                );";

            var sqlEntregaLivros = @"
                CREATE TABLE IF NOT EXISTS EntregaLivros (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Aluno TEXT NOT NULL,
                    Materia TEXT,
                    AulasConcluidas TEXT,
                    DiasAgendamento TEXT,
                    HorasAgendamento TEXT,
                    ProximaMateria TEXT,
                    Apostila TEXT,
                    EntregaFisica TEXT NOT NULL DEFAULT 'N'
                );";

            var sqlAgendamentos = @"
                CREATE TABLE IF NOT EXISTS agendamentos (
                    id          INTEGER PRIMARY KEY AUTOINCREMENT,
                    aluno_id    INTEGER NOT NULL,
                    data_hora   TEXT NOT NULL,
                    tipo        TEXT NOT NULL,
                    local       TEXT,
                    observacao  TEXT,
                    status      TEXT NOT NULL DEFAULT 'PENDENTE',
                    criado_em   TEXT NOT NULL DEFAULT (datetime('now','localtime')),
                    atualizado_em TEXT,
                    FOREIGN KEY (aluno_id) REFERENCES alunos(id)
                );";

            var sqlTemplatesMensagem = @"
                CREATE TABLE IF NOT EXISTS templates_mensagem (
                    id      INTEGER PRIMARY KEY AUTOINCREMENT,
                    codigo  TEXT NOT NULL,
                    canal   TEXT NOT NULL,
                    assunto TEXT,
                    corpo   TEXT NOT NULL,
                    ativo   INTEGER NOT NULL DEFAULT 1,
                    UNIQUE(codigo, canal)
                );";

                var sqlNotificacoes = @"
                CREATE TABLE IF NOT EXISTS notificacoes (
                    id              INTEGER PRIMARY KEY AUTOINCREMENT,
                    agendamento_id  INTEGER NOT NULL,
                    canal           TEXT NOT NULL,
                    template_id     INTEGER,
                    titulo          TEXT,
                    mensagem        TEXT NOT NULL,
                    status          TEXT NOT NULL DEFAULT 'PENDENTE',
                    criado_em       TEXT NOT NULL DEFAULT (datetime('now')),
                    enviado_em      TEXT,
                    FOREIGN KEY (agendamento_id) REFERENCES agendamentos(id),
                    FOREIGN KEY (template_id) REFERENCES templates_mensagem(id)
                );";

            var sqlIndexNotificacoes = @"
                CREATE INDEX IF NOT EXISTS ix_notif_status
                ON notificacoes(status, canal);";

            var sqlNotificacaoDestinatarios = @"
                CREATE TABLE IF NOT EXISTS notificacao_destinatarios (
                    id              INTEGER PRIMARY KEY AUTOINCREMENT,
                    notificacao_id  INTEGER NOT NULL,
                    tipo_pessoa     TEXT NOT NULL,
                    nome            TEXT,
                    email           TEXT,
                    telefone        TEXT,
                    status          TEXT NOT NULL DEFAULT 'PENDENTE',
                    enviado_em      TEXT,
                    entregue_em     TEXT,
                    erro            TEXT,
                    FOREIGN KEY (notificacao_id) REFERENCES notificacoes(id)
                );";

            var sqlIndexDestNotif = @"
                CREATE INDEX IF NOT EXISTS ix_dest_notif
                ON notificacao_destinatarios(notificacao_id, tipo_pessoa);";

            var sqlNotificacaoTentativas = @"
                CREATE TABLE IF NOT EXISTS notificacao_tentativas (
                    id                  INTEGER PRIMARY KEY AUTOINCREMENT,
                    destinatario_id     INTEGER NOT NULL,
                    tentativa_num       INTEGER NOT NULL DEFAULT 1,
                    status              TEXT NOT NULL,
                    provider            TEXT,
                    provider_message_id TEXT,
                    retorno_raw         TEXT,
                    criado_em           TEXT NOT NULL DEFAULT (datetime('now')),
                    FOREIGN KEY (destinatario_id) REFERENCES notificacao_destinatarios(id)
                );";

            var sqlIndexTentDest = @"
                CREATE INDEX IF NOT EXISTS ix_tent_dest
                ON notificacao_tentativas(destinatario_id, criado_em);";

            var sqlConfiguracaoEmail = @"
                CREATE TABLE IF NOT EXISTS configuracao_email (
                    id              INTEGER PRIMARY KEY CHECK (id = 1),
                    smtp_host       TEXT NOT NULL DEFAULT '',
                    smtp_porta      INTEGER NOT NULL DEFAULT 587,
                    usar_ssl        INTEGER NOT NULL DEFAULT 1,
                    usuario         TEXT NOT NULL DEFAULT '',
                    senha           TEXT NOT NULL DEFAULT '',
                    remetente_nome  TEXT NOT NULL DEFAULT 'Central do Educador',
                    remetente_email TEXT NOT NULL DEFAULT '',
                    ativo           INTEGER NOT NULL DEFAULT 0
                );";

            var sqlConfiguracaoWhatsApp = @"
                CREATE TABLE IF NOT EXISTS configuracao_whatsapp (
                    id              INTEGER PRIMARY KEY CHECK (id = 1),
                    modo            TEXT NOT NULL DEFAULT 'LINK',
                    api_url         TEXT NOT NULL DEFAULT '',
                    api_key         TEXT NOT NULL DEFAULT '',
                    instancia       TEXT NOT NULL DEFAULT '',
                    ddi_padrao      TEXT NOT NULL DEFAULT '55',
                    ativo           INTEGER NOT NULL DEFAULT 0
                );";

            var sqlHistoricoEnvioChatbot = @"
                CREATE TABLE IF NOT EXISTS historico_envio_chatbot (
                    id                  INTEGER PRIMARY KEY AUTOINCREMENT,
                    nome_aluno          TEXT,
                    numero_aluno        TEXT,
                    numero_responsavel  TEXT,
                    email               TEXT,
                    mensagem            TEXT,
                    status              TEXT,
                    erro                TEXT,
                    operador            TEXT,
                    operador_id         INTEGER,
                    data_hora           TEXT NOT NULL DEFAULT (datetime('now')),
                    FOREIGN KEY (operador_id) REFERENCES usuarios(id)
                );";

            // ── Tabela relatos_falhas ──
            var sqlRelatosFalhas = @"
                CREATE TABLE IF NOT EXISTS relatos_falhas (
                    id              INTEGER PRIMARY KEY AUTOINCREMENT,
                    usuario_id      INTEGER NOT NULL,
                    usuario_nome    TEXT NOT NULL,
                    titulo          TEXT NOT NULL,
                    descricao       TEXT NOT NULL,
                    categoria       TEXT NOT NULL DEFAULT 'Bug',
                    status          TEXT NOT NULL DEFAULT 'ABERTO',
                    resposta_adm    TEXT,
                    criado_em       TEXT NOT NULL DEFAULT (datetime('now','localtime')),
                    atualizado_em   TEXT,
                    FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
                );";

            // ── NOVAS TABELAS FINANCEIRAS ──
            var sqlContratos = @"
                CREATE TABLE IF NOT EXISTS contratos (
                    id                  INTEGER PRIMARY KEY AUTOINCREMENT,
                    aluno_id            INTEGER NOT NULL,
                    descricao           TEXT NOT NULL,
                    valor_total         NUMERIC NOT NULL,
                    data_inicio         TEXT NOT NULL,
                    data_fim            TEXT,
                    quantidade_parcelas INTEGER NOT NULL DEFAULT 1,
                    dia_vencimento      INTEGER NOT NULL DEFAULT 10,
                    status              TEXT NOT NULL DEFAULT 'ATIVO',
                    observacao          TEXT,
                    criado_em           TEXT NOT NULL DEFAULT (datetime('now','localtime')),
                    atualizado_em       TEXT,
                    FOREIGN KEY (aluno_id) REFERENCES alunos(id)
                );";

            var sqlParcelas = @"
                CREATE TABLE IF NOT EXISTS parcelas (
                    id                  INTEGER PRIMARY KEY AUTOINCREMENT,
                    contrato_id         INTEGER NOT NULL,
                    aluno_id            INTEGER NOT NULL,
                    numero_parcela      INTEGER NOT NULL,
                    valor_parcela       NUMERIC NOT NULL,
                    data_vencimento     TEXT NOT NULL,
                    data_pagamento      TEXT,
                    valor_pago          NUMERIC,
                    status              TEXT NOT NULL DEFAULT 'PENDENTE',
                    forma_pagamento     TEXT,
                    observacao          TEXT,
                    notificado_em       TEXT,
                    criado_em           TEXT NOT NULL DEFAULT (datetime('now','localtime')),
                    atualizado_em       TEXT,
                    FOREIGN KEY (contrato_id) REFERENCES contratos(id),
                    FOREIGN KEY (aluno_id) REFERENCES alunos(id)
                );";

            var sqlIndexParcelasVencimento = @"
                CREATE INDEX IF NOT EXISTS ix_parcelas_vencimento
                ON parcelas(data_vencimento, status);";

            var sqlIndexParcelasAluno = @"
                CREATE INDEX IF NOT EXISTS ix_parcelas_aluno
                ON parcelas(aluno_id, status);";

            var sqlConfiguracaoFinanceiro = @"
                CREATE TABLE IF NOT EXISTS configuracao_financeiro (
                    id                          INTEGER PRIMARY KEY CHECK (id = 1),
                    dias_antecedencia_lembrete  INTEGER NOT NULL DEFAULT 3,
                    dias_atraso_cobranca        INTEGER NOT NULL DEFAULT 1,
                    enviar_email                INTEGER NOT NULL DEFAULT 1,
                    enviar_whatsapp             INTEGER NOT NULL DEFAULT 1,
                    horario_envio               TEXT NOT NULL DEFAULT '09:00',
                    ativo                       INTEGER NOT NULL DEFAULT 1
                );";

            await ExecuteNonQueryAsync(sqlAlunos);
            await ExecuteNonQueryAsync(sqlReposicao);
            await ExecuteNonQueryAsync(sqlEntregaLivros);
            await ExecuteNonQueryAsync(sqlAgendamentos);
            await ExecuteNonQueryAsync(sqlTemplatesMensagem);
            await ExecuteNonQueryAsync(sqlNotificacoes);
            await ExecuteNonQueryAsync(sqlIndexNotificacoes);
            await ExecuteNonQueryAsync(sqlNotificacaoDestinatarios);
            await ExecuteNonQueryAsync(sqlIndexDestNotif);
            await ExecuteNonQueryAsync(sqlNotificacaoTentativas);
            await ExecuteNonQueryAsync(sqlIndexTentDest);
            await ExecuteNonQueryAsync(sqlConfiguracaoEmail);
            await ExecuteNonQueryAsync(sqlConfiguracaoWhatsApp);
            await ExecuteNonQueryAsync(sqlHistoricoEnvioChatbot);
            await ExecuteNonQueryAsync(sqlRelatosFalhas);
            
            // Executar tabelas financeiras
            await ExecuteNonQueryAsync(sqlContratos);
            await ExecuteNonQueryAsync(sqlParcelas);
            await ExecuteNonQueryAsync(sqlIndexParcelasVencimento);
            await ExecuteNonQueryAsync(sqlIndexParcelasAluno);
            await ExecuteNonQueryAsync(sqlConfiguracaoFinanceiro);

            // Seed: inserir templates padrão se tabela estiver vazia
            await SeedTemplatesAsync();

            // Seed: inserir registro padrão de configuração de e-mail
            await ExecuteNonQueryAsync(
                "INSERT OR IGNORE INTO configuracao_email (id) VALUES (1);");

            // Seed: inserir registro padrão de configuração de WhatsApp
            await ExecuteNonQueryAsync(
                "INSERT OR IGNORE INTO configuracao_whatsapp (id) VALUES (1);");

            // Seed: inserir registro padrão de configuração financeiro
            await ExecuteNonQueryAsync(
                "INSERT OR IGNORE INTO configuracao_financeiro (id) VALUES (1);");

            // Garantir compatibilidade em bases antigas: adicionar colunas que podem faltar
            using var con = await OpenConnectionAsync();

            // ── alunos: adicionar NomeResponsavel se ausente ──
            await AdicionarColunaSeFaltar(con, "alunos", "NomeResponsavel", "ALTER TABLE alunos ADD COLUMN NomeResponsavel TEXT;");

            // ── reposicao: adicionar colunas opcionais ──
            using var cmd = con.CreateCommand();
            cmd.CommandText = "PRAGMA table_info('reposicao');";
            using var reader = await cmd.ExecuteReaderAsync();

            var existing = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            while (await reader.ReadAsync())
            {
                var nameObj = reader["name"];
                if (nameObj != null)
                    existing.Add(nameObj.ToString()!);
            }

            var addStatements = new List<string>();
            if (!existing.Contains("aluno")) addStatements.Add("ALTER TABLE reposicao ADD COLUMN aluno TEXT;");
            if (!existing.Contains("aluno_id")) addStatements.Add("ALTER TABLE reposicao ADD COLUMN aluno_id INTEGER;");
            if (!existing.Contains("UsuarioResponsavel")) addStatements.Add("ALTER TABLE reposicao ADD COLUMN UsuarioResponsavel TEXT;");
            if (!existing.Contains("usuario_id")) addStatements.Add("ALTER TABLE reposicao ADD COLUMN usuario_id INTEGER;");
            if (!existing.Contains("Data_Reposicao")) addStatements.Add("ALTER TABLE reposicao ADD COLUMN Data_Reposicao TEXT;");
            if (!existing.Contains("Observacao")) addStatements.Add("ALTER TABLE reposicao ADD COLUMN Observacao TEXT;");
            if (!existing.Contains("valor_total")) addStatements.Add("ALTER TABLE reposicao ADD COLUMN valor_total NUMERIC NOT NULL DEFAULT 0;");

            foreach (var s in addStatements)
            {
                using var c2 = con.CreateCommand();
                c2.CommandText = s;
                await c2.ExecuteNonQueryAsync();
            }

            // ── usuarios: adicionar coluna nivel se ausente ──
            await AdicionarColunaSeFaltar(con, "usuarios", "nivel",
                "ALTER TABLE usuarios ADD COLUMN nivel TEXT NOT NULL DEFAULT 'OPERADOR';");
        }

        /// <summary>
        /// Insere templates de mensagem padrão se a tabela estiver vazia.
        /// </summary>
        private static async Task SeedTemplatesAsync()
        {
            var count = await ExecuteScalarAsync<long>("SELECT COUNT(*) FROM templates_mensagem;");
            if (count > 0) return;

            var templates = new[]
            {
                ("CONFIRMACAO_AGENDAMENTO", "EMAIL",
                 "Confirmação de Agendamento - {tipo}",
                 "Olá {aluno},\n\nSeu agendamento de {tipo} foi confirmado.\n\nData/Hora: {data_hora}\nLocal: {local}\n\nQualquer dúvida, entre em contato.\n\nAtenciosamente,\nCentral do Educador"),

                ("CONFIRMACAO_AGENDAMENTO", "WHATSAPP",
                 (string?)null,
                 "Olá *{aluno}*! ✅\n\nSeu agendamento de *{tipo}* foi confirmado:\n📅 {data_hora}\n📍 {local}\n\nDúvidas? Responda esta mensagem."),

                ("LEMBRETE_AGENDAMENTO", "EMAIL",
                 "Lembrete: {tipo} amanhã",
                 "Olá {aluno},\n\nLembramos que você tem um(a) {tipo} agendado(a) para amanhã.\n\nData/Hora: {data_hora}\nLocal: {local}\n\nNão falte!\n\nAtenciosamente,\nCentral do Educador"),

                ("LEMBRETE_AGENDAMENTO", "WHATSAPP",
                 (string?)null,
                 "Oi *{aluno}*! ⏰\n\nLembrete: você tem *{tipo}* amanhã.\n📅 {data_hora}\n📍 {local}\n\nTe esperamos!"),

                ("CANCELAMENTO_AGENDAMENTO", "EMAIL",
                 "Agendamento Cancelado - {tipo}",
                 "Olá {aluno},\n\nInformamos que seu agendamento de {tipo} em {data_hora} foi cancelado.\n\nMotivo: {observacao}\n\nPara reagendar, entre em contato.\n\nAtenciosamente,\nCentral do Educador"),

                ("CANCELAMENTO_AGENDAMENTO", "WHATSAPP",
                 (string?)null,
                 "Oi *{aluno}* ❌\n\nSeu agendamento de *{tipo}* em {data_hora} foi *cancelado*.\n\nMotivo: {observacao}\n\nPara reagendar, responda esta mensagem."),

                // ── TEMPLATES FINANCEIROS ──
                ("LEMBRETE_VENCIMENTO", "EMAIL",
                 "Lembrete: Parcela vence em {dias_restantes} dias",
                 "Olá {aluno},\n\nLembramos que você tem uma parcela no valor de R$ {valor_parcela} com vencimento em {data_vencimento}.\n\nParcela: {numero_parcela}/{total_parcelas}\nDescrição: {descricao_contrato}\n\nPara efetuar o pagamento, entre em contato.\n\nAtenciosamente,\nCentral do Educador"),

                ("LEMBRETE_VENCIMENTO", "WHATSAPP",
                 (string?)null,
                 "Olá *{aluno}*! 📋\n\n⏰ Lembrete: Sua parcela vence em *{dias_restantes} dias*\n\n💰 Valor: R$ {valor_parcela}\n📅 Vencimento: {data_vencimento}\n📝 Parcela {numero_parcela}/{total_parcelas}\n\nDúvidas? Responda esta mensagem."),

                ("COBRANCA_VENCIDA", "EMAIL",
                 "Parcela em Atraso - Ação Necessária",
                 "Olá {aluno},\n\nIdentificamos que a parcela no valor de R$ {valor_parcela} está em atraso desde {data_vencimento}.\n\nParcela: {numero_parcela}/{total_parcelas}\nDescrição: {descricao_contrato}\nDias em atraso: {dias_atraso}\n\nPor favor, regularize sua situação o quanto antes.\n\nAtenciosamente,\nCentral do Educador"),

                ("COBRANCA_VENCIDA", "WHATSAPP",
                 (string?)null,
                 "Olá *{aluno}* ⚠️\n\n❗ Sua parcela está em atraso\n\n💰 Valor: R$ {valor_parcela}\n📅 Vencimento: {data_vencimento}\n⏱️ Dias em atraso: {dias_atraso}\n📝 Parcela {numero_parcela}/{total_parcelas}\n\nPor favor, regularize sua situação. Responda para mais informações."),

                ("CONFIRMACAO_PAGAMENTO", "EMAIL",
                 "Pagamento Confirmado - Obrigado!",
                 "Olá {aluno},\n\nConfirmamos o recebimento do pagamento da parcela {numero_parcela}/{total_parcelas} no valor de R$ {valor_pago}.\n\nData do pagamento: {data_pagamento}\nForma de pagamento: {forma_pagamento}\n\nObrigado!\n\nAtenciosamente,\nCentral do Educador"),

                ("CONFIRMACAO_PAGAMENTO", "WHATSAPP",
                 (string?)null,
                 "Olá *{aluno}*! ✅\n\n🎉 Pagamento confirmado!\n\n💰 Valor: R$ {valor_pago}\n📅 Data: {data_pagamento}\n📝 Parcela {numero_parcela}/{total_parcelas}\n\nObrigado! 😊"),
            };

            foreach (var (codigo, canal, assunto, corpo) in templates)
            {
                await ExecuteNonQueryAsync(
                    @"INSERT INTO templates_mensagem (codigo, canal, assunto, corpo)
                      VALUES (@codigo, @canal, @assunto, @corpo);",
                    [
                        P("@codigo", codigo),
                        P("@canal", canal),
                        P("@assunto", assunto),
                        P("@corpo", corpo),
                    ]);
            }
        }

        /// <summary>
        /// Adiciona uma coluna a uma tabela existente se ela ainda não existir.
        /// </summary>
        private static async Task AdicionarColunaSeFaltar(SqliteConnection con, string tabela, string coluna, string alterSql)
        {
            using var cmd = con.CreateCommand();
            cmd.CommandText = $"PRAGMA table_info('{tabela}');";
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var nameObj = reader["name"];
                if (nameObj != null && string.Equals(nameObj.ToString(), coluna, StringComparison.OrdinalIgnoreCase))
                    return; // coluna já existe
            }

            using var cmdAlter = con.CreateCommand();
            cmdAlter.CommandText = alterSql;
            await cmdAlter.ExecuteNonQueryAsync();
        }
    }
}