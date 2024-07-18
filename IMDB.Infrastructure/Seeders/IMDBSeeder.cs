using IMDB.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

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
                        Description = "Grupa bohaterów walczy ze złem"
                    };
                    await _dbContext.Movies.AddAsync(Avengers);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
