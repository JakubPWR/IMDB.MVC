using AutoMapper;
using IMDB.Application.ApplicationUser;
using IMDB.Application.DTOs;
using IMDB.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetMovieByNameList
{
    public class GetMovieByNameListQueryHandler : IRequestHandler<GetMovieByNameListQuery, IEnumerable<MovieDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IIMDBRepository _repository;

        public GetMovieByNameListQueryHandler(IMapper mapper, IUserContext userContext, IIMDBRepository repository)
        {
            _mapper = mapper;
            _userContext = userContext;
            _repository = repository;
        }
        public async Task<IEnumerable<MovieDto>> Handle(GetMovieByNameListQuery request, CancellationToken cancellationToken)
        {
            var movies = await _repository.GetMoviesList(request.SearchedName);
            if (!movies.Any())
            {
                return Enumerable.Empty<MovieDto>();
            }
            var moviesDtos = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return moviesDtos;
        }
    }
}
