namespace Central_do_Educador.Forms
{
    partial class FrmLogin
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
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            txtLogin = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtSenha = new TextBox();
            toolTip1 = new ToolTip(components);
            btnEntrar = new PictureBox();
            btnCancelar = new PictureBox();
            chkLembrarSenha = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)btnEntrar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnCancelar).BeginInit();
            SuspendLayout();
            // 
            // txtLogin
            // 
            txtLogin.Cursor = Cursors.IBeam;
            txtLogin.Location = new Point(103, 42);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(100, 22);
            txtLogin.TabIndex = 0;
            toolTip1.SetToolTip(txtLogin, "Usuário");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(103, 22);
            label1.Name = "label1";
            label1.Size = new Size(51, 17);
            label1.TabIndex = 2;
            label1.Text = "Usuário";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(103, 72);
            label2.Name = "label2";
            label2.Size = new Size(45, 17);
            label2.TabIndex = 4;
            label2.Text = "Senha";
            // 
            // txtSenha
            // 
            txtSenha.Cursor = Cursors.IBeam;
            txtSenha.Location = new Point(103, 92);
            txtSenha.Name = "txtSenha";
            txtSenha.PasswordChar = '*';
            txtSenha.Size = new Size(100, 22);
            txtSenha.TabIndex = 1;
            toolTip1.SetToolTip(txtSenha, "Senha");
            // 
            // btnEntrar
            // 
            btnEntrar.Cursor = Cursors.Hand;
            btnEntrar.Image = Properties.Resources.enter;
            btnEntrar.Location = new Point(196, 173);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.Size = new Size(35, 35);
            btnEntrar.SizeMode = PictureBoxSizeMode.Zoom;
            btnEntrar.TabIndex = 7;
            btnEntrar.TabStop = false;
            toolTip1.SetToolTip(btnEntrar, "Entrar");
            btnEntrar.Click += btnEntrar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Image = Properties.Resources.close_login;
            btnCancelar.Location = new Point(83, 173);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(35, 35);
            btnCancelar.SizeMode = PictureBoxSizeMode.Zoom;
            btnCancelar.TabIndex = 8;
            btnCancelar.TabStop = false;
            toolTip1.SetToolTip(btnCancelar, "Sair");
            btnCancelar.Click += btnCancelar_Click;
            // 
            // chkLembrarSenha
            // 
            chkLembrarSenha.AutoSize = true;
            chkLembrarSenha.Cursor = Cursors.Hand;
            chkLembrarSenha.Location = new Point(55, 134);
            chkLembrarSenha.Name = "chkLembrarSenha";
            chkLembrarSenha.Size = new Size(114, 21);
            chkLembrarSenha.TabIndex = 6;
            chkLembrarSenha.Text = "Lembrar senha";
            chkLembrarSenha.UseVisualStyleBackColor = true;
            chkLembrarSenha.CheckedChanged += chkLembrarSenha_CheckedChanged;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(310, 236);
            Controls.Add(btnCancelar);
            Controls.Add(btnEntrar);
            Controls.Add(chkLembrarSenha);
            Controls.Add(label2);
            Controls.Add(txtSenha);
            Controls.Add(label1);
            Controls.Add(txtLogin);
            Font = new Font("Century Gothic", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += FrmLogin_Load;
            ((System.ComponentModel.ISupportInitialize)btnEntrar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnCancelar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtLogin;
        private Label label1;
        private Label label2;
        private TextBox txtSenha;
        private ToolTip toolTip1;
        private CheckBox chkLembrarSenha;
        private PictureBox btnEntrar;
        private PictureBox btnCancelar;
    }
}