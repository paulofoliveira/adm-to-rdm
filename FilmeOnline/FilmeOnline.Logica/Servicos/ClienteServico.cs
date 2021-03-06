using FilmeOnline.Logica.Entidades;
using System;
using System.Linq;

namespace FilmeOnline.Logica.Servicos
{
    public class ClienteServico
    {
        private readonly FilmeServico _filmeServico;

        public ClienteServico(FilmeServico filmeServico)
        {
            _filmeServico = filmeServico;
        }

        private decimal CalcularPreco(ClienteStatus status, DateTime? dataExpiracaoStatus, LicencaTipo licencaTipo)
        {
            decimal valor;
            switch (licencaTipo)
            {
                case LicencaTipo.DoisDias:
                    valor = 4;
                    break;

                case LicencaTipo.Vitalicio:
                    valor = 8;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (status == ClienteStatus.Avancado && (dataExpiracaoStatus == null || dataExpiracaoStatus.Value >= DateTime.UtcNow))
            {
                valor = valor * 0.75m;
            }

            return valor;
        }

        public void AlugarFilme(Cliente cliente, Filme filme)
        {
            DateTime? dataExpiracao = _filmeServico.RecuperarDataExpiracao(filme.Licenca);
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

        public bool PromoverCliente(Cliente cliente)
        {
            // Pelo menos 2 filmes alugados nos �ltimos 30 dias
            if (cliente.Alugueis.Count(x => x.DataExpiracao == null || x.DataExpiracao.Value >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // Pelo menos 100 reais gastos no �ltimo ano.
            if (cliente.Alugueis.Where(x => x.DataAluguel > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Valor) < 100m)
                return false;

            cliente.Status = ClienteStatus.Avancado;
            cliente.DataExpiracaoStatus = DateTime.UtcNow.AddYears(1);

            return true;
        }
    }
}
