using ADC.Portal.Dominio.ObjetoDeValor;

namespace ADC.Portal.Dominio.Interfaces
{
    public interface IUsuario : INivelAcesso
    {
        bool EhAutorizadoSobre(IUsuario comparar);

        bool EhAutorizadoSobre(INivelAcesso comparar);

        bool EhAutorizadoSobre(NivelAcesso comparar);

        bool EhAutorizadoComo(NivelAcesso comparar);
    }
}
