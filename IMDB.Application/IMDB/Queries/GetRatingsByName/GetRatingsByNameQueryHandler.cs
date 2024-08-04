using AutoMapper;
using IMDB.Application.ApplicationUser;
using IMDB.Application.DTOs;
using IMDB.Domain.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetRatingsByName
{
    public class GetRatingsByNameQueryHandler : IRequestHandler<GetRatingsByNameQuery, IEnumerable<RatingDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IIMDBRepository _repository;

        public GetRatingsByNameQueryHandler(IMapper mapper, IUserContext userContext, IIMDBRepository repository)
        {
            _mapper = mapper;
            _userContext = userContext;
            _repository = repository;
        }
        public async Task<IEnumerable<RatingDto>> Handle(GetRatingsByNameQuery request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var movie = await _repository.GetMovieByName(request.RatingMovieName);
            var ratings = movie.Ratings;
            if (!ratings.Any())
            {
                return Enumerable.Empty<RatingDto>();
            }
            var ratingsDtos = _mapper.Map<IEnumerable<RatingDto>>(ratings);
            if (user != null)
            {
                foreach (var i in ratingsDtos)
                {
                    if (!user.Id.IsNullOrEmpty())
                    {
                        if (user.Id == i.UserId || user.IsInRole("Admin"))
                        {
                            i.IsEditable = true;
                        }
                    }
                }
            }
            return ratingsDtos;

        }
    }
}
