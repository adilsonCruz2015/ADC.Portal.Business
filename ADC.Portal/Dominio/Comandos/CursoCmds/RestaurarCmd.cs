using ADC.Portal.Dominio.Comandos.Comum;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using System;

namespace ADC.Portal.Dominio.Comandos.CursoCmds
{
    public class RestaurarCmd : GuidCmd, IRegistrarLogger
    {
        public RestaurarCmd() : base() { }

        public RestaurarCmd(Guid? id) : base(id) { }

        #region Logger

        public string LoggerAcao()
        {
            return "Restaurar curso";
        }

        public string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion
    }
}
