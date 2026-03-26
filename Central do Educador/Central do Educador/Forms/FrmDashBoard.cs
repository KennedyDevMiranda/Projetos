using Central_do_Educador.Data;
using Central_do_Educador.Forms;
using Central_do_Educador.Services;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace Central_do_Educador
{
    public partial class FrmDashBoard : Form
    {
        private Form? _activeForm = null;
        private readonly string _usuario;
        public bool IsLogout { get; private set; }

        public FrmDashBoard(string usuario)
        {
            InitializeComponent();

            _usuario = usuario;
            lblUsuario.Text = $"Usuário: {_usuario}";

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private async void FrmDashBord_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = $"Usuário: {_usuario} ({Sessao.NivelUsuario})";
            AplicarPermissoes();
            await CarregarResumoDoMesAsync();
        }

        private async void FrmDashBoard_Activated(object sender, EventArgs e)
        {
            lblUsuario.Text = $"Usuário: {_usuario} ({Sessao.NivelUsuario})";
            await CarregarResumoDoMesAsync();
        }

        private async Task CarregarResumoDoMesAsync()
        {
            try
            {
                using var conn = await Db.OpenConnectionAsync();

                lblTotalAlunos.Text = await ObterTotalAlunosAsync(conn);
                lblReceitaMensal.Text = (await ObterReceitaMensalAsync(conn)).ToString("C2");
                lblPendencias.Text = await ObterPendenciasAsync(conn);
                lblNovosRegistos.Text = "-";

                if (chartAlunosMes != null)
                {
                    ConfigurarGraficoStatusAlunos();
                    await CarregarGraficoStatusAlunosAsync(conn);
                }

                if (chartStatusFinancas != null)
                {
                    ConfigurarGraficoFinanceiro();
                    await CarregarGraficoFinanceiroMesAsync(conn);
                }

                if (dgvAtividades != null)
                {
                    await CarregarAtividadesRecentesAsync(conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar dashboard: {ex.Message}",
                    "Dashboard",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void ConfigurarGraficoStatusAlunos()
        {
            chartAlunosMes.Series.Clear();
            chartAlunosMes.ChartAreas.Clear();
            chartAlunosMes.Titles.Clear();
            chartAlunosMes.Legends.Clear();

            var area = new ChartArea("AreaAlunos");
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.Minimum = 0;
            chartAlunosMes.ChartAreas.Add(area);

            var serie = new Series("Alunos")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                BorderWidth = 1
            };

            chartAlunosMes.Series.Add(serie);
            chartAlunosMes.Titles.Add("Alunos ativos x inativos");
        }

        private void ConfigurarGraficoFinanceiro()
        {
            chartStatusFinancas.Series.Clear();
            chartStatusFinancas.ChartAreas.Clear();
            chartStatusFinancas.Titles.Clear();
            chartStatusFinancas.Legends.Clear();

            var area = new ChartArea("AreaFinanceiro");
            area.BackColor = Color.White;
            chartStatusFinancas.ChartAreas.Add(area);

            var legend = new Legend("LegendFinanceiro")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center
            };
            chartStatusFinancas.Legends.Add(legend);

            var serie = new Series("Financeiro")
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = true,
                LabelFormat = "C2",
                Legend = "LegendFinanceiro"
            };

            chartStatusFinancas.Series.Add(serie);
            chartStatusFinancas.Titles.Add("Financeiro do mês");
        }

        private async Task<string> ObterTotalAlunosAsync(SqliteConnection conn)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT COUNT(*)
                FROM alunos
                WHERE COALESCE(ativo, 1) = 1;";

            var total = await cmd.ExecuteScalarAsync();
            return Convert.ToString(total) ?? "0";
        }

        private async Task<decimal> ObterReceitaMensalAsync(SqliteConnection conn)
        {
            var (inicioMes, fimMes) = ObterPeriodoMesAtual();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT IFNULL(SUM(COALESCE(NULLIF(valor_pago, 0), valor_parcela)), 0)
                FROM parcelas
                WHERE LOWER(COALESCE(status, '')) = 'pago'
                  AND date(COALESCE(data_pagamento, data_vencimento)) BETWEEN date($inicio) AND date($fim);";

            cmd.Parameters.AddWithValue("$inicio", inicioMes);
            cmd.Parameters.AddWithValue("$fim", fimMes);

            var result = await cmd.ExecuteScalarAsync();
            return result == null || result == DBNull.Value ? 0m : Convert.ToDecimal(result);
        }

        private async Task<string> ObterPendenciasAsync(SqliteConnection conn)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT COUNT(*)
                FROM parcelas
                WHERE LOWER(COALESCE(status, '')) <> 'pago';";

            var total = await cmd.ExecuteScalarAsync();
            return Convert.ToString(total) ?? "0";
        }

        private async Task CarregarGraficoStatusAlunosAsync(SqliteConnection conn)
        {
            chartAlunosMes.Series[0].Points.Clear();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT
                    SUM(CASE WHEN COALESCE(ativo, 1) = 1 THEN 1 ELSE 0 END) AS Ativos,
                    SUM(CASE WHEN COALESCE(ativo, 1) = 0 THEN 1 ELSE 0 END) AS Inativos
                FROM alunos;";

            using var reader = await cmd.ExecuteReaderAsync();

            int ativos = 0;
            int inativos = 0;

            if (await reader.ReadAsync())
            {
                ativos = reader["Ativos"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Ativos"]);
                inativos = reader["Inativos"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Inativos"]);
            }

            chartAlunosMes.Series[0].Points.AddXY("Ativos", ativos);
            chartAlunosMes.Series[0].Points.AddXY("Inativos", inativos);
        }

        private async Task CarregarGraficoFinanceiroMesAsync(SqliteConnection conn)
        {
            chartStatusFinancas.Series[0].Points.Clear();

            var (inicioMes, fimMes) = ObterPeriodoMesAtual();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT
                    IFNULL(SUM(
                        CASE
                            WHEN LOWER(COALESCE(status, '')) = 'pago'
                            THEN COALESCE(NULLIF(valor_pago, 0), valor_parcela)
                            ELSE 0
                        END
                    ), 0) AS pago,
                    IFNULL(SUM(
                        CASE
                            WHEN LOWER(COALESCE(status, '')) <> 'pago'
                            THEN COALESCE(valor_parcela, 0)
                            ELSE 0
                        END
                    ), 0) AS pendente
                FROM parcelas
                WHERE date(data_vencimento) BETWEEN date($inicio) AND date($fim);";

            cmd.Parameters.AddWithValue("$inicio", inicioMes);
            cmd.Parameters.AddWithValue("$fim", fimMes);

            using var reader = await cmd.ExecuteReaderAsync();

            decimal pago = 0m;
            decimal pendente = 0m;

            if (await reader.ReadAsync())
            {
                pago = reader["pago"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["pago"]);
                pendente = reader["pendente"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["pendente"]);
            }

            chartStatusFinancas.Series[0].Points.AddXY("Pago", pago);
            chartStatusFinancas.Series[0].Points.AddXY("Pendente", pendente);
        }

        private async Task CarregarAtividadesRecentesAsync(SqliteConnection conn)
        {
            dgvAtividades.Rows.Clear();

            bool tabelaExiste = await TabelaExisteAsync(conn, "notificacoes");
            if (!tabelaExiste)
                return;

            var colunas = await ObterColunasDaTabelaAsync(conn, "notificacoes");

            string colunaHora = EscolherPrimeiraColunaExistente(colunas, "data_envio", "data_criacao", "criado_em", "data", "id");
            string colunaAcao = EscolherPrimeiraColunaExistente(colunas, "mensagem", "titulo", "descricao", "tipo", "id");
            string colunaUsuario = EscolherPrimeiraColunaExistente(colunas, "destinatario", "nome", "telefone", "numero", "id");
            string colunaStatus = EscolherPrimeiraColunaExistente(colunas, "status", "situacao", "resultado", "id");

            using var cmd = conn.CreateCommand();
            cmd.CommandText = $@"
                SELECT {colunaHora} AS Hora, {colunaAcao} AS Acao, {colunaUsuario} AS Utilizador, {colunaStatus} AS Status
                FROM notificacoes
                ORDER BY id DESC
                LIMIT 10;";

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                dgvAtividades.Rows.Add(
                    reader["Hora"]?.ToString(),
                    reader["Acao"]?.ToString(),
                    reader["Utilizador"]?.ToString(),
                    reader["Status"]?.ToString());
            }
        }

        private static (string inicioMes, string fimMes) ObterPeriodoMesAtual()
        {
            var hoje = DateTime.Today;
            var inicioMes = new DateTime(hoje.Year, hoje.Month, 1).ToString("yyyy-MM-dd");
            var fimMes = new DateTime(hoje.Year, hoje.Month, DateTime.DaysInMonth(hoje.Year, hoje.Month)).ToString("yyyy-MM-dd");
            return (inicioMes, fimMes);
        }

        private static string EscolherPrimeiraColunaExistente(HashSet<string> colunas, params string[] candidatos)
        {
            foreach (var candidato in candidatos)
            {
                if (colunas.Contains(candidato))
                    return candidato;
            }

            return "id";
        }

        private static async Task<bool> TabelaExisteAsync(SqliteConnection conn, string nomeTabela)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT COUNT(*)
                FROM sqlite_master
                WHERE type = 'table' AND name = $nome;";
            cmd.Parameters.AddWithValue("$nome", nomeTabela);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }

        private static async Task<HashSet<string>> ObterColunasDaTabelaAsync(SqliteConnection conn, string nomeTabela)
        {
            var colunas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"PRAGMA table_info({nomeTabela});";

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var nome = reader["name"]?.ToString();
                if (!string.IsNullOrWhiteSpace(nome))
                    colunas.Add(nome);
            }

            return colunas;
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void AplicarPermissoes()
        {
            bool isAdmin = Sessao.IsAdmin;

            if (usuariosToolStripMenuItem != null)
                usuariosToolStripMenuItem.Visible = isAdmin;

            if (bancoDeDadToolStripMenuItem != null)
                bancoDeDadToolStripMenuItem.Visible = isAdmin;
        }

        private async void controleDeTaxaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string usuarioAtual = Sessao.UsuarioNome;

            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmControleTaxa"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmControleTaxa(usuarioAtual);
            frm.ShowDialog();
        }

        private async void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmUsuarios"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmUsuarios();
            frm.ShowDialog();
        }

        private async void entregaDeLivrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmEntregaLivros"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmEntregaLivros();
            frm.ShowDialog();
        }

        private async void chatBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmChatBot"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmChatBot();
            frm.ShowDialog();
        }

        private async void bancoDeDadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmAlterBanco"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmAlterBanco();
            frm.ShowDialog();
        }

        private async void alunoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmAluno"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmAluno();
            frm.ShowDialog();
        }

        private async void registroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmRegistro"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmRegistro();
            frm.ShowDialog();
        }

        private async void agendamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmAgendamentos"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmAgendamentos();
            frm.ShowDialog();
        }

        private void sobreNosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var frm = new FrmSobreNos();
            frm.ShowDialog();
        }

        private async void reportarErroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmReportarFalha"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmReportarFalha();
            frm.ShowDialog();
        }

        private async void gerenciarErroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmGerenciarFalhas"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmGerenciarFalhas();
            frm.ShowDialog();
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var resp = MessageBox.Show(
                "Deseja realmente sair da conta atual?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resp != DialogResult.Yes)
                return;

            IsLogout = true;
            Close();
        }

        private async void financeiroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmFinanceiro"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmFinanceiro();
            frm.ShowDialog();
        }

        private async void permissaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Sessao.IsAdmin &&
                !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmPermissao"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new FrmPermissao();
            frm.ShowDialog();
        }

        private void pedagógicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void financeiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void picPrepArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = "Excel (*.xlsx;*.xls)|*.xlsx;*.xls";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                string caminhoExcel = ofd.FileName;
                string? pythonExe = EncontrarPython();
                if (pythonExe == null)
                {
                    MessageBox.Show(
                        "Python não encontrado.\n\n" +
                        "Verifique se o Python está instalado e presente no PATH do sistema,\n" +
                        "ou instale em um dos caminhos padrão (ex: AppData\\Local\\Programs\\Python).",
                        "Python não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string pastaApp = AppDomain.CurrentDomain.BaseDirectory;
                string scriptPy = Path.Combine(pastaApp, "Scripts", "limpar_colunas_excel_faltas.py");

                if (!File.Exists(scriptPy))
                {
                    MessageBox.Show(
                        $"Script não encontrado em:\n{scriptPy}\n\n" +
                        "Coloque o arquivo 'limpar_colunas_excel_faltas.py' na pasta 'Scripts' junto ao executável.",
                        "Script não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string pastaSaida = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (string.IsNullOrWhiteSpace(pastaSaida))
                    pastaSaida = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents");

                if (!Directory.Exists(pastaSaida))
                    Directory.CreateDirectory(pastaSaida);

                var psi = new ProcessStartInfo
                {
                    FileName = pythonExe,
                    Arguments = $"\"{scriptPy}\" \"{caminhoExcel}\" \"{pastaSaida}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                psi.Environment["PYTHONIOENCODING"] = "utf-8";

                using var proc = Process.Start(psi);
                if (proc == null)
                {
                    MessageBox.Show("Não foi possível iniciar o processo Python.");
                    return;
                }

                string saida = proc.StandardOutput.ReadToEnd();
                string erro = proc.StandardError.ReadToEnd();
                proc.WaitForExit();

                if (proc.ExitCode == 0)
                    MessageBox.Show("OK!\n" + saida);
                else
                    MessageBox.Show("Deu erro:\n" + erro);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private static string? EncontrarPython()
        {
            string[] nomesPath = ["python", "python3"];

            foreach (var nome in nomesPath)
            {
                string? encontrado = BuscarNoPath(nome);
                if (encontrado != null) return encontrado;
            }

            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            string[] pastasBase =
            [
                Path.Combine(localAppData, "Programs", "Python"),
                Path.Combine(programFiles, "Python"),
                Path.Combine(programFiles + " (x86)", "Python"),
            ];

            foreach (var pastaBase in pastasBase)
            {
                if (!Directory.Exists(pastaBase)) continue;

                var subpastas = Directory.GetDirectories(pastaBase)
                    .OrderByDescending(d => d);

                foreach (var pasta in subpastas)
                {
                    string candidato = Path.Combine(pasta, "python.exe");
                    if (File.Exists(candidato)) return candidato;
                }
            }

            return null;
        }

        private static string? BuscarNoPath(string nomeExecutavel)
        {
            string pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
            string[] extensoes = Environment.OSVersion.Platform == PlatformID.Win32NT
                ? [".exe", ".cmd", ".bat"]
                : [""];

            foreach (var diretorio in pathEnv.Split(Path.PathSeparator))
            {
                foreach (var ext in extensoes)
                {
                    string caminho = Path.Combine(diretorio.Trim(), nomeExecutavel + ext);
                    if (File.Exists(caminho)) return caminho;
                }
            }

            return null;
        }
    }
}