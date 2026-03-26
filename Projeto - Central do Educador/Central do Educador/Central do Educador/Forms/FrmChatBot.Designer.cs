namespace Central_do_Educador.Forms
{
    partial class FrmChatBot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChatBot));
            ptbConectar = new PictureBox();
            lblStatus = new Label();
            label1 = new Label();
            txtMensagem = new TextBox();
            pgbProgresso = new ProgressBar();
            btnAnalisar = new PictureBox();
            btnEnviarMassa = new PictureBox();
            txtHistorico = new TextBox();
            lblPorcentagem = new Label();
            lblContador = new Label();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)ptbConectar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAnalisar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEnviarMassa).BeginInit();
            SuspendLayout();
            // 
            // ptbConectar
            // 
            ptbConectar.Cursor = Cursors.Hand;
            ptbConectar.Image = Properties.Resources.codigo_qr_OFF;
            ptbConectar.Location = new Point(603, 12);
            ptbConectar.Name = "ptbConectar";
            ptbConectar.Size = new Size(40, 40);
            ptbConectar.SizeMode = PictureBoxSizeMode.Zoom;
            ptbConectar.TabIndex = 1;
            ptbConectar.TabStop = false;
            toolTip1.SetToolTip(ptbConectar, "Clique para desconectar a sessão do WhatsApp");
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 9);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(50, 17);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Status:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 45);
            label1.Name = "label1";
            label1.Size = new Size(83, 17);
            label1.TabIndex = 3;
            label1.Text = "Mensagem:";
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(12, 65);
            txtMensagem.Multiline = true;
            txtMensagem.Name = "txtMensagem";
            txtMensagem.ScrollBars = ScrollBars.Vertical;
            txtMensagem.Size = new Size(631, 96);
            txtMensagem.TabIndex = 4;
            // 
            // pgbProgresso
            // 
            pgbProgresso.Location = new Point(12, 167);
            pgbProgresso.Name = "pgbProgresso";
            pgbProgresso.Size = new Size(539, 14);
            pgbProgresso.TabIndex = 5;
            // 
            // btnAnalisar
            // 
            btnAnalisar.Cursor = Cursors.Hand;
            btnAnalisar.Image = Properties.Resources.analyze;
            btnAnalisar.Location = new Point(557, 185);
            btnAnalisar.Name = "btnAnalisar";
            btnAnalisar.Size = new Size(40, 40);
            btnAnalisar.SizeMode = PictureBoxSizeMode.Zoom;
            btnAnalisar.TabIndex = 6;
            btnAnalisar.TabStop = false;
            toolTip1.SetToolTip(btnAnalisar, "Buscar planilha");
            // 
            // btnEnviarMassa
            // 
            btnEnviarMassa.Cursor = Cursors.Hand;
            btnEnviarMassa.Image = Properties.Resources.enviar_emMassa;
            btnEnviarMassa.Location = new Point(603, 185);
            btnEnviarMassa.Name = "btnEnviarMassa";
            btnEnviarMassa.Size = new Size(40, 40);
            btnEnviarMassa.SizeMode = PictureBoxSizeMode.Zoom;
            btnEnviarMassa.TabIndex = 7;
            btnEnviarMassa.TabStop = false;
            toolTip1.SetToolTip(btnEnviarMassa, "Enviar em massa");
            // 
            // txtHistorico
            // 
            txtHistorico.Location = new Point(12, 231);
            txtHistorico.Multiline = true;
            txtHistorico.Name = "txtHistorico";
            txtHistorico.ScrollBars = ScrollBars.Vertical;
            txtHistorico.Size = new Size(631, 231);
            txtHistorico.TabIndex = 8;
            // 
            // lblPorcentagem
            // 
            lblPorcentagem.AutoSize = true;
            lblPorcentagem.Location = new Point(557, 164);
            lblPorcentagem.Name = "lblPorcentagem";
            lblPorcentagem.Size = new Size(12, 17);
            lblPorcentagem.TabIndex = 9;
            lblPorcentagem.Text = "-";
            // 
            // lblContador
            // 
            lblContador.AutoSize = true;
            lblContador.Location = new Point(12, 197);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(12, 17);
            lblContador.TabIndex = 10;
            lblContador.Text = "-";
            // 
            // FrmChatBot
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(655, 474);
            Controls.Add(lblContador);
            Controls.Add(lblPorcentagem);
            Controls.Add(txtHistorico);
            Controls.Add(btnEnviarMassa);
            Controls.Add(btnAnalisar);
            Controls.Add(pgbProgresso);
            Controls.Add(txtMensagem);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            Controls.Add(ptbConectar);
            Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmChatBot";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chat Bot";
            Load += FrmChatBot_Load;
            ((System.ComponentModel.ISupportInitialize)ptbConectar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAnalisar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEnviarMassa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox ptbConectar;
        private Label lblStatus;
        private Label label1;
        private TextBox txtMensagem;
        private ProgressBar pgbProgresso;
        private PictureBox btnAnalisar;
        private PictureBox btnEnviarMassa;
        private TextBox txtHistorico;
        private Label lblPorcentagem;
        private Label lblContador;
        private ToolTip toolTip1;
    }
}