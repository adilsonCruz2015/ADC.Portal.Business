using ADC.Portal.Dominio.Escopos;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.ObjetoDeValor;
using Newtonsoft.Json;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ADC.Portal.Dominio.Entidades
{
    public class Logger : IAutoValidacao,
        ICriadoEm, IRegistrarLogger
    {
        [JsonConstructor]
        protected internal Logger()
        {
            this.Id = Guid.NewGuid();
            this.CriadoEm = DateTime.Now;
            this.Thread = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
        }

        public Logger(
                      NivelLogger nivel, Guid rastreio,
                      string acao, string mensagem,
                      int ordem, DateTime iniciaEm,
                      Type tipo, MinhaConta usuario = null)
            : this()
        {
            this.Nivel = nivel;
            this.Rastreio = rastreio;
            this.Acao = acao;
            this.Mensagem = mensagem;
            this.Ordem = ordem;
            this.IniciaEm = iniciaEm;
            this.Usuario = new LoggerUsuario(usuario);
            this.Namespace = tipo.FullName;
        }

        [JsonProperty]
        public virtual Guid Id { get; protected internal set; }

        [JsonProperty]
        public virtual Guid Rastreio { get; protected internal set; }

        [JsonProperty]
        public virtual int Ordem { get; protected internal set; }

        [JsonProperty]
        public virtual string Thread { get; protected internal set; }

        [JsonProperty]
        [Display(Name = "Usuário")]
        public virtual LoggerUsuario Usuario { get; protected internal set; }

        [JsonProperty]
        [Display(Name = "Nível")]
        public virtual NivelLogger Nivel { get; protected internal set; }

        [JsonProperty]
        [Display(Name = "Namespace da classe")]
        public virtual string Namespace { get; protected internal set; }

        [JsonProperty]
        [Display(Name = "Exceção")]
        public virtual Exception Excecao { get; protected internal set; }

        [JsonProperty]
        [Display(Name = "Ação")]
        public virtual string Acao { get; protected internal set; }

        [JsonProperty]
        public virtual string Mensagem { get; protected internal set; }

        [JsonProperty]
        public virtual string Ip { get; protected internal set; }

        [JsonProperty]
        public virtual string Useragent { get; protected internal set; }

        [JsonProperty]
        [Display(Name = "Criado em")]
        public virtual DateTime CriadoEm { get; protected internal set; }

        [JsonProperty]
        [Display(Name = "Inicia em")]
        public virtual DateTime IniciaEm { get; protected internal set; }

        public override string ToString()
        {
            return this.Acao;
        }

        #region Operador

        public override bool Equals(object obj)
        {
            Logger comparar = obj as Logger;
            return !object.Equals(comparar, null)
                && this.GetHashCode() == comparar.GetHashCode();
        }

        public override int GetHashCode()
        {
            return string.Format("{0}:{1}", this.Id, this.GetType()).GetHashCode();
        }

        public static bool operator ==(Logger a, Logger b)
        {
            return object.Equals(a, null) && object.Equals(b, null)
                || (!object.Equals(a, null) && !object.Equals(b, null) && a.Equals(b));
        }

        public static bool operator !=(Logger a, Logger b)
        {
            return !(object.Equals(a, null) && object.Equals(b, null)
                || (!object.Equals(a, null) && !object.Equals(b, null) && a.Equals(b)));
        }

        #endregion

        #region Auto Validação

        private readonly Validacao<Logger> _regra = new Validacao<Logger>();

        public virtual INotificarValidacao Notificacoes { get; protected internal set; } = new NotificarValidacao();

        public virtual bool EhValido()
        {
            if (this._regra.ObterRegras().Count().Equals(0))
            {
                LoggerEscp escopo = new LoggerEscp();
                this._regra.AdicionarRegra(escopo.NomeEhValido<Logger>(x => x.Usuario.Nome));
                this._regra.AdicionarRegra(escopo.NivelLoggerEhValido<Logger>(x => x.Nivel));
                this._regra.AdicionarRegra(escopo.RastreioEhValido<Logger>(x => x.Rastreio));
                this._regra.AdicionarRegra(escopo.CriadoEmEhValido<Logger>(x => x.CriadoEm));
                this._regra.AdicionarRegra(escopo.UsuarioEhValido<Logger>(x => x.Usuario.Id));
            }

            this.Notificacoes = this._regra.Validar(this);
            return this.Notificacoes.EhValido();
        }

        #endregion

        DateTime ICriadoEm.CriadoEm
        {
            get { return this.CriadoEm; }
            set { this.CriadoEm = value; }
        }

        #region Logger

        public virtual string LoggerAcao()
        {
            return "Informação do logger";
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
