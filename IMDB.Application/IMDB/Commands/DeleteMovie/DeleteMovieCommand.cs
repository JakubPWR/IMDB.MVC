using IMDB.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IMDB.Application.IMDB.Commands.DeleteMovie
{
    public class DeleteMovieCommand : MovieDto, IRequest
    {
        public string? Confirmation { get; set; }
    }
}
