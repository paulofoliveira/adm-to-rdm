using FilmeOnline.Entidades;
using System;
using System.Linq;

namespace FilmeOnline.Servicos
{
    public class ClienteServico
    {
        private readonly FilmeServico _filmeServico;

        public ClienteServico(FilmeServico filmeServico)
        {
            _filmeServico = filmeServico;
        }

        private decimal CalcularPreco(ClienteStatus status, DateTime? statusExpirationDate, LicencaTipo licensingModel)
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

        public void AlugarFilme(Cliente cliente, Filme filme)
        {
            DateTime? dataExpiracao = _filmeServico.GetExpirationDate(filme.Licenca);
            decimal valor = CalcularPreco(cliente.Status, cliente.DataExpiracaoStatus, filme.Licenca);

            var aluguel = new Aluguel
            {
                FilmeId = filme.Id,
                ClienteId = cliente.Id,
                DataExpiracao = dataExpiracao,
                Valor = valor
            };

            cliente.Alugueis.Add(aluguel);
            cliente.ValorGasto += valor;
        }

        public bool PromoverCliente(Cliente customer)
        {
            // Pelo menos 2 filmes alugados nos últimos 30 dias
            if (customer.Alugueis.Count(x => x.DataExpiracao == null || x.DataExpiracao.Value >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // Pelo menos 100 reais gastos no último ano.
            if (customer.Alugueis.Where(x => x.DataAluguel > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Valor) < 100m)
                return false;

            customer.Status = ClienteStatus.Avancado;
            customer.DataExpiracaoStatus = DateTime.UtcNow.AddYears(1);

            return true;
        }
    }
}
