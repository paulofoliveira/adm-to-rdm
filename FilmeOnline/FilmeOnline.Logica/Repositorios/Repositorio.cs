using FilmeOnline.Entidades;
using FilmeOnline.Utils;

namespace FilmeOnline.Repositorios
{
    public abstract class Repositorio<T>
        where T : Entidade
    {
        protected readonly UnitOfWork _unitOfWork;

        protected Repositorio(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T RecuperarPorId(long id)
        {
            return _unitOfWork.Get<T>(id);
        }

        public void Adicionar(T entity)
        {
            _unitOfWork.SaveOrUpdate(entity);
        }

        public void Commitar()
        {
            _unitOfWork.Commit();
        }
    }
}
