using CSharpFunctionalExtensions;
using System;

namespace FilmeOnline.Logica.Entidades
{
    public class ClienteNome : ValueObject<ClienteNome>
    {
        private ClienteNome(string value)
        {
            Value = value;
        }

        public static Result<ClienteNome> Criar(string nome)
        {
            nome = (nome ?? string.Empty).Trim();

            if (nome.Length == 0)
                return Result.Failure<ClienteNome>("Nome do cliente não deve estar vazio");

            if (nome.Length > 100)
                return Result.Failure<ClienteNome>("Nome do cliente é muito longo");

            return Result.Ok(new ClienteNome(nome));
        }

        public string Value { get; }

        protected override bool EqualsCore(ClienteNome other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }
        protected override int GetHashCodeCore()
        {
            return  Value.GetHashCode();
        }

        // Conversões implicitas (de objeto de valor para string) e explicitas (de string para objeto de valor).

        /* É a famosa lógica do: Toda string é um objeto mas nem todo objeto é uma string. Se eu quero uma string de um objeto.
            var x = (string)obj; // Pode dar erro por conta da afirmação acima. */

        public static implicit operator string(ClienteNome nome) => nome.Value;
        public static explicit operator ClienteNome(string nome) => Criar(nome).Value;
    }
}
