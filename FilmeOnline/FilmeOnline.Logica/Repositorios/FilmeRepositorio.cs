using FilmeOnline.Entidades;
using FilmeOnline.Utils;
using System.Collections.Generic;
using System.Linq;

namespace FilmeOnline.Repositorios
{
    public class FilmeRepositorio : Repositorio<Filme>
    {
        public FilmeRepositorio(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IReadOnlyList<Filme> RecuperarLista()
        {
            return _unitOfWork.Query<Filme>().ToList();
        }
    }
}
