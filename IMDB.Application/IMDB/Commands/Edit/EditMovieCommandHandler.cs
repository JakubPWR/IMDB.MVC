using AutoMapper;
using IMDB.Application.ApplicationUser;
using IMDB.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.Edit
{
    public class EditMovieCommandHandler : IRequestHandler<EditMovieCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IIMDBRepository _repository;

        public EditMovieCommandHandler(IMapper mapper, IUserContext userContext, IIMDBRepository repository)
        {
            _mapper = mapper;
            _userContext = userContext;
            _repository = repository;
        }
        public async Task<Unit> Handle(EditMovieCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.IsInRole("Admin"))
            {
                return Unit.Value;
            }
            var movie = await _repository.GetMovieByName(request.MovieName!);
            movie.MovieName = request.MovieName;
            movie.PictureUrl = request.PictureUrl;
            movie.Description = request.Description;
            await _repository.Commit();
            return Unit.Value;
        }
    }
}
