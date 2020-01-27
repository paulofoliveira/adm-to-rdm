using System;
using Newtonsoft.Json;

namespace FilmeOnline.Logica.Entidades
{
    public class Aluguel : Entidade
    {
        [JsonIgnore]
        public virtual long FilmeId { get; set; }

        public virtual Filme Filme { get; set; }

        [JsonIgnore]
        public virtual long ClienteId { get; set; }

        public virtual decimal Valor { get; set; }

        public virtual DateTime DataAluguel { get; set; }

        public virtual DateTime? DataExpiracao { get; set; }
    }
}
