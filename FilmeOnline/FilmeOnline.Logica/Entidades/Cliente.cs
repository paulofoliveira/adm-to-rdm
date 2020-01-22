using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmeOnline.Entidades
{
    public class Cliente : Entidade
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Nome é muito longo")]
        public virtual string Nome { get; set; }

        [Required]
        [RegularExpression(@"^(.+)@(.+)$", ErrorMessage = "Email é inválido")]
        public virtual string Email { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual ClienteStatus Status { get; set; }

        public virtual DateTime? DataExpiracaoStatus { get; set; }

        public virtual decimal ValorGasto { get; set; }

        public virtual IList<Aluguel> Alugueis { get; set; }
    }
}
