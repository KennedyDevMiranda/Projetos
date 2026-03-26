using Central_do_Educador.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmConfiguracaoEmail : Form
    {
        public FrmConfiguracaoEmail()
        {
            InitializeComponent();
        }

        private async void FrmConfiguracaoEmail_Load(object? sender, EventArgs e)
        {
            try
            {
                var cfg = await EmailService.ObterConfigAsync();

                txtHost.Text           = cfg.Host;
                nudPorta.Value         = cfg.Porta;
                chkSsl.Checked         = cfg.UsarSsl;
                txtUsuario.Text        = cfg.Usuario;
                txtSenha.Text          = cfg.Senha;
                txtRemetenteNome.Text  = cfg.RemetenteNome;
                txtRemetenteEmail.Text = cfg.RemetenteEmail;
                chkAtivo.Checked       = cfg.Ativo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar configuração: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private EmailService.SmtpConfig MontarConfig() => new()
        {
            Host           = txtHost.Text.Trim(),
            Porta          = (int)nudPorta.Value,
            UsarSsl        = chkSsl.Checked,
            Usuario        = txtUsuario.Text.Trim(),
            Senha          = txtSenha.Text.Trim(),
            RemetenteNome  = txtRemetenteNome.Text.Trim(),
            RemetenteEmail = txtRemetenteEmail.Text.Trim(),
            Ativo          = chkAtivo.Checked,
        };

        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            var cfg = MontarConfig();

            if (!cfg.EstaConfigurado)
            {
                MessageBox.Show("Preencha todos os campos obrigatórios (Host, Usuário, Senha, E-mail remetente).",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await EmailService.SalvarConfigAsync(cfg);
                MessageBox.Show("Configuração salva com sucesso!", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnTestar_Click(object? sender, EventArgs e)
        {
            var cfg = MontarConfig();

            if (!cfg.EstaConfigurado)
            {
                MessageBox.Show("Preencha todos os campos antes de testar.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string emailTeste = txtEmailTeste.Text.Trim();
            if (string.IsNullOrWhiteSpace(emailTeste))
            {
                MessageBox.Show("Informe um e-mail de destino para o teste.",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailTeste.Focus();
                return;
            }

            btnTestar.Enabled = false;
            btnTestar.Text = "Enviando...";

            try
            {
                string resultado = await EmailService.EnviarTesteAsync(cfg, emailTeste);
                MessageBox.Show(resultado, "Teste de E-mail",
                    MessageBoxButtons.OK,
                    resultado.StartsWith("✅") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            finally
            {
                btnTestar.Enabled = true;
                btnTestar.Text = "📧 Enviar Teste";
            }
        }
    }
}
