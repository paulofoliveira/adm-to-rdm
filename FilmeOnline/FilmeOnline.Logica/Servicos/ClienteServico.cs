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

        private Reais CalcularPreco(ClienteStatus status, LicencaTipo licencaTipo)
        {
            Reais reais;

            switch (licencaTipo)
            {
                case LicencaTipo.DoisDias:
                    reais = Reais.Of(4);
                    break;

                case LicencaTipo.Vitalicio:
                    reais = Reais.Of(8);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (status.Avancado)
            {
                reais *= 0.75m;
            }

            return reais;
        }

        public void AlugarFilme(Cliente cliente, Filme filme)
        {
            DataExpiracao dataExpiracao = _filmeServico.RecuperarDataExpiracao(filme.Licenca);
            var valor = CalcularPreco(cliente.Status, filme.Licenca);

            //cliente.Alugueis.Add(aluguel);
            cliente.AdicionarFilmeAlugado(filme, dataExpiracao, valor);
        }

        public bool PromoverCliente(Cliente cliente)
        {
            // Pelo menos 2 filmes alugados nos �ltimos 30 dias
            if (cliente.Alugueis.Count(x => x.DataExpiracao == DataExpiracao.Infinito || x.DataExpiracao.Value >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // Pelo menos 100 reais gastos no �ltimo ano.
            if (cliente.Alugueis.Where(x => x.DataAluguel > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Valor) < 100m)
                return false;

            cliente.Status = cliente.Status.Promover();

            return true;
        }
    }
}
