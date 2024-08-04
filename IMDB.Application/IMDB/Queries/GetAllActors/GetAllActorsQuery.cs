using IMDB.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.IMDB.Queries.GetAllActors
{
    public class GetAllActorsQuery : IRequest<IEnumerable<ActorDto>>
    {
    }
}
