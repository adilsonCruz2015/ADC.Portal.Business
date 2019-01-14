using ADC.Portal.Dominio.ObjetoDeValor;
using Solucoes.Auxiliares.Especificacoes;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ADC.Portal.Dominio.Escopos
{
    public class CursoEscp
    {
        public IEnumerable<IRegraDeValidacao<TClasse>> IdEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>()
            {
                new RegraDeValidacao<TClasse>(new EhGuidEspc<TClasse>(expressao))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> NomeEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>()
            {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 100))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> DescricaoEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>()
            {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 300))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> SiglaEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>()
            {
                new RegraDeValidacao<TClasse>(new MaximoDeCaracteresEspc<TClasse>(expressao, 20))
            };
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> StatusEhValido<TClasse>(Expression<Func<TClasse, object>> expressao)
        {
            return new List<IRegraDeValidacao<TClasse>>() {
                new RegraDeValidacao<TClasse>(new EhEnumeradorEspc<TClasse>(expressao, typeof(Status)))
            };
        }    
    }
}
