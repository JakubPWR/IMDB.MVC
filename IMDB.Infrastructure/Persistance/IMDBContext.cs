using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Infrastructure.Persistance
{
    public class IMDBContext : IdentityDbContext
    {
        public IMDBContext(DbContextOptions<IMDBContext> options):base(options)
        {

        }
        public DbSet<IMDB.Domain.Entities.Movie> Movies { get; set; }
        public DbSet<IMDB.Domain.Entities.Actors> Actors { get; set; }
        public DbSet<IMDB.Domain.Entities.Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.Movie>(m =>
            m.OwnsOne(m => m.Ratings)
            );
            modelBuilder.Entity<Domain.Entities.Actors>(a =>
            a.OwnsOne(a => a.Movies)
            );
        }
    }
}
