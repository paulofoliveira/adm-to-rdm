using System.ComponentModel.DataAnnotations;

namespace FilmeOnline.Logica.Dtos
{
    public class AtualizarClienteDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Nome é muito longo")]
        public string Nome { get; set; }
    }
}
