using Newtonsoft.Json;

namespace FilmeOnline.Entidades
{
    public class Filme : Entidade
    {
        public virtual string Nome { get; set; }
        public virtual LicencaTipo Licenca { get; set; }
    }
}
