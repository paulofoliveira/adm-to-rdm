using FilmeOnline.Logica.Entidades;
using FluentNHibernate.Mapping;

namespace FilmeOnline.Logica.Mapeamentos
{
    public class CustomerMap : ClassMap<Cliente>
    {
        public CustomerMap()
        {
            Id(x => x.Id);

            Map(x => x.Nome);
            Map(x => x.Email);
            Map(x => x.Status).CustomType<int>();
            Map(x => x.DataExpiracaoStatus).Nullable();
            Map(x => x.ValorGasto);

            HasMany(x => x.Alugueis);
        }
    }
}
