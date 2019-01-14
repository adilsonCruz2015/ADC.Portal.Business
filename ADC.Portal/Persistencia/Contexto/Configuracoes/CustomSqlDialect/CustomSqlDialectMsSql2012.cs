using NHibernate;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;

namespace ADC.Portal.Persistencia.Contexto.Configuracoes.CustomSqlDialect
{
    public class CustomSqlDialectMsSql2012 : MsSql2012Dialect
    {
        public CustomSqlDialectMsSql2012()
        {
            RegisterFunction("CollateLatinGeneral",
              new SQLFunctionTemplate(NHibernateUtil.String,
                  "?1 collate Latin1_general_CI_AI"));

            RegisterFunction("MD5",
              new SQLFunctionTemplate(NHibernateUtil.String,
                  "CONVERT(VARCHAR(32), HASHBYTES('MD5', ?1), 2)"));

            RegisterFunction("Concat",
              new VarArgsSQLFunction(NHibernateUtil.String,
                  "CONCAT(", ",", ")"));
        }
    }
}
