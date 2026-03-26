using System.Windows.Forms.DataVisualization.Charting;

namespace Central_do_Educador
{
    partial class FrmDashBoard
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDashBoard));
            pnlCards = new FlowLayoutPanel();
            pnlCard1 = new Panel();
            label1 = new Label();
            lblTotalAlunos = new Label();
            pnlCard2 = new Panel();
            label4 = new Label();
            lblReceitaMensal = new Label();
            pnlCard3 = new Panel();
            label6 = new Label();
            lblPendencias = new Label();
            pnlCard4 = new Panel();
            label8 = new Label();
            lblNovosRegistos = new Label();
            chartAlunosMes = new Chart();
            chartStatusFinancas = new Chart();
            dgvAtividades = new DataGridView();
            Hora = new DataGridViewTextBoxColumn();
            Acao = new DataGridViewTextBoxColumn();
            Utilizador = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            label9 = new Label();
            menuStrip1 = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            sobreNosToolStripMenuItem = new ToolStripMenuItem();
            administradorToolStripMenuItem = new ToolStripMenuItem();
            controleDeTaxaToolStripMenuItem = new ToolStripMenuItem();
            registroToolStripMenuItem = new ToolStripMenuItem();
            agendamentoToolStripMenuItem = new ToolStripMenuItem();
            entregaDeLivroToolStripMenuItem = new ToolStripMenuItem();
            financeiroToolStripMenuItem1 = new ToolStripMenuItem();
            alunoToolStripMenuItem = new ToolStripMenuItem();
            chatBotToolStripMenuItem = new ToolStripMenuItem();
            relatórioToolStripMenuItem = new ToolStripMenuItem();
            pedagógicoToolStripMenuItem = new ToolStripMenuItem();
            financeiroToolStripMenuItem = new ToolStripMenuItem();
            taxaToolStripMenuItem = new ToolStripMenuItem();
            configuraçãoToolStripMenuItem = new ToolStripMenuItem();
            usuariosToolStripMenuItem = new ToolStripMenuItem();
            bancoDeDadToolStripMenuItem = new ToolStripMenuItem();
            reportarErroToolStripMenuItem = new ToolStripMenuItem();
            gerenciarErroToolStripMenuItem = new ToolStripMenuItem();
            permissaoToolStripMenuItem = new ToolStripMenuItem();
            sairToolStripMenuItem = new ToolStripMenuItem();
            logToolStripMenuItem = new ToolStripMenuItem();
            panel3 = new Panel();
            lblUsuario = new Label();
            toolTip1 = new ToolTip(components);
            pnlCards.SuspendLayout();
            pnlCard1.SuspendLayout();
            pnlCard2.SuspendLayout();
            pnlCard3.SuspendLayout();
            pnlCard4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartAlunosMes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartStatusFinancas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAtividades).BeginInit();
            menuStrip1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCards
            // 
            pnlCards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlCards.Controls.Add(pnlCard1);
            pnlCards.Controls.Add(pnlCard2);
            pnlCards.Controls.Add(pnlCard3);
            pnlCards.Controls.Add(pnlCard4);
            pnlCards.Location = new Point(20, 40);
            pnlCards.Margin = new Padding(0);
            pnlCards.Name = "pnlCards";
            pnlCards.Size = new Size(1220, 110);
            pnlCards.TabIndex = 0;
            pnlCards.WrapContents = false;
            // 
            // pnlCard1
            // 
            pnlCard1.BackColor = Color.FromArgb(52, 152, 219);
            pnlCard1.Controls.Add(label1);
            pnlCard1.Controls.Add(lblTotalAlunos);
            pnlCard1.Location = new Point(10, 10);
            pnlCard1.Margin = new Padding(10);
            pnlCard1.Name = "pnlCard1";
            pnlCard1.Size = new Size(280, 90);
            pnlCard1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(15, 12);
            label1.Name = "label1";
            label1.Size = new Size(110, 17);
            label1.TabIndex = 0;
            label1.Text = "Total de alunos";
            // 
            // lblTotalAlunos
            // 
            lblTotalAlunos.AutoSize = true;
            lblTotalAlunos.Font = new Font("Century Gothic", 20F, FontStyle.Bold);
            lblTotalAlunos.ForeColor = Color.White;
            lblTotalAlunos.Location = new Point(15, 38);
            lblTotalAlunos.Name = "lblTotalAlunos";
            lblTotalAlunos.Size = new Size(29, 32);
            lblTotalAlunos.TabIndex = 1;
            lblTotalAlunos.Text = "0";
            // 
            // pnlCard2
            // 
            pnlCard2.BackColor = Color.FromArgb(46, 204, 113);
            pnlCard2.Controls.Add(label4);
            pnlCard2.Controls.Add(lblReceitaMensal);
            pnlCard2.Location = new Point(310, 10);
            pnlCard2.Margin = new Padding(10);
            pnlCard2.Name = "pnlCard2";
            pnlCard2.Size = new Size(280, 90);
            pnlCard2.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(15, 12);
            label4.Name = "label4";
            label4.Size = new Size(115, 17);
            label4.TabIndex = 0;
            label4.Text = "Receita do mês";
            // 
            // lblReceitaMensal
            // 
            lblReceitaMensal.AutoSize = true;
            lblReceitaMensal.Font = new Font("Century Gothic", 20F, FontStyle.Bold);
            lblReceitaMensal.ForeColor = Color.White;
            lblReceitaMensal.Location = new Point(15, 38);
            lblReceitaMensal.Name = "lblReceitaMensal";
            lblReceitaMensal.Size = new Size(106, 32);
            lblReceitaMensal.TabIndex = 1;
            lblReceitaMensal.Text = "R$ 0,00";
            // 
            // pnlCard3
            // 
            pnlCard3.BackColor = Color.FromArgb(231, 76, 60);
            pnlCard3.Controls.Add(label6);
            pnlCard3.Controls.Add(lblPendencias);
            pnlCard3.Location = new Point(610, 10);
            pnlCard3.Margin = new Padding(10);
            pnlCard3.Name = "pnlCard3";
            pnlCard3.Size = new Size(280, 90);
            pnlCard3.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            label6.ForeColor = Color.White;
            label6.Location = new Point(15, 12);
            label6.Name = "label6";
            label6.Size = new Size(87, 17);
            label6.TabIndex = 0;
            label6.Text = "Pendências";
            // 
            // lblPendencias
            // 
            lblPendencias.AutoSize = true;
            lblPendencias.Font = new Font("Century Gothic", 20F, FontStyle.Bold);
            lblPendencias.ForeColor = Color.White;
            lblPendencias.Location = new Point(15, 38);
            lblPendencias.Name = "lblPendencias";
            lblPendencias.Size = new Size(29, 32);
            lblPendencias.TabIndex = 1;
            lblPendencias.Text = "0";
            // 
            // pnlCard4
            // 
            pnlCard4.BackColor = Color.FromArgb(155, 89, 182);
            pnlCard4.Controls.Add(label8);
            pnlCard4.Controls.Add(lblNovosRegistos);
            pnlCard4.Location = new Point(910, 10);
            pnlCard4.Margin = new Padding(10);
            pnlCard4.Name = "pnlCard4";
            pnlCard4.Size = new Size(280, 90);
            pnlCard4.TabIndex = 3;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            label8.ForeColor = Color.White;
            label8.Location = new Point(15, 12);
            label8.Name = "label8";
            label8.Size = new Size(108, 17);
            label8.TabIndex = 0;
            label8.Text = "Novos registros";
            // 
            // lblNovosRegistos
            // 
            lblNovosRegistos.AutoSize = true;
            lblNovosRegistos.Font = new Font("Century Gothic", 20F, FontStyle.Bold);
            lblNovosRegistos.ForeColor = Color.White;
            lblNovosRegistos.Location = new Point(15, 38);
            lblNovosRegistos.Name = "lblNovosRegistos";
            lblNovosRegistos.Size = new Size(25, 32);
            lblNovosRegistos.TabIndex = 1;
            lblNovosRegistos.Text = "-";
            // 
            // chartAlunosMes
            // 
            chartAlunosMes.BorderlineColor = Color.Silver;
            chartAlunosMes.BorderlineDashStyle = ChartDashStyle.Solid;
            chartAlunosMes.Location = new Point(20, 170);
            chartAlunosMes.Name = "chartAlunosMes";
            chartAlunosMes.Size = new Size(580, 220);
            chartAlunosMes.TabIndex = 1;
            // 
            // chartStatusFinancas
            // 
            chartStatusFinancas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            chartStatusFinancas.BorderlineColor = Color.Silver;
            chartStatusFinancas.BorderlineDashStyle = ChartDashStyle.Solid;
            chartStatusFinancas.Location = new Point(620, 170);
            chartStatusFinancas.Name = "chartStatusFinancas";
            chartStatusFinancas.Size = new Size(620, 220);
            chartStatusFinancas.TabIndex = 2;
            // 
            // dgvAtividades
            // 
            dgvAtividades.AllowUserToAddRows = false;
            dgvAtividades.AllowUserToDeleteRows = false;
            dgvAtividades.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAtividades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAtividades.BackgroundColor = Color.White;
            dgvAtividades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAtividades.Columns.AddRange(new DataGridViewColumn[] { Hora, Acao, Utilizador, Status });
            dgvAtividades.Location = new Point(20, 435);
            dgvAtividades.Name = "dgvAtividades";
            dgvAtividades.ReadOnly = true;
            dgvAtividades.RowHeadersVisible = false;
            dgvAtividades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAtividades.Size = new Size(1220, 170);
            dgvAtividades.TabIndex = 4;
            // 
            // Hora
            // 
            Hora.HeaderText = "Hora";
            Hora.Name = "Hora";
            Hora.ReadOnly = true;
            // 
            // Acao
            // 
            Acao.HeaderText = "Ação";
            Acao.Name = "Acao";
            Acao.ReadOnly = true;
            // 
            // Utilizador
            // 
            Utilizador.HeaderText = "Usuário";
            Utilizador.Name = "Utilizador";
            Utilizador.ReadOnly = true;
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.Name = "Status";
            Status.ReadOnly = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
            label9.Location = new Point(20, 405);
            label9.Name = "label9";
            label9.Size = new Size(153, 18);
            label9.TabIndex = 3;
            label9.Text = "Atividades recentes";
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Century Gothic", 9F);
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem, administradorToolStripMenuItem, relatórioToolStripMenuItem, configuraçãoToolStripMenuItem, sairToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1262, 25);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sobreNosToolStripMenuItem });
            arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            arquivoToolStripMenuItem.Size = new Size(67, 21);
            arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // sobreNosToolStripMenuItem
            // 
            sobreNosToolStripMenuItem.Name = "sobreNosToolStripMenuItem";
            sobreNosToolStripMenuItem.Size = new Size(134, 22);
            sobreNosToolStripMenuItem.Text = "Sobre nós";
            sobreNosToolStripMenuItem.Click += sobreNosToolStripMenuItem_Click;
            // 
            // administradorToolStripMenuItem
            // 
            administradorToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { controleDeTaxaToolStripMenuItem, registroToolStripMenuItem, agendamentoToolStripMenuItem, entregaDeLivroToolStripMenuItem, financeiroToolStripMenuItem1, alunoToolStripMenuItem, chatBotToolStripMenuItem });
            administradorToolStripMenuItem.Name = "administradorToolStripMenuItem";
            administradorToolStripMenuItem.Size = new Size(107, 21);
            administradorToolStripMenuItem.Text = "Administrativo";
            // 
            // controleDeTaxaToolStripMenuItem
            // 
            controleDeTaxaToolStripMenuItem.Name = "controleDeTaxaToolStripMenuItem";
            controleDeTaxaToolStripMenuItem.Size = new Size(177, 22);
            controleDeTaxaToolStripMenuItem.Text = "Controle de Taxa";
            controleDeTaxaToolStripMenuItem.Click += controleDeTaxaToolStripMenuItem_Click;
            // 
            // registroToolStripMenuItem
            // 
            registroToolStripMenuItem.Name = "registroToolStripMenuItem";
            registroToolStripMenuItem.Size = new Size(177, 22);
            registroToolStripMenuItem.Text = "Registro";
            registroToolStripMenuItem.Click += registroToolStripMenuItem_Click;
            // 
            // agendamentoToolStripMenuItem
            // 
            agendamentoToolStripMenuItem.Name = "agendamentoToolStripMenuItem";
            agendamentoToolStripMenuItem.Size = new Size(177, 22);
            agendamentoToolStripMenuItem.Text = "Agendamento";
            agendamentoToolStripMenuItem.Click += agendamentoToolStripMenuItem_Click;
            // 
            // entregaDeLivroToolStripMenuItem
            // 
            entregaDeLivroToolStripMenuItem.Name = "entregaDeLivroToolStripMenuItem";
            entregaDeLivroToolStripMenuItem.Size = new Size(177, 22);
            entregaDeLivroToolStripMenuItem.Text = "Entrega de Livro";
            entregaDeLivroToolStripMenuItem.Click += entregaDeLivrosToolStripMenuItem_Click;
            // 
            // financeiroToolStripMenuItem1
            // 
            financeiroToolStripMenuItem1.Name = "financeiroToolStripMenuItem1";
            financeiroToolStripMenuItem1.Size = new Size(177, 22);
            financeiroToolStripMenuItem1.Text = "Financeiro";
            financeiroToolStripMenuItem1.Click += financeiroToolStripMenuItem1_Click;
            // 
            // alunoToolStripMenuItem
            // 
            alunoToolStripMenuItem.Name = "alunoToolStripMenuItem";
            alunoToolStripMenuItem.Size = new Size(177, 22);
            alunoToolStripMenuItem.Text = "Aluno";
            alunoToolStripMenuItem.Click += alunoToolStripMenuItem_Click;
            // 
            // chatBotToolStripMenuItem
            // 
            chatBotToolStripMenuItem.Name = "chatBotToolStripMenuItem";
            chatBotToolStripMenuItem.Size = new Size(177, 22);
            chatBotToolStripMenuItem.Text = "ChatBot";
            chatBotToolStripMenuItem.Click += chatBotToolStripMenuItem_Click;
            // 
            // relatórioToolStripMenuItem
            // 
            relatórioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pedagógicoToolStripMenuItem, financeiroToolStripMenuItem, taxaToolStripMenuItem });
            relatórioToolStripMenuItem.Name = "relatórioToolStripMenuItem";
            relatórioToolStripMenuItem.Size = new Size(74, 21);
            relatórioToolStripMenuItem.Text = "Relatório";
            // 
            // pedagógicoToolStripMenuItem
            // 
            pedagógicoToolStripMenuItem.Name = "pedagógicoToolStripMenuItem";
            pedagógicoToolStripMenuItem.Size = new Size(150, 22);
            pedagógicoToolStripMenuItem.Text = "Pedagógico";
            pedagógicoToolStripMenuItem.Click += pedagógicoToolStripMenuItem_Click;
            // 
            // financeiroToolStripMenuItem
            // 
            financeiroToolStripMenuItem.Name = "financeiroToolStripMenuItem";
            financeiroToolStripMenuItem.Size = new Size(150, 22);
            financeiroToolStripMenuItem.Text = "Financeiro";
            financeiroToolStripMenuItem.Click += financeiroToolStripMenuItem_Click;
            // 
            // taxaToolStripMenuItem
            // 
            taxaToolStripMenuItem.Name = "taxaToolStripMenuItem";
            taxaToolStripMenuItem.Size = new Size(150, 22);
            taxaToolStripMenuItem.Text = "Taxa";
            // 
            // configuraçãoToolStripMenuItem
            // 
            configuraçãoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { usuariosToolStripMenuItem, bancoDeDadToolStripMenuItem, reportarErroToolStripMenuItem, gerenciarErroToolStripMenuItem, permissaoToolStripMenuItem });
            configuraçãoToolStripMenuItem.Name = "configuraçãoToolStripMenuItem";
            configuraçãoToolStripMenuItem.Size = new Size(102, 21);
            configuraçãoToolStripMenuItem.Text = "Configuração";
            // 
            // usuariosToolStripMenuItem
            // 
            usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            usuariosToolStripMenuItem.Size = new Size(173, 22);
            usuariosToolStripMenuItem.Text = "Usuários";
            usuariosToolStripMenuItem.Click += usuariosToolStripMenuItem_Click;
            // 
            // bancoDeDadToolStripMenuItem
            // 
            bancoDeDadToolStripMenuItem.Name = "bancoDeDadToolStripMenuItem";
            bancoDeDadToolStripMenuItem.Size = new Size(173, 22);
            bancoDeDadToolStripMenuItem.Text = "Banco de dados";
            bancoDeDadToolStripMenuItem.Click += bancoDeDadToolStripMenuItem_Click;
            // 
            // reportarErroToolStripMenuItem
            // 
            reportarErroToolStripMenuItem.Name = "reportarErroToolStripMenuItem";
            reportarErroToolStripMenuItem.Size = new Size(173, 22);
            reportarErroToolStripMenuItem.Text = "Reportar Erro";
            reportarErroToolStripMenuItem.Click += reportarErroToolStripMenuItem_Click;
            // 
            // gerenciarErroToolStripMenuItem
            // 
            gerenciarErroToolStripMenuItem.Name = "gerenciarErroToolStripMenuItem";
            gerenciarErroToolStripMenuItem.Size = new Size(173, 22);
            gerenciarErroToolStripMenuItem.Text = "Gerenciar Erro";
            gerenciarErroToolStripMenuItem.Click += gerenciarErroToolStripMenuItem_Click;
            // 
            // permissaoToolStripMenuItem
            // 
            permissaoToolStripMenuItem.Name = "permissaoToolStripMenuItem";
            permissaoToolStripMenuItem.Size = new Size(173, 22);
            permissaoToolStripMenuItem.Text = "Permissão";
            permissaoToolStripMenuItem.Click += permissaoToolStripMenuItem_Click;
            // 
            // sairToolStripMenuItem
            // 
            sairToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { logToolStripMenuItem });
            sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            sairToolStripMenuItem.Size = new Size(42, 21);
            sairToolStripMenuItem.Text = "Sair";
            // 
            // logToolStripMenuItem
            // 
            logToolStripMenuItem.Name = "logToolStripMenuItem";
            logToolStripMenuItem.Size = new Size(118, 22);
            logToolStripMenuItem.Text = "Logout";
            logToolStripMenuItem.Click += logToolStripMenuItem_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblUsuario);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 629);
            panel3.Name = "panel3";
            panel3.Size = new Size(1262, 27);
            panel3.TabIndex = 5;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(8, 5);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(12, 17);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "-";
            // 
            // FrmDashBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            ClientSize = new Size(1262, 656);
            Controls.Add(panel3);
            Controls.Add(dgvAtividades);
            Controls.Add(label9);
            Controls.Add(chartStatusFinancas);
            Controls.Add(chartAlunosMes);
            Controls.Add(pnlCards);
            Controls.Add(menuStrip1);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "FrmDashBoard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Central do Educador - 0.0.1.9";
            Activated += FrmDashBoard_Activated;
            Load += FrmDashBord_Load;
            pnlCards.ResumeLayout(false);
            pnlCard1.ResumeLayout(false);
            pnlCard1.PerformLayout();
            pnlCard2.ResumeLayout(false);
            pnlCard2.PerformLayout();
            pnlCard3.ResumeLayout(false);
            pnlCard3.PerformLayout();
            pnlCard4.ResumeLayout(false);
            pnlCard4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartAlunosMes).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartStatusFinancas).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAtividades).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel pnlCards;
        private Panel pnlCard1;
        private Label label1;
        private Label lblTotalAlunos;
        private Panel pnlCard2;
        private Label label4;
        private Label lblReceitaMensal;
        private Panel pnlCard3;
        private Label label6;
        private Label lblPendencias;
        private Panel pnlCard4;
        private Label label8;
        private Label lblNovosRegistos;
        private Chart chartAlunosMes;
        private Chart chartStatusFinancas;
        private DataGridView dgvAtividades;
        private DataGridViewTextBoxColumn Hora;
        private DataGridViewTextBoxColumn Acao;
        private DataGridViewTextBoxColumn Utilizador;
        private DataGridViewTextBoxColumn Status;
        private Label label9;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem sobreNosToolStripMenuItem;
        private ToolStripMenuItem administradorToolStripMenuItem;
        private ToolStripMenuItem controleDeTaxaToolStripMenuItem;
        private ToolStripMenuItem registroToolStripMenuItem;
        private ToolStripMenuItem agendamentoToolStripMenuItem;
        private ToolStripMenuItem entregaDeLivroToolStripMenuItem;
        private ToolStripMenuItem financeiroToolStripMenuItem1;
        private ToolStripMenuItem alunoToolStripMenuItem;
        private ToolStripMenuItem chatBotToolStripMenuItem;
        private ToolStripMenuItem relatórioToolStripMenuItem;
        private ToolStripMenuItem pedagógicoToolStripMenuItem;
        private ToolStripMenuItem financeiroToolStripMenuItem;
        private ToolStripMenuItem taxaToolStripMenuItem;
        private ToolStripMenuItem configuraçãoToolStripMenuItem;
        private ToolStripMenuItem usuariosToolStripMenuItem;
        private ToolStripMenuItem bancoDeDadToolStripMenuItem;
        private ToolStripMenuItem reportarErroToolStripMenuItem;
        private ToolStripMenuItem gerenciarErroToolStripMenuItem;
        private ToolStripMenuItem permissaoToolStripMenuItem;
        private ToolStripMenuItem sairToolStripMenuItem;
        private ToolStripMenuItem logToolStripMenuItem;
        private Panel panel3;
        private Label lblUsuario;
        private ToolTip toolTip1;
    }
}