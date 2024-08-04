using IMDB.Application.DTOs;
using IMDB.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetRatingByName
{
    public class GetRatingByNameQuery : IRequest<RatingDto>
    {
        public string UserId { get; set; }
        public string MovieName { get; set; }
    }
}
