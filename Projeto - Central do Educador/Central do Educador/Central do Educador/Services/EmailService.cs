using Central_do_Educador.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Data.Sqlite;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Central_do_Educador.Services
{
    /// <summary>
    /// Envia e-mails via SMTP usando MailKit.
    /// Lê a configuração da tabela configuracao_email.
    /// </summary>
    public static class EmailService
    {
        // ──────────────────────────────────────────────
        //  Modelo de configuração
        // ──────────────────────────────────────────────

        public class SmtpConfig
        {
            public string Host { get; set; } = "";
            public int Porta { get; set; } = 587;
            public bool UsarSsl { get; set; } = true;
            public string Usuario { get; set; } = "";
            public string Senha { get; set; } = "";
            public string RemetenteNome { get; set; } = "Central do Educador";
            public string RemetenteEmail { get; set; } = "";
            public bool Ativo { get; set; }

            public bool EstaConfigurado =>
                !string.IsNullOrWhiteSpace(Host) &&
                !string.IsNullOrWhiteSpace(Usuario) &&
                !string.IsNullOrWhiteSpace(Senha) &&
                !string.IsNullOrWhiteSpace(RemetenteEmail);
        }

        // ──────────────────────────────────────────────
        //  Ler / Salvar configuração
        // ──────────────────────────────────────────────

        public static async Task<SmtpConfig> ObterConfigAsync()
        {
            const string sql = @"
                SELECT smtp_host, smtp_porta, usar_ssl, usuario, senha,
                       remetente_nome, remetente_email, ativo
                FROM configuracao_email
                WHERE id = 1;";

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqliteCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return new SmtpConfig();

            return new SmtpConfig
            {
                Host           = reader["smtp_host"]?.ToString() ?? "",
                Porta          = Convert.ToInt32(reader["smtp_porta"]),
                UsarSsl        = Convert.ToInt32(reader["usar_ssl"]) == 1,
                Usuario        = reader["usuario"]?.ToString() ?? "",
                Senha          = reader["senha"]?.ToString() ?? "",
                RemetenteNome  = reader["remetente_nome"]?.ToString() ?? "Central do Educador",
                RemetenteEmail = reader["remetente_email"]?.ToString() ?? "",
                Ativo          = Convert.ToInt32(reader["ativo"]) == 1,
            };
        }

        public static async Task SalvarConfigAsync(SmtpConfig cfg)
        {
            const string sql = @"
                UPDATE configuracao_email
                SET smtp_host       = @host,
                    smtp_porta      = @porta,
                    usar_ssl        = @ssl,
                    usuario         = @usuario,
                    senha           = @senha,
                    remetente_nome  = @rem_nome,
                    remetente_email = @rem_email,
                    ativo           = @ativo
                WHERE id = 1;";

            await Db.ExecuteNonQueryAsync(sql,
            [
                Db.P("@host",      cfg.Host),
                Db.P("@porta",     cfg.Porta),
                Db.P("@ssl",       cfg.UsarSsl ? 1 : 0),
                Db.P("@usuario",   cfg.Usuario),
                Db.P("@senha",     cfg.Senha),
                Db.P("@rem_nome",  cfg.RemetenteNome),
                Db.P("@rem_email", cfg.RemetenteEmail),
                Db.P("@ativo",     cfg.Ativo ? 1 : 0),
            ]);
        }

        // ──────────────────────────────────────────────
        //  Enviar e-mail único
        // ──────────────────────────────────────────────

        /// <summary>
        /// Envia um e-mail para o destinatário informado.
        /// Lança exceção em caso de falha.
        /// </summary>
        public static async Task EnviarAsync(string destinatarioEmail, string destinatarioNome,
            string assunto, string corpoTexto, SmtpConfig? config = null)
        {
            config ??= await ObterConfigAsync();

            if (!config.Ativo)
                throw new InvalidOperationException("O envio de e-mail está desativado nas configurações.");

            if (!config.EstaConfigurado)
                throw new InvalidOperationException("Configuração SMTP incompleta. Verifique host, usuário, senha e remetente.");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.RemetenteNome, config.RemetenteEmail));
            message.To.Add(new MailboxAddress(destinatarioNome, destinatarioEmail));
            message.Subject = assunto;

            message.Body = new TextPart("plain")
            {
                Text = corpoTexto
            };

            using var client = new SmtpClient();

            var secureOption = config.UsarSsl
                ? SecureSocketOptions.StartTls
                : SecureSocketOptions.Auto;

            await client.ConnectAsync(config.Host, config.Porta, secureOption);
            await client.AuthenticateAsync(config.Usuario, config.Senha);
            await client.SendAsync(message);
            await client.DisconnectAsync(quit: true);
        }

        // ──────────────────────────────────────────────
        //  Enviar e-mail de teste
        // ──────────────────────────────────────────────

        public static async Task<string> EnviarTesteAsync(SmtpConfig config, string emailDestino)
        {
            try
            {
                await EnviarAsync(
                    emailDestino,
                    "Teste",
                    "✅ Teste - Central do Educador",
                    "Este é um e-mail de teste enviado pela Central do Educador.\n\nSe você recebeu esta mensagem, a configuração SMTP está funcionando corretamente.",
                    config);

                return "✅ E-mail de teste enviado com sucesso!";
            }
            catch (Exception ex)
            {
                return $"❌ Falha ao enviar: {ex.Message}";
            }
        }

        // ──────────────────────────────────────────────
        //  Processar notificações pendentes de e-mail
        // ──────────────────────────────────────────────

        /// <summary>
        /// Busca todas as notificações pendentes do canal EMAIL e tenta enviá-las.
        /// Atualiza o status de cada destinatário e registra tentativas.
        /// Retorna (enviados, falhas).
        /// </summary>
        public static async Task<(int enviados, int falhas)> ProcessarPendentesAsync()
        {
            var config = await ObterConfigAsync();
            if (!config.Ativo || !config.EstaConfigurado)
                return (0, 0);

            // Buscar destinatários pendentes de notificações do canal EMAIL
            const string sql = @"
                SELECT
                    nd.id           AS dest_id,
                    nd.nome         AS dest_nome,
                    nd.email        AS dest_email,
                    n.id            AS notif_id,
                    n.titulo        AS titulo,
                    n.mensagem      AS mensagem
                FROM notificacao_destinatarios nd
                INNER JOIN notificacoes n ON n.id = nd.notificacao_id
                WHERE nd.status = 'PENDENTE'
                  AND n.canal   = 'EMAIL'
                  AND nd.email IS NOT NULL
                  AND nd.email <> ''
                ORDER BY n.criado_em ASC;";

            var pendentes = new List<(long destId, long notifId, string nome, string email, string titulo, string mensagem)>();

            using (var conn = new SqliteConnection(Db.ConnectionString))
            {
                await conn.OpenAsync();
                using var cmd = new SqliteCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    pendentes.Add((
                        destId:   reader.GetInt64(reader.GetOrdinal("dest_id")),
                        notifId:  reader.GetInt64(reader.GetOrdinal("notif_id")),
                        nome:     reader["dest_nome"]?.ToString() ?? "",
                        email:    reader["dest_email"]!.ToString()!,
                        titulo:   reader["titulo"]?.ToString() ?? "Notificação",
                        mensagem: reader["mensagem"]!.ToString()!
                    ));
                }
            }

            int enviados = 0, falhas = 0;

            foreach (var p in pendentes)
            {
                string status;
                string? erro = null;

                try
                {
                    await EnviarAsync(p.email, p.nome, p.titulo, p.mensagem, config);
                    status = "ENVIADO";
                    enviados++;
                }
                catch (Exception ex)
                {
                    status = "ERRO";
                    erro = ex.Message;
                    falhas++;
                }

                // Atualizar destinatário
                await Db.ExecuteNonQueryAsync(@"
                    UPDATE notificacao_destinatarios
                    SET status     = @status,
                        enviado_em = CASE WHEN @status = 'ENVIADO' THEN datetime('now') ELSE enviado_em END,
                        erro       = @erro
                    WHERE id = @id;",
                [
                    Db.P("@status", status),
                    Db.P("@erro",   erro),
                    Db.P("@id",     p.destId),
                ]);

                // Registrar tentativa
                int tentNum = await Db.ExecuteScalarAsync<int>(
                    "SELECT COALESCE(MAX(tentativa_num), 0) + 1 FROM notificacao_tentativas WHERE destinatario_id = @did;",
                    [Db.P("@did", p.destId)]);

                await Db.ExecuteNonQueryAsync(@"
                    INSERT INTO notificacao_tentativas (destinatario_id, tentativa_num, status, provider, retorno_raw)
                    VALUES (@did, @num, @status, 'MAILKIT', @raw);",
                [
                    Db.P("@did",    p.destId),
                    Db.P("@num",    tentNum),
                    Db.P("@status", status),
                    Db.P("@raw",    erro),
                ]);

                // Se todos os destinatários da notificação foram enviados, marcar notificação como ENVIADA
                await AtualizarStatusNotificacaoAsync(p.notifId);
            }

            return (enviados, falhas);
        }

        private static async Task AtualizarStatusNotificacaoAsync(long notifId)
        {
            // Verifica se ainda restam destinatários pendentes
            var pendentes = await Db.ExecuteScalarAsync<long>(
                "SELECT COUNT(*) FROM notificacao_destinatarios WHERE notificacao_id = @id AND status = 'PENDENTE';",
                [Db.P("@id", notifId)]);

            if (pendentes == 0)
            {
                // Se ao menos um teve erro, marca como PARCIAL; senão, ENVIADA
                var comErro = await Db.ExecuteScalarAsync<long>(
                    "SELECT COUNT(*) FROM notificacao_destinatarios WHERE notificacao_id = @id AND status = 'ERRO';",
                    [Db.P("@id", notifId)]);

                string statusFinal = comErro > 0 ? "PARCIAL" : "ENVIADA";

                await Db.ExecuteNonQueryAsync(@"
                    UPDATE notificacoes
                    SET status     = @status,
                        enviado_em = datetime('now')
                    WHERE id = @id;",
                [
                    Db.P("@status", statusFinal),
                    Db.P("@id",     notifId),
                ]);
            }
        }
    }
}
