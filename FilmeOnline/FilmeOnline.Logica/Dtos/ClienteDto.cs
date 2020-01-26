using System;
using System.Collections.Generic;

namespace FilmeOnline.Logica.Dtos
{
    public class ClienteDto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }   
        public string Status { get; set; }
        public DateTime? DataExpiracaoStatus { get; set; }
        public decimal ValorGasto { get; set; }
        public IList<AluguelDto> Alugueis { get; set; }
    }
}
