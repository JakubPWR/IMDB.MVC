using AutoMapper;
using IMDB.Application.IMDB.Commands.CreateMovie;
using IMDB.Application.IMDB.Commands.DeleteMovie;
using IMDB.Application.IMDB.Commands.Edit;
using IMDB.Application.IMDB.Queries;
using IMDB.Application.IMDB.Queries.GetAllMovies;
using IMDB.Application.IMDB.Queries.GetMovieByName;
using IMDB.Domain.Interfaces;
using IMDB.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles =("Admin"))]
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
        [Authorize(Roles = ("Admin"))]
        public IActionResult Create()
        {
            return View();
        }
        [Route("IMDB/Create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediatR.Send(command);

            this.SetNotification("success", $"Created movie {command.MovieName}");
            return RedirectToAction("Index"); //TODO refactor
        }
        [Authorize(Roles = ("Admin"))]
        [Route("IMDB/{MovieName}/Edit")]
        public async Task<IActionResult> Edit(string MovieName)
        {
            var movie = await _mediatR.Send(new GetMovieByNameQuery(MovieName));
            if (!movie.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }
            EditMovieCommand model = _mapper.Map<EditMovieCommand>(movie);
            model.EncodedNameEdit = movie.EncodedName;
            return View(model);
        }
        [Route("IMDB/{MovieName}/Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditMovieCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediatR.Send(command);

            this.SetNotification("success", $"Edited movie {command.MovieName}");
            return RedirectToAction("Index"); //TODO refactor
        }
    }
}
