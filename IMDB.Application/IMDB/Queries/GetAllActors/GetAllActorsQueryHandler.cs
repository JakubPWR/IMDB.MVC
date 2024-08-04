using AutoMapper;
using IMDB.Application.DTOs;
using IMDB.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetAllActors
{
    public class GetAllActorsQueryHandler : IRequestHandler<GetAllActorsQuery, IEnumerable<ActorDto>>
    {
        private readonly IIMDBRepository _repository;
        private readonly IMapper _mapper;

        public GetAllActorsQueryHandler(IIMDBRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ActorDto>> Handle(GetAllActorsQuery request, CancellationToken cancellationToken)
        {
            var actors = await _repository.GetAllActors();
            var actorsDto = _mapper.Map<IEnumerable<ActorDto>>(actors);
            return actorsDto;
        }
    }
}
