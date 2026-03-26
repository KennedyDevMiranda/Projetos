public static class ControleTaxaGridHelper
{
    public static void AjustarColunas(DataGridView dgv)
    {
        dgv.AutoGenerateColumns = true;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.MultiSelect = false;

        HideIfExists(dgv, "AlunoId");
        HideIfExists(dgv, "UsuarioId");

        RenameIfExists(dgv, "NomeAluno", "Nome do Aluno");
        RenameIfExists(dgv, "NomeUsuario", "Usuário");
        RenameIfExists(dgv, "DataFalta", "Data da Falta");
        RenameIfExists(dgv, "DataRegistro", "Data do Registro");
        RenameIfExists(dgv, "DataReposicao", "Data de Reposição");
        RenameIfExists(dgv, "Quantidade", "Quantidade");
        RenameIfExists(dgv, "Lancado", "Lançado");
        RenameIfExists(dgv, "Observacao", "Observação");

        FormatDateIfExists(dgv, "DataFalta");
        FormatDateIfExists(dgv, "DataRegistro");
        FormatDateIfExists(dgv, "DataReposicao");
    }

    private static void HideIfExists(DataGridView dgv, string name)
    {
        if (dgv.Columns.Contains(name))
            dgv.Columns[name].Visible = false;
    }

    private static void RenameIfExists(DataGridView dgv, string name, string header)
    {
        if (dgv.Columns.Contains(name))
            dgv.Columns[name].HeaderText = header;
    }

    private static void FormatDateIfExists(DataGridView dgv, string name)
    {
        if (dgv.Columns.Contains(name))
            dgv.Columns[name].DefaultCellStyle.Format = "dd/MM/yyyy";
    }
}