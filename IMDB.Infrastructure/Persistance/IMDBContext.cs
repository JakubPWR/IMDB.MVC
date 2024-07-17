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
        public DbSet<IMDB>
    }
}
