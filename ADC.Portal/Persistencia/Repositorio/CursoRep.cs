using ADC.Portal.Dominio.Comandos.CursoCmds;
using ADC.Portal.Dominio.Entidades;
using ADC.Portal.Dominio.Interfaces.Repositorio;
using ADC.Portal.Dominio.ObjetoDeValor;
using ADC.Portal.Persistencia.Contexto.Interfaces;
using ADC.Portal.Persistencia.Repositorio.Mensagem;
using NHibernate.Transform;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ADC.Portal.Persistencia.Repositorio
{
    public class CursoRep
        : Comum.Repositorio<Curso, Guid>, ICursoRep
    {
        public CursoRep(IConexao conexao)
            : base(conexao) { }

        public IEnumerable<Curso> EhUnico(Curso entidade)
        {
            IList<Curso> resultado = new List<Curso>();
            Curso item = null;
            INotificarValidacao notificacoes = new NotificarValidacao();

            item = this.NomeEhUnico(entidade);
            notificacoes.Adicionar(this);
            if (!this.EhValido())
                resultado.Add(item);

            item = this.SiglaEhUnico(entidade);
            notificacoes.Adicionar(this);
            if (!this.EhValido())
                resultado.Add(item);

            this.Notificacoes.Limpar();
            this.Notificacoes.Adicionar(notificacoes);

            return resultado.ToArray();
        }

        public IEnumerable<Curso> Filtrar(FiltrarCmd comando)
        {
            this.Notificacoes.Limpar();
            IList<Curso> resultado = new List<Curso>();
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            StringBuilder sqlPalavraChave = new StringBuilder();
            IList<string> textoPalavraChave = comando.DesmebrarPalavraChave();

            sql.Append("    SELECT  cur FROM Curso AS cur ");

            if (this.HaItens<Status>(comando.Status))
                sql.Append(" AND cur.Status IN (:Status) ");

            if(this.HaItens<Guid>(comando.Cursos))
                sql.Append(" AND cur.Id IN (:CursoId) ");

            if(!string.IsNullOrWhiteSpace(comando.PorNome))
                sql.Append(" AND cur.Nome = :PorNome ");

            if (!string.IsNullOrWhiteSpace(comando.PorSigla))
                sql.Append(" AND cur.Sigla = :PorSigla ");

            if (this.HaItens<string>(textoPalavraChave))
            {
                sqlFiltro.Append(" AND ( ");
                for (int i = 0; i < textoPalavraChave.Count(); i++)
                {
                    sqlPalavraChave.Append(string.Format(" OR cur.Nome LIKE CollateLatinGeneral(:texto{0}) ", i));
                    sqlPalavraChave.Append(string.Format(" OR cur.Sigla LIKE CollateLatinGeneral(:texto{0}) ", i));
                }
                sqlFiltro.Append(Regex.Replace(sqlPalavraChave.ToString(), @"^ OR ", ""));
                sqlFiltro.Append(" ) ");
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^ AND ", " WHERE "));
            sql.Append(" ORDER BY cur.AlteradoEm DESC ");

            var consulta = this.Conexao.Sessao.CreateQuery(sql.ToString());

            consulta.SetMaxResults(comando.Maximo);
            consulta.SetFirstResult((comando.Pagina - 1) * comando.Maximo);
            consulta.SetResultTransformer(new DistinctRootEntityResultTransformer());

            if (this.HaItens<Guid>(comando.Cursos))
                consulta.SetParameterList("CursoId", comando.Cursos);

            if (this.HaItens<Status>(comando.Status))
                consulta.SetParameterList("Status", comando.Status);

            if (!string.IsNullOrWhiteSpace(comando.PorNome))
                consulta.SetString("PorNome", comando.PorNome);

            if (!string.IsNullOrWhiteSpace(comando.PorSigla))
                consulta.SetString("PorSigla", comando.PorSigla);

            if (this.HaItens<string>(textoPalavraChave))
            {
                for (int i = 0; i < textoPalavraChave.Count(); i++)
                {
                    consulta.SetString(string.Format("texto{0}", i), string.Format("%{0}%", textoPalavraChave[i]));
                }
            }

            resultado = consulta.List<Curso>();

            if (resultado.Count().Equals(0))
                this.Notificacoes.Erro(NotificacoesMsg_Br.NaoEncontrados);

            return resultado;

        }        

        public Curso NomeEhUnico(Curso entidade)
        {
            this.Notificacoes.Limpar();
            Curso resultado = null;

            if(!string.IsNullOrWhiteSpace(entidade?.Nome))
            {
                resultado = this.Conexao.Sessao.QueryOver<Curso>()
                    .Where(x => x.Nome == entidade.Nome
                        && x.Id != entidade.Id).Take(1).List()
                        .FirstOrDefault();

                if (!object.Equals(resultado, null))
                    this.Notificacoes.Erro<Curso>(x => x.Nome, NotificacoesMsg_Br.XNaoEhUnico);
            }

            return resultado;
        }

        public Curso ObterPorNome(string valor)
        {
            this.Notificacoes.Limpar();
            Curso resultado = null;
            if (!string.IsNullOrWhiteSpace(valor))
            {
                resultado = this.Conexao.Sessao.QueryOver<Curso>()
                    .WhereRestrictionOn(x => x.Nome)
                    .IsInsensitiveLike(valor)
                    .SingleOrDefault();
            }

            if (object.Equals(resultado, null))
                this.Notificacoes.Erro(NotificacoesMsg_Br.NaoEncontrados);

            return resultado;
        }

        public Curso ObterPorSigla(string valor)
        {
            this.Notificacoes.Limpar();
            Curso resultado = null;
            if(!string.IsNullOrWhiteSpace(valor))
            {
                resultado = this.Conexao.Sessao.QueryOver<Curso>()
                    .WhereRestrictionOn(x => x.Sigla)
                    .IsInsensitiveLike(valor)
                    .SingleOrDefault();
            }

            if (object.Equals(resultado, null))
                this.Notificacoes.Erro(NotificacoesMsg_Br.NaoEncontrados);

            return resultado;
        }

        public Curso SiglaEhUnico(Curso entidade)
        {
            this.Notificacoes.Limpar();
            Curso resultado = null;

            if (!string.IsNullOrWhiteSpace(entidade?.Sigla))
            {
                resultado = this.Conexao.Sessao.QueryOver<Curso>()
                    .Where(x => x.Sigla == entidade.Sigla
                        && x.Id != entidade.Id).Take(1).List()
                        .FirstOrDefault();

                if (!object.Equals(resultado, null))
                    this.Notificacoes.Erro<Curso>(x => x.Sigla, NotificacoesMsg_Br.XNaoEhUnico);
            }

            return resultado;
        }
    }
}
