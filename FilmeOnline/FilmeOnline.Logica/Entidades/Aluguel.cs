using System;

namespace FilmeOnline.Logica.Entidades
{
    public class Aluguel : Entidade
    {
        public virtual long FilmeId { get; set; }

        public virtual Filme Filme { get; set; }

        public virtual long ClienteId { get; set; }

        public virtual decimal Valor { get; set; }

        public virtual DateTime DataAluguel { get; set; }

        public virtual DateTime? DataExpiracao { get; set; }
    }
}
