using IMDB.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Commands.EditRating
{
    public class EditRatingCommand : RatingDto, IRequest
    {
        public string EncodedNameEditRating { get; set; }
    }
}
