using AutoMapper;
using IMDB.Application.DTOs;
using IMDB.Application.IMDB.Queries.GetMovieByName;
using IMDB.Domain.Entities;
using IMDB.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetRatingByName
{
    public class GetRatingByNameQueryHandler : IRequestHandler<GetRatingByNameQuery, RatingDto>
    {
        private readonly IMapper _mapper;
        private readonly IIMDBRepository _repository;

        public GetRatingByNameQueryHandler(IMapper mapper, IIMDBRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<RatingDto> Handle(GetRatingByNameQuery request, CancellationToken cancellationToken)
        {
            var rating = await _repository.GetRatingById(request.MovieName, request.UserId);
            var ratingDto = _mapper.Map<RatingDto>(rating); 
            return ratingDto;
        }
    }
}
