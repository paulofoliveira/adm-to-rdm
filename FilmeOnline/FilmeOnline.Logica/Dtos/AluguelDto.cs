using System;

namespace FilmeOnline.Logica.Dtos
{
    public class AluguelDto
    {
        public FilmeDto Filme { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataAluguel { get; set; }
        public DateTime? DataExpiracao { get; set; }
    }
}
