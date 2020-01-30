using Newtonsoft.Json;
using System;

namespace FilmeOnline.Logica.Entidades
{
    public class Filme : Entidade
    {
        public virtual string Nome { get; protected set; }
        public virtual LicencaTipo Licenca { get; protected set; }

        public DataExpiracao RecuperarDataExpiracao()
        {
            DataExpiracao result;

            switch (Licenca)
            {
                case LicencaTipo.DoisDias:
                    result = (DataExpiracao)DateTime.UtcNow.AddDays(2);
                    break;

                case LicencaTipo.Vitalicio:
                    result = DataExpiracao.Infinito;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public virtual Reais CalcularPreco(ClienteStatus status)
        {
            var modificador = 1 - status.ObterDesconto(Licenca);

            switch (Licenca)
            {
                case LicencaTipo.DoisDias:
                    return Reais.Of(4) * modificador;

                case LicencaTipo.Vitalicio:
                    return Reais.Of(8) * modificador;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
