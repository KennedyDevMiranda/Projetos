namespace Central_do_Educador.Forms
{
    partial class FrmAgendamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgendamento));
            panel1 = new Panel();
            ptbAbrirTemplates = new PictureBox();
            ptbViaWhatsApp = new PictureBox(); // ← nome REAL do controle
            ptbConfiguracaoEmail = new PictureBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            lblObs = new Label();
            txtObservacao = new TextBox();
            lblLocal = new Label();
            txtLocal = new TextBox();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            lblDataHora = new Label();
            dtpDataHora = new DateTimePicker();
            lblAluno = new Label();
            cmbAluno = new ComboBox();
            ptbSalvar = new PictureBox();
            panelFiltros = new Panel();
            lblFiltroData = new Label();
            dtpFiltroData = new DateTimePicker();
            lblFiltroTipo = new Label();
            cmbFiltroTipo = new ComboBox();
            lblFiltroStatus = new Label();
            cmbFiltroStatus = new ComboBox();
            lblFiltroAluno = new Label();
            txtFiltroAluno = new TextBox();
            panel3 = new Panel();
            lblTotal = new Label();
            panelGrid = new Panel();
            dgvAgendamentos = new DataGridView();
            ctxMenuStatus = new ContextMenuStrip(components);
            tsmiConfirmar = new ToolStripMenuItem();
            tsmiCancelar = new ToolStripMenuItem();
            tsmiRemarcar = new ToolStripMenuItem();
            tsmiConcluir = new ToolStripMenuItem();
            tsmiPendente = new ToolStripMenuItem();
            toolTip1 = new ToolTip(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ptbAbrirTemplates).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbViaWhatsApp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbConfiguracaoEmail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbSalvar).BeginInit();
            panelFiltros.SuspendLayout();
            panel3.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAgendamentos).BeginInit();
            ctxMenuStatus.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(ptbAbrirTemplates);
            panel1.Controls.Add(ptbViaWhatsApp);
            panel1.Controls.Add(ptbConfiguracaoEmail);
            panel1.Controls.Add(lblStatus);
            panel1.Controls.Add(cmbStatus);
            panel1.Controls.Add(lblObs);
            panel1.Controls.Add(txtObservacao);
            panel1.Controls.Add(lblLocal);
            panel1.Controls.Add(txtLocal);
            panel1.Controls.Add(lblTipo);
            panel1.Controls.Add(cmbTipo);
            panel1.Controls.Add(lblDataHora);
            panel1.Controls.Add(dtpDataHora);
            panel1.Controls.Add(lblAluno);
            panel1.Controls.Add(cmbAluno);
            panel1.Controls.Add(ptbSalvar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1178, 100);
            panel1.TabIndex = 0;
            // 
            // ptbAbrirTemplates
            // 
            ptbAbrirTemplates.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ptbAbrirTemplates.Cursor = Cursors.Hand;
            ptbAbrirTemplates.Image = Properties.Resources.message;
            ptbAbrirTemplates.Location = new Point(1131, 12);
            ptbAbrirTemplates.Name = "ptbAbrirTemplates";
            ptbAbrirTemplates.Size = new Size(35, 35);
            ptbAbrirTemplates.SizeMode = PictureBoxSizeMode.Zoom;
            ptbAbrirTemplates.TabIndex = 24;
            ptbAbrirTemplates.TabStop = false;
            toolTip1.SetToolTip(ptbAbrirTemplates, "Mensagem");
            ptbAbrirTemplates.Click += ptbAbrirTemplates_Click;
            // 
            // ptbViaWhatsApp
            // 
            ptbViaWhatsApp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ptbViaWhatsApp.Cursor = Cursors.Hand;
            ptbViaWhatsApp.Image = Properties.Resources.whatsapp;
            ptbViaWhatsApp.Location = new Point(1090, 12);
            ptbViaWhatsApp.Name = "ptbViaWhatsApp";
            ptbViaWhatsApp.Size = new Size(35, 35);
            ptbViaWhatsApp.SizeMode = PictureBoxSizeMode.Zoom;
            ptbViaWhatsApp.TabIndex = 23;
            ptbViaWhatsApp.TabStop = false;
            toolTip1.SetToolTip(ptbViaWhatsApp, "Enviar Mensagem Whatsapp\r\n");
            ptbViaWhatsApp.Click += ptbViaWhatsApp_Click; // o controle REAL é ptbViaWhatsApp
            // 
            // ptbConfiguracaoEmail
            // 
            ptbConfiguracaoEmail.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ptbConfiguracaoEmail.Cursor = Cursors.Hand;
            ptbConfiguracaoEmail.Image = Properties.Resources.email;
            ptbConfiguracaoEmail.Location = new Point(1049, 12);
            ptbConfiguracaoEmail.Name = "ptbConfiguracaoEmail";
            ptbConfiguracaoEmail.Size = new Size(35, 35);
            ptbConfiguracaoEmail.SizeMode = PictureBoxSizeMode.Zoom;
            ptbConfiguracaoEmail.TabIndex = 22;
            ptbConfiguracaoEmail.TabStop = false;
            toolTip1.SetToolTip(ptbConfiguracaoEmail, "Enviar Email");
            ptbConfiguracaoEmail.Click += ptbConfiguracaoEmail_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(591, 6);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(46, 17);
            lblStatus.TabIndex = 20;
            lblStatus.Text = "Status";
            lblStatus.Visible = false;
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Items.AddRange(new object[] { "PENDENTE", "CONFIRMADO", "CANCELADO", "REMARCADO", "CONCLUIDO" });
            cmbStatus.Location = new Point(591, 26);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(140, 25);
            cmbStatus.TabIndex = 21;
            cmbStatus.Visible = false;
            // 
            // lblObs
            // 
            lblObs.AutoSize = true;
            lblObs.Location = new Point(270, 54);
            lblObs.Name = "lblObs";
            lblObs.Size = new Size(88, 17);
            lblObs.TabIndex = 18;
            lblObs.Text = "Observação";
            // 
            // txtObservacao
            // 
            txtObservacao.Location = new Point(270, 74);
            txtObservacao.Name = "txtObservacao";
            txtObservacao.Size = new Size(313, 23);
            txtObservacao.TabIndex = 19;
            // 
            // lblLocal
            // 
            lblLocal.AutoSize = true;
            lblLocal.Location = new Point(12, 54);
            lblLocal.Name = "lblLocal";
            lblLocal.Size = new Size(43, 17);
            lblLocal.TabIndex = 16;
            lblLocal.Text = "Local";
            // 
            // txtLocal
            // 
            txtLocal.CharacterCasing = CharacterCasing.Upper;
            txtLocal.Location = new Point(12, 74);
            txtLocal.Name = "txtLocal";
            txtLocal.Size = new Size(250, 23);
            txtLocal.TabIndex = 17;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(453, 6);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(34, 17);
            lblTipo.TabIndex = 14;
            lblTipo.Text = "Tipo";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Items.AddRange(new object[] { "Reposição", "Aula", "Complementar" });
            cmbTipo.Location = new Point(453, 26);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(130, 25);
            cmbTipo.TabIndex = 15;
            // 
            // lblDataHora
            // 
            lblDataHora.AutoSize = true;
            lblDataHora.Location = new Point(270, 6);
            lblDataHora.Name = "lblDataHora";
            lblDataHora.Size = new Size(78, 17);
            lblDataHora.TabIndex = 12;
            lblDataHora.Text = "Data/Hora";
            // 
            // dtpDataHora
            // 
            dtpDataHora.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDataHora.Format = DateTimePickerFormat.Custom;
            dtpDataHora.Location = new Point(270, 26);
            dtpDataHora.Name = "dtpDataHora";
            dtpDataHora.Size = new Size(175, 23);
            dtpDataHora.TabIndex = 13;
            // 
            // lblAluno
            // 
            lblAluno.AutoSize = true;
            lblAluno.Location = new Point(12, 6);
            lblAluno.Name = "lblAluno";
            lblAluno.Size = new Size(45, 17);
            lblAluno.TabIndex = 10;
            lblAluno.Text = "Aluno";
            // 
            // cmbAluno
            // 
            cmbAluno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAluno.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbAluno.FormattingEnabled = true;
            cmbAluno.Location = new Point(12, 26);
            cmbAluno.Name = "cmbAluno";
            cmbAluno.Size = new Size(250, 25);
            cmbAluno.TabIndex = 11;
            // 
            // ptbSalvar
            // 
            ptbSalvar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ptbSalvar.Cursor = Cursors.Hand;
            ptbSalvar.Image = Properties.Resources.save;
            ptbSalvar.Location = new Point(1008, 12);
            ptbSalvar.Name = "ptbSalvar";
            ptbSalvar.Size = new Size(35, 35);
            ptbSalvar.SizeMode = PictureBoxSizeMode.Zoom;
            ptbSalvar.TabIndex = 3;
            ptbSalvar.TabStop = false;
            toolTip1.SetToolTip(ptbSalvar, "Salvar");
            ptbSalvar.Click += ptbSalvar_Click;
            // 
            // panelFiltros
            // 
            panelFiltros.Controls.Add(lblFiltroData);
            panelFiltros.Controls.Add(dtpFiltroData);
            panelFiltros.Controls.Add(lblFiltroTipo);
            panelFiltros.Controls.Add(cmbFiltroTipo);
            panelFiltros.Controls.Add(lblFiltroStatus);
            panelFiltros.Controls.Add(cmbFiltroStatus);
            panelFiltros.Controls.Add(lblFiltroAluno);
            panelFiltros.Controls.Add(txtFiltroAluno);
            panelFiltros.Dock = DockStyle.Left;
            panelFiltros.Location = new Point(0, 100);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Size = new Size(162, 440);
            panelFiltros.TabIndex = 1;
            // 
            // lblFiltroData
            // 
            lblFiltroData.AutoSize = true;
            lblFiltroData.Location = new Point(12, 168);
            lblFiltroData.Name = "lblFiltroData";
            lblFiltroData.Size = new Size(41, 17);
            lblFiltroData.TabIndex = 6;
            lblFiltroData.Text = "Data";
            // 
            // dtpFiltroData
            // 
            dtpFiltroData.Checked = false;
            dtpFiltroData.Format = DateTimePickerFormat.Short;
            dtpFiltroData.Location = new Point(12, 188);
            dtpFiltroData.Name = "dtpFiltroData";
            dtpFiltroData.ShowCheckBox = true;
            dtpFiltroData.Size = new Size(138, 23);
            dtpFiltroData.TabIndex = 7;
            dtpFiltroData.ValueChanged += FiltroChanged;
            // 
            // lblFiltroTipo
            // 
            lblFiltroTipo.AutoSize = true;
            lblFiltroTipo.Location = new Point(12, 114);
            lblFiltroTipo.Name = "lblFiltroTipo";
            lblFiltroTipo.Size = new Size(34, 17);
            lblFiltroTipo.TabIndex = 4;
            lblFiltroTipo.Text = "Tipo";
            // 
            // cmbFiltroTipo
            // 
            cmbFiltroTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroTipo.FormattingEnabled = true;
            cmbFiltroTipo.Items.AddRange(new object[] { "Todos", "Reposição", "Aula", "Complementar" });
            cmbFiltroTipo.Location = new Point(12, 134);
            cmbFiltroTipo.Name = "cmbFiltroTipo";
            cmbFiltroTipo.Size = new Size(138, 25);
            cmbFiltroTipo.TabIndex = 5;
            cmbFiltroTipo.SelectedIndexChanged += FiltroChanged;
            // 
            // lblFiltroStatus
            // 
            lblFiltroStatus.AutoSize = true;
            lblFiltroStatus.Location = new Point(12, 60);
            lblFiltroStatus.Name = "lblFiltroStatus";
            lblFiltroStatus.Size = new Size(46, 17);
            lblFiltroStatus.TabIndex = 2;
            lblFiltroStatus.Text = "Status";
            // 
            // cmbFiltroStatus
            // 
            cmbFiltroStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroStatus.FormattingEnabled = true;
            cmbFiltroStatus.Items.AddRange(new object[] { "Todos", "PENDENTE", "CONFIRMADO", "CANCELADO", "REMARCADO", "CONCLUIDO" });
            cmbFiltroStatus.Location = new Point(12, 80);
            cmbFiltroStatus.Name = "cmbFiltroStatus";
            cmbFiltroStatus.Size = new Size(138, 25);
            cmbFiltroStatus.TabIndex = 3;
            cmbFiltroStatus.SelectedIndexChanged += FiltroChanged;
            // 
            // lblFiltroAluno
            // 
            lblFiltroAluno.AutoSize = true;
            lblFiltroAluno.Location = new Point(12, 8);
            lblFiltroAluno.Name = "lblFiltroAluno";
            lblFiltroAluno.Size = new Size(45, 17);
            lblFiltroAluno.TabIndex = 0;
            lblFiltroAluno.Text = "Aluno";
            // 
            // txtFiltroAluno
            // 
            txtFiltroAluno.CharacterCasing = CharacterCasing.Upper;
            txtFiltroAluno.Location = new Point(12, 28);
            txtFiltroAluno.Name = "txtFiltroAluno";
            txtFiltroAluno.Size = new Size(138, 23);
            txtFiltroAluno.TabIndex = 1;
            txtFiltroAluno.TextChanged += FiltroChanged;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblTotal);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 540);
            panel3.Name = "panel3";
            panel3.Size = new Size(1178, 29);
            panel3.TabIndex = 1;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(12, 7);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(39, 17);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Total";
            // 
            // panelGrid
            // 
            panelGrid.Controls.Add(dgvAgendamentos);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(162, 100);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(1016, 440);
            panelGrid.TabIndex = 2;
            // 
            // dgvAgendamentos
            // 
            dgvAgendamentos.BackgroundColor = SystemColors.Control;
            dgvAgendamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAgendamentos.ContextMenuStrip = ctxMenuStatus;
            dgvAgendamentos.Dock = DockStyle.Fill;
            dgvAgendamentos.Location = new Point(0, 0);
            dgvAgendamentos.Name = "dgvAgendamentos";
            dgvAgendamentos.Size = new Size(1016, 440);
            dgvAgendamentos.TabIndex = 0;
            dgvAgendamentos.CellDoubleClick += dgvAgendamentos_CellDoubleClick;
            dgvAgendamentos.MouseDown += dgvAgendamentos_MouseDown;
            // 
            // ctxMenuStatus
            // 
            ctxMenuStatus.Items.AddRange(new ToolStripItem[] { tsmiConfirmar, tsmiCancelar, tsmiRemarcar, tsmiConcluir, tsmiPendente });
            ctxMenuStatus.Name = "ctxMenuStatus";
            ctxMenuStatus.Size = new Size(191, 114);
            // 
            // tsmiConfirmar
            // 
            tsmiConfirmar.Name = "tsmiConfirmar";
            tsmiConfirmar.Size = new Size(190, 22);
            tsmiConfirmar.Text = "✅  Confirmar";
            tsmiConfirmar.Click += tsmiConfirmar_Click;
            // 
            // tsmiCancelar
            // 
            tsmiCancelar.Name = "tsmiCancelar";
            tsmiCancelar.Size = new Size(190, 22);
            tsmiCancelar.Text = "❌  Cancelar";
            tsmiCancelar.Click += tsmiCancelar_Click;
            // 
            // tsmiRemarcar
            // 
            tsmiRemarcar.Name = "tsmiRemarcar";
            tsmiRemarcar.Size = new Size(190, 22);
            tsmiRemarcar.Text = "🔄  Remarcar";
            tsmiRemarcar.Click += tsmiRemarcar_Click;
            // 
            // tsmiConcluir
            // 
            tsmiConcluir.Name = "tsmiConcluir";
            tsmiConcluir.Size = new Size(190, 22);
            tsmiConcluir.Text = "✔️  Concluir";
            tsmiConcluir.Click += tsmiConcluir_Click;
            // 
            // tsmiPendente
            // 
            tsmiPendente.Name = "tsmiPendente";
            tsmiPendente.Size = new Size(190, 22);
            tsmiPendente.Text = "⏳  Voltar p/ Pendente";
            tsmiPendente.Click += tsmiPendente_Click;
            // 
            // FrmAgendamento
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1178, 569);
            Controls.Add(panelGrid);
            Controls.Add(panelFiltros);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAgendamento";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Agendamento";
            Load += FrmAgendamento_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ptbAbrirTemplates).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbViaWhatsApp).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbConfiguracaoEmail).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbSalvar).EndInit();
            panelFiltros.ResumeLayout(false);
            panelFiltros.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAgendamentos).EndInit();
            ctxMenuStatus.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panelFiltros;
        private Panel panel3;
        private Panel panelGrid;
        private PictureBox ptbSalvar;
        private ToolTip toolTip1;
        private DataGridView dgvAgendamentos;
        private Label lblFiltroAluno;
        private TextBox txtFiltroAluno;
        private Label lblFiltroStatus;
        private ComboBox cmbFiltroStatus;
        private Label lblFiltroTipo;
        private ComboBox cmbFiltroTipo;
        private Label lblFiltroData;
        private DateTimePicker dtpFiltroData;
        private Label lblTotal;
        private Label lblAluno;
        private ComboBox cmbAluno;
        private Label lblDataHora;
        private DateTimePicker dtpDataHora;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Label lblLocal;
        private TextBox txtLocal;
        private Label lblObs;
        private TextBox txtObservacao;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private ContextMenuStrip ctxMenuStatus;
        private ToolStripMenuItem tsmiConfirmar;
        private ToolStripMenuItem tsmiCancelar;
        private ToolStripMenuItem tsmiRemarcar;
        private ToolStripMenuItem tsmiConcluir;
        private ToolStripMenuItem tsmiPendente;
        private PictureBox ptbConfiguracaoEmail;
        private PictureBox ptbAbrirTemplates;
        private PictureBox ptbViaWhatsApp;
    }
}