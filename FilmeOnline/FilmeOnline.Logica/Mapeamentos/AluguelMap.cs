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
            Map(x => x.FilmeId);
            Map(x => x.ClienteId);

            References(x => x.Filme).LazyLoad(Laziness.False).ReadOnly();
        }
    }
}
