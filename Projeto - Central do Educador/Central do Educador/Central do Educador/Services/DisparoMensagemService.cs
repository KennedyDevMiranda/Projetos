using Central_do_Educador.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Central_do_Educador.Services
{
    public class DisparoMensagemService
    {
        public async Task<ResultadoDisparoLote> EnviarAsync(List<MensagemLoteItem> itens, bool enviarWhatsapp = true, bool enviarEmail = false)
        {
            var resultado = new ResultadoDisparoLote
            {
                TotalItens = itens.Count
            };

            foreach (var item in itens)
            {
                try
                {
                    bool enviadoWhatsapp = false;
                    bool enviadoEmail = false;

                    if (enviarWhatsapp)
                    {
                        var telefoneDestino = !string.IsNullOrWhiteSpace(item.TelefoneResponsavel)
                            ? item.TelefoneResponsavel
                            : item.TelefoneAluno;

                        if (!string.IsNullOrWhiteSpace(telefoneDestino))
                        {
                            await WhatsAppDriverManager.EnviarMensagemAsync(telefoneDestino, item.MensagemFinal);
                            resultado.WhatsAppEnviados++;
                            enviadoWhatsapp = true;
                        }
                    }

                    if (enviarEmail)
                    {
                        var emailDestino = !string.IsNullOrWhiteSpace(item.EmailResponsavel)
                            ? item.EmailResponsavel
                            : item.EmailAluno;

                        if (!string.IsNullOrWhiteSpace(emailDestino))
                        {
                            // Substitua pelo seu EmailService real
                            // await EmailService.EnviarAsync(emailDestino, "Cobrança", item.MensagemFinal);
                            resultado.EmailEnviados++;
                            enviadoEmail = true;
                        }
                    }

                    if (!enviadoWhatsapp && !enviadoEmail)
                    {
                        resultado.Logs.Add($"SEM DESTINO: {item.NomeAluno} - Parcela {item.Parcela}");
                    }
                    else
                    {
                        resultado.Logs.Add($"OK: {item.NomeAluno} - Parcela {item.Parcela}");
                    }
                }
                catch (Exception ex)
                {
                    if (enviarWhatsapp)
                        resultado.WhatsAppFalhas++;

                    if (enviarEmail)
                        resultado.EmailFalhas++;

                    resultado.Logs.Add($"ERRO: {item.NomeAluno} - Parcela {item.Parcela} - {ex.Message}");
                }
            }

            return resultado;
        }
    }
}