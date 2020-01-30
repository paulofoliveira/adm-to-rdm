using FilmeOnline.Logica.Entidades;
using FluentNHibernate.Mapping;
using System;

namespace FilmeOnline.Logica.Mapeamentos
{
    public class AluguelMap : ClassMap<Aluguel>
    {
        public AluguelMap()
        {
            Id(x => x.Id);

            Map(x => x.Valor).CustomType<decimal>().Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.DataAluguel);
            Map(x => x.DataExpiracao).CustomType<DateTime?>().Access.CamelCaseField(Prefix.Underscore).Nullable();

            References(x => x.Filme);
            References(x => x.Cliente);
        }
    }
}
