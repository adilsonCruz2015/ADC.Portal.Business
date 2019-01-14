using Solucoes.Auxiliares.Especificacoes;
using Solucoes.Auxiliares.Interfaces.Especificacao;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ADC.Portal.Dominio.Escopos
{
    public class MinhaContaEscp
    {
        #region IdEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> IdEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>()
            {
                new RegraDeValidacao<TClasse>(new EhGuidEspc<TClasse>(expressao))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> IdEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>()
            {
                new RegraDeValidacao<TClasse>(new EhGuidEspc<TClasse>(expressao){ ChecarSeDevoIgnorar = checarSeDevoIgnorar})
            };
        }

        #endregion

        #region ChaveAcessoEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> ChaveAcessoEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhGuidEspc<TClasse>(expressao))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> ChaveAcessoEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
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
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 30))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> LoginEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 30) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region EmailEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> EmailEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 30)),
                new RegraDeValidacao<TClasse>(new EhEmailEspc<TClasse>(expressao))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> EmailEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 30) { ChecarSeDevoIgnorar = checarSeDevoIgnorar }),
                new RegraDeValidacao<TClasse>(new EhEmailEspc<TClasse>(expressao) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region CpfEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> CpfEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhCpfEspc<TClasse>(expressao)),
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 11))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> CpfEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhCpfEspc<TClasse>(expressao) { ChecarSeDevoIgnorar = checarSeDevoIgnorar }),
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 11) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region SenhaEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> SenhaEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 30))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> SenhaEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 30) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region ConfirmaSenhaEhValido        


        public IEnumerable<IRegraDeValidacao<TClasse>> ConfirmaSenhaEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, Expression<Func<TClasse, object>> expressaoCompare)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhIgualEspc<TClasse>(expressao, expressaoCompare)),
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 30))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> ConfirmaSenhaEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, Expression<Func<TClasse, object>> expressaoCompare, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhIgualEspc<TClasse>(expressao, expressaoCompare) { ChecarSeDevoIgnorar = checarSeDevoIgnorar }),
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 30) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion

        #region TokenAcessoEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> TokenAcessoEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>()
            {

            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> TokenAcessoEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>()
            {

            };
        }

        #endregion

        #region SenhaAtualEhValido

        public IEnumerable<IRegraDeValidacao<TClasse>> SenhaAtualEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 300))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> SenhaAtualEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool checarSeDevoIgnorar)
            where TClasse : IEhParaIgnorar<TClasse>
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 300) { ChecarSeDevoIgnorar = checarSeDevoIgnorar })
            };
        }

        #endregion
    }
}
