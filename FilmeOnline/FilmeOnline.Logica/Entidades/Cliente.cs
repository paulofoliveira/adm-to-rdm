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

        public virtual Email Email
        {
            get => (Email)_email;
            protected set => _email = value;
        }

        public virtual ClienteStatus Status { get; set; }

        public virtual Reais ValorGasto
        {
            get => Reais.Of(_valorGasto);
            protected set => _valorGasto = value;
        }
        public virtual IReadOnlyList<Aluguel> Alugueis => _alugueis.ToList();
        public virtual void AdicionarFilmeAlugado(Filme filme, DataExpiracao dataExpiracao, Reais valor)
        {
            var aluguel = new Aluguel(filme, this, valor, dataExpiracao);

            _alugueis.Add(aluguel);
            ValorGasto += valor;
        }

        public virtual bool Promover()
        {
            // Pelo menos 2 filmes alugados nos últimos 30 dias
            if (Alugueis.Count(x => x.DataExpiracao == DataExpiracao.Infinito || x.DataExpiracao.Value >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // Pelo menos 100 reais gastos no último ano.
            if (Alugueis.Where(x => x.DataAluguel > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Valor) < 100m)
                return false;

            Status = Status.Promover();

            return true;
        }
    }
}
