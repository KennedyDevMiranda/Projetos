using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Central_do_Educador.Forms
{
    public partial class FrmLogin : Form
    {
        // Caminho onde as credenciais serão salvas (protegidas pelo DPAPI, escopo do usuário)
        private readonly string _credFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Central_do_Educador",
            "credentials.dat");

        public FrmLogin()
        {
            InitializeComponent();
        }

        private static string HashSenha(string senha)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToHexString(bytes);
        }

        private async void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                var login = (txtLogin.Text ?? "").Trim();
                var senha = (txtSenha.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
                {
                    MessageBox.Show("Informe login e senha.", "Validação",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var senhaHash = HashSenha(senha);

                using var con = new SqliteConnection(Db.ConnectionString);
                await con.OpenAsync();

                using var cmd = con.CreateCommand();
                cmd.CommandText = @"
                    SELECT id, nome, login, ativo,
                           COALESCE(nivel, 'OPERADOR') AS nivel
                    FROM usuarios
                    WHERE login = @login AND senha_hash = @hash
                    LIMIT 1;
                    ";
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@hash", senhaHash);

                using var r = await cmd.ExecuteReaderAsync();

                if (!await r.ReadAsync())
                {
                    MessageBox.Show("Login ou senha inválidos.", "Acesso negado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSenha.Clear();
                    txtSenha.Focus();
                    return;
                }

                Sessao.UsuarioId = Convert.ToInt32(r["id"]);
                Sessao.UsuarioNome = r["nome"]?.ToString() ?? login;
                Sessao.UsuarioLogin = r["login"]?.ToString() ?? login;
                Sessao.NivelUsuario = r["nivel"]?.ToString() ?? "OPERADOR";

                var ativo = Convert.ToInt32(r["ativo"]) == 1;
                if (!ativo)
                {
                    MessageBox.Show("Usuário desativado. Fale com o administrador.", "Acesso negado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Salva ou remove credenciais conforme checkbox
                try
                {
                    if (chkLembrarSenha.Checked)
                    {
                        SaveCredentials(login, senha);
                    }
                    else
                    {
                        DeleteCredentials();
                    }
                }
                catch
                {
                    // Não impedir login em caso de falha ao salvar/limpar credenciais
                }

                var nomeUsuario = r["nome"]?.ToString() ?? login;

                var frm = new FrmDashBoard(nomeUsuario);
                frm.FormClosed += (s, args) =>
                {
                    if (frm.IsLogout)
                    {
                        // Limpa a sessão
                        Sessao.UsuarioId = 0;
                        Sessao.UsuarioNome = "";
                        Sessao.UsuarioLogin = "";
                        Sessao.NivelUsuario = "OPERADOR";

                        txtLogin.Clear();
                        txtSenha.Clear();
                        chkLembrarSenha.Checked = false;
                        this.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                };
                frm.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao entrar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // PictureBox não implementa IButtonControl, então usamos KeyPreview + KeyDown
            this.KeyPreview = true;
            this.KeyDown += FrmLogin_KeyDown;

            // Ao carregar: tentar ler credenciais salvas e preencher os campos
            try
            {
                var creds = LoadCredentials();
                if (creds != null)
                {
                    txtLogin.Text = creds.Value.login;
                    txtSenha.Text = creds.Value.senha;
                    chkLembrarSenha.Checked = true;
                }
            }
            catch
            {
                // Ignorar erros de leitura (arquivo corrompido, permissões, etc.)
            }
        }

        private void FrmLogin_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnEntrar_Click(btnEntrar, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                btnCancelar_Click(btnCancelar, EventArgs.Empty);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show(
                "Deseja realmente sair do sistema?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
            {
                return;
            }

            txtLogin.Clear();
            txtSenha.Clear();

            this.Close();
            Application.Exit();
        }

        private void chkLembrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            // Se o usuário desmarcar, removemos imediatamente as credenciais salvas.
            if (!chkLembrarSenha.Checked)
            {
                try
                {
                    DeleteCredentials();
                }
                catch
                {
                    // ignorar erros de exclusão
                }
            }
            // Se marcar, a credencial será salva após login bem-sucedido (no btnEntrar_Click).
        }

        // Salva login e senha protegidos (DPAPI - CurrentUser)
        private void SaveCredentials(string login, string senha)
        {
            var dir = Path.GetDirectoryName(_credFilePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir!);
            }

            var plain = Encoding.UTF8.GetBytes($"{login}\n{senha}");
            var encrypted = ProtectedData.Protect(plain, null, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(_credFilePath, encrypted);
        }

        // Lê credenciais salvas, retorna null se não existir ou erro
        private (string login, string senha)? LoadCredentials()
        {
            if (!File.Exists(_credFilePath))
                return null;

            var encrypted = File.ReadAllBytes(_credFilePath);
            var plain = ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser);
            var text = Encoding.UTF8.GetString(plain);
            var parts = text.Split(new[] { '\n' }, 2);
            var login = parts.Length > 0 ? parts[0] : "";
            var senha = parts.Length > 1 ? parts[1] : "";
            if (string.IsNullOrEmpty(login))
                return null;
            return (login, senha);
        }

        private void DeleteCredentials()
        {
            if (File.Exists(_credFilePath))
            {
                File.Delete(_credFilePath);
            }
        }
    }
}
