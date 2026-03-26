namespace Central_do_Educador.Forms
{
    partial class FrmGerenciarFalhas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblFiltro = new Label();
            cmbFiltroStatus = new ComboBox();
            lblTotal = new Label();
            panelBottom = new Panel();
            lblStatus = new Label();
            cmbNovoStatus = new ComboBox();
            lblResposta = new Label();
            txtRespostaAdm = new TextBox();
            btnSalvarResposta = new Button();
            btnExcluirRelato = new Button();
            dgvFalhas = new DataGridView();
            panelTop.SuspendLayout();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFalhas).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(lblFiltro);
            panelTop.Controls.Add(cmbFiltroStatus);
            panelTop.Controls.Add(lblTotal);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(850, 45);
            panelTop.TabIndex = 0;
            // 
            // lblFiltro
            // 
            lblFiltro.AutoSize = true;
            lblFiltro.Location = new Point(14, 14);
            lblFiltro.Name = "lblFiltro";
            lblFiltro.Size = new Size(48, 17);
            lblFiltro.Text = "Status:";
            // 
            // cmbFiltroStatus
            // 
            cmbFiltroStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroStatus.Location = new Point(68, 10);
            cmbFiltroStatus.Name = "cmbFiltroStatus";
            cmbFiltroStatus.Size = new Size(160, 25);
            cmbFiltroStatus.TabIndex = 1;
            cmbFiltroStatus.SelectedIndexChanged += cmbFiltroStatus_SelectedIndexChanged;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(250, 14);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(50, 17);
            lblTotal.Text = "Total: 0";
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(lblStatus);
            panelBottom.Controls.Add(cmbNovoStatus);
            panelBottom.Controls.Add(lblResposta);
            panelBottom.Controls.Add(txtRespostaAdm);
            panelBottom.Controls.Add(btnSalvarResposta);
            panelBottom.Controls.Add(btnExcluirRelato);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 370);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(850, 130);
            panelBottom.TabIndex = 2;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(14, 10);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(90, 17);
            lblStatus.Text = "Novo Status:";
            // 
            // cmbNovoStatus
            // 
            cmbNovoStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNovoStatus.Items.AddRange(new object[] { "ABERTO", "EM ANDAMENTO", "RESOLVIDO", "REJEITADO" });
            cmbNovoStatus.Location = new Point(110, 6);
            cmbNovoStatus.Name = "cmbNovoStatus";
            cmbNovoStatus.Size = new Size(160, 25);
            cmbNovoStatus.TabIndex = 1;
            // 
            // lblResposta
            // 
            lblResposta.AutoSize = true;
            lblResposta.Location = new Point(14, 40);
            lblResposta.Name = "lblResposta";
            lblResposta.Size = new Size(120, 17);
            lblResposta.Text = "Resposta do ADM:";
            // 
            // txtRespostaAdm
            // 
            txtRespostaAdm.Location = new Point(14, 60);
            txtRespostaAdm.Multiline = true;
            txtRespostaAdm.Name = "txtRespostaAdm";
            txtRespostaAdm.Size = new Size(600, 55);
            txtRespostaAdm.TabIndex = 2;
            // 
            // btnSalvarResposta
            // 
            btnSalvarResposta.Location = new Point(630, 60);
            btnSalvarResposta.Name = "btnSalvarResposta";
            btnSalvarResposta.Size = new Size(100, 35);
            btnSalvarResposta.TabIndex = 3;
            btnSalvarResposta.Text = "💾 Salvar";
            btnSalvarResposta.UseVisualStyleBackColor = true;
            btnSalvarResposta.Click += btnSalvarResposta_Click;
            // 
            // btnExcluirRelato
            // 
            btnExcluirRelato.Location = new Point(740, 60);
            btnExcluirRelato.Name = "btnExcluirRelato";
            btnExcluirRelato.Size = new Size(100, 35);
            btnExcluirRelato.TabIndex = 4;
            btnExcluirRelato.Text = "🗑 Excluir";
            btnExcluirRelato.UseVisualStyleBackColor = true;
            btnExcluirRelato.Click += btnExcluirRelato_Click;
            // 
            // dgvFalhas
            // 
            dgvFalhas.BackgroundColor = SystemColors.Control;
            dgvFalhas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFalhas.Dock = DockStyle.Fill;
            dgvFalhas.Location = new Point(0, 45);
            dgvFalhas.Name = "dgvFalhas";
            dgvFalhas.Size = new Size(850, 325);
            dgvFalhas.TabIndex = 1;
            dgvFalhas.CellClick += dgvFalhas_CellClick;
            // 
            // FrmGerenciarFalhas
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 500);
            Controls.Add(dgvFalhas);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Font = new Font("Century Gothic", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmGerenciarFalhas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gerenciar Falhas Reportadas (ADM)";
            Load += FrmGerenciarFalhas_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFalhas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblFiltro;
        private ComboBox cmbFiltroStatus;
        private Label lblTotal;
        private Panel panelBottom;
        private Label lblStatus;
        private ComboBox cmbNovoStatus;
        private Label lblResposta;
        private TextBox txtRespostaAdm;
        private Button btnSalvarResposta;
        private Button btnExcluirRelato;
        private DataGridView dgvFalhas;
    }
}