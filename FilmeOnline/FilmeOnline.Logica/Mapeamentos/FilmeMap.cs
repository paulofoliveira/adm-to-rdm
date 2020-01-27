using FilmeOnline.Logica.Entidades;
using FluentNHibernate.Mapping;

namespace FilmeOnline.Logica.Mapeamentos
{
    public class FilmeMap : ClassMap<Filme>
    {
        public FilmeMap()
        {
            Id(x => x.Id);

            Map(x => x.Nome);
            Map(x => x.Licenca).CustomType<int>();
        }
    }
}
