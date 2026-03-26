using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Central_do_Educador.Data;

namespace Central_do_Educador
{
    public partial class FrmAluno : Form
    {
        private int? _alunoId;

        public FrmAluno()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Carrega os dados de um aluno existente nos campos do formulário.
        /// </summary>
        public void CarregarAluno(int id, string nome, string email,
            string numeroAluno, string nomeResponsavel,
            string numeroResponsavel, bool ativo)
        {
            _alunoId = id;
            txtAluno.Text = nome;
            txtEmail.Text = email;
            maskNumAluno.Text = numeroAluno;
            textBox1.Text = nomeResponsavel;
            maskNumResp.Text = numeroResponsavel;
            chkAlunoAtivo.Checked = ativo;
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Ler e validar campos
                string nome = txtAluno.Text.Trim();
                if (string.IsNullOrWhiteSpace(nome))
                {
                    MessageBox.Show("Informe o nome do aluno.", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string email = txtEmail.Text.Trim();
                string numeroAluno = maskNumAluno.Text.Trim();
                string nomeResponsavel = textBox1.Text.Trim();
                string numeroResponsavel = maskNumResp.Text.Trim();
                int ativo = chkAlunoAtivo.Checked ? 1 : 0;

                string sql;
                SqliteParameter[] parametros;

                if (_alunoId.HasValue)
                {
                    // Atualização
                    sql = @"
                        UPDATE alunos
                        SET nome = @nome,
                            email = @email,
                            numero_aluno = @numero_aluno,
                            NomeResponsavel = @NomeResponsavel,
                            numero_responsavel = @numero_responsavel,
                            ativo = @ativo
                        WHERE id = @id;
                    ";

                    parametros =
                    [
                        Db.P("@id", _alunoId.Value),
                        Db.P("@nome", nome),
                        Db.P("@email", string.IsNullOrEmpty(email) ? DBNull.Value : email),
                        Db.P("@numero_aluno", string.IsNullOrEmpty(numeroAluno) ? DBNull.Value : numeroAluno),
                        Db.P("@NomeResponsavel", string.IsNullOrEmpty(nomeResponsavel) ? DBNull.Value : nomeResponsavel),
                        Db.P("@numero_responsavel", string.IsNullOrEmpty(numeroResponsavel) ? DBNull.Value : numeroResponsavel),
                        Db.P("@ativo", ativo)
                    ];
                }
                else
                {
                    // Inserção
                    sql = @"
                        INSERT INTO alunos
                            (nome, email, numero_aluno, NomeResponsavel, numero_responsavel, ativo)
                        VALUES
                            (@nome, @email, @numero_aluno, @NomeResponsavel, @numero_responsavel, @ativo);
                    ";

                    parametros =
                    [
                        Db.P("@nome", nome),
                        Db.P("@email", string.IsNullOrEmpty(email) ? DBNull.Value : email),
                        Db.P("@numero_aluno", string.IsNullOrEmpty(numeroAluno) ? DBNull.Value : numeroAluno),
                        Db.P("@NomeResponsavel", string.IsNullOrEmpty(nomeResponsavel) ? DBNull.Value : nomeResponsavel),
                        Db.P("@numero_responsavel", string.IsNullOrEmpty(numeroResponsavel) ? DBNull.Value : numeroResponsavel),
                        Db.P("@ativo", ativo)
                    ];
                }

                // Executa
                int rows = await Db.ExecuteNonQueryAsync(sql, parametros);

                if (rows > 0)
                {
                    MessageBox.Show("Aluno salvo com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Não foi possível salvar o aluno.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmAluno_Activated(object sender, EventArgs e)
        {

        }
    }
}