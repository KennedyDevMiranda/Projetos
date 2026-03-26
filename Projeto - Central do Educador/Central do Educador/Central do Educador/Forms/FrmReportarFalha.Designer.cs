namespace Central_do_Educador.Forms
{
    partial class FrmReportarFalha
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
            lblTitulo = new Label();
            txtTitulo = new TextBox();
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            lblDescricao = new Label();
            txtDescricao = new TextBox();
            btnEnviar = new Button();
            lblMeusRelatos = new Label();
            dgvMeusRelatos = new DataGridView();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMeusRelatos).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(lblTitulo);
            panelTop.Controls.Add(txtTitulo);
            panelTop.Controls.Add(lblCategoria);
            panelTop.Controls.Add(cmbCategoria);
            panelTop.Controls.Add(lblDescricao);
            panelTop.Controls.Add(txtDescricao);
            panelTop.Controls.Add(btnEnviar);
            panelTop.Controls.Add(lblMeusRelatos);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(684, 260);
            panelTop.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(14, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(42, 17);
            lblTitulo.Text = "Título";
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(14, 32);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(400, 23);
            txtTitulo.TabIndex = 1;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(430, 12);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(67, 17);
            lblCategoria.Text = "Categoria";
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(430, 32);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(150, 25);
            cmbCategoria.TabIndex = 2;
            // 
            // lblDescricao
            // 
            lblDescricao.AutoSize = true;
            lblDescricao.Location = new Point(14, 65);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(72, 17);
            lblDescricao.Text = "Descrição";
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(14, 85);
            txtDescricao.Multiline = true;
            txtDescricao.ScrollBars = ScrollBars.Vertical;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(566, 100);
            txtDescricao.TabIndex = 3;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(14, 195);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(140, 35);
            btnEnviar.TabIndex = 4;
            btnEnviar.Text = "📨 Enviar Relato";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // lblMeusRelatos
            // 
            lblMeusRelatos.AutoSize = true;
            lblMeusRelatos.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            lblMeusRelatos.Location = new Point(14, 240);
            lblMeusRelatos.Name = "lblMeusRelatos";
            lblMeusRelatos.Size = new Size(120, 17);
            lblMeusRelatos.Text = "Meus Relatos";
            // 
            // dgvMeusRelatos
            // 
            dgvMeusRelatos.BackgroundColor = SystemColors.Control;
            dgvMeusRelatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMeusRelatos.Dock = DockStyle.Fill;
            dgvMeusRelatos.Location = new Point(0, 260);
            dgvMeusRelatos.Name = "dgvMeusRelatos";
            dgvMeusRelatos.Size = new Size(684, 240);
            dgvMeusRelatos.TabIndex = 1;
            // 
            // FrmReportarFalha
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 500);
            Controls.Add(dgvMeusRelatos);
            Controls.Add(panelTop);
            Font = new Font("Century Gothic", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmReportarFalha";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportar Falha do Sistema";
            Load += FrmReportarFalha_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMeusRelatos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblTitulo;
        private TextBox txtTitulo;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblDescricao;
        private TextBox txtDescricao;
        private Button btnEnviar;
        private Label lblMeusRelatos;
        private DataGridView dgvMeusRelatos;
    }
}