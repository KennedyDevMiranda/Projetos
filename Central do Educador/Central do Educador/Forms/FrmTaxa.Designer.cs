namespace Central_do_Educador
{
    partial class FrmTaxa : Form // Adicione ": Form" aqui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTaxa));
            pImagem = new PictureBox();
            label7 = new Label();
            txtObs = new TextBox();
            label2 = new Label();
            chkHistorico = new CheckBox();
            label3 = new Label();
            chkNaoAgendado = new CheckBox();
            dtpFalta = new DateTimePicker();
            chkLancado = new CheckBox();
            label4 = new Label();
            label6 = new Label();
            dtpReposicao = new DateTimePicker();
            nudQtd = new NumericUpDown();
            label5 = new Label();
            dtpRegistro = new DateTimePicker();
            panel1 = new Panel();
            txtId = new TextBox();
            lblCabecalho = new Label();
            panel2 = new Panel();
            cmbAluno = new ComboBox();
            toolTip1 = new ToolTip(components);
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pImagem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudQtd).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pImagem
            // 
            pImagem.Enabled = false;
            pImagem.Location = new Point(422, 14);
            pImagem.Name = "pImagem";
            pImagem.Size = new Size(173, 220);
            pImagem.SizeMode = PictureBoxSizeMode.Zoom;
            pImagem.TabIndex = 34;
            pImagem.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(24, 200);
            label7.Name = "label7";
            label7.Size = new Size(37, 17);
            label7.TabIndex = 33;
            label7.Text = "Obs:";
            // 
            // txtObs
            // 
            txtObs.CharacterCasing = CharacterCasing.Upper;
            txtObs.Location = new Point(24, 220);
            txtObs.Multiline = true;
            txtObs.Name = "txtObs";
            txtObs.Size = new Size(349, 69);
            txtObs.TabIndex = 8;
            toolTip1.SetToolTip(txtObs, "Observação");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 14);
            label2.Name = "label2";
            label2.Size = new Size(45, 17);
            label2.TabIndex = 20;
            label2.Text = "Aluno";
            // 
            // chkHistorico
            // 
            chkHistorico.AutoSize = true;
            chkHistorico.Cursor = Cursors.Hand;
            chkHistorico.Location = new Point(264, 172);
            chkHistorico.Name = "chkHistorico";
            chkHistorico.Size = new Size(82, 21);
            chkHistorico.TabIndex = 7;
            chkHistorico.Text = "Histórico";
            toolTip1.SetToolTip(chkHistorico, "Histórico feito");
            chkHistorico.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 64);
            label3.Name = "label3";
            label3.Size = new Size(99, 17);
            label3.TabIndex = 21;
            label3.Text = "Data da Falta";
            // 
            // chkNaoAgendado
            // 
            chkNaoAgendado.AutoSize = true;
            chkNaoAgendado.Cursor = Cursors.Hand;
            chkNaoAgendado.Location = new Point(122, 172);
            chkNaoAgendado.Name = "chkNaoAgendado";
            chkNaoAgendado.Size = new Size(129, 21);
            chkNaoAgendado.TabIndex = 6;
            chkNaoAgendado.Text = "Não agendado";
            toolTip1.SetToolTip(chkNaoAgendado, "Foi agendado?");
            chkNaoAgendado.UseVisualStyleBackColor = true;
            chkNaoAgendado.CheckedChanged += chkNaoAgendado_CheckedChanged;
            // 
            // dtpFalta
            // 
            dtpFalta.Cursor = Cursors.Hand;
            dtpFalta.Format = DateTimePickerFormat.Short;
            dtpFalta.Location = new Point(24, 84);
            dtpFalta.Name = "dtpFalta";
            dtpFalta.Size = new Size(108, 23);
            dtpFalta.TabIndex = 1;
            toolTip1.SetToolTip(dtpFalta, "Data da falta");
            // 
            // chkLancado
            // 
            chkLancado.AutoSize = true;
            chkLancado.Cursor = Cursors.Hand;
            chkLancado.Location = new Point(24, 172);
            chkLancado.Name = "chkLancado";
            chkLancado.Size = new Size(85, 21);
            chkLancado.TabIndex = 5;
            chkLancado.Text = "Lançado";
            toolTip1.SetToolTip(chkLancado, "Lançado?");
            chkLancado.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(144, 64);
            label4.Name = "label4";
            label4.Size = new Size(135, 17);
            label4.TabIndex = 23;
            label4.Text = "Data da Reposição";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 117);
            label6.Name = "label6";
            label6.Size = new Size(94, 17);
            label6.TabIndex = 28;
            label6.Text = "Qtd. de Falta";
            // 
            // dtpReposicao
            // 
            dtpReposicao.Cursor = Cursors.Hand;
            dtpReposicao.Format = DateTimePickerFormat.Short;
            dtpReposicao.Location = new Point(144, 84);
            dtpReposicao.Name = "dtpReposicao";
            dtpReposicao.Size = new Size(108, 23);
            dtpReposicao.TabIndex = 2;
            toolTip1.SetToolTip(dtpReposicao, "Data da Reposição");
            // 
            // nudQtd
            // 
            nudQtd.Cursor = Cursors.Hand;
            nudQtd.Location = new Point(24, 137);
            nudQtd.Name = "nudQtd";
            nudQtd.Size = new Size(109, 23);
            nudQtd.TabIndex = 4;
            toolTip1.SetToolTip(nudQtd, "Quantidade de falta");
            nudQtd.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(291, 64);
            label5.Name = "label5";
            label5.Size = new Size(118, 17);
            label5.TabIndex = 25;
            label5.Text = "Data do Registro";
            // 
            // dtpRegistro
            // 
            dtpRegistro.Cursor = Cursors.Hand;
            dtpRegistro.Format = DateTimePickerFormat.Short;
            dtpRegistro.Location = new Point(291, 84);
            dtpRegistro.Name = "dtpRegistro";
            dtpRegistro.Size = new Size(108, 23);
            dtpRegistro.TabIndex = 3;
            toolTip1.SetToolTip(dtpRegistro, "Data do registro");
            // 
            // panel1
            // 
            panel1.Controls.Add(txtId);
            panel1.Controls.Add(lblCabecalho);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(619, 39);
            panel1.TabIndex = 35;
            // 
            // txtId
            // 
            txtId.Location = new Point(3, 3);
            txtId.Name = "txtId";
            txtId.Size = new Size(55, 23);
            txtId.TabIndex = 35;
            txtId.Text = "Id";
            txtId.Visible = false;
            // 
            // lblCabecalho
            // 
            lblCabecalho.AutoSize = true;
            lblCabecalho.Location = new Point(231, 11);
            lblCabecalho.Name = "lblCabecalho";
            lblCabecalho.Size = new Size(133, 17);
            lblCabecalho.TabIndex = 3;
            lblCabecalho.Text = "CONTROLE DE TAXA";
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(cmbAluno);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(dtpRegistro);
            panel2.Controls.Add(pImagem);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(nudQtd);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(dtpReposicao);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(txtObs);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(chkLancado);
            panel2.Controls.Add(chkHistorico);
            panel2.Controls.Add(dtpFalta);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(chkNaoAgendado);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 39);
            panel2.Name = "panel2";
            panel2.Size = new Size(619, 317);
            panel2.TabIndex = 0;
            // 
            // cmbAluno
            // 
            cmbAluno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAluno.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbAluno.FormattingEnabled = true;
            cmbAluno.Location = new Point(24, 34);
            cmbAluno.Name = "cmbAluno";
            cmbAluno.Size = new Size(330, 25);
            cmbAluno.TabIndex = 0;
            cmbAluno.SelectionChangeCommitted += cmbAluno_SelectionChangeCommitted;
            // 
            // pictureBox1
            // 
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources.save;
            pictureBox1.Location = new Point(442, 240);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(35, 35);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 35;
            pictureBox1.TabStop = false;
            toolTip1.SetToolTip(pictureBox1, "Salvar");
            pictureBox1.Click += btnSalvar_Click;
            // 
            // FrmControleTaxa
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(619, 356);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmControleTaxa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Controle de Taxa";
            Activated += FrmControleTaxa_Activated;
            Load += FrmControleTaxa_Load;
            KeyDown += FrmControleTaxa_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pImagem).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudQtd).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pImagem;
        private Label label7;
        private TextBox txtObs;
        private Label label2;
        private CheckBox chkHistorico;
        private Label label3;
        private CheckBox chkNaoAgendado;
        private DateTimePicker dtpFalta;
        private CheckBox chkLancado;
        private Label label4;
        private Label label6;
        private DateTimePicker dtpReposicao;
        private NumericUpDown nudQtd;
        private Label label5;
        private DateTimePicker dtpRegistro;
        private Panel panel1;
        private Label lblCabecalho;
        private Panel panel2;
        public TextBox txtId;
        private ToolTip toolTip1;
        private ComboBox cmbAluno;
        private PictureBox pictureBox1;
    }
}