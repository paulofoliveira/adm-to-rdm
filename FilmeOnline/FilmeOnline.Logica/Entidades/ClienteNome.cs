using CSharpFunctionalExtensions;
using System;

namespace FilmeOnline.Logica.Entidades
{
    public class ClienteNome : ValueObject<ClienteNome>
    {
        public ClienteNome(string value)
        {
            Value = value;
        }

        public string Value { get;}

        protected override bool EqualsCore(ClienteNome other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }
        protected override int GetHashCodeCore()
        {
            return GetHashCode();
        }
    }
}
