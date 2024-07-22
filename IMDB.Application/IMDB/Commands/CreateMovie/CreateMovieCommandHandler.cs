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

namespace IMDB.Application.IMDB.Commands.CreateMovie
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IIMDBRepository _repository;

        public CreateMovieCommandHandler(IMapper mapper, IUserContext userContext, IIMDBRepository repository)
        {
            _mapper = mapper;
            _userContext = userContext;
            _repository = repository;
        }
        public async Task<Unit> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.IsInRole("Admin"))
            {
                return Unit.Value;
            }
            var movie = _mapper.Map<Movie>(request);
            movie.CreatedById = currentUser.Id;
            movie.EncodeName();
            await _repository.Create(movie);
            await _repository.Commit();
            return Unit.Value;
        }
    }
}
