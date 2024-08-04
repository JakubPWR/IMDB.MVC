using IMDB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.DTOs
{
    public class ActorDto
    {
        public string ActorName { get; set; }
        public List<Movie> Movies { get; set; } = new();
        public string PictureUrl { get; set; }
    }
}
