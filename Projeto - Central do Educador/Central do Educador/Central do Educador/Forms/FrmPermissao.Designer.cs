namespace Central_do_Educador.Forms
{
    partial class FrmPermissao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPermissao));
            cmbUsuarios = new ComboBox();
            label1 = new Label();
            clbPermissoes = new CheckedListBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.FormattingEnabled = true;
            cmbUsuarios.Location = new Point(12, 29);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(235, 25);
            cmbUsuarios.TabIndex = 0;
            cmbUsuarios.SelectedIndexChanged += cmbUsuarios_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(54, 17);
            label1.TabIndex = 1;
            label1.Text = "Usuário";
            // 
            // clbPermissoes
            // 
            clbPermissoes.FormattingEnabled = true;
            clbPermissoes.Location = new Point(12, 60);
            clbPermissoes.Name = "clbPermissoes";
            clbPermissoes.Size = new Size(235, 364);
            clbPermissoes.TabIndex = 2;
            clbPermissoes.SelectedIndexChanged += cmbUsuarios_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(253, 239);
            button1.Name = "button1";
            button1.Size = new Size(103, 23);
            button1.TabIndex = 3;
            button1.Text = "Salvar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSalvar_Click;
            // 
            // FrmPermissao
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(385, 430);
            Controls.Add(button1);
            Controls.Add(clbPermissoes);
            Controls.Add(label1);
            Controls.Add(cmbUsuarios);
            Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmPermissao";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Permissão";
            Load += FrmPermissao_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbUsuarios;
        private Label label1;
        private CheckedListBox clbPermissoes;
        private Button button1;
    }
}