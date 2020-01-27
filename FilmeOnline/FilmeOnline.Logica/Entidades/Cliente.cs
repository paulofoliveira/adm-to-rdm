using System;
using System.Collections.Generic;

namespace FilmeOnline.Logica.Entidades
{
    public class Cliente : Entidade
    {
        public virtual ClienteNome Nome { get; set; }
        public virtual Email Email { get; set; }
        public virtual ClienteStatus Status { get; set; }
        public virtual DateTime? DataExpiracaoStatus { get; set; }
        public virtual decimal ValorGasto { get; set; }
        public virtual IList<Aluguel> Alugueis { get; set; }
    }
}
