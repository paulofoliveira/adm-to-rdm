using System;
using System.Collections.Generic;

namespace FilmeOnline.Logica.Entidades
{
    public class Cliente : Entidade
    {
        private string _nome;
        private string _email;
        private decimal _valorGasto;
        public virtual ClienteNome Nome
        {
            get => (ClienteNome)_nome;
            set => _nome = value;
        }

        public virtual Email Email
        {
            get => (Email)_email;
            set => _email = value;
        }
        public virtual ClienteStatus Status { get; set; }
        public virtual DateTime? DataExpiracaoStatus { get; set; }
        public virtual Reais ValorGasto
        {
            get => Reais.Of(_valorGasto);
            set => _valorGasto = value;
        }
        public virtual IList<Aluguel> Alugueis { get; set; }
    }
}
