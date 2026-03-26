using Central_do_Educador.Data;
using Central_do_Educador.Services;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmControleTaxa : Form
    {
        private int _idEmEdicao;      // 0 = novo | >0 = edição
        private readonly string _usuario;
        private Configuracao? _config; // Adicione esta linha para declarar _config
        private readonly System.Windows.Forms.Timer _timerFiltro = new System.Windows.Forms.Timer();
        private bool _loaded = false;

        // evita mostrar diversas vezes o alerta "BD desligado"
        private bool _dbWarningShown = false;

        /// <summary>Indica que o formulário foi fechado por um logout (não por encerramento normal).</summary>
        public bool IsLogout { get; private set; }

        //private async void dtpDe_ValueChanged(object sender, EventArgs e) => await CarregarGridAsync();
        //private async void dtpAte_ValueChanged(object sender, EventArgs e) => await CarregarGridAsync();

        public FrmControleTaxa(string usuario) : this(usuario, 0) { }
        
        public FrmControleTaxa(string usuario, int idParaEditar)
        {
            InitializeComponent();


            _usuario = usuario ?? string.Empty; // Corrigido para garantir não nulo
            _idEmEdicao = idParaEditar;

            _timerFiltro.Interval = 300; // 300ms: bom equilíbrio
            _timerFiltro.Tick += async (s, e) =>
            {
                _timerFiltro.Stop();
                //await CarregarGridAsync();
            };

            // Inscreve-se para ser notificado quando o estado do BD mudar
            // DbState.StateChanged += OnDbStateChanged;
        }

        private void FrmControleTaxa_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = $"Usuário: {_usuario} ({Sessao.NivelUsuario})";

            CarregarFiltroLancado();
            //AplicarPermissoes();
            _loaded = true;
            //_ = CarregarGridAsync();
        }

        private async void FrmControleTaxa_Activated(object sender, EventArgs e)
        {
            if (!_loaded) return;

            lblUsuario.Text = $"Usuário: {_usuario}";
            RodapeTotaisDoGrid();
            //await CarregarGridAsync();
        }

        /*private async Task CarregarGridAsync()
        {
            // 🔒 proteção contra eventos disparando cedo (antes do form estar pronto)
            if (!IsHandleCreated) return;
            if (dgvCT == null) return;

            string filtroNome = (txtFiltroAluno?.Text ?? "").Trim();
            string filtroLancado = cmbFiltroLancado?.SelectedItem?.ToString() ?? "Todos";

            string de = dtpDe?.Value.Date.ToString("yyyy-MM-dd") ?? DateTime.Today.ToString("yyyy-MM-dd");
            string ate = dtpAte?.Value.Date.ToString("yyyy-MM-dd") ?? DateTime.Today.ToString("yyyy-MM-dd");

            // Filtros laterais
            bool usarFiltroDataFalta = dtpFiltroDataFalta?.ShowCheckBox == true && dtpFiltroDataFalta.Checked;
            string? filtroDataFalta = usarFiltroDataFalta ? dtpFiltroDataFalta?.Value.Date.ToString("yyyy-MM-dd") : null;

            bool usarFiltroDataReposicao = dtpFiltroDataReposicao?.ShowCheckBox == true && dtpFiltroDataReposicao.Checked;
            string? filtroDataReposicao = usarFiltroDataReposicao ? dtpFiltroDataReposicao?.Value.Date.ToString("yyyy-MM-dd") : null;

            bool usarFiltroDataRegistro = dtpFiltroDataRegistro?.ShowCheckBox == true && dtpFiltroDataRegistro.Checked;
            string? filtroDataRegistro = usarFiltroDataRegistro ? dtpFiltroDataRegistro?.Value.Date.ToString("yyyy-MM-dd") : null;

            string filtroFaltas = (txtFiltroFaltas?.Text ?? "").Trim();
            string filtroUsuario = (txtFiltroUsuario?.Text ?? "").Trim();

            // Tenta abrir conexão — se falhar, limpa o grid e informa o usuário, sem "bloquear" por estado externo
            try
            {
                using var con = new SqliteConnection(Db.ConnectionString);
                await con.OpenAsync();

                string sql = @"
                    SELECT
                        r.id AS Id,
                        COALESCE(a.nome, r.aluno) AS Aluno,
                        r.data_falta AS DataFalta,
                        r.Data_Reposicao AS DataReposicao,
                        r.data_registro AS DataRegistro,
                        r.quantidade AS Quantidade,
                        CASE WHEN r.lancado = 1 THEN 'Sim' ELSE 'Não' END AS Lancado,
                        CASE WHEN r.Historico = 1 THEN 'Sim' ELSE 'Não' END AS Historico,
                        r.Observacao AS Observacao,
                        r.UsuarioResponsavel AS Usuario
                    FROM reposicao r
                    LEFT JOIN alunos a ON a.id = r.aluno_id
                    WHERE 1=1
                      AND r.data_registro BETWEEN @de AND @ate";

                if (filtroLancado == "Lançados")
                    sql += " AND lancado = 1 ";
                else if (filtroLancado == "Não lançados")
                    sql += " AND lancado = 0 ";

                if (!string.IsNullOrWhiteSpace(filtroNome))
                    sql += " AND aluno LIKE @nome ESCAPE '\\' ";

                // Filtros laterais
                if (filtroDataFalta != null)
                    sql += " AND r.data_falta = @dataFalta ";

                if (filtroDataReposicao != null)
                    sql += " AND r.Data_Reposicao = @dataReposicao ";

                if (filtroDataRegistro != null)
                    sql += " AND r.data_registro = @dataRegistro ";

                if (!string.IsNullOrWhiteSpace(filtroFaltas) && int.TryParse(filtroFaltas, out int qtdFaltas))
                    sql += " AND r.quantidade = @quantidade ";

                if (!string.IsNullOrWhiteSpace(filtroUsuario))
                    sql += " AND r.UsuarioResponsavel LIKE @usuario ESCAPE '\\' ";

                sql += " ORDER BY id DESC;";

                using var cmd = new SqliteCommand(sql, con);
                cmd.Parameters.AddWithValue("@de", de);
                cmd.Parameters.AddWithValue("@ate", ate);

                if (!string.IsNullOrWhiteSpace(filtroNome))
                {
                    string nomeEscapado = filtroNome.Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
                    cmd.Parameters.AddWithValue("@nome", $"%{nomeEscapado}%");
                }

                if (filtroDataFalta != null)
                    cmd.Parameters.AddWithValue("@dataFalta", filtroDataFalta);

                if (filtroDataReposicao != null)
                    cmd.Parameters.AddWithValue("@dataReposicao", filtroDataReposicao);

                if (filtroDataRegistro != null)
                    cmd.Parameters.AddWithValue("@dataRegistro", filtroDataRegistro);

                if (!string.IsNullOrWhiteSpace(filtroFaltas) && int.TryParse(filtroFaltas, out int qtdParam))
                    cmd.Parameters.AddWithValue("@quantidade", qtdParam);

                if (!string.IsNullOrWhiteSpace(filtroUsuario))
                {
                    string usuarioEscapado = filtroUsuario.Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
                    cmd.Parameters.AddWithValue("@usuario", $"%{usuarioEscapado}%");
                }

                var dt = new DataTable();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }

                dgvCT.AutoGenerateColumns = true;
                dgvCT.Columns.Clear();
                dgvCT.DataSource = dt;

                FormatarGridReposicao();
                RodapeTotaisDoGrid();
            }
            catch (Exception ex)
            {
                // Se falhar (por ex. BD inacessível), limpa UI e avisa sem tentar "forçar" estado externo
                dgvCT.DataSource = null;
                RodapeTotaisDoGrid();

                // Mensagem informativa ao usuário
                MessageBox.Show($"Não foi possível carregar os dados do banco: {ex.Message}", "Erro de conexão",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }*/

        private void FormatarGridReposicao()
        {
            var g = dgvCT; // ajuste pro nome do seu DataGridView

            // comportamento/visual
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

            // Zebra (linhas alternadas) - usa cor padrão do sistema se quiser,
            // mas aqui vai uma leve pra ficar "app"
            g.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            // Fonte (opcional)
            g.DefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 10F);
            g.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);

            // Oculta a coluna Id (mantém acessível via código, mas invisível no grid)
            if (g.Columns.Contains("Id"))
                g.Columns["Id"].Visible = false;

            // Cabeçalhos e ajustes por coluna (se existirem)
            AjustarColunaTexto(g, "Aluno", "Aluno", 200, DataGridViewContentAlignment.MiddleLeft);

            AjustarColunaData(g, "DataFalta", "Data da Falta", 110);
            AjustarColunaData(g, "DataReposicao", "Data da Reposição", 125);
            AjustarColunaData(g, "DataRegistro", "Data do Registro", 110);

            AjustarColunaNumero(g, "Quantidade", "Qtd.", 70);

            AjustarColunaTexto(g, "Lancado", "Lançado", 80, DataGridViewContentAlignment.MiddleCenter);
            AjustarColunaTexto(g, "Historico", "Histórico", 90, DataGridViewContentAlignment.MiddleCenter);

            AjustarColunaTexto(g, "Observacao", "Obs.", 260, DataGridViewContentAlignment.MiddleLeft);
            AjustarColunaTexto(g, "Usuario", "Usuário", 120, DataGridViewContentAlignment.MiddleLeft);

            // Deixa Obs quebrar linha (se você quiser ver textos maiores)
            if (g.Columns.Contains("Observacao"))
            {
                g.Columns["Observacao"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            }

            // Centraliza datas e "Sim/Não"
            CentralizarSeExistir(g, "DataFalta");
            CentralizarSeExistir(g, "DataReposicao");
            CentralizarSeExistir(g, "DataRegistro");
            CentralizarSeExistir(g, "Lancado");
            CentralizarSeExistir(g, "Historico");
            CentralizarSeExistir(g, "Quantidade");

            // Formata valores especiais: DataReposicao vazia -> "Não agendado"
            g.CellFormatting -= dgvCT_CellFormatting; // evita duplicar evento
            g.CellFormatting += dgvCT_CellFormatting;
        }

        private void AjustarColunaTexto(DataGridView g, string nome, string header, int minWidth, DataGridViewContentAlignment align)
        {
            if (!g.Columns.Contains(nome)) return;
            var c = g.Columns[nome];
            c.HeaderText = header;
            c.MinimumWidth = minWidth;
            c.DefaultCellStyle.Alignment = align;
        }

        private void CentralizarSeExistir(DataGridView g, string nome)
        {
            if (!g.Columns.Contains(nome)) return;
            g.Columns[nome].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void AjustarColunaNumero(DataGridView g, string nome, string header, int minWidth)
        {
            if (!g.Columns.Contains(nome)) return;
            var c = g.Columns[nome];
            c.HeaderText = header;
            c.MinimumWidth = minWidth;
            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void AjustarColunaData(DataGridView g, string nome, string header, int minWidth)
        {
            if (!g.Columns.Contains(nome)) return;
            var c = g.Columns[nome];
            c.HeaderText = header;
            c.MinimumWidth = minWidth;
            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void RodapeTotaisDoGrid()
        {
            int registros = dgvCT.Rows.Count;

            int qtdTotal = 0;
            foreach (DataGridViewRow row in dgvCT.Rows)
            {
                if (row.Cells["Quantidade"].Value != null &&
                    int.TryParse(row.Cells["Quantidade"].Value.ToString(), out int q))
                {
                    qtdTotal += Math.Max(0, q);
                }
            }

            var valorTaxa = _config?.ValorTaxaPadrao ?? 17.5m;
            var percComissao = _config?.PercentualComissao ?? 10m;

            var total = Math.Round(qtdTotal * valorTaxa, 2, MidpointRounding.AwayFromZero);
            var comissao = Math.Round(total * (percComissao / 100m), 2, MidpointRounding.AwayFromZero);

            lblTotalRegistros.Text = $"Total registros: {registros}";
            lblTotalFaltas.Text = $"Qtd. faltas: {qtdTotal}";
            lblTotal.Text = $"Total: R$ {total:N2}";
            lblComissao.Text = $"Comissão: R$ {comissao:N2}";
        }

        private void dgvCT_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            // Segurança: garante que 'sender' é um DataGridView e que o índice está dentro do range
            if (sender is not DataGridView g) return;
            if (e.ColumnIndex < 0 || e.ColumnIndex >= g.Columns.Count) return;

            var columnName = g.Columns[e.ColumnIndex].Name ?? string.Empty;

            // DataReposicao: se vier vazio/NULL, mostra "Não agendado"
            if (columnName == "DataReposicao")
            {
                if (e.Value == null || e.Value == DBNull.Value || string.IsNullOrWhiteSpace(e.Value?.ToString()))
                {
                    e.Value = "Não agendado";
                    e.FormattingApplied = true;
                }
            }

            // Se suas datas vierem como "yyyy-MM-dd" em string, converte pra dd/MM/yyyy
            if (columnName == "DataFalta" || columnName == "DataReposicao" || columnName == "DataRegistro")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    var s = e.Value.ToString();

                    // Só tenta converter se parecer data ISO
                    if (!string.IsNullOrWhiteSpace(s) && s.Length >= 10)
                    {
                        if (DateTime.TryParseExact(s.Substring(0, 10), "yyyy-MM-dd",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None, out var dt))
                        {
                            e.Value = dt.ToString("dd/MM/yyyy");
                            e.FormattingApplied = true;
                        }
                    }
                }
            }
        }

        private async void dgvCT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ignora double-click em cabeçalho ou áreas inválidas
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                // Segurança: garante que temos uma linha válida
                if (dgvCT.Rows.Count <= e.RowIndex) return;
                var row = dgvCT.Rows[e.RowIndex];

                // Tenta obter o ID da célula "Id"
                var idObj = row.Cells["Id"]?.Value;
                if (idObj == null || idObj == DBNull.Value || !int.TryParse(idObj.ToString(), out int id) || id <= 0)
                {
                    MessageBox.Show("Não foi possível identificar o ID do registro selecionado.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Abre a tela de edição passando usuário e id
                var tela = new FrmTaxa(_usuario, id);
                tela.ShowDialog();

                // Recarrega o grid após fechar a tela de edição
                //await CarregarGridAsync();
            }
            catch (Exception ex)
            {
                var msg =
                    "Erro ao abrir edição.\n\n" +
                    "EXCEPTION:\n" + ex + "\n\n" +
                    "INNER:\n" + (ex.InnerException?.ToString() ?? "(sem InnerException)");

                MessageBox.Show(msg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFiltroAluno_TextChanged(object sender, EventArgs e)
        {
            _timerFiltro.Stop();
            _timerFiltro.Start();
        }

        private async void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCT.CurrentRow == null)
                {
                    MessageBox.Show("Selecione um registro para excluir.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Sessao.IsAdmin &&
                    !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmControleTaxa"))
                {
                    MessageBox.Show("Apenas administradores podem acessar esta tela.",
                        "Acesso negado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Pega o ID da linha selecionada
                var idObj = dgvCT.CurrentRow.Cells["Id"].Value;

                if (idObj == null || idObj == DBNull.Value || !int.TryParse(idObj.ToString(), out int id) || id <= 0)
                {
                    MessageBox.Show("Não foi possível identificar o ID do registro selecionado.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // (Opcional) pega o nome do aluno pra mostrar no alerta
                var aluno = dgvCT.CurrentRow.Cells["Aluno"]?.Value?.ToString() ?? "";

                var resp = MessageBox.Show(
                    $"Tem certeza que deseja excluir o registro {id} {(string.IsNullOrWhiteSpace(aluno) ? "" : $"do aluno '{aluno}' ")}?",
                    "Confirmar exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resp != DialogResult.Yes)
                    return;

                // Exclui no banco
                await Task.Run(() =>
                {
                    using var con = new SqliteConnection(Db.ConnectionString);
                    con.Open();

                    using var cmd = new SqliteCommand("DELETE FROM reposicao WHERE id = @id;", con);
                    cmd.Parameters.AddWithValue("@id", id);

                    var rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new Exception("O registro não foi encontrado (talvez já tenha sido excluído).");
                });

                MessageBox.Show("Registro excluído com sucesso!", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recarrega o grid (use o seu método)
                //await CarregarGridAsync();

                // Atualiza o rodapé, se você estiver usando
                // RodapeTotais(lista) ou RodapeTotaisDoGrid();
                RodapeTotaisDoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCT.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registro para editar.");
                return;
            }

            if (!Sessao.IsAdmin &&
                    !await PermissaoService.TemPermissaoAsync(Sessao.UsuarioId, "FrmControleTaxa"))
            {
                MessageBox.Show("Apenas administradores podem acessar esta tela.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var idObj = dgvCT.CurrentRow.Cells["Id"].Value;
            if (idObj == null || !int.TryParse(idObj.ToString(), out int id) || id <= 0)
            {
                MessageBox.Show("Não foi possível obter o ID do registro.");
                return;
            }

            // Abre a tela de edição passando usuário e id
            var tela = new FrmTaxa(_usuario, id);
            tela.ShowDialog();

            // Recarrega o grid depois que fechar
            //_ = CarregarGridAsync();
        }

        private void picPrepArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = "Excel (*.xlsx;*.xls)|*.xlsx;*.xls";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                string caminhoExcel = ofd.FileName;

                // 1) Busca o Python automaticamente
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

                // 2) Script relativo ao executável da aplicação
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

                // 3) Pasta de saída: Documentos do usuário atual
                string pastaSaida = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Fallback se MyDocuments retornar vazio (redirecionamento OneDrive)
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
            // Candidatos: nomes que podem estar no PATH
            string[] nomesPath = ["python", "python3"];

            foreach (var nome in nomesPath)
            {
                string? encontrado = BuscarNoPath(nome);
                if (encontrado != null) return encontrado;
            }

            // Candidatos: pastas comuns de instalação do Python (Windows)
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            string[] pastasBase =
            [
                Path.Combine(localAppData, "Programs", "Python"),   // Instalação por usuário
                Path.Combine(programFiles, "Python"),                // Instalação global
                Path.Combine(programFiles + " (x86)", "Python"),    // 32-bit em máquina 64-bit
            ];

            foreach (var pastaBase in pastasBase)
            {
                if (!Directory.Exists(pastaBase)) continue;

                // Percorre subpastas (Python311, Python312, Python314, etc.)
                // Ordena decrescente para pegar a versão mais recente primeiro
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

        private void FiltroLateral_Changed(object? sender, EventArgs e)
        {
            if (!_loaded) return;
            _timerFiltro.Stop();
            _timerFiltro.Start();
        }

        private void CarregarFiltroLancado()
        {
            cmbFiltroLancado.Items.Clear();
            cmbFiltroLancado.Items.Add("Todos");
            cmbFiltroLancado.Items.Add("Lançados");
            cmbFiltroLancado.Items.Add("Não lançados");
            cmbFiltroLancado.SelectedIndex = 0;
        }

        private async void picAddTaxa_Click(object sender, EventArgs e)
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

            using var frm = new FrmTaxa(usuarioAtual);
            frm.ShowDialog();
        }
    }
}