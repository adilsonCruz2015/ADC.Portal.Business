using ADC.Portal.Dominio.Entidades;
using ADC.Portal.Dominio.Escopos.Comando.CursoEscps;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ADC.Portal.Dominio.Comandos.CursoCmds
{
    public class InserirCmd : IAutoValidacao, 
        IRegistrarLogger, IAplicar<Curso>
    {
        public InserirCmd() {  }

        public InserirCmd(Curso valor)
            : this()
        {
            this.Nome = valor.Nome;
            this.Sigla = valor.Sigla;
            this.Descricao = valor.Descricao;
        }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public void Aplicar(ref Curso valor)
        {
            valor = new Curso(this.Nome, this.Sigla)
            {
                Descricao = this.Descricao
            };
        }

        public void Desfazer(ref Curso valor)
        {
            valor = null;
        }

        #region Auto Validação

        private readonly Validacao<InserirCmd> _regra = new Validacao<InserirCmd>();

        public virtual INotificarValidacao Notificacoes { get; protected internal set; } = new NotificarValidacao();

        public virtual bool EhValido()
        {
            if (this._regra.ObterRegras().Count().Equals(0))
            {
                InserirEscp escopo = new InserirEscp();
                this._regra.AdicionarRegra(escopo.NomeEhValido<InserirCmd>(x => x.Nome));
                this._regra.AdicionarRegra(escopo.SiglaEhValido<InserirCmd>(x => x.Sigla));
                this._regra.AdicionarRegra(escopo.DescricaoEhValido<InserirCmd>(x => x.Descricao));
            }
            this.Notificacoes = this._regra.Validar(this);
            return this.Notificacoes.EhValido();
        }

        #endregion

        #region Logger

        public string LoggerAcao()
        {
            return "Inserir curso";
        }

        public string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion
    }
}
