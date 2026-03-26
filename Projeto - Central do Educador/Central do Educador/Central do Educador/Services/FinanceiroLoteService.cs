using Central_do_Educador.Data;
using Central_do_Educador.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Central_do_Educador.Services
{
    public class FinanceiroLoteService
    {
        public async Task<List<MensagemLoteItem>> ObterItensParaCobrancaAsync(FiltroCobrancaLote filtro)
        {
            var itens = new List<MensagemLoteItem>();

            using var conn = await Db.OpenConnectionAsync();

            var sql = new StringBuilder(@"
SELECT
    p.id AS ParcelaId,
    p.numero_parcela,
    p.valor_parcela,
    p.data_vencimento,
    p.status,
    COALESCE(c.descricao, '') AS contrato,
    COALESCE(c.quantidade_parcelas, 0) AS total_parcelas,

    a.id AS AlunoId,
    COALESCE(a.nome, '') AS NomeAluno,
    COALESCE(a.numero_aluno, '') AS TelefoneAluno,
    COALESCE(a.email, '') AS EmailAluno,
    COALESCE(a.NomeResponsavel, '') AS NomeResponsavel,
    COALESCE(a.numero_responsavel, '') AS TelefoneResponsavel,
    '' AS EmailResponsavel
FROM parcelas p
INNER JOIN alunos a ON a.id = p.aluno_id
LEFT JOIN contratos c ON c.id = p.contrato_id
WHERE 1 = 1
");

            var cmd = conn.CreateCommand();

            if (!string.IsNullOrWhiteSpace(filtro.Status))
            {
                sql.AppendLine("AND p.status = @status");
                cmd.Parameters.AddWithValue("@status", filtro.Status);
            }

            if (!string.IsNullOrWhiteSpace(filtro.NomeAluno))
            {
                sql.AppendLine("AND a.nome LIKE @nomeAluno");
                cmd.Parameters.AddWithValue("@nomeAluno", $"%{filtro.NomeAluno}%");
            }

            if (filtro.VencimentoInicial.HasValue)
            {
                sql.AppendLine("AND date(p.data_vencimento) >= date(@vencIni)");
                cmd.Parameters.AddWithValue("@vencIni", filtro.VencimentoInicial.Value.ToString("yyyy-MM-dd"));
            }

            if (filtro.VencimentoFinal.HasValue)
            {
                sql.AppendLine("AND date(p.data_vencimento) <= date(@vencFim)");
                cmd.Parameters.AddWithValue("@vencFim", filtro.VencimentoFinal.Value.ToString("yyyy-MM-dd"));
            }

            if (filtro.SomenteComTelefone)
            {
                sql.AppendLine(@"
                    AND (
                        COALESCE(a.numero_aluno, '') <> ''
                        OR COALESCE(a.numero_responsavel, '') <> ''
                    )");
            }

            sql.AppendLine("ORDER BY date(p.data_vencimento), a.nome, p.numero_parcela");

            cmd.CommandText = sql.ToString();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var numeroParcela = reader["numero_parcela"]?.ToString() ?? "";
                var totalParcelas = reader["total_parcelas"]?.ToString() ?? "0";

                itens.Add(new MensagemLoteItem
                {
                    ParcelaId = Convert.ToInt32(reader["ParcelaId"]),
                    Parcela = totalParcelas != "0"
                        ? $"{numeroParcela}/{totalParcelas}"
                        : numeroParcela,
                    Valor = Convert.ToDecimal(reader["valor_parcela"]),
                    Vencimento = DateTime.Parse(reader["data_vencimento"].ToString()!),
                    Status = reader["status"].ToString() ?? "",
                    Contrato = reader["contrato"].ToString() ?? "",

                    AlunoId = Convert.ToInt32(reader["AlunoId"]),
                    NomeAluno = reader["NomeAluno"].ToString() ?? "",
                    TelefoneAluno = reader["TelefoneAluno"].ToString() ?? "",
                    EmailAluno = reader["EmailAluno"].ToString() ?? "",
                    NomeResponsavel = reader["NomeResponsavel"].ToString() ?? "",
                    TelefoneResponsavel = reader["TelefoneResponsavel"].ToString() ?? "",
                    EmailResponsavel = reader["EmailResponsavel"].ToString() ?? ""
                });
            }

            return itens;
        }
    }
}