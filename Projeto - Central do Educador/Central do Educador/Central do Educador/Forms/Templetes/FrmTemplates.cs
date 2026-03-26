using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmTemplates : Form
    {
        private int _idEmEdicao = 0;

        public FrmTemplates()
        {
            InitializeComponent();
        }

        private async void FrmTemplates_Load(object? sender, EventArgs e)
        {
            await AtualizarGridAsync();
        }

        // ══════════════════════════════════════════════
        //  CARREGAR GRID
        // ══════════════════════════════════════════════

        private async Task AtualizarGridAsync()
        {
            try
            {
                using var conn = new SqliteConnection(Db.ConnectionString);
                await conn.OpenAsync();

                const string sql = @"
                    SELECT
                        id      AS Id,
                        codigo  AS Codigo,
                        canal   AS Canal,
                        assunto AS Assunto,
                        corpo   AS Corpo,
                        CASE WHEN ativo = 1 THEN 'Sim' ELSE 'Não' END AS Ativo
                    FROM templates_mensagem
                    ORDER BY codigo, canal;";

                using var cmd = new SqliteCommand(sql, conn);
                var dt = new DataTable();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }

                dgvTemplates.DataSource = dt;
                FormatarGrid();

                lblTotal.Text = $"Total de templates: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar templates: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════════
        //  NOVO
        // ══════════════════════════════════════════════

        private void btnNovo_Click(object? sender, EventArgs e)
        {
            LimparFormulario();
            txtCodigo.Focus();
        }

        // ══════════════════════════════════════════════
        //  SALVAR (Novo + Edição)
        // ══════════════════════════════════════════════

        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim().ToUpper().Replace(" ", "_");
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Informe o código do template.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return;
            }

            string canal = cmbCanal.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(canal))
            {
                MessageBox.Show("Selecione o canal.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCanal.Focus();
                return;
            }

            string corpo = txtCorpo.Text.Trim();
            if (string.IsNullOrWhiteSpace(corpo))
            {
                MessageBox.Show("O corpo da mensagem não pode estar vazio.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorpo.Focus();
                return;
            }

            string? assunto = txtAssunto.Text.Trim();
            if (string.IsNullOrWhiteSpace(assunto)) assunto = null;

            int ativo = chkAtivo.Checked ? 1 : 0;

            try
            {
                using var conn = new SqliteConnection(Db.ConnectionString);
                await conn.OpenAsync();

                if (_idEmEdicao > 0)
                {
                    const string sqlUpdate = @"
                        UPDATE templates_mensagem
                        SET codigo  = @codigo,
                            canal   = @canal,
                            assunto = @assunto,
                            corpo   = @corpo,
                            ativo   = @ativo
                        WHERE id = @id;";

                    using var cmd = new SqliteCommand(sqlUpdate, conn);
                    cmd.Parameters.AddWithValue("@id", _idEmEdicao);
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.Parameters.AddWithValue("@canal", canal);
                    cmd.Parameters.AddWithValue("@assunto", (object?)assunto ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@corpo", corpo);
                    cmd.Parameters.AddWithValue("@ativo", ativo);
                    await cmd.ExecuteNonQueryAsync();

                    MessageBox.Show("Template atualizado!", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    const string sqlInsert = @"
                        INSERT INTO templates_mensagem (codigo, canal, assunto, corpo, ativo)
                        VALUES (@codigo, @canal, @assunto, @corpo, @ativo);";

                    using var cmd = new SqliteCommand(sqlInsert, conn);
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.Parameters.AddWithValue("@canal", canal);
                    cmd.Parameters.AddWithValue("@assunto", (object?)assunto ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@corpo", corpo);
                    cmd.Parameters.AddWithValue("@ativo", ativo);
                    await cmd.ExecuteNonQueryAsync();

                    MessageBox.Show("Template criado!", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimparFormulario();
                await AtualizarGridAsync();
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 19) // UNIQUE constraint
            {
                MessageBox.Show(
                    $"Já existe um template com código '{codigo}' para o canal '{canal}'.\nAltere o código ou o canal.",
                    "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════════
        //  EXCLUIR
        // ══════════════════════════════════════════════

        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
            if (dgvTemplates.CurrentRow == null)
            {
                MessageBox.Show("Selecione um template.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idObj = dgvTemplates.CurrentRow.Cells["Id"]?.Value;
            if (idObj == null || !int.TryParse(idObj.ToString(), out int id) || id <= 0)
            {
                MessageBox.Show("Não foi possível identificar o registro.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string codigo = dgvTemplates.CurrentRow.Cells["Codigo"]?.Value?.ToString() ?? "";
            string canal = dgvTemplates.CurrentRow.Cells["Canal"]?.Value?.ToString() ?? "";

            var resp = MessageBox.Show(
                $"Excluir o template '{codigo}' ({canal})?\n\nNotificações já geradas com este template não serão afetadas.",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resp != DialogResult.Yes) return;

            try
            {
                using var conn = new SqliteConnection(Db.ConnectionString);
                await conn.OpenAsync();

                using var cmd = new SqliteCommand(
                    "DELETE FROM templates_mensagem WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                await cmd.ExecuteNonQueryAsync();

                MessageBox.Show("Template excluído!", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparFormulario();
                await AtualizarGridAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════════
        //  DUPLO CLIQUE → carregar no formulário
        // ══════════════════════════════════════════════

        private void dgvTemplates_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvTemplates.Rows[e.RowIndex];

            var idObj = row.Cells["Id"]?.Value;
            if (idObj == null || !int.TryParse(idObj.ToString(), out int id) || id <= 0) return;

            _idEmEdicao = id;

            txtCodigo.Text = row.Cells["Codigo"]?.Value?.ToString() ?? "";

            string canal = row.Cells["Canal"]?.Value?.ToString() ?? "";
            for (int i = 0; i < cmbCanal.Items.Count; i++)
            {
                if (string.Equals(cmbCanal.Items[i].ToString(), canal, StringComparison.OrdinalIgnoreCase))
                { cmbCanal.SelectedIndex = i; break; }
            }

            txtAssunto.Text = row.Cells["Assunto"]?.Value?.ToString() ?? "";
            txtCorpo.Text = row.Cells["Corpo"]?.Value?.ToString() ?? "";

            string ativoStr = row.Cells["Ativo"]?.Value?.ToString() ?? "Sim";
            chkAtivo.Checked = ativoStr == "Sim";

            Text = $"Templates — Editando #{id}";
            txtCodigo.Focus();
        }

        // ══════════════════════════════════════════════
        //  CANAL ALTERADO → mostra/oculta assunto
        // ══════════════════════════════════════════════

        private void cmbCanal_SelectedIndexChanged(object? sender, EventArgs e)
        {
            bool isEmail = cmbCanal.SelectedItem?.ToString() == "EMAIL";
            lblAssunto.Visible = isEmail;
            txtAssunto.Visible = isEmail;
        }

        // ══════════════════════════════════════════════
        //  LIMPAR FORMULÁRIO
        // ══════════════════════════════════════════════

        private void LimparFormulario()
        {
            _idEmEdicao = 0;
            txtCodigo.Text = "";
            cmbCanal.SelectedIndex = 0;
            txtAssunto.Text = "";
            txtCorpo.Text = "";
            chkAtivo.Checked = true;
            Text = "Templates de Mensagem";
        }

        // ══════════════════════════════════════════════
        //  PREVIEW AO VIVO
        // ══════════════════════════════════════════════

        private void txtCorpo_TextChanged(object? sender, EventArgs e)
        {
            string preview = txtCorpo.Text
                .Replace("{aluno}", "Maria Silva")
                .Replace("{tipo}", "Reposição")
                .Replace("{data_hora}", DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm"))
                .Replace("{local}", "Sala 3")
                .Replace("{observacao}", "Aula de matemática")
                .Replace("{status}", "CONFIRMADO");

            txtPreview.Text = preview;
        }

        // ══════════════════════════════════════════════
        //  FORMATAÇÃO DO GRID
        // ══════════════════════════════════════════════

        private void FormatarGrid()
        {
            var g = dgvTemplates;
            if (g.Columns.Count == 0) return;

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

            if (g.Columns.Contains("Id"))
                g.Columns["Id"].Visible = false;

            // Ocultar corpo no grid (muito longo)
            if (g.Columns.Contains("Corpo"))
                g.Columns["Corpo"].Visible = false;

            AjustarColuna(g, "Codigo", "Código", 220, DataGridViewContentAlignment.MiddleLeft);
            AjustarColuna(g, "Canal", "Canal", 100, DataGridViewContentAlignment.MiddleCenter);
            AjustarColuna(g, "Assunto", "Assunto", 250, DataGridViewContentAlignment.MiddleLeft);
            AjustarColuna(g, "Ativo", "Ativo", 60, DataGridViewContentAlignment.MiddleCenter);

            g.CellFormatting -= DgvTemplates_CellFormatting;
            g.CellFormatting += DgvTemplates_CellFormatting;
        }

        private void DgvTemplates_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (sender is not DataGridView g) return;
            if (e.ColumnIndex < 0 || e.ColumnIndex >= g.Columns.Count) return;

            string col = g.Columns[e.ColumnIndex].Name;

            if (col == "Canal" && e.Value != null)
            {
                string canal = e.Value.ToString()?.ToUpper() ?? "";
                var row = g.Rows[e.RowIndex];

                row.DefaultCellStyle.ForeColor = canal switch
                {
                    "EMAIL" => Color.FromArgb(0, 120, 215),
                    "WHATSAPP" => Color.FromArgb(37, 211, 102),
                    _ => Color.Black,
                };
            }

            if (col == "Ativo" && e.Value != null)
            {
                string ativo = e.Value.ToString() ?? "";
                var cell = g.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Style.ForeColor = ativo == "Sim" ? Color.DarkGreen : Color.Red;
            }
        }

        private static void AjustarColuna(DataGridView g, string nome, string header,
            int minWidth, DataGridViewContentAlignment align)
        {
            if (!g.Columns.Contains(nome)) return;
            var c = g.Columns[nome];
            c.HeaderText = header;
            c.MinimumWidth = minWidth;
            c.DefaultCellStyle.Alignment = align;
        }
    }
}
