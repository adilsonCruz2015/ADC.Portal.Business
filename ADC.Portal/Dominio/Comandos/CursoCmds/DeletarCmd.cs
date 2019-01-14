using ADC.Portal.Dominio.Comandos.Comum;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using System;
using System.Collections.Generic;

namespace ADC.Portal.Dominio.Comandos.CursoCmds
{
    public class DeletarCmd : GuidCmd, IRegistrarLogger
    {
        public DeletarCmd() : base() {  }

        public DeletarCmd(Guid? id) : base(id) { }

        public DeletarCmd(IEnumerable<Guid?> ids) : base(ids) { }

        #region Logger

        public string LoggerAcao()
        {
            return "Deletar curso";
        }

        public string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion
    }
}
