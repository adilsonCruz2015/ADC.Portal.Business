using ADC.Portal.Dominio.Comandos.Comum;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using System;
using System.Collections.Generic;

namespace ADC.Portal.Dominio.Comandos.CursoCmds
{
    public class ObterCmd : GuidCmd, IRegistrarLogger
    {
        public ObterCmd() : base() { }

        public ObterCmd(Guid? id) : base(id) { }

        public ObterCmd(IEnumerable<Guid?> ids) : base(ids) { }

        #region Logger

        public string LoggerAcao()
        {
            return "Obter curso";
        }

        public string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion
    }
}
