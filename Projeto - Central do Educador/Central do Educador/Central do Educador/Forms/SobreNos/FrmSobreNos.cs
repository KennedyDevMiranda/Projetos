using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmSobreNos : Form
    {
        public FrmSobreNos()
        {
            InitializeComponent();
        }

        private void FrmSobreNos_Load(object sender, EventArgs e)
        {
            string versao = System.Reflection.Assembly
                .GetExecutingAssembly()
                .GetName()
                .Version?
                .ToString() ?? "0.0.1.3";

            lblVersao.Text = $"Versão: {versao}";
        }

        private void lnkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/GustavoFiwororth",
                    UseShellExecute = true
                });
            }
            catch { }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
