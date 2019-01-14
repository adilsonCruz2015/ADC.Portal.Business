using ADC.Portal.Dominio.Escopos;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.ObjetoDeValor;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ADC.Portal.Dominio.Entidades
{
    public class Curso : IAutoValidacao,
        ICriadoEm, IAlteradoEm, IRegistrarLogger
    {
        public Curso()
        {
            this.Id = Guid.NewGuid();
        }

        public Curso(string nome, string sigla)
            :this()
        {
            this.Nome = nome;
            this.Sigla = sigla;
        }

        [Display(Name="Curso")]
        public virtual Guid Id { get; protected internal set; }

        public virtual string Nome { get;  set; }

        [Display(Name ="Descrição")]
        public virtual string Descricao { get;  set; }

        public virtual string Sigla { get;  set; }

        public virtual Status Status { get;  set; }

        [Display(Name ="Criado Em")]
        public virtual DateTime CriadoEm { get; protected internal set; }

        [Display(Name ="Alterado Em")]
        public virtual DateTime AlteradoEm { get; protected internal set; }

        public override string ToString()
        {
            return this.Nome;
        }

        #region Operador -------------------------------------

        public override bool Equals(object obj)
        {
            Curso comparar = obj as Curso;
            return !object.Equals(comparar, null)
                && this.GetHashCode() == comparar.GetHashCode();
        }

        public override int GetHashCode()
        {
            return string.Format("[Curso:{0}]", this.Id).GetHashCode();
        }

        public static bool operator ==(Curso a, Curso b)
        {
            return object.Equals(a, null) && object.Equals(b, null)
                || (!object.Equals(a, null) && !object.Equals(b, null) && a.Equals(b));
        }

        public static bool operator !=(Curso a, Curso b)
        {
            return !(object.Equals(a, null) && object.Equals(b, null)
                || (!object.Equals(a, null) && !object.Equals(b, null) && a.Equals(b)));
        }

        #endregion

        #region Validacao

        private readonly Validacao<Curso> _regra = new Validacao<Curso>();

        public virtual INotificarValidacao Notificacoes { get; protected internal set; } = new NotificarValidacao();

        public virtual bool EhValido()
        {
            if(this._regra.ObterRegras().Count().Equals(0))
            {
                CursoEscp escopo = new CursoEscp();
                this._regra.AdicionarRegra(escopo.IdEhValido<Curso>(x => x.Id));
                this._regra.AdicionarRegra(escopo.NomeEhValido<Curso>(x => x.Nome));
                this._regra.AdicionarRegra(escopo.SiglaEhValido<Curso>(x => x.Sigla));
                this._regra.AdicionarRegra(escopo.DescricaoEhValido<Curso>(x => x.Descricao));
                this._regra.AdicionarRegra(escopo.StatusEhValido<Curso>(x => x.Status));
            }
            this.Notificacoes = this._regra.Validar(this);
            return this.Notificacoes.EhValido();
        }


        #endregion

        DateTime IAlteradoEm.AlteradoEm
        {
            get { return this.AlteradoEm; }
            set { this.AlteradoEm = value;  }
        }

        DateTime ICriadoEm.CriadoEm
        {
            get { return this.CriadoEm; }
            set { this.CriadoEm = value; }
        }

        #region Logger

        public virtual string LoggerAcao()
        {
            return "Informação do curso";
        }

        public virtual string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(new
            {
                @Id = this.Id,
                @Informacao = this.ToString()
            });
        }

        #endregion
    }
}
