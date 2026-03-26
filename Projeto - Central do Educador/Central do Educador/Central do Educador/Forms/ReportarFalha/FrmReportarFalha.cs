using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmReportarFalha : Form
    {
        public FrmReportarFalha()
        {
            InitializeComponent();
        }

        private void FrmReportarFalha_Load(object sender, EventArgs e)
        {
            cmbCategoria.Items.AddRange(new object[] { "Bug", "Melhoria", "Dúvida", "Outro" });
            cmbCategoria.SelectedIndex = 0;

            CarregarMeusRelatos();
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                var titulo = (txtTitulo.Text ?? "").Trim();
                var descricao = (txtDescricao.Text ?? "").Trim();
                var categoria = cmbCategoria.SelectedItem?.ToString() ?? "Bug";

                if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descricao))
                {
                    MessageBox.Show("Preencha o título e a descrição.", "Validação",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await Db.ExecuteNonQueryAsync(
                    @"INSERT INTO relatos_falhas (usuario_id, usuario_nome, titulo, descricao, categoria)
                      VALUES (@uid, @nome, @titulo, @desc, @cat);",
                    [
                        Db.P("@uid", Sessao.UsuarioId),
                        Db.P("@nome", Sessao.UsuarioNome),
                        Db.P("@titulo", titulo),
                        Db.P("@desc", descricao),
                        Db.P("@cat", categoria),
                    ]);

                MessageBox.Show("Falha reportada com sucesso! O administrador será notificado.",
                    "Enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtTitulo.Clear();
                txtDescricao.Clear();
                cmbCategoria.SelectedIndex = 0;

                CarregarMeusRelatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar relato: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CarregarMeusRelatos()
        {
            try
            {
                var dt = await Db.QueryDataTableAsync(
                    @"SELECT id AS Id, titulo AS Título, categoria AS Categoria,
                             status AS Status, criado_em AS 'Data',
                             COALESCE(resposta_adm, '') AS 'Resposta ADM'
                      FROM relatos_falhas
                      WHERE usuario_id = @uid
                      ORDER BY id DESC;",
                    [Db.P("@uid", Sessao.UsuarioId)]);

                dgvMeusRelatos.AutoGenerateColumns = true;
                dgvMeusRelatos.Columns.Clear();
                dgvMeusRelatos.DataSource = dt;

                dgvMeusRelatos.ReadOnly = true;
                dgvMeusRelatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMeusRelatos.MultiSelect = false;
                dgvMeusRelatos.RowHeadersVisible = false;
                dgvMeusRelatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvMeusRelatos.AllowUserToAddRows = false;

                if (dgvMeusRelatos.Columns.Contains("Id"))
                    dgvMeusRelatos.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar relatos: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
