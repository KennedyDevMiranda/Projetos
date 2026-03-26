namespace Central_do_Educador.Forms
{
    partial class FrmConfiguracaoEmail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblHost = new Label();
            txtHost = new TextBox();
            lblPorta = new Label();
            nudPorta = new NumericUpDown();
            chkSsl = new CheckBox();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblSenha = new Label();
            txtSenha = new TextBox();
            lblRemetenteNome = new Label();
            txtRemetenteNome = new TextBox();
            lblRemetenteEmail = new Label();
            txtRemetenteEmail = new TextBox();
            chkAtivo = new CheckBox();
            btnSalvar = new Button();
            grpTeste = new GroupBox();
            lblEmailTeste = new Label();
            txtEmailTeste = new TextBox();
            btnTestar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudPorta).BeginInit();
            grpTeste.SuspendLayout();
            SuspendLayout();

            // lblHost
            lblHost.AutoSize = true;
            lblHost.Location = new Point(20, 20);
            lblHost.Text = "Servidor SMTP";

            // txtHost
            txtHost.Location = new Point(20, 40);
            txtHost.Size = new Size(300, 23);
            txtHost.PlaceholderText = "ex.: smtp.gmail.com";

            // lblPorta
            lblPorta.AutoSize = true;
            lblPorta.Location = new Point(340, 20);
            lblPorta.Text = "Porta";

            // nudPorta
            nudPorta.Location = new Point(340, 40);
            nudPorta.Size = new Size(80, 23);
            nudPorta.Minimum = 1;
            nudPorta.Maximum = 65535;
            nudPorta.Value = 587;

            // chkSsl
            chkSsl.AutoSize = true;
            chkSsl.Location = new Point(440, 42);
            chkSsl.Text = "Usar SSL/TLS";
            chkSsl.Checked = true;

            // lblUsuario
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(20, 76);
            lblUsuario.Text = "Usuário (login)";

            // txtUsuario
            txtUsuario.Location = new Point(20, 96);
            txtUsuario.Size = new Size(300, 23);
            txtUsuario.PlaceholderText = "seu-email@gmail.com";

            // lblSenha
            lblSenha.AutoSize = true;
            lblSenha.Location = new Point(340, 76);
            lblSenha.Text = "Senha / App Password";

            // txtSenha
            txtSenha.Location = new Point(340, 96);
            txtSenha.Size = new Size(220, 23);
            txtSenha.UseSystemPasswordChar = true;

            // lblRemetenteNome
            lblRemetenteNome.AutoSize = true;
            lblRemetenteNome.Location = new Point(20, 132);
            lblRemetenteNome.Text = "Nome do Remetente";

            // txtRemetenteNome
            txtRemetenteNome.Location = new Point(20, 152);
            txtRemetenteNome.Size = new Size(300, 23);
            txtRemetenteNome.Text = "Central do Educador";

            // lblRemetenteEmail
            lblRemetenteEmail.AutoSize = true;
            lblRemetenteEmail.Location = new Point(340, 132);
            lblRemetenteEmail.Text = "E-mail do Remetente";

            // txtRemetenteEmail
            txtRemetenteEmail.Location = new Point(340, 152);
            txtRemetenteEmail.Size = new Size(220, 23);
            txtRemetenteEmail.PlaceholderText = "noreply@escola.com";

            // chkAtivo
            chkAtivo.AutoSize = true;
            chkAtivo.Location = new Point(20, 190);
            chkAtivo.Text = "✅ Envio de e-mail ativo";
            chkAtivo.Font = new Font("Century Gothic", 10F, FontStyle.Bold);

            // btnSalvar
            btnSalvar.Location = new Point(400, 186);
            btnSalvar.Size = new Size(160, 32);
            btnSalvar.Text = "💾 Salvar";
            btnSalvar.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnSalvar.BackColor = Color.FromArgb(0, 120, 215);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Click += btnSalvar_Click;

            // grpTeste
            grpTeste.Location = new Point(20, 235);
            grpTeste.Size = new Size(540, 80);
            grpTeste.Text = "Teste de Envio";
            grpTeste.Font = new Font("Century Gothic", 9F);

            // lblEmailTeste
            lblEmailTeste.AutoSize = true;
            lblEmailTeste.Location = new Point(12, 28);
            lblEmailTeste.Text = "Enviar para:";

            // txtEmailTeste
            txtEmailTeste.Location = new Point(100, 25);
            txtEmailTeste.Size = new Size(250, 23);
            txtEmailTeste.PlaceholderText = "teste@email.com";

            // btnTestar
            btnTestar.Location = new Point(370, 22);
            btnTestar.Size = new Size(150, 30);
            btnTestar.Text = "📧 Enviar Teste";
            btnTestar.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnTestar.BackColor = Color.FromArgb(40, 167, 69);
            btnTestar.ForeColor = Color.White;
            btnTestar.FlatStyle = FlatStyle.Flat;
            btnTestar.Click += btnTestar_Click;

            grpTeste.Controls.Add(lblEmailTeste);
            grpTeste.Controls.Add(txtEmailTeste);
            grpTeste.Controls.Add(btnTestar);

            // FrmConfiguracaoEmail
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 335);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Configuração de E-mail (SMTP)";
            Load += FrmConfiguracaoEmail_Load;

            Controls.Add(lblHost);
            Controls.Add(txtHost);
            Controls.Add(lblPorta);
            Controls.Add(nudPorta);
            Controls.Add(chkSsl);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(lblSenha);
            Controls.Add(txtSenha);
            Controls.Add(lblRemetenteNome);
            Controls.Add(txtRemetenteNome);
            Controls.Add(lblRemetenteEmail);
            Controls.Add(txtRemetenteEmail);
            Controls.Add(chkAtivo);
            Controls.Add(btnSalvar);
            Controls.Add(grpTeste);

            ((System.ComponentModel.ISupportInitialize)nudPorta).EndInit();
            grpTeste.ResumeLayout(false);
            grpTeste.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHost;
        private TextBox txtHost;
        private Label lblPorta;
        private NumericUpDown nudPorta;
        private CheckBox chkSsl;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblSenha;
        private TextBox txtSenha;
        private Label lblRemetenteNome;
        private TextBox txtRemetenteNome;
        private Label lblRemetenteEmail;
        private TextBox txtRemetenteEmail;
        private CheckBox chkAtivo;
        private Button btnSalvar;
        private GroupBox grpTeste;
        private Label lblEmailTeste;
        private TextBox txtEmailTeste;
        private Button btnTestar;
    }
}