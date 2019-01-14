using Solucoes.Auxiliares.Interfaces.Especificacao;
using Solucoes.Auxiliares.Notacoes.Validacao;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Solucoes.Auxiliares.Extensoes;

namespace ADC.Portal.Dominio.Especificacoes.UsuarioEspcs
{
    public class NaoEhEmailEspc<TClasse> : IEspecificacao<TClasse>
    {
        protected Expression<Func<TClasse, object>> Expressao;
        protected string Propriedade;
        protected string Nome;

        EhEmailAttribute Validar;

        public NaoEhEmailEspc(Expression<Func<TClasse, object>> expressao)
        {
            this.Expressao = expressao;
            this.Propriedade = expressao.PropExtensoComTrilha();
            this.Nome = expressao.PropNome();
            this.Referencia = this.Propriedade ?? this.Nome;
            this.Validar = new EhEmailAttribute();
            this.Mensagem = "{0} não pode ser e-mail.";
        }

        public bool EhSatisfeitoPor(TClasse entidade)
        {
            IEhParaIgnorar<TClasse> checar = entidade as IEhParaIgnorar<TClasse>;
            if (this.ChecarSeDevoIgnorar && checar.EhParaIgnorar(this.Expressao))
                return true;

            object valor = entidade.ObterValor(this.Propriedade);

            return object.Equals(valor, null) || !this.Validar.IsValid(valor);
        }

        private string _mensagem;
        public string Mensagem
        {
            get { return this._mensagem; }
            set
            {
                this._mensagem = value;

                if (!string.IsNullOrWhiteSpace(this.Propriedade))
                    this._mensagem = Regex.Replace(this._mensagem, @"\{0\}", this.Nome);

            }
        }

        public string Referencia { get; set; }

        public bool ChecarSeDevoIgnorar { get; set; }
    }
}
