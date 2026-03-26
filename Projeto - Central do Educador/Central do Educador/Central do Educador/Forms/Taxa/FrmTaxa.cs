using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Central_do_Educador
{
    public partial class FrmTaxa : Form
    {
        private readonly string _usuario;
        private int _idEmEdicao;
        private bool _carregado = false;

        public FrmTaxa(string usuario) : this(usuario, 0) { }

        public FrmTaxa(string usuario, int idParaEditar)
        {
            InitializeComponent();
            _usuario = usuario ?? "";
            _idEmEdicao = idParaEditar;
        }

        private async Task CarregarAlunosAsync()
        {
            using var con = new SqliteConnection(Db.ConnectionString);
            await con.OpenAsync();

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                SELECT id, nome
                FROM alunos
                ORDER BY nome;
            ";

            using var reader = (SqliteDataReader)await cmd.ExecuteReaderAsync();

            var dt = new System.Data.DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("nome", typeof(string));

            while (await reader.ReadAsync())
            {
                object idObj = reader["id"];
                int id = 0;
                if (idObj != null && idObj != DBNull.Value)
                {
                    // SQLite retorna long; converte com segurança
                    id = Convert.ToInt32(Convert.ToInt64(idObj));
                }

                string nome = reader["nome"]?.ToString() ?? string.Empty;
                dt.Rows.Add(id, nome);
            }

            // Vincula ao ComboBox. Se houver texto atual (por exemplo, vindo de edição),
            // o ComboBox tentará casar pelo DisplayMember quando atribuirmos Text.
            cmbAluno.DisplayMember = "nome";
            cmbAluno.ValueMember = "id";
            cmbAluno.DataSource = dt; // DataTable com id,nome
        }

        private async void FrmControleTaxa_Load(object sender, EventArgs e)
        {
            if (_carregado) return;
            _carregado = true;

            try
            {
                // 1) Sempre carrega a lista antes
                await CarregarAlunosAsync();

                // 2) Se for edição, agora sim carrega e seleciona no combo
                if (_idEmEdicao > 0)
                    await CarregarParaEdicaoAsync(_idEmEdicao);

                // (Opcional) Se você quiser que txtAluno acompanhe o combo
                cmbAluno.SelectionChangeCommitted -= cmbAluno_SelectionChangeCommitted;
                cmbAluno.SelectionChangeCommitted += cmbAluno_SelectionChangeCommitted;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Erro ao carregar tela: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbAluno_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Atualiza txtAluno com o nome selecionado ou com o texto livre do combo
            if (cmbAluno.SelectedItem is System.Data.DataRowView drv)
            {
                cmbAluno.Text = drv["nome"]?.ToString() ?? string.Empty;
            }
            else
            {
                // Quando o usuário digita um nome que não está na lista
                cmbAluno.Text = cmbAluno.Text ?? string.Empty;
            }
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                int? alunoId = null;
                if (cmbAluno?.SelectedValue != null && int.TryParse(cmbAluno.SelectedValue.ToString(), out var idSel))
                    alunoId = idSel;

                string nomeAluno = cmbAluno?.Text?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(nomeAluno))
                {
                    MessageBox.Show("Selecione um aluno.");
                    cmbAluno?.Focus();
                    return;
                }

                // Trabalhar com DateTime localmente
                DateTime dataFalta = dtpFalta.Value.Date;
                DateTime dataRegistro = dtpRegistro.Value.Date;
                DateTime? dataReposicaoDt = chkNaoAgendado.Checked ? null : dtpReposicao.Value.Date;

                int quantidade = (int)nudQtd.Value;
                bool lancado = chkLancado.Checked;
                bool historico = chkHistorico.Checked;
                object observacao = string.IsNullOrWhiteSpace(txtObs.Text) ? DBNull.Value : txtObs.Text.Trim();

                string usuarioResp = !string.IsNullOrWhiteSpace(Sessao.UsuarioNome)
                    ? Sessao.UsuarioNome
                    : (_usuario ?? string.Empty);

                int idAtual = _idEmEdicao;
                int idRetorno = 0;

                using (var con = new SqliteConnection(Db.ConnectionString))
                {
                    await con.OpenAsync();
                    using var tx = con.BeginTransaction();
                    try
                    {
                        // Converter para string ISO (yyyy-MM-dd) para armazenar apenas a data
                        string dataFaltaParam = dataFalta.ToString("yyyy-MM-dd");
                        string dataRegistroParam = dataRegistro.ToString("yyyy-MM-dd");
                        object dataReposicaoParam = dataReposicaoDt.HasValue
                            ? (object)dataReposicaoDt.Value.ToString("yyyy-MM-dd")
                            : DBNull.Value;

                        if (idAtual > 0)
                        {
                            const string sqlUpdate = @"
                                UPDATE reposicao
                                SET
                                    aluno_id = @aluno_id,
                                    aluno = @aluno,
                                    data_falta = @data_falta,
                                    data_registro = @data_registro,
                                    Data_Reposicao = @data_reposicao,
                                    quantidade = @quantidade,
                                    lancado = @lancado,
                                    Observacao = @observacao,
                                    Historico = @historico,
                                    UsuarioResponsavel = @usuario
                                WHERE id = @id;
                                ";
                            using var cmd = con.CreateCommand();
                            cmd.Transaction = tx;
                            cmd.CommandText = sqlUpdate;
                            cmd.Parameters.AddWithValue("@id", idAtual);
                            cmd.Parameters.AddWithValue("@aluno_id", (object?)alunoId ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@aluno", nomeAluno);
                            cmd.Parameters.AddWithValue("@data_falta", dataFaltaParam);
                            cmd.Parameters.AddWithValue("@data_registro", dataRegistroParam);
                            cmd.Parameters.AddWithValue("@data_reposicao", dataReposicaoParam);
                            cmd.Parameters.AddWithValue("@quantidade", quantidade);
                            cmd.Parameters.AddWithValue("@lancado", lancado ? 1 : 0);
                            cmd.Parameters.AddWithValue("@observacao", observacao);
                            cmd.Parameters.AddWithValue("@historico", historico ? 1 : 0); // preserva semântico
                            cmd.Parameters.AddWithValue("@usuario", usuarioResp);

                            var rows = await cmd.ExecuteNonQueryAsync();
                            idRetorno = rows > 0 ? idAtual : 0;
                        }
                        else
                        {
                            const string sqlInsert = @"
                                INSERT INTO reposicao
                                (aluno_id, aluno, data_falta, data_registro, Data_Reposicao, quantidade, lancado, Observacao, Historico, UsuarioResponsavel)
                                VALUES
                                (@aluno_id, @aluno, @data_falta, @data_registro, @data_reposicao, @quantidade, @lancado, @observacao, @historico, @usuario);
                                ";
                            using var cmd = con.CreateCommand();
                            cmd.Transaction = tx;
                            cmd.CommandText = sqlInsert;
                            cmd.Parameters.AddWithValue("@aluno_id", (object?)alunoId ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@aluno", nomeAluno);
                            cmd.Parameters.AddWithValue("@data_falta", dataFaltaParam);
                            cmd.Parameters.AddWithValue("@data_registro", dataRegistroParam);
                            cmd.Parameters.AddWithValue("@data_reposicao", dataReposicaoParam);
                            cmd.Parameters.AddWithValue("@quantidade", quantidade);
                            cmd.Parameters.AddWithValue("@lancado", lancado ? 1 : 0);
                            cmd.Parameters.AddWithValue("@observacao", observacao);
                            cmd.Parameters.AddWithValue("@historico", historico ? 1 : 0);
                            cmd.Parameters.AddWithValue("@usuario", usuarioResp);

                            await cmd.ExecuteNonQueryAsync();

                            // Recupera o id inserido via função SQLite padrão
                            using var cmdLast = con.CreateCommand();
                            cmdLast.Transaction = tx;
                            cmdLast.CommandText = "SELECT last_insert_rowid();";
                            var result = await cmdLast.ExecuteScalarAsync();
                            long lastId = result == null || result == DBNull.Value ? 0 : Convert.ToInt64(result);
                            idRetorno = lastId > 0 ? Convert.ToInt32(lastId) : 0;
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        try { tx.Rollback(); } catch { }
                        throw;
                    }
                }

                if (idRetorno > 0)
                {
                    MessageBox.Show(this, "Registro salvo com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show(this, "Não foi possível salvar o registro.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}");
            }
        }

        private void LimparTela()
        {
            txtId.Text = "";
            cmbAluno.Text = "";

            dtpFalta.Value = DateTime.Today;
            dtpReposicao.Value = DateTime.Today;
            dtpRegistro.Value = DateTime.Today;

            nudQtd.Value = 1;

            chkLancado.Checked = false;
            chkNaoAgendado.Checked = false;
            chkHistorico.Checked = false;

            txtObs.Text = "";
        }

        private async Task<int> ObterUsuarioIdPorLoginAsync(string loginOuNome)
        {
            if (string.IsNullOrWhiteSpace(loginOuNome))
                return 0;

            using (var con = new SqliteConnection(Db.ConnectionString))
            {
                await con.OpenAsync();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id
                        FROM usuarios
                        WHERE login = @u OR nome = @u
                        LIMIT 1;
                        ";
                    ((SqliteCommand)cmd).Parameters.AddWithValue("@u", loginOuNome.Trim());

                    var result = await cmd.ExecuteScalarAsync();
                    if (result == null || result == DBNull.Value)
                        return 0;

                    return Convert.ToInt32(Convert.ToInt64(result));
                }
            }
        }

        private void chkNaoAgendado_CheckedChanged(object sender, EventArgs e)
        {
            dtpReposicao.Enabled = !chkNaoAgendado.Checked;
        }

        private async void FrmControleTaxa_Activated(object sender, EventArgs e)
        {

        }

        private async Task CarregarParaEdicaoAsync(int id)
        {
            using var con = new SqliteConnection(Db.ConnectionString);
            await con.OpenAsync();

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                        SELECT
                            id, aluno_id, data_falta, data_registro, Data_Reposicao,
                            quantidade, lancado, Observacao, Historico, UsuarioResponsavel
                        FROM reposicao
                        WHERE id = @id
                        LIMIT 1";

            ((SqliteCommand)cmd).Parameters.AddWithValue("@id", id);

            using var r = (SqliteDataReader)await cmd.ExecuteReaderAsync();
            if (!await r.ReadAsync())
            {
                MessageBox.Show("Registro não encontrado.");
                Close();
                return;
            }

            // garanta que o cmbAluno já foi carregado antes (CarregarAlunosAsync rodou)
            var alunoIdObj = r["aluno_id"];

            if (cmbAluno != null && alunoIdObj != DBNull.Value && int.TryParse(alunoIdObj.ToString(), out var alunoId))
            {
                cmbAluno.SelectedValue = alunoId;

                // fallback se não existir mais na lista
                if (cmbAluno.SelectedIndex < 0)
                    cmbAluno.Text = r["aluno"]?.ToString() ?? "";
            }
            else
            {
                // fallback para registros antigos
                if (cmbAluno != null)
                    cmbAluno.Text = r["aluno"]?.ToString() ?? "";
            }

            // Função auxiliar para fazer parse seguro de datas armazenadas como texto
            static DateTime ParseDbDate(object? dbValue)
            {
                if (dbValue == null || dbValue == DBNull.Value) return DateTime.Today;

                var s = dbValue.ToString()!.Trim();
                if (string.IsNullOrEmpty(s)) return DateTime.Today;

                // tenta yyyy-MM-dd
                if (s.Length >= 10 && DateTime.TryParseExact(s.Substring(0, 10), "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var dt))
                {
                    return dt;
                }

                // tenta formatos com hora
                if (DateTime.TryParse(s, out dt))
                    return dt;

                return DateTime.Today;
            }

            var dfObj = r["data_falta"];
            dtpFalta.Value = ParseDbDate(dfObj);

            var drgObj = r["data_registro"];
            dtpRegistro.Value = ParseDbDate(drgObj);

            var dr = r["Data_Reposicao"];
            if (dr == DBNull.Value || string.IsNullOrWhiteSpace(dr?.ToString()))
            {
                chkNaoAgendado.Checked = true;
                dtpReposicao.Value = DateTime.Today;
            }
            else
            {
                chkNaoAgendado.Checked = false;
                dtpReposicao.Value = ParseDbDate(dr);
            }

            decimal qtd = 0;
            if (r["quantidade"] != DBNull.Value && decimal.TryParse(r["quantidade"].ToString(), out var q))
                qtd = q;

            nudQtd.Value = Math.Max(nudQtd.Minimum, Math.Min(nudQtd.Maximum, qtd));

            chkLancado.Checked = Convert.ToInt32(r["lancado"]) == 1;
            chkHistorico.Checked = Convert.ToInt32(r["Historico"]) == 1;

            txtObs.Text = r["Observacao"] == DBNull.Value ? "" : r["Observacao"].ToString();
        }

        private void FrmControleTaxa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                this.Close();
            }
        }

        private void cmbAluno_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            // Atualizado quando o usuário confirma a seleção no ComboBox
        }
    }
}