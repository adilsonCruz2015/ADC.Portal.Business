

using NHibernate;
using NHibernate.Engine;
using NHibernate.UserTypes;
using System;
using System.Data;
using System.Data.Common;

namespace ADC.Portal.Persistencia.Contexto.Configuracoes.UserTypes
{
    public class DateTimeAsString : IUserType
    {
        #region IUserType Members

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public int GetHashCode(object x)
        {
            if (x == null)
                return 0;
            return x.GetHashCode();
        }
        public bool IsMutable
        {
            get { return false; }
        }

        // Conversão do banco para o c#
        //public object NullSafeGet(System.Data.IDataReader rs, string[] names,  object owner)
        //{
        //    var obj = NHibernateUtil.String.NullSafeGet(rs, names[0]);
        //    if (obj == null)
        //        return null;

        //    DateTime value;
        //    try
        //    {
        //        value = DateTime.ParseExact(obj.ToString(), "yyyy-MM-dd HH:mm:ss.fff", null);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return value;
        //}

        // Conversão do c# para o banco

        //public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        //{
        //    try
        //    {
        //        if (!object.Equals(value, null))
        //            ((IDataParameter)cmd.Parameters[index]).Value = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff");
        //        else
        //            ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
        //    }
        //    catch
        //    {
        //        ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
        //    }
        //}
        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public Type ReturnedType
        {
            get { return typeof(DateTime); }
        }

        public NHibernate.SqlTypes.SqlType[] SqlTypes
        {
            get { return new[] { NHibernateUtil.AnsiString.SqlType }; }
        }
        #endregion

        bool IUserType.Equals(object x, object y)
        {
            return object.Equals(x, y);
        }

        // Conversão do banco para o c#
        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var obj = NHibernateUtil.String.NullSafeGet(rs, names[0], session, owner);
            if (obj == null)
                return null;

            DateTime value;
            try
            {
                value = DateTime.ParseExact(obj.ToString(), "yyyy-MM-dd HH:mm:ss.fff", null);
            }
            catch (Exception)
            {
                return null;
            }
            return value;
        }

        // Conversão do c# para o banco
        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            try
            {
                if (!object.Equals(value, null))
                    cmd.Parameters[index].Value = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff");
                else
                    cmd.Parameters[index].Value = DBNull.Value;
            }
            catch
            {
                cmd.Parameters[index].Value = DBNull.Value;
            }
        }
    }
}
