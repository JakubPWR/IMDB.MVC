using IMDB.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.Edit
{
    public class EditMovieCommand : MovieDto, IRequest
    {
        public string EncodedNameEdit { get; set; } 
    }
}
