using FluentValidation;
using IMDB.Application.IMDB.Commands.CreateMovie;
using IMDB.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator(IIMDBRepository repository)
        {
            RuleFor(d => d.Confirmation).NotEmpty().WithMessage("You must type yes to confirm delete");
        }
    }
}
