using Central_do_Educador.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace Central_do_Educador.Services
{
    public class AgendamentoService
    {
        private readonly string _cs;

        public AgendamentoService(string connectionString)
        {
            _cs = connectionString;
        }

        // ─────────────────────────────────────────────
        // CRUD
        // ─────────────────────────────────────────────

        public int Inserir(MensagemAgendada msg)
        {
            using var conn = new SqliteConnection(_cs);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO mensagens_agendadas
                (Destinatario, Telefone, Mensagem, DataHoraAgendada, Ativo, Status, UltimaTentativa, Erro)
                VALUES
                ($dest, $tel, $msg, $dh, $ativo, $status, NULL, '');

                SELECT last_insert_rowid();
            ";

            cmd.Parameters.AddWithValue("$dest", msg.Destinatario ?? "");
            cmd.Parameters.AddWithValue("$tel", msg.Telefone ?? "");
            cmd.Parameters.AddWithValue("$msg", msg.Mensagem ?? "");
            cmd.Parameters.AddWithValue("$dh", msg.DataHoraAgendada.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("$ativo", msg.Ativo ? 1 : 0);
            cmd.Parameters.AddWithValue("$status", msg.Status ?? "Pendente");

            var id = Convert.ToInt32(cmd.ExecuteScalar());
            return id;
        }

        public void Atualizar(MensagemAgendada msg)
        {
            using var conn = new SqliteConnection(_cs);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE mensagens_agendadas
                SET Destinatario = $dest,
                    Telefone = $tel,
                    Mensagem = $msg,
                    DataHoraAgendada = $dh,
                    Ativo = $ativo,
                    Status = $status
                WHERE Id = $id;
            ";

            cmd.Parameters.AddWithValue("$id", msg.Id);
            cmd.Parameters.AddWithValue("$dest", msg.Destinatario ?? "");
            cmd.Parameters.AddWithValue("$tel", msg.Telefone ?? "");
            cmd.Parameters.AddWithValue("$msg", msg.Mensagem ?? "");
            cmd.Parameters.AddWithValue("$dh", msg.DataHoraAgendada.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("$ativo", msg.Ativo ? 1 : 0);
            cmd.Parameters.AddWithValue("$status", msg.Status ?? "Pendente");

            cmd.ExecuteNonQuery();
        }

        public void Cancelar(int id)
        {
            using var conn = new SqliteConnection(_cs);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE mensagens_agendadas
                SET Ativo = 0,
                    Status = 'Cancelada'
                WHERE Id = $id;
            ";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }

        public MensagemAgendada? ObterPorId(int id)
        {
            using var conn = new SqliteConnection(_cs);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT Id, Destinatario, Telefone, Mensagem, DataHoraAgendada, Ativo, Status, UltimaTentativa, Erro
                FROM mensagens_agendadas
                WHERE Id = $id;
            ";
            cmd.Parameters.AddWithValue("$id", id);

            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return Map(r);
        }

        public List<MensagemAgendada> ListarTodos(bool incluirCanceladas = true)
        {
            var lista = new List<MensagemAgendada>();

            using var conn = new SqliteConnection(_cs);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = incluirCanceladas
                ? @"SELECT Id, Destinatario, Telefone, Mensagem, DataHoraAgendada, Ativo, Status, UltimaTentativa, Erro FROM mensagens_agendadas ORDER BY datetime(DataHoraAgendada) DESC;"
                : @"SELECT Id, Destinatario, Telefone, Mensagem, DataHoraAgendada, Ativo, Status, UltimaTentativa, Erro FROM mensagens_agendadas WHERE Status <> 'Cancelada' ORDER BY datetime(DataHoraAgendada) DESC;";

            using var r = cmd.ExecuteReader();
            while (r.Read())
                lista.Add(Map(r));

            return lista;
        }

        // ─────────────────────────────────────────────
        // Motor de envio (busca pendentes e marca resultado)
        // ─────────────────────────────────────────────

        public List<MensagemAgendada> BuscarPendentesParaEnviar(int limite = 50)
        {
            var lista = new List<MensagemAgendada>();

            using var conn = new SqliteConnection(_cs);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT Id, Destinatario, Telefone, Mensagem, DataHoraAgendada, Ativo, Status, UltimaTentativa, Erro
                FROM mensagens_agendadas
                WHERE Ativo = 1
                  AND Status = 'Pendente'
                  AND datetime(DataHoraAgendada) <= datetime('now', 'localtime')
                ORDER BY datetime(DataHoraAgendada) ASC
                LIMIT $lim;
            ";
            cmd.Parameters.AddWithValue("$lim", limite);

            using var r = cmd.ExecuteReader();
            while (r.Read())
                lista.Add(Map(r));

            return lista;
        }

        public void MarcarTentativa(int id, bool sucesso, string? erro = null)
        {
            using var conn = new SqliteConnection(_cs);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE mensagens_agendadas
                SET Status = $status,
                    UltimaTentativa = $ultima,
                    Erro = $erro
                WHERE Id = $id;
            ";

            cmd.Parameters.AddWithValue("$id", id);
            cmd.Parameters.AddWithValue("$status", sucesso ? "Enviada" : "Falhou");
            cmd.Parameters.AddWithValue("$ultima", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("$erro", erro ?? "");

            cmd.ExecuteNonQuery();
        }

        public void Reagendar(int id, DateTime novaDataHora)
        {
            using var conn = new SqliteConnection(_cs);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE mensagens_agendadas
                SET DataHoraAgendada = $dh,
                    Status = 'Pendente',
                    Ativo = 1,
                    Erro = ''
                WHERE Id = $id;
            ";

            cmd.Parameters.AddWithValue("$id", id);
            cmd.Parameters.AddWithValue("$dh", novaDataHora.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
        }

        private static MensagemAgendada Map(SqliteDataReader r)
        {
            // Colunas: 0..8 conforme SELECT
            var dh = DateTime.Parse(r.GetString(4));
            DateTime? ultima = null;
            if (!r.IsDBNull(7))
                ultima = DateTime.Parse(r.GetString(7));

            return new MensagemAgendada
            {
                Id = r.GetInt32(0),
                Destinatario = r.IsDBNull(1) ? "" : r.GetString(1),
                Telefone = r.IsDBNull(2) ? "" : r.GetString(2),
                Mensagem = r.IsDBNull(3) ? "" : r.GetString(3),
                DataHoraAgendada = dh,
                Ativo = r.GetInt32(5) == 1,
                Status = r.IsDBNull(6) ? "Pendente" : r.GetString(6),
                UltimaTentativa = ultima,
                Erro = r.IsDBNull(8) ? "" : r.GetString(8)
            };
        }
    }
}