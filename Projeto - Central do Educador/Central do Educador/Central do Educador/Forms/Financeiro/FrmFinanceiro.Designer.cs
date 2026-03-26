namespace Central_do_Educador.Forms
{
    partial class FrmFinanceiro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFinanceiro));
            pnlDashboard = new Panel();
            tlpCards = new TableLayoutPanel();
            pnlCard1 = new Panel();
            lblCard1Value = new Label();
            lblCard1Title = new Label();
            pnlCard2 = new Panel();
            lblCard2Value = new Label();
            lblCard2Title = new Label();
            pnlCard3 = new Panel();
            lblCard3Value = new Label();
            lblCard3Title = new Label();
            pnlCard4 = new Panel();
            lblCard4Value = new Label();
            lblCard4Title = new Label();
            pnlCard5 = new Panel();
            lblCard5Value = new Label();
            lblCard5Title = new Label();
            tabControl = new TabControl();
            tabContratos = new TabPage();
            dgvContratos = new DataGridView();
            pnlBotoesContratos = new Panel();
            btnNovoContrato = new Button();
            btnExcluirContrato = new Button();
            btnVerParcelas = new Button();
            btnAtualizarContratos = new Button();
            pnlFiltroContratos = new Panel();
            txtPesquisaContrato = new TextBox();
            lblFiltroAlunoC = new Label();
            cmbStatusContrato = new ComboBox();
            lblFiltroStatusC = new Label();
            tabParcelas = new TabPage();
            dgvParcelas = new DataGridView();
            pnlBotoesParcelas = new Panel();
            btnDisparoAgendado = new Button();
            btnRegistrarPgto = new Button();
            btnConfirmacaoPgto = new Button();
            btnAtualizarParcelas = new Button();
            btnProcessarNotif = new Button();
            btnEstornar = new Button();
            btnEnviarFiltrados = new Button();
            pnlFiltroParcelas = new Panel();
            txtPesquisaParcela = new TextBox();
            lblFiltroAlunoP = new Label();
            cmbStatusParcela = new ComboBox();
            lblFiltroStatusP = new Label();
            tabConfig = new TabPage();
            grpExecutar = new GroupBox();
            txtResultado = new TextBox();
            btnExecutarNotif = new Button();
            grpConfigNotif = new GroupBox();
            btnSalvarConfig = new Button();
            chkFinanceiroAtivo = new CheckBox();
            txtHorarioEnvio = new TextBox();
            lblHorarioEnvio = new Label();
            chkEnviarWhatsapp = new CheckBox();
            chkEnviarEmail = new CheckBox();
            nudDiasCobranca = new NumericUpDown();
            lblDiasCobranca = new Label();
            nudDiasLembrete = new NumericUpDown();
            lblDiasLembrete = new Label();
            pnlRodape = new Panel();
            lblTotalRegistros = new Label();
            lblStatusBar = new Label();
            pnlDashboard.SuspendLayout();
            tlpCards.SuspendLayout();
            pnlCard1.SuspendLayout();
            pnlCard2.SuspendLayout();
            pnlCard3.SuspendLayout();
            pnlCard4.SuspendLayout();
            pnlCard5.SuspendLayout();
            tabControl.SuspendLayout();
            tabContratos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvContratos).BeginInit();
            pnlBotoesContratos.SuspendLayout();
            pnlFiltroContratos.SuspendLayout();
            tabParcelas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvParcelas).BeginInit();
            pnlBotoesParcelas.SuspendLayout();
            pnlFiltroParcelas.SuspendLayout();
            tabConfig.SuspendLayout();
            grpExecutar.SuspendLayout();
            grpConfigNotif.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDiasCobranca).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiasLembrete).BeginInit();
            pnlRodape.SuspendLayout();
            SuspendLayout();
            // 
            // pnlDashboard
            // 
            pnlDashboard.BackColor = Color.FromArgb(245, 245, 245);
            pnlDashboard.Controls.Add(tlpCards);
            pnlDashboard.Dock = DockStyle.Top;
            pnlDashboard.Location = new Point(0, 0);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Padding = new Padding(10);
            pnlDashboard.Size = new Size(1139, 100);
            pnlDashboard.TabIndex = 0;
            // 
            // tlpCards
            // 
            tlpCards.ColumnCount = 5;
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpCards.Controls.Add(pnlCard1, 0, 0);
            tlpCards.Controls.Add(pnlCard2, 1, 0);
            tlpCards.Controls.Add(pnlCard3, 2, 0);
            tlpCards.Controls.Add(pnlCard4, 3, 0);
            tlpCards.Controls.Add(pnlCard5, 4, 0);
            tlpCards.Dock = DockStyle.Fill;
            tlpCards.Location = new Point(10, 10);
            tlpCards.Name = "tlpCards";
            tlpCards.RowCount = 1;
            tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCards.Size = new Size(1119, 80);
            tlpCards.TabIndex = 0;
            // 
            // pnlCard1
            // 
            pnlCard1.BackColor = Color.FromArgb(33, 150, 243);
            pnlCard1.BorderStyle = BorderStyle.FixedSingle;
            pnlCard1.Controls.Add(lblCard1Value);
            pnlCard1.Controls.Add(lblCard1Title);
            pnlCard1.Dock = DockStyle.Fill;
            pnlCard1.Location = new Point(4, 4);
            pnlCard1.Margin = new Padding(4);
            pnlCard1.Name = "pnlCard1";
            pnlCard1.Padding = new Padding(10);
            pnlCard1.Size = new Size(215, 72);
            pnlCard1.TabIndex = 0;
            // 
            // lblCard1Value
            // 
            lblCard1Value.Dock = DockStyle.Fill;
            lblCard1Value.Font = new Font("Century Gothic", 18F, FontStyle.Bold);
            lblCard1Value.ForeColor = Color.White;
            lblCard1Value.Location = new Point(10, 34);
            lblCard1Value.Name = "lblCard1Value";
            lblCard1Value.Size = new Size(193, 26);
            lblCard1Value.TabIndex = 1;
            lblCard1Value.Text = "0";
            lblCard1Value.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCard1Title
            // 
            lblCard1Title.Dock = DockStyle.Top;
            lblCard1Title.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblCard1Title.ForeColor = Color.White;
            lblCard1Title.Location = new Point(10, 10);
            lblCard1Title.Name = "lblCard1Title";
            lblCard1Title.Size = new Size(193, 24);
            lblCard1Title.TabIndex = 0;
            lblCard1Title.Text = "CONTRATOS ATIVOS";
            lblCard1Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlCard2
            // 
            pnlCard2.BackColor = Color.FromArgb(76, 175, 80);
            pnlCard2.BorderStyle = BorderStyle.FixedSingle;
            pnlCard2.Controls.Add(lblCard2Value);
            pnlCard2.Controls.Add(lblCard2Title);
            pnlCard2.Dock = DockStyle.Fill;
            pnlCard2.Location = new Point(227, 4);
            pnlCard2.Margin = new Padding(4);
            pnlCard2.Name = "pnlCard2";
            pnlCard2.Padding = new Padding(10);
            pnlCard2.Size = new Size(215, 72);
            pnlCard2.TabIndex = 1;
            // 
            // lblCard2Value
            // 
            lblCard2Value.Dock = DockStyle.Fill;
            lblCard2Value.Font = new Font("Century Gothic", 18F, FontStyle.Bold);
            lblCard2Value.ForeColor = Color.White;
            lblCard2Value.Location = new Point(10, 34);
            lblCard2Value.Name = "lblCard2Value";
            lblCard2Value.Size = new Size(193, 26);
            lblCard2Value.TabIndex = 1;
            lblCard2Value.Text = "R$ 0,00";
            lblCard2Value.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCard2Title
            // 
            lblCard2Title.Dock = DockStyle.Top;
            lblCard2Title.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblCard2Title.ForeColor = Color.White;
            lblCard2Title.Location = new Point(10, 10);
            lblCard2Title.Name = "lblCard2Title";
            lblCard2Title.Size = new Size(193, 24);
            lblCard2Title.TabIndex = 0;
            lblCard2Title.Text = "TOTAL RECEBIDO";
            lblCard2Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlCard3
            // 
            pnlCard3.BackColor = Color.FromArgb(255, 152, 0);
            pnlCard3.BorderStyle = BorderStyle.FixedSingle;
            pnlCard3.Controls.Add(lblCard3Value);
            pnlCard3.Controls.Add(lblCard3Title);
            pnlCard3.Dock = DockStyle.Fill;
            pnlCard3.Location = new Point(450, 4);
            pnlCard3.Margin = new Padding(4);
            pnlCard3.Name = "pnlCard3";
            pnlCard3.Padding = new Padding(10);
            pnlCard3.Size = new Size(215, 72);
            pnlCard3.TabIndex = 2;
            // 
            // lblCard3Value
            // 
            lblCard3Value.Dock = DockStyle.Fill;
            lblCard3Value.Font = new Font("Century Gothic", 18F, FontStyle.Bold);
            lblCard3Value.ForeColor = Color.White;
            lblCard3Value.Location = new Point(10, 34);
            lblCard3Value.Name = "lblCard3Value";
            lblCard3Value.Size = new Size(193, 26);
            lblCard3Value.TabIndex = 1;
            lblCard3Value.Text = "R$ 0,00";
            lblCard3Value.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCard3Title
            // 
            lblCard3Title.Dock = DockStyle.Top;
            lblCard3Title.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblCard3Title.ForeColor = Color.White;
            lblCard3Title.Location = new Point(10, 10);
            lblCard3Title.Name = "lblCard3Title";
            lblCard3Title.Size = new Size(193, 24);
            lblCard3Title.TabIndex = 0;
            lblCard3Title.Text = "TOTAL PENDENTE";
            lblCard3Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlCard4
            // 
            pnlCard4.BackColor = Color.FromArgb(244, 67, 54);
            pnlCard4.BorderStyle = BorderStyle.FixedSingle;
            pnlCard4.Controls.Add(lblCard4Value);
            pnlCard4.Controls.Add(lblCard4Title);
            pnlCard4.Dock = DockStyle.Fill;
            pnlCard4.Location = new Point(673, 4);
            pnlCard4.Margin = new Padding(4);
            pnlCard4.Name = "pnlCard4";
            pnlCard4.Padding = new Padding(10);
            pnlCard4.Size = new Size(215, 72);
            pnlCard4.TabIndex = 3;
            // 
            // lblCard4Value
            // 
            lblCard4Value.Dock = DockStyle.Fill;
            lblCard4Value.Font = new Font("Century Gothic", 18F, FontStyle.Bold);
            lblCard4Value.ForeColor = Color.White;
            lblCard4Value.Location = new Point(10, 34);
            lblCard4Value.Name = "lblCard4Value";
            lblCard4Value.Size = new Size(193, 26);
            lblCard4Value.TabIndex = 1;
            lblCard4Value.Text = "R$ 0,00";
            lblCard4Value.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCard4Title
            // 
            lblCard4Title.Dock = DockStyle.Top;
            lblCard4Title.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblCard4Title.ForeColor = Color.White;
            lblCard4Title.Location = new Point(10, 10);
            lblCard4Title.Name = "lblCard4Title";
            lblCard4Title.Size = new Size(193, 24);
            lblCard4Title.TabIndex = 0;
            lblCard4Title.Text = "TOTAL VENCIDO";
            lblCard4Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlCard5
            // 
            pnlCard5.BackColor = Color.FromArgb(156, 39, 176);
            pnlCard5.BorderStyle = BorderStyle.FixedSingle;
            pnlCard5.Controls.Add(lblCard5Value);
            pnlCard5.Controls.Add(lblCard5Title);
            pnlCard5.Dock = DockStyle.Fill;
            pnlCard5.Location = new Point(896, 4);
            pnlCard5.Margin = new Padding(4);
            pnlCard5.Name = "pnlCard5";
            pnlCard5.Padding = new Padding(10);
            pnlCard5.Size = new Size(219, 72);
            pnlCard5.TabIndex = 4;
            // 
            // lblCard5Value
            // 
            lblCard5Value.Dock = DockStyle.Fill;
            lblCard5Value.Font = new Font("Century Gothic", 18F, FontStyle.Bold);
            lblCard5Value.ForeColor = Color.White;
            lblCard5Value.Location = new Point(10, 34);
            lblCard5Value.Name = "lblCard5Value";
            lblCard5Value.Size = new Size(197, 26);
            lblCard5Value.TabIndex = 1;
            lblCard5Value.Text = "0";
            lblCard5Value.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCard5Title
            // 
            lblCard5Title.Dock = DockStyle.Top;
            lblCard5Title.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblCard5Title.ForeColor = Color.White;
            lblCard5Title.Location = new Point(10, 10);
            lblCard5Title.Name = "lblCard5Title";
            lblCard5Title.Size = new Size(197, 24);
            lblCard5Title.TabIndex = 0;
            lblCard5Title.Text = "A VENCER (7 DIAS)";
            lblCard5Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabContratos);
            tabControl.Controls.Add(tabParcelas);
            tabControl.Controls.Add(tabConfig);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 100);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1139, 497);
            tabControl.TabIndex = 1;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabContratos
            // 
            tabContratos.Controls.Add(dgvContratos);
            tabContratos.Controls.Add(pnlBotoesContratos);
            tabContratos.Controls.Add(pnlFiltroContratos);
            tabContratos.Location = new Point(4, 26);
            tabContratos.Name = "tabContratos";
            tabContratos.Padding = new Padding(3);
            tabContratos.Size = new Size(1131, 467);
            tabContratos.TabIndex = 0;
            tabContratos.Text = "Contratos";
            tabContratos.UseVisualStyleBackColor = true;
            // 
            // dgvContratos
            // 
            dgvContratos.BackgroundColor = SystemColors.Control;
            dgvContratos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContratos.Dock = DockStyle.Fill;
            dgvContratos.Location = new Point(3, 53);
            dgvContratos.Name = "dgvContratos";
            dgvContratos.RowHeadersWidth = 51;
            dgvContratos.Size = new Size(985, 411);
            dgvContratos.TabIndex = 0;
            // 
            // pnlBotoesContratos
            // 
            pnlBotoesContratos.Controls.Add(btnNovoContrato);
            pnlBotoesContratos.Controls.Add(btnExcluirContrato);
            pnlBotoesContratos.Controls.Add(btnVerParcelas);
            pnlBotoesContratos.Controls.Add(btnAtualizarContratos);
            pnlBotoesContratos.Dock = DockStyle.Right;
            pnlBotoesContratos.Location = new Point(988, 53);
            pnlBotoesContratos.Name = "pnlBotoesContratos";
            pnlBotoesContratos.Size = new Size(140, 411);
            pnlBotoesContratos.TabIndex = 1;
            // 
            // btnNovoContrato
            // 
            btnNovoContrato.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNovoContrato.Cursor = Cursors.Hand;
            btnNovoContrato.Location = new Point(10, 10);
            btnNovoContrato.Name = "btnNovoContrato";
            btnNovoContrato.Size = new Size(120, 38);
            btnNovoContrato.TabIndex = 0;
            btnNovoContrato.Text = "Novo";
            btnNovoContrato.UseVisualStyleBackColor = true;
            btnNovoContrato.Click += btnNovoContrato_Click;
            // 
            // btnExcluirContrato
            // 
            btnExcluirContrato.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExcluirContrato.Cursor = Cursors.Hand;
            btnExcluirContrato.Location = new Point(10, 56);
            btnExcluirContrato.Name = "btnExcluirContrato";
            btnExcluirContrato.Size = new Size(120, 38);
            btnExcluirContrato.TabIndex = 1;
            btnExcluirContrato.Text = "Excluir";
            btnExcluirContrato.UseVisualStyleBackColor = true;
            btnExcluirContrato.Click += btnExcluirContrato_Click;
            // 
            // btnVerParcelas
            // 
            btnVerParcelas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerParcelas.Cursor = Cursors.Hand;
            btnVerParcelas.Location = new Point(10, 102);
            btnVerParcelas.Name = "btnVerParcelas";
            btnVerParcelas.Size = new Size(120, 38);
            btnVerParcelas.TabIndex = 2;
            btnVerParcelas.Text = "Ver Parcelas";
            btnVerParcelas.UseVisualStyleBackColor = true;
            btnVerParcelas.Click += btnVerParcelas_Click;
            // 
            // btnAtualizarContratos
            // 
            btnAtualizarContratos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAtualizarContratos.Cursor = Cursors.Hand;
            btnAtualizarContratos.Location = new Point(10, 148);
            btnAtualizarContratos.Name = "btnAtualizarContratos";
            btnAtualizarContratos.Size = new Size(120, 38);
            btnAtualizarContratos.TabIndex = 3;
            btnAtualizarContratos.Text = "Atualizar";
            btnAtualizarContratos.UseVisualStyleBackColor = true;
            btnAtualizarContratos.Click += btnAtualizarContratos_Click;
            // 
            // pnlFiltroContratos
            // 
            pnlFiltroContratos.Controls.Add(txtPesquisaContrato);
            pnlFiltroContratos.Controls.Add(lblFiltroAlunoC);
            pnlFiltroContratos.Controls.Add(cmbStatusContrato);
            pnlFiltroContratos.Controls.Add(lblFiltroStatusC);
            pnlFiltroContratos.Dock = DockStyle.Top;
            pnlFiltroContratos.Location = new Point(3, 3);
            pnlFiltroContratos.Name = "pnlFiltroContratos";
            pnlFiltroContratos.Size = new Size(1125, 50);
            pnlFiltroContratos.TabIndex = 2;
            // 
            // txtPesquisaContrato
            // 
            txtPesquisaContrato.Location = new Point(275, 14);
            txtPesquisaContrato.Name = "txtPesquisaContrato";
            txtPesquisaContrato.PlaceholderText = "Pesquisar aluno...";
            txtPesquisaContrato.Size = new Size(220, 22);
            txtPesquisaContrato.TabIndex = 1;
            txtPesquisaContrato.TextChanged += cmbFiltroContrato_Changed;
            // 
            // lblFiltroAlunoC
            // 
            lblFiltroAlunoC.AutoSize = true;
            lblFiltroAlunoC.Location = new Point(225, 18);
            lblFiltroAlunoC.Name = "lblFiltroAlunoC";
            lblFiltroAlunoC.Size = new Size(45, 17);
            lblFiltroAlunoC.TabIndex = 2;
            lblFiltroAlunoC.Text = "Aluno:";
            // 
            // cmbStatusContrato
            // 
            cmbStatusContrato.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusContrato.FormattingEnabled = true;
            cmbStatusContrato.Items.AddRange(new object[] { "Todos", "ATIVO", "CONCLUIDO", "CANCELADO" });
            cmbStatusContrato.Location = new Point(65, 14);
            cmbStatusContrato.Name = "cmbStatusContrato";
            cmbStatusContrato.Size = new Size(140, 25);
            cmbStatusContrato.TabIndex = 0;
            cmbStatusContrato.SelectedIndexChanged += cmbFiltroContrato_Changed;
            // 
            // lblFiltroStatusC
            // 
            lblFiltroStatusC.AutoSize = true;
            lblFiltroStatusC.Location = new Point(12, 18);
            lblFiltroStatusC.Name = "lblFiltroStatusC";
            lblFiltroStatusC.Size = new Size(48, 17);
            lblFiltroStatusC.TabIndex = 3;
            lblFiltroStatusC.Text = "Status:";
            // 
            // tabParcelas
            // 
            tabParcelas.Controls.Add(dgvParcelas);
            tabParcelas.Controls.Add(pnlBotoesParcelas);
            tabParcelas.Controls.Add(pnlFiltroParcelas);
            tabParcelas.Location = new Point(4, 24);
            tabParcelas.Name = "tabParcelas";
            tabParcelas.Padding = new Padding(3);
            tabParcelas.Size = new Size(1131, 469);
            tabParcelas.TabIndex = 1;
            tabParcelas.Text = "Parcelas";
            tabParcelas.UseVisualStyleBackColor = true;
            // 
            // dgvParcelas
            // 
            dgvParcelas.BackgroundColor = SystemColors.Control;
            dgvParcelas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvParcelas.Dock = DockStyle.Fill;
            dgvParcelas.Location = new Point(3, 53);
            dgvParcelas.Name = "dgvParcelas";
            dgvParcelas.RowHeadersWidth = 51;
            dgvParcelas.Size = new Size(985, 413);
            dgvParcelas.TabIndex = 0;
            // 
            // pnlBotoesParcelas
            // 
            pnlBotoesParcelas.Controls.Add(btnDisparoAgendado);
            pnlBotoesParcelas.Controls.Add(btnRegistrarPgto);
            pnlBotoesParcelas.Controls.Add(btnConfirmacaoPgto);
            pnlBotoesParcelas.Controls.Add(btnAtualizarParcelas);
            pnlBotoesParcelas.Controls.Add(btnProcessarNotif);
            pnlBotoesParcelas.Controls.Add(btnEstornar);
            pnlBotoesParcelas.Controls.Add(btnEnviarFiltrados);
            pnlBotoesParcelas.Dock = DockStyle.Right;
            pnlBotoesParcelas.Location = new Point(988, 53);
            pnlBotoesParcelas.Name = "pnlBotoesParcelas";
            pnlBotoesParcelas.Size = new Size(140, 413);
            pnlBotoesParcelas.TabIndex = 1;
            // 
            // btnDisparoAgendado
            // 
            btnDisparoAgendado.Cursor = Cursors.Hand;
            btnDisparoAgendado.Location = new Point(10, 196);
            btnDisparoAgendado.Name = "btnDisparoAgendado";
            btnDisparoAgendado.Size = new Size(120, 59);
            btnDisparoAgendado.TabIndex = 0;
            btnDisparoAgendado.Text = "Disparar mensagens vencimento";
            btnDisparoAgendado.UseVisualStyleBackColor = true;
            btnDisparoAgendado.Click += btnDisparoAgendado_Click;
            // 
            // btnRegistrarPgto
            // 
            btnRegistrarPgto.Cursor = Cursors.Hand;
            btnRegistrarPgto.Location = new Point(10, 10);
            btnRegistrarPgto.Name = "btnRegistrarPgto";
            btnRegistrarPgto.Size = new Size(120, 38);
            btnRegistrarPgto.TabIndex = 0;
            btnRegistrarPgto.Text = "Registrar Pgto";
            btnRegistrarPgto.UseVisualStyleBackColor = true;
            btnRegistrarPgto.Click += btnRegistrarPgto_Click;
            // 
            // btnConfirmacaoPgto
            // 
            btnConfirmacaoPgto.Cursor = Cursors.Hand;
            btnConfirmacaoPgto.Location = new Point(10, 54);
            btnConfirmacaoPgto.Name = "btnConfirmacaoPgto";
            btnConfirmacaoPgto.Size = new Size(120, 44);
            btnConfirmacaoPgto.TabIndex = 1;
            btnConfirmacaoPgto.Text = "Confirmação Pgto";
            btnConfirmacaoPgto.UseVisualStyleBackColor = true;
            btnConfirmacaoPgto.Click += btnConfirmacaoPgto_Click;
            // 
            // btnAtualizarParcelas
            // 
            btnAtualizarParcelas.Cursor = Cursors.Hand;
            btnAtualizarParcelas.Location = new Point(10, 106);
            btnAtualizarParcelas.Name = "btnAtualizarParcelas";
            btnAtualizarParcelas.Size = new Size(120, 38);
            btnAtualizarParcelas.TabIndex = 2;
            btnAtualizarParcelas.Text = "Atualizar";
            btnAtualizarParcelas.UseVisualStyleBackColor = true;
            btnAtualizarParcelas.Click += btnAtualizarParcelas_Click;
            // 
            // btnProcessarNotif
            // 
            btnProcessarNotif.Cursor = Cursors.Hand;
            btnProcessarNotif.Location = new Point(10, 152);
            btnProcessarNotif.Name = "btnProcessarNotif";
            btnProcessarNotif.Size = new Size(120, 38);
            btnProcessarNotif.TabIndex = 3;
            btnProcessarNotif.Text = "Notificar";
            btnProcessarNotif.UseVisualStyleBackColor = true;
            btnProcessarNotif.Click += btnProcessarNotif_Click;
            // 
            // btnEstornar
            // 
            btnEstornar.Cursor = Cursors.Hand;
            btnEstornar.Location = new Point(10, 310);
            btnEstornar.Name = "btnEstornar";
            btnEstornar.Size = new Size(120, 38);
            btnEstornar.TabIndex = 4;
            btnEstornar.Text = "Estornar";
            btnEstornar.UseVisualStyleBackColor = true;
            btnEstornar.Click += btnEstornar_Click;
            // 
            // btnEnviarFiltrados
            // 
            btnEnviarFiltrados.Cursor = Cursors.Hand;
            btnEnviarFiltrados.Location = new Point(10, 261);
            btnEnviarFiltrados.Name = "btnEnviarFiltrados";
            btnEnviarFiltrados.Size = new Size(120, 43);
            btnEnviarFiltrados.TabIndex = 0;
            btnEnviarFiltrados.Text = "Enviar cobrança dos filtrados";
            btnEnviarFiltrados.UseVisualStyleBackColor = true;
            btnEnviarFiltrados.Click += btnEnviarFiltrados_Click;
            // 
            // pnlFiltroParcelas
            // 
            pnlFiltroParcelas.Controls.Add(txtPesquisaParcela);
            pnlFiltroParcelas.Controls.Add(lblFiltroAlunoP);
            pnlFiltroParcelas.Controls.Add(cmbStatusParcela);
            pnlFiltroParcelas.Controls.Add(lblFiltroStatusP);
            pnlFiltroParcelas.Dock = DockStyle.Top;
            pnlFiltroParcelas.Location = new Point(3, 3);
            pnlFiltroParcelas.Name = "pnlFiltroParcelas";
            pnlFiltroParcelas.Size = new Size(1125, 50);
            pnlFiltroParcelas.TabIndex = 2;
            // 
            // txtPesquisaParcela
            // 
            txtPesquisaParcela.Location = new Point(275, 14);
            txtPesquisaParcela.Name = "txtPesquisaParcela";
            txtPesquisaParcela.PlaceholderText = "Pesquisar aluno...";
            txtPesquisaParcela.Size = new Size(220, 22);
            txtPesquisaParcela.TabIndex = 1;
            txtPesquisaParcela.TextChanged += cmbFiltroParcela_Changed;
            // 
            // lblFiltroAlunoP
            // 
            lblFiltroAlunoP.AutoSize = true;
            lblFiltroAlunoP.Location = new Point(225, 18);
            lblFiltroAlunoP.Name = "lblFiltroAlunoP";
            lblFiltroAlunoP.Size = new Size(45, 17);
            lblFiltroAlunoP.TabIndex = 2;
            lblFiltroAlunoP.Text = "Aluno:";
            // 
            // cmbStatusParcela
            // 
            cmbStatusParcela.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusParcela.FormattingEnabled = true;
            cmbStatusParcela.Items.AddRange(new object[] { "Todos", "PENDENTE", "VENCIDA", "PAGA", "CANCELADA" });
            cmbStatusParcela.Location = new Point(65, 14);
            cmbStatusParcela.Name = "cmbStatusParcela";
            cmbStatusParcela.Size = new Size(140, 25);
            cmbStatusParcela.TabIndex = 0;
            cmbStatusParcela.SelectedIndexChanged += cmbFiltroParcela_Changed;
            // 
            // lblFiltroStatusP
            // 
            lblFiltroStatusP.AutoSize = true;
            lblFiltroStatusP.Location = new Point(12, 18);
            lblFiltroStatusP.Name = "lblFiltroStatusP";
            lblFiltroStatusP.Size = new Size(48, 17);
            lblFiltroStatusP.TabIndex = 3;
            lblFiltroStatusP.Text = "Status:";
            // 
            // tabConfig
            // 
            tabConfig.Controls.Add(grpExecutar);
            tabConfig.Controls.Add(grpConfigNotif);
            tabConfig.Location = new Point(4, 24);
            tabConfig.Name = "tabConfig";
            tabConfig.Padding = new Padding(3);
            tabConfig.Size = new Size(1131, 469);
            tabConfig.TabIndex = 2;
            tabConfig.Text = "Configurações";
            tabConfig.UseVisualStyleBackColor = true;
            // 
            // grpExecutar
            // 
            grpExecutar.Controls.Add(txtResultado);
            grpExecutar.Controls.Add(btnExecutarNotif);
            grpExecutar.Location = new Point(440, 16);
            grpExecutar.Name = "grpExecutar";
            grpExecutar.Size = new Size(540, 245);
            grpExecutar.TabIndex = 1;
            grpExecutar.TabStop = false;
            grpExecutar.Text = "Executar Manualmente";
            // 
            // txtResultado
            // 
            txtResultado.BackColor = Color.White;
            txtResultado.Location = new Point(16, 76);
            txtResultado.Multiline = true;
            txtResultado.Name = "txtResultado";
            txtResultado.ReadOnly = true;
            txtResultado.ScrollBars = ScrollBars.Vertical;
            txtResultado.Size = new Size(500, 150);
            txtResultado.TabIndex = 1;
            // 
            // btnExecutarNotif
            // 
            btnExecutarNotif.Cursor = Cursors.Hand;
            btnExecutarNotif.Location = new Point(16, 30);
            btnExecutarNotif.Name = "btnExecutarNotif";
            btnExecutarNotif.Size = new Size(250, 36);
            btnExecutarNotif.TabIndex = 0;
            btnExecutarNotif.Text = "⚡ Processar Notificações Agora";
            btnExecutarNotif.UseVisualStyleBackColor = true;
            btnExecutarNotif.Click += btnExecutarNotif_Click;
            // 
            // grpConfigNotif
            // 
            grpConfigNotif.Controls.Add(btnSalvarConfig);
            grpConfigNotif.Controls.Add(chkFinanceiroAtivo);
            grpConfigNotif.Controls.Add(txtHorarioEnvio);
            grpConfigNotif.Controls.Add(lblHorarioEnvio);
            grpConfigNotif.Controls.Add(chkEnviarWhatsapp);
            grpConfigNotif.Controls.Add(chkEnviarEmail);
            grpConfigNotif.Controls.Add(nudDiasCobranca);
            grpConfigNotif.Controls.Add(lblDiasCobranca);
            grpConfigNotif.Controls.Add(nudDiasLembrete);
            grpConfigNotif.Controls.Add(lblDiasLembrete);
            grpConfigNotif.Location = new Point(16, 16);
            grpConfigNotif.Name = "grpConfigNotif";
            grpConfigNotif.Size = new Size(400, 245);
            grpConfigNotif.TabIndex = 0;
            grpConfigNotif.TabStop = false;
            grpConfigNotif.Text = "Configuração de Notificações Automáticas";
            // 
            // btnSalvarConfig
            // 
            btnSalvarConfig.Cursor = Cursors.Hand;
            btnSalvarConfig.Location = new Point(16, 195);
            btnSalvarConfig.Name = "btnSalvarConfig";
            btnSalvarConfig.Size = new Size(160, 32);
            btnSalvarConfig.TabIndex = 9;
            btnSalvarConfig.Text = "Salvar Configuração";
            btnSalvarConfig.UseVisualStyleBackColor = true;
            btnSalvarConfig.Click += btnSalvarConfig_Click;
            // 
            // chkFinanceiroAtivo
            // 
            chkFinanceiroAtivo.AutoSize = true;
            chkFinanceiroAtivo.Checked = true;
            chkFinanceiroAtivo.CheckState = CheckState.Checked;
            chkFinanceiroAtivo.Location = new Point(16, 160);
            chkFinanceiroAtivo.Name = "chkFinanceiroAtivo";
            chkFinanceiroAtivo.Size = new Size(212, 21);
            chkFinanceiroAtivo.TabIndex = 8;
            chkFinanceiroAtivo.Text = "Notificações financeiras ativas";
            chkFinanceiroAtivo.UseVisualStyleBackColor = true;
            // 
            // txtHorarioEnvio
            // 
            txtHorarioEnvio.Location = new Point(150, 125);
            txtHorarioEnvio.Name = "txtHorarioEnvio";
            txtHorarioEnvio.Size = new Size(70, 22);
            txtHorarioEnvio.TabIndex = 7;
            txtHorarioEnvio.Text = "09:00";
            // 
            // lblHorarioEnvio
            // 
            lblHorarioEnvio.AutoSize = true;
            lblHorarioEnvio.Location = new Point(16, 128);
            lblHorarioEnvio.Name = "lblHorarioEnvio";
            lblHorarioEnvio.Size = new Size(110, 17);
            lblHorarioEnvio.TabIndex = 6;
            lblHorarioEnvio.Text = "Horário de envio:";
            // 
            // chkEnviarWhatsapp
            // 
            chkEnviarWhatsapp.AutoSize = true;
            chkEnviarWhatsapp.Checked = true;
            chkEnviarWhatsapp.CheckState = CheckState.Checked;
            chkEnviarWhatsapp.Location = new Point(200, 96);
            chkEnviarWhatsapp.Name = "chkEnviarWhatsapp";
            chkEnviarWhatsapp.Size = new Size(152, 21);
            chkEnviarWhatsapp.TabIndex = 5;
            chkEnviarWhatsapp.Text = "Enviar por WhatsApp";
            chkEnviarWhatsapp.UseVisualStyleBackColor = true;
            // 
            // chkEnviarEmail
            // 
            chkEnviarEmail.AutoSize = true;
            chkEnviarEmail.Checked = true;
            chkEnviarEmail.CheckState = CheckState.Checked;
            chkEnviarEmail.Location = new Point(16, 96);
            chkEnviarEmail.Name = "chkEnviarEmail";
            chkEnviarEmail.Size = new Size(124, 21);
            chkEnviarEmail.TabIndex = 4;
            chkEnviarEmail.Text = "Enviar por E-mail";
            chkEnviarEmail.UseVisualStyleBackColor = true;
            // 
            // nudDiasCobranca
            // 
            nudDiasCobranca.Location = new Point(260, 60);
            nudDiasCobranca.Maximum = new decimal(new int[] { 90, 0, 0, 0 });
            nudDiasCobranca.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudDiasCobranca.Name = "nudDiasCobranca";
            nudDiasCobranca.Size = new Size(70, 22);
            nudDiasCobranca.TabIndex = 3;
            nudDiasCobranca.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblDiasCobranca
            // 
            lblDiasCobranca.AutoSize = true;
            lblDiasCobranca.Location = new Point(16, 62);
            lblDiasCobranca.Name = "lblDiasCobranca";
            lblDiasCobranca.Size = new Size(170, 17);
            lblDiasCobranca.TabIndex = 2;
            lblDiasCobranca.Text = "Dias de atraso para cobrar:";
            // 
            // nudDiasLembrete
            // 
            nudDiasLembrete.Location = new Point(260, 28);
            nudDiasLembrete.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            nudDiasLembrete.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudDiasLembrete.Name = "nudDiasLembrete";
            nudDiasLembrete.Size = new Size(70, 22);
            nudDiasLembrete.TabIndex = 1;
            nudDiasLembrete.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // lblDiasLembrete
            // 
            lblDiasLembrete.AutoSize = true;
            lblDiasLembrete.Location = new Point(16, 30);
            lblDiasLembrete.Name = "lblDiasLembrete";
            lblDiasLembrete.Size = new Size(211, 17);
            lblDiasLembrete.TabIndex = 0;
            lblDiasLembrete.Text = "Dias de antecedência (lembrete):";
            // 
            // pnlRodape
            // 
            pnlRodape.Controls.Add(lblTotalRegistros);
            pnlRodape.Controls.Add(lblStatusBar);
            pnlRodape.Dock = DockStyle.Bottom;
            pnlRodape.Location = new Point(0, 597);
            pnlRodape.Name = "pnlRodape";
            pnlRodape.Size = new Size(1139, 32);
            pnlRodape.TabIndex = 2;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Location = new Point(909, 8);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(0, 17);
            lblTotalRegistros.TabIndex = 1;
            // 
            // lblStatusBar
            // 
            lblStatusBar.AutoSize = true;
            lblStatusBar.Location = new Point(12, 8);
            lblStatusBar.Name = "lblStatusBar";
            lblStatusBar.Size = new Size(47, 17);
            lblStatusBar.TabIndex = 0;
            lblStatusBar.Text = "Pronto";
            // 
            // FrmFinanceiro
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1139, 629);
            Controls.Add(tabControl);
            Controls.Add(pnlRodape);
            Controls.Add(pnlDashboard);
            Font = new Font("Century Gothic", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmFinanceiro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Financeiro";
            Load += FrmFinanceiro_Load;
            pnlDashboard.ResumeLayout(false);
            tlpCards.ResumeLayout(false);
            pnlCard1.ResumeLayout(false);
            pnlCard2.ResumeLayout(false);
            pnlCard3.ResumeLayout(false);
            pnlCard4.ResumeLayout(false);
            pnlCard5.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabContratos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvContratos).EndInit();
            pnlBotoesContratos.ResumeLayout(false);
            pnlFiltroContratos.ResumeLayout(false);
            pnlFiltroContratos.PerformLayout();
            tabParcelas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvParcelas).EndInit();
            pnlBotoesParcelas.ResumeLayout(false);
            pnlFiltroParcelas.ResumeLayout(false);
            pnlFiltroParcelas.PerformLayout();
            tabConfig.ResumeLayout(false);
            grpExecutar.ResumeLayout(false);
            grpExecutar.PerformLayout();
            grpConfigNotif.ResumeLayout(false);
            grpConfigNotif.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDiasCobranca).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDiasLembrete).EndInit();
            pnlRodape.ResumeLayout(false);
            pnlRodape.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        // ── Dashboard ──
        private Panel pnlDashboard;
        private TableLayoutPanel tlpCards;
        private Panel pnlCard1;
        private Panel pnlCard2;
        private Panel pnlCard3;
        private Panel pnlCard4;
        private Panel pnlCard5;
        private Label lblCard1Title;
        private Label lblCard1Value;
        private Label lblCard2Title;
        private Label lblCard2Value;
        private Label lblCard3Title;
        private Label lblCard3Value;
        private Label lblCard4Title;
        private Label lblCard4Value;
        private Label lblCard5Title;
        private Label lblCard5Value;

        // ── TabControl ──
        private TabControl tabControl;
        private TabPage tabContratos;
        private TabPage tabParcelas;
        private TabPage tabConfig;

        // ── Contratos ──
        private Panel pnlFiltroContratos;
        private Panel pnlBotoesContratos;
        private Label lblFiltroStatusC;
        private Label lblFiltroAlunoC;
        private ComboBox cmbStatusContrato;
        private TextBox txtPesquisaContrato;
        private Button btnNovoContrato;
        private Button btnExcluirContrato;
        private Button btnVerParcelas;
        private Button btnAtualizarContratos;
        private DataGridView dgvContratos;

        // ── Parcelas ──
        private Panel pnlFiltroParcelas;
        private Panel pnlBotoesParcelas;
        private Label lblFiltroStatusP;
        private Label lblFiltroAlunoP;
        private ComboBox cmbStatusParcela;
        private TextBox txtPesquisaParcela;
        private Button btnRegistrarPgto;
        private Button btnConfirmacaoPgto;
        private Button btnAtualizarParcelas;
        private Button btnProcessarNotif;
        private Button btnEstornar;
        private DataGridView dgvParcelas;

        // ── Configurações ──
        private GroupBox grpExecutar;
        private TextBox txtResultado;
        private Button btnExecutarNotif;
        private GroupBox grpConfigNotif;
        private Button btnSalvarConfig;
        private CheckBox chkFinanceiroAtivo;
        private TextBox txtHorarioEnvio;
        private Label lblHorarioEnvio;
        private CheckBox chkEnviarWhatsapp;
        private CheckBox chkEnviarEmail;
        private NumericUpDown nudDiasCobranca;
        private Label lblDiasCobranca;
        private NumericUpDown nudDiasLembrete;
        private Label lblDiasLembrete;

        // ── Rodapé ──
        private Panel pnlRodape;
        private Label lblTotalRegistros;
        private Label lblStatusBar;
        private Button btnDisparoAgendado;
        private Button btnEnviarFiltrados;
    }
}