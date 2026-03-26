using System.Windows.Forms;

namespace Central_do_Educador.Services
{
    public static class ControleTaxaFormBinder
    {
        public static void AjustarReposicao(CheckBox chkNaoAgendado, DateTimePicker dtpReposicao)
        {
            if (chkNaoAgendado.Checked)
            {
                dtpReposicao.Enabled = false;
                dtpReposicao.Checked = false;
            }
            else
            {
                dtpReposicao.Enabled = true;
            }
        }
    }
}