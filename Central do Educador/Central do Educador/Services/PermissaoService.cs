using Central_do_Educador.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Central_do_Educador.Services
{
    public static class PermissaoService
    {
        public class PermissaoItem
        {
            public long UsuarioId { get; set; }
            public string Tela { get; set; } = string.Empty;
            public bool Permitido { get; set; }
        }

        public static async Task CriarTabelaAsync()
        {
            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            string sql = @"
                CREATE TABLE IF NOT EXISTS permissoes (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    usuario_id INTEGER NOT NULL,
                    tela TEXT NOT NULL,
                    permitido INTEGER NOT NULL DEFAULT 0,
                    UNIQUE(usuario_id, tela)
                );";

            using var cmd = new SqliteCommand(sql, conn);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task<bool> TemPermissaoAsync(long usuarioId, string tela)
        {
            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            string sql = @"
                SELECT permitido
                FROM permissoes
                WHERE usuario_id = @usuario_id AND tela = @tela
                LIMIT 1;";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usuario_id", usuarioId);
            cmd.Parameters.AddWithValue("@tela", tela.ToUpper());

            var result = await cmd.ExecuteScalarAsync();

            if (result == null || result == DBNull.Value)
                return false;

            return Convert.ToInt32(result) == 1;
        }

        public static async Task<List<PermissaoItem>> ListarPermissoesUsuarioAsync(long usuarioId)
        {
            var lista = new List<PermissaoItem>();

            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            string sql = @"
                SELECT usuario_id, tela, permitido
                FROM permissoes
                WHERE usuario_id = @usuario_id
                ORDER BY tela;";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new PermissaoItem
                {
                    UsuarioId = reader.GetInt64(0),
                    Tela = reader.GetString(1),
                    Permitido = reader.GetInt32(2) == 1
                });
            }

            return lista;
        }

        public static async Task SalvarPermissoesAsync(long usuarioId, List<(string tela, bool permitido)> permissoes)
        {
            using var conn = new SqliteConnection(Db.ConnectionString);
            await conn.OpenAsync();

            using var transaction = conn.BeginTransaction();

            foreach (var item in permissoes)
            {
                string sql = @"
            INSERT INTO permissoes (usuario_id, tela, permitido)
            VALUES (@usuario_id, @tela, @permitido)
            ON CONFLICT(usuario_id, tela)
            DO UPDATE SET permitido = excluded.permitido;";

                using var cmd = new SqliteCommand(sql, conn, transaction);
                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);
                cmd.Parameters.AddWithValue("@tela", item.tela.ToUpper());
                cmd.Parameters.AddWithValue("@permitido", item.permitido ? 1 : 0);

                await cmd.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
    }
}