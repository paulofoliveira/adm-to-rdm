using Newtonsoft.Json;

namespace FilmeOnline.Entidades
{
    public class Filme : Entidade
    {
        public virtual string Nome { get; set; }

        [JsonIgnore]
        public virtual LicencaTipo Licenca { get; set; }
    }
}
