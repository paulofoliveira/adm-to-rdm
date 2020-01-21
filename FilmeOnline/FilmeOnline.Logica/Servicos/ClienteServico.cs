using System;
using System.Linq;
using FilmeOnline.Entidades;

namespace FilmeOnline.Servicos
{
    public class ClienteServico
    {
        private readonly FilmeServico _movieService;

        public ClienteServico(FilmeServico movieService)
        {
            _movieService = movieService;
        }

        private decimal CalculatePrice(ClienteStatus status, DateTime? statusExpirationDate, LicencaTipo licensingModel)
        {
            decimal price;
            switch (licensingModel)
            {
                case LicencaTipo.DoisDias:
                    price = 4;
                    break;

                case LicencaTipo.Vitalicio:
                    price = 8;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (status == ClienteStatus.Avancado && (statusExpirationDate == null || statusExpirationDate.Value >= DateTime.UtcNow))
            {
                price = price * 0.75m;
            }

            return price;
        }

        public void PurchaseMovie(Cliente customer, Filme movie)
        {
            DateTime? expirationDate = _movieService.GetExpirationDate(movie.Licenca);
            decimal price = CalculatePrice(customer.Status, customer.DataExpiracaoStatus, movie.Licenca);

            var purchasedMovie = new Aluguel
            {
                FilmeId = movie.Id,
                ClienteId = customer.Id,
                DataExpiracao = expirationDate,
                Valor = price
            };

            customer.Alugueis.Add(purchasedMovie);
            customer.ValorGasto += price;
        }

        public bool PromoteCustomer(Cliente customer)
        {
            // at least 2 active movies during the last 30 days
            if (customer.Alugueis.Count(x => x.DataExpiracao == null || x.DataExpiracao.Value >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // at least 100 dollars spent during the last year
            if (customer.Alugueis.Where(x => x.DataAluguel > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Valor) < 100m)
                return false;

            customer.Status = ClienteStatus.Avancado;
            customer.DataExpiracaoStatus = DateTime.UtcNow.AddYears(1);

            return true;
        }
    }
}
