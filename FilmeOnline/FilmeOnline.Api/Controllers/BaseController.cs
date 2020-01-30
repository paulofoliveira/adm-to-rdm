using FilmeOnline.Api.Utils;
using FilmeOnline.Logica.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FilmeOnline.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly UnitOfWork _uow;

        public BaseController(UnitOfWork uow)
        {
            _uow = uow;
        }

        protected new IActionResult Ok()
        {
            _uow.Commit();
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            _uow.Commit();
            return base.Ok(Envelope.Ok(result));
        }

        public IActionResult Error(string errorMessage) => BadRequest(errorMessage);
    }
}
