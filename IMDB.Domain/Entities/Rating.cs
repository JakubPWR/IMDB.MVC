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
        public float rating { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    }
}
