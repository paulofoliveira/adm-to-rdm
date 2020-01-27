using CSharpFunctionalExtensions;
using System;
using System.Text.RegularExpressions;

namespace FilmeOnline.Logica.Entidades
{
    public class Email : ValueObject<Email>
    {
        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Criar(string email)
        {
            email = (email ?? string.Empty).Trim();

            if (email.Length == 0)
                return Result.Failure<Email>("Email não deve estar vazio");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Result.Failure<Email>("Email é inválido");

            return Result.Ok(new Email(email));
        }

        public string Value { get; }

        protected override bool EqualsCore(Email other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }
        protected override int GetHashCodeCore()
        {
            return GetHashCode();
        }
    }
}
