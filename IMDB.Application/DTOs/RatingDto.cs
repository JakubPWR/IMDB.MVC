using IMDB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.DTOs
{
    public class RatingDto
    {
        public int rating { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public string? About { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
    }
}
