using ADC.Portal.Dominio.Entidades;
using ADC.Portal.Dominio.Escopos.Comando.CursoEscps;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ADC.Portal.Dominio.Comandos.CursoCmds
{
    public class AtualizarCmd : IRegistrarLogger, 
        IAplicar<Curso>, IAutoValidacao
    {
        public AtualizarCmd()
        {
            this._regras = new Validacao<AtualizarCmd>();
        }

        public AtualizarCmd(Curso valor)
            : this()
        {
            this.Id = valor.Id;
            this.Nome = valor.Nome;
            this.Sigla = valor.Sigla;
            this.Descricao = valor.Descricao;
        }

        protected virtual bool IgnorarNulo { get; set; } = false;        

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }        

        public void Aplicar(ref Curso valor)
        {
            if (!object.Equals(this.Nome, null))
                valor.Nome = this.Nome;

            if (!object.Equals(this.Sigla, null))
                valor.Sigla = this.Sigla;

            if (!object.Equals(this.Descricao, null))
                valor.Descricao = this.Descricao;
        }

        public void Desfazer(ref Curso valor)
        {
            valor = null;
        }

        #region Logger

        public virtual string LoggerAcao()
        {
            return "Atualizar instituto";
        }

        public string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion

        #region Auto Validação

        private readonly Validacao<AtualizarCmd> _regras;

        public INotificarValidacao Notificacoes { get; set; }

        public bool EhValido()
        {
            if (this._regras.ObterRegras().Count().Equals(0))
            {
                AtualizarEscp escopo = new AtualizarEscp();

                this._regras.AdicionarRegra(escopo.IdEhValido<AtualizarCmd>(x => x.Id));
                this._regras.AdicionarRegra(escopo.NomeEhValido<AtualizarCmd>(x => x.Nome, this.IgnorarNulo));
                this._regras.AdicionarRegra(escopo.SiglaEhValido<AtualizarCmd>(x => x.Sigla, this.IgnorarNulo));
                this._regras.AdicionarRegra(escopo.DescricaoEhValido<AtualizarCmd>(x => x.Descricao, this.IgnorarNulo));
            }
            this.Notificacoes = this._regras.Validar(this);
            return this.Notificacoes.EhValido();
        }
        #endregion
    }
}
