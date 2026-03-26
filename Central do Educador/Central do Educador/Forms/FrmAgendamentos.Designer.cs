namespace Central_do_Educador
{
    partial class FrmAgendamentos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            groupBox1 = new GroupBox();
            btnReagendar = new Button();
            btnCancelarAgendamento = new Button();
            btnProcessarAgora = new Button();
            btnRecarregar = new Button();
            btnCancelar = new Button();
            btnSalvar = new Button();
            btnNovo = new Button();
            chkAtivo = new CheckBox();
            dtpHora = new DateTimePicker();
            dtpData = new DateTimePicker();
            txtMensagem = new TextBox();
            txtTelefone = new TextBox();
            txtDestinatario = new TextBox();
            lblHora = new Label();
            lblData = new Label();
            lblMensagem = new Label();
            lblTelefone = new Label();
            lblDestinatario = new Label();
            dgvAgendamentos = new DataGridView();
            panelBottom = new Panel();
            lblTotal = new Label();
            lblMotor = new Label();
            button1 = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAgendamentos).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(12, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(279, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Agendamentos de Mensagens";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(btnReagendar);
            groupBox1.Controls.Add(btnCancelarAgendamento);
            groupBox1.Controls.Add(btnProcessarAgora);
            groupBox1.Controls.Add(btnRecarregar);
            groupBox1.Controls.Add(btnCancelar);
            groupBox1.Controls.Add(btnSalvar);
            groupBox1.Controls.Add(btnNovo);
            groupBox1.Controls.Add(chkAtivo);
            groupBox1.Controls.Add(dtpHora);
            groupBox1.Controls.Add(dtpData);
            groupBox1.Controls.Add(txtMensagem);
            groupBox1.Controls.Add(txtTelefone);
            groupBox1.Controls.Add(txtDestinatario);
            groupBox1.Controls.Add(lblHora);
            groupBox1.Controls.Add(lblData);
            groupBox1.Controls.Add(lblMensagem);
            groupBox1.Controls.Add(lblTelefone);
            groupBox1.Controls.Add(lblDestinatario);
            groupBox1.Location = new Point(12, 40);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(980, 210);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Cadastro";
            // 
            // btnReagendar
            // 
            btnReagendar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnReagendar.Location = new Point(835, 184);
            btnReagendar.Name = "btnReagendar";
            btnReagendar.Size = new Size(142, 27);
            btnReagendar.TabIndex = 17;
            btnReagendar.Text = "Reagendar";
            btnReagendar.UseVisualStyleBackColor = true;
            btnReagendar.Click += btnReagendar_Click;
            // 
            // btnCancelarAgendamento
            // 
            btnCancelarAgendamento.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelarAgendamento.Location = new Point(835, 151);
            btnCancelarAgendamento.Name = "btnCancelarAgendamento";
            btnCancelarAgendamento.Size = new Size(142, 27);
            btnCancelarAgendamento.TabIndex = 16;
            btnCancelarAgendamento.Text = "Cancelar agendamento";
            btnCancelarAgendamento.UseVisualStyleBackColor = true;
            btnCancelarAgendamento.Click += btnCancelarAgendamento_Click;
            // 
            // btnProcessarAgora
            // 
            btnProcessarAgora.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProcessarAgora.Location = new Point(832, 118);
            btnProcessarAgora.Name = "btnProcessarAgora";
            btnProcessarAgora.Size = new Size(142, 27);
            btnProcessarAgora.TabIndex = 15;
            btnProcessarAgora.Text = "Processar agora";
            btnProcessarAgora.UseVisualStyleBackColor = true;
            btnProcessarAgora.Click += btnProcessarAgora_Click;
            // 
            // btnRecarregar
            // 
            btnRecarregar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRecarregar.Location = new Point(832, 85);
            btnRecarregar.Name = "btnRecarregar";
            btnRecarregar.Size = new Size(142, 27);
            btnRecarregar.TabIndex = 14;
            btnRecarregar.Text = "Recarregar grid";
            btnRecarregar.UseVisualStyleBackColor = true;
            btnRecarregar.Click += btnRecarregar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelar.Location = new Point(832, 52);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(142, 27);
            btnCancelar.TabIndex = 13;
            btnCancelar.Text = "Limpar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSalvar.Location = new Point(832, 19);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(142, 27);
            btnSalvar.TabIndex = 12;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnNovo
            // 
            btnNovo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNovo.Location = new Point(684, 19);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(142, 27);
            btnNovo.TabIndex = 11;
            btnNovo.Text = "Novo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            // 
            // chkAtivo
            // 
            chkAtivo.AutoSize = true;
            chkAtivo.Location = new Point(557, 46);
            chkAtivo.Name = "chkAtivo";
            chkAtivo.Size = new Size(54, 19);
            chkAtivo.TabIndex = 10;
            chkAtivo.Text = "Ativo";
            chkAtivo.UseVisualStyleBackColor = true;
            // 
            // dtpHora
            // 
            dtpHora.Location = new Point(418, 43);
            dtpHora.Name = "dtpHora";
            dtpHora.Size = new Size(121, 23);
            dtpHora.TabIndex = 9;
            // 
            // dtpData
            // 
            dtpData.Location = new Point(86, 43);
            dtpData.Name = "dtpData";
            dtpData.Size = new Size(215, 23);
            dtpData.TabIndex = 8;
            // 
            // txtMensagem
            // 
            txtMensagem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMensagem.Location = new Point(86, 104);
            txtMensagem.Multiline = true;
            txtMensagem.Name = "txtMensagem";
            txtMensagem.ScrollBars = ScrollBars.Vertical;
            txtMensagem.Size = new Size(740, 94);
            txtMensagem.TabIndex = 7;
            // 
            // txtTelefone
            // 
            txtTelefone.Location = new Point(418, 18);
            txtTelefone.Name = "txtTelefone";
            txtTelefone.Size = new Size(191, 23);
            txtTelefone.TabIndex = 6;
            // 
            // txtDestinatario
            // 
            txtDestinatario.Location = new Point(86, 18);
            txtDestinatario.Name = "txtDestinatario";
            txtDestinatario.Size = new Size(215, 23);
            txtDestinatario.TabIndex = 5;
            // 
            // lblHora
            // 
            lblHora.AutoSize = true;
            lblHora.Location = new Point(360, 46);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(33, 15);
            lblHora.TabIndex = 4;
            lblHora.Text = "Hora";
            // 
            // lblData
            // 
            lblData.AutoSize = true;
            lblData.Location = new Point(6, 46);
            lblData.Name = "lblData";
            lblData.Size = new Size(31, 15);
            lblData.TabIndex = 3;
            lblData.Text = "Data";
            // 
            // lblMensagem
            // 
            lblMensagem.AutoSize = true;
            lblMensagem.Location = new Point(6, 107);
            lblMensagem.Name = "lblMensagem";
            lblMensagem.Size = new Size(66, 15);
            lblMensagem.TabIndex = 2;
            lblMensagem.Text = "Mensagem";
            // 
            // lblTelefone
            // 
            lblTelefone.AutoSize = true;
            lblTelefone.Location = new Point(360, 21);
            lblTelefone.Name = "lblTelefone";
            lblTelefone.Size = new Size(52, 15);
            lblTelefone.TabIndex = 1;
            lblTelefone.Text = "Telefone";
            // 
            // lblDestinatario
            // 
            lblDestinatario.AutoSize = true;
            lblDestinatario.Location = new Point(6, 21);
            lblDestinatario.Name = "lblDestinatario";
            lblDestinatario.Size = new Size(70, 15);
            lblDestinatario.TabIndex = 0;
            lblDestinatario.Text = "Destinatário";
            // 
            // dgvAgendamentos
            // 
            dgvAgendamentos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAgendamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAgendamentos.Location = new Point(12, 257);
            dgvAgendamentos.Name = "dgvAgendamentos";
            dgvAgendamentos.Size = new Size(980, 371);
            dgvAgendamentos.TabIndex = 2;
            // 
            // panelBottom
            // 
            panelBottom.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBottom.Controls.Add(lblTotal);
            panelBottom.Controls.Add(lblMotor);
            panelBottom.Location = new Point(12, 634);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(980, 34);
            panelBottom.TabIndex = 3;
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(3, 9);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(39, 15);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "Total: ";
            // 
            // lblMotor
            // 
            lblMotor.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblMotor.Location = new Point(300, 9);
            lblMotor.Name = "lblMotor";
            lblMotor.Size = new Size(677, 15);
            lblMotor.TabIndex = 0;
            lblMotor.Text = "Motor";
            lblMotor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(844, 26);
            button1.Name = "button1";
            button1.Size = new Size(142, 27);
            button1.TabIndex = 18;
            button1.Text = "ChatBot";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FrmAgendamentos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1004, 680);
            Controls.Add(button1);
            Controls.Add(panelBottom);
            Controls.Add(dgvAgendamentos);
            Controls.Add(groupBox1);
            Controls.Add(lblTitulo);
            MinimumSize = new Size(900, 650);
            Name = "FrmAgendamentos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Agendamentos";
            FormClosing += FrmAgendamentos_FormClosing;
            Load += FrmAgendamentos_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAgendamentos).EndInit();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReagendar;
        private System.Windows.Forms.Button btnCancelarAgendamento;
        private System.Windows.Forms.Button btnProcessarAgora;
        private System.Windows.Forms.Button btnRecarregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.TextBox txtDestinatario;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.Label lblDestinatario;
        private System.Windows.Forms.DataGridView dgvAgendamentos;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblMotor;
        private Button button1;
    }
}