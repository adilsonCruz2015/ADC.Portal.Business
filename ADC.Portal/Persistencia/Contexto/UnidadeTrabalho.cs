using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Persistencia.Contexto.Interfaces;
using System;

namespace ADC.Portal.Persistencia.Contexto
{
    public class UnidadeTrabalho
        : IUnidadeTrabalho, IDisposable
    {
        public UnidadeTrabalho(IConexao connexao)
        {
            this._connexao = connexao;
        }

        private readonly IConexao _connexao;

        public void Dispose()
        {
            if (this._connexao.HaSessao())
                if (this._connexao.HaTransicao())
                    this.SalvarAlteracoes();

            GC.SuppressFinalize(this);
        }

        public void IniciarTransicao()
        {
            this._connexao.IniciarTransicao();
        }

        public void SalvarAlteracoes()
        {
            this._connexao.FecharTransicao();
        }

        public void DesfazerAlteracoes()
        {
            this._connexao.DesfazerTransicao();
        }

        public bool HaAlteracoes()
        {
            return this._connexao.HaTransicao();
        }
    }
}
