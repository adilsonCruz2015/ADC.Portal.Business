using ADC.Portal.Dominio.Escopos.Comando.LoggerEscps;
using ADC.Portal.Dominio.Fabricas;
using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.ObjetoDeValor;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using Log = ADC.Portal.Dominio.Entidades;

namespace ADC.Portal.Dominio.Comandos.LoggerCmd
{
    public class FiltrarCmd : Comum.FiltrarCmd<FiltrarCmd>,
        IAutoValidacao, IRegistrarLogger
    {
        public FiltrarCmd()
        {
            this.Logs = new List<Guid>();
            this.Niveis = new List<NivelLogger>();
            this.Rastreios = new List<Guid>();
            this.Usuarios = new List<Guid>();

            this._regras = new Validacao<FiltrarCmd>();
        }

        #region Logs --------------------------------------

        public IList<Guid> Logs { get; set; }
        public FiltrarCmd AdicionarLog(Guid valor)
        {
            this.Logs.Add(valor);
            return this;
        }
        public FiltrarCmd AdicionarLog(Log.Logger valor)
        {
            this.Logs.Add(valor.Id);
            return this;
        }
        public FiltrarCmd RemoverLog(Guid valor)
        {
            this.Logs.Remove(valor);
            return this;
        }
        public FiltrarCmd RemoverLog(Log.Logger valor)
        {
            this.Logs.Remove(valor.Id);
            return this;
        }
        public FiltrarCmd ConcatenarLogs(IEnumerable<Guid> valores)
        {
            this.Logs = this.Logs.Concat(valores).ToList();
            return this;
        }
        public FiltrarCmd ConcatenarLogs(IEnumerable<Log.Logger> valores)
        {
            this.Logs = this.Logs.Concat(valores.Select(x => x.Id)).ToList();
            return this;
        }
        public FiltrarCmd LimparLogs()
        {
            this.Logs.Clear();
            return this;
        }
        public IEnumerable<Guid> ObterLogs()
        {
            return this.Logs;
        }

        #endregion

        #region Rastreios --------------------------------------

        public IList<Guid> Rastreios { get; set; }
        public FiltrarCmd AdicionarRastreios(Guid valor)
        {
            this.Rastreios.Add(valor);
            return this;
        }
        public FiltrarCmd AdicionarRastreios(Log.Logger valor)
        {
            this.Rastreios.Add(valor.Id);
            return this;
        }
        public FiltrarCmd RemoverRastreios(Guid valor)
        {
            this.Rastreios.Remove(valor);
            return this;
        }
        public FiltrarCmd RemoverRastreios(Log.Logger valor)
        {
            this.Logs.Remove(valor.Id);
            return this;
        }
        public FiltrarCmd ConcatenarRastreios(IEnumerable<Guid> valores)
        {
            this.Logs = this.Logs.Concat(valores).ToList();
            return this;
        }
        public FiltrarCmd ConcatenarRastreios(IEnumerable<Log.Logger> valores)
        {
            this.Logs = this.Logs.Concat(valores.Select(x => x.Id)).ToList();
            return this;
        }
        public FiltrarCmd LimparRastreios()
        {
            this.Logs.Clear();
            return this;
        }
        public IEnumerable<Guid> ObterRastreios()
        {
            return this.Logs;
        }

        #endregion

        #region Usuarios --------------------------------------

        public IList<Guid> Usuarios { get; set; }
        public FiltrarCmd AdicionarUsuarios(Guid valor)
        {
            this.Usuarios.Add(valor);
            return this;
        }
        public FiltrarCmd AdicionarUsuarios(Log.Logger valor)
        {
            this.Usuarios.Add(valor.Id);
            return this;
        }
        public FiltrarCmd RemoverUsuarios(Guid valor)
        {
            this.Usuarios.Remove(valor);
            return this;
        }
        public FiltrarCmd RemoverUsuarios(Log.Logger valor)
        {
            this.Logs.Remove(valor.Id);
            return this;
        }
        public FiltrarCmd ConcatenarUsuarios(IEnumerable<Guid> valores)
        {
            this.Logs = this.Logs.Concat(valores).ToList();
            return this;
        }
        public FiltrarCmd ConcatenarUsuarios(IEnumerable<Log.Logger> valores)
        {
            this.Logs = this.Logs.Concat(valores.Select(x => x.Id)).ToList();
            return this;
        }
        public FiltrarCmd LimparUsuarios()
        {
            this.Logs.Clear();
            return this;
        }
        public IEnumerable<Guid> ObterUsuarios()
        {
            return this.Logs;
        }

        #endregion

        #region Comum -------------------------------

        public override FiltrarCmd DefinirMaximo(int valor)
        {
            this.Maximo = valor < 1 ? 0 : valor;
            return this;
        }

        public override FiltrarCmd DefinirPagina(int valor)
        {
            this.Pagina = valor < 1 ? 0 : valor;
            return this;
        }

        public override FiltrarCmd DefinirPalavraChave(string valor)
        {
            this.PalavraChave = valor;
            return this;
        }

        #endregion

        #region NivelLogger --------------------------------------

        public IList<NivelLogger> Niveis { get; set; }
        public FiltrarCmd AdicionarNivelLogger(NivelLogger valor)
        {
            this.Niveis.Add(valor);
            return this;
        }
        public FiltrarCmd RemoverNivelLogger(NivelLogger valor)
        {
            this.Niveis.Remove(valor);
            return this;
        }
        public FiltrarCmd ConcatenarNivelLogger(IEnumerable<NivelLogger> valores)
        {
            this.Niveis = this.Niveis.Concat(valores).ToList();
            return this;
        }
        public FiltrarCmd LimparNivelLogger()
        {
            this.Niveis.Clear();
            return this;
        }
        public IEnumerable<NivelLogger> ObterNivelLogger()
        {
            return this.Niveis;
        }

        #endregion

        #region Auto Validação --------------------------

        private readonly Validacao<FiltrarCmd> _regras;

        public INotificarValidacao Notificacoes { get; set; }

        public bool EhValido()
        {
            if (this._regras.ObterRegras().Count().Equals(0))
            {
                FiltrarEscp escopo = new FiltrarEscp();
                this._regras.AdicionarRegra(escopo.LoggerEhValido<FiltrarCmd>(x => x.Logs));
                this._regras.AdicionarRegra(escopo.UsuarioEhValido<FiltrarCmd>(x => x.Usuarios));
                this._regras.AdicionarRegra(escopo.RastreioEhValido<FiltrarCmd>(x => x.Rastreios));
                this._regras.AdicionarRegra(escopo.NivelLoggerEhValido<FiltrarCmd>(x => x.Niveis));
            }
            this.Notificacoes = this._regras.Validar(this);
            return this.Notificacoes.EhValido();
        }

        #endregion

        #region Logger

        public string LoggerAcao()
        {
            return "Filtrar Loggers";
        }

        public string LoggerMensagem()
        {
            return ContratoLogJson.Serializar(this);
        }

        #endregion
    }
}
