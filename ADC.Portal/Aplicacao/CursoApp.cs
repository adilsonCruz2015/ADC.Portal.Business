using System;
using System.Collections.Generic;
using ADC.Portal.Dominio.Comandos.CursoCmds;
using ADC.Portal.Dominio.Entidades;
using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Dominio.Interfaces.Aplicacao;
using ADC.Portal.Dominio.Interfaces.Servico;

namespace ADC.Portal.Aplicacao
{
    public class CursoApp 
        : Comum.AplicacaoBase, ICursoApp
    {
        private readonly ICursoServico _servico;

        public CursoApp(IUnidadeTrabalho udt,
                        ICursoServico servico,
                        ILoggerServico log)
            :base(udt, log)
        {
            this._servico = servico;

        }

        public void Ativar(AtivarCmd comando)
        {
            throw new System.NotImplementedException();
        }

        public Curso Atualizar(AtualizarCmd comando)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(DeletarCmd comando)
        {
            throw new System.NotImplementedException();
        }

        public void Desativar(DesativarCmd comando)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Curso> Filtrar(FiltrarCmd comando)
        {
            throw new System.NotImplementedException();
        }

        public Curso Inserir(InserirCmd comando)
        {
            throw new System.NotImplementedException();
        }

        public Curso Obter(ObterCmd comando)
        {
            throw new System.NotImplementedException();
        }

        public void Restaurar(RestaurarCmd comando)
        {
            throw new System.NotImplementedException();
        }
    }
}
