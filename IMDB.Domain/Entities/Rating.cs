using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int rating { get; set; }
        public string UserId { get; set; }
        public bool IsEditable { get; set; }
        public string UserName { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public string? About { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }

    }
}
