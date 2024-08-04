using IMDB.Application.DTOs;
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
        public async Task DeleteMovie(string name)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.MovieName == name);
            _dbContext.Movies.Remove(movie!);
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Movie> GetMovieByName(string name)
        {
            var movie = _dbContext.Movies.Include(m=>m.Ratings).FirstOrDefault(m => m.MovieName == name);
            return movie;
        }
        public async Task Create(Movie movie)
        {
            await _dbContext.AddAsync(movie);
        }
        public async Task Edit(Movie movie)
        {
            var movieToEdit = _dbContext.Movies.FirstOrDefault(mte => mte.MovieName == movie.MovieName);
        }
        public async Task<Movie> GetByEncodedName(string encodedName) => _dbContext.Movies.FirstOrDefault(m => m.EncodedName == encodedName);

        public async Task AddRating(Rating rating) => await _dbContext.Ratings.AddAsync(rating);
        public async Task CalculateRating(string name)
        {
            var movie = await _dbContext.Movies.Include(m=>m.Ratings).FirstOrDefaultAsync(m => m.MovieName == name);
            var calculated_rating = movie.GetRating();
            movie.Rating = calculated_rating;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Movie>> GetMoviesList(string name)
        {
            var movies = _dbContext.Movies.Where(m=>m.MovieName.ToLower().Contains(name.ToLower()));
            return movies;
        }
        public async Task<Rating> GetRatingById(string MovieName, string UserId)
        {
            var rating = _dbContext.Ratings.Include(r=>r.Movie).Where(r=>r.Movie.MovieName == MovieName && r.UserId == UserId).FirstOrDefault();
            return rating;
        }
        public async Task DeleteRating(Rating rating)
        {
            _dbContext.Ratings.Remove(rating);
        }
        public async Task AddActorToDb(Actor actor)
        {
            await _dbContext.Actors.AddAsync(actor);
        }
        public async Task<Actor> GetActorByName(string name)
        {
            var actor = await _dbContext.Actors.Include(a=>a.Movies).FirstOrDefaultAsync(a=>a.ActorName == name);
            return actor;
        }
        public async Task<IEnumerable<Actor>> GetAllActors()=> await _dbContext.Actors.ToListAsync();
    }
}
