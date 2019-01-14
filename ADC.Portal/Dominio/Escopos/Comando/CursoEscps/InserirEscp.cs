using Solucoes.Auxiliares.Especificacoes;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ADC.Portal.Dominio.Escopos.Comando.CursoEscps
{
    public class InserirEscp
    {
        protected CursoEscp _escpCurso = new CursoEscp();

        public IEnumerable<IRegraDeValidacao<TClasse>> NomeEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool ignorarNulo = false)
        {
            var resultado = this._escpCurso.NomeEhValido<TClasse>(expressao).ToList();
            resultado.Add(new RegraDeValidacao<TClasse>(new EhRequeridoEspc<TClasse>(expressao, ignorarNulo)));
            return resultado;
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> SiglaEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool ignorarNulo = false)
        {
            var resultado = this._escpCurso.SiglaEhValido<TClasse>(expressao).ToList();
            resultado.Add(new RegraDeValidacao<TClasse>(new EhRequeridoEspc<TClasse>(expressao, ignorarNulo)));
            return resultado;
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> DescricaoEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool ignorarNulo = false)
        {
            var resultado = this._escpCurso.DescricaoEhValido<TClasse>(expressao).ToList();
            return resultado;
        }
    }
}
