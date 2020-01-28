using System;
using FilmeOnline.Logica.Entidades;

namespace FilmeOnline.Logica.Servicos
{
    public class FilmeServico
    {
        public DataExpiracao RecuperarDataExpiracao(LicencaTipo licencaTipo)
        {
            DataExpiracao result;

            switch (licencaTipo)
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
    }
}
