using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmGerenciarFalhas : Form
    {
        public FrmGerenciarFalhas()
        {
            InitializeComponent();
        }

        private void FrmGerenciarFalhas_Load(object sender, EventArgs e)
        {
            cmbFiltroStatus.Items.AddRange(new object[] { "Todos", "ABERTO", "EM ANDAMENTO", "RESOLVIDO", "REJEITADO" });
            cmbFiltroStatus.SelectedIndex = 0;

            CarregarRelatos();
        }

        private async void CarregarRelatos()
        {
            try
            {
                var filtro = cmbFiltroStatus.SelectedItem?.ToString() ?? "Todos";

                string sql = @"
                    SELECT id AS Id, usuario_nome AS Usuário, titulo AS Título,
                           categoria AS Categoria, status AS Status,
                           criado_em AS 'Data Criação',
                           COALESCE(resposta_adm, '') AS 'Resposta ADM'
                    FROM relatos_falhas
                    WHERE 1=1 ";

                var parametros = new System.Collections.Generic.List<SqliteParameter>();

                if (filtro != "Todos")
                {
                    sql += " AND status = @status ";
                    parametros.Add(Db.P("@status", filtro));
                }

                sql += " ORDER BY id DESC;";

                var dt = await Db.QueryDataTableAsync(sql, parametros);

                dgvFalhas.AutoGenerateColumns = true;
                dgvFalhas.Columns.Clear();
                dgvFalhas.DataSource = dt;

                dgvFalhas.ReadOnly = true;
                dgvFalhas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvFalhas.MultiSelect = false;
                dgvFalhas.RowHeadersVisible = false;
                dgvFalhas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvFalhas.AllowUserToAddRows = false;

                lblTotal.Text = $"Total: {dt.Rows.Count} relato(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar relatos: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbFiltroStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarRelatos();
        }

        private void dgvFalhas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvFalhas.Rows[e.RowIndex];
            txtRespostaAdm.Text = row.Cells["Resposta ADM"]?.Value?.ToString() ?? "";

            var statusAtual = row.Cells["Status"]?.Value?.ToString() ?? "ABERTO";
            for (int i = 0; i < cmbNovoStatus.Items.Count; i++)
            {
                if (cmbNovoStatus.Items[i].ToString() == statusAtual)
                {
                    cmbNovoStatus.SelectedIndex = i;
                    break;
                }
            }
        }

        private async void btnSalvarResposta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFalhas.CurrentRow == null)
                {
                    MessageBox.Show("Selecione um relato no grid.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var idObj = dgvFalhas.CurrentRow.Cells["Id"]?.Value;
                if (idObj == null || !int.TryParse(idObj.ToString(), out int id) || id <= 0)
                    return;

                var novoStatus = cmbNovoStatus.SelectedItem?.ToString() ?? "ABERTO";
                var resposta = (txtRespostaAdm.Text ?? "").Trim();

                await Db.ExecuteNonQueryAsync(
                    @"UPDATE relatos_falhas
                      SET status = @status,
                          resposta_adm = @resp,
                          atualizado_em = datetime('now','localtime')
                      WHERE id = @id;",
                    [
                        Db.P("@status", novoStatus),
                        Db.P("@resp", string.IsNullOrWhiteSpace(resposta) ? DBNull.Value : resposta),
                        Db.P("@id", id),
                    ]);

                MessageBox.Show("Resposta salva com sucesso!", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarRelatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExcluirRelato_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFalhas.CurrentRow == null) return;

                var idObj = dgvFalhas.CurrentRow.Cells["Id"]?.Value;
                if (idObj == null || !int.TryParse(idObj.ToString(), out int id) || id <= 0)
                    return;

                var resp = MessageBox.Show("Excluir este relato permanentemente?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp != DialogResult.Yes) return;

                await Db.ExecuteNonQueryAsync(
                    "DELETE FROM relatos_falhas WHERE id = @id;",
                    [Db.P("@id", id)]);

                CarregarRelatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
