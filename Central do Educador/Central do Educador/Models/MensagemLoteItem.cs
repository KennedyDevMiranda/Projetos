using System;

namespace Central_do_Educador.Models
{
    public class MensagemLoteItem
    {
        public int AlunoId { get; set; }
        public string NomeAluno { get; set; } = "";
        public string NomeResponsavel { get; set; } = "";

        public string TelefoneAluno { get; set; } = "";
        public string TelefoneResponsavel { get; set; } = "";

        public string EmailAluno { get; set; } = "";
        public string EmailResponsavel { get; set; } = "";

        public int ParcelaId { get; set; }
        public string Parcela { get; set; } = "";
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public string Status { get; set; } = "";
        public string Contrato { get; set; } = "";

        public string MensagemFinal { get; set; } = "";
    }
}