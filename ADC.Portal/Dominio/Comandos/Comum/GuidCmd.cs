using Solucoes.Auxiliares.Especificacoes;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADC.Portal.Dominio.Comandos.Comum
{
    public class GuidCmd
    {
        public GuidCmd(){ }

        public GuidCmd(Guid? id)
            : this()
        {
            if (!object.Equals(id, null))
                this.Id.Add(id);
        }

        public GuidCmd(IEnumerable<Guid?> ids)
            : this()
        {
            this.Id = object.Equals(ids, null) 
                ? null : ids.ToList();
        }

        public IList<Guid?> Id { get; private set; } = new List<Guid?>();

        #region Auto Validação

        protected Validacao<GuidCmd> _regra = new Validacao<GuidCmd>();

        public virtual INotificarValidacao Notificacoes { get; protected internal set; } = new NotificarValidacao();

        protected virtual void MontarRegra()
        {
            this._regra.AdicionarRegra(new EhRequeridoEspc<GuidCmd>(y => y.Id));
            this._regra.AdicionarRegra(new EhGuidEspc<GuidCmd>(y => y.Id));
        }

        public virtual bool EhValido()
        {
            if (this._regra.ObterRegras().Count().Equals(0))
                this.MontarRegra();

            this.Notificacoes = this._regra.Validar(this);
            return this.Notificacoes.EhValido();
        }

        #endregion
    }
}
