using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Central_do_Educador.Forms
{
    public partial class FrmUsuarios : Form
    {
        private int _idEmEdicao = 0; // 0 = novo | >0 = editando

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void CarregarGrid(string filtroLogin = "")
        {
            using var con = new SqliteConnection(Db.ConnectionString);
            con.Open();

            var sql = @"
                SELECT
                    id AS Id,
                    nome AS Nome,
                    login AS Login,
                    COALESCE(nivel, 'OPERADOR') AS Nivel,
                    CASE WHEN ativo = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status
                FROM usuarios
                WHERE 1=1
                ";

            if (!string.IsNullOrWhiteSpace(filtroLogin))
                sql += " AND login LIKE @filtro ";

            sql += " ORDER BY nome;";

            using var cmd = new SqliteCommand(sql, con);

            if (!string.IsNullOrWhiteSpace(filtroLogin))
                cmd.Parameters.AddWithValue("@filtro", $"%{filtroLogin.Trim()}%");

            var dt = new DataTable();
            using (var reader = cmd.ExecuteReader())
            {
                dt.Load(reader);
            }

            dgvUsuarios.AutoGenerateColumns = true;
            dgvUsuarios.Columns.Clear();
            dgvUsuarios.DataSource = dt;

            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvUsuarios.Columns.Contains("Id"))
                dgvUsuarios.Columns["Id"].FillWeight = 20;
        }


        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            cmbNivel.Items.AddRange(new object[] { "ADM", "OPERADOR", "VISUALIZADOR" });
            cmbNivel.SelectedIndex = 1; // padrão: OPERADOR

            CarregarGrid();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarGrid(txtFiltroLogin.Text);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtLogin.Clear();
            txtSenha.Clear();
            txtNome.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                var nome = (txtNome.Text ?? "").Trim();
                var login = (txtLogin.Text ?? "").Trim();
                var senha = (txtSenha.Text ?? "").Trim();
                var nivel = cmbNivel.SelectedItem?.ToString() ?? "OPERADOR";

                if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(login))
                {
                    MessageBox.Show("Informe Nome e Login.", "Validação",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var con = new SqliteConnection(Db.ConnectionString);
                con.Open();

                if (_idEmEdicao == 0)
                {
                    if (string.IsNullOrWhiteSpace(senha))
                    {
                        MessageBox.Show("Informe uma senha para o novo usuário.", "Validação",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var hash = HashSenha(senha);

                    using var cmd = con.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO usuarios (nome, login, senha_hash, nivel, ativo)
                        VALUES (@nome, @login, @hash, @nivel, 1);
                        ";
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@hash", hash);
                    cmd.Parameters.AddWithValue("@nivel", nivel);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuário criado com sucesso!", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using var cmd = con.CreateCommand();

                    if (string.IsNullOrWhiteSpace(senha))
                    {
                        cmd.CommandText = @"
                            UPDATE usuarios
                            SET nome = @nome, login = @login, nivel = @nivel
                            WHERE id = @id;
                            ";
                        cmd.Parameters.AddWithValue("@id", _idEmEdicao);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@nivel", nivel);
                    }
                    else
                    {
                        var hash = HashSenha(senha);

                        cmd.CommandText = @"
                            UPDATE usuarios
                            SET nome = @nome, login = @login, senha_hash = @hash, nivel = @nivel
                            WHERE id = @id;
                            ";
                        cmd.Parameters.AddWithValue("@id", _idEmEdicao);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@hash", hash);
                        cmd.Parameters.AddWithValue("@nivel", nivel);
                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuário atualizado com sucesso!", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                btnNovo_Click(sender, e);
                _idEmEdicao = 0;
                CarregarGrid(txtFiltroLogin.Text);
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
            {
                MessageBox.Show("Esse login já existe. Escolha outro.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtivarDesativar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idEmEdicao <= 0)
                {
                    MessageBox.Show("Selecione um usuário no grid.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var con = new SqliteConnection(Db.ConnectionString);
                con.Open();

                // pega status atual
                using var cmdGet = con.CreateCommand();
                cmdGet.CommandText = "SELECT ativo FROM usuarios WHERE id = @id;";
                cmdGet.Parameters.AddWithValue("@id", _idEmEdicao);
                var atualObj = cmdGet.ExecuteScalar();
                var atual = Convert.ToInt32(atualObj ?? 1);

                var novo = atual == 1 ? 0 : 1;

                var resp = MessageBox.Show(
                    novo == 1 ? "Ativar este usuário?" : "Desativar este usuário?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp != DialogResult.Yes) return;

                using var cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE usuarios SET ativo = @ativo WHERE id = @id;";
                cmd.Parameters.AddWithValue("@ativo", novo);
                cmd.Parameters.AddWithValue("@id", _idEmEdicao);
                cmd.ExecuteNonQuery();

                MessageBox.Show(novo == 1 ? "Usuário ativado!" : "Usuário desativado!", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarGrid(txtFiltroLogin.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetSenha_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idEmEdicao <= 0)
                {
                    MessageBox.Show("Selecione um usuário no grid.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var resp = MessageBox.Show("Deseja resetar a senha deste usuário?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp != DialogResult.Yes) return;

                var novaSenha = GerarSenha(8);
                var hash = HashSenha(novaSenha);

                using var con = new SqliteConnection(Db.ConnectionString);
                con.Open();

                using var cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE usuarios SET senha_hash = @hash WHERE id = @id;";
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.Parameters.AddWithValue("@id", _idEmEdicao);
                cmd.ExecuteNonQuery();

                MessageBox.Show($"Senha resetada!\nNova senha: {novaSenha}\n\n(Anote e envie ao usuário.)",
                    "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string HashSenha(string senha)
        {
            // SHA256 simples (melhor que texto puro). 
            // Depois a gente pode evoluir pra PBKDF2/BCrypt se você quiser.
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToHexString(bytes);
        }

        private static string GerarSenha(int tamanho)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789@!";
            var rnd = RandomNumberGenerator.Create();
            var bytes = new byte[tamanho];
            rnd.GetBytes(bytes);

            var sb = new StringBuilder(tamanho);
            foreach (var b in bytes)
                sb.Append(chars[b % chars.Length]);

            return sb.ToString();
        }

        private void txtFiltroLogin_TextChanged(object sender, EventArgs e)
        {
            CarregarGrid(txtFiltroLogin.Text);
        }

        private void FrmUsuarios_Activated(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvUsuarios.Rows[e.RowIndex];
            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out var id)) return;

            _idEmEdicao = id;
            txtNome.Text = row.Cells["Nome"].Value?.ToString() ?? "";
            txtLogin.Text = row.Cells["Login"].Value?.ToString() ?? "";
            txtSenha.Clear();

            // Seleciona o nível no combo
            var nivel = row.Cells["Nivel"]?.Value?.ToString() ?? "OPERADOR";
            for (int i = 0; i < cmbNivel.Items.Count; i++)
            {
                if (cmbNivel.Items[i].ToString() == nivel)
                {
                    cmbNivel.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
