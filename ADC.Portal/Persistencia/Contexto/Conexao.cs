using ADC.Portal.Persistencia.Contexto.Configuracoes.CustomSqlDialect;
using ADC.Portal.Persistencia.Contexto.Convention;
using ADC.Portal.Persistencia.Contexto.Interceptor;
using ADC.Portal.Persistencia.Contexto.Interfaces;
using ADC.Portal.Persistencia.Contexto.Mapementos;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Configuration;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using Solucoes.Auxiliares;
using FluentNHibernate.Conventions.Helpers;
using System;

namespace ADC.Portal.Persistencia.Contexto
{
    public class Conexao : IConexao
    {
        public Conexao(IResolverConexao dadosDeConexao)
        {
            this._dadosDeConexao = dadosDeConexao;
        }

        private ISession _sessao;
        private readonly IResolverConexao _dadosDeConexao;

        private ISessionFactory SessaoComFluentNHibernate()
        {
            FluentConfiguration _configuracao;

            _configuracao = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .Dialect<CustomSqlDialectMsSql2012>()
                .ConnectionString(this._dadosDeConexao.ObterConexao()))
                .Mappings(map => {
                    map.FluentMappings.Conventions.AddFromAssemblyOf<ColumnConvention>();
                    map.FluentMappings.AddFromAssemblyOf<CursoMap>();
                }).ExposeConfiguration(config => {
                    config.Interceptor = new EntityInterceptor();
                });

            return _configuracao.BuildSessionFactory();
        }

        //private ISessionFactory SessaoComNHibernate()
        //{
        //    var configuration = new Configuration();
        //    configuration.Configure(AppInfo.Local(@"~\NHibernate\ADC.Portal.cfg.xml"));
        //    configuration.AddDirectory(new DirectoryInfo(AppInfo.Local(@"~\NHibernate\ADC.Portal")));
        //    configuration.DataBaseIntegration(db => {
        //        db.Dialect<CustomSqlDialectMsSql2012>();
        //    }).SetInterceptor(new EntityInterceptor());

        //    var factory = configuration.BuildSessionFactory();

        //    return factory;
        //}

        private static ISessionFactory _fabricaDeSessao;

        private ISessionFactory FabricaDeSessao
        {
            get
            {
                if (object.Equals(_fabricaDeSessao, null))
                    _fabricaDeSessao = this.SessaoComFluentNHibernate();

                return _fabricaDeSessao;
            }
        }

        public ISession Sessao
        {
            get { return this._sessao ?? this.Abrir(); }
            private set { this._sessao = value;  }
        }

        public static void GerarHBM(string nomeDaConexao)
        {
            string dir = AppInfo.Local(@"~\NHibernate\TvWeb.Portal");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            FluentConfiguration configuracao = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .Dialect<CustomSqlDialectMsSql2012>()
                .ConnectionString(x => x.FromConnectionStringWithKey(nomeDaConexao)))
                .Mappings(x => {
                    x.FluentMappings.Conventions.Setup(y => {
                        y.AddFromAssemblyOf<ColumnConvention>();
                        y.Add(AutoImport.Never());
                    });
                    x.FluentMappings.AddFromAssemblyOf<CursoMap>().ExportTo(dir);
                });

            configuracao.BuildSessionFactory();
        }

        public static void CriarTabela(string nomeDaConexao)
        {
            FluentConfiguration configuracao = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(x => x.FromConnectionStringWithKey(nomeDaConexao)))
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .Mappings(x => {
                    x.FluentMappings.Conventions.Setup(y => {
                        y.AddFromAssemblyOf<ColumnConvention>();
                        y.Add(AutoImport.Never());
                    });
                    x.FluentMappings.AddFromAssemblyOf<CursoMap>();
                });

            configuracao.BuildSessionFactory();
        }

        public ISession Abrir()
        {
            if (object.Equals(this._sessao, null) || this._sessao.IsOpen == false)
            {
                this._sessao = this.FabricaDeSessao.OpenSession();
            }
            return _sessao;
        }

        public void DesfazerTransicao()
        {
            this._sessao.Transaction.Rollback();
        }

        public void Dispose()
        {
            this.Fechar();
            GC.SuppressFinalize(this);
        }

        public void Fechar()
        {
            if (this._sessao != null)
            {
                if (this._sessao.IsOpen)
                    this._sessao.Close();

                this._sessao.Dispose();
                this._sessao = null;
            }
        }

        public void FecharTransicao()
        {
            this._sessao.Transaction.Commit();
        }

        public bool HaSessao()
        {
            return !object.Equals(this._sessao, null);
        }

        public bool HaTransicao()
        {
            return this._sessao.Transaction.IsActive;
        }

        public void IniciarTransicao()
        {
            this.Sessao.Transaction.Begin();
        }
    }
}
