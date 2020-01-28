using FilmeOnline.Logica.Entidades;
using FluentNHibernate.Mapping;

namespace FilmeOnline.Logica.Mapeamentos
{
    public class CustomerMap : ClassMap<Cliente>
    {
        public CustomerMap()
        {
            Id(x => x.Id);

            Map(x => x.Nome).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Email).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Status).CustomType<int>();
            Map(x => x.DataExpiracaoStatus).Nullable();
            Map(x => x.ValorGasto).CustomType<decimal>().Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.Alugueis);
        }
    }
}
