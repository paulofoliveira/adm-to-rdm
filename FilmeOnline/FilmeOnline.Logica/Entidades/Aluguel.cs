using System;

namespace FilmeOnline.Logica.Entidades
{
    public class Aluguel : Entidade
    {
        private DateTime? _dataExpiracao;
        private decimal _valor;
        public virtual long FilmeId { get; set; }

        public virtual Filme Filme { get; set; }

        public virtual long ClienteId { get; set; }

        public virtual Reais Valor
        {
            get => Reais.Of(_valor);
            set => _valor = value;
        }

        public virtual DateTime DataAluguel { get; set; }

        public virtual DataExpiracao DataExpiracao
        {
            get => (DataExpiracao)_dataExpiracao;
            set => _dataExpiracao = value;
        }

    }
}
