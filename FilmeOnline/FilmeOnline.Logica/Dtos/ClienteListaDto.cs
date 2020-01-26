using System;

namespace FilmeOnline.Logica.Dtos
{
    public class ClienteListaDto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime? DataExpiracaoStatus { get; set; }
        public decimal ValorGasto { get; set; }
    }
}
