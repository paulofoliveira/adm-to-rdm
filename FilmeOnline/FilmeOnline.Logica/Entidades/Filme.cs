using System;

namespace FilmeOnline.Logica.Entidades
{
    public abstract class Filme : Entidade
    {
        public virtual string Nome { get; protected set; }
        protected virtual LicencaTipo Licenca { get;  set; }

        public abstract DataExpiracao RecuperarDataExpiracao();
        public virtual Reais CalcularPreco(ClienteStatus status)
        {
            var modificador = 1 - status.ObterDesconto(Licenca);
            var precoBase = ObterPrecoBase();

            return precoBase * modificador;
        }

        public abstract Reais ObterPrecoBase();
    }

    public class DoisDiasFilme : Filme
    {
        public override Reais ObterPrecoBase() => Reais.Of(4);
        public override DataExpiracao RecuperarDataExpiracao() => (DataExpiracao)DateTime.UtcNow.AddDays(2);
    }

    public class VitalicioFilme : Filme
    {
        public override Reais ObterPrecoBase() => Reais.Of(8);
        public override DataExpiracao RecuperarDataExpiracao() => DataExpiracao.Infinito;
    }
}
