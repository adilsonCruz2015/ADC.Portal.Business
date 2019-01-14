

using Solucoes.Auxiliares.Interfaces.Especificacao;
using Solucoes.Auxiliares.Interfaces.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ADC.Portal.Dominio.Escopos.Comando.LoggerEscps
{
    public class FiltrarEscp
    {
        public FiltrarEscp()
        {
            this._escpLogger = new LoggerEscp();
        }

        private LoggerEscp _escpLogger;

        #region LoggerEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> LoggerEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            var resultado = _escpLogger.IdEhValido<TClasse>(expressao).ToList();
            return resultado;
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> LoggerEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            var resultado = _escpLogger.IdEhValido<TClasse>(expressao, checarSeDevoIgnorar).ToList();
            return resultado;
        }

        #endregion

        #region UsuarioEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> UsuarioEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            var resultado = _escpLogger.UsuarioEhValido<TClasse>(expressao).ToList();
            return resultado;
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> UsuarioEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            var resultado = _escpLogger.UsuarioEhValido<TClasse>(expressao, checarSeDevoIgnorar).ToList();
            return resultado;
        }

        #endregion

        #region NivelLoggerEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> NivelLoggerEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            var resultado = _escpLogger.NivelLoggerEhValido<TClasse>(expressao).ToList();
            return resultado;
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> NivelLoggerEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            var resultado = _escpLogger.NivelLoggerEhValido<TClasse>(expressao, checarSeDevoIgnorar).ToList();
            return resultado;
        }

        #endregion

        #region RastreioEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> RastreioEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            var resultado = _escpLogger.RastreioEhValido<TClasse>(expressao).ToList();
            return resultado;
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> RastreioEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            var resultado = _escpLogger.RastreioEhValido<TClasse>(expressao, checarSeDevoIgnorar).ToList();
            return resultado;
        }

        #endregion

    }
}
