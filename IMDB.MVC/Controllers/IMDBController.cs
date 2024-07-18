using AutoMapper;
using IMDB.Application.IMDB.Commands;
using IMDB.Application.IMDB.Queries;
using IMDB.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.MVC.Controllers
{
    public class IMDBController : Controller
    {
        private readonly IMediator _mediatR;
        private IMapper _mapper;

        public IMDBController(IMediator mediatR, IMapper mapper)
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var moviesDto = await _mediatR.Send(new GetAllMoviesQuery());
            return View(moviesDto);
        }
        [Route("IMDB/{MovieName}/Delete")]
        public async Task<IActionResult> Delete(string MovieName)
        {
            var movie = await _mediatR.Send(new GetMovieByNameQuery(MovieName));
            DeleteMovieCommand model = _mapper.Map<DeleteMovieCommand>(movie);
            return View(model);
        }
        [HttpPost]
        [Route("IMDB/{MovieName}/Delete")]
        public async Task<IActionResult> Delete(DeleteMovieCommand command, string name)
        {
            await _mediatR.Send(command);
            return RedirectToAction("Index");
        }
    }
}
