using ADC.Portal.Dominio.Comandos.Comum;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using System;

namespace ADC.Portal.Dominio.Comandos.CursoCmds
{
    public class AtivarCmd : GuidCmd, IRegistrarLogger
    {
        public AtivarCmd() : base() { }

        public AtivarCmd(Guid? id) : base(id) { }

        #region Logger

        public string LoggerAcao()
        {
            return "Ativar Curso";
        }

        public string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion
    }
}
