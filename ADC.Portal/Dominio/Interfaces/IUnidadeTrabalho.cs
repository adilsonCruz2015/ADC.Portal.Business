using System;

namespace ADC.Portal.Dominio.Interfaces
{
    public interface IUnidadeTrabalho : IDisposable
    {
        void IniciarTransicao();

        void SalvarAlteracoes();

        void DesfazerAlteracoes();

        bool HaAlteracoes();
    }
}
