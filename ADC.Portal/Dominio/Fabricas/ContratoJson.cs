using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Solucoes.Auxiliares.Interfaces.Validacao;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ADC.Portal.Dominio.Fabricas
{
    public class ContratoJson : DefaultContractResolver
    {
        public static string Serializar(object objetoParaSerializar)
        {
            var serializer = new JsonSerializer { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ContractResolver = new ContratoJson() };
            StringWriter stringWriter = new StringWriter();
            JsonWriter jsonWriter = new JsonTextWriter(stringWriter);
            serializer.Serialize(jsonWriter, objetoParaSerializar);
            string serializedObject = stringWriter.ToString();
            return serializedObject;
        }

        public ContratoJson()
            : base()
        {
            // Aplicar o comportamento de CamelCasePropertyNamesContractResolver
            NamingStrategy = new CamelCaseNamingStrategy
            {
                ProcessDictionaryKeys = true,
                OverrideSpecifiedNames = true
            };
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            var actualProperties = type.GetProperties();

            foreach (var jsonProperty in properties)
            {
                var property = actualProperties.FirstOrDefault(x => x.Name.ToLower() == jsonProperty.PropertyName.ToLower());
                if (property != null && (property.GetCustomAttribute(typeof(JsonIgnoreAttribute)) != null
                    || property.PropertyType == typeof(INotificarValidacao)))
                {
                    jsonProperty.Ignored = true;
                }
            }
            return properties;

        }

        protected override JsonContract CreateContract(Type objectType)
        {
            if (typeof(NHibernate.Proxy.INHibernateProxy).IsAssignableFrom(objectType))
                return base.CreateContract(objectType.BaseType);
            else return base.CreateContract(objectType);
        }
    }
}
