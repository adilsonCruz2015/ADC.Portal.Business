

using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.ObjetoDeValor;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ADC.Portal.Dominio.Comandos.LoggerCmd
{
    public class DebugCmd : IInserirCmd
    {
        public DebugCmd()
        {
            this._regras = new Validacao<DebugCmd>();
        }

        [Display(Name = "Ação")]
        public virtual string Acao { get; set; }

        public virtual string Mensagem { get; set; }

        public virtual Type Tipo { get; set; }

        [Display(Name = "Exceção")]
        public virtual Exception Excecao { get; set; }

        public void Aplicar(
            ref Entidades.Logger dados, Guid rastreio, int ordem,
            DateTime iniciadoEm, IInformacaoDoAmbiente ambiente,
            Entidades.MinhaConta usuario)
        {
            dados = new Entidades.Logger(NivelLogger.Debug, rastreio,
                this.Acao, this.Mensagem, ordem,
                iniciadoEm, this.Tipo, usuario)
            {
                Excecao = this.Excecao,
                Useragent = ambiente.ObterUseragent(),
                Ip = ambiente.ObterIp()
            };
        }

        public void Desfazer(ref Entidades.Logger dados)
        {
            dados = null;
        }


        #region Auto Validação

        private readonly Validacao<DebugCmd> _regras;

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
