namespace Central_do_Educador.Forms
{
    partial class FrmUsuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUsuarios));
            panel1 = new Panel();
            pictureBox5 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            txtFiltroLogin = new TextBox();
            label3 = new Label();
            txtSenha = new TextBox();
            label = new Label();
            txtLogin = new TextBox();
            label2 = new Label();
            txtNome = new TextBox();
            label1 = new Label();
            lblNivel = new Label();
            cmbNivel = new ComboBox();
            panel2 = new Panel();
            dgvUsuarios = new DataGridView();
            toolTip1 = new ToolTip(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox5);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(txtFiltroLogin);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtSenha);
            panel1.Controls.Add(label);
            panel1.Controls.Add(txtLogin);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtNome);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblNivel);
            panel1.Controls.Add(cmbNivel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(828, 115);
            panel1.TabIndex = 0;
            // 
            // pictureBox5
            // 
            pictureBox5.Cursor = Cursors.Hand;
            pictureBox5.Image = Properties.Resources.update;
            pictureBox5.Location = new Point(776, 60);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(40, 40);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 17;
            pictureBox5.TabStop = false;
            toolTip1.SetToolTip(pictureBox5, "Atualizar");
            pictureBox5.Click += btnAtualizar_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.Image = Properties.Resources.reset;
            pictureBox3.Location = new Point(730, 60);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(40, 40);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 16;
            pictureBox3.TabStop = false;
            toolTip1.SetToolTip(pictureBox3, "Redefinir");
            pictureBox3.Click += btnResetSenha_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Cursor = Cursors.Hand;
            pictureBox4.Image = Properties.Resources.button_ON;
            pictureBox4.Location = new Point(684, 60);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(40, 40);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 15;
            pictureBox4.TabStop = false;
            toolTip1.SetToolTip(pictureBox4, "Ativar");
            pictureBox4.Click += btnAtivarDesativar_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = Properties.Resources.save;
            pictureBox2.Location = new Point(638, 60);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(40, 40);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 14;
            pictureBox2.TabStop = false;
            toolTip1.SetToolTip(pictureBox2, "Salvar");
            pictureBox2.Click += btnSalvar_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources.adicionar_usuario;
            pictureBox1.Location = new Point(592, 60);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 40);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            toolTip1.SetToolTip(pictureBox1, "Novo");
            pictureBox1.Click += btnNovo_Click;
            // 
            // txtFiltroLogin
            // 
            txtFiltroLogin.CharacterCasing = CharacterCasing.Upper;
            txtFiltroLogin.Location = new Point(14, 81);
            txtFiltroLogin.Name = "txtFiltroLogin";
            txtFiltroLogin.Size = new Size(198, 23);
            txtFiltroLogin.TabIndex = 12;
            txtFiltroLogin.TextChanged += txtFiltroLogin_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 60);
            label3.Name = "label3";
            label3.Size = new Size(42, 17);
            label3.TabIndex = 11;
            label3.Text = "Filtrar";
            // 
            // txtSenha
            // 
            txtSenha.Location = new Point(402, 31);
            txtSenha.Name = "txtSenha";
            txtSenha.Size = new Size(149, 23);
            txtSenha.TabIndex = 6;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(402, 10);
            label.Name = "label";
            label.Size = new Size(47, 17);
            label.TabIndex = 5;
            label.Text = "Senha";
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(246, 31);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(149, 23);
            txtLogin.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(246, 10);
            label2.Name = "label2";
            label2.Size = new Size(54, 17);
            label2.TabIndex = 3;
            label2.Text = "Usuário";
            // 
            // txtNome
            // 
            txtNome.CharacterCasing = CharacterCasing.Upper;
            txtNome.Location = new Point(14, 31);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(225, 23);
            txtNome.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 10);
            label1.Name = "label1";
            label1.Size = new Size(48, 17);
            label1.TabIndex = 1;
            label1.Text = "Nome";
            // 
            // lblNivel
            // 
            lblNivel.AutoSize = true;
            lblNivel.Location = new Point(246, 60);
            lblNivel.Name = "lblNivel";
            lblNivel.Size = new Size(40, 17);
            lblNivel.TabIndex = 18;
            lblNivel.Text = "Nível";
            // 
            // cmbNivel
            // 
            cmbNivel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNivel.Location = new Point(246, 81);
            cmbNivel.Name = "cmbNivel";
            cmbNivel.Size = new Size(149, 25);
            cmbNivel.TabIndex = 18;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvUsuarios);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 115);
            panel2.Name = "panel2";
            panel2.Size = new Size(828, 321);
            panel2.TabIndex = 1;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.BackgroundColor = SystemColors.Control;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Dock = DockStyle.Fill;
            dgvUsuarios.Location = new Point(0, 0);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(828, 321);
            dgvUsuarios.TabIndex = 0;
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(828, 436);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Usuários";
            Activated += FrmUsuarios_Activated;
            Load += FrmUsuarios_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private DataGridView dgvUsuarios;
        private TextBox txtNome;
        private Label label1;
        private TextBox txtLogin;
        private Label label2;
        private TextBox txtSenha;
        private Label label;
        private TextBox txtFiltroLogin;
        private Label label3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private ToolTip toolTip1;
        private Label lblNivel;
        private ComboBox cmbNivel;
    }
}