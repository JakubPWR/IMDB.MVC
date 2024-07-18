using IMDB.Domain.Entities;
using IMDB.Domain.Interfaces;
using IMDB.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Infrastructure.Repositories
{
    public class IMDBRepository : IIMDBRepository
    {
        public IMDBContext _dbContext;
        public IMDBRepository(IMDBContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Domain.Entities.Movie>> GetAllMovies() => await _dbContext.Movies.ToListAsync();

    }
}
