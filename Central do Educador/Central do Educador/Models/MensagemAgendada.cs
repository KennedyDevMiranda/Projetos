using System;
using System.Collections.Generic;
using System.Text;

namespace Central_do_Educador.Models
{
    public class MensagemAgendada
    {
        public int Id { get; set; }
        public string Destinatario { get; set; } = "";
        public string Telefone { get; set; } = "";
        public string Mensagem { get; set; } = "";
        public DateTime DataHoraAgendada { get; set; }
        public bool Ativo { get; set; }
        public string Status { get; set; } = "Pendente";
        public DateTime? UltimaTentativa { get; set; }
        public string Erro { get; set; } = "";
    }
}
