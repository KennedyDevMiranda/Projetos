namespace Central_do_Educador
{
    public static class Sessao
    {
        public static int UsuarioId { get; set; }
        public static string UsuarioNome { get; set; } = "";
        public static string UsuarioLogin { get; set; } = "";
        public static string NivelUsuario { get; set; } = "OPERADOR"; // ADM, OPERADOR, VISUALIZADOR

        public static bool IsAdmin => NivelUsuario.Equals("ADM", StringComparison.OrdinalIgnoreCase);
    }
}