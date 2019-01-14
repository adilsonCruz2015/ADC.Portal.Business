using ADC.Portal.Dominio.Comandos.LoggerCmd;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.Interfaces.Servico;
using Solucoes.Auxiliares.Interfaces.Validacao;
using System;
using System.Collections.Generic;

namespace ADC.Portal.Aplicacao.Comum
{
    public class AplicacaoBase
        : IAutoValidacao
    {
        public AplicacaoBase(
            IUnidadeTrabalho udt,
            ILoggerServico log)
        {
            this._udt = udt;
            this._log = log;
            this.Referencia = this.GetType();
        }

        private IUnidadeTrabalho _udt;
        private ILoggerServico _log;

        public virtual INotificarValidacao Notificacoes { get; protected set; }

        protected virtual Type Referencia { get; set; }

        public virtual bool EhValido()
        {
            return this.Notificacoes.EhValido();
        }

        /// <summary>
        /// Verifica se o objeto é válido e adiciona os erros se houver
        /// </summary>
        /// <param name="objeto">Objeto a ser verificado</param>
        /// <returns>Retorna true caso não haja nenhum erro</returns>
        protected bool Validar(IAutoValidacao objeto)
        {
            bool resultado = objeto.EhValido();
            this.Notificacoes.Adicionar(objeto.Notificacoes);
            return resultado;
        }

        public void IniciarTransicao()
        {
            this._udt.IniciarTransicao();
        }

        /// <summary>
        /// Aplica Cometer se nhão hover erros, ou Desfazer se houver erros.
        /// </summary>
        public void EncerrarTransicao()
        {
            if (this.EhValido())
                this.Cometer();
            else
                this.Desfazer();
        }

        public void Cometer()
        {
            this._udt.SalvarAlteracoes();
            this.SalvarLogTemp();
        }

        public void Desfazer()
        {
            this._udt.DesfazerAlteracoes();
            this.SalvarLogTemp();
        }

        #region Logger

        private readonly IList<IInserirCmd> _logTemp = new List<IInserirCmd>();

        private void SalvarLogTemp()
        {
            this._udt.IniciarTransicao();

            int total = this._logTemp.Count;
            for (int i = 0; i < total; i++)
                this._log.Inserir(this._logTemp[i]);

            this._logTemp.Clear();

            this._udt.SalvarAlteracoes();
        }

        protected void LogDebug(string acao, string mensagem, Exception excecao = null)
        {
            IInserirCmd comando = new DebugCmd()
            {
                Acao = acao,
                Mensagem = mensagem,
                Tipo = this.Referencia,
                Excecao = excecao
            };
            if (this._udt.HaAlteracoes())
                this._logTemp.Add(comando);
            else
                this._log.Inserir(comando);
        }

        protected void LogDebug(IRegistrarLogger dados, Exception excecao = null)
        {
            this.LogDebug(dados.LoggerAcao(), dados.LoggerMensagem(), excecao);
        }

        protected void LogError(string acao, string mensagem)
        {
            IInserirCmd comando = new ErrorCmd()
            {
                Acao = acao,
                Mensagem = mensagem,
                Tipo = this.Referencia
            };
            if (this._udt.HaAlteracoes())
                this._logTemp.Add(comando);
            else
                this._log.Inserir(comando);
        }

        protected void LogError(IRegistrarLogger dados)
        {
            this.LogError(dados.LoggerAcao(), dados.LoggerMensagem());
        }

        protected void LogInfo(string acao, string mensagem)
        {
            IInserirCmd comando = new InfoCmd()
            {
                Acao = acao,
                Mensagem = mensagem,
                Tipo = this.Referencia
            };
            if (this._udt.HaAlteracoes())
                this._logTemp.Add(comando);
            else
                this._log.Inserir(comando);
        }

        protected void LogInfo(IRegistrarLogger dados)
        {
            this.LogInfo(dados.LoggerAcao(), dados.LoggerMensagem());
        }

        protected void LogWarn(string acao, string mensagem)
        {
            IInserirCmd comando = new WarnCmd()
            {
                Acao = acao,
                Mensagem = mensagem,
                Tipo = this.Referencia
            };
            if (this._udt.HaAlteracoes())
                this._logTemp.Add(comando);
            else
                this._log.Inserir(comando);
        }

        protected void LogWarn(IRegistrarLogger dados)
        {
            this.LogWarn(dados.LoggerAcao(), dados.LoggerMensagem());
        }

        protected void LogNotificacoes(IAutoValidacao validacao)
        {
            if (!validacao.Notificacoes.EhValido())
            {
                IInserirCmd comando = new WarnCmd()
                {
                    Acao = "Notificações",
                    Mensagem = ContratoJson.Serializar(validacao.Notificacoes.Mensagens),
                    Tipo = this.Referencia
                };

                if (this._udt.HaAlteracoes())
                    this._logTemp.Add(comando);
                else
                    this._log.Inserir(comando);
            }
        }

        #endregion
    }
}
