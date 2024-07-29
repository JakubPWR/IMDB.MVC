using IMDB.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetRatingsByName
{
    public class GetRatingsByNameQuery : IRequest<IEnumerable<RatingDto>>
    {
        public string RatingMovieName { get; set; }
    }
}
