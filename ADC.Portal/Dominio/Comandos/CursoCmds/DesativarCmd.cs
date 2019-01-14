using ADC.Portal.Dominio.Comandos.Comum;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using System;
using System.Collections.Generic;

namespace ADC.Portal.Dominio.Comandos.CursoCmds
{
    public class DesativarCmd : GuidCmd, IRegistrarLogger
    {
        public DesativarCmd() : base() { }

        public DesativarCmd(Guid? id) : base(id) { }

        public DesativarCmd(IEnumerable<Guid?> ids) : base(ids) { }

        #region Logger

        public string LoggerAcao()
        {
            return "Desativar curso";
        }

        public string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion
    }
}
