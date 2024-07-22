using AutoMapper;
using IMDB.Application.DTOs;
using IMDB.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetAllMovies
{
    public class GetAllMovieQueryHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IIMDBRepository _repository;
        private readonly IMapper _mapper;

        public GetAllMovieQueryHandler(IIMDBRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MovieDto>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _repository.GetAllMovies();
            var moviesDto = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return moviesDto;

        }
    }
}
