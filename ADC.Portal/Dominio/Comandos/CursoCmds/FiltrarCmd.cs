using ADC.Portal.Dominio.Escopos.Comando.CursoEscps;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.ObjetoDeValor;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ADC.Portal.Dominio.Comandos.CursoCmds
{
    public class FiltrarCmd : Comum.FiltrarCmd,
        IAutoValidacao, IRegistrarLogger
    {

        public override void Limpar()
        {
            base.Limpar();

        }

        [Display(Name ="Por Nome")]
        public virtual string PorNome { get; set; }

        [Display(Name = "Por Sigla")]
        public virtual string PorSigla { get; set; }

        public virtual IList<Guid> Cursos { get; set; } = new List<Guid>();

        public virtual IList<Status> Status { get; set; } = new List<Status>();

        #region Auto Validação

        private readonly Validacao<FiltrarCmd> _regra = new Validacao<FiltrarCmd>();

        public virtual INotificarValidacao Notificacoes { get; protected internal set; } = new NotificarValidacao();

        public virtual bool EhValido()
        {
            if (this._regra.ObterRegras().Count().Equals(0))
            {
                FiltrarEscp escopo = new FiltrarEscp();
                this._regra.AdicionarRegra(escopo.CursooEhValido<FiltrarCmd>(x => x.Cursos));
                this._regra.AdicionarRegra(escopo.StatusEhValido<FiltrarCmd>(x => x.Status));
            }

            this.Notificacoes = this._regra.Validar(this);
            return this.Notificacoes.EhValido();
        }

        #endregion

        #region Logger

        public virtual string LoggerAcao()
        {
            return "Filtrar curso";
        }

        public virtual string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion
    }
}
