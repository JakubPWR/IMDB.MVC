using AutoMapper;
using IMDB.Application.ApplicationUser;
using IMDB.Application.DTOs;
using IMDB.Domain.Entities;
using IMDB.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.AddRating
{
    public class AddRatingCommandHandler : IRequestHandler<AddRatingCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IIMDBRepository _repository;

        public AddRatingCommandHandler(IMapper mapper, IUserContext userContext, IIMDBRepository repository)
        {
            _mapper = mapper;
            _userContext = userContext;
            _repository = repository;
        }

        public async Task<Unit> Handle(AddRatingCommand request, CancellationToken cancellationToken)
        {
            var movie = await _repository.GetMovieByName(request.RMovieName);
            var rating = _mapper.Map<Rating>(request);
            rating.Movie = movie;
            rating.MovieId = movie.Id;
            await _repository.AddRating(rating);
            movie.AddRating(rating);
            await _repository.CalculateRating(request.RMovieName);
            await _repository.Commit();
            
            return Unit.Value;
        }
    }
}
