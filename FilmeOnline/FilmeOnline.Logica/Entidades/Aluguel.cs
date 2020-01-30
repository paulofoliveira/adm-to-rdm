using System;

namespace FilmeOnline.Logica.Entidades
{
    public class Aluguel : Entidade
    {
        private DateTime? _dataExpiracao;
        private decimal _valor;

        protected Aluguel() { }

        // A ideia da entidade Aluguel estar com construtor interno é porque ele é uma entidade que só deve ser criada pelo Aggregate Root (no caso o Cliente). 
        // Lê-se que ela faz parte do cliente.
        internal Aluguel(Filme filme, Cliente cliente, Reais valor, DataExpiracao dataExpiracao)
        {
            if (valor == null || valor.Zero)
                throw new ArgumentException(nameof(valor));

            if (dataExpiracao == null || dataExpiracao.Expirou)
                throw new ArgumentException(nameof(dataExpiracao));

            Filme = filme ?? throw new ArgumentNullException(nameof(filme));
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            Valor = valor;
            DataExpiracao = dataExpiracao;
            DataAluguel = DateTime.UtcNow;
        }

        public virtual Filme Filme { get; protected set; }
        public virtual Cliente Cliente { get; protected set; }
        public virtual Reais Valor
        {
            get => Reais.Of(_valor);
            protected set => _valor = value;
        }

        public virtual DateTime DataAluguel { get; protected set; }

        public virtual DataExpiracao DataExpiracao
        {
            get => (DataExpiracao)_dataExpiracao;
            protected set => _dataExpiracao = value;
        }

    }
}
