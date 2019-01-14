using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace ADC.Portal.Persistencia.Contexto.Convention
{
    public class ColumnConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            if (instance.Property.PropertyType == typeof(string))
                instance.CustomType("AnsiString");
        }
    }
}
