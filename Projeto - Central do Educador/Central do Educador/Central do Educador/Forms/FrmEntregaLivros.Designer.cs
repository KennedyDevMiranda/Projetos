namespace Central_do_Educador
{
    partial class FrmEntregaLivros
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEntregaLivros));
            dgvEntregas = new DataGridView();
            btnImportar = new Button();
            panelCabecalho = new Panel();
            groupBox2 = new GroupBox();
            cmbMateria = new ComboBox();
            groupBox1 = new GroupBox();
            cmbHorario = new ComboBox();
            grpDiaSemana = new GroupBox();
            cmbDiaSemana = new ComboBox();
            grpLivroEntregue = new GroupBox();
            rdbNao = new RadioButton();
            rdbSim = new RadioButton();
            rdbTodos = new RadioButton();
            grpAluno = new GroupBox();
            cmbPesquisaAluno = new ComboBox();
            btnExportarPDF = new Button();
            btnExcluir = new Button();
            btnPrepArquivo = new Button();
            btnConfirmarImportacao = new Button();
            btnFiltros = new Button();
            panelGrid = new Panel();
            panelRodape = new Panel();
            lblEmissao = new Label();
            lblTotal = new Label();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvEntregas).BeginInit();
            panelCabecalho.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            grpDiaSemana.SuspendLayout();
            grpLivroEntregue.SuspendLayout();
            grpAluno.SuspendLayout();
            panelGrid.SuspendLayout();
            panelRodape.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvEntregas
            // 
            dgvEntregas.BackgroundColor = SystemColors.Control;
            dgvEntregas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEntregas.Dock = DockStyle.Fill;
            dgvEntregas.Location = new Point(0, 0);
            dgvEntregas.Name = "dgvEntregas";
            dgvEntregas.Size = new Size(994, 471);
            dgvEntregas.TabIndex = 0;
            // 
            // btnImportar
            // 
            btnImportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnImportar.Location = new Point(30, 72);
            btnImportar.Name = "btnImportar";
            btnImportar.Size = new Size(75, 27);
            btnImportar.TabIndex = 1;
            btnImportar.Text = "Importar";
            btnImportar.UseVisualStyleBackColor = true;
            btnImportar.Click += btnImportar_Click;
            // 
            // panelCabecalho
            // 
            panelCabecalho.Controls.Add(groupBox2);
            panelCabecalho.Controls.Add(groupBox1);
            panelCabecalho.Controls.Add(grpDiaSemana);
            panelCabecalho.Controls.Add(grpLivroEntregue);
            panelCabecalho.Controls.Add(grpAluno);
            panelCabecalho.Dock = DockStyle.Top;
            panelCabecalho.Location = new Point(0, 0);
            panelCabecalho.Name = "panelCabecalho";
            panelCabecalho.Size = new Size(1124, 91);
            panelCabecalho.TabIndex = 4;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cmbMateria);
            groupBox2.Location = new Point(238, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(223, 60);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Matéria";
            // 
            // cmbMateria
            // 
            cmbMateria.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbMateria.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbMateria.FormattingEnabled = true;
            cmbMateria.Location = new Point(6, 22);
            cmbMateria.Name = "cmbMateria";
            cmbMateria.Size = new Size(208, 25);
            cmbMateria.TabIndex = 1;
            cmbMateria.TextChanged += AplicarFiltros;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbHorario);
            groupBox1.Location = new Point(616, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(146, 60);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Horário";
            // 
            // cmbHorario
            // 
            cmbHorario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHorario.FormattingEnabled = true;
            cmbHorario.Location = new Point(6, 22);
            cmbHorario.Name = "cmbHorario";
            cmbHorario.Size = new Size(131, 25);
            cmbHorario.TabIndex = 1;
            cmbHorario.SelectedIndexChanged += AplicarFiltros;
            // 
            // grpDiaSemana
            // 
            grpDiaSemana.Controls.Add(cmbDiaSemana);
            grpDiaSemana.Location = new Point(467, 12);
            grpDiaSemana.Name = "grpDiaSemana";
            grpDiaSemana.Size = new Size(143, 60);
            grpDiaSemana.TabIndex = 8;
            grpDiaSemana.TabStop = false;
            grpDiaSemana.Text = "Dia da Semana";
            // 
            // cmbDiaSemana
            // 
            cmbDiaSemana.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDiaSemana.FormattingEnabled = true;
            cmbDiaSemana.Items.AddRange(new object[] { "Todos", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado", "Domingo" });
            cmbDiaSemana.Location = new Point(6, 22);
            cmbDiaSemana.Name = "cmbDiaSemana";
            cmbDiaSemana.Size = new Size(131, 25);
            cmbDiaSemana.TabIndex = 0;
            cmbDiaSemana.SelectedIndexChanged += AplicarFiltros;
            // 
            // grpLivroEntregue
            // 
            grpLivroEntregue.Controls.Add(rdbNao);
            grpLivroEntregue.Controls.Add(rdbSim);
            grpLivroEntregue.Controls.Add(rdbTodos);
            grpLivroEntregue.Location = new Point(768, 12);
            grpLivroEntregue.Name = "grpLivroEntregue";
            grpLivroEntregue.Size = new Size(200, 60);
            grpLivroEntregue.TabIndex = 7;
            grpLivroEntregue.TabStop = false;
            grpLivroEntregue.Text = "Livro Entregue";
            // 
            // rdbNao
            // 
            rdbNao.AutoSize = true;
            rdbNao.Location = new Point(125, 25);
            rdbNao.Name = "rdbNao";
            rdbNao.Size = new Size(51, 21);
            rdbNao.TabIndex = 2;
            rdbNao.Text = "Não";
            rdbNao.UseVisualStyleBackColor = true;
            rdbNao.CheckedChanged += AplicarFiltros;
            // 
            // rdbSim
            // 
            rdbSim.AutoSize = true;
            rdbSim.Location = new Point(72, 25);
            rdbSim.Name = "rdbSim";
            rdbSim.Size = new Size(47, 21);
            rdbSim.TabIndex = 1;
            rdbSim.Text = "Sim";
            rdbSim.UseVisualStyleBackColor = true;
            rdbSim.CheckedChanged += AplicarFiltros;
            // 
            // rdbTodos
            // 
            rdbTodos.AutoSize = true;
            rdbTodos.Checked = true;
            rdbTodos.Location = new Point(6, 25);
            rdbTodos.Name = "rdbTodos";
            rdbTodos.Size = new Size(60, 21);
            rdbTodos.TabIndex = 0;
            rdbTodos.TabStop = true;
            rdbTodos.Text = "Todos";
            rdbTodos.UseVisualStyleBackColor = true;
            rdbTodos.CheckedChanged += AplicarFiltros;
            // 
            // grpAluno
            // 
            grpAluno.Controls.Add(cmbPesquisaAluno);
            grpAluno.Location = new Point(12, 12);
            grpAluno.Name = "grpAluno";
            grpAluno.Size = new Size(220, 60);
            grpAluno.TabIndex = 6;
            grpAluno.TabStop = false;
            grpAluno.Text = "Aluno";
            // 
            // cmbPesquisaAluno
            // 
            cmbPesquisaAluno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbPesquisaAluno.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbPesquisaAluno.FormattingEnabled = true;
            cmbPesquisaAluno.Location = new Point(6, 22);
            cmbPesquisaAluno.Name = "cmbPesquisaAluno";
            cmbPesquisaAluno.Size = new Size(208, 25);
            cmbPesquisaAluno.TabIndex = 0;
            cmbPesquisaAluno.TextChanged += AplicarFiltros;
            // 
            // btnExportarPDF
            // 
            btnExportarPDF.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarPDF.Location = new Point(30, 105);
            btnExportarPDF.Name = "btnExportarPDF";
            btnExportarPDF.Size = new Size(75, 27);
            btnExportarPDF.TabIndex = 11;
            btnExportarPDF.Text = "Exportar";
            btnExportarPDF.UseVisualStyleBackColor = true;
            btnExportarPDF.Click += btnExportarPDF_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExcluir.Location = new Point(30, 171);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(75, 27);
            btnExcluir.TabIndex = 10;
            btnExcluir.Text = "Excluir";
            btnExcluir.UseVisualStyleBackColor = true;
            // 
            // btnPrepArquivo
            // 
            btnPrepArquivo.Location = new Point(8, 39);
            btnPrepArquivo.Name = "btnPrepArquivo";
            btnPrepArquivo.Size = new Size(118, 27);
            btnPrepArquivo.TabIndex = 9;
            btnPrepArquivo.Text = "Preparar arquivo";
            btnPrepArquivo.UseVisualStyleBackColor = true;
            btnPrepArquivo.Click += btnPrepArquivo_Click;
            // 
            // btnConfirmarImportacao
            // 
            btnConfirmarImportacao.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConfirmarImportacao.Location = new Point(30, 138);
            btnConfirmarImportacao.Name = "btnConfirmarImportacao";
            btnConfirmarImportacao.Size = new Size(75, 27);
            btnConfirmarImportacao.TabIndex = 5;
            btnConfirmarImportacao.Text = "Salvar";
            btnConfirmarImportacao.UseVisualStyleBackColor = true;
            btnConfirmarImportacao.Click += btnConfirmarImportacao_Click;
            // 
            // btnFiltros
            // 
            btnFiltros.Location = new Point(12, 6);
            btnFiltros.Name = "btnFiltros";
            btnFiltros.Size = new Size(110, 27);
            btnFiltros.TabIndex = 4;
            btnFiltros.Text = "Ocultar Filtros";
            btnFiltros.UseVisualStyleBackColor = true;
            btnFiltros.Click += btnFiltros_Click;
            // 
            // panelGrid
            // 
            panelGrid.Controls.Add(dgvEntregas);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(130, 91);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(994, 471);
            panelGrid.TabIndex = 4;
            // 
            // panelRodape
            // 
            panelRodape.Controls.Add(lblEmissao);
            panelRodape.Controls.Add(lblTotal);
            panelRodape.Dock = DockStyle.Bottom;
            panelRodape.Location = new Point(130, 562);
            panelRodape.Name = "panelRodape";
            panelRodape.Size = new Size(994, 36);
            panelRodape.TabIndex = 5;
            // 
            // lblEmissao
            // 
            lblEmissao.AutoSize = true;
            lblEmissao.Location = new Point(752, 10);
            lblEmissao.Name = "lblEmissao";
            lblEmissao.Size = new Size(54, 17);
            lblEmissao.TabIndex = 1;
            lblEmissao.Text = "Emissão";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(12, 10);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(37, 17);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Total";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnFiltros);
            panel1.Controls.Add(btnExportarPDF);
            panel1.Controls.Add(btnPrepArquivo);
            panel1.Controls.Add(btnExcluir);
            panel1.Controls.Add(btnImportar);
            panel1.Controls.Add(btnConfirmarImportacao);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 91);
            panel1.Name = "panel1";
            panel1.Size = new Size(130, 507);
            panel1.TabIndex = 6;
            // 
            // FrmEntregaLivros
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1124, 598);
            Controls.Add(panelGrid);
            Controls.Add(panelRodape);
            Controls.Add(panel1);
            Controls.Add(panelCabecalho);
            Font = new Font("Century Gothic", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmEntregaLivros";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Entrega de Livros";
            Load += EntregaLivros_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEntregas).EndInit();
            panelCabecalho.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            grpDiaSemana.ResumeLayout(false);
            grpLivroEntregue.ResumeLayout(false);
            grpLivroEntregue.PerformLayout();
            grpAluno.ResumeLayout(false);
            panelGrid.ResumeLayout(false);
            panelRodape.ResumeLayout(false);
            panelRodape.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvEntregas;
        private Button btnImportar;
        private Panel panelCabecalho;
        private Panel panelGrid;
        private Panel panelRodape;
        private Button btnFiltros;
        private Button btnConfirmarImportacao;
        private GroupBox grpAluno;
        private ComboBox cmbPesquisaAluno;
        private GroupBox grpLivroEntregue;
        private RadioButton rdbTodos;
        private RadioButton rdbSim;
        private RadioButton rdbNao;
        private GroupBox grpDiaSemana;
        private ComboBox cmbDiaSemana;
        private Label lblTotal;
        private Label lblEmissao;
        private Button btnPrepArquivo;
        private Button btnExcluir;
        private Button btnExportarPDF;
        private GroupBox groupBox1;
        private ComboBox cmbHorario;
        private Panel panel1;
        private GroupBox groupBox2;
        private ComboBox cmbMateria;
    }
}