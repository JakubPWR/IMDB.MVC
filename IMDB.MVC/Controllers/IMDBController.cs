using AutoMapper;
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
    }
}
