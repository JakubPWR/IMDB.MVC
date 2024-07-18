using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain.Entities
{
    public class Actors
    {
        public int Id { get; set; }
        public string ActorName { get; set; }
        public IEnumerable<Movie>? Movies { get; set; }
        public IEnumerable<Rating>? Ratings { get; set; }

    }
}
