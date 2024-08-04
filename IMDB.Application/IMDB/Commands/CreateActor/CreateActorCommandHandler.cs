using AutoMapper;
using IMDB.Application.ApplicationUser;
using IMDB.Domain.Entities;
using IMDB.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.CreateActor
{
    public class CreateActorCommandHandler : IRequestHandler<CreateActorCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IIMDBRepository _repository;

        public CreateActorCommandHandler(IMapper mapper, IUserContext userContext, IIMDBRepository repository)
        {
            _mapper = mapper;
            _userContext = userContext;
            _repository = repository;
        }
        public async Task<Unit> Handle(CreateActorCommand request, CancellationToken cancellationToken)
        {
            var actor = _mapper.Map<Actor>(request);
            await _repository.AddActorToDb(actor);
            await _repository.Commit();
            return Unit.Value;
        }
    }
}
