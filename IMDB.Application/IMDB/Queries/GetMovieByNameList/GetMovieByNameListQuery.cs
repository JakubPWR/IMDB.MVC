using IMDB.Application.DTOs;
using IMDB.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetMovieByNameList
{
    public class GetMovieByNameListQuery : IRequest<IEnumerable<MovieDto>>
    {
        public string SearchedName { get; set; }
    }
}
