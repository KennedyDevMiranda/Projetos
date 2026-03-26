using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Central_do_Educador.Services
{
    /// <summary>
    /// Envia mensagens por WhatsApp.
    /// Modo API  → Evolution API (POST /message/sendText/{instancia})
    /// Modo LINK → abre wa.me no navegador padrão (sem API externa)
    /// </summary>
    public static class WhatsAppService
    {
        private static readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(30) };

        // Preserva emojis e caracteres Unicode no JSON (sem escapar para \uXXXX)
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        // ──────────────────────────────────────────────
        //  Modelo de configuração
        // ──────────────────────────────────────────────

        public class WhatsAppConfig
        {
            /// <summary>API ou LINK</summary>
            public string Modo { get; set; } = "LINK";
            public string ApiUrl { get; set; } = "";
            public string ApiKey { get; set; } = "";
            public string Instancia { get; set; } = "";
            public string DdiPadrao { get; set; } = "55";
            public bool Ativo { get; set; }

            public string ModoNormalizado => (Modo ?? "LINK").Trim().ToUpperInvariant();

            public bool EstaConfiguradoApi =>
                ModoNormalizado == "API" &&
                !string.IsNullOrWhiteSpace(ApiUrl) &&
                !string.IsNullOrWhiteSpace(ApiKey) &&
                !string.IsNullOrWhiteSpace(Instancia);

            public bool EstaConfiguradoLink => ModoNormalizado == "LINK";
        }

        // ──────────────────────────────────────────────
        //  Ler / Salvar configuração
        // ──────────────────────────────────────────────

        public static async Task<WhatsAppConfig> ObterConfigAsync()
        {
            const string sql = @"
                SELECT modo, api_url, api_key, instancia, ddi_padrao, ativo
                FROM configuracao_whatsapp
                WHERE id = 1;";

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqliteCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return new WhatsAppConfig();

            return new WhatsAppConfig
            {
                Modo      = reader["modo"]?.ToString() ?? "LINK",
                ApiUrl    = reader["api_url"]?.ToString() ?? "",
                ApiKey    = reader["api_key"]?.ToString() ?? "",
                Instancia = reader["instancia"]?.ToString() ?? "",
                DdiPadrao = reader["ddi_padrao"]?.ToString() ?? "55",
                Ativo     = Convert.ToInt32(reader["ativo"]) == 1,
            };
        }

        public static async Task SalvarConfigAsync(WhatsAppConfig cfg)
        {
            const string update = @"
            UPDATE configuracao_whatsapp
            SET modo=@modo, api_url=@api_url, api_key=@api_key, instancia=@instancia, ddi_padrao=@ddi, ativo=@ativo
            WHERE id=1;";

                int rows = await Db.ExecuteNonQueryAsync(update,
                [
                    Db.P("@modo", cfg.Modo),
            Db.P("@api_url", cfg.ApiUrl),
            Db.P("@api_key", cfg.ApiKey),
            Db.P("@instancia", cfg.Instancia),
            Db.P("@ddi", cfg.DdiPadrao),
            Db.P("@ativo", cfg.Ativo ? 1 : 0),
        ]);

            if (rows == 0)
            {
                const string insert = @"
            INSERT INTO configuracao_whatsapp (id, modo, api_url, api_key, instancia, ddi_padrao, ativo)
            VALUES (1, @modo, @api_url, @api_key, @instancia, @ddi, @ativo);";

                await Db.ExecuteNonQueryAsync(insert,
                [
                    Db.P("@modo", cfg.Modo),
            Db.P("@api_url", cfg.ApiUrl),
            Db.P("@api_key", cfg.ApiKey),
            Db.P("@instancia", cfg.Instancia),
            Db.P("@ddi", cfg.DdiPadrao),
            Db.P("@ativo", cfg.Ativo ? 1 : 0),
        ]);
            }
        }

        // ──────────────────────────────────────────────
        //  Normalizar telefone
        // ──────────────────────────────────────────────

        /// <summary>
        /// Remove caracteres não numéricos e garante DDI.
        /// Ex.: "(11) 99999-8888" → "5511999998888"
        /// </summary>
        public static string NormalizarTelefone(string telefone, string ddi = "55")
        {
            string soNumeros = Regex.Replace(telefone, @"\D", "");

            if (!string.IsNullOrWhiteSpace(ddi))
            {
                ddi = Regex.Replace(ddi, @"\D", "");
                if (soNumeros.StartsWith(ddi)) return soNumeros;
            }

            // Se começa com 0, remove (ex.: 011...)
            if (soNumeros.StartsWith('0'))
                soNumeros = soNumeros[1..];

            return ddi + soNumeros;
        }

        // ──────────────────────────────────────────────
        //  Enviar mensagem única
        // ──────────────────────────────────────────────

        /// <summary>
        /// Envia uma mensagem de texto para o número informado.
        /// Retorna o messageId da Evolution API ou "LINK_ABERTO".
        /// </summary>
        public static async Task<string> EnviarAsync(string telefone, string mensagem, WhatsAppConfig? config = null)
        {
            config ??= await ObterConfigAsync();

            if (!config.Ativo)
                throw new InvalidOperationException("O envio por WhatsApp está desativado nas configurações.");

            string numero = NormalizarTelefone(telefone, config.DdiPadrao);
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("Número de telefone inválido ou vazio.");

            if (config.Modo == "API")
                return await EnviarViaApiAsync(numero, mensagem, config);

            // Modo LINK → abre wa.me no navegador
            return EnviarViaLink(numero, mensagem);
        }

        // ── Evolution API ──────────────────────────────

        private static async Task<string> EnviarViaApiAsync(string numero, string mensagem, WhatsAppConfig config)
        {
            if (!config.EstaConfiguradoApi)
                throw new InvalidOperationException("Configuração da Evolution API incompleta (URL, API Key ou Instância).");

            string baseUrl = config.ApiUrl.TrimEnd('/');
            string url = $"{baseUrl}/message/sendText/{config.Instancia}";

            var body = new
            {
                number = numero,
                text = mensagem
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("apikey", config.ApiKey);
            request.Content = new StringContent(
                JsonSerializer.Serialize(body, _jsonOptions),
                Encoding.UTF8,
                "application/json");

            using var response = await _http.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(
                    $"Evolution API retornou {(int)response.StatusCode}: {responseBody}");

            // Tentar extrair o messageId da resposta
            try
            {
                using var doc = JsonDocument.Parse(responseBody);
                if (doc.RootElement.TryGetProperty("key", out var keyEl) &&
                    keyEl.TryGetProperty("id", out var idEl))
                {
                    return idEl.GetString() ?? "OK";
                }
            }
            catch { /* resposta não padrão, retornar OK */ }

            return "OK";
        }

        // ── Link wa.me ─────────────────────────────────

        private static string EnviarViaLink(string numero, string mensagem)
        {
            string textoEncoded = Uri.EscapeDataString(mensagem);
            string url = $"https://wa.me/{numero}?text={textoEncoded}";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

            return "LINK_ABERTO";
        }

        // ──────────────────────────────────────────────
        //  Teste de conexão com a Evolution API
        // ──────────────────────────────────────────────

        public static async Task<string> TestarConexaoAsync(WhatsAppConfig config)
        {
            if (config.Modo == "LINK")
                return "✅ Modo LINK ativo. Nenhuma API necessária — o link wa.me será aberto no navegador.";

            if (!config.EstaConfiguradoApi)
                return "❌ Preencha URL, API Key e Instância antes de testar.";

            try
            {
                string baseUrl = config.ApiUrl.TrimEnd('/');
                string url = $"{baseUrl}/instance/connectionState/{config.Instancia}";

                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("apikey", config.ApiKey);

                using var response = await _http.SendAsync(request);
                string body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return $"❌ API retornou {(int)response.StatusCode}: {body}";

                // Verifica se a instância está conectada
                using var doc = JsonDocument.Parse(body);
                string? state = null;

                if (doc.RootElement.TryGetProperty("instance", out var instEl) &&
                    instEl.TryGetProperty("state", out var stateEl))
                {
                    state = stateEl.GetString();
                }
                else if (doc.RootElement.TryGetProperty("state", out var stateEl2))
                {
                    state = stateEl2.GetString();
                }

                return state?.ToUpper() switch
                {
                    "OPEN" or "CONNECTED" => "✅ Instância conectada e pronta para enviar!",
                    "CLOSE" or "DISCONNECTED" => "⚠️ Instância desconectada. Escaneie o QR Code na Evolution API.",
                    "CONNECTING" => "⏳ Instância está conectando... Tente novamente em alguns segundos.",
                    _ => $"ℹ️ Estado da instância: {state ?? "desconhecido"}\n\nResposta: {body}",
                };
            }
            catch (Exception ex)
            {
                return $"❌ Falha ao conectar: {ex.Message}";
            }
        }

        // ──────────────────────────────────────────────
        //  Enviar mensagem de teste
        // ──────────────────────────────────────────────

        public static async Task<string> EnviarTesteAsync(WhatsAppConfig config, string telefone)
        {
            try
            {
                string msgId = await EnviarAsync(
                    telefone,
                    "✅ *Teste - Central do Educador*\n\nSe você recebeu esta mensagem, o envio por WhatsApp está funcionando!",
                    config);

                return config.Modo == "API"
                    ? $"✅ Mensagem enviada! (ID: {msgId})"
                    : "✅ Link aberto no navegador. Confirme o envio manualmente no WhatsApp Web.";
            }
            catch (Exception ex)
            {
                return $"❌ Falha ao enviar: {ex.Message}";
            }
        }

        // ──────────────────────────────────────────────
        //  Processar notificações pendentes de WhatsApp
        // ──────────────────────────────────────────────

        /// <summary>
        /// Busca destinatários pendentes do canal WHATSAPP e tenta enviá-los.
        /// No modo LINK, abre cada mensagem no navegador (uma por vez).
        /// Retorna (enviados, falhas, links).
        /// </summary>
        public static async Task<(int enviados, int falhas, int links)> ProcessarPendentesAsync()
        {
            var config = await ObterConfigAsync();
            if (!config.Ativo)
                return (0, 0, 0);

            const string sql = @"
                SELECT
                    nd.id           AS dest_id,
                    nd.nome         AS dest_nome,
                    nd.telefone     AS dest_telefone,
                    n.id            AS notif_id,
                    n.mensagem      AS mensagem
                FROM notificacao_destinatarios nd
                INNER JOIN notificacoes n ON n.id = nd.notificacao_id
                WHERE nd.status    = 'PENDENTE'
                  AND n.canal      = 'WHATSAPP'
                  AND nd.telefone IS NOT NULL
                  AND nd.telefone <> ''
                ORDER BY n.criado_em ASC;";

            var pendentes = new List<(long destId, long notifId, string nome, string telefone, string mensagem)>();

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
                        telefone: reader["dest_telefone"]!.ToString()!,
                        mensagem: reader["mensagem"]!.ToString()!
                    ));
                }
            }

            int enviados = 0, falhas = 0, linksAbertos = 0;

            foreach (var p in pendentes)
            {
                string status;
                string? erro = null;
                string? messageId = null;

                try
                {
                    messageId = await EnviarAsync(p.telefone, p.mensagem, config);

                    if (config.Modo == "LINK")
                    {
                        status = "LINK_ABERTO";
                        linksAbertos++;
                    }
                    else
                    {
                        status = "ENVIADO";
                        enviados++;
                    }
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
                        enviado_em = CASE WHEN @status IN ('ENVIADO','LINK_ABERTO') THEN datetime('now') ELSE enviado_em END,
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
                    INSERT INTO notificacao_tentativas (destinatario_id, tentativa_num, status, provider, provider_message_id, retorno_raw)
                    VALUES (@did, @num, @status, @provider, @msgId, @raw);",
                [
                    Db.P("@did",      p.destId),
                    Db.P("@num",      tentNum),
                    Db.P("@status",   status),
                    Db.P("@provider", config.Modo == "API" ? "EVOLUTION_API" : "WA_ME_LINK"),
                    Db.P("@msgId",    messageId),
                    Db.P("@raw",      erro),
                ]);

                // Atualizar status consolidado da notificação
                await AtualizarStatusNotificacaoAsync(p.notifId);
            }

            return (enviados, falhas, linksAbertos);
        }

        private static async Task AtualizarStatusNotificacaoAsync(long notifId)
        {
            var pendentes = await Db.ExecuteScalarAsync<long>(
                "SELECT COUNT(*) FROM notificacao_destinatarios WHERE notificacao_id = @id AND status = 'PENDENTE';",
                [Db.P("@id", notifId)]);

            if (pendentes == 0)
            {
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
