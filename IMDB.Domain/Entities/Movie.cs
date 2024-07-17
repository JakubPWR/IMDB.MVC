using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string PictureUrl { get; set; }
        public float Rating { get; set; }
    }
}
