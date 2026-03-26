namespace Central_do_Educador.Forms
{
    partial class FrmTemplates
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
            chkAtivo = new CheckBox();
            btnExcluir = new Button();
            btnSalvar = new Button();
            btnNovo = new Button();
            lblCorpo = new Label();
            txtCorpo = new TextBox();
            lblAssunto = new Label();
            txtAssunto = new TextBox();
            lblCanal = new Label();
            cmbCanal = new ComboBox();
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            lblPlaceholders = new Label();
            panelPreview = new Panel();
            lblPreviewTitle = new Label();
            txtPreview = new TextBox();
            panelGrid = new Panel();
            dgvTemplates = new DataGridView();
            panelBottom = new Panel();
            lblTotal = new Label();
            panelTop.SuspendLayout();
            panelPreview.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTemplates).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(chkAtivo);
            panelTop.Controls.Add(btnExcluir);
            panelTop.Controls.Add(btnSalvar);
            panelTop.Controls.Add(btnNovo);
            panelTop.Controls.Add(lblCorpo);
            panelTop.Controls.Add(txtCorpo);
            panelTop.Controls.Add(lblAssunto);
            panelTop.Controls.Add(txtAssunto);
            panelTop.Controls.Add(lblCanal);
            panelTop.Controls.Add(cmbCanal);
            panelTop.Controls.Add(lblCodigo);
            panelTop.Controls.Add(txtCodigo);
            panelTop.Controls.Add(lblPlaceholders);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(950, 210);
            panelTop.TabIndex = 0;
            // 
            // chkAtivo
            // 
            chkAtivo.AutoSize = true;
            chkAtivo.Checked = true;
            chkAtivo.CheckState = CheckState.Checked;
            chkAtivo.Location = new Point(430, 32);
            chkAtivo.Name = "chkAtivo";
            chkAtivo.Size = new Size(61, 21);
            chkAtivo.TabIndex = 0;
            chkAtivo.Text = "Ativo";
            // 
            // btnExcluir
            // 
            btnExcluir.BackColor = Color.FromArgb(220, 53, 69);
            btnExcluir.FlatStyle = FlatStyle.Flat;
            btnExcluir.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            btnExcluir.ForeColor = Color.White;
            btnExcluir.Location = new Point(829, 172);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(100, 32);
            btnExcluir.TabIndex = 1;
            btnExcluir.Text = "🗑 Excluir";
            btnExcluir.UseVisualStyleBackColor = false;
            btnExcluir.Click += btnExcluir_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.FromArgb(0, 120, 215);
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.Location = new Point(719, 172);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(100, 32);
            btnSalvar.TabIndex = 2;
            btnSalvar.Text = "💾 Salvar";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnNovo
            // 
            btnNovo.BackColor = Color.FromArgb(108, 117, 125);
            btnNovo.FlatStyle = FlatStyle.Flat;
            btnNovo.Font = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            btnNovo.ForeColor = Color.White;
            btnNovo.Location = new Point(609, 172);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(100, 32);
            btnNovo.TabIndex = 3;
            btnNovo.Text = "📄 Novo";
            btnNovo.UseVisualStyleBackColor = false;
            btnNovo.Click += btnNovo_Click;
            // 
            // lblCorpo
            // 
            lblCorpo.AutoSize = true;
            lblCorpo.Location = new Point(15, 62);
            lblCorpo.Name = "lblCorpo";
            lblCorpo.Size = new Size(149, 17);
            lblCorpo.TabIndex = 4;
            lblCorpo.Text = "Corpo da mensagem";
            // 
            // txtCorpo
            // 
            txtCorpo.AcceptsReturn = true;
            txtCorpo.Location = new Point(15, 82);
            txtCorpo.Multiline = true;
            txtCorpo.Name = "txtCorpo";
            txtCorpo.PlaceholderText = "Olá {aluno}, seu agendamento de {tipo} foi confirmado...";
            txtCorpo.ScrollBars = ScrollBars.Vertical;
            txtCorpo.Size = new Size(580, 110);
            txtCorpo.TabIndex = 5;
            txtCorpo.TextChanged += txtCorpo_TextChanged;
            // 
            // lblAssunto
            // 
            lblAssunto.AutoSize = true;
            lblAssunto.Location = new Point(520, 10);
            lblAssunto.Name = "lblAssunto";
            lblAssunto.Size = new Size(110, 17);
            lblAssunto.TabIndex = 6;
            lblAssunto.Text = "Assunto (e-mail)";
            // 
            // txtAssunto
            // 
            txtAssunto.Location = new Point(520, 30);
            txtAssunto.Name = "txtAssunto";
            txtAssunto.PlaceholderText = "ex.: Confirmação de Agendamento - {tipo}";
            txtAssunto.Size = new Size(410, 23);
            txtAssunto.TabIndex = 7;
            // 
            // lblCanal
            // 
            lblCanal.AutoSize = true;
            lblCanal.Location = new Point(280, 10);
            lblCanal.Name = "lblCanal";
            lblCanal.Size = new Size(48, 17);
            lblCanal.TabIndex = 8;
            lblCanal.Text = "Canal";
            // 
            // cmbCanal
            // 
            cmbCanal.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCanal.Items.AddRange(new object[] { "EMAIL", "WHATSAPP" });
            cmbCanal.Location = new Point(280, 30);
            cmbCanal.Name = "cmbCanal";
            cmbCanal.Size = new Size(130, 25);
            cmbCanal.TabIndex = 9;
            cmbCanal.SelectedIndexChanged += cmbCanal_SelectedIndexChanged;
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(15, 10);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(58, 17);
            lblCodigo.TabIndex = 10;
            lblCodigo.Text = "Código";
            // 
            // txtCodigo
            // 
            txtCodigo.CharacterCasing = CharacterCasing.Upper;
            txtCodigo.Location = new Point(15, 30);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.PlaceholderText = "ex.: CONFIRMACAO_AGENDAMENTO";
            txtCodigo.Size = new Size(250, 23);
            txtCodigo.TabIndex = 11;
            // 
            // lblPlaceholders
            // 
            lblPlaceholders.Font = new Font("Century Gothic", 8.5F);
            lblPlaceholders.ForeColor = Color.FromArgb(100, 100, 100);
            lblPlaceholders.Location = new Point(610, 62);
            lblPlaceholders.Name = "lblPlaceholders";
            lblPlaceholders.Size = new Size(320, 100);
            lblPlaceholders.TabIndex = 12;
            lblPlaceholders.Text = "📌 Placeholders disponíveis:\n{aluno}  •  {tipo}  •  {data_hora}\n{local}  •  {observacao}  •  {status}\n\nWhatsApp: use *negrito* e _itálico_";
            // 
            // panelPreview
            // 
            panelPreview.Controls.Add(lblPreviewTitle);
            panelPreview.Controls.Add(txtPreview);
            panelPreview.Dock = DockStyle.Right;
            panelPreview.Location = new Point(630, 210);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(320, 300);
            panelPreview.TabIndex = 3;
            // 
            // lblPreviewTitle
            // 
            lblPreviewTitle.AutoSize = true;
            lblPreviewTitle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblPreviewTitle.Location = new Point(8, 6);
            lblPreviewTitle.Name = "lblPreviewTitle";
            lblPreviewTitle.Size = new Size(171, 16);
            lblPreviewTitle.TabIndex = 0;
            lblPreviewTitle.Text = "👁 Preview (dados fictícios)";
            // 
            // txtPreview
            // 
            txtPreview.BackColor = Color.FromArgb(250, 250, 250);
            txtPreview.Font = new Font("Consolas", 9F);
            txtPreview.Location = new Point(8, 28);
            txtPreview.Multiline = true;
            txtPreview.Name = "txtPreview";
            txtPreview.ReadOnly = true;
            txtPreview.ScrollBars = ScrollBars.Vertical;
            txtPreview.Size = new Size(300, 260);
            txtPreview.TabIndex = 1;
            // 
            // panelGrid
            // 
            panelGrid.Controls.Add(dgvTemplates);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(0, 210);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(630, 300);
            panelGrid.TabIndex = 2;
            // 
            // dgvTemplates
            // 
            dgvTemplates.BackgroundColor = SystemColors.Control;
            dgvTemplates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTemplates.Dock = DockStyle.Fill;
            dgvTemplates.Location = new Point(0, 0);
            dgvTemplates.Name = "dgvTemplates";
            dgvTemplates.Size = new Size(630, 300);
            dgvTemplates.TabIndex = 0;
            dgvTemplates.CellDoubleClick += dgvTemplates_CellDoubleClick;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(lblTotal);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 510);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(950, 28);
            panelBottom.TabIndex = 4;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(12, 6);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(39, 17);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Total";
            // 
            // FrmTemplates
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 538);
            Controls.Add(panelGrid);
            Controls.Add(panelPreview);
            Controls.Add(panelTop);
            Controls.Add(panelBottom);
            Font = new Font("Century Gothic", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTemplates";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Templates de Mensagem";
            Load += FrmTemplates_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelPreview.ResumeLayout(false);
            panelPreview.PerformLayout();
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTemplates).EndInit();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblCodigo;
        private TextBox txtCodigo;
        private Label lblCanal;
        private ComboBox cmbCanal;
        private CheckBox chkAtivo;
        private Label lblAssunto;
        private TextBox txtAssunto;
        private Label lblCorpo;
        private TextBox txtCorpo;
        private Label lblPlaceholders;
        private Button btnNovo;
        private Button btnSalvar;
        private Button btnExcluir;
        private Panel panelPreview;
        private Label lblPreviewTitle;
        private TextBox txtPreview;
        private Panel panelGrid;
        private DataGridView dgvTemplates;
        private Panel panelBottom;
        private Label lblTotal;
    }
}