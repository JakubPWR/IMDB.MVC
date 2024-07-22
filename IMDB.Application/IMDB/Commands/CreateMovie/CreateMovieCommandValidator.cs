using FluentValidation;
using IMDB.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator(IIMDBRepository repository) 
        {
            RuleFor(c => c.MovieName).NotEmpty().WithMessage("Movie name can not be empty")
                .Custom((value, context) =>
                {
                    var existingMovie = repository.GetMovieByName(value).Result;
                    if (existingMovie != null)
                    {
                        context.AddFailure($"{value} is not unique name for movie");
                    }
                });
            RuleFor(c => c.PictureUrl).NotEmpty().WithMessage("Movie picture url can not be empty");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Movie description can not be empty");
        }
    }
}
