﻿using Solucoes.Auxiliares.Especificacoes;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ADC.Portal.Dominio.Escopos.Comando.CursoEscps
{
    public class AtualizarEscp : InserirEscp
    {
        public IEnumerable<IRegraDeValidacao<TClasse>> IdEhValido<TClasse>(Expression<Func<TClasse, object>> expressao, bool ignorarNulo = false)
        {
            var resultado = this._escpCurso.IdEhValido<TClasse>(expressao).ToList();
            resultado.Add(new RegraDeValidacao<TClasse>(new EhRequeridoEspc<TClasse>(expressao, ignorarNulo)));
            return resultado;
        }
    }
}
