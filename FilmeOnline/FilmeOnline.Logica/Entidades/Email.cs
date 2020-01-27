using CSharpFunctionalExtensions;
using System;

namespace FilmeOnline.Logica.Entidades
{
    public class Email : ValueObject<Email>
    {
        public Email(string value)
        {
            Value = value;
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
