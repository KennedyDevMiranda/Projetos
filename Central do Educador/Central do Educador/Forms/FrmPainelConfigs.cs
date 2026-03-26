using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmPainelConfigs : Form
    {
        public FrmPainelConfigs()
        {
            InitializeComponent();
        }

        private void BtnconfigEmail_Click(object sender, EventArgs e)
        {
            using (FrmConfiguracaoEmail frmEmail = new FrmConfiguracaoEmail())
            {
                frmEmail.ShowDialog();
            }
        }

        private void BtnConfigDb_Click(object sender, EventArgs e)
        {
            /*using (FrmConfigBanco frmBanco = new FrmConfigBanco())
            {
                frmBanco.ShowDialog();
            }*/
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            var confirmacao = MessageBox.Show(
                "Isso irá encerrar todas as janelas do Microsoft Edge abertas pelo bot para limpar erros. Deseja continuar?",
                "Resetar Navegador",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    // 1. Mata o Driver (o que controla o navegador)
                    foreach (var process in Process.GetProcessesByName("msedgedriver"))
                    {
                        try { process.Kill(); } catch { }
                    }

                    // 2. Mata o Navegador (as janelas presas)
                    foreach (var process in Process.GetProcessesByName("msedge"))
                    {
                        try { process.Kill(); } catch { }
                    }

                    MessageBox.Show("Memória limpa com sucesso! O sistema tentará reconectar ao fechar esta tela.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Sinaliza para o Painel principal que deve reiniciar a conexão
                    this.DialogResult = DialogResult.Retry;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao limpar processos: " + ex.Message);
                }
            }
        }

        private void btnTrocarOperador_Click(object sender, EventArgs e)
        {
            // 1. Limpa o operador atual para forçar a nova identificação
            Properties.Settings.Default.OperadorAtual = "";
            Properties.Settings.Default.Save();

            // 2. Define o resultado como OK e fecha
            // Isso diz ao FrmPainel que ele deve rodar o VerificarOperador()
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
