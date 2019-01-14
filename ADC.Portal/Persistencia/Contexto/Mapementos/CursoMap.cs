using ADC.Portal.Dominio.Entidades;
using ADC.Portal.Dominio.ObjetoDeValor;
using ADC.Portal.Persistencia.Contexto.Configuracoes.UserTypes;
using FluentNHibernate.Mapping;

namespace ADC.Portal.Persistencia.Contexto.Mapementos
{
    public class CursoMap : ClassMap<Curso>
    {
        public CursoMap()
        {
            Id(x => x.Id)
                .GeneratedBy
                .GuidNative();

            Map(x => x.Nome)
                .Nullable()
                .Length(100);

            Map(x => x.Descricao)
                .Nullable()
                .Length(300);

            Map(x => x.Sigla)
                .Nullable()
                .Length(20);

            Map(x => x.Status)
                .CustomType<EnumAsString<Status>>()
                .CustomSqlType("varchar(255)")
                .Nullable();

            Map(x => x.CriadoEm)
                .Length(23)
                .CustomType<DateTimeAsString>()
                .Nullable();

            Map(x => x.AlteradoEm)
                .Length(23)
                .CustomType<DateTimeAsString>()
                .Nullable();
        }
    }
}
