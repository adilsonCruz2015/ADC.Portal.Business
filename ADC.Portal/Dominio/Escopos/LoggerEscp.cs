using ADC.Portal.Dominio.ObjetoDeValor;
using Solucoes.Auxiliares.Especificacoes;
using Solucoes.Auxiliares.Interfaces.Especificacao;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ADC.Portal.Dominio.Escopos
{
    public class LoggerEscp
    {

        public LoggerEscp()
        {
            this._usuarioEscp = new UsuarioEscp();
        }

        private UsuarioEscp _usuarioEscp;

        #region IdEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> IdEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhGuidEspc<TClasse>(expressao))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> IdEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhGuidEspc<TClasse>(expressao) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region NomeEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> NomeEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 250))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> NomeEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 250) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region NivelLoggerEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> NivelLoggerEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhEnumeradorEspc<TClasse>(expressao, typeof(NivelLogger)))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> NivelLoggerEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhEnumeradorEspc<TClasse>(expressao, typeof(NivelLogger)) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region RastreioEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> RastreioEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhGuidEspc<TClasse>(expressao))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> RastreioEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhGuidEspc<TClasse>(expressao) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region CriadoEmEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> CriadoEmEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhDataHoraEspc<TClasse>(expressao))
            };

        }

        public IEnumerable<IRegraDeValidacao<TClasse>> CriadoEmEmEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhDataHoraEspc<TClasse>(expressao){ ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region UsuarioEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> UsuarioEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            var resultado = this._usuarioEscp.IdEhValido<TClasse>(expressao).ToList();
            return resultado;
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> UsuarioEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            var resultado = this._usuarioEscp.IdEhValido<TClasse>(expressao, checarSeDevoIgnorar).ToList();
            return resultado;
        }

        #endregion
    }
}
