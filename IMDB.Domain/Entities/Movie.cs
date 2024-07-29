using Microsoft.AspNetCore.Identity;
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
        public string Description { get; set; }
        public DateTime PremierDate { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public List<Rating> Ratings { get; set; } = new();
        public List<Actor> Cast { get; set; } = new();
        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }
        public string EncodedName { get; private set; } = default!;
        public void EncodeName() => EncodedName = MovieName.ToLower().Replace(" ", "-");
        public float GetRating()
        {
            float sum = 0;
            if (Ratings.Count > 0)
            {
                foreach (var r in Ratings)
                {
                    sum += r.rating;
                }
                return sum / Ratings.Count;
            }
            else
            {
                return 0;
            }
        }
        public void AddRating(Rating rating)
        {
            Ratings.Insert(0,rating);
            Console.WriteLine($"Rating added {rating.About}");
        }
    }
}
