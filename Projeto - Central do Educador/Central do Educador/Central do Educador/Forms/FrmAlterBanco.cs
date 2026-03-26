using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace Central_do_Educador.Forms
{
    public partial class FrmAlterBanco : Form
    {
        private static readonly string StorageDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Central do Educador");
        private static readonly string StorageFile = Path.Combine(StorageDir, "databases.json");

        public FrmAlterBanco()
        {
            InitializeComponent();
        }

        private void FrmAlterBanco_Activated(object sender, EventArgs e)
        {
            // Mantém compatibilidade com a configuração existente (último banco usado)
            txtCaminho.Text = Properties.Settings.Default.CaminhoBanco;
        }

        private void btnProcurarBD_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                // Filtro combinado para os bancos de arquivo mais usados
                ofd.Filter = "Arquivos de Banco de Dados|*.accdb;*.mdb;*.db;*.sqlite;*.sqlite3;*.mdf;*.ndf;*.sdf|Access (*.accdb;*.mdb)|*.accdb;*.mdb|SQLite (*.db;*.sqlite;*.sqlite3)|*.db;*.sqlite;*.sqlite3|SQL Server (*.mdf;*.ndf)|*.mdf;*.ndf|Todos os arquivos (*.*)|*.*";
                ofd.Title = "Procurar arquivo de banco de dados";
                ofd.CheckFileExists = true;
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtCaminho.Text = ofd.FileName;
                }
            }
        }

        private void btnSalvarBD_Click(object sender, EventArgs e)
        {
            string novoCaminho = txtCaminho.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(novoCaminho))
            {
                MessageBox.Show("Informe o caminho do banco de dados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(novoCaminho))
            {
                MessageBox.Show("Arquivo não encontrado! Verifique o caminho.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var list = LoadDatabaseList();
                bool alreadyExists = list.Exists(e => string.Equals(e.Path, novoCaminho, StringComparison.OrdinalIgnoreCase));

                if (!alreadyExists)
                {
                    var entry = new DatabaseEntry
                    {
                        Path = novoCaminho,
                        Type = DetectDatabaseType(novoCaminho),
                        Added = DateTime.UtcNow
                    };

                    list.Add(entry);
                    SaveDatabaseList(list);

                    MessageBox.Show("Caminho adicionado à lista de bancos salvos.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Este caminho já está na lista de bancos salvos. Atualizando como padrão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Mantém compatibilidade com a configuração existente: salva o último caminho usado
                Properties.Settings.Default.CaminhoBanco = novoCaminho;
                Properties.Settings.Default.Save();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar a configuração: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static List<DatabaseEntry> LoadDatabaseList()
        {
            try
            {
                if (!File.Exists(StorageFile))
                    return new List<DatabaseEntry>();

                string json = File.ReadAllText(StorageFile);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var list = JsonSerializer.Deserialize<List<DatabaseEntry>>(json, options);
                return list ?? new List<DatabaseEntry>();
            }
            catch
            {
                // Se houver erro ao ler/desserializar, retorna lista vazia para evitar quebra do app
                return new List<DatabaseEntry>();
            }
        }

        private static void SaveDatabaseList(List<DatabaseEntry> list)
        {
            Directory.CreateDirectory(StorageDir);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(list, options);
            File.WriteAllText(StorageFile, json);
        }

        private static string DetectDatabaseType(string path)
        {
            string ext = Path.GetExtension(path).ToLowerInvariant();

            return ext switch
            {
                ".accdb" or ".mdb" => "Access",
                ".db" or ".sqlite" or ".sqlite3" => "SQLite",
                ".mdf" or ".ndf" => "SQL Server (MDF)",
                ".sdf" => "SQL Server Compact",
                _ => "Desconhecido"
            };
        }

        private class DatabaseEntry
        {
            public string Path { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public DateTime Added { get; set; }
        }
    }
}