using ADC.Portal.Dominio.Entidades;
using ADC.Portal.Dominio.Escopos.Comando.CursoEscps;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.ObjetoDeValor;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADC.Portal.Dominio.Comandos.CursoCmds
{
    public class AtualizarStatusCmd : IRegistrarLogger, IAplicar<Curso>
    {
        protected internal AtualizarStatusCmd() { }

        public AtualizarStatusCmd(Curso atual)
            : this()
        {
            this.Id.Add(atual.Id);
            this.Status = atual.Status;
        }

        public AtualizarStatusCmd(Guid? id, Status status)
            : this()
        {
            if (id != null)
                this.Id.Add(id.Value);

            this.Status = status;
        }

        public AtualizarStatusCmd(IEnumerable<Guid> ids, Status status)
            : this()
        {
            this.Id = this.Id.Concat(ids).ToList();
            this.Status = status;
        }

        public IList<Guid> Id { get; set; } = new List<Guid>();

        public Status? Status { get; set; }

        public void Aplicar(ref Curso valor)
        {
            valor.Status = this.Status.Value;
        }

        public void Desfazer(ref Curso valor)
        {
            valor = null;
        }

        #region Auto Validação
               
        private readonly Validacao<AtualizarStatusCmd> _regras = new Validacao<AtualizarStatusCmd>();
        public virtual INotificarValidacao Notificacoes { get; protected internal set; } = new NotificarValidacao();

        public bool EhValido()
        {
            if(this._regras.ObterRegras().Count().Equals(0))
            {
                AtualizarStatusEscp escopo = new AtualizarStatusEscp();
                this._regras.AdicionarRegra(escopo.IdEhValido<AtualizarStatusCmd>(x => x.Id));
                this._regras.AdicionarRegra(escopo.StatusEhValido<AtualizarStatusCmd>(x => x.Status));
            }
            this.Notificacoes = this._regras.Validar(this);
            return this.Notificacoes.EhValido();
        }

        #endregion

        #region Logger

        public string LoggerAcao()
        {
            return "Atualizar status do curso";
        }

        public string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion
    }
}
