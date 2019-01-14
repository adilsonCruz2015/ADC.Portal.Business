using System.ComponentModel.DataAnnotations;

namespace ADC.Portal.Dominio.ObjetoDeValor
{
    public enum Status
    {
        Inativo,
        Ativo,
        [Display(Name = "Excluído")]
        Excluido,
        Cache
    }
}
