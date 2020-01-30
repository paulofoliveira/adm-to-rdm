using CSharpFunctionalExtensions;
using System;

namespace FilmeOnline.Logica.Entidades
{
    public class ClienteStatus : ValueObject<ClienteStatus>
    {
        public static readonly ClienteStatus Normal = new ClienteStatus(ClienteStatusTipo.Normal, DataExpiracao.Infinito);

        private readonly DateTime? _dataExpiracao;

        private ClienteStatus() { }
        private ClienteStatus(ClienteStatusTipo tipo, DataExpiracao dataExpiracao) : this()
        {
            Tipo = tipo;
            _dataExpiracao = dataExpiracao ?? throw new ArgumentNullException(nameof(dataExpiracao));
        }
        public bool Avancado => Tipo == ClienteStatusTipo.Avancado && !DataExpiracao.Expirou;
        public ClienteStatusTipo Tipo { get; }
        public DataExpiracao DataExpiracao => (DataExpiracao)_dataExpiracao;        
        public virtual decimal ObterDesconto(LicencaTipo licencaTipo) => Avancado ? 0.25m : 0m;
        public ClienteStatus Promover() => new ClienteStatus(ClienteStatusTipo.Avancado, (DataExpiracao)DateTime.UtcNow.AddYears(1));
        protected override bool EqualsCore(ClienteStatus other) => Tipo == other.Tipo && DataExpiracao == other.DataExpiracao;
        protected override int GetHashCodeCore() => Tipo.GetHashCode() ^ DataExpiracao.GetHashCode();
    }

    public enum ClienteStatusTipo
    {
        Normal = 1,
        Avancado = 2
    }
}
