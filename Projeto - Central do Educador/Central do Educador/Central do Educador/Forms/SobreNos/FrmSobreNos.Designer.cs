namespace Central_do_Educador.Forms
{
    partial class FrmSobreNos
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
            lblDescricao = new Label();
            lblDesenvolvedor = new Label();
            lblVersao = new Label();
            lnkGitHub = new LinkLabel();
            btnFechar = new Button();
            panelTopo = new Panel();
            lblDireitos = new Label();
            panelTopo.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.DodgerBlue;
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Century Gothic", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(420, 70);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Central do Educador";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDescricao
            // 
            lblDescricao.Font = new Font("Century Gothic", 9F);
            lblDescricao.ForeColor = Color.FromArgb(80, 80, 80);
            lblDescricao.Location = new Point(30, 85);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(360, 40);
            lblDescricao.TabIndex = 1;
            lblDescricao.Text = "Sistema de gestão educacional para controle de reposições, taxas, alunos e comunicação via ChatBot.";
            lblDescricao.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDesenvolvedor
            // 
            lblDesenvolvedor.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            lblDesenvolvedor.ForeColor = Color.FromArgb(33, 37, 41);
            lblDesenvolvedor.Location = new Point(30, 135);
            lblDesenvolvedor.Name = "lblDesenvolvedor";
            lblDesenvolvedor.Size = new Size(360, 20);
            lblDesenvolvedor.TabIndex = 2;
            lblDesenvolvedor.Text = "👨‍💻 Desenvolvido por: Kennedy Miranda Venancio";
            lblDesenvolvedor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVersao
            // 
            lblVersao.Font = new Font("Century Gothic", 9F);
            lblVersao.ForeColor = Color.FromArgb(100, 100, 100);
            lblVersao.Location = new Point(30, 165);
            lblVersao.Name = "lblVersao";
            lblVersao.Size = new Size(360, 20);
            lblVersao.TabIndex = 3;
            lblVersao.Text = "Versão: -";
            lblVersao.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lnkGitHub
            // 
            lnkGitHub.Font = new Font("Century Gothic", 9F);
            lnkGitHub.LinkColor = Color.FromArgb(0, 102, 204);
            lnkGitHub.Location = new Point(30, 195);
            lnkGitHub.Name = "lnkGitHub";
            lnkGitHub.Size = new Size(360, 20);
            lnkGitHub.TabIndex = 4;
            lnkGitHub.TabStop = true;
            lnkGitHub.Text = "🔗 GitHub do Desenvolvedor";
            lnkGitHub.TextAlign = ContentAlignment.MiddleCenter;
            lnkGitHub.LinkClicked += lnkGitHub_LinkClicked;
            // 
            // btnFechar
            // 
            btnFechar.BackColor = Color.FromArgb(33, 37, 41);
            btnFechar.Cursor = Cursors.Hand;
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.FlatStyle = FlatStyle.Flat;
            btnFechar.Font = new Font("Century Gothic", 9.75F, FontStyle.Bold);
            btnFechar.ForeColor = Color.White;
            btnFechar.Location = new Point(160, 260);
            btnFechar.Name = "btnFechar";
            btnFechar.Size = new Size(100, 32);
            btnFechar.TabIndex = 6;
            btnFechar.Text = "Fechar";
            btnFechar.UseVisualStyleBackColor = false;
            btnFechar.Click += btnFechar_Click;
            // 
            // panelTopo
            // 
            panelTopo.BackColor = Color.FromArgb(33, 37, 41);
            panelTopo.Controls.Add(lblTitulo);
            panelTopo.Dock = DockStyle.Top;
            panelTopo.Location = new Point(0, 0);
            panelTopo.Name = "panelTopo";
            panelTopo.Size = new Size(420, 70);
            panelTopo.TabIndex = 0;
            // 
            // lblDireitos
            // 
            lblDireitos.Font = new Font("Century Gothic", 8F);
            lblDireitos.ForeColor = Color.Gray;
            lblDireitos.Location = new Point(30, 225);
            lblDireitos.Name = "lblDireitos";
            lblDireitos.Size = new Size(360, 20);
            lblDireitos.TabIndex = 5;
            lblDireitos.Text = "© 2026 Central do Educador — Todos os direitos reservados.";
            lblDireitos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmSobreNos
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(420, 310);
            Controls.Add(btnFechar);
            Controls.Add(lblDireitos);
            Controls.Add(lnkGitHub);
            Controls.Add(lblVersao);
            Controls.Add(lblDesenvolvedor);
            Controls.Add(lblDescricao);
            Controls.Add(panelTopo);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSobreNos";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sobre Nós";
            Load += FrmSobreNos_Load;
            panelTopo.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTopo;
        private Label lblTitulo;
        private Label lblDescricao;
        private Label lblDesenvolvedor;
        private Label lblVersao;
        private LinkLabel lnkGitHub;
        private Label lblDireitos;
        private Button btnFechar;
    }
}