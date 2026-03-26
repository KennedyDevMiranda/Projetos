using Central_do_Educador.Data;
using ExcelDataReader;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmRegistro : Form
    {
        private DataTable? _dtPreview;
        private bool _loaded = false;

        public FrmRegistro()
        {
            InitializeComponent();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (cmbFiltroAtivo != null)
                cmbFiltroAtivo.SelectedIndex = 0; // "Todos"
        }

        private void FrmRegistro_Load(object? sender, EventArgs e)
        {
            _loaded = true;
            //AtualizarGrid();
        }

        private void AplicarFiltros(object? sender, EventArgs e)
        {
            //AtualizarGrid();
        }

        /*private void AtualizarGrid()
        {
            if (!_loaded) return;

            string filtroNome = (txtFiltroNome?.Text ?? "").Trim();
            string filtroEmail = (txtFiltroEmail?.Text ?? "").Trim();
            string filtroNumAluno = (txtFiltroNumAluno?.Text ?? "").Trim();
            string filtroResponsavel = (txtFiltroResponsavel?.Text ?? "").Trim();
            string filtroNumResp = (txtFiltroNumResp?.Text ?? "").Trim();
            string filtroAtivo = cmbFiltroAtivo?.SelectedItem?.ToString() ?? "Todos";

            try
            {
                using var conn = new SqliteConnection(Db.ConnectionString);
                conn.Open();

                string sql = @"
                    SELECT id, nome, email, numero_aluno, NomeResponsavel, numero_responsavel, ativo
                    FROM alunos
                    WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(filtroNome))
                    sql += " AND nome LIKE @nome ESCAPE '\\'";

                if (!string.IsNullOrWhiteSpace(filtroEmail))
                    sql += " AND email LIKE @email ESCAPE '\\'";

                if (!string.IsNullOrWhiteSpace(filtroNumAluno))
                    sql += " AND numero_aluno LIKE @numero_aluno ESCAPE '\\'";

                if (!string.IsNullOrWhiteSpace(filtroResponsavel))
                    sql += " AND NomeResponsavel LIKE @NomeResponsavel ESCAPE '\\'";

                if (!string.IsNullOrWhiteSpace(filtroNumResp))
                    sql += " AND numero_responsavel LIKE @numero_responsavel ESCAPE '\\'";

                if (filtroAtivo == "Sim")
                    sql += " AND ativo = 1";
                else if (filtroAtivo == "Não")
                    sql += " AND ativo = 0";

                sql += " ORDER BY nome";

                using var cmd = new SqliteCommand(sql, conn);

                if (!string.IsNullOrWhiteSpace(filtroNome))
                    cmd.Parameters.AddWithValue("@nome", $"%{EscapeLike(filtroNome)}%");

                if (!string.IsNullOrWhiteSpace(filtroEmail))
                    cmd.Parameters.AddWithValue("@email", $"%{EscapeLike(filtroEmail)}%");

                if (!string.IsNullOrWhiteSpace(filtroNumAluno))
                    cmd.Parameters.AddWithValue("@numero_aluno", $"%{EscapeLike(filtroNumAluno)}%");

                if (!string.IsNullOrWhiteSpace(filtroResponsavel))
                    cmd.Parameters.AddWithValue("@NomeResponsavel", $"%{EscapeLike(filtroResponsavel)}%");

                if (!string.IsNullOrWhiteSpace(filtroNumResp))
                    cmd.Parameters.AddWithValue("@numero_responsavel", $"%{EscapeLike(filtroNumResp)}%");

                var dt = new DataTable();
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }

                dgvRegistro.DataSource = dt;

                if (lblTotal != null)
                    lblTotal.Text = $"Total de registros: {dt.Rows.Count}";

                if (dt.Rows.Count > 0)
                    FormatarGridRegistro();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar grid: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/

        private static string EscapeLike(string valor)
        {
            return valor
                .Replace("\\", "\\\\")
                .Replace("%", "\\%")
                .Replace("_", "\\_");
        }

        private void FormatarGridRegistro()
        {
            var g = dgvRegistro;
            if (g == null || g.Columns == null || g.Columns.Count == 0) return;

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

            // zebra
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // fonte
            g.DefaultCellStyle.Font = new Font("Century Gothic", 10F);
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);

            // ====== colunas (headers + alinhamento + largura mínima) ======
            //AjustarColunaNumero(g, "id", "ID", 60);
            AjustarColunaTexto(g, "nome", "Aluno", 220, DataGridViewContentAlignment.MiddleLeft);
            AjustarColunaTexto(g, "email", "E-mail", 200, DataGridViewContentAlignment.MiddleLeft);
            AjustarColunaTexto(g, "numero_aluno", "Nº Aluno", 110, DataGridViewContentAlignment.MiddleCenter);
            AjustarColunaTexto(g, "NomeResponsavel", "Responsável", 200, DataGridViewContentAlignment.MiddleLeft);
            AjustarColunaTexto(g, "numero_responsavel", "Nº Responsável", 130, DataGridViewContentAlignment.MiddleCenter);
            AjustarColunaTexto(g, "ativo", "Ativo", 70, DataGridViewContentAlignment.MiddleCenter);

            // centralizações (reforço)
            //CentralizarSeExistir(g, "id");
            CentralizarSeExistir(g, "numero_aluno");
            CentralizarSeExistir(g, "numero_responsavel");
            CentralizarSeExistir(g, "ativo");

            g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        private DataTable LerExcelParaDataTable(string caminho)
        {
            using var stream = File.Open(caminho, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            var dt = new DataTable();
                dt.Columns.Add("nome");
                dt.Columns.Add("email");
                dt.Columns.Add("numero_aluno");
                dt.Columns.Add("NomeResponsavel");
                dt.Columns.Add("numero_responsavel");
                dt.Columns.Add("ativo");

            // Colunas fixas: A=nome, B=email, C=numero_aluno, D=NomeResponsavel, E=numero_responsavel, F=ativo
            bool primeiraLinha = true;

            while (reader.Read())
            {
                int campos = reader.FieldCount;

                string nome = campos > 0 ? reader.GetValue(0)?.ToString()?.Trim() ?? "" : "";
                string email = campos > 1 ? reader.GetValue(1)?.ToString()?.Trim() ?? "" : "";
                string numeroAluno = campos > 2 ? reader.GetValue(2)?.ToString()?.Trim() ?? "" : "";
                string nomeResponsavel = campos > 3 ? reader.GetValue(3)?.ToString()?.Trim() ?? "" : "";
                string numeroResponsavel = campos > 4 ? reader.GetValue(4)?.ToString()?.Trim() ?? "" : "";
                string ativo = campos > 5 ? reader.GetValue(5)?.ToString()?.Trim() ?? "1" : "1";

                if (string.IsNullOrWhiteSpace(nome)) continue;

                if (primeiraLinha)
                {
                    primeiraLinha = false;

                    var sb = new StringBuilder();
                    sb.AppendLine("Verifique se os dados estão na coluna correta:\n");
                    sb.AppendLine($"  Nome:               \"{Truncar(nome, 40)}\"");
                    sb.AppendLine($"  E-mail:             \"{Truncar(email, 40)}\"");
                    sb.AppendLine($"  Nº Aluno:           \"{Truncar(numeroAluno, 30)}\"");
                    sb.AppendLine($"  Responsável:        \"{Truncar(nomeResponsavel, 40)}\"");
                    sb.AppendLine($"  Nº Responsável:     \"{Truncar(numeroResponsavel, 30)}\"");
                    sb.AppendLine($"  Ativo:              \"{ativo}\"");
                    sb.AppendLine();
                    sb.AppendLine("Os dados estão corretos?");

                    var resultado = MessageBox.Show(
                        sb.ToString(),
                        "Verificação de Colunas",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.No)
                    {
                        MessageBox.Show(
                            "Importação cancelada.\n\n" +
                            "O arquivo precisa ter as colunas nesta ordem:\n" +
                            "  A = Nome do aluno\n" +
                            "  B = E-mail\n" +
                            "  C = Número do aluno\n" +
                            "  D = Nome do responsável\n" +
                            "  E = Número do responsável\n" +
                            "  F = Ativo (1 ou 0)",
                            "Importação Cancelada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return dt; // retorna DataTable vazio
                    }
                }

                ativo = ativo == "1" || ativo.Equals("sim", StringComparison.OrdinalIgnoreCase) ? "1" : "0";
                dt.Rows.Add(nome, email, numeroAluno, nomeResponsavel, numeroResponsavel, ativo);
            }

            return dt;
        }

        private DataTable LerCsvParaDataTable(string caminho)
        {
            var dt = new DataTable();
            dt.Columns.Add("nome");
            dt.Columns.Add("email");
            dt.Columns.Add("numero_aluno");
            dt.Columns.Add("NomeResponsavel");
            dt.Columns.Add("numero_responsavel");
            dt.Columns.Add("ativo");

            var linhas = File.ReadAllLines(caminho, Encoding.GetEncoding(1252));
            if (linhas.Length == 0) return dt;

            char sep = linhas[0].Contains(';') ? ';' : ',';

            bool primeiraLinha = true;

            foreach (var linha in linhas.Skip(1)) // pula cabeçalho
            {
                if (string.IsNullOrWhiteSpace(linha)) continue;

                var dados = linha.Split(sep);
                string nome = dados.Length > 0 ? dados[0].Trim().Trim('"') : "";
                string email = dados.Length > 1 ? dados[1].Trim().Trim('"') : "";
                string numeroAluno = dados.Length > 2 ? dados[2].Trim().Trim('"') : "";
                string nomeResponsavel = dados.Length > 3 ? dados[3].Trim().Trim('"') : "";
                string numeroResponsavel = dados.Length > 4 ? dados[4].Trim().Trim('"') : "";
                string ativo = dados.Length > 5 ? dados[5].Trim().Trim('"') : "1";

                if (string.IsNullOrWhiteSpace(nome)) continue;

                if (primeiraLinha)
                {
                    primeiraLinha = false;

                    var sb = new StringBuilder();
                    sb.AppendLine("Verifique se os dados estão na coluna correta:\n");
                    sb.AppendLine($"  Nome:               \"{Truncar(nome, 40)}\"");
                    sb.AppendLine($"  E-mail:             \"{Truncar(email, 40)}\"");
                    sb.AppendLine($"  Nº Aluno:           \"{Truncar(numeroAluno, 30)}\"");
                    sb.AppendLine($"  Responsável:        \"{Truncar(nomeResponsavel, 40)}\"");
                    sb.AppendLine($"  Nº Responsável:     \"{Truncar(numeroResponsavel, 30)}\"");
                    sb.AppendLine($"  Ativo:              \"{ativo}\"");
                    sb.AppendLine();
                    sb.AppendLine("Os dados estão corretos?");

                    var resultado = MessageBox.Show(
                        sb.ToString(),
                        "Verificação de Colunas",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.No)
                    {
                        MessageBox.Show(
                            "Importação cancelada.\n\n" +
                            "O arquivo CSV precisa ter as colunas nesta ordem:\n" +
                            "  1 = Nome do aluno\n" +
                            "  2 = E-mail\n" +
                            "  3 = Número do aluno\n" +
                            "  4 = Nome do responsável\n" +
                            "  5 = Número do responsável\n" +
                            "  6 = Ativo (1 ou 0)",
                            "Importação Cancelada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return dt;
                    }
                }

                ativo = ativo == "1" || ativo.Equals("sim", StringComparison.OrdinalIgnoreCase) ? "1" : "0";
                dt.Rows.Add(nome, email, numeroAluno, nomeResponsavel, numeroResponsavel, ativo);
            }

            return dt;
        }

        private static string Truncar(string texto, int maxLength)
        {
            if (string.IsNullOrEmpty(texto)) return "";
            return texto.Length <= maxLength ? texto : texto[..maxLength] + "...";
        }

        private void btnImportarDados_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Selecione o arquivo para importar",
                Filter = "Arquivos Suportados (*.xls, *.xlsx, *.csv)|*.xls;*.xlsx;*.csv"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                string ext = Path.GetExtension(ofd.FileName).ToLower();

                _dtPreview = ext == ".csv"
                    ? LerCsvParaDataTable(ofd.FileName)
                    : LerExcelParaDataTable(ofd.FileName);

                if (_dtPreview == null || _dtPreview.Rows.Count == 0)
                {
                    MessageBox.Show("Nenhum dado encontrado no arquivo.",
                        "Importação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mostra prévia no grid
                dgvRegistro.DataSource = _dtPreview;
                FormatarGridRegistro();

                MessageBox.Show($"Prévia carregada: {_dtPreview.Rows.Count} linhas.\nConfirme para gravar no banco.",
                    "Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var confirmar = MessageBox.Show(
                    $"Deseja gravar {_dtPreview.Rows.Count} registros no banco?\nRegistros duplicados (mesmo nome) serão atualizados.",
                    "Confirmar Importação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmar != DialogResult.Yes)
                {
                    //AtualizarGrid();
                    return;
                }

                SalvarPreviewNoBanco();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao importar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SalvarPreviewNoBanco()
        {
            if (_dtPreview == null || _dtPreview.Rows.Count == 0) return;

            int inseridos = 0;
            int atualizados = 0;

            using var conn = new SqliteConnection(Db.ConnectionString);
            conn.Open();
            using var tx = conn.BeginTransaction();

            foreach (DataRow row in _dtPreview.Rows)
            {
                string nome = row["nome"]?.ToString()?.Trim() ?? "";
                string email = row["email"]?.ToString()?.Trim() ?? "";
                string numeroAluno = row["numero_aluno"]?.ToString()?.Trim() ?? "";
                string nomeResponsavel = row["NomeResponsavel"]?.ToString()?.Trim() ?? "";
                string numeroResponsavel = row["numero_responsavel"]?.ToString()?.Trim() ?? "";
                string ativoStr = row["ativo"]?.ToString()?.Trim() ?? "1";
                int ativo = int.TryParse(ativoStr, out int a) ? a : 1;

                if (string.IsNullOrWhiteSpace(nome)) continue;

                // Tenta atualizar primeiro (evita duplicata por nome)
                const string sqlUpdate = @"
                    UPDATE alunos
                    SET email = @email,
                        numero_aluno = @numero_aluno,
                        NomeResponsavel = @NomeResponsavel,
                        numero_responsavel = @numero_responsavel,
                        ativo = @ativo
                    WHERE nome = @nome";

                using (var cmdUpdate = new SqliteCommand(sqlUpdate, conn, tx))
                {
                    cmdUpdate.Parameters.AddWithValue("@nome", nome);
                    cmdUpdate.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : email);
                    cmdUpdate.Parameters.AddWithValue("@numero_aluno", string.IsNullOrWhiteSpace(numeroAluno) ? DBNull.Value : numeroAluno);
                    cmdUpdate.Parameters.AddWithValue("@NomeResponsavel", string.IsNullOrWhiteSpace(nomeResponsavel) ? DBNull.Value : nomeResponsavel);
                    cmdUpdate.Parameters.AddWithValue("@numero_responsavel", string.IsNullOrWhiteSpace(numeroResponsavel) ? DBNull.Value : numeroResponsavel);
                    cmdUpdate.Parameters.AddWithValue("@ativo", ativo);

                    int rowsAffected = cmdUpdate.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        atualizados++;
                        continue;
                    }
                }

                // Se não atualizou nenhuma linha, insere
                const string sqlInsert = @"
                    INSERT INTO alunos (nome, email, numero_aluno, NomeResponsavel, numero_responsavel, ativo)
                    VALUES (@nome, @email, @numero_aluno, @NomeResponsavel, @numero_responsavel, @ativo)";

                using var cmdInsert = new SqliteCommand(sqlInsert, conn, tx);
                cmdInsert.Parameters.AddWithValue("@nome", nome);
                cmdInsert.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : email);
                cmdInsert.Parameters.AddWithValue("@numero_aluno", string.IsNullOrWhiteSpace(numeroAluno) ? DBNull.Value : numeroAluno);
                cmdInsert.Parameters.AddWithValue("@NomeResponsavel", string.IsNullOrWhiteSpace(nomeResponsavel) ? DBNull.Value : nomeResponsavel);
                cmdInsert.Parameters.AddWithValue("@numero_responsavel", string.IsNullOrWhiteSpace(numeroResponsavel) ? DBNull.Value : numeroResponsavel);
                cmdInsert.Parameters.AddWithValue("@ativo", ativo);
                cmdInsert.ExecuteNonQuery();
                inseridos++;
            }

            tx.Commit();

            MessageBox.Show(
                $"Importação concluída!\n\nNovos registros: {inseridos}\nAtualizados (duplicados): {atualizados}",
                "Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _dtPreview = null;
            //AtualizarGrid();
        }

        private void AjustarColunaTexto(DataGridView g, string nome, string header, int minWidth, DataGridViewContentAlignment alignment)
        {
            if (g?.Columns == null || !g.Columns.Contains(nome)) return;
            var coluna = g.Columns[nome];
            if (coluna != null)
            {
                coluna.HeaderText = header;
                coluna.MinimumWidth = minWidth;
                coluna.DefaultCellStyle.Alignment = alignment;
            }
        }

        private void AjustarColunaNumero(DataGridView g, string nome, string header, int minWidth)
        {
            if (g?.Columns == null || !g.Columns.Contains(nome)) return;
            var coluna = g.Columns[nome];
            if (coluna != null)
            {
                coluna.HeaderText = header;
                coluna.MinimumWidth = minWidth;
                coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void CentralizarSeExistir(DataGridView g, string nomeColuna)
        {
            if (g?.Columns != null && g.Columns.Contains(nomeColuna))
            {
                var coluna = g.Columns[nomeColuna];
                if (coluna != null)
                    coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dgvRegistro_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            // evita duplo clique no cabeçalho
            if (e.RowIndex < 0) return;

            var row = dgvRegistro.Rows[e.RowIndex];

            // pega o ID da linha clicada
            var idObj = row.Cells["id"].Value;
            if (idObj == null) return;

            int id = Convert.ToInt32(idObj);

            string nome = row.Cells["nome"].Value?.ToString() ?? "";
            string email = row.Cells["email"].Value?.ToString() ?? "";
            string numeroAluno = row.Cells["numero_aluno"].Value?.ToString() ?? "";
            string nomeResponsavel = row.Cells["NomeResponsavel"].Value?.ToString() ?? "";
            string numeroResponsavel = row.Cells["numero_responsavel"].Value?.ToString() ?? "";
            bool ativo = Convert.ToInt32(row.Cells["ativo"].Value ?? 1) == 1;

            using var frm = new FrmAluno();
            frm.CarregarAluno(id, nome, email, numeroAluno, nomeResponsavel, numeroResponsavel, ativo);
            var result = frm.ShowDialog();

            // se salvou, recarrega
            /*if (result == DialogResult.OK) 
                AtualizarGrid();*/
        }

        private void btnPrepAquivos_Click(object sender, EventArgs e)
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
                string scriptPy = Path.Combine(pastaApp, "Scripts", "limpar_colunas_excel_alunos.py");

                if (!File.Exists(scriptPy))
                {
                    MessageBox.Show(
                        $"Script não encontrado em:\n{scriptPy}\n\n" +
                        "Coloque o arquivo 'limpar_colunas_excel_alunos.py' na pasta 'Scripts' junto ao executável.",
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
                    MessageBox.Show("Não foi possível iniciar o processo Python.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string saida = proc.StandardOutput.ReadToEnd();
                string erro = proc.StandardError.ReadToEnd();
                proc.WaitForExit();

                if (proc.ExitCode == 0)
                    MessageBox.Show("OK!\n" + saida, "Preparar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Deu erro:\n" + erro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Procura o executável do Python em locais comuns e no PATH do sistema.
        /// </summary>
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

        /// <summary>
        /// Verifica se um executável existe no PATH do sistema.
        /// </summary>
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