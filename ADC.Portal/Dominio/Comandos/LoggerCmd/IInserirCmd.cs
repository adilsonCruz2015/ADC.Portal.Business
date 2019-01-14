using ADC.Portal.Dominio.Entidades;
using ADC.Portal.Dominio.Interfaces;
using Solucoes.Auxiliares.Interfaces.Validacao;
using System;

namespace ADC.Portal.Dominio.Comandos.LoggerCmd
{
    public interface IInserirCmd : IAutoValidacao
    {
        void Aplicar(ref Logger dados, Guid rastreio, int ordem, DateTime iniciadoEm, IInformacaoDoAmbiente ambiente, MinhaConta usuario);

        void Desfazer(ref Logger dados);
    }
}
