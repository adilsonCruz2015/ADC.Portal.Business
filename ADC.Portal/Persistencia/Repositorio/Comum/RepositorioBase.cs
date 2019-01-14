using ADC.Portal.Persistencia.Contexto.Interfaces;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADC.Portal.Persistencia.Repositorio.Comum
{
    public abstract class RepositorioBase : IAutoValidacao
    {
        public RepositorioBase(IConexao conexao)
        {
            this._conexao = conexao;
            this.Notificacoes = new NotificarValidacao();
        }

        public virtual bool EhValido()
        {
            return this.Notificacoes.EhValido();
        }

        public virtual INotificarValidacao Notificacoes { get; protected set; }

        protected bool Validar(IAutoValidacao objeto)
        {
            bool resultado = objeto.EhValido();
            this.Notificacoes.Adicionar(objeto.Notificacoes);
            return resultado;             
        }

        private readonly IConexao _conexao;

        public IConexao Conexao
        {
            get { return this._conexao; }
        }

        protected bool HaItens<TTipo>(IEnumerable<TTipo> valor)
        {
            return !object.Equals(valor, null)
                && !valor.Count().Equals(0);
        }

        protected string DataComoString(DateTime data)
        {
            return data.ToString("yyyy-MM-dd");
        }

        protected string DataHoraComoString(DateTime data)
        {
            return data.ToString("yyyy-MM-ddTHH:mm:ss.fff");
        }
    }
}
