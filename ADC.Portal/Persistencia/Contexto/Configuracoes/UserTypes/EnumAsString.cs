using NHibernate;
using NHibernate.Engine;
using NHibernate.UserTypes;
using System;
using System.Data;
using System.Data.Common;

namespace ADC.Portal.Persistencia.Contexto.Configuracoes.UserTypes
{
    public class EnumAsString<T> : IUserType where T : struct
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

        // represents conversion on load-from-db operations:
        //public object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        //{
        //    var obj = NHibernateUtil.String.NullSafeGet(rs, names[0]);
        //    if (obj == null)
        //        return null;
        //    T value;
        //    try
        //    {
        //        Enum.TryParse<T>(obj.ToString(), out value);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return value;
        //}

        //// represents conversion on save-to-db operations:
        //public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        //{
        //    if (value == null)
        //    {
        //        ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        var text = Enum.ToObject(typeof(T), value).ToString();
        //        ((IDataParameter)cmd.Parameters[index]).Value = text.ToString();
        //    }
        //}

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public Type ReturnedType
        {
            get { return typeof(T); }
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

        // represents conversion on load-from-db operations:
        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var obj = NHibernateUtil.String.NullSafeGet(rs, names[0], session, owner);
            if (obj == null)
                return null;
            T value;
            try
            {
                Enum.TryParse<T>(obj.ToString(), out value);
            }
            catch (Exception)
            {
                return null;
            }
            return value;
        }

        // represents conversion on save-to-db operations:
        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            if (value == null)
            {
                cmd.Parameters[index].Value = DBNull.Value;
            }
            else
            {
                var text = Enum.ToObject(typeof(T), value).ToString();
                cmd.Parameters[index].Value = text.ToString();
            }
        }
    }
}
