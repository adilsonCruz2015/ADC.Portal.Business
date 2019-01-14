using ADC.Portal.Dominio.Escopos;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.ObjetoDeValor;
using ADC.Portal.Dominio.Seguranca;
using Newtonsoft.Json;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Notacoes.Validacao;
using Solucoes.Auxiliares.Seguranca;
using Solucoes.Auxiliares.Validacao;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace ADC.Portal.Dominio.Entidades
{
    public class MinhaConta : IAutoValidacao,
        ICriadoEm, IAlteradoEm, INivelAcesso, IRegistrarLogger
    {
        [JsonConstructor]
        internal protected MinhaConta()
        {
            this.Id = Guid.NewGuid();
            this.CriadoEm = DateTime.Now;
            this.AlteradoEm = DateTime.Now;
        }

        public MinhaConta(
            string nome,
            string sobrenome,
            string email,
            string cpf,
            string senha)
            : this()
        {
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Email = email;
            this.Cpf = cpf;
            this.Senha = senha;

        }

        [JsonProperty]
        [Display(Name = "Minha Conta")]
        public virtual Guid Id { get; protected internal set; }

        public virtual string Nome { get; set; }

        public virtual string Sobrenome { get; set; }

        public virtual string Login { get; set; }

        [Display(Name = "E-mail")]
        public virtual string Email { get; set; }

        protected internal virtual string Senha { get; set; }

        private string _cpf;
        public virtual string Cpf
        {
            get { return this._cpf; }
            set { this._cpf = !object.Equals(value, null) ? Regex.Replace(value, @"[.|-]", "").Trim() : value; }

        }

        [JsonProperty]
        [Display(Name = "Nível de acesso")]
        public virtual NivelAcesso NivelAcesso { get; protected internal set; }

        protected internal virtual Status Status { get; set; }

        public virtual bool EstaAutenticado()
        {
            return !string.IsNullOrEmpty(this.Cpf) && !string.IsNullOrEmpty(this.Senha);
        }

        public virtual bool SenhaEhValida(string senha, bool md5)
        {
            string compara = md5 ? MD5Hash.Obter(this.Senha).ToLower() : this.Senha;
            return compara.Equals(senha);
        }

        public virtual string CodificarToken()
        {
            return this.CodificarToken("2q52WGgXt4wV");
        }

        public virtual string CodificarToken(string chave)
        {
            return Token.EncriptarTexto(this.Id.ToString(), string.Format("minha_conta-{0}", chave));
        }

        public static Guid DecodificarToken(string token)
        {
            return DecodificarToken(token, "2q52WGgXt4wV");
        }

        public static Guid DecodificarToken(string token, string chave)
        {
            EhGuidAttribute validar = new EhGuidAttribute();
            string resultado = Token.DecriptarTexto(token, string.Format("minha_conta-{0}", chave));
            return !string.IsNullOrWhiteSpace(resultado)
                && validar.IsValid(resultado) ? Guid.Parse(resultado) : Guid.Empty;
        }

        [Display(Name = "Criado em")]
        public virtual DateTime CriadoEm { get; protected internal set; }

        [Display(Name = "Alterado em")]
        public virtual DateTime AlteradoEm { get; protected internal set; }

        #region Operador

        public override bool Equals(object obj)
        {
            MinhaConta comparar = obj as MinhaConta;
            return !object.Equals(comparar, null)
                && this.GetHashCode() == comparar.GetHashCode();
        }

        public override int GetHashCode()
        {
            return string.Format("{0}:{1}", this.Id, this.GetType()).GetHashCode();
        }

        public static bool operator ==(MinhaConta a, MinhaConta b)
        {
            return object.Equals(a, null) && object.Equals(b, null)
                || (!object.Equals(a, null) && !object.Equals(b, null) && a.Equals(b));
        }

        public static bool operator !=(MinhaConta a, MinhaConta b)
        {
            return !(object.Equals(a, null) && object.Equals(b, null)
                || (!object.Equals(a, null) && !object.Equals(b, null) && a.Equals(b)));
        }

        #endregion

        #region Auto Validação

        private readonly Validacao<MinhaConta> _regra = new Validacao<MinhaConta>();

        public virtual INotificarValidacao Notificacoes { get; protected internal set; } = new NotificarValidacao();

        public virtual bool EhValido()
        {
            if (this._regra.ObterRegras().Count().Equals(0))
            {
                MinhaContaEscp escopo = new MinhaContaEscp();
                this._regra.AdicionarRegra(escopo.NomeEhValido<MinhaConta>(x => x.Nome));
                this._regra.AdicionarRegra(escopo.SobrenomeEhValido<MinhaConta>(x => x.Sobrenome));
                this._regra.AdicionarRegra(escopo.LoginEhValido<MinhaConta>(x => x.Login));
                this._regra.AdicionarRegra(escopo.EmailEhValido<MinhaConta>(x => x.Email));
                this._regra.AdicionarRegra(escopo.SenhaEhValido<MinhaConta>(x => x.Senha));
                this._regra.AdicionarRegra(escopo.CpfEhValido<MinhaConta>(x => x.Cpf));
            }

            this.Notificacoes = this._regra.Validar(this);
            return this.Notificacoes.EhValido();
        }

        #endregion

        DateTime IAlteradoEm.AlteradoEm
        {
            get { return this.AlteradoEm; }
            set { this.AlteradoEm = value; }
        }

        DateTime ICriadoEm.CriadoEm
        {
            get { return this.CriadoEm; }
            set { this.CriadoEm = value; }
        }

        #region Logger

        public virtual string LoggerAcao()
        {
            return "Informação da minha conta";
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
