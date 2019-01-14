

namespace ADC.Portal.Dominio.Interfaces
{
    public interface IAplicar<T> where T : class
    {
        void Aplicar(ref T valor);

        void Desfazer(ref T valor);
    }
}
