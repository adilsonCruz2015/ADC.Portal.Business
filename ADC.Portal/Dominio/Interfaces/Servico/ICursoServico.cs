using ADC.Portal.Dominio.Comandos.CursoCmds;
using ADC.Portal.Dominio.Entidades;
using Solucoes.Auxiliares.Interfaces.Validacao;
using System;
using System.Collections.Generic;

namespace ADC.Portal.Dominio.Interfaces.Servico
{
    public interface ICursoServico
        : Comum.IServico<Curso, Guid>, IAutoValidacao
    {
        IEnumerable<Curso> Filtrar(FiltrarCmd comando);

        Curso Obter(ObterCmd comando);

        Curso Inserir(InserirCmd comando);

        Curso Atualizar(AtualizarCmd comando);

        void AtualizarStatus(AtualizarStatusCmd comando);
    }
}
