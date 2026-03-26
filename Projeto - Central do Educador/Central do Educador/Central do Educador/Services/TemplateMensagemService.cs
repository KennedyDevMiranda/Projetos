using Central_do_Educador.Models;
using System.Globalization;

namespace Central_do_Educador.Services
{
    public class TemplateMensagemService
    {
        public string AplicarTemplate(string template, MensagemLoteItem item)
        {
            return template
                .Replace("{ALUNO}", item.NomeAluno)
                .Replace("{RESPONSAVEL}", item.NomeResponsavel)
                .Replace("{PARCELA}", item.Parcela)
                .Replace("{VALOR}", item.Valor.ToString("N2", new CultureInfo("pt-BR")))
                .Replace("{VENCIMENTO}", item.Vencimento.ToString("dd/MM/yyyy"))
                .Replace("{STATUS}", item.Status)
                .Replace("{CONTRATO}", item.Contrato);
        }
    }
}