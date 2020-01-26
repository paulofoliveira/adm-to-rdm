using FilmeOnline.Entidades;
using FilmeOnline.Utils;
using System.Collections.Generic;
using System.Linq;

namespace FilmeOnline.Repositorios
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
