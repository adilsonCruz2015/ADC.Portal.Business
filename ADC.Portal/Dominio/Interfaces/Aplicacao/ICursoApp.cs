using ADC.Portal.Dominio.Comandos.CursoCmds;
using ADC.Portal.Dominio.Entidades;
using Solucoes.Auxiliares.Interfaces.Validacao;
using System.Collections.Generic;

namespace ADC.Portal.Dominio.Interfaces.Aplicacao
{
    public interface ICursoApp
        : IAutoValidacao
    {
        Curso Obter(ObterCmd comando);

        IEnumerable<Curso> Filtrar(FiltrarCmd comando);

        Curso Inserir(InserirCmd comando);

        Curso Atualizar(AtualizarCmd comando);

        void Deletar(DeletarCmd comando);

        void Ativar(AtivarCmd comando);

        void Desativar(DesativarCmd comando);

        void Restaurar(RestaurarCmd comando);
    }
}
