using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace ADC.Portal.Dominio.Fabricas
{
    public class ContratoLogJson : DefaultContractResolver
    {
        public static string Serializar(object objetoParaSerializar)
        {
            var serializer = new Newtonsoft.Json.JsonSerializer { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ContractResolver = new ContratoLogJson() };
            StringWriter stringWriter = new StringWriter();
            JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
            serializer.Serialize(jsonWriter, objetoParaSerializar);
            string serializedObject = stringWriter.ToString();
            return serializedObject;
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            if (typeof(NHibernate.Proxy.INHibernateProxy).IsAssignableFrom(objectType))
                return base.CreateContract(objectType.BaseType);
            else return base.CreateContract(objectType);
        }
    }
}
