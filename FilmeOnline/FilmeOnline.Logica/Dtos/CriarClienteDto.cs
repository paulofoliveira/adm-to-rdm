using System.ComponentModel.DataAnnotations;

namespace FilmeOnline.Logica.Dtos
{
    public class CriarClienteDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Nome é muito longo")]
        public string Nome { get; set; }

        [Required]
        [RegularExpression(@"^(.+)@(.+)$", ErrorMessage = "Email é inválido")]
        public string Email { get; set; }
    }
}
