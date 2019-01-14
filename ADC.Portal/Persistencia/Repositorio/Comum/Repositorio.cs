using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ADC.Portal.Dominio.Repositorio.Comum;
using ADC.Portal.Persistencia.Contexto.Interfaces;
using ADC.Portal.Persistencia.Repositorio.Mensagem;
using Solucoes.Auxiliares.Interfaces.Validacao;

namespace ADC.Portal.Persistencia.Repositorio.Comum
{
    public abstract class Repositorio<TEntidade, TIdentificador>
        : RepositorioBase, IRepositorio<TEntidade, TIdentificador>
        where TEntidade : class, IAutoValidacao
    {
        public Repositorio(IConexao conexao)
            : base(conexao) {  }

        public virtual void Atualizar(TEntidade entidade)
        {
            this.Notificacoes.Limpar();
            if (this.Validar(entidade))
            {
                this.Conexao.Sessao.Update(entidade);
            }
        }

        public void Deletar(TEntidade entidade)
        {
            this.Notificacoes.Limpar();
            this.Conexao.Sessao.Delete(entidade);
        }

        public void Deletar(TIdentificador id)
        {
            this.Notificacoes.Limpar();
            this.Deletar(new List<TIdentificador>() { id });
        }

        public void Deletar(IEnumerable<TIdentificador> ids)
        {
            var queryString = string.Format("DELETE {0} WHERE Id IN (:id)", typeof(TEntidade));
            this.Conexao.Sessao.CreateQuery(queryString)
               .SetParameterList("id", ids)
               .ExecuteUpdate();
        }

        public virtual void Inserir(TEntidade entidade)
        {
            this.Notificacoes.Limpar();
            if(this.Validar(entidade))
            {
                this.Conexao.Sessao.Save(entidade);
            }
        }

        public TEntidade Obter(TIdentificador id)
        {
            this.Notificacoes.Limpar();
            TEntidade resultado = (TEntidade)this.Conexao.Sessao.Get<TEntidade>(id);

            if (object.Equals(resultado, null))
                this.Notificacoes.Erro(NotificacoesMsg_Br.NaoEncontrado);

            return resultado;
        }

        public TEntidade Obter<Tipo>(TIdentificador id, Expression<Func<Tipo, object>> expressao)
        {
            this.Notificacoes.Limpar();
            TEntidade resultado = (TEntidade)this.Conexao.Sessao.Get<TEntidade>(id);

            if (object.Equals(resultado, null))
                this.Notificacoes.Erro<Tipo>(expressao, NotificacoesMsg_Br.XNaoEncontrado);

            return resultado;
        }
    }
}
