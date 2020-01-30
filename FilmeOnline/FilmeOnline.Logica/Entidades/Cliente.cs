using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

// Os campos devem serem mapeados como "protected" e não private para casos de encapsulamento por conta do mapeamento do ORM.

namespace FilmeOnline.Logica.Entidades
{
    public class Cliente : Entidade
    {
        private string _nome;
        private string _email;
        private decimal _valorGasto;

        private IList<Aluguel> _alugueis;
        protected Cliente() // Por conta do mapeamento do ORM.
        {
            _alugueis = new List<Aluguel>();
        }

        public Cliente(ClienteNome nome, Email email) : this()
        {
            _nome = nome ?? throw new ArgumentNullException(nameof(nome));
            _email = email ?? throw new ArgumentNullException(nameof(email));

            ValorGasto = Reais.Of(0);
            Status = ClienteStatus.Normal;
        }

        public virtual ClienteNome Nome
        {
            get => (ClienteNome)_nome;
            set => _nome = value;
        }

        public virtual Email Email => (Email)_email;

        public virtual ClienteStatus Status { get; protected set; }

        public virtual Reais ValorGasto
        {
            get => Reais.Of(_valorGasto);
            protected set => _valorGasto = value;
        }
        public virtual IReadOnlyList<Aluguel> Alugueis => _alugueis.ToList();
        public virtual void AlugarFilme(Filme filme)
        {
            if (TemFilmeAlugado(filme))
                throw new Exception();

            var dataExpiracao = filme.RecuperarDataExpiracao();
            var valor = filme.CalcularPreco(Status);

            var aluguel = new Aluguel(filme, this, valor, dataExpiracao);

            _alugueis.Add(aluguel);
            ValorGasto += valor;
        }

        public virtual Result PodePromover()
        {
            if (Status.Avancado)
                return Result.Failure("O cliente ja está com status Avançado");

            if (Alugueis.Count(x => x.DataExpiracao == DataExpiracao.Infinito || x.DataExpiracao.Value >= DateTime.UtcNow.AddDays(-30)) < 2)
                return Result.Failure("O cliente tem que ter pelo menos 2 filmes alugados nos últimos 30 dias");

            if (Alugueis.Where(x => x.DataAluguel > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Valor) < 100m)
                return Result.Failure("O cliente tem que ter pelo menos 100 reais gastos no último ano");

            return Result.Ok();
        }

        public virtual void Promover()
        {
            if (PodePromover().IsFailure)
                throw new Exception();

            Status = Status.Promover();
        }

        public virtual bool TemFilmeAlugado(Filme filme) => Alugueis.Any(x => x.Filme == filme && !x.DataExpiracao.Expirou);
    }
}
