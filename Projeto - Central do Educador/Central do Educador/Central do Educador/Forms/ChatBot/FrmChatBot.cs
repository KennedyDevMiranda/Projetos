//using FrmChatBot.Forms;
using ClosedXML.Excel;
using Central_do_Educador.Data;
using Central_do_Educador.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmChatBot : Form
    {
        private List<dynamic> filaDeEnvio = new List<dynamic>();
        private FrmQRCode janelaLogin;
        private Random rnd = new Random();


        public FrmChatBot()
        {
            InitializeComponent();

            this.btnAnalisar.Click += new System.EventHandler(this.btnAnalisarPlanilha_Click);
            this.btnEnviarMassa.Click += new System.EventHandler(this.btnEnviarMassa_Click);
            this.ptbConectar.Click += new System.EventHandler(this.ptbConectar_Click);
            this.FormClosing += new FormClosingEventHandler(this.FrmPainel_FormClosing);
        }

        private void FrmChatBot_Load(object sender, EventArgs e)
        {
            txtHistorico.ReadOnly = true;
            btnEnviarMassa.Enabled = false;
            ptbConectar.Enabled = false;
            EsconderProgresso();
            AtualizarStatusVisual("Iniciando...", Color.Gray);

            string operador = Sessao.UsuarioNome;
            if (string.IsNullOrEmpty(operador))
            {
                MessageBox.Show("Nenhum usuário logado. Faça login primeiro.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            string versaoReal = System.Reflection.Assembly
                .GetExecutingAssembly()
                .GetName()
                .Version
                .ToString();

            AtualizarHistorico("------------------------------------------");
            AtualizarHistorico("🚀 BEM-VINDO AO CHATBOT!");
            AtualizarHistorico("👨‍💻 Desenvolvido por: Gustavo Rodrigues Fiori");
            AtualizarHistorico($"⚙️ Versão ChatBot : {versaoReal}");
            AtualizarHistorico($"👤 Operador: {operador}");
            AtualizarHistorico("------------------------------------------");

            // Inscreve nos eventos do manager
            WhatsAppDriverManager.OnConectado += WppManager_OnConectado;
            WhatsAppDriverManager.OnDesconectado += WppManager_OnDesconectado;
            WhatsAppDriverManager.OnQRCodeDisponivel += WppManager_OnQRCode;

            // Já está conectado? Atualiza UI imediatamente
            if (WhatsAppDriverManager.Conectado)
            {
                ptbConectar.Image = Properties.Resources.codigo_qr_ON;
                ptbConectar.Enabled = true;
                toolTip1.SetToolTip(ptbConectar, "Clique para desconectar a sessão do WhatsApp");
                btnEnviarMassa.Enabled = filaDeEnvio.Count > 0;
                AtualizarStatusVisual("ChatBot Conectado", Color.ForestGreen);
                AtualizarHistorico("✅ SISTEMA: ChatBot já estava conectado!");

                string? numero = WhatsAppDriverManager.NumeroConectado
                              ?? WhatsAppDriverManager.ObterNumeroConectado();
                if (!string.IsNullOrEmpty(numero))
                    AtualizarHistorico($"📱 Número conectado: {FormatarNumero(numero)}");
            }
            else
            {
                _ = Task.Run(() => WhatsAppDriverManager.IniciarAsync(exibirQR: false));
            }
        }

        // ── Callbacks do WhatsAppDriverManager ──

        private void WppManager_OnConectado()
        {
            if (IsDisposed || !IsHandleCreated) return;

            // Obter o número conectado (executado em background para não travar a UI)
            string? numero = WhatsAppDriverManager.ObterNumeroConectado();

            this.Invoke((MethodInvoker)delegate
            {
                if (janelaLogin != null) { janelaLogin.FecharLogin(); janelaLogin = null; }
                ptbConectar.Image = Properties.Resources.codigo_qr_ON;
                ptbConectar.Enabled = true;
                toolTip1.SetToolTip(ptbConectar, "Clique para desconectar a sessão do WhatsApp");
                btnEnviarMassa.Enabled = filaDeEnvio.Count > 0;
                AtualizarStatusVisual("ChatBot Conectado", Color.ForestGreen);
                AtualizarHistorico("✅ SISTEMA: ChatBot Conectado!");

                if (!string.IsNullOrEmpty(numero))
                {
                    string formatado = FormatarNumero(numero);
                    AtualizarHistorico($"📱 Número conectado: {formatado}");
                }
            });
        }

        /// <summary>Formata o número (ex: 5511999999999 → +55 (11) 99999-9999).</summary>
        private static string FormatarNumero(string numero)
        {
            if (numero.Length == 13) // DDI + DDD + 9 dígitos
                return $"+{numero[..2]} ({numero[2..4]}) {numero[4..9]}-{numero[9..]}";
            if (numero.Length == 12) // DDI + DDD + 8 dígitos
                return $"+{numero[..2]} ({numero[2..4]}) {numero[4..8]}-{numero[8..]}";
            return $"+{numero}";
        }

        private void WppManager_OnDesconectado()
        {
            if (IsDisposed || !IsHandleCreated) return;
            this.Invoke((MethodInvoker)delegate
            {
                ptbConectar.Enabled = true;
                ptbConectar.Image = Properties.Resources.codigo_qr_OFF;
                AtualizarStatusVisual("Desconectado", Color.Red);
                toolTip1.SetToolTip(ptbConectar, "Clique para conectar a sessão do WhatsApp");
                AtualizarHistorico("ℹ️ SISTEMA: Clique no ícone QR para conectar ao WhatsApp.");
            });
        }

        private void WppManager_OnQRCode(Image imgQR)
        {
            if (IsDisposed || !IsHandleCreated) return;
            this.Invoke((MethodInvoker)delegate
            {
                if (janelaLogin == null || janelaLogin.IsDisposed)
                {
                    janelaLogin = new FrmQRCode();
                    janelaLogin.Show();
                }
                janelaLogin.AtualizarQR(imgQR);
                AtualizarStatusVisual("Aguardando QR", Color.OrangeRed);
            });
        }

        private void AtualizarHistorico(string texto)
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { AtualizarHistorico(texto); }); return; }
            txtHistorico.AppendText(texto + Environment.NewLine);
            txtHistorico.SelectionStart = txtHistorico.Text.Length;
            txtHistorico.ScrollToCaret();
        }

        private void btnEnviarMassa_Click(object sender, EventArgs e)
        {
            _ = EnviarMassaAsync();
        }

        private async Task EnviarMassaAsync()
        {
            if (!WhatsAppDriverManager.Conectado || filaDeEnvio.Count == 0) return;

            string msgBase = txtMensagem.Text;
            btnAnalisar.Enabled = btnEnviarMassa.Enabled = false;

            int totalProcessos = filaDeEnvio.Count;
            int processados = 0;

            this.Invoke((MethodInvoker)delegate
            {
                MostrarProgresso();
                pgbProgresso.Maximum = totalProcessos;
                pgbProgresso.Value = 0;
                lblPorcentagem.Text = $"0 / {totalProcessos}";
            });

            await Task.Run(async () =>
            {
                foreach (var aluno in filaDeEnvio)
                {
                    if (!WhatsAppDriverManager.Conectado)
                    {
                        this.Invoke((MethodInvoker)delegate { AtualizarHistorico("⚠️ Conexão perdida. Tentando reconectar..."); });
                        _ = WhatsAppDriverManager.IniciarAsync(exibirQR: false);
                        await Task.Delay(5000);
                        if (!WhatsAppDriverManager.Conectado) continue;
                    }

                    // Mensagens distintas: nome do aluno para o aluno, nome do responsável para o responsável
                    string msgAluno = msgBase.Replace("{nome}", aluno.Nome);
                    string msgResp = msgBase.Replace("{nome}", aluno.NomeResp);
                    string statusAcumulado = "";
                    string errosAcumulados = "";

                    // ── Normalizar números para comparação ──
                    string? telAlunoNorm = NormalizarNumero(aluno.TelAluno);
                    string? telRespNorm = NormalizarNumero(aluno.TelResp);
                    bool numerosDuplicados = !string.IsNullOrEmpty(telAlunoNorm) 
                                          && !string.IsNullOrEmpty(telRespNorm) 
                                          && telAlunoNorm == telRespNorm;

                    // 1. Envio para o Aluno (WhatsApp) - PULAR se número duplicado
                    if (!string.IsNullOrEmpty(aluno.TelAluno) && aluno.TelAluno != "Sem número" && !numerosDuplicados)
                    {
                        try
                        {
                            await WhatsAppDriverManager.EnviarMensagemAsync(aluno.TelAluno, msgAluno);
                            statusAcumulado += "[Zap Aluno: OK] ";

                            this.Invoke((MethodInvoker)delegate
                            {
                                AtualizarHistorico($"✅ Zap enviado para Aluno: {aluno.Nome} ({aluno.TelAluno})");
                            });
                        }
                        catch (Exception ex)
                        {
                            string motivo = ex.Message.Contains("Timed out")
                                ? "Tempo esgotado (O número pode não ter WhatsApp / Incorreto.)"
                                : ex.Message;

                            statusAcumulado += "[Zap Aluno: Falhou] ";
                            errosAcumulados += $"Erro Aluno: {motivo}; ";

                            this.Invoke((MethodInvoker)delegate
                            {
                                AtualizarHistorico($"❌ Erro Aluno {aluno.Nome}: {motivo}");
                            });
                        }
                        await Task.Delay(rnd.Next(1500, 3000));
                    }
                    else if (numerosDuplicados)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            AtualizarHistorico($"ℹ️ Aluno e Responsável têm o mesmo número ({aluno.TelResp}). Enviando apenas para Responsável.");
                        });
                    }

                    // 2. Envio para o Responsável (WhatsApp) - SEMPRE envia se houver número
                    if (!string.IsNullOrEmpty(aluno.TelResp) && aluno.TelResp != "Sem número")
                    {
                        try
                        {
                            await WhatsAppDriverManager.EnviarMensagemAsync(aluno.TelResp, msgResp);
                            statusAcumulado += "[Zap Resp: OK] ";

                            string labelExtra = numerosDuplicados ? " (Aluno/Resp)" : "";
                            this.Invoke((MethodInvoker)delegate
                            {
                                AtualizarHistorico($"✅ Zap enviado para Resp. de: {aluno.Nome} → {aluno.NomeResp} ({aluno.TelResp}){labelExtra}");
                            });
                        }
                        catch (Exception ex)
                        {
                            string motivo = ex.Message.Contains("Timed out")
                                ? "Tempo esgotado (O número pode não ter WhatsApp / Incorreto.)"
                                : ex.Message;

                            statusAcumulado += "[Zap Resp: Falhou] ";
                            errosAcumulados += $"Erro Resp: {motivo}; ";

                            this.Invoke((MethodInvoker)delegate
                            {
                                AtualizarHistorico($"❌ Erro Resp {aluno.NomeResp}: {motivo}");
                            });
                        }
                        await Task.Delay(rnd.Next(1500, 3000));
                    }

                    // 3. Envio de E-mail
                    if (!string.IsNullOrEmpty(aluno.Email) && aluno.Email != "Não cadastrado" && aluno.Email.Contains("@"))
                    {
                        try
                        {
                            await EnviarEmailAsync(aluno.Email, "", msgAluno);
                            statusAcumulado += "[E-mail: OK] ";
                        }
                        catch (Exception ex)
                        {
                            statusAcumulado += "[E-mail: Falhou] ";
                            errosAcumulados += $"Erro E-mail: {ex.Message}; ";
                            this.Invoke((MethodInvoker)delegate { AtualizarHistorico($"❌ Erro e-mail {aluno.Nome}: {ex.Message}"); });
                        }
                    }

                    // --- REGISTRO NO BANCO ---
                    if (!string.IsNullOrEmpty(statusAcumulado))
                    {
                        string erroFinal = string.IsNullOrWhiteSpace(errosAcumulados) ? "Sucesso" : errosAcumulados.Trim();

                        await SalvarLogEnvioChatBotAsync(
                            aluno.Nome,
                            aluno.TelAluno,
                            aluno.TelResp,
                            aluno.Email,
                            msgAluno,
                            statusAcumulado.Trim(),
                            erroFinal
                        );
                    }

                    processados++;
                    this.Invoke((MethodInvoker)(() =>
                    {
                        pgbProgresso.Value = processados;
                        lblPorcentagem.Text = $"{processados} / {totalProcessos}";
                    }));
                }

                this.Invoke((MethodInvoker)(() =>
                {
                    btnAnalisar.Enabled = btnEnviarMassa.Enabled = true;
                    filaDeEnvio.Clear();
                    AtualizarInterface();
                    EsconderProgresso();
                    AtualizarHistorico("------------------------------------------");
                    AtualizarHistorico("🚀 DISPARO CONCLUÍDO E REGISTRADO!");
                    AtualizarHistorico("------------------------------------------");
                    MessageBox.Show("Disparo concluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            });
        }

        private async void btnAnalisarPlanilha_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Excel|*.xlsx" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filaDeEnvio.Clear();
                List<string> nomesParaVerificar = new List<string>();

                try
                {
                    using (var workbook = new XLWorkbook(ofd.FileName))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var linhas = worksheet.RangeUsed().RowsUsed();
                        foreach (var linha in linhas)
                        {
                            string nome = linha.Cell(1).GetString().Trim();
                            if (!string.IsNullOrEmpty(nome)) nomesParaVerificar.Add(nome);
                        }
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show(
                        $"Não foi possível abrir o arquivo:\n\n\"{ofd.FileName}\"\n\nEle está sendo usado por outro programa. Feche o arquivo no Excel e tente novamente.",
                        "Arquivo em uso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                List<string> nomesNaoEncontrados = new List<string>();

                foreach (var nome in nomesParaVerificar)
                {
                    var dados = await BuscarDadosAlunoAsync(nome);
                    if (dados != null)
                    {
                        filaDeEnvio.Add(new
                        {
                            Nome = nome,
                            NomeResp = string.IsNullOrWhiteSpace((string)dados.NomeResp) ? nome : (string)dados.NomeResp,
                            TelAluno = (string)dados.TelAluno,
                            TelResp = (string)dados.TelResp,
                            Email = (string)dados.Email
                        });
                    }
                    else
                    {
                        nomesNaoEncontrados.Add(nome);
                    }
                }

                if (nomesNaoEncontrados.Count > 0)
                {
                    AtualizarHistorico($"⚠️ {nomesNaoEncontrados.Count} aluno(s) não encontrado(s) no banco:");
                    foreach (var nome in nomesNaoEncontrados)
                    {
                        AtualizarHistorico($"   ❌ {nome}");
                    }
                }

                AtualizarInterface();
                AtualizarHistorico($"✅ Fila atualizada: {filaDeEnvio.Count} alunos prontos.");
            }
        }

        // ======================== ACESSO A DADOS (SQLite via Db) ========================

        private async Task<dynamic> BuscarDadosAlunoAsync(string nome)
        {
            try
            {
                var dt = await Db.QueryDataTableAsync(
                    "SELECT numero_aluno, numero_responsavel, NomeResponsavel, email FROM alunos WHERE TRIM(nome) = @nome LIMIT 1",
                    new[] { Db.P("@nome", nome.Trim()) });

                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    return new
                    {
                        TelAluno = row["numero_aluno"]?.ToString() ?? "",
                        TelResp = row["numero_responsavel"]?.ToString() ?? "",
                        NomeResp = row["NomeResponsavel"]?.ToString() ?? "",
                        Email = row["email"]?.ToString() ?? "Não cadastrado"
                    };
                }
            }
            catch { }
            return null;
        }

        private async Task SalvarLogEnvioChatBotAsync(string nome, string telAlu, string telRes, string email, string msg, string status, string erro = "")
        {
            try
            {
                const string sql = @"
                    INSERT INTO historico_envio_chatbot 
                        (nome_aluno, numero_aluno, numero_responsavel, email, mensagem, status, erro, operador, operador_id)
                    VALUES 
                        (@nome, @telAlu, @telRes, @email, @msg, @status, @erro, @operador, @operadorId)";

                await Db.ExecuteNonQueryAsync(sql, new[]
                {
                    Db.P("@nome", nome ?? ""),
                    Db.P("@telAlu", telAlu ?? ""),
                    Db.P("@telRes", telRes ?? ""),
                    Db.P("@email", email ?? ""),
                    Db.P("@msg", msg ?? ""),
                    Db.P("@status", status ?? ""),
                    Db.P("@erro", string.IsNullOrWhiteSpace(erro) ? "Sucesso" : erro),
                    Db.P("@operador", Sessao.UsuarioNome),
                    Db.P("@operadorId", Sessao.UsuarioId)
                });
            }
            catch (Exception ex)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    AtualizarHistorico("⚠️ Erro Crítico no Banco: " + ex.Message);
                });
            }
        }

        // ======================== E-MAIL (config via banco) ========================

        private async Task EnviarEmailAsync(string destino, string assuntoPadrao, string corpo)
        {
            var dt = await Db.QueryDataTableAsync(
                "SELECT smtp_host, smtp_porta,usar_ssl, usuario, senha, remetente_nome, remetente_email, ativo FROM configuracao_email WHERE id = 1");

            if (dt.Rows.Count == 0)
            {
                AtualizarHistorico("⚠️ Configuração de e-mail não encontrada no banco.");
                throw new Exception("Configuração de e-mail ausente.");
            }

            var row = dt.Rows[0];
            bool ativo = Convert.ToInt32(row["ativo"]) == 1;
            if (!ativo)
            {
                AtualizarHistorico("⚠️ Envio de e-mail desativado nas configurações.");
                return;
            }

            string smtpHost = row["smtp_host"]?.ToString() ?? "";
            int smtpPorta = Convert.ToInt32(row["smtp_porta"]);
            bool usarSsl = Convert.ToInt32(row["usar_ssl"]) == 1;
            string usuario = row["usuario"]?.ToString() ?? "";
            string senha = row["senha"]?.ToString() ?? "";
            string remetenteNome = row["remetente_nome"]?.ToString() ?? "Central do Educador";
            string remetenteEmail = row["remetente_email"]?.ToString() ?? usuario;

            if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(usuario))
            {
                AtualizarHistorico("⚠️ Configuração de e-mail incompleta (host ou usuário vazio).");
                throw new Exception("Configuração de e-mail incompleta.");
            }

            string assuntoFinal = string.IsNullOrWhiteSpace(assuntoPadrao)
                ? "Central do Educador - Comunicado"
                : assuntoPadrao;

            using var smtpClient = new SmtpClient(smtpHost)
            {
                Port = smtpPorta,
                Credentials = new NetworkCredential(usuario, senha),
                EnableSsl = usarSsl,
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(remetenteEmail, remetenteNome),
                Subject = assuntoFinal,
                Body = corpo,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(destino);
            await Task.Run(() => smtpClient.Send(mailMessage));
            AtualizarHistorico($"📧 E-mail enviado: {destino}");
        }

        // ======================== WHATSAPP / CONTROLES ========================

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            WhatsAppDriverManager.Desconectar(limparSessao: true);
            AtualizarHistorico("🧹 SISTEMA: Dados de sessão limpos.");
            AtualizarHistorico("⚠️ SISTEMA: Sessão encerrada.");

            // Reinicia para ficar pronto para novo QR
            _ = Task.Run(() => WhatsAppDriverManager.IniciarAsync(exibirQR: false));
        }

        private void FrmPainel_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Apenas desinscreve dos eventos — NÃO mata o driver
            WhatsAppDriverManager.OnConectado -= WppManager_OnConectado;
            WhatsAppDriverManager.OnDesconectado -= WppManager_OnDesconectado;
            WhatsAppDriverManager.OnQRCodeDisponivel -= WppManager_OnQRCode;
        }

        private void ptbConectar_Click(object sender, EventArgs e)
        {
            if (WhatsAppDriverManager.Conectado)
            {
                // Desconectar
                WhatsAppDriverManager.Desconectar(limparSessao: true);
                AtualizarHistorico("⚠️ SISTEMA: Sessão encerrada.");
                _ = Task.Run(() => WhatsAppDriverManager.IniciarAsync(exibirQR: false));
            }
            else
            {
                // Conectar — exibe QR
                _ = Task.Run(() => WhatsAppDriverManager.IniciarAsync(exibirQR: true));
            }
        }

        // ======================== UI HELPERS ========================

        private void EsconderProgresso()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { EsconderProgresso(); }); return; }
            if (pgbProgresso != null) pgbProgresso.Visible = false;
            if (lblPorcentagem != null) lblPorcentagem.Visible = false;
        }

        private void MostrarProgresso()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { MostrarProgresso(); }); return; }
            if (pgbProgresso != null) pgbProgresso.Visible = true;
            if (lblPorcentagem != null) lblPorcentagem.Visible = true;
        }

        private void AtualizarStatusVisual(string texto, Color cor)
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { AtualizarStatusVisual(texto, cor); }); return; }
            lblStatus.Text = texto.ToUpper();
            lblStatus.ForeColor = cor;
        }

        private void AtualizarInterface()
        {
            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { AtualizarInterface(); }); return; }
            lblContador.Text = $"Alunos na fila: {filaDeEnvio.Count}";
            btnEnviarMassa.Enabled = (filaDeEnvio.Count > 0 && WhatsAppDriverManager.Conectado);
        }

        /// <summary>
        /// Remove caracteres não numéricos e normaliza números para comparação.
        /// Ex: "(11) 99999-9999" ou "11999999999" → "11999999999"
        /// </summary>
        private static string? NormalizarNumero(string? numero)
        {
            if (string.IsNullOrWhiteSpace(numero) || numero == "Sem número" || numero == "Não cadastrado")
                return null;

            // Remove tudo que não é dígito
            string somenteDigitos = new string(numero.Where(char.IsDigit).ToArray());

            if (somenteDigitos.Length == 0)
                return null;

            // Remove DDI 55 se presente (para comparar apenas DDD+Número)
            if (somenteDigitos.Length == 13 && somenteDigitos.StartsWith("55"))
                somenteDigitos = somenteDigitos[2..];

            return somenteDigitos;
        }
    }
}