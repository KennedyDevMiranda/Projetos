// ================================
// Arquivo: FrmNovidades.cs
// ================================
using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Central_do_Educador.Forms
{
    public partial class FrmNovidades : Form
    {
        private class NovidadeItem
        {
            public long Id { get; set; }
            public string Titulo { get; set; } = string.Empty;
            public string Categoria { get; set; } = string.Empty;
            public string Versao { get; set; } = string.Empty;
            public DateTime Data { get; set; }
            public string Resumo { get; set; } = string.Empty;
            public string Detalhes { get; set; } = string.Empty;
            public bool Ativo { get; set; }

            public override string ToString()
            {
                return $"{Data:dd/MM/yyyy} • {Titulo}";
            }
        }

        private readonly List<NovidadeItem> _novidades = new();

        public FrmNovidades()
        {
            InitializeComponent();
        }

        private async void FrmNovidades_Load(object sender, EventArgs e)
        {
            ConfigurarTela();
            await CriarTabelaNovidadesAsync();
            await InserirNovidadesPadraoSeVazioAsync();
            await CarregarNovidadesAsync();
        }

        private void ConfigurarTela()
        {
            lblTitulo.Text = "Novidades do Sistema";
            lblSubtitulo.Text = "Acompanhe as melhorias, correções e recursos novos da Central do Educador.";
            lblVersaoSistema.Text = "Versão atual: 1.0.0";
            lblUltimaAtualizacao.Text = "Última atualização: --/--/----";
            lblContador.Text = "0 novidade(s) encontrada(s)";
            LimparDetalhes();
        }

        private async Task CriarTabelaNovidadesAsync()
        {
            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            string sql = @"
                CREATE TABLE IF NOT EXISTS novidades (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    titulo TEXT NOT NULL,
                    categoria TEXT NOT NULL,
                    versao TEXT NOT NULL,
                    data_novidade TEXT NOT NULL,
                    resumo TEXT NOT NULL,
                    detalhes TEXT NOT NULL,
                    ativo INTEGER NOT NULL DEFAULT 1,
                    data_cadastro TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP
                );";

            using var cmd = new SqliteCommand(sql, conn);
            await cmd.ExecuteNonQueryAsync();
        }

        private async Task InserirNovidadesPadraoSeVazioAsync()
        {
            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            string sqlCount = "SELECT COUNT(*) FROM novidades;";
            using var cmdCount = new SqliteCommand(sqlCount, conn);
            long total = (long)(await cmdCount.ExecuteScalarAsync() ?? 0L);

            if (total > 0)
                return;

            var novidadesPadrao = new List<NovidadeItem>
            {
                new NovidadeItem
                {
                    Titulo = "Novo módulo Financeiro",
                    Categoria = "Financeiro",
                    Versao = "v1.0.0",
                    Data = new DateTime(2026, 03, 05),
                    Resumo = "Controle de contratos, parcelas e pagamentos.",
                    Detalhes = "O módulo Financeiro agora permite cadastrar contratos, gerar parcelas automaticamente, registrar pagamentos, estornar valores, acompanhar pendências e visualizar indicadores no dashboard.",
                    Ativo = true
                },
                new NovidadeItem
                {
                    Titulo = "Notificações por WhatsApp e E-mail",
                    Categoria = "Comunicação",
                    Versao = "v1.0.0",
                    Data = new DateTime(2026, 03, 04),
                    Resumo = "Mais agilidade no contato com alunos e responsáveis.",
                    Detalhes = "O sistema passou a oferecer envio de notificações por WhatsApp e E-mail, facilitando lembretes, avisos e confirmações de pagamento.",
                    Ativo = true
                },
                new NovidadeItem
                {
                    Titulo = "Melhorias no painel de configurações",
                    Categoria = "Configurações",
                    Versao = "v0.9.9",
                    Data = new DateTime(2026, 03, 03),
                    Resumo = "Mais organização e facilidade na administração.",
                    Detalhes = "O painel de configurações recebeu ajustes visuais e estruturais para deixar o gerenciamento dos recursos mais claro e profissional.",
                    Ativo = true
                },
                new NovidadeItem
                {
                    Titulo = "Ajustes visuais nos formulários",
                    Categoria = "Interface",
                    Versao = "v0.9.8",
                    Data = new DateTime(2026, 03, 02),
                    Resumo = "Telas mais limpas, legíveis e consistentes.",
                    Detalhes = "Diversos formulários foram reorganizados visualmente para melhorar a experiência do usuário, com melhor espaçamento, leitura e padronização.",
                    Ativo = true
                },
                new NovidadeItem
                {
                    Titulo = "Preparação para controle de permissões",
                    Categoria = "Segurança",
                    Versao = "v0.9.8",
                    Data = new DateTime(2026, 03, 01),
                    Resumo = "Base preparada para acesso por perfil de usuário.",
                    Detalhes = "Foi iniciada a estrutura para controle de permissões, permitindo que administradores decidam quais módulos cada usuário pode acessar.",
                    Ativo = true
                }
            };

            foreach (var item in novidadesPadrao)
            {
                string sqlInsert = @"
                    INSERT INTO novidades
                    (titulo, categoria, versao, data_novidade, resumo, detalhes, ativo)
                    VALUES
                    (@titulo, @categoria, @versao, @data_novidade, @resumo, @detalhes, @ativo);";

                using var cmdInsert = new SqliteCommand(sqlInsert, conn);
                cmdInsert.Parameters.AddWithValue("@titulo", item.Titulo);
                cmdInsert.Parameters.AddWithValue("@categoria", item.Categoria);
                cmdInsert.Parameters.AddWithValue("@versao", item.Versao);
                cmdInsert.Parameters.AddWithValue("@data_novidade", item.Data.ToString("yyyy-MM-dd"));
                cmdInsert.Parameters.AddWithValue("@resumo", item.Resumo);
                cmdInsert.Parameters.AddWithValue("@detalhes", item.Detalhes);
                cmdInsert.Parameters.AddWithValue("@ativo", item.Ativo ? 1 : 0);
                await cmdInsert.ExecuteNonQueryAsync();
            }
        }

        private async Task CarregarNovidadesAsync()
        {
            try
            {
                _novidades.Clear();

                using var conn = new SqliteConnection(Db.ConnectionString);
                await conn.OpenAsync();

                string sql = @"
                    SELECT id, titulo, categoria, versao, data_novidade, resumo, detalhes, ativo
                    FROM novidades
                    WHERE ativo = 1
                    ORDER BY date(data_novidade) DESC, id DESC;";

                using var cmd = new SqliteCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    DateTime data = DateTime.Today;
                    if (!DateTime.TryParse(reader[4]?.ToString(), out data))
                        data = DateTime.Today;

                    _novidades.Add(new NovidadeItem
                    {
                        Id = Convert.ToInt64(reader[0]),
                        Titulo = reader[1]?.ToString() ?? string.Empty,
                        Categoria = reader[2]?.ToString() ?? string.Empty,
                        Versao = reader[3]?.ToString() ?? string.Empty,
                        Data = data,
                        Resumo = reader[5]?.ToString() ?? string.Empty,
                        Detalhes = reader[6]?.ToString() ?? string.Empty,
                        Ativo = Convert.ToInt32(reader[7]) == 1
                    });
                }

                PreencherLista(_novidades);
                AtualizarResumo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar novidades: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PreencherLista(List<NovidadeItem> lista)
        {
            lstNovidades.Items.Clear();

            foreach (var item in lista)
                lstNovidades.Items.Add(item);

            if (lstNovidades.Items.Count > 0)
                lstNovidades.SelectedIndex = 0;
            else
                LimparDetalhes();
        }

        private void AtualizarResumo()
        {
            lblContador.Text = $"{lstNovidades.Items.Count} novidade(s) encontrada(s)";

            if (_novidades.Count > 0)
                lblUltimaAtualizacao.Text = $"Última atualização: {_novidades[0].Data:dd/MM/yyyy}";
            else
                lblUltimaAtualizacao.Text = "Última atualização: --/--/----";
        }

        private void LimparDetalhes()
        {
            lblCategoriaValor.Text = "--";
            lblVersaoValor.Text = "--";
            lblDataValor.Text = "--/--/----";
            lblResumoValor.Text = "Selecione uma novidade para visualizar os detalhes.";
            txtDetalhes.Clear();
        }

        private void lstNovidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNovidades.SelectedItem is not NovidadeItem item)
            {
                LimparDetalhes();
                return;
            }

            lblCategoriaValor.Text = item.Categoria;
            lblVersaoValor.Text = item.Versao;
            lblDataValor.Text = item.Data.ToString("dd/MM/yyyy");
            lblResumoValor.Text = item.Resumo;
            txtDetalhes.Text = item.Detalhes;
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            string termo = txtPesquisa.Text.Trim();

            if (string.IsNullOrWhiteSpace(termo))
            {
                PreencherLista(_novidades);
                AtualizarResumo();
                return;
            }

            var filtradas = _novidades.FindAll(n =>
                n.Titulo.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                n.Categoria.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                n.Resumo.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                n.Detalhes.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                n.Versao.Contains(termo, StringComparison.OrdinalIgnoreCase));

            PreencherLista(filtradas);
            lblContador.Text = $"{filtradas.Count} novidade(s) encontrada(s)";
        }

        private async void btnAtualizar_Click(object sender, EventArgs e)
        {
            await CarregarNovidadesAsync();
        }

        private void btnLimparFiltro_Click(object sender, EventArgs e)
        {
            txtPesquisa.Clear();
            PreencherLista(_novidades);
            AtualizarResumo();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}