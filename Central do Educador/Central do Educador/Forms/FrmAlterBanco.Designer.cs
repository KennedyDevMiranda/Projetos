namespace Central_do_Educador.Forms
{
    partial class FrmAlterBanco
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlterBanco));
            btnProcurarBD = new Button();
            txtCaminho = new TextBox();
            label1 = new Label();
            btnOnOffBD = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnProcurarBD
            // 
            btnProcurarBD.Location = new Point(212, 58);
            btnProcurarBD.Name = "btnProcurarBD";
            btnProcurarBD.Size = new Size(75, 26);
            btnProcurarBD.TabIndex = 0;
            btnProcurarBD.Text = "Procurar";
            btnProcurarBD.UseVisualStyleBackColor = true;
            btnProcurarBD.Click += btnProcurarBD_Click;
            // 
            // txtCaminho
            // 
            txtCaminho.Location = new Point(12, 30);
            txtCaminho.Name = "txtCaminho";
            txtCaminho.Size = new Size(356, 22);
            txtCaminho.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(121, 17);
            label1.TabIndex = 2;
            label1.Text = "Caminho do Banco";
            // 
            // btnOnOffBD
            // 
            btnOnOffBD.Location = new Point(293, 113);
            btnOnOffBD.Name = "btnOnOffBD";
            btnOnOffBD.Size = new Size(75, 26);
            btnOnOffBD.TabIndex = 4;
            btnOnOffBD.Text = "Ligar";
            btnOnOffBD.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.save;
            pictureBox1.Location = new Point(333, 58);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(35, 35);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            pictureBox1.Click += btnSalvarBD_Click;
            // 
            // FrmAlterBanco
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(387, 288);
            Controls.Add(pictureBox1);
            Controls.Add(btnOnOffBD);
            Controls.Add(label1);
            Controls.Add(txtCaminho);
            Controls.Add(btnProcurarBD);
            Font = new Font("Century Gothic", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmAlterBanco";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AlterBanco";
            Activated += FrmAlterBanco_Activated;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnProcurarBD;
        private TextBox txtCaminho;
        private Label label1;
        private Button btnOnOffBD;
        private PictureBox pictureBox1;
    }
}