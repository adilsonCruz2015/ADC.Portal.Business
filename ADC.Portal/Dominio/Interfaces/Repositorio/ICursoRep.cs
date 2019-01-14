using ADC.Portal.Dominio.Comandos.CursoCmds;
using ADC.Portal.Dominio.Entidades;
using ADC.Portal.Dominio.Repositorio.Comum;
using Solucoes.Auxiliares.Interfaces.Validacao;
using System;
using System.Collections.Generic;

namespace ADC.Portal.Dominio.Interfaces.Repositorio
{
    public interface ICursoRep
        : IRepositorio<Curso, Guid>, IAutoValidacao
    {
        IEnumerable<Curso> Filtrar(FiltrarCmd comando);

        IEnumerable<Curso> EhUnico(Curso entidade);

        Curso NomeEhUnico(Curso entidade);

        Curso SiglaEhUnico(Curso entidade);

        Curso ObterPorNome(string valor);

        Curso ObterPorSigla(string valor);
    }
}
