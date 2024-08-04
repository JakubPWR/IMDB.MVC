using AutoMapper;
using Azure.Core;
using IMDB.Application.DTOs;
using IMDB.Application.IMDB.Commands.AddRating;
using IMDB.Application.IMDB.Commands.CreateActor;
using IMDB.Application.IMDB.Commands.CreateMovie;
using IMDB.Application.IMDB.Commands.DeleteMovie;
using IMDB.Application.IMDB.Commands.DeleteRating;
using IMDB.Application.IMDB.Commands.Edit;
using IMDB.Application.IMDB.Commands.EditRating;
using IMDB.Application.IMDB.Queries;
using IMDB.Application.IMDB.Queries.GetAllActors;
using IMDB.Application.IMDB.Queries.GetAllMovies;
using IMDB.Application.IMDB.Queries.GetMovieByName;
using IMDB.Application.IMDB.Queries.GetMovieByNameList;
using IMDB.Application.IMDB.Queries.GetRatingByName;
using IMDB.Application.IMDB.Queries.GetRatingsByName;
using IMDB.Domain.Entities;
using IMDB.Domain.Interfaces;
using IMDB.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IMDB.MVC.Controllers
{
    public class IMDBController : Controller
    {
        private readonly IMediator _mediatR;
        private IMapper _mapper;
        private readonly IIMDBRepository _repository;

        public IMDBController(IMediator mediatR, IMapper mapper, IIMDBRepository repository)
        {
            _mediatR = mediatR;
            _mapper = mapper;
            _repository = repository;
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
            TempData["message"] = $"Movie {command.MovieName} created succesfully";
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
        [Route("IMDB/{MovieName}/Details")]
        public async Task<IActionResult> Details(string MovieName)
        {
            var movie = await _mediatR.Send(new GetMovieByNameQuery(MovieName)); 
            return View(movie);
        }
        [Authorize]
        [Route("IMDB/{MovieName}/AddRating")]
        public async Task<IActionResult> AddRating(string MovieName)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var movieDto = await _mediatR.Send(new GetMovieByNameQuery(MovieName));
            var movie = await _repository.GetMovieByName(movieDto.MovieName);
            AddRatingCommand rating = new AddRatingCommand{ RMovieName = movieDto.MovieName};
            var ExistingRating = movie.Ratings.FirstOrDefault(r => r.UserId == userId);
            if (ExistingRating != null)
            {
                string decodedRMovieName = Uri.UnescapeDataString(rating.RMovieName);

                // Use the decoded movie name for redirection
                return RedirectToAction("ShowUsersRating", "IMDB", new { MovieName = decodedRMovieName, UserId = userId });
            }
            return View(rating);
        }
        [Authorize]
        [Route("IMDB/{MovieName}/AddRating")]
        [HttpPost]
        public async Task<IActionResult> AddRating(AddRatingCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediatR.Send(command);

            this.SetNotification("success", $"Added rating to movie {command.RMovieName}");
            string decodedRMovieName = Uri.UnescapeDataString(command.RMovieName);

            // Use the decoded movie name for redirection
            return RedirectToAction("Details", "IMDB", new { MovieName = decodedRMovieName });
        }
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(string SearchedName)
        {
            var movies = await _mediatR.Send(new GetMovieByNameListQuery { SearchedName = SearchedName });
            if (!movies.Any())
            {
                ViewBag.Message = "No movies found.";
                return View();
            }
            return View("SearchResults", movies);
        }
        [Route("IMDB/{MovieName}/Details/Ratings")]
        public async Task<IActionResult> ViewRatings(string MovieName)
        {
            var movie = await _mediatR.Send(new GetRatingsByNameQuery { RatingMovieName = MovieName});
            return View(movie);
        }

        [Route("IMDB/{MovieName}/{UserId}/DeleteRating")]
        public async Task<IActionResult> DeleteRating(string MovieName, string UserId)
        {
            var rating = await _mediatR.Send(new GetRatingByNameQuery {MovieName = MovieName, UserId = UserId});
            DeleteRatingCommand model = _mapper.Map<DeleteRatingCommand>(rating);
            return View(model);
        }
        [HttpPost]
        [Route("IMDB/{MovieName}/{UserId}/DeleteRating")]
        public async Task<IActionResult> DeleteRating(DeleteRatingCommand command)
        {
            await _mediatR.Send(command);
            string decodedMovieName = Uri.UnescapeDataString(command.MovieName);

            // Use the decoded movie name for redirection
            return RedirectToAction("Details", "IMDB", new { MovieName = decodedMovieName });
        }
        [Route("IMDB/{MovieName}/{UserId}/EditRating")]
        public async Task<IActionResult> EditRating(string MovieName, string UserId)
        {
            var rating = await _mediatR.Send(new GetRatingByNameQuery { MovieName = MovieName, UserId = UserId });
            EditRatingCommand model = _mapper.Map<EditRatingCommand>(rating);
            model.EncodedNameEditRating = rating.Movie.MovieName;
            return View(model);
        }
        [Route("IMDB/{MovieName}/{UserId}/EditRating")]
        [HttpPost]
        public async Task<IActionResult> EditRating(EditRatingCommand command)
        {
            if(!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediatR.Send(command);
            this.SetNotification("success", $"Edited rating");
            string decodedMovieName = Uri.UnescapeDataString(command.EncodedNameEditRating);
            // Use the decoded movie name for redirection
            return RedirectToAction("Details", "IMDB", new { MovieName = decodedMovieName });


        }
        [Route("IMDB/{MovieName}/{UserId}/UsersRating")]
        public async Task<IActionResult> ShowUsersRating(string MovieName, string UserId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rating = await _mediatR.Send(new GetRatingByNameQuery { MovieName= MovieName,UserId = UserId });
            if(User.IsInRole("Admin") || rating.UserId == userId)
            {
                rating.IsEditable = true;
            }
            return View(rating);
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult CreateActor()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = ("Admin"))]
        [Route("IMDB/CreateActor")]
        public async Task<IActionResult> CreateActor(CreateActorCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediatR.Send(command);
            TempData["message"] = $"Movie {command.ActorName} created succesfully";
            this.SetNotification("success", $"Created movie {command.ActorName}");
            return RedirectToAction("Actors"); //TODO refactor
        }

        [Route("IMDB/Actors")]
        public async Task<IActionResult> Actors()
        {
            var actors = await _mediatR.Send(new GetAllActorsQuery());
            return View(actors);
        }
    }
}
