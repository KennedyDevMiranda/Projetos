using Central_do_Educador.Data;
using Central_do_Educador.Models;
using Central_do_Educador.Services;
using Microsoft.Data.Sqlite;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmFinanceiro : Form
    {
        private readonly CultureInfo _ptBR = new("pt-BR");
        private long _contratoIdFiltro;
        int enviadosDriver = 0;
        int falhasDriver = 0;
        private readonly Random _rnd = new Random();
        private System.Windows.Forms.Timer _timerFinanceiro;
        private bool _processamentoFinanceiroEmAndamento = false;
        private DateTime? _ultimaExecucaoFinanceira = null;
        //private System.Windows.Forms.Button btnEnviarFiltrados;

        public FrmFinanceiro()
        {
            InitializeComponent();
        }

        // ══════════════════════════════════════════════
        // LOAD
        // ══════════════════════════════════════════════

        private async void FrmFinanceiro_Load(object sender, EventArgs e)
        {
            FormatarGrid(dgvContratos);
            FormatarGrid(dgvParcelas);
            dgvParcelas.CellFormatting += dgvParcelas_CellFormatting;

            ConfigurarCombos();
            ConfigurarDashboard();

            await FinanceiroService.AtualizarParcelasVencidasAsync();
            await CarregarDashboardAsync();
            await CarregarContratosAsync();
            await CarregarConfigAsync();

            InicializarTimerFinanceiro();
        }

        private void InicializarTimerFinanceiro()
        {
            _timerFinanceiro = new System.Windows.Forms.Timer();
            _timerFinanceiro.Interval = 30000; // 30 segundos
            _timerFinanceiro.Tick += async (s, e) => await VerificarEnvioAutomaticoFinanceiroAsync();
            _timerFinanceiro.Start();
        }

        private async Task VerificarEnvioAutomaticoFinanceiroAsync()
        {
            if (_processamentoFinanceiroEmAndamento)
                return;

            try
            {
                var cfg = await FinanceiroService.ObterConfigAsync();

                if (cfg == null || !cfg.Ativo)
                    return;

                if (string.IsNullOrWhiteSpace(cfg.HorarioEnvio))
                    return;

                if (!TimeSpan.TryParse(cfg.HorarioEnvio, out TimeSpan horarioConfigurado))
                    return;

                DateTime agora = DateTime.Now;

                if (agora.Hour != horarioConfigurado.Hours || agora.Minute != horarioConfigurado.Minutes)
                    return;

                if (_ultimaExecucaoFinanceira.HasValue &&
                    _ultimaExecucaoFinanceira.Value.Date == agora.Date &&
                    _ultimaExecucaoFinanceira.Value.Hour == agora.Hour &&
                    _ultimaExecucaoFinanceira.Value.Minute == agora.Minute)
                    return;

                await ExecutarProcessamentoFinanceiroAsync(false);
                _ultimaExecucaoFinanceira = agora;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FINANCEIRO AUTO] Erro: {ex.Message}");
            }
        }

        private async Task ExecutarProcessamentoFinanceiroAsync(bool execucaoManual)
        {
            if (_processamentoFinanceiroEmAndamento)
                return;

            try
            {
                _processamentoFinanceiroEmAndamento = true;

                if (execucaoManual)
                {
                    txtResultado.Clear();
                    txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] Iniciando processamento manual...\r\n");
                }

                var resultado = await NotificacaoFinanceiraService.ProcessarAsync();

                if (execucaoManual)
                {
                    txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] Lembretes: {resultado.LembretesGerados}\r\n");
                    txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] Cobranças: {resultado.CobrancasGeradas}\r\n");
                    txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] E-mails: {resultado.EmailsEnviados}/{resultado.EmailsFalhas}\r\n");
                    txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] WhatsApp: {resultado.WhatsAppEnviados}/{resultado.WhatsAppFalhas}\r\n");
                    txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] Concluído!\r\n");
                }

                lblStatusBar.Text =
                    $"Lembretes: {resultado.LembretesGerados} | Cobranças: {resultado.CobrancasGeradas} | WhatsApp: {resultado.WhatsAppEnviados}";
            }
            finally
            {
                _processamentoFinanceiroEmAndamento = false;
            }
        }

        private async void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabContratos)
                await CarregarContratosAsync();
            else if (tabControl.SelectedTab == tabParcelas)
                await CarregarParcelasAsync();
        }

        // ══════════════════════════════════════════════
        // CONFIGURAÇÃO INICIAL
        // ══════════════════════════════════════════════

        private void ConfigurarCombos()
        {
            if (cmbStatusContrato.Items.Count == 0)
                cmbStatusContrato.Items.AddRange(new object[] { "Todos", "ATIVO", "CONCLUIDO", "CANCELADO" });

            if (cmbStatusParcela.Items.Count == 0)
                cmbStatusParcela.Items.AddRange(new object[] { "Todos", "PENDENTE", "VENCIDA", "PAGA", "CANCELADA" });

            if (cmbStatusContrato.SelectedIndex < 0)
                cmbStatusContrato.SelectedIndex = 0;

            if (cmbStatusParcela.SelectedIndex < 0)
                cmbStatusParcela.SelectedIndex = 0;
        }

        private void ConfigurarDashboard()
        {
            lblCard1Title.Text = "CONTRATOS ATIVOS";
            lblCard1Value.Text = "0";

            lblCard2Title.Text = "TOTAL RECEBIDO";
            lblCard2Value.Text = "R$ 0,00";

            lblCard3Title.Text = "TOTAL PENDENTE";
            lblCard3Value.Text = "R$ 0,00";

            lblCard4Title.Text = "TOTAL VENCIDO";
            lblCard4Value.Text = "R$ 0,00";

            lblCard5Title.Text = "A VENCER (7 DIAS)";
            lblCard5Value.Text = "0";
        }

        // ══════════════════════════════════════════════
        // DASHBOARD
        // ══════════════════════════════════════════════

        private async Task CarregarDashboardAsync()
        {
            try
            {
                var r = await FinanceiroService.ObterResumoAsync();
                lblCard1Value.Text = r.ContratosAtivos.ToString();
                lblCard2Value.Text = r.TotalRecebido.ToString("C2", _ptBR);
                lblCard3Value.Text = r.TotalPendente.ToString("C2", _ptBR);
                lblCard4Value.Text = r.TotalVencido.ToString("C2", _ptBR);
                lblCard5Value.Text = r.ParcelasAVencer.ToString();
            }
            catch (Exception ex)
            {
                lblStatusBar.Text = $"Erro ao carregar resumo: {ex.Message}";
            }
        }

        // ══════════════════════════════════════════════
        // CONTRATOS
        // ══════════════════════════════════════════════

        private async Task CarregarContratosAsync()
        {
            try
            {
                string? status = cmbStatusContrato.SelectedItem?.ToString();
                if (status == "Todos") status = null;

                var lista = await FinanceiroService.ListarContratosAsync(status);

                string pesquisa = txtPesquisaContrato.Text.Trim();
                if (!string.IsNullOrEmpty(pesquisa))
                {
                    lista = lista.Where(c =>
                        c.NomeAluno.Contains(pesquisa, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                var dt = new DataTable();
                dt.Columns.Add("Id", typeof(long));
                dt.Columns.Add("Aluno");
                dt.Columns.Add("Descrição");
                dt.Columns.Add("Valor Total");
                dt.Columns.Add("Parcelas", typeof(int));
                dt.Columns.Add("Dia Venc.", typeof(int));
                dt.Columns.Add("Status");
                dt.Columns.Add("Início");

                foreach (var c in lista)
                {
                    string inicio = DateTime.TryParse(c.DataInicio, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var di)
                        ? di.ToString("dd/MM/yyyy")
                        : c.DataInicio;

                    dt.Rows.Add(
                        c.Id,
                        c.NomeAluno,
                        c.Descricao,
                        c.ValorTotal.ToString("N2", _ptBR),
                        c.QuantidadeParcelas,
                        c.DiaVencimento,
                        c.Status,
                        inicio);
                }

                dgvContratos.DataSource = dt;

                if (dgvContratos.Columns.Contains("Id"))
                    dgvContratos.Columns["Id"]!.Visible = false;

                lblTotalRegistros.Text = $"Contratos: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar contratos: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnNovoContrato_Click(object sender, EventArgs e)
        {
            using var dlg = CriarDialogoContrato();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var contrato = (FinanceiroService.Contrato)dlg.Tag!;
                await FinanceiroService.InserirContratoAsync(contrato);
                lblStatusBar.Text = "Contrato criado com sucesso!";
                await CarregarContratosAsync();
                await CarregarDashboardAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar contrato: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExcluirContrato_Click(object sender, EventArgs e)
        {
            long id = ObterIdSelecionado(dgvContratos);
            if (id <= 0)
            {
                MessageBox.Show("Selecione um contrato.");
                return;
            }

            if (MessageBox.Show("Excluir este contrato e todas as suas parcelas?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                await FinanceiroService.ExcluirContratoAsync(id);
                lblStatusBar.Text = "Contrato excluído.";
                await CarregarContratosAsync();
                await CarregarDashboardAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnVerParcelas_Click(object sender, EventArgs e)
        {
            long id = ObterIdSelecionado(dgvContratos);
            if (id <= 0)
            {
                MessageBox.Show("Selecione um contrato.");
                return;
            }

            _contratoIdFiltro = id;
            tabControl.SelectedTab = tabParcelas;
            await CarregarParcelasAsync();
        }

        private async void cmbFiltroContrato_Changed(object? sender, EventArgs e)
            => await CarregarContratosAsync();

        private async void btnAtualizarContratos_Click(object? sender, EventArgs e)
            => await CarregarContratosAsync();

        // ══════════════════════════════════════════════
        // PARCELAS
        // ══════════════════════════════════════════════

        private async Task CarregarParcelasAsync()
        {
            try
            {
                string? status = cmbStatusParcela.SelectedItem?.ToString();
                if (status == "Todos") status = null;

                long? contratoId = _contratoIdFiltro > 0 ? _contratoIdFiltro : null;
                var lista = await FinanceiroService.ListarParcelasAsync(contratoId, filtroStatus: status);

                string pesquisa = txtPesquisaParcela.Text.Trim();
                if (!string.IsNullOrEmpty(pesquisa))
                {
                    lista = lista.Where(p =>
                        p.NomeAluno.Contains(pesquisa, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                var dt = new DataTable();
                dt.Columns.Add("Id", typeof(long));
                dt.Columns.Add("Aluno");
                dt.Columns.Add("Parcela");
                dt.Columns.Add("Valor");
                dt.Columns.Add("Vencimento");
                dt.Columns.Add("Status");
                dt.Columns.Add("Pagamento");
                dt.Columns.Add("Valor Pago");
                dt.Columns.Add("Forma Pgto");
                dt.Columns.Add("Contrato");

                foreach (var p in lista)
                {
                    string venc = DateTime.TryParse(p.DataVencimento, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var dv)
                        ? dv.ToString("dd/MM/yyyy")
                        : p.DataVencimento;

                    string? pgto = DateTime.TryParse(p.DataPagamento, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var dp)
                        ? dp.ToString("dd/MM/yyyy")
                        : p.DataPagamento;

                    dt.Rows.Add(
                        p.Id,
                        p.NomeAluno,
                        $"{p.NumeroParcela}/{p.TotalParcelas}",
                        p.ValorParcela.ToString("N2", _ptBR),
                        venc,
                        p.Status,
                        pgto ?? "—",
                        p.ValorPago?.ToString("N2", _ptBR) ?? "—",
                        p.FormaPagamento ?? "—",
                        p.DescricaoContrato ?? "");
                }

                dgvParcelas.DataSource = dt;

                if (dgvParcelas.Columns.Contains("Id"))
                    dgvParcelas.Columns["Id"]!.Visible = false;

                lblTotalRegistros.Text = $"Parcelas: {dt.Rows.Count}";
                _contratoIdFiltro = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar parcelas: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRegistrarPgto_Click(object sender, EventArgs e)
        {
            long id = ObterIdSelecionado(dgvParcelas);
            if (id <= 0)
            {
                MessageBox.Show("Selecione uma parcela.");
                return;
            }

            var row = dgvParcelas.SelectedRows[0];
            string statusAtual = row.Cells["Status"].Value?.ToString() ?? "";
            if (statusAtual == "PAGA")
            {
                MessageBox.Show("Esta parcela já está paga.");
                return;
            }

            string valorStr = row.Cells["Valor"].Value?.ToString() ?? "0";

            using var dlg = CriarDialogoPagamento(valorStr);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var dados = ((decimal valor, string forma, string? obs))dlg.Tag!;
                await FinanceiroService.RegistrarPagamentoAsync(id, dados.valor, dados.forma, dados.obs);
                await NotificacaoFinanceiraService.GerarConfirmacaoPagamentoAsync(id);
                lblStatusBar.Text = "Pagamento registrado!";
                await CarregarParcelasAsync();
                await CarregarDashboardAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEstornar_Click(object sender, EventArgs e)
        {
            long id = ObterIdSelecionado(dgvParcelas);
            if (id <= 0)
            {
                MessageBox.Show("Selecione uma parcela.");
                return;
            }

            var row = dgvParcelas.SelectedRows[0];
            if (row.Cells["Status"].Value?.ToString() != "PAGA")
            {
                MessageBox.Show("Só é possível estornar parcelas pagas.");
                return;
            }

            if (MessageBox.Show("Estornar o pagamento desta parcela?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                await FinanceiroService.EstornarPagamentoAsync(id);
                lblStatusBar.Text = "Pagamento estornado.";
                await CarregarParcelasAsync();
                await CarregarDashboardAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnConfirmacaoPgto_Click(object sender, EventArgs e)
        {
            long id = ObterIdSelecionado(dgvParcelas);
            if (id <= 0)
            {
                MessageBox.Show("Selecione uma parcela paga.");
                return;
            }

            try
            {
                int geradas = await NotificacaoFinanceiraService.GerarConfirmacaoPagamentoAsync(id);
                lblStatusBar.Text = geradas > 0
                    ? $"{geradas} notificação(ões) de confirmação gerada(s)."
                    : "Nenhuma notificação gerada (verifique templates/contatos).";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnProcessarNotif_Click(object sender, EventArgs e)
        {
            long id = ObterIdSelecionado(dgvParcelas);
            if (id <= 0)
            {
                MessageBox.Show("Selecione uma parcela.");
                return;
            }

            btnProcessarNotif.Enabled = false;
            lblStatusBar.Text = "Processando notificação da parcela...";

            try
            {
                var row = dgvParcelas.SelectedRows[0];

                string aluno = row.Cells["Aluno"].Value?.ToString() ?? "";
                string parcela = row.Cells["Parcela"].Value?.ToString() ?? "";
                string valor = row.Cells["Valor"].Value?.ToString() ?? "0,00";
                string vencimento = row.Cells["Vencimento"].Value?.ToString() ?? "";
                string status = row.Cells["Status"].Value?.ToString() ?? "";

                var contato = await BuscarContatosParcelaAsync(id);

                bool mesmaPessoa = MesmoNome(contato.nomeAluno, contato.nomeResp);

                string msgAluno =
                    $"Olá, {contato.nomeAluno}! " +
                    $"Sua parcela {parcela}, no valor de R$ {valor}, com vencimento em {vencimento}, está com {status}.";

                string msgResp = mesmaPessoa
                    ? msgAluno
                    : $"Olá, {contato.nomeResp}! " +
                      $"A parcela de {contato.nomeAluno} ({parcela}), no valor de R$ {valor}, com vencimento em {vencimento}, está com {status}.";

                var r = await EnviarWhatsAppFinanceiroAsync(
                    contato.telAluno,
                    contato.telResp,
                    contato.nomeAluno,
                    contato.nomeResp,
                    msgAluno,
                    msgResp);

                lblStatusBar.Text = $"WhatsApp enviado: {r.enviados} ✓ | Falhas: {r.falhas} ✗";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnProcessarNotif.Enabled = true;
            }
        }

        private async void cmbFiltroParcela_Changed(object? sender, EventArgs e)
            => await CarregarParcelasAsync();

        private async void btnAtualizarParcelas_Click(object? sender, EventArgs e)
            => await CarregarParcelasAsync();

        // ══════════════════════════════════════════════
        // CONFIGURAÇÕES
        // ══════════════════════════════════════════════

        private async Task CarregarConfigAsync()
        {
            try
            {
                var cfg = await FinanceiroService.ObterConfigAsync();
                nudDiasLembrete.Value = cfg.DiasAntecedenciaLembrete;
                nudDiasCobranca.Value = cfg.DiasAtrasoCobranca;
                chkEnviarEmail.Checked = cfg.EnviarEmail;
                chkEnviarWhatsapp.Checked = cfg.EnviarWhatsapp;
                txtHorarioEnvio.Text = cfg.HorarioEnvio;
                chkFinanceiroAtivo.Checked = cfg.Ativo;
            }
            catch (Exception ex)
            {
                lblStatusBar.Text = $"Erro ao carregar config: {ex.Message}";
            }
        }

        private async void btnSalvarConfig_Click(object sender, EventArgs e)
        {
            try
            {
                var cfg = new FinanceiroService.ConfiguracaoFinanceiro
                {
                    DiasAntecedenciaLembrete = (int)nudDiasLembrete.Value,
                    DiasAtrasoCobranca = (int)nudDiasCobranca.Value,
                    EnviarEmail = chkEnviarEmail.Checked,
                    EnviarWhatsapp = chkEnviarWhatsapp.Checked,
                    HorarioEnvio = txtHorarioEnvio.Text.Trim(),
                    Ativo = chkFinanceiroAtivo.Checked,
                };

                await FinanceiroService.SalvarConfigAsync(cfg);
                MessageBox.Show("Configuração salva!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExecutarNotif_Click(object sender, EventArgs e)
        {
            long id = ObterIdSelecionado(dgvParcelas);
            if (id <= 0)
            {
                MessageBox.Show("Selecione uma parcela na aba Parcelas antes de executar.");
                return;
            }

            btnExecutarNotif.Enabled = false;
            txtResultado.Clear();
            LogFinanceiro("Iniciando disparo manual...");
            // txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] Iniciando processamento...\r\n");

            try
            {
                var row = dgvParcelas.SelectedRows[0];

                string parcela = row.Cells["Parcela"].Value?.ToString() ?? "";
                string valor = row.Cells["Valor"].Value?.ToString() ?? "0,00";
                string vencimento = row.Cells["Vencimento"].Value?.ToString() ?? "";
                string status = row.Cells["Status"].Value?.ToString() ?? "";

                var contato = await BuscarContatosParcelaAsync(id);
                bool mesmaPessoa = MesmoNome(contato.nomeAluno, contato.nomeResp);

                string msgAluno =
                    $"Olá, {contato.nomeAluno}! " +
                    $"Sua parcela {parcela}, no valor de R$ {valor}, com vencimento em {vencimento}, está com {status}.";

                string msgResp = mesmaPessoa
                    ? msgAluno
                    : $"Olá, {contato.nomeResp}! " +
                      $"A parcela de {contato.nomeAluno} ({parcela}), no valor de R$ {valor}, com vencimento em {vencimento}, está com {status}.";
                var r = await EnviarWhatsAppFinanceiroAsync(
                    contato.telAluno,
                    contato.telResp,
                    contato.nomeAluno,
                    contato.nomeResp,
                    msgAluno,
                    msgResp,
                    msg => txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n"));

                txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] Concluído!\r\n");
                txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] WhatsApp: {r.enviados} enviados, {r.falhas} falhas\r\n");

                lblStatusBar.Text = $"WhatsApp: {r.enviados} ✓ {r.falhas} ✗";
            }
            catch (Exception ex)
            {
                txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] ❌ Erro: {ex.Message}\r\n");
            }
            finally
            {
                btnExecutarNotif.Enabled = true;
            }
        }

        private void LogFinanceiro(string mensagem)
        {
            if (txtResultado.InvokeRequired)
            {
                txtResultado.Invoke(new Action(() => LogFinanceiro(mensagem)));
                return;
            }

            txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] {mensagem}\r\n");
            txtResultado.SelectionStart = txtResultado.Text.Length;
            txtResultado.ScrollToCaret();
        }

        // ══════════════════════════════════════════════
        // FORMATAÇÃO DOS GRIDS
        // ══════════════════════════════════════════════

        private static void FormatarGrid(DataGridView g)
        {
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.ReadOnly = true;
            g.MultiSelect = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.RowHeadersVisible = false;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 32;
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            g.DefaultCellStyle.Font = new Font("Century Gothic", 10F);
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void dgvParcelas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvParcelas.Columns[e.ColumnIndex].Name != "Status" || e.Value == null)
                return;

            string status = e.Value.ToString() ?? "";
            var row = dgvParcelas.Rows[e.RowIndex];

            switch (status)
            {
                case "PAGA":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233);
                    e.CellStyle!.ForeColor = Color.FromArgb(27, 94, 32);
                    e.CellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
                    break;

                case "VENCIDA":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238);
                    e.CellStyle!.ForeColor = Color.FromArgb(183, 28, 28);
                    e.CellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
                    break;

                case "PENDENTE":
                    e.CellStyle!.ForeColor = Color.FromArgb(230, 126, 0);
                    break;
            }
        }

        // ══════════════════════════════════════════════
        // DIÁLOGO — NOVO CONTRATO
        // ══════════════════════════════════════════════

        private Form CriarDialogoContrato()
        {
            var frm = new Form
            {
                Text = "Novo Contrato",
                Size = new Size(460, 420),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                Font = new Font("Century Gothic", 9F),
            };

            int y = 16;
            const int lblX = 16, ctrlX = 180, ctrlW = 240;

            var lblAluno = new Label { Text = "Aluno:", Location = new Point(lblX, y + 4), AutoSize = true };
            var cmbAluno = new ComboBox
            {
                Location = new Point(ctrlX, y),
                Size = new Size(ctrlW, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            CarregarAlunosCombo(cmbAluno);
            frm.Controls.Add(lblAluno);
            frm.Controls.Add(cmbAluno);

            y += 36;
            var lblDesc = new Label { Text = "Descrição:", Location = new Point(lblX, y + 4), AutoSize = true };
            var txtDesc = new TextBox { Location = new Point(ctrlX, y), Size = new Size(ctrlW, 25) };
            frm.Controls.Add(lblDesc);
            frm.Controls.Add(txtDesc);

            y += 36;
            var lblValor = new Label { Text = "Valor Total (R$):", Location = new Point(lblX, y + 4), AutoSize = true };
            var txtValor = new TextBox { Location = new Point(ctrlX, y), Size = new Size(ctrlW, 25), Text = "0,00" };
            frm.Controls.Add(lblValor);
            frm.Controls.Add(txtValor);

            y += 36;
            var lblQtd = new Label { Text = "Qtd. Parcelas:", Location = new Point(lblX, y + 4), AutoSize = true };
            var nudQtd = new NumericUpDown
            {
                Location = new Point(ctrlX, y),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 120,
                Value = 1
            };
            frm.Controls.Add(lblQtd);
            frm.Controls.Add(nudQtd);

            y += 36;
            var lblDia = new Label { Text = "Dia Vencimento:", Location = new Point(lblX, y + 4), AutoSize = true };
            var nudDia = new NumericUpDown
            {
                Location = new Point(ctrlX, y),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 31,
                Value = 10
            };
            frm.Controls.Add(lblDia);
            frm.Controls.Add(nudDia);

            y += 36;
            var lblInicio = new Label { Text = "Data Início:", Location = new Point(lblX, y + 4), AutoSize = true };
            var dtpInicio = new DateTimePicker
            {
                Location = new Point(ctrlX, y),
                Size = new Size(ctrlW, 25),
                Format = DateTimePickerFormat.Short
            };
            frm.Controls.Add(lblInicio);
            frm.Controls.Add(dtpInicio);

            y += 36;
            var lblObs = new Label { Text = "Observação:", Location = new Point(lblX, y + 4), AutoSize = true };
            var txtObs = new TextBox
            {
                Location = new Point(ctrlX, y),
                Size = new Size(ctrlW, 50),
                Multiline = true
            };
            frm.Controls.Add(lblObs);
            frm.Controls.Add(txtObs);

            y += 64;
            var btnOk = new Button
            {
                Text = "Salvar",
                DialogResult = DialogResult.OK,
                Location = new Point(ctrlX, y),
                Size = new Size(100, 32)
            };
            var btnCancel = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Location = new Point(ctrlX + 110, y),
                Size = new Size(100, 32)
            };
            frm.Controls.Add(btnOk);
            frm.Controls.Add(btnCancel);
            frm.AcceptButton = btnOk;
            frm.CancelButton = btnCancel;

            btnOk.Click += (s, e) =>
            {
                if (cmbAluno.SelectedValue == null)
                {
                    MessageBox.Show("Selecione um aluno.");
                    frm.DialogResult = DialogResult.None;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDesc.Text))
                {
                    MessageBox.Show("Informe a descrição.");
                    frm.DialogResult = DialogResult.None;
                    return;
                }

                if (!decimal.TryParse(txtValor.Text, NumberStyles.Any, _ptBR, out decimal valor) || valor <= 0)
                {
                    MessageBox.Show("Valor inválido.");
                    frm.DialogResult = DialogResult.None;
                    return;
                }

                frm.Tag = new FinanceiroService.Contrato
                {
                    AlunoId = Convert.ToInt64(cmbAluno.SelectedValue),
                    Descricao = txtDesc.Text.Trim(),
                    ValorTotal = valor,
                    QuantidadeParcelas = (int)nudQtd.Value,
                    DiaVencimento = (int)nudDia.Value,
                    DataInicio = dtpInicio.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Observacao = txtObs.Text.Trim(),
                };
            };

            return frm;
        }

        // ══════════════════════════════════════════════
        // DIÁLOGO — REGISTRAR PAGAMENTO
        // ══════════════════════════════════════════════

        private Form CriarDialogoPagamento(string valorSugerido)
        {
            var frm = new Form
            {
                Text = "Registrar Pagamento",
                Size = new Size(400, 280),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                Font = new Font("Century Gothic", 9F),
            };

            int y = 16;
            const int lblX = 16, ctrlX = 160, ctrlW = 190;

            var lblValor = new Label { Text = "Valor Pago (R$):", Location = new Point(lblX, y + 4), AutoSize = true };
            var txtValor = new TextBox { Location = new Point(ctrlX, y), Size = new Size(ctrlW, 25), Text = valorSugerido };
            frm.Controls.Add(lblValor);
            frm.Controls.Add(txtValor);

            y += 36;
            var lblForma = new Label { Text = "Forma de Pgto:", Location = new Point(lblX, y + 4), AutoSize = true };
            var cmbForma = new ComboBox
            {
                Location = new Point(ctrlX, y),
                Size = new Size(ctrlW, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbForma.Items.AddRange(new object[]
            {
                "PIX",
                "Dinheiro",
                "Cartão Crédito",
                "Cartão Débito",
                "Boleto",
                "Transferência"
            });
            cmbForma.SelectedIndex = 0;
            frm.Controls.Add(lblForma);
            frm.Controls.Add(cmbForma);

            y += 36;
            var lblObs = new Label { Text = "Observação:", Location = new Point(lblX, y + 4), AutoSize = true };
            var txtObs = new TextBox { Location = new Point(ctrlX, y), Size = new Size(ctrlW, 50), Multiline = true };
            frm.Controls.Add(lblObs);
            frm.Controls.Add(txtObs);

            y += 70;
            var btnOk = new Button
            {
                Text = "Confirmar",
                DialogResult = DialogResult.OK,
                Location = new Point(ctrlX, y),
                Size = new Size(90, 32)
            };
            var btnCancel = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Location = new Point(ctrlX + 100, y),
                Size = new Size(90, 32)
            };
            frm.Controls.Add(btnOk);
            frm.Controls.Add(btnCancel);
            frm.AcceptButton = btnOk;
            frm.CancelButton = btnCancel;

            btnOk.Click += (s, e) =>
            {
                if (!decimal.TryParse(txtValor.Text, NumberStyles.Any, _ptBR, out decimal valor) || valor <= 0)
                {
                    MessageBox.Show("Valor inválido.");
                    frm.DialogResult = DialogResult.None;
                    return;
                }

                frm.Tag = (
                    valor,
                    cmbForma.SelectedItem!.ToString()!,
                    txtObs.Text.Trim().Length > 0 ? txtObs.Text.Trim() : (string?)null);
            };

            return frm;
        }

        // ══════════════════════════════════════════════
        // HELPERS
        // ══════════════════════════════════════════════

        private static long ObterIdSelecionado(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0 || !dgv.Columns.Contains("Id"))
                return 0;

            var val = dgv.SelectedRows[0].Cells["Id"].Value;
            return val switch
            {
                long l => l,
                int i => i,
                string s when long.TryParse(s, out var x) => x,
                _ => 0
            };
        }

        private static void CarregarAlunosCombo(ComboBox cmb)
        {
            try
            {
                using var conn = new SqliteConnection(Db.ConnectionString);
                conn.Open();

                using var cmd = new SqliteCommand(
                    "SELECT id, nome FROM alunos WHERE ativo = 1 ORDER BY nome;", conn);
                using var reader = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Columns.Add("id", typeof(long));
                dt.Columns.Add("nome", typeof(string));

                while (reader.Read())
                    dt.Rows.Add(reader.GetInt64(0), reader.GetString(1));

                cmb.DataSource = dt;
                cmb.DisplayMember = "nome";
                cmb.ValueMember = "id";
            }
            catch
            {
                // Se falhar, o combo permanece vazio.
            }
        }
        private static string? NormalizarNumeroFinanceiro(string? numero)
        {
            if (string.IsNullOrWhiteSpace(numero) ||
                numero == "Sem número" ||
                numero == "Não cadastrado")
                return null;

            string somenteDigitos = new string(numero.Where(char.IsDigit).ToArray());

            if (somenteDigitos.Length == 0)
                return null;

            if (somenteDigitos.Length == 13 && somenteDigitos.StartsWith("55"))
                somenteDigitos = somenteDigitos[2..];

            return somenteDigitos;
        }

        private async Task GarantirConexaoWhatsAppAsync()
        {
            if (WhatsAppDriverManager.Conectado)
                return;

            lblStatusBar.Text = "Conectando ao WhatsApp...";
            await WhatsAppDriverManager.IniciarAsync(exibirQR: false);

            await Task.Delay(5000);

            if (!WhatsAppDriverManager.Conectado)
                throw new Exception("WhatsApp não conectado.");
        }

        private async Task<(int enviados, int falhas)> EnviarWhatsAppFinanceiroAsync(
    string? telAluno,
    string? telResponsavel,
    string nomeAluno,
    string nomeResponsavel,
    string msgAluno,
    string msgResponsavel,
    Action<string>? log = null)
        {
            await GarantirConexaoWhatsAppAsync();

            int enviados = 0;
            int falhas = 0;

            string? telAlunoNorm = NormalizarNumeroFinanceiro(telAluno);
            string? telRespNorm = NormalizarNumeroFinanceiro(telResponsavel);

            bool numerosDuplicados =
                !string.IsNullOrEmpty(telAlunoNorm) &&
                !string.IsNullOrEmpty(telRespNorm) &&
                telAlunoNorm == telRespNorm;

            bool nomesDuplicados = MesmoNome(nomeAluno, nomeResponsavel);

            // Se for a mesma pessoa por nome OU por número, envia só uma vez
            bool mesmaPessoa = nomesDuplicados || numerosDuplicados;

            if (!string.IsNullOrWhiteSpace(telAlunoNorm) && !mesmaPessoa)
            {
                try
                {
                    await WhatsAppDriverManager.EnviarMensagemAsync(telAlunoNorm, msgAluno);
                    enviados++;
                    log?.Invoke($"✅ WhatsApp enviado para o aluno: {nomeAluno} ({telAluno})");
                }
                catch (Exception ex)
                {
                    falhas++;
                    string motivo = ex.Message.Contains("Timed out")
                        ? "Tempo esgotado (o número pode não ter WhatsApp ou estar incorreto)."
                        : ex.Message;

                    log?.Invoke($"❌ Falha no WhatsApp do aluno {nomeAluno}: {motivo}");
                }

                await Task.Delay(_rnd.Next(1500, 3000));
            }
            else if (mesmaPessoa && !string.IsNullOrWhiteSpace(telRespNorm))
            {
                log?.Invoke($"ℹ️ Aluno e responsável são a mesma pessoa. Enviando apenas uma mensagem.");
            }

            // Se for a mesma pessoa, envia só para o responsável (ou número principal final)
            if (!string.IsNullOrWhiteSpace(telRespNorm))
            {
                try
                {
                    string mensagemFinal = mesmaPessoa ? msgAluno : msgResponsavel;

                    await WhatsAppDriverManager.EnviarMensagemAsync(telRespNorm, mensagemFinal);
                    enviados++;

                    string extra = mesmaPessoa ? " (Aluno/Resp)" : "";
                    log?.Invoke($"✅ WhatsApp enviado para responsável de {nomeAluno}: {nomeResponsavel} ({telResponsavel}){extra}");
                }
                catch (Exception ex)
                {
                    falhas++;
                    string motivo = ex.Message.Contains("Timed out")
                        ? "Tempo esgotado (o número pode não ter WhatsApp ou estar incorreto)."
                        : ex.Message;

                    log?.Invoke($"❌ Falha no WhatsApp do responsável {nomeResponsavel}: {motivo}");
                }

                await Task.Delay(_rnd.Next(1500, 3000));
            }
            else if (mesmaPessoa && !string.IsNullOrWhiteSpace(telAlunoNorm))
            {
                // Se não houver telefone de responsável, manda só para o aluno
                try
                {
                    await WhatsAppDriverManager.EnviarMensagemAsync(telAlunoNorm, msgAluno);
                    enviados++;
                    log?.Invoke($"✅ WhatsApp enviado para {nomeAluno} ({telAluno})");
                }
                catch (Exception ex)
                {
                    falhas++;
                    string motivo = ex.Message.Contains("Timed out")
                        ? "Tempo esgotado (o número pode não ter WhatsApp ou estar incorreto)."
                        : ex.Message;

                    log?.Invoke($"❌ Falha no WhatsApp de {nomeAluno}: {motivo}");
                }

                await Task.Delay(_rnd.Next(1500, 3000));
            }

            return (enviados, falhas);
        }

        private async Task<(string nomeAluno, string nomeResp, string telAluno, string telResp, string email)> BuscarContatosParcelaAsync(long parcelaId)
        {
            const string sql = @"
        SELECT 
            a.nome AS nome_aluno,
            COALESCE(a.NomeResponsavel, a.nome) AS nome_resp,
            COALESCE(a.numero_aluno, '') AS tel_aluno,
            COALESCE(a.numero_responsavel, '') AS tel_resp,
            COALESCE(a.email, '') AS email
        FROM parcelas p
        INNER JOIN contratos c ON c.id = p.contrato_id
        INNER JOIN alunos a ON a.id = c.aluno_id
        WHERE p.id = @parcelaId
        LIMIT 1;";

            var dt = await Db.QueryDataTableAsync(sql, new[]
            {
        Db.P("@parcelaId", parcelaId)
    });

            if (dt.Rows.Count == 0)
                throw new Exception("Contatos da parcela não encontrados.");

            var row = dt.Rows[0];

            return (
                row["nome_aluno"]?.ToString() ?? "",
                row["nome_resp"]?.ToString() ?? "",
                row["tel_aluno"]?.ToString() ?? "",
                row["tel_resp"]?.ToString() ?? "",
                row["email"]?.ToString() ?? ""
            );
        }

        private static string NormalizarNomeComparacao(string? nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return string.Empty;

            string texto = nome.Trim().ToUpperInvariant();

            texto = texto
                .Replace("Á", "A").Replace("À", "A").Replace("Ã", "A").Replace("Â", "A").Replace("Ä", "A")
                .Replace("É", "E").Replace("È", "E").Replace("Ê", "E").Replace("Ë", "E")
                .Replace("Í", "I").Replace("Ì", "I").Replace("Î", "I").Replace("Ï", "I")
                .Replace("Ó", "O").Replace("Ò", "O").Replace("Õ", "O").Replace("Ô", "O").Replace("Ö", "O")
                .Replace("Ú", "U").Replace("Ù", "U").Replace("Û", "U").Replace("Ü", "U")
                .Replace("Ç", "C");

            return texto;
        }

        private static bool MesmoNome(string? nome1, string? nome2)
        {
            return NormalizarNomeComparacao(nome1) == NormalizarNomeComparacao(nome2);
        }

        private async void btnDisparoAgendado_Click(object sender, EventArgs e)
        {
            btnDisparoAgendado.Enabled = false;

            try
            {
                await ExecutarProcessamentoFinanceiroAsync(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDisparoAgendado.Enabled = true;
            }
            /*
            if (_processamentoFinanceiroEmAndamento)
            {
                MessageBox.Show(
                    "Já existe um processamento em andamento.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            btnDisparoAgendado.Enabled = false;
            lblStatusBar.Text = "Processando mensagens financeiras...";
            txtResultado.Clear();
            txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] Iniciando disparo manual...\r\n");

            try
            {
                _processamentoFinanceiroEmAndamento = true;

                var resultado = await NotificacaoFinanceiraService.ProcessarAsync();

                txtResultado.AppendText(
                    $"[{DateTime.Now:HH:mm:ss}] Lembretes gerados: {resultado.LembretesGerados}\r\n");
                txtResultado.AppendText(
                    $"[{DateTime.Now:HH:mm:ss}] Cobranças geradas: {resultado.CobrancasGeradas}\r\n");
                txtResultado.AppendText(
                    $"[{DateTime.Now:HH:mm:ss}] E-mails enviados: {resultado.EmailsEnviados}\r\n");
                txtResultado.AppendText(
                    $"[{DateTime.Now:HH:mm:ss}] E-mails com falha: {resultado.EmailsFalhas}\r\n");
                txtResultado.AppendText(
                    $"[{DateTime.Now:HH:mm:ss}] WhatsApp enviados: {resultado.WhatsAppEnviados}\r\n");
                txtResultado.AppendText(
                    $"[{DateTime.Now:HH:mm:ss}] WhatsApp com falha: {resultado.WhatsAppFalhas}\r\n");
                txtResultado.AppendText(
                    $"[{DateTime.Now:HH:mm:ss}] Processamento concluído!\r\n");

                lblStatusBar.Text =
                    $"Lembretes: {resultado.LembretesGerados} | Cobranças: {resultado.CobrancasGeradas} | WhatsApp: {resultado.WhatsAppEnviados} enviados";
            }
            catch (Exception ex)
            {
                txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] ❌ Erro: {ex.Message}\r\n");
                lblStatusBar.Text = "Erro ao processar notificações.";
                MessageBox.Show(
                    $"Erro ao processar notificações:\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _processamentoFinanceiroEmAndamento = false;
                btnDisparoAgendado.Enabled = true;
            }*/
        }

        private FiltroCobrancaLote MontarFiltroAtual()
        {
            string? status = cmbStatusParcela.SelectedItem?.ToString();
            if (status == "Todos")
                status = null;

            return new FiltroCobrancaLote
            {
                Status = status,
                Contrato = null,
                NomeAluno = string.IsNullOrWhiteSpace(txtPesquisaParcela.Text)
                    ? null
                    : txtPesquisaParcela.Text.Trim(),
                VencimentoInicial = null,
                VencimentoFinal = null,
                SomenteComTelefone = true,
                SomenteComEmail = false
            };
        }

        private void LogResultado(string mensagem)
        {
            if (txtResultado.InvokeRequired)
            {
                txtResultado.Invoke(new Action(() => LogResultado(mensagem)));
                return;
            }

            txtResultado.AppendText($"[{DateTime.Now:HH:mm:ss}] {mensagem}{Environment.NewLine}");
            txtResultado.SelectionStart = txtResultado.Text.Length;
            txtResultado.ScrollToCaret();
        }

        private async void btnEnviarFiltrados_Click(object sender, EventArgs e)
        {
            try
            {
                btnEnviarFiltrados.Enabled = false;
                txtResultado.Clear();

                LogResultado("Montando filtro atual...");
                var filtro = MontarFiltroAtual();

                var loteService = new FinanceiroLoteService();
                var templateService = new TemplateMensagemService();
                var disparoService = new DisparoMensagemService();
                var auditoriaService = new AuditoriaDisparoService();

                LogResultado("Buscando destinatários no banco...");
                var itens = await loteService.ObterItensParaCobrancaAsync(filtro);

                if (itens.Count == 0)
                {
                    LogResultado("Nenhum destinatário encontrado para os filtros informados.");
                    MessageBox.Show(
                        "Nenhum destinatário encontrado para os filtros informados.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                string template =
                    "Olá, {RESPONSAVEL}. Informamos que a parcela {PARCELA} do aluno {ALUNO}, " +
                    "no valor de R$ {VALOR}, com vencimento em {VENCIMENTO}, está com status {STATUS}.";

                foreach (var item in itens)
                    item.MensagemFinal = templateService.AplicarTemplate(template, item);

                var totalComDestino = itens.Count(x =>
                    !string.IsNullOrWhiteSpace(x.TelefoneResponsavel) ||
                    !string.IsNullOrWhiteSpace(x.TelefoneAluno));

                var confirmacao = MessageBox.Show(
                    $"Foram encontrados {itens.Count} itens.\n" +
                    $"Com destino para WhatsApp: {totalComDestino}\n\n" +
                    "Deseja continuar com o envio?",
                    "Confirmar disparo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacao != DialogResult.Yes)
                {
                    LogResultado("Envio cancelado pelo usuário.");
                    return;
                }

                LogResultado("Iniciando disparo...");
                var resultado = await disparoService.EnviarAsync(itens, enviarWhatsapp: true, enviarEmail: false);

                LogResultado($"Total de itens: {resultado.TotalItens}");
                LogResultado($"WhatsApp enviados: {resultado.WhatsAppEnviados}");
                LogResultado($"WhatsApp falhas: {resultado.WhatsAppFalhas}");

                foreach (var log in resultado.Logs)
                    LogResultado(log);

                string filtroResumo =
                    $"Status={filtro.Status}; Nome={filtro.NomeAluno}";

                await auditoriaService.RegistrarLoteAsync(
                    Sessao.UsuarioNome,
                    "FrmFinanceiro",
                    filtroResumo,
                    resultado);

                MessageBox.Show(
                    $"Envio concluído.\n\n" +
                    $"WhatsApp enviados: {resultado.WhatsAppEnviados}\n" +
                    $"Falhas: {resultado.WhatsAppFalhas}",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogResultado($"Erro: {ex.Message}");
                MessageBox.Show(
                    $"Erro ao enviar mensagens: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnEnviarFiltrados.Enabled = true;
            }
        }
    }
}