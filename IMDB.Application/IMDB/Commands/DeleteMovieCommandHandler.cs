using IMDB.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IIMDBRepository _repository;

        public DeleteMovieCommandHandler(IIMDBRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = _repository.DeleteMovie(request.MovieName);
            if (movie == default)
            {
                return Unit.Value;
            }
            await _repository.Commit();
            return Unit.Value;
        }
    }
}
