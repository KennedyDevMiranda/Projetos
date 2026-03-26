using Central_do_Educador.Data;
using Central_do_Educador.Services;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmPermissao : Form
    {
        public FrmPermissao()
        {
            InitializeComponent();
        }

        private class TelaPermissaoItem
        {
            public string NomeExibicao { get; set; } = string.Empty;
            public string ChaveTela { get; set; } = string.Empty;

            public override string ToString()
            {
                return NomeExibicao;
            }
        }

        private async void FrmPermissao_Load(object sender, EventArgs e)
        {
            CarregarTelas();
            await CarregarUsuariosAsync();
            await CarregarPermissoesUsuarioAsync();
        }

        private async Task CarregarUsuariosAsync()
        {
            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            string sql = "SELECT id, nome FROM usuarios ORDER BY nome;";
            using var cmd = new SqliteCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Columns.Add("id", typeof(long));
            dt.Columns.Add("nome", typeof(string));

            while (await reader.ReadAsync())
                dt.Rows.Add(reader.GetInt64(0), reader.GetString(1));

            cmbUsuarios.DataSource = dt;
            cmbUsuarios.DisplayMember = "nome";
            cmbUsuarios.ValueMember = "id";
        }

        private void CarregarTelas()
        {
            clbPermissoes.Items.Clear();

            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Agendamento", ChaveTela = "FrmAgendamento" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Alterar Banco", ChaveTela = "FrmAlterBanco" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Alunos", ChaveTela = "FrmAluno" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "ChatBot", ChaveTela = "FrmChatBot" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Configuração de E-mail", ChaveTela = "FrmConfiguracaoEmail" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Configuração de WhatsApp", ChaveTela = "FrmConfiguracaoWhatsapp" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Controle de Taxa", ChaveTela = "FrmControleTaxa" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Entrega de Livros", ChaveTela = "FrmEntregaLivros" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Financeiro", ChaveTela = "FrmFinanceiro" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Gerenciar Falhas", ChaveTela = "FrmGerenciarFalhas" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Login", ChaveTela = "FrmLogin" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Novidades", ChaveTela = "FrmNovidades" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Painel de Configurações", ChaveTela = "FrmPainelConfigs" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Permissão", ChaveTela = "FrmPermissao" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Principal", ChaveTela = "FrmPrincipal" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "QR Code", ChaveTela = "FrmQRCode" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Registro", ChaveTela = "FrmRegistro" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Reportar Falha", ChaveTela = "FrmReportarFalha" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Sobre Nós", ChaveTela = "FrmSobreNos" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Templates", ChaveTela = "FrmTemplates" });
            clbPermissoes.Items.Add(new TelaPermissaoItem { NomeExibicao = "Usuários", ChaveTela = "FrmUsuarios" });
        }

        private async void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            await CarregarPermissoesUsuarioAsync();
        }

        private async Task CarregarPermissoesUsuarioAsync()
        {
            if (cmbUsuarios.SelectedValue == null)
                return;

            if (!long.TryParse(cmbUsuarios.SelectedValue.ToString(), out long usuarioId))
                return;

            var permissoes = await PermissaoService.ListarPermissoesUsuarioAsync(usuarioId);

            for (int i = 0; i < clbPermissoes.Items.Count; i++)
            {
                if (clbPermissoes.Items[i] is TelaPermissaoItem item)
                {
                    bool permitido = permissoes.Any(p =>
                        string.Equals(p.Tela, item.ChaveTela, StringComparison.OrdinalIgnoreCase) &&
                        p.Permitido);

                    clbPermissoes.SetItemChecked(i, permitido);
                }
            }
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedValue == null)
            {
                MessageBox.Show("Selecione um usuário.");
                return;
            }

            long usuarioId = Convert.ToInt64(cmbUsuarios.SelectedValue);

            var lista = new List<(string tela, bool permitido)>();

            for (int i = 0; i < clbPermissoes.Items.Count; i++)
            {
                if (clbPermissoes.Items[i] is TelaPermissaoItem item)
                {
                    bool permitido = clbPermissoes.GetItemChecked(i);
                    lista.Add((item.ChaveTela, permitido));
                }
            }

            await PermissaoService.SalvarPermissoesAsync(usuarioId, lista);

            MessageBox.Show("Permissões salvas com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}