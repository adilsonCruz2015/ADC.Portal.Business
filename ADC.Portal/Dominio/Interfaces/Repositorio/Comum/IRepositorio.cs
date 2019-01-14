using System.Collections.Generic;

namespace ADC.Portal.Dominio.Repositorio.Comum
{
    public interface IRepositorio<TEntidade, TIdentificador>
        where TEntidade : class
    {
        void Inserir(TEntidade entidade);

        void Atualizar(TEntidade entidade);

        void Deletar(TEntidade entidade);
        void Deletar(TIdentificador id);
        void Deletar(IEnumerable<TIdentificador> ids);

        TEntidade Obter(TIdentificador id);
    }
}
