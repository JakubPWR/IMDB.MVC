using FluentValidation;
using IMDB.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.CreateActor
{
    public class AddActorCommandValidator : AbstractValidator<CreateActorCommand>
    {
        public AddActorCommandValidator(IIMDBRepository repository)
        {
            RuleFor(a=>a.ActorName).NotEmpty().WithMessage("Actor Name can not be empty")
                .Custom(async (value, context) =>
                {
                    var existingActor = repository.GetActorByName(value).Result;
                    if (existingActor != null)
                    {
                        context.AddFailure($"{value} is not unique name for Actor");
                    }
                });
            RuleFor(a => a.PictureUrl).NotEmpty().WithMessage("Picture url can not be empty");
        }
    }
}
