using ADC.Portal.Dominio.Escopos;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.ObjetoDeValor;
using ADC.Portal.Dominio.Seguranca;
using Newtonsoft.Json;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Notacoes.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ADC.Portal.Dominio.Entidades
{
    public class Usuario : IAutoValidacao,
        ICriadoEm, IAlteradoEm, INivelAcesso, IUsuario, IRegistrarLogger
    {
        [JsonConstructor]
        protected internal Usuario()
        {
            this.Id = Guid.NewGuid();
            this.CriadoEm = DateTime.Now;
            this.AlteradoEm = DateTime.Now;
            this.Status = Status.Ativo;
        }

        public Usuario(string nome, string sobrenome, string email)
            : this()
        {
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Email = email;
        }

        [JsonProperty]
        [Display(Name = "Usuário")]
        public virtual Guid Id { get; protected internal set; }

        public virtual string Nome { get; set; }

        public virtual string Sobrenome { get; set; }

        public virtual string Login { get; set; }

        [Display(Name = "E-mail")]
        public virtual string Email { get; set; }

        protected internal virtual string Senha { get; set; }

        public virtual string Cpf { get; set; }

        [JsonProperty]
        [Display(Name = "Nível de acesso")]
        public virtual NivelAcesso NivelAcesso { get; protected internal set; }

        public virtual Status Status { get; set; }

        [JsonProperty]
        [Display(Name = "Criado em")]
        public virtual DateTime CriadoEm { get; protected internal set; }

        [JsonProperty]
        [Display(Name = "Alterado em")]
        public virtual DateTime AlteradoEm { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Nome, this.Sobrenome).Trim();
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

        public virtual bool EhAutorizadoSobre(IUsuario comparar)
        {
            return this.EhAutorizadoSobre(comparar.NivelAcesso);
        }

        public virtual bool EhAutorizadoSobre(INivelAcesso comparar)
        {
            return this.EhAutorizadoSobre(comparar.NivelAcesso);
        }

        public virtual bool EhAutorizadoSobre(NivelAcesso comparar)
        {
            int nivel = (int)this.NivelAcesso;
            int nivelComparar = (int)comparar;

            if (nivelComparar < 1)
                throw new ArgumentException(string.Format("Nível de comparação deve ser diferende de {0}", NivelAcesso.Indefinido.ToString()), "comparar");

            if (nivel < 1)
                throw new Exception(string.Format("Nível de comparação deve ser diferende de {0}", NivelAcesso.Indefinido.ToString()));

            return nivel < nivelComparar;
        }

        public virtual bool EhAutorizadoComo(NivelAcesso comparar)
        {
            int nivel = (int)this.NivelAcesso;
            int nivelComparar = (int)comparar;

            if (nivelComparar < 1)
                throw new ArgumentException(string.Format("Nível de comparação deve ser diferende de {0}", NivelAcesso.Indefinido.ToString()), "comparar");

            if (nivel < 1)
                throw new Exception(string.Format("Nível de comparação deve ser diferende de {0}", NivelAcesso.Indefinido.ToString()));

            return nivel <= nivelComparar;
        }

        public virtual bool EstaAutenticado()
        {
            return !string.IsNullOrEmpty(this.Cpf) && !string.IsNullOrEmpty(this.Senha);
        }

        #region Operador

        public override bool Equals(object obj)
        {
            Usuario comparar = obj as Usuario;
            return !object.Equals(comparar, null)
                && this.GetHashCode() == comparar.GetHashCode();
        }

        public override int GetHashCode()
        {
            return string.Format("{0}:{1}", this.Id, this.GetType()).GetHashCode();
        }

        public static bool operator ==(Usuario a, Usuario b)
        {
            return object.Equals(a, null) && object.Equals(b, null)
                || (!object.Equals(a, null) && !object.Equals(b, null) && a.Equals(b));
        }

        public static bool operator !=(Usuario a, Usuario b)
        {
            return !(object.Equals(a, null) && object.Equals(b, null)
                || (!object.Equals(a, null) && !object.Equals(b, null) && a.Equals(b)));
        }

        #endregion

        #region Auto Validação

        private readonly Validacao<Usuario> _regra = new Validacao<Usuario>();

        public virtual INotificarValidacao Notificacoes { get; protected internal set; } = new NotificarValidacao();

        public virtual bool EhValido()
        {
            if (this._regra.ObterRegras().Count().Equals(0))
            {
                UsuarioEscp escopo = new UsuarioEscp();
                this._regra.AdicionarRegra(escopo.NomeEhValido<Usuario>(x => x.Nome));
                this._regra.AdicionarRegra(escopo.SobrenomeEhValido<Usuario>(x => x.Sobrenome));
                this._regra.AdicionarRegra(escopo.LoginEhValido<Usuario>(x => x.Login));
                this._regra.AdicionarRegra(escopo.EmailEhValido<Usuario>(x => x.Email));
                this._regra.AdicionarRegra(escopo.SenhaEhValido<Usuario>(x => x.Senha));
                this._regra.AdicionarRegra(escopo.CpfEhValido<Usuario>(x => x.Cpf));
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
            return "Informação do usuário";
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
