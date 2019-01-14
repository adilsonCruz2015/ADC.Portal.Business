

using ADC.Portal.Dominio.Comandos.Comum;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using System;

namespace ADC.Portal.Dominio.Comandos.LoggerCmd
{
    public class ObterCmd : GuidCmd, IRegistrarLogger
    {
        public ObterCmd() : base() { }

        public ObterCmd(Guid? id) : base(id) { }

        #region Logger

        public string LoggerAcao()
        {
            return "Obter logger";
        }

        public string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion
    }
}
