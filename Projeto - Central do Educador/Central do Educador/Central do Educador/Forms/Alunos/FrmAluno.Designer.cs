namespace Central_do_Educador
{
    partial class FrmAluno : Form // Corrigido: herda de Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAluno));
            btnSalvar = new Button();
            txtAluno = new TextBox();
            lblCabecalho = new Label();
            label2 = new Label();
            panelMain = new Panel();
            chkAlunoAtivo = new CheckBox();
            maskNumResp = new MaskedTextBox();
            maskNumAluno = new MaskedTextBox();
            label3 = new Label();
            label4 = new Label();
            txtEmail = new TextBox();
            label1 = new Label();
            pImagem = new PictureBox();
            panelCabecalho = new Panel();
            textBox1 = new TextBox();
            label5 = new Label();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pImagem).BeginInit();
            panelCabecalho.SuspendLayout();
            SuspendLayout();
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(389, 233);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(86, 26);
            btnSalvar.TabIndex = 0;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // txtAluno
            // 
            txtAluno.CharacterCasing = CharacterCasing.Upper;
            txtAluno.Location = new Point(14, 24);
            txtAluno.Name = "txtAluno";
            txtAluno.Size = new Size(302, 23);
            txtAluno.TabIndex = 1;
            // 
            // lblCabecalho
            // 
            lblCabecalho.AutoSize = true;
            lblCabecalho.Location = new Point(215, 10);
            lblCabecalho.Name = "lblCabecalho";
            lblCabecalho.Size = new Size(146, 17);
            lblCabecalho.TabIndex = 2;
            lblCabecalho.Text = "CADASTRO DE ALUNO";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 3);
            label2.Name = "label2";
            label2.Size = new Size(45, 17);
            label2.TabIndex = 3;
            label2.Text = "Aluno";
            // 
            // panelMain
            // 
            panelMain.Controls.Add(textBox1);
            panelMain.Controls.Add(label5);
            panelMain.Controls.Add(chkAlunoAtivo);
            panelMain.Controls.Add(maskNumResp);
            panelMain.Controls.Add(maskNumAluno);
            panelMain.Controls.Add(label3);
            panelMain.Controls.Add(label4);
            panelMain.Controls.Add(txtEmail);
            panelMain.Controls.Add(label1);
            panelMain.Controls.Add(pImagem);
            panelMain.Controls.Add(btnSalvar);
            panelMain.Controls.Add(txtAluno);
            panelMain.Controls.Add(label2);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 35);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(577, 298);
            panelMain.TabIndex = 17;
            // 
            // chkAlunoAtivo
            // 
            chkAlunoAtivo.AutoSize = true;
            chkAlunoAtivo.Checked = true;
            chkAlunoAtivo.CheckState = CheckState.Checked;
            chkAlunoAtivo.Location = new Point(14, 260);
            chkAlunoAtivo.Name = "chkAlunoAtivo";
            chkAlunoAtivo.Size = new Size(61, 21);
            chkAlunoAtivo.TabIndex = 27;
            chkAlunoAtivo.Text = "Ativo";
            chkAlunoAtivo.UseVisualStyleBackColor = true;
            // 
            // maskNumResp
            // 
            maskNumResp.Location = new Point(14, 217);
            maskNumResp.Mask = "(00) 00000-9999";
            maskNumResp.Name = "maskNumResp";
            maskNumResp.Size = new Size(302, 23);
            maskNumResp.TabIndex = 26;
            // 
            // maskNumAluno
            // 
            maskNumAluno.Location = new Point(14, 168);
            maskNumAluno.Mask = "(00) 00000-9999";
            maskNumAluno.Name = "maskNumAluno";
            maskNumAluno.Size = new Size(302, 23);
            maskNumAluno.TabIndex = 25;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 197);
            label3.Name = "label3";
            label3.Size = new Size(166, 17);
            label3.TabIndex = 23;
            label3.Text = "Número do Responsável";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 147);
            label4.Name = "label4";
            label4.Size = new Size(141, 17);
            label4.TabIndex = 21;
            label4.Text = "Número do Aluno(a)";
            // 
            // txtEmail
            // 
            txtEmail.CharacterCasing = CharacterCasing.Lower;
            txtEmail.Location = new Point(14, 74);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(302, 23);
            txtEmail.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 53);
            label1.Name = "label1";
            label1.Size = new Size(47, 17);
            label1.TabIndex = 19;
            label1.Text = "E-mail";
            // 
            // pImagem
            // 
            pImagem.Location = new Point(389, 7);
            pImagem.Name = "pImagem";
            pImagem.Size = new Size(173, 220);
            pImagem.SizeMode = PictureBoxSizeMode.Zoom;
            pImagem.TabIndex = 17;
            pImagem.TabStop = false;
            // 
            // panelCabecalho
            // 
            panelCabecalho.Controls.Add(lblCabecalho);
            panelCabecalho.Dock = DockStyle.Top;
            panelCabecalho.Location = new Point(0, 0);
            panelCabecalho.Name = "panelCabecalho";
            panelCabecalho.Size = new Size(577, 35);
            panelCabecalho.TabIndex = 17;
            // 
            // textBox1
            // 
            textBox1.CharacterCasing = CharacterCasing.Upper;
            textBox1.Location = new Point(14, 121);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(302, 23);
            textBox1.TabIndex = 28;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 100);
            label5.Name = "label5";
            label5.Size = new Size(88, 17);
            label5.TabIndex = 29;
            label5.Text = "Responsável";
            // 
            // FrmAluno
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(577, 333);
            Controls.Add(panelMain);
            Controls.Add(panelCabecalho);
            Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAluno";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Aluno";
            Activated += FrmAluno_Activated;
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pImagem).EndInit();
            panelCabecalho.ResumeLayout(false);
            panelCabecalho.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSalvar;
        private TextBox txtAluno;
        private Label lblCabecalho;
        private Label label2;
        private Panel panelMain;
        private Panel panelCabecalho;
        private PictureBox pImagem;
        private TextBox txtEmail;
        private Label label1;
        private TextBox txtNumResp;
        private Label label3;
        private TextBox txtNumAluno;
        private Label label4;
        private CheckBox chkAlunoAtivo;
        private MaskedTextBox maskedTextBox2;
        private MaskedTextBox maskNumAluno;
        private MaskedTextBox maskNumResp;
        private TextBox textBox1;
        private Label label5;
    }
}