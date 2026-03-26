using System;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmQRCode : Form
    {
        public FrmQRCode()
        {
            InitializeComponent();

            this.Text = "Conectar WhatsApp";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        // Método para o Painel principal enviar a imagem do QR para cá
        public void AtualizarQR(System.Drawing.Image img)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { picQRCode.Image = img; });
            }
            else
            {
                picQRCode.Image = img;
            }
        }

        // Método para fechar quando conectar
        public void FecharLogin()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose(); // Libera os recursos e fecha de vez
                });
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }
    }
}
