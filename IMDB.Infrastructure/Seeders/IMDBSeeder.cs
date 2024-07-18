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
                        PictureUrl = "https://www.filmweb.pl/film/Avengers-2012-371515/posters"
                    };
                    await _dbContext.Movies.AddAsync(Avengers);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
