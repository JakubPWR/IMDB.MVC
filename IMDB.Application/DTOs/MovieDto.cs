using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.DTOs
{
    public class MovieDto
    {
        public string MovieName { get; set; }
        public string PictureUrl { get; set; }
        public float Rating { get; set; }
    }
}
