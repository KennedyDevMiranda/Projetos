namespace Central_do_Educador.Forms
{
    partial class FrmConfiguracaoWhatsApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfiguracaoWhatsApp));
            lblModo = new Label();
            cmbModo = new ComboBox();
            lblApiUrl = new Label();
            txtApiUrl = new TextBox();
            lblApiKey = new Label();
            txtApiKey = new TextBox();
            lblInstancia = new Label();
            txtInstancia = new TextBox();
            lblDdi = new Label();
            txtDdi = new TextBox();
            chkAtivo = new CheckBox();
            btnSalvar = new Button();
            btnTestarConexao = new Button();
            lblInfoLink = new Label();
            grpTeste = new GroupBox();
            lblTelTeste = new Label();
            txtTelTeste = new TextBox();
            btnEnviarTeste = new Button();
            grpTeste.SuspendLayout();
            SuspendLayout();
            // 
            // lblModo
            // 
            lblModo.AutoSize = true;
            lblModo.Location = new Point(20, 9);
            lblModo.Name = "lblModo";
            lblModo.Size = new Size(99, 17);
            lblModo.TabIndex = 0;
            lblModo.Text = "Modo de envio";
            // 
            // cmbModo
            // 
            cmbModo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModo.Items.AddRange(new object[] { "API", "LINK" });
            cmbModo.Location = new Point(20, 29);
            cmbModo.Name = "cmbModo";
            cmbModo.Size = new Size(160, 25);
            cmbModo.TabIndex = 1;
            cmbModo.SelectedIndexChanged += cmbModo_SelectedIndexChanged;
            // 
            // lblApiUrl
            // 
            lblApiUrl.AutoSize = true;
            lblApiUrl.Location = new Point(20, 170);
            lblApiUrl.Name = "lblApiUrl";
            lblApiUrl.Size = new Size(128, 17);
            lblApiUrl.TabIndex = 5;
            lblApiUrl.Text = "URL da Evolution API";
            // 
            // txtApiUrl
            // 
            txtApiUrl.Location = new Point(20, 190);
            txtApiUrl.Name = "txtApiUrl";
            txtApiUrl.PlaceholderText = "https://seu-servidor:8080";
            txtApiUrl.Size = new Size(360, 22);
            txtApiUrl.TabIndex = 6;
            // 
            // lblApiKey
            // 
            lblApiKey.AutoSize = true;
            lblApiKey.Location = new Point(400, 170);
            lblApiKey.Name = "lblApiKey";
            lblApiKey.Size = new Size(51, 17);
            lblApiKey.TabIndex = 7;
            lblApiKey.Text = "API Key";
            // 
            // txtApiKey
            // 
            txtApiKey.Location = new Point(400, 190);
            txtApiKey.Name = "txtApiKey";
            txtApiKey.Size = new Size(190, 22);
            txtApiKey.TabIndex = 8;
            txtApiKey.UseSystemPasswordChar = true;
            // 
            // lblInstancia
            // 
            lblInstancia.AutoSize = true;
            lblInstancia.Location = new Point(20, 226);
            lblInstancia.Name = "lblInstancia";
            lblInstancia.Size = new Size(120, 17);
            lblInstancia.TabIndex = 9;
            lblInstancia.Text = "Nome da Instância";
            // 
            // txtInstancia
            // 
            txtInstancia.Location = new Point(20, 246);
            txtInstancia.Name = "txtInstancia";
            txtInstancia.PlaceholderText = "central-educador";
            txtInstancia.Size = new Size(220, 22);
            txtInstancia.TabIndex = 10;
            // 
            // lblDdi
            // 
            lblDdi.AutoSize = true;
            lblDdi.Location = new Point(200, 9);
            lblDdi.Name = "lblDdi";
            lblDdi.Size = new Size(76, 17);
            lblDdi.TabIndex = 2;
            lblDdi.Text = "DDI padrão";
            // 
            // txtDdi
            // 
            txtDdi.Location = new Point(200, 29);
            txtDdi.Name = "txtDdi";
            txtDdi.Size = new Size(60, 22);
            txtDdi.TabIndex = 3;
            txtDdi.Text = "55";
            // 
            // chkAtivo
            // 
            chkAtivo.AutoSize = true;
            chkAtivo.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            chkAtivo.Location = new Point(290, 31);
            chkAtivo.Name = "chkAtivo";
            chkAtivo.Size = new Size(153, 21);
            chkAtivo.TabIndex = 4;
            chkAtivo.Text = "✅ WhatsApp ativo";
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.FromArgb(0, 120, 215);
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.Location = new Point(430, 241);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(160, 32);
            btnSalvar.TabIndex = 13;
            btnSalvar.Text = "💾 Salvar";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnTestarConexao
            // 
            btnTestarConexao.BackColor = Color.FromArgb(108, 117, 125);
            btnTestarConexao.FlatStyle = FlatStyle.Flat;
            btnTestarConexao.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnTestarConexao.ForeColor = Color.White;
            btnTestarConexao.Location = new Point(254, 242);
            btnTestarConexao.Name = "btnTestarConexao";
            btnTestarConexao.Size = new Size(170, 30);
            btnTestarConexao.TabIndex = 11;
            btnTestarConexao.Text = "🔌 Testar Conexão";
            btnTestarConexao.UseVisualStyleBackColor = false;
            btnTestarConexao.Click += btnTestarConexao_Click;
            // 
            // lblInfoLink
            // 
            lblInfoLink.Font = new Font("Century Gothic", 9F);
            lblInfoLink.ForeColor = Color.FromArgb(80, 80, 80);
            lblInfoLink.Location = new Point(20, 57);
            lblInfoLink.Name = "lblInfoLink";
            lblInfoLink.Size = new Size(560, 92);
            lblInfoLink.TabIndex = 12;
            lblInfoLink.Text = resources.GetString("lblInfoLink.Text");
            // 
            // grpTeste
            // 
            grpTeste.Controls.Add(lblTelTeste);
            grpTeste.Controls.Add(txtTelTeste);
            grpTeste.Controls.Add(btnEnviarTeste);
            grpTeste.Font = new Font("Century Gothic", 9F);
            grpTeste.Location = new Point(20, 279);
            grpTeste.Name = "grpTeste";
            grpTeste.Size = new Size(570, 80);
            grpTeste.TabIndex = 14;
            grpTeste.TabStop = false;
            grpTeste.Text = "Teste de Envio";
            // 
            // lblTelTeste
            // 
            lblTelTeste.AutoSize = true;
            lblTelTeste.Location = new Point(12, 30);
            lblTelTeste.Name = "lblTelTeste";
            lblTelTeste.Size = new Size(58, 17);
            lblTelTeste.TabIndex = 0;
            lblTelTeste.Text = "Número:";
            // 
            // txtTelTeste
            // 
            txtTelTeste.Location = new Point(80, 27);
            txtTelTeste.Name = "txtTelTeste";
            txtTelTeste.PlaceholderText = "(11) 99999-8888";
            txtTelTeste.Size = new Size(200, 22);
            txtTelTeste.TabIndex = 1;
            // 
            // btnEnviarTeste
            // 
            btnEnviarTeste.BackColor = Color.FromArgb(37, 211, 102);
            btnEnviarTeste.FlatStyle = FlatStyle.Flat;
            btnEnviarTeste.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            btnEnviarTeste.ForeColor = Color.White;
            btnEnviarTeste.Location = new Point(300, 24);
            btnEnviarTeste.Name = "btnEnviarTeste";
            btnEnviarTeste.Size = new Size(250, 30);
            btnEnviarTeste.TabIndex = 2;
            btnEnviarTeste.Text = "📱 Enviar Teste";
            btnEnviarTeste.UseVisualStyleBackColor = false;
            btnEnviarTeste.Click += btnEnviarTeste_Click;
            // 
            // FrmConfiguracaoWhatsApp
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(610, 378);
            Controls.Add(lblModo);
            Controls.Add(cmbModo);
            Controls.Add(lblDdi);
            Controls.Add(lblInfoLink);
            Controls.Add(txtDdi);
            Controls.Add(chkAtivo);
            Controls.Add(lblApiUrl);
            Controls.Add(txtApiUrl);
            Controls.Add(lblApiKey);
            Controls.Add(txtApiKey);
            Controls.Add(lblInstancia);
            Controls.Add(txtInstancia);
            Controls.Add(btnTestarConexao);
            Controls.Add(btnSalvar);
            Controls.Add(grpTeste);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfiguracaoWhatsApp";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Configuração do WhatsApp";
            Load += FrmConfiguracaoWhatsApp_Load;
            grpTeste.ResumeLayout(false);
            grpTeste.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblModo;
        private ComboBox cmbModo;
        private Label lblApiUrl;
        private TextBox txtApiUrl;
        private Label lblApiKey;
        private TextBox txtApiKey;
        private Label lblInstancia;
        private TextBox txtInstancia;
        private Label lblDdi;
        private TextBox txtDdi;
        private CheckBox chkAtivo;
        private Button btnSalvar;
        private Button btnTestarConexao;
        private Label lblInfoLink;
        private GroupBox grpTeste;
        private Label lblTelTeste;
        private TextBox txtTelTeste;
        private Button btnEnviarTeste;
    }
}