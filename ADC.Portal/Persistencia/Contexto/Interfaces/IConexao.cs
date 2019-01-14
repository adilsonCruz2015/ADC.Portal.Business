using NHibernate;
using System;

namespace ADC.Portal.Persistencia.Contexto.Interfaces
{
    public interface IConexao : IDisposable
    {
        void Fechar();

        ISession Abrir();

        bool HaSessao();

        bool HaTransicao();

        void IniciarTransicao();

        void FecharTransicao();

        void DesfazerTransicao();

        ISession Sessao { get; }
    }
}
