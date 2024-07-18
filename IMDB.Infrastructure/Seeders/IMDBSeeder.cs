using IMDB.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace IMDB.Infrastructure.Seeders
{
    public class IMDBSeeder
    {
        private readonly IMDBContext _dbContext;

        public IMDBSeeder(IMDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Movies.Any())
                {
                    var Avengers = new Domain.Entities.Movie
                    {
                        MovieName = "Avengers",
                        Description = "Grupa bohaterów walczy ze złem",
                        PictureUrl = "https://ocdn.eu/pulscms-transforms/1/ZDik9kpTURBXy9jYjQ4N2I1ZmI0OWViZGU3ZWNjMDU1ZGQ5MmE4ZGZlYy5qcGeTlQM9Cc0EYs0CeJMFzQMUzQG8kwmmYzI4MGU0Bt4AAaEwBg/avengers.avif"
                    };
                    await _dbContext.Movies.AddAsync(Avengers);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
