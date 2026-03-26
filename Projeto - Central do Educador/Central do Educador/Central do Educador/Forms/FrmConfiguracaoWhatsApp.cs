using Central_do_Educador.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmConfiguracaoWhatsApp : Form
    {
        public FrmConfiguracaoWhatsApp()
        {
            InitializeComponent();
        }

        private async void FrmConfiguracaoWhatsApp_Load(object? sender, EventArgs e)
        {
            try
            {
                var cfg = await WhatsAppService.ObterConfigAsync();

                cmbModo.SelectedItem = cfg.Modo;
                txtApiUrl.Text       = cfg.ApiUrl;
                txtApiKey.Text       = cfg.ApiKey;
                txtInstancia.Text    = cfg.Instancia;
                txtDdi.Text          = cfg.DdiPadrao;
                chkAtivo.Checked     = cfg.Ativo;

                SelecionarModo(cfg.Modo);

                AtualizarVisibilidadeCamposApi();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar configuração: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelecionarModo(string modo)
        {
            modo = (modo ?? "LINK").Trim().ToUpper();

            for (int i = 0; i < cmbModo.Items.Count; i++)
            {
                var item = cmbModo.Items[i]?.ToString()?.Trim().ToUpper();
                if (item == modo)
                {
                    cmbModo.SelectedIndex = i;
                    return;
                }
            }

            cmbModo.SelectedIndex = 0; // fallback
        }

        private void cmbModo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AtualizarVisibilidadeCamposApi();
        }

        private void AtualizarVisibilidadeCamposApi()
        {
            bool isApi = cmbModo.SelectedItem?.ToString() == "API";
            lblApiUrl.Visible    = isApi;
            txtApiUrl.Visible    = isApi;
            lblApiKey.Visible    = isApi;
            txtApiKey.Visible    = isApi;
            lblInstancia.Visible = isApi;
            txtInstancia.Visible = isApi;
            btnTestarConexao.Visible = isApi;

            lblInfoLink.Visible = !isApi;
        }

        private WhatsAppService.WhatsAppConfig MontarConfig() => new()
        {
            Modo      = cmbModo.SelectedItem?.ToString() ?? "LINK",
            ApiUrl    = txtApiUrl.Text.Trim(),
            ApiKey    = txtApiKey.Text.Trim(),
            Instancia = txtInstancia.Text.Trim(),
            DdiPadrao = txtDdi.Text.Trim(),
            Ativo     = chkAtivo.Checked,
        };

        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            var cfg = MontarConfig();

            if (cfg.Modo == "API" && !cfg.EstaConfiguradoApi)
            {
                MessageBox.Show("Preencha URL, API Key e Instância para o modo API.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await WhatsAppService.SalvarConfigAsync(cfg);
                MessageBox.Show("Configuração salva com sucesso!", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnTestarConexao_Click(object? sender, EventArgs e)
        {
            var cfg = MontarConfig();

            if (cfg.Modo == "API" && !cfg.EstaConfiguradoApi)
            {
                MessageBox.Show("Preencha URL, API Key e Instância para o modo API.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnTestarConexao.Enabled = false;
            btnTestarConexao.Text = "Verificando...";

            try
            {
                string resultado = await WhatsAppService.TestarConexaoAsync(cfg);
                MessageBox.Show(resultado, "Teste de Conexão",
                    MessageBoxButtons.OK,
                    resultado.StartsWith("✅") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Falha ao testar conexão: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTestarConexao.Enabled = true;
                btnTestarConexao.Text = "🔌 Testar Conexão";
            }
        }

        private async void btnEnviarTeste_Click(object? sender, EventArgs e)
        {
            var cfg = MontarConfig();

            if (cfg.Modo == "API" && !cfg.EstaConfiguradoApi)
            {
                MessageBox.Show("Preencha URL, API Key e Instância para o modo API.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string telefone = System.Text.RegularExpressions.Regex.Replace(txtTelTeste.Text ?? "", @"\D", "");

            if (string.IsNullOrWhiteSpace(telefone))
            {
                MessageBox.Show("Informe um número para o teste.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelTeste.Focus();
                return;
            }

            btnEnviarTeste.Enabled = false;
            btnEnviarTeste.Text = "Enviando...";

            try
            {
                string resultado = await WhatsAppService.EnviarTesteAsync(cfg, telefone);
                MessageBox.Show(resultado, "Teste WhatsApp",
                    MessageBoxButtons.OK,
                    resultado.StartsWith("✅") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Falha ao enviar teste: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnEnviarTeste.Enabled = true;
                btnEnviarTeste.Text = "📱 Enviar Teste";
            }
        }
    }
}
