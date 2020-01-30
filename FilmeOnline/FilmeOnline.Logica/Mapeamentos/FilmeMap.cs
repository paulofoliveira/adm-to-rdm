using FilmeOnline.Logica.Entidades;
using FluentNHibernate;
using FluentNHibernate.Mapping;

// Quando o NHibernate for instanciar objetos do tipo Filme, irá analisar a licença onde tipos concretos como DoisDiasFilme e VitalicioFilme serão instanciados.

namespace FilmeOnline.Logica.Mapeamentos
{
    public class FilmeMap : ClassMap<Filme>
    {
        public FilmeMap()
        {
            Id(x => x.Id);

            DiscriminateSubClassesOnColumn("Licenca");

            Map(x => x.Nome);
            Map(Reveal.Member<Filme>("Licenca")).CustomType<int>(); // Define um binding feito com uma propriedade da classe que é/está protegida/privada.
        }
    }

    public class DoisDiasFilmeMap : SubclassMap<DoisDiasFilme>
    {
        public DoisDiasFilmeMap()
        {
            DiscriminatorValue(1);
        }
    }

    public class VitalicioFilmeMap : SubclassMap<VitalicioFilme>
    {
        public VitalicioFilmeMap()
        {
            DiscriminatorValue(2);
        }
    }
}
