using Solucoes.Auxiliares.Interfaces.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ADC.Portal.Dominio.Escopos.Comando.CursoEscps
{
    public class FiltrarEscp
    {
        protected CursoEscp _escpCurso = new CursoEscp();

        public IEnumerable<IRegraDeValidacao<TClasse>> CursooEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool ignorarNulo = false)
        {
            var resultado = this._escpCurso.IdEhValido<TClasse>(expressao).ToList();
            return resultado;
        }

        public IEnumerable<IRegraDeValidacao<TClasse>> StatusEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool ignorarNulo = false)
        {
            var resultado = this._escpCurso.StatusEhValido<TClasse>(expressao).ToList();
            return resultado;
        }
    }
}
