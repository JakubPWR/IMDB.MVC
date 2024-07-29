using FluentValidation;
using IMDB.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.AddRating
{
    public class AddRatingCommandValidator : AbstractValidator<AddRatingCommand>
    {
        public AddRatingCommandValidator(IIMDBRepository repository) 
        {
            RuleFor(r => r.rating).NotEmpty().WithMessage("Rating can not be empty")
                .GreaterThanOrEqualTo(0).WithMessage("Minimal rating is 0")
                .LessThanOrEqualTo(10).WithMessage("Maximum rating is 10");
        }
    }
}
