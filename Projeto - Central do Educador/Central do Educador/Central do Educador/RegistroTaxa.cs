public class RegistroTaxa
{
    public int Id { get; set; }
    public string Aluno { get; set; } = "";
    public DateTime DataFalta { get; set; }
    public DateTime DataRegistro { get; set; }
    public DateTime? DataReposicao { get; set; }
    public int Quantidade { get; set; }
    public bool Lancado { get; set; }
    public bool Historico { get; set; }
    public string? Observacao { get; set; }
    public string Usuario { get; set; } = "";
}