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
    }
}
