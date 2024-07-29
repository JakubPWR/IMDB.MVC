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
        public DbSet<Domain.Entities.Movie> Movies { get; set; }
        public DbSet<Domain.Entities.Actor> Actors { get; set; }
        public DbSet<Domain.Entities.Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.Movie>(m =>
            {
                m.HasMany(m => m.Ratings)
                .WithOne(r=>r.Movie)
                .HasForeignKey(r=>r.MovieId);
                m.OwnsOne(m => m.Cast);
            }
            );
            modelBuilder.Entity<Domain.Entities.Actor>(a =>
            a.OwnsOne(a => a.Movies)
            );
        }
    }
}
