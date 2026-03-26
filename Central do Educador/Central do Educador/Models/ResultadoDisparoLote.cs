using System.Collections.Generic;

namespace Central_do_Educador.Models
{
    public class ResultadoDisparoLote
    {
        public int TotalItens { get; set; }
        public int WhatsAppEnviados { get; set; }
        public int WhatsAppFalhas { get; set; }
        public int EmailEnviados { get; set; }
        public int EmailFalhas { get; set; }
        public List<string> Logs { get; set; } = new();
    }
}