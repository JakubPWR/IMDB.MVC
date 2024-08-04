using AutoMapper;
using IMDB.Application.ApplicationUser;
using IMDB.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.EditRating
{
    public class EditRatingCommandHandler : IRequestHandler<EditRatingCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IIMDBRepository _repository;

        public EditRatingCommandHandler(IMapper mapper, IUserContext userContext, IIMDBRepository repository)
        {
            _mapper = mapper;
            _userContext = userContext;
            _repository = repository;
        }

        public async Task<Unit> Handle(EditRatingCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null)
            {
                return Unit.Value;
            }
            if(currentUser.Id != request.UserId)
            {
                if(!currentUser.IsInRole("Admin"))
                {
                    return Unit.Value;
                }
                return Unit.Value;
                
            }
            var rating = await _repository.GetRatingById(request.EncodedNameEditRating! , request.UserId);
            rating.rating = request.rating;
            rating.About = request.About;
            var movie = await _repository.GetMovieByName(rating.Movie.MovieName);
            await _repository.CalculateRating(rating.Movie.MovieName);
            await _repository.Commit();
            return Unit.Value;
        }
    }
}
