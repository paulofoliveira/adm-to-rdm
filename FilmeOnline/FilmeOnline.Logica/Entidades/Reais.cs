using CSharpFunctionalExtensions;

namespace FilmeOnline.Logica.Entidades
{
    public class Reais : ValueObject<Reais>
    {
        private const decimal MaxQuantidadeEmReais = 1_000_000;
        public Reais(decimal quantidadeEmReais)
        {
            Value = quantidadeEmReais;
        }

        public static Result<Reais> Criar(decimal quantidadeEmReais)
        {
            if (quantidadeEmReais < 0)
                return Result.Failure<Reais>("Quantidade em reais não pode ser negativo");

            if (quantidadeEmReais > MaxQuantidadeEmReais)
                return Result.Failure<Reais>("Quantidade em reais excede a quantidade máxima permitida");

            if (quantidadeEmReais % 0.01m > 0)
                return Result.Failure<Reais>("Quantidade em reais não contem parte de centavos");

            return Result.Ok(new Reais(quantidadeEmReais));
        }

        public decimal Value { get; }
        protected override bool EqualsCore(Reais other)
        {
            return Value == other.Value;
        }
        protected override int GetHashCodeCore() => Value.GetHashCode();

        public static implicit operator decimal(Reais reais) => reais.Value;
        //public static explicit operator Reais(decimal quantidadeEmReais) => Criar(quantidadeEmReais).Value;

        public static Reais Of(decimal quantidadeEmReais) => Criar(quantidadeEmReais).Value;

        public static Reais operator +(Reais reais1, Reais reais2) => Criar(reais1.Value + reais2.Value).Value;
        public static Reais operator *(Reais reais, decimal multiplicador) => Criar(reais.Value * multiplicador).Value;
    }
}
