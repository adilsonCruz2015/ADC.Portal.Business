

namespace ADC.Portal.Dominio.Comandos.CursoCmds
{
    public class AtualizarParcialCmd : AtualizarCmd
    {
        protected override bool IgnorarNulo { get; set; } = true;

        public override string LoggerAcao()
        {
            return "Atulizar Curso Parcialmente";
        }
    }
}
