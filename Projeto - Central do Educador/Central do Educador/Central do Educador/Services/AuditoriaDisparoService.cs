using Central_do_Educador.Data;
using Central_do_Educador.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

namespace Central_do_Educador.Services
{
    public class AuditoriaDisparoService
    {
        public async Task RegistrarLoteAsync(string usuario, string origem, string filtroResumo, ResultadoDisparoLote resultado)
        {
            using var conn = await Db.OpenConnectionAsync();
            await conn.OpenAsync();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                            INSERT INTO auditoria_disparos
                            (
                            usuario,
                                origem,
                                filtro_resumo,
                                total_itens,
                                whatsapp_enviados,
                                whatsapp_falhas,
                                email_enviados,
                                email_falhas,
                                data_execucao
                            )
                            VALUES
                            (
                                @usuario,
                                @origem,
                                @filtro_resumo,
                                @total_itens,
                                @whatsapp_enviados,
                                @whatsapp_falhas,
                                @email_enviados,
                                @email_falhas,
                                @data_execucao
                            );";

            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@origem", origem);
            cmd.Parameters.AddWithValue("@filtro_resumo", filtroResumo);
            cmd.Parameters.AddWithValue("@total_itens", resultado.TotalItens);
            cmd.Parameters.AddWithValue("@whatsapp_enviados", resultado.WhatsAppEnviados);
            cmd.Parameters.AddWithValue("@whatsapp_falhas", resultado.WhatsAppFalhas);
            cmd.Parameters.AddWithValue("@email_enviados", resultado.EmailEnviados);
            cmd.Parameters.AddWithValue("@email_falhas", resultado.EmailFalhas);
            cmd.Parameters.AddWithValue("@data_execucao", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            await cmd.ExecuteNonQueryAsync();
        }
    }
}