using FilmeOnline.Entidades;
using FluentNHibernate.Mapping;

namespace FilmeOnline.Mapeamentos
{
    public class AluguelMap : ClassMap<Aluguel>
    {
        public AluguelMap()
        {
            Id(x => x.Id);

            Map(x => x.Valor);
            Map(x => x.DataAluguel);
            Map(x => x.DataExpiracao).Nullable();
            Map(x => x.FilmeId);
            Map(x => x.ClienteId);

            References(x => x.Filme).LazyLoad(Laziness.False).ReadOnly();
        }
    }
}
