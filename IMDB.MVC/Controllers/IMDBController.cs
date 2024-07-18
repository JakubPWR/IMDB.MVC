using IMDB.Application.IMDB.Queries;
using IMDB.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.MVC.Controllers
{
    public class IMDBController : Controller
    {
        private readonly IMediator _mediatR;

        public IMDBController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }
        public IActionResult Index()
        {
            var moviesDto = _mediatR.Send(new GetAllMoviesQuery());
            return View(moviesDto);
        }
    }
}
