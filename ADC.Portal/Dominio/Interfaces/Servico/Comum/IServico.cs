using Solucoes.Auxiliares.Interfaces.Validacao;
using System.Collections.Generic;

namespace ADC.Portal.Dominio.Interfaces.Servico.Comum
{
    public interface IServico<TEntidade, TIdentificador> : IAutoValidacao
        where TEntidade : class, IAutoValidacao
    {
        TEntidade Obter(TIdentificador id);

        void Inserir(TEntidade entidade);

        void Atualizar(TEntidade entidade);

        void Deletar(TEntidade entidade);
        void Deletar(TIdentificador id);
        void Deletar(IEnumerable<TIdentificador> ids);
    }
}
