using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string ActorName { get; set; }
        public List<Movie> Movies { get; set; } = new ();
        public string PictureUrl { get; set; }
    }
}
