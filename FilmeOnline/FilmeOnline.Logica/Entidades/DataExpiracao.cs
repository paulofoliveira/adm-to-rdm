using CSharpFunctionalExtensions;
using System;

namespace FilmeOnline.Logica.Entidades
{
    public class DataExpiracao : ValueObject<DataExpiracao>
    {
        public static readonly DataExpiracao Infinito = new DataExpiracao(null);
        private DataExpiracao(DateTime? data)
        {
            Value = data;
        }

        public DateTime? Value { get; }
        public bool Expirou => this != Infinito && Value < DateTime.UtcNow;
        public static Result<DataExpiracao> Criar(DateTime data)
        {
            //if (data < DateTime.UtcNow)
            //    return Result.Failure<DataExpiracao>("Data de expiração não pode ser no passado");

            return Result.Ok(new DataExpiracao(data));
        }

        protected override bool EqualsCore(DataExpiracao other) => Value == other.Value;
        protected override int GetHashCodeCore() => Value.GetHashCode();

        public static explicit operator DataExpiracao(DateTime? data)
        {
            if (data.HasValue)
                return Criar(data.Value).Value;

            return Infinito;
        }

        public static implicit operator DateTime?(DataExpiracao data) => data.Value;
    }
}
