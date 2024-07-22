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
        public bool IsEditable { get; set; }
        public string CreatedById { get; set; }
        public string Description { get; set; }
    }
}
