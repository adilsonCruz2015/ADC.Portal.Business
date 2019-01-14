using ADC.Portal.Dominio.Comandos.LoggerCmd;
using ADC.Portal.Dominio.Entidades;
using Solucoes.Auxiliares.Interfaces.Validacao;
using System;
using System.Collections.Generic;

namespace ADC.Portal.Dominio.Interfaces.Servico
{
    public interface ILoggerServico
        : IDisposable, IAutoValidacao
    {
        Guid ObterRastreio();

        Logger Inserir(IInserirCmd comando);

        IEnumerable<Logger> Filtrar(FiltrarCmd comando);

        Logger Obter(ObterCmd comando);
    }
}
