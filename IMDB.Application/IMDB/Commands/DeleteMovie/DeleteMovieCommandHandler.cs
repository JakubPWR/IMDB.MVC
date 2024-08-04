using IMDB.Application.ApplicationUser;
using IMDB.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.DeleteMovie
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IIMDBRepository _repository;
        private readonly IUserContext _httpContext;

        public DeleteMovieCommandHandler(IIMDBRepository repository, IUserContext httpContext)
        {
            _repository = repository;
            _httpContext = httpContext;
        }
        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _httpContext.GetCurrentUser();
            var isEditable = currentUser != null && currentUser.IsInRole("Admin");
            if (!isEditable)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this movie.");
            }
            var confirmedDelete = request.Confirmation;
            if (confirmedDelete != "yes")
            {
                return Unit.Value;
            }
            var movie = await _repository.GetMovieByName(request.MovieName);
            if (movie == default)
            {
                return Unit.Value;
            }
            await _repository.DeleteMovie(request.MovieName);
            await _repository.Commit();
            return Unit.Value;
        }
    }
}
