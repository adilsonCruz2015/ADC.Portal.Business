using ADC.Portal.Dominio.Entidades;
using Newtonsoft.Json;
using System;

namespace ADC.Portal.Dominio.ObjetoDeValor
{
    public class LoggerUsuario
    {
        [JsonConstructor]
        protected internal LoggerUsuario() { }

        public LoggerUsuario(Usuario dados)
        {
            this.Id = dados.Id;
            this.Nome = string.Format("{0} {1}", dados.Nome, dados.Sobrenome).Trim();
        }

        public LoggerUsuario(MinhaConta dados)
        {
            this.Id = !object.Equals(dados, null) ? dados.Id : (Guid?)null;
            this.Nome = !object.Equals(dados, null) ? string.Format("{0} {1}", dados.Nome, dados.Sobrenome).Trim() : null;
        }

        [JsonProperty]
        public Guid? Id { get; protected internal set; }

        [JsonProperty]
        public string Nome { get; protected internal set; }
    }
}
