using FilmeOnline.Logica.Entidades;
using FilmeOnline.Logica.Utils;
using System.Collections.Generic;
using System.Linq;

namespace FilmeOnline.Logica.Repositorios
{
    public class ClienteRepositorio : Repositorio<Cliente>
    {
        public ClienteRepositorio(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IReadOnlyList<Cliente> RecuperarLista()
        {
            return _unitOfWork
                .Query<Cliente>()
                .ToList()
                .Select(x =>
                {
                    x.Alugueis = null;
                    return x;
                })
                .ToList();
        }

        public Cliente RecuperarPorEmail(string email)
        {
            return _unitOfWork
                .Query<Cliente>()
                .SingleOrDefault(x => x.Email == email);
        }
    }
}
