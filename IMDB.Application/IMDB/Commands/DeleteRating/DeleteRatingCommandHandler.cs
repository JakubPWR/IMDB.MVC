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

namespace IMDB.Application.IMDB.Commands.DeleteRating
{
    public class DeleteRatingCommandHandler : IRequestHandler<DeleteRatingCommand>
    {
        private readonly IIMDBRepository _repository;
        private readonly IUserContext _httpContext;
        private readonly IMapper _mapper;

        public DeleteRatingCommandHandler(IIMDBRepository repository, IUserContext httpContext, IMapper mapper)
        {
            _repository = repository;
            _httpContext = httpContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _httpContext.GetCurrentUser();
            var isEditable = currentUser != null && currentUser.IsInRole("Admin");
            if (!isEditable)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this rating.");
            }
            var rating = await _repository.GetRatingById(request.MovieName, request.UserId);
            var confirmedDelete = request.Confirmation;
            if (confirmedDelete.ToLower() != "yes")
            {
                return Unit.Value;
            }
            if (rating == default)
            {
                return Unit.Value;
            }
            var movie = await _repository.GetMovieByName(request.MovieName);
            movie.Ratings.Remove(rating);
            await _repository.DeleteRating(rating);
            await _repository.CalculateRating(request.MovieName);
            await _repository.Commit();
            return Unit.Value;
        }
    }
}
