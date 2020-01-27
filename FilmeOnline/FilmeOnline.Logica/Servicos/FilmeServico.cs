using System;
using FilmeOnline.Logica.Entidades;

namespace FilmeOnline.Logica.Servicos
{
    public class FilmeServico
    {
        public DateTime? GetExpirationDate(LicencaTipo licensingModel)
        {
            DateTime? result;

            switch (licensingModel)
            {
                case LicencaTipo.DoisDias:
                    result = DateTime.UtcNow.AddDays(2);
                    break;

                case LicencaTipo.Vitalicio:
                    result = null;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}
