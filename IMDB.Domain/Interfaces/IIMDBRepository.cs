using IMDB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain.Interfaces
{
    public interface IIMDBRepository
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task DeleteMovie(string name);
        Task Commit();
        Task<Movie> GetMovieByName(string name);
        Task Create(Movie movie);
        Task Edit(Movie movie);
        Task<Movie> GetByEncodedName(string encodedName);
        Task AddRating(Rating rating);
        Task CalculateRating(string name);
    }
}
