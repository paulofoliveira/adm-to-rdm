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
    }
}
