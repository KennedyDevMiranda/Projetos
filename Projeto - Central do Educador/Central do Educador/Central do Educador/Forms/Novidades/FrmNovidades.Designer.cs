namespace Central_do_Educador.Forms
{
    partial class FrmNovidades
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
            pnlTopo = new Panel();
            lblUltimaAtualizacao = new Label();
            lblVersaoSistema = new Label();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            pnlFiltro = new Panel();
            btnAtualizar = new Button();
            btnLimparFiltro = new Button();
            txtPesquisa = new TextBox();
            lblPesquisa = new Label();
            lblContador = new Label();
            pnlConteudo = new Panel();
            grpDetalhes = new GroupBox();
            txtDetalhes = new TextBox();
            pnlInfo = new Panel();
            lblResumoValor = new Label();
            lblResumoTitulo = new Label();
            lblDataValor = new Label();
            lblDataTitulo = new Label();
            lblVersaoValor = new Label();
            lblVersaoTitulo = new Label();
            lblCategoriaValor = new Label();
            lblCategoriaTitulo = new Label();
            grpNovidades = new GroupBox();
            lstNovidades = new ListBox();
            pnlRodape = new Panel();
            btnFechar = new Button();
            pnlTopo.SuspendLayout();
            pnlFiltro.SuspendLayout();
            pnlConteudo.SuspendLayout();
            grpDetalhes.SuspendLayout();
            pnlInfo.SuspendLayout();
            grpNovidades.SuspendLayout();
            pnlRodape.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopo
            // 
            pnlTopo.BackColor = Color.FromArgb(33, 150, 243);
            pnlTopo.Controls.Add(lblUltimaAtualizacao);
            pnlTopo.Controls.Add(lblVersaoSistema);
            pnlTopo.Controls.Add(lblSubtitulo);
            pnlTopo.Controls.Add(lblTitulo);
            pnlTopo.Dock = DockStyle.Top;
            pnlTopo.Location = new Point(0, 0);
            pnlTopo.Name = "pnlTopo";
            pnlTopo.Size = new Size(1080, 110);
            pnlTopo.TabIndex = 0;
            // 
            // lblUltimaAtualizacao
            // 
            lblUltimaAtualizacao.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUltimaAtualizacao.AutoSize = true;
            lblUltimaAtualizacao.ForeColor = Color.White;
            lblUltimaAtualizacao.Location = new Point(834, 68);
            lblUltimaAtualizacao.Name = "lblUltimaAtualizacao";
            lblUltimaAtualizacao.Size = new Size(145, 17);
            lblUltimaAtualizacao.TabIndex = 3;
            lblUltimaAtualizacao.Text = "Última atualização: --";
            // 
            // lblVersaoSistema
            // 
            lblVersaoSistema.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblVersaoSistema.AutoSize = true;
            lblVersaoSistema.ForeColor = Color.White;
            lblVersaoSistema.Location = new Point(870, 40);
            lblVersaoSistema.Name = "lblVersaoSistema";
            lblVersaoSistema.Size = new Size(109, 17);
            lblVersaoSistema.TabIndex = 2;
            lblVersaoSistema.Text = "Versão atual: --";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = Color.White;
            lblSubtitulo.Location = new Point(24, 67);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(66, 17);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Subtítulo";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Century Gothic", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(20, 25);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(88, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Título";
            // 
            // pnlFiltro
            // 
            pnlFiltro.BackColor = Color.FromArgb(245, 245, 245);
            pnlFiltro.Controls.Add(btnAtualizar);
            pnlFiltro.Controls.Add(btnLimparFiltro);
            pnlFiltro.Controls.Add(txtPesquisa);
            pnlFiltro.Controls.Add(lblPesquisa);
            pnlFiltro.Controls.Add(lblContador);
            pnlFiltro.Dock = DockStyle.Top;
            pnlFiltro.Location = new Point(0, 110);
            pnlFiltro.Name = "pnlFiltro";
            pnlFiltro.Size = new Size(1080, 60);
            pnlFiltro.TabIndex = 1;
            // 
            // btnAtualizar
            // 
            btnAtualizar.Location = new Point(536, 16);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.Size = new Size(95, 28);
            btnAtualizar.TabIndex = 3;
            btnAtualizar.Text = "Atualizar";
            btnAtualizar.UseVisualStyleBackColor = true;
            btnAtualizar.Click += btnAtualizar_Click;
            // 
            // btnLimparFiltro
            // 
            btnLimparFiltro.Location = new Point(420, 16);
            btnLimparFiltro.Name = "btnLimparFiltro";
            btnLimparFiltro.Size = new Size(110, 28);
            btnLimparFiltro.TabIndex = 2;
            btnLimparFiltro.Text = "Limpar filtro";
            btnLimparFiltro.UseVisualStyleBackColor = true;
            btnLimparFiltro.Click += btnLimparFiltro_Click;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(90, 19);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.PlaceholderText = "Pesquisar novidade...";
            txtPesquisa.Size = new Size(315, 22);
            txtPesquisa.TabIndex = 1;
            txtPesquisa.TextChanged += txtPesquisa_TextChanged;
            // 
            // lblPesquisa
            // 
            lblPesquisa.AutoSize = true;
            lblPesquisa.Location = new Point(24, 22);
            lblPesquisa.Name = "lblPesquisa";
            lblPesquisa.Size = new Size(62, 17);
            lblPesquisa.TabIndex = 0;
            lblPesquisa.Text = "Pesquisar:";
            // 
            // lblContador
            // 
            lblContador.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblContador.AutoSize = true;
            lblContador.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblContador.Location = new Point(886, 22);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(129, 16);
            lblContador.TabIndex = 4;
            lblContador.Text = "0 novidades encontradas";
            // 
            // pnlConteudo
            // 
            pnlConteudo.Controls.Add(grpDetalhes);
            pnlConteudo.Controls.Add(grpNovidades);
            pnlConteudo.Dock = DockStyle.Fill;
            pnlConteudo.Location = new Point(0, 170);
            pnlConteudo.Name = "pnlConteudo";
            pnlConteudo.Padding = new Padding(12);
            pnlConteudo.Size = new Size(1080, 430);
            pnlConteudo.TabIndex = 2;
            // 
            // grpDetalhes
            // 
            grpDetalhes.Controls.Add(txtDetalhes);
            grpDetalhes.Controls.Add(pnlInfo);
            grpDetalhes.Dock = DockStyle.Fill;
            grpDetalhes.Location = new Point(402, 12);
            grpDetalhes.Name = "grpDetalhes";
            grpDetalhes.Padding = new Padding(12);
            grpDetalhes.Size = new Size(666, 406);
            grpDetalhes.TabIndex = 1;
            grpDetalhes.TabStop = false;
            grpDetalhes.Text = "Detalhes da atualização";
            // 
            // txtDetalhes
            // 
            txtDetalhes.BackColor = Color.White;
            txtDetalhes.BorderStyle = BorderStyle.FixedSingle;
            txtDetalhes.Dock = DockStyle.Fill;
            txtDetalhes.Location = new Point(12, 165);
            txtDetalhes.Multiline = true;
            txtDetalhes.Name = "txtDetalhes";
            txtDetalhes.ReadOnly = true;
            txtDetalhes.ScrollBars = ScrollBars.Vertical;
            txtDetalhes.Size = new Size(642, 229);
            txtDetalhes.TabIndex = 1;
            // 
            // pnlInfo
            // 
            pnlInfo.Controls.Add(lblResumoValor);
            pnlInfo.Controls.Add(lblResumoTitulo);
            pnlInfo.Controls.Add(lblDataValor);
            pnlInfo.Controls.Add(lblDataTitulo);
            pnlInfo.Controls.Add(lblVersaoValor);
            pnlInfo.Controls.Add(lblVersaoTitulo);
            pnlInfo.Controls.Add(lblCategoriaValor);
            pnlInfo.Controls.Add(lblCategoriaTitulo);
            pnlInfo.Dock = DockStyle.Top;
            pnlInfo.Location = new Point(12, 27);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new Size(642, 138);
            pnlInfo.TabIndex = 0;
            // 
            // lblResumoValor
            // 
            lblResumoValor.Location = new Point(93, 74);
            lblResumoValor.Name = "lblResumoValor";
            lblResumoValor.Size = new Size(531, 46);
            lblResumoValor.TabIndex = 7;
            lblResumoValor.Text = "--";
            // 
            // lblResumoTitulo
            // 
            lblResumoTitulo.AutoSize = true;
            lblResumoTitulo.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblResumoTitulo.Location = new Point(15, 74);
            lblResumoTitulo.Name = "lblResumoTitulo";
            lblResumoTitulo.Size = new Size(60, 16);
            lblResumoTitulo.TabIndex = 6;
            lblResumoTitulo.Text = "Resumo:";
            // 
            // lblDataValor
            // 
            lblDataValor.AutoSize = true;
            lblDataValor.Location = new Point(430, 28);
            lblDataValor.Name = "lblDataValor";
            lblDataValor.Size = new Size(18, 17);
            lblDataValor.TabIndex = 5;
            lblDataValor.Text = "--";
            // 
            // lblDataTitulo
            // 
            lblDataTitulo.AutoSize = true;
            lblDataTitulo.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblDataTitulo.Location = new Point(382, 28);
            lblDataTitulo.Name = "lblDataTitulo";
            lblDataTitulo.Size = new Size(40, 16);
            lblDataTitulo.TabIndex = 4;
            lblDataTitulo.Text = "Data:";
            // 
            // lblVersaoValor
            // 
            lblVersaoValor.AutoSize = true;
            lblVersaoValor.Location = new Point(267, 28);
            lblVersaoValor.Name = "lblVersaoValor";
            lblVersaoValor.Size = new Size(18, 17);
            lblVersaoValor.TabIndex = 3;
            lblVersaoValor.Text = "--";
            // 
            // lblVersaoTitulo
            // 
            lblVersaoTitulo.AutoSize = true;
            lblVersaoTitulo.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblVersaoTitulo.Location = new Point(204, 28);
            lblVersaoTitulo.Name = "lblVersaoTitulo";
            lblVersaoTitulo.Size = new Size(53, 16);
            lblVersaoTitulo.TabIndex = 2;
            lblVersaoTitulo.Text = "Versão:";
            // 
            // lblCategoriaValor
            // 
            lblCategoriaValor.AutoSize = true;
            lblCategoriaValor.Location = new Point(90, 28);
            lblCategoriaValor.Name = "lblCategoriaValor";
            lblCategoriaValor.Size = new Size(18, 17);
            lblCategoriaValor.TabIndex = 1;
            lblCategoriaValor.Text = "--";
            // 
            // lblCategoriaTitulo
            // 
            lblCategoriaTitulo.AutoSize = true;
            lblCategoriaTitulo.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            lblCategoriaTitulo.Location = new Point(15, 28);
            lblCategoriaTitulo.Name = "lblCategoriaTitulo";
            lblCategoriaTitulo.Size = new Size(69, 16);
            lblCategoriaTitulo.TabIndex = 0;
            lblCategoriaTitulo.Text = "Categoria:";
            // 
            // grpNovidades
            // 
            grpNovidades.Controls.Add(lstNovidades);
            grpNovidades.Dock = DockStyle.Left;
            grpNovidades.Location = new Point(12, 12);
            grpNovidades.Name = "grpNovidades";
            grpNovidades.Padding = new Padding(12);
            grpNovidades.Size = new Size(390, 406);
            grpNovidades.TabIndex = 0;
            grpNovidades.TabStop = false;
            grpNovidades.Text = "Linha do tempo de novidades";
            // 
            // lstNovidades
            // 
            lstNovidades.Dock = DockStyle.Fill;
            lstNovidades.FormattingEnabled = true;
            lstNovidades.ItemHeight = 17;
            lstNovidades.Location = new Point(12, 27);
            lstNovidades.Name = "lstNovidades";
            lstNovidades.Size = new Size(366, 367);
            lstNovidades.TabIndex = 0;
            lstNovidades.SelectedIndexChanged += lstNovidades_SelectedIndexChanged;
            // 
            // pnlRodape
            // 
            pnlRodape.Controls.Add(btnFechar);
            pnlRodape.Dock = DockStyle.Bottom;
            pnlRodape.Location = new Point(0, 600);
            pnlRodape.Name = "pnlRodape";
            pnlRodape.Size = new Size(1080, 55);
            pnlRodape.TabIndex = 3;
            // 
            // btnFechar
            // 
            btnFechar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFechar.Location = new Point(955, 12);
            btnFechar.Name = "btnFechar";
            btnFechar.Size = new Size(95, 30);
            btnFechar.TabIndex = 0;
            btnFechar.Text = "Fechar";
            btnFechar.UseVisualStyleBackColor = true;
            btnFechar.Click += btnFechar_Click;
            // 
            // FrmNovidades
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 655);
            Controls.Add(pnlConteudo);
            Controls.Add(pnlRodape);
            Controls.Add(pnlFiltro);
            Controls.Add(pnlTopo);
            Font = new Font("Century Gothic", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmNovidades";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Novidades";
            Load += FrmNovidades_Load;
            pnlTopo.ResumeLayout(false);
            pnlTopo.PerformLayout();
            pnlFiltro.ResumeLayout(false);
            pnlFiltro.PerformLayout();
            pnlConteudo.ResumeLayout(false);
            grpDetalhes.ResumeLayout(false);
            grpDetalhes.PerformLayout();
            pnlInfo.ResumeLayout(false);
            pnlInfo.PerformLayout();
            grpNovidades.ResumeLayout(false);
            pnlRodape.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTopo;
        private Label lblUltimaAtualizacao;
        private Label lblVersaoSistema;
        private Label lblSubtitulo;
        private Label lblTitulo;
        private Panel pnlFiltro;
        private Button btnAtualizar;
        private Button btnLimparFiltro;
        private TextBox txtPesquisa;
        private Label lblPesquisa;
        private Label lblContador;
        private Panel pnlConteudo;
        private GroupBox grpDetalhes;
        private TextBox txtDetalhes;
        private Panel pnlInfo;
        private Label lblResumoValor;
        private Label lblResumoTitulo;
        private Label lblDataValor;
        private Label lblDataTitulo;
        private Label lblVersaoValor;
        private Label lblVersaoTitulo;
        private Label lblCategoriaValor;
        private Label lblCategoriaTitulo;
        private GroupBox grpNovidades;
        private ListBox lstNovidades;
        private Panel pnlRodape;
        private Button btnFechar;
    }
}