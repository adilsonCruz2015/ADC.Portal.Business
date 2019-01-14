using ADC.Portal.Dominio.Especificacoes.UsuarioEspcs;
using ADC.Portal.Dominio.ObjetoDeValor;
using Solucoes.Auxiliares.Especificacoes;
using Solucoes.Auxiliares.Interfaces.Especificacao;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ADC.Portal.Dominio.Escopos
{
    public class UsuarioEscp
    {
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
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 50))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> NomeEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 50) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region SobrenomeEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> SobrenomeEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 100))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> SobrenomeEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 100) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region LoginEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> LoginEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 30)),
                new RegraDeValidacao<TClasse>(new NaoEhEmailEspc<TClasse>(expressao)),
                new RegraDeValidacao<TClasse>(new NaoEhCpfEspc<TClasse>(expressao))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> LoginEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 30) { ChecarSeDevoIgnorar = checarSeDevoIgnorar }),
                new RegraDeValidacao<TClasse>(new NaoEhEmailEspc<TClasse>(expressao) { ChecarSeDevoIgnorar = checarSeDevoIgnorar }),
                new RegraDeValidacao<TClasse>(new NaoEhCpfEspc<TClasse>(expressao){ ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region EmailEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> EmailEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 300)),
                new RegraDeValidacao<TClasse>(new EhEmailEspc<TClasse>(expressao))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> EmailEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 300) { ChecarSeDevoIgnorar = checarSeDevoIgnorar }),
                new RegraDeValidacao<TClasse>(new EhEmailEspc<TClasse>(expressao) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region SenhaEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> SenhaEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 300))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> SenhaEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 300) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region CpfEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> CpfEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhCpfEspc<TClasse>(expressao))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> CpfEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhCpfEspc<TClasse>(expressao) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region StatusEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> StatusEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhEnumeradorEspc<TClasse>(expressao, typeof(Status)))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> StatusEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhEnumeradorEspc<TClasse>(expressao, typeof(Status)) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region Nivel De Acesso

        public IEnumerable<IRegraDeValidacao<TClasse>> NivelAcessoEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhEnumeradorEspc<TClasse>(expressao, typeof(NivelAcesso)))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> NivelAcessoEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhEnumeradorEspc<TClasse>(expressao, typeof(NivelAcesso)) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion
    }
}
