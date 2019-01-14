using ADC.Portal.Dominio.Interfaces;
using NHibernate;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace ADC.Portal.Persistencia.Contexto.Interceptor
{
    class EntityInterceptor : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Trace.WriteLine(sql.ToString());
            return sql;
        }

        public override bool OnSave(object entity, object id, object[] state,
            string[] propertyNames, NHibernate.Type.IType[] types)
        {
            if (!(entity is ICriadoEm) && !(entity is IAlteradoEm)) return false;

            DateTime created = DateTime.Now;
            if ((entity is ICriadoEm))
            {
                ICriadoEm value = (ICriadoEm)entity;
                SetState(value, x => x.CriadoEm, propertyNames, state, created);
            }
            if ((entity is IAlteradoEm))
            {
                IAlteradoEm value = (IAlteradoEm)entity;
                SetState(value, x => x.AlteradoEm, propertyNames, state, created);
            }

            return true;
        }

        public override bool OnFlushDirty(object entity, object id, object[] currentState,
             object[] previousState, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            if (!(entity is ICriadoEm) && !(entity is IAlteradoEm)) return false;

            DateTime created = DateTime.Now;
            if ((entity is ICriadoEm))
            {
                ICriadoEm value = (ICriadoEm)entity;
                if (object.Equals(value.CriadoEm, null) || object.Equals(value.CriadoEm, new DateTime()))
                    SetState(value, x => x.CriadoEm, propertyNames, currentState, created);
            }
            if ((entity is IAlteradoEm))
            {
                IAlteradoEm value = (IAlteradoEm)entity;
                SetState(value, x => x.AlteradoEm, propertyNames, currentState, created);
            }

            return true;
        }

        private void SetState<TValue, TProperty>(TValue reference, Expression<Func<TValue, TProperty>> property, string[] propertyNames, object[] state, object value)
        {
            MemberExpression member = property.Body as MemberExpression;
            PropertyInfo propInfo = member.Member as PropertyInfo;

            var index = Array.IndexOf(propertyNames, propInfo.Name);
            if (index == -1) return;
            state[index] = value;
        }
    }
}
