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
            var aluguel = new Aluguel
            {
                FilmeId = filme.Id,
                ClienteId = Id,
                DataExpiracao = dataExpiracao,
                Valor = valor,
                DataAluguel = DateTime.UtcNow
            };

            _alugueis.Add(aluguel);
            ValorGasto += valor;
        }
    }
}
