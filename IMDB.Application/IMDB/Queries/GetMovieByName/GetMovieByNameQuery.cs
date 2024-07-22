using IMDB.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetMovieByName
{
    public class GetMovieByNameQuery : IRequest<MovieDto>
    {
        public string MovieName { get; set; }
        public GetMovieByNameQuery(string name)
        {
            MovieName = name;
        }
    }
}
