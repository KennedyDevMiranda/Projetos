namespace Central_do_Educador.Forms
{
    partial class FrmControleTaxa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmControleTaxa));
            panelFiltro = new Panel();
            txtFiltroUsuario = new TextBox();
            label9 = new Label();
            txtFiltroFaltas = new TextBox();
            label8 = new Label();
            dtpFiltroDataRegistro = new DateTimePicker();
            label7 = new Label();
            dtpFiltroDataReposicao = new DateTimePicker();
            label6 = new Label();
            dtpFiltroDataFalta = new DateTimePicker();
            label5 = new Label();
            label4 = new Label();
            cmbFiltroLancado = new ComboBox();
            panelCabecalho = new Panel();
            picAddTaxa = new PictureBox();
            picPrepArquivo = new PictureBox();
            btnFiltros = new PictureBox();
            btnEditar = new PictureBox();
            btnExcluir = new PictureBox();
            label3 = new Label();
            txtFiltroAluno = new TextBox();
            label2 = new Label();
            label1 = new Label();
            dtpAte = new DateTimePicker();
            dtpDe = new DateTimePicker();
            panel1 = new Panel();
            dgvCT = new DataGridView();
            panel3 = new Panel();
            lblUsuario = new Label();
            lblComissao = new Label();
            lblTotal = new Label();
            lblTotalFaltas = new Label();
            lblTotalRegistros = new Label();
            panelFiltro.SuspendLayout();
            panelCabecalho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAddTaxa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPrepArquivo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnFiltros).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEditar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnExcluir).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCT).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panelFiltro
            // 
            panelFiltro.Controls.Add(txtFiltroUsuario);
            panelFiltro.Controls.Add(label9);
            panelFiltro.Controls.Add(txtFiltroFaltas);
            panelFiltro.Controls.Add(label8);
            panelFiltro.Controls.Add(dtpFiltroDataRegistro);
            panelFiltro.Controls.Add(label7);
            panelFiltro.Controls.Add(dtpFiltroDataReposicao);
            panelFiltro.Controls.Add(label6);
            panelFiltro.Controls.Add(dtpFiltroDataFalta);
            panelFiltro.Controls.Add(label5);
            panelFiltro.Controls.Add(label4);
            panelFiltro.Controls.Add(cmbFiltroLancado);
            panelFiltro.Dock = DockStyle.Left;
            panelFiltro.Location = new Point(0, 75);
            panelFiltro.Name = "panelFiltro";
            panelFiltro.Size = new Size(139, 508);
            panelFiltro.TabIndex = 7;
            // 
            // txtFiltroUsuario
            // 
            txtFiltroUsuario.CharacterCasing = CharacterCasing.Upper;
            txtFiltroUsuario.Location = new Point(12, 257);
            txtFiltroUsuario.Name = "txtFiltroUsuario";
            txtFiltroUsuario.Size = new Size(115, 23);
            txtFiltroUsuario.TabIndex = 18;
            txtFiltroUsuario.TextChanged += FiltroLateral_Changed;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 237);
            label9.Name = "label9";
            label9.Size = new Size(47, 15);
            label9.TabIndex = 19;
            label9.Text = "Usuário";
            // 
            // txtFiltroFaltas
            // 
            txtFiltroFaltas.Location = new Point(12, 164);
            txtFiltroFaltas.Name = "txtFiltroFaltas";
            txtFiltroFaltas.Size = new Size(115, 23);
            txtFiltroFaltas.TabIndex = 12;
            txtFiltroFaltas.TextChanged += FiltroLateral_Changed;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 144);
            label8.Name = "label8";
            label8.Size = new Size(37, 15);
            label8.TabIndex = 17;
            label8.Text = "Faltas";
            // 
            // dtpFiltroDataRegistro
            // 
            dtpFiltroDataRegistro.Checked = false;
            dtpFiltroDataRegistro.Format = DateTimePickerFormat.Short;
            dtpFiltroDataRegistro.Location = new Point(12, 117);
            dtpFiltroDataRegistro.Name = "dtpFiltroDataRegistro";
            dtpFiltroDataRegistro.ShowCheckBox = true;
            dtpFiltroDataRegistro.Size = new Size(115, 23);
            dtpFiltroDataRegistro.TabIndex = 16;
            dtpFiltroDataRegistro.ValueChanged += FiltroLateral_Changed;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 97);
            label7.Name = "label7";
            label7.Size = new Size(94, 15);
            label7.TabIndex = 15;
            label7.Text = "Data do Registro";
            // 
            // dtpFiltroDataReposicao
            // 
            dtpFiltroDataReposicao.Checked = false;
            dtpFiltroDataReposicao.Format = DateTimePickerFormat.Short;
            dtpFiltroDataReposicao.Location = new Point(12, 70);
            dtpFiltroDataReposicao.Name = "dtpFiltroDataReposicao";
            dtpFiltroDataReposicao.ShowCheckBox = true;
            dtpFiltroDataReposicao.Size = new Size(115, 23);
            dtpFiltroDataReposicao.TabIndex = 14;
            dtpFiltroDataReposicao.ValueChanged += FiltroLateral_Changed;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 50);
            label6.Name = "label6";
            label6.Size = new Size(104, 15);
            label6.TabIndex = 13;
            label6.Text = "Data da Reposição";
            // 
            // dtpFiltroDataFalta
            // 
            dtpFiltroDataFalta.Checked = false;
            dtpFiltroDataFalta.Format = DateTimePickerFormat.Short;
            dtpFiltroDataFalta.Location = new Point(12, 23);
            dtpFiltroDataFalta.Name = "dtpFiltroDataFalta";
            dtpFiltroDataFalta.ShowCheckBox = true;
            dtpFiltroDataFalta.Size = new Size(115, 23);
            dtpFiltroDataFalta.TabIndex = 12;
            dtpFiltroDataFalta.ValueChanged += FiltroLateral_Changed;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 3);
            label5.Name = "label5";
            label5.Size = new Size(75, 15);
            label5.TabIndex = 10;
            label5.Text = "Data da Falta";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 189);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 9;
            label4.Text = "Lançados";
            // 
            // cmbFiltroLancado
            // 
            cmbFiltroLancado.Cursor = Cursors.Hand;
            cmbFiltroLancado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroLancado.FormattingEnabled = true;
            cmbFiltroLancado.Location = new Point(12, 209);
            cmbFiltroLancado.Name = "cmbFiltroLancado";
            cmbFiltroLancado.Size = new Size(115, 23);
            cmbFiltroLancado.TabIndex = 9;
            // 
            // panelCabecalho
            // 
            panelCabecalho.Controls.Add(picAddTaxa);
            panelCabecalho.Controls.Add(picPrepArquivo);
            panelCabecalho.Controls.Add(btnFiltros);
            panelCabecalho.Controls.Add(btnEditar);
            panelCabecalho.Controls.Add(btnExcluir);
            panelCabecalho.Controls.Add(label3);
            panelCabecalho.Controls.Add(txtFiltroAluno);
            panelCabecalho.Controls.Add(label2);
            panelCabecalho.Controls.Add(label1);
            panelCabecalho.Controls.Add(dtpAte);
            panelCabecalho.Controls.Add(dtpDe);
            panelCabecalho.Dock = DockStyle.Top;
            panelCabecalho.Location = new Point(0, 0);
            panelCabecalho.Name = "panelCabecalho";
            panelCabecalho.Size = new Size(1226, 75);
            panelCabecalho.TabIndex = 8;
            // 
            // picAddTaxa
            // 
            picAddTaxa.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picAddTaxa.Cursor = Cursors.Hand;
            picAddTaxa.Image = Properties.Resources.add;
            picAddTaxa.Location = new Point(869, 34);
            picAddTaxa.Name = "picAddTaxa";
            picAddTaxa.Size = new Size(35, 35);
            picAddTaxa.SizeMode = PictureBoxSizeMode.Zoom;
            picAddTaxa.TabIndex = 13;
            picAddTaxa.TabStop = false;
            picAddTaxa.Click += picAddTaxa_Click;
            // 
            // picPrepArquivo
            // 
            picPrepArquivo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picPrepArquivo.Cursor = Cursors.Hand;
            picPrepArquivo.Image = Properties.Resources.documento;
            picPrepArquivo.Location = new Point(992, 34);
            picPrepArquivo.Name = "picPrepArquivo";
            picPrepArquivo.Size = new Size(35, 35);
            picPrepArquivo.SizeMode = PictureBoxSizeMode.Zoom;
            picPrepArquivo.TabIndex = 12;
            picPrepArquivo.TabStop = false;
            picPrepArquivo.Click += picPrepArquivo_Click;
            // 
            // btnFiltros
            // 
            btnFiltros.Cursor = Cursors.Hand;
            btnFiltros.Image = Properties.Resources.filter_hide;
            btnFiltros.Location = new Point(12, 34);
            btnFiltros.Name = "btnFiltros";
            btnFiltros.Size = new Size(35, 35);
            btnFiltros.SizeMode = PictureBoxSizeMode.Zoom;
            btnFiltros.TabIndex = 11;
            btnFiltros.TabStop = false;
            // 
            // btnEditar
            // 
            btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.Image = Properties.Resources.edit;
            btnEditar.Location = new Point(910, 34);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(35, 35);
            btnEditar.SizeMode = PictureBoxSizeMode.Zoom;
            btnEditar.TabIndex = 10;
            btnEditar.TabStop = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExcluir.Cursor = Cursors.Hand;
            btnExcluir.Image = Properties.Resources.delete;
            btnExcluir.Location = new Point(951, 34);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(35, 35);
            btnExcluir.SizeMode = PictureBoxSizeMode.Zoom;
            btnExcluir.TabIndex = 9;
            btnExcluir.TabStop = false;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 6);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 5;
            label3.Text = "Nome";
            // 
            // txtFiltroAluno
            // 
            txtFiltroAluno.CharacterCasing = CharacterCasing.Upper;
            txtFiltroAluno.Location = new Point(62, 3);
            txtFiltroAluno.Name = "txtFiltroAluno";
            txtFiltroAluno.Size = new Size(175, 23);
            txtFiltroAluno.TabIndex = 4;
            txtFiltroAluno.TextChanged += txtFiltroAluno_TextChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(1084, 45);
            label2.Name = "label2";
            label2.Size = new Size(28, 15);
            label2.TabIndex = 3;
            label2.Text = "Até:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(1045, 16);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 2;
            label1.Text = "Periodo de:";
            // 
            // dtpAte
            // 
            dtpAte.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtpAte.Cursor = Cursors.Hand;
            dtpAte.Format = DateTimePickerFormat.Short;
            dtpAte.Location = new Point(1118, 41);
            dtpAte.Name = "dtpAte";
            dtpAte.Size = new Size(96, 23);
            dtpAte.TabIndex = 1;
            //dtpAte.ValueChanged += dtpAte_ValueChanged;
            // 
            // dtpDe
            // 
            dtpDe.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtpDe.Cursor = Cursors.Hand;
            dtpDe.Format = DateTimePickerFormat.Short;
            dtpDe.Location = new Point(1118, 12);
            dtpDe.Name = "dtpDe";
            dtpDe.Size = new Size(96, 23);
            dtpDe.TabIndex = 0;
            dtpDe.Value = new DateTime(2026, 3, 1, 0, 0, 0, 0);
            //dtpDe.ValueChanged += dtpDe_ValueChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(dgvCT);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(139, 75);
            panel1.Name = "panel1";
            panel1.Size = new Size(1087, 508);
            panel1.TabIndex = 9;
            // 
            // dgvCT
            // 
            dgvCT.BackgroundColor = SystemColors.Control;
            dgvCT.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCT.Dock = DockStyle.Fill;
            dgvCT.Location = new Point(0, 0);
            dgvCT.Name = "dgvCT";
            dgvCT.Size = new Size(1087, 508);
            dgvCT.TabIndex = 0;
            dgvCT.CellContentClick += dgvCT_CellDoubleClick;
            dgvCT.CellFormatting += dgvCT_CellFormatting;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblUsuario);
            panel3.Controls.Add(lblComissao);
            panel3.Controls.Add(lblTotal);
            panel3.Controls.Add(lblTotalFaltas);
            panel3.Controls.Add(lblTotalRegistros);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 583);
            panel3.Name = "panel3";
            panel3.Size = new Size(1226, 27);
            panel3.TabIndex = 10;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(3, 5);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(12, 15);
            lblUsuario.TabIndex = 4;
            lblUsuario.Text = "-";
            // 
            // lblComissao
            // 
            lblComissao.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblComissao.AutoSize = true;
            lblComissao.Location = new Point(1118, 5);
            lblComissao.Name = "lblComissao";
            lblComissao.Size = new Size(12, 15);
            lblComissao.TabIndex = 3;
            lblComissao.Text = "-";
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(982, 5);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(12, 15);
            lblTotal.TabIndex = 2;
            lblTotal.Text = "-";
            // 
            // lblTotalFaltas
            // 
            lblTotalFaltas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalFaltas.AutoSize = true;
            lblTotalFaltas.Location = new Point(853, 5);
            lblTotalFaltas.Name = "lblTotalFaltas";
            lblTotalFaltas.Size = new Size(12, 15);
            lblTotalFaltas.TabIndex = 1;
            lblTotalFaltas.Text = "-";
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Location = new Point(733, 5);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(12, 15);
            lblTotalRegistros.TabIndex = 0;
            lblTotalRegistros.Text = "-";
            // 
            // FrmControleTaxa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1226, 610);
            Controls.Add(panel1);
            Controls.Add(panelFiltro);
            Controls.Add(panelCabecalho);
            Controls.Add(panel3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmControleTaxa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Controle Taxa";
            Activated += FrmControleTaxa_Activated;
            Load += FrmControleTaxa_Load;
            panelFiltro.ResumeLayout(false);
            panelFiltro.PerformLayout();
            panelCabecalho.ResumeLayout(false);
            panelCabecalho.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAddTaxa).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPrepArquivo).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnFiltros).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEditar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnExcluir).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCT).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelFiltro;
        private TextBox txtFiltroUsuario;
        private Label label9;
        private TextBox txtFiltroFaltas;
        private Label label8;
        private DateTimePicker dtpFiltroDataRegistro;
        private Label label7;
        private DateTimePicker dtpFiltroDataReposicao;
        private Label label6;
        private DateTimePicker dtpFiltroDataFalta;
        private Label label5;
        private Label label4;
        private ComboBox cmbFiltroLancado;
        private Panel panelCabecalho;
        private PictureBox picPrepArquivo;
        private PictureBox btnFiltros;
        private PictureBox btnEditar;
        private PictureBox btnExcluir;
        private Label label3;
        private TextBox txtFiltroAluno;
        private Label label2;
        private Label label1;
        private DateTimePicker dtpAte;
        private DateTimePicker dtpDe;
        private Panel panel1;
        private DataGridView dgvCT;
        private Panel panel3;
        private Label lblUsuario;
        private Label lblComissao;
        private Label lblTotal;
        private Label lblTotalFaltas;
        private Label lblTotalRegistros;
        private PictureBox picAddTaxa;
    }
}