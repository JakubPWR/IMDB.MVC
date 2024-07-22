using AutoMapper;
using IMDB.Application.DTOs;
using IMDB.Domain.Entities;
using IMDB.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetMovieByName
{
    public class GetMovieByNameQueryHandler : IRequestHandler<GetMovieByNameQuery, MovieDto>
    {
        private readonly IMapper _mapper;
        private readonly IIMDBRepository _repository;

        public GetMovieByNameQueryHandler(IMapper mapper, IIMDBRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<MovieDto> Handle(GetMovieByNameQuery request, CancellationToken cancellationToken)
        {
            var movie = await _repository.GetMovieByName(request.MovieName);
            var movieDto = _mapper.Map<MovieDto>(movie);
            return movieDto;
        }
    }
}
