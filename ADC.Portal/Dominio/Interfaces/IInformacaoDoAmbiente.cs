

namespace ADC.Portal.Dominio.Interfaces
{
    public interface IInformacaoDoAmbiente
    {
        string ObterIp();

        string ObterUseragent();

        string ObterNome();

        string ObterTipo();
    }
}
