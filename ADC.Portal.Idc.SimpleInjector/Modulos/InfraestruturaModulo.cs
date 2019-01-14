using ADC.Portal.Dominio.Interfaces;
using ADC.Portal.Persistencia.Contexto;
using ADC.Portal.Persistencia.Contexto.Interfaces;
using SimpleInjector;
using Solucoes.Auxiliares.Interfaces.Validacao;
using Solucoes.Auxiliares.Validacao;

namespace ADC.Portal.Idc.SimpleInjector.Modulos
{
    public class InfraestruturaModulo
    {
        public static void Carregar(Container recipiente)
        {
            recipiente.Register<INotificarValidacao, NotificarValidacao>(Lifestyle.Scoped);
            recipiente.Register<IConexao, Conexao>(Lifestyle.Scoped);
            recipiente.Register<IUnidadeTrabalho, UnidadeTrabalho>(Lifestyle.Scoped);
        }
    }
}
