using ADC.Portal.Dominio.Entidades;
using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.ObjetoDeValor;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ADC.Portal.Dominio.Comandos.LoggerCmd
{
    public class InfoCmd : IInserirCmd
    {
        public InfoCmd()
        {
            this._regras = new Validacao<InfoCmd>();
        }

        [Display(Name = "Ação")]
        public virtual string Acao { get; set; }

        public virtual string Mensagem { get; set; }

        public virtual Type Tipo { get; set; }

        public virtual void Aplicar(
            ref Logger dados, Guid rastreio, int ordem,
            DateTime iniciadoEm, IInformacaoDoAmbiente ambiente,
            MinhaConta usuario)
        {
            dados = new Logger(
                NivelLogger.Info, rastreio,
                this.Acao, this.Mensagem, ordem,
                iniciadoEm, this.Tipo, usuario)
            {
                Useragent = ambiente.ObterUseragent(),
                Ip = ambiente.ObterIp()
            };
        }

        public virtual void Desfazer(ref Logger dados)
        {
            dados = null;
        }


        #region Auto Validação

        private readonly Validacao<InfoCmd> _regras;

        public INotificarValidacao Notificacoes { get; set; }

        public bool EhValido()
        {
            if (this._regras.ObterRegras().Count().Equals(0))
            {

            }

            this.Notificacoes = this._regras.Validar(this);
            return this.Notificacoes.EhValido();
        }

        #endregion        
    }
}
