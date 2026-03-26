namespace Central_do_Educador.Forms
{
    partial class FrmRegistro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistro));
            panel1 = new Panel();
            grpAtivo = new GroupBox();
            cmbFiltroAtivo = new ComboBox();
            grpNumeroResp = new GroupBox();
            txtFiltroNumResp = new TextBox();
            grpResponsavel = new GroupBox();
            txtFiltroResponsavel = new TextBox();
            grpNumeroAluno = new GroupBox();
            txtFiltroNumAluno = new TextBox();
            grpEmail = new GroupBox();
            txtFiltroEmail = new TextBox();
            grpNome = new GroupBox();
            txtFiltroNome = new TextBox();
            btnPrepAquivos = new Button();
            btnImportarDados = new Button();
            panel2 = new Panel();
            dgvRegistro = new DataGridView();
            panel4 = new Panel();
            panel3 = new Panel();
            lblTotal = new Label();
            panel1.SuspendLayout();
            grpAtivo.SuspendLayout();
            grpNumeroResp.SuspendLayout();
            grpResponsavel.SuspendLayout();
            grpNumeroAluno.SuspendLayout();
            grpEmail.SuspendLayout();
            grpNome.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRegistro).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(grpAtivo);
            panel1.Controls.Add(grpNumeroResp);
            panel1.Controls.Add(grpResponsavel);
            panel1.Controls.Add(grpNumeroAluno);
            panel1.Controls.Add(grpEmail);
            panel1.Controls.Add(grpNome);
            panel1.Controls.Add(btnPrepAquivos);
            panel1.Controls.Add(btnImportarDados);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(994, 109);
            panel1.TabIndex = 0;
            // 
            // grpAtivo
            // 
            grpAtivo.Controls.Add(cmbFiltroAtivo);
            grpAtivo.Location = new Point(384, 56);
            grpAtivo.Name = "grpAtivo";
            grpAtivo.Size = new Size(100, 46);
            grpAtivo.TabIndex = 15;
            grpAtivo.TabStop = false;
            grpAtivo.Text = "Ativo";
            // 
            // cmbFiltroAtivo
            // 
            cmbFiltroAtivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroAtivo.FormattingEnabled = true;
            cmbFiltroAtivo.Items.AddRange(new object[] { "Todos", "Sim", "Não" });
            cmbFiltroAtivo.Location = new Point(6, 18);
            cmbFiltroAtivo.Name = "cmbFiltroAtivo";
            cmbFiltroAtivo.Size = new Size(88, 23);
            cmbFiltroAtivo.TabIndex = 0;
            cmbFiltroAtivo.SelectedIndexChanged += AplicarFiltros;
            // 
            // grpNumeroResp
            // 
            grpNumeroResp.Controls.Add(txtFiltroNumResp);
            grpNumeroResp.Location = new Point(218, 56);
            grpNumeroResp.Name = "grpNumeroResp";
            grpNumeroResp.Size = new Size(160, 46);
            grpNumeroResp.TabIndex = 14;
            grpNumeroResp.TabStop = false;
            grpNumeroResp.Text = "Nº Responsável";
            // 
            // txtFiltroNumResp
            // 
            txtFiltroNumResp.Location = new Point(6, 18);
            txtFiltroNumResp.Name = "txtFiltroNumResp";
            txtFiltroNumResp.Size = new Size(148, 23);
            txtFiltroNumResp.TabIndex = 0;
            txtFiltroNumResp.TextChanged += AplicarFiltros;
            // 
            // grpResponsavel
            // 
            grpResponsavel.Controls.Add(txtFiltroResponsavel);
            grpResponsavel.Location = new Point(12, 56);
            grpResponsavel.Name = "grpResponsavel";
            grpResponsavel.Size = new Size(200, 46);
            grpResponsavel.TabIndex = 13;
            grpResponsavel.TabStop = false;
            grpResponsavel.Text = "Responsável";
            // 
            // txtFiltroResponsavel
            // 
            txtFiltroResponsavel.Location = new Point(6, 18);
            txtFiltroResponsavel.Name = "txtFiltroResponsavel";
            txtFiltroResponsavel.Size = new Size(188, 23);
            txtFiltroResponsavel.TabIndex = 0;
            txtFiltroResponsavel.TextChanged += AplicarFiltros;
            // 
            // grpNumeroAluno
            // 
            grpNumeroAluno.Controls.Add(txtFiltroNumAluno);
            grpNumeroAluno.Location = new Point(424, 6);
            grpNumeroAluno.Name = "grpNumeroAluno";
            grpNumeroAluno.Size = new Size(140, 46);
            grpNumeroAluno.TabIndex = 12;
            grpNumeroAluno.TabStop = false;
            grpNumeroAluno.Text = "Nº Aluno";
            // 
            // txtFiltroNumAluno
            // 
            txtFiltroNumAluno.Location = new Point(6, 18);
            txtFiltroNumAluno.Name = "txtFiltroNumAluno";
            txtFiltroNumAluno.Size = new Size(128, 23);
            txtFiltroNumAluno.TabIndex = 0;
            txtFiltroNumAluno.TextChanged += AplicarFiltros;
            // 
            // grpEmail
            // 
            grpEmail.Controls.Add(txtFiltroEmail);
            grpEmail.Location = new Point(218, 6);
            grpEmail.Name = "grpEmail";
            grpEmail.Size = new Size(200, 46);
            grpEmail.TabIndex = 11;
            grpEmail.TabStop = false;
            grpEmail.Text = "E-mail";
            // 
            // txtFiltroEmail
            // 
            txtFiltroEmail.Location = new Point(6, 18);
            txtFiltroEmail.Name = "txtFiltroEmail";
            txtFiltroEmail.Size = new Size(188, 23);
            txtFiltroEmail.TabIndex = 0;
            txtFiltroEmail.TextChanged += AplicarFiltros;
            // 
            // grpNome
            // 
            grpNome.Controls.Add(txtFiltroNome);
            grpNome.Location = new Point(12, 6);
            grpNome.Name = "grpNome";
            grpNome.Size = new Size(200, 46);
            grpNome.TabIndex = 10;
            grpNome.TabStop = false;
            grpNome.Text = "Aluno";
            // 
            // txtFiltroNome
            // 
            txtFiltroNome.Location = new Point(6, 18);
            txtFiltroNome.Name = "txtFiltroNome";
            txtFiltroNome.Size = new Size(188, 23);
            txtFiltroNome.TabIndex = 0;
            txtFiltroNome.TextChanged += AplicarFiltros;
            // 
            // btnPrepAquivos
            // 
            btnPrepAquivos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPrepAquivos.Location = new Point(885, 56);
            btnPrepAquivos.Name = "btnPrepAquivos";
            btnPrepAquivos.Size = new Size(97, 23);
            btnPrepAquivos.TabIndex = 3;
            btnPrepAquivos.Text = "Prep. Arquivo";
            btnPrepAquivos.UseVisualStyleBackColor = true;
            btnPrepAquivos.Click += btnPrepAquivos_Click;
            // 
            // btnImportarDados
            // 
            btnImportarDados.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnImportarDados.Location = new Point(896, 27);
            btnImportarDados.Name = "btnImportarDados";
            btnImportarDados.Size = new Size(75, 23);
            btnImportarDados.TabIndex = 1;
            btnImportarDados.Text = "Importar";
            btnImportarDados.UseVisualStyleBackColor = true;
            btnImportarDados.Click += btnImportarDados_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvRegistro);
            panel2.Controls.Add(panel4);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 109);
            panel2.Name = "panel2";
            panel2.Size = new Size(994, 456);
            panel2.TabIndex = 1;
            // 
            // dgvRegistro
            // 
            dgvRegistro.BackgroundColor = SystemColors.Control;
            dgvRegistro.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRegistro.Dock = DockStyle.Fill;
            dgvRegistro.Location = new Point(174, 0);
            dgvRegistro.Name = "dgvRegistro";
            dgvRegistro.Size = new Size(820, 456);
            dgvRegistro.TabIndex = 0;
            dgvRegistro.CellDoubleClick += dgvRegistro_CellDoubleClick;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(174, 456);
            panel4.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblTotal);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 565);
            panel3.Name = "panel3";
            panel3.Size = new Size(994, 29);
            panel3.TabIndex = 2;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(12, 7);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(33, 15);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Total";
            // 
            // FrmRegistro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(994, 594);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRegistro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registros";
            Load += FrmRegistro_Load;
            panel1.ResumeLayout(false);
            grpAtivo.ResumeLayout(false);
            grpNumeroResp.ResumeLayout(false);
            grpNumeroResp.PerformLayout();
            grpResponsavel.ResumeLayout(false);
            grpResponsavel.PerformLayout();
            grpNumeroAluno.ResumeLayout(false);
            grpNumeroAluno.PerformLayout();
            grpEmail.ResumeLayout(false);
            grpEmail.PerformLayout();
            grpNome.ResumeLayout(false);
            grpNome.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRegistro).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private DataGridView dgvRegistro;
        private Panel panel4;
        private Button btnImportarDados;
        private Button btnPrepAquivos;
        private GroupBox grpNome;
        private TextBox txtFiltroNome;
        private GroupBox grpEmail;
        private TextBox txtFiltroEmail;
        private GroupBox grpNumeroAluno;
        private TextBox txtFiltroNumAluno;
        private GroupBox grpResponsavel;
        private TextBox txtFiltroResponsavel;
        private GroupBox grpNumeroResp;
        private TextBox txtFiltroNumResp;
        private GroupBox grpAtivo;
        private ComboBox cmbFiltroAtivo;
        private Label lblTotal;
    }
}