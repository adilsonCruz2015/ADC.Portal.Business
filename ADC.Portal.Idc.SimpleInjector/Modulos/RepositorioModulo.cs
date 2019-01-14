using ADC.Portal.Dominio.Interfaces.Repositorio;
using ADC.Portal.Persistencia.Repositorio;
using SimpleInjector;

namespace ADC.Portal.Idc.SimpleInjector.Modulos
{
    public static class RepositorioModulo
    {
        public static void Carregar(Container recipiente)
        {
            recipiente.Register<ICursoRep, CursoRep>();
        }
    }
}
