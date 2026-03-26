using System;

namespace Central_do_Educador.Models
{
    public class FiltroCobrancaLote
    {
        public string? Status { get; set; }
        public string? Contrato { get; set; }
        public string? NomeAluno { get; set; }
        public DateTime? VencimentoInicial { get; set; }
        public DateTime? VencimentoFinal { get; set; }
        public bool SomenteComTelefone { get; set; }
        public bool SomenteComEmail { get; set; }
    }
}