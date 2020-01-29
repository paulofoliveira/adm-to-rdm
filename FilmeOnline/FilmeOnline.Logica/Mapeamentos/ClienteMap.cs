using FilmeOnline.Logica.Entidades;
using FluentNHibernate.Mapping;
using System;

namespace FilmeOnline.Logica.Mapeamentos
{
    public class CustomerMap : ClassMap<Cliente>
    {
        public CustomerMap()
        {
            Id(x => x.Id);

            Map(x => x.Nome).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Email).CustomType<string>().Access.CamelCaseField(Prefix.Underscore);
            //Map(x => x.Status).CustomType<int>();
            //Map(x => x.DataExpiracaoStatus).CustomType<DateTime?>().Access.CamelCaseField(Prefix.Underscore).Nullable();
            Map(x => x.ValorGasto).CustomType<decimal>().Access.CamelCaseField(Prefix.Underscore);

            Component(x => x.Status, y =>
             {
                 y.Map(x => x.Tipo, "Status").CustomType<int>();
                 
                 y.Map(x => x.DataExpiracao, "DataExpiracaoStatus")
                 .CustomType<DateTime?>()
                 .Access.CamelCaseField(Prefix.Underscore);
             });

            HasMany(x => x.Alugueis).Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
