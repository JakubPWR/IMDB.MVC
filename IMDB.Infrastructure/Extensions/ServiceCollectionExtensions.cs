using IMDB.Domain.Interfaces;
using IMDB.Infrastructure.Persistance;
using IMDB.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IMDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IMDB")));
            services.AddScoped<Seeders.IMDBSeeder>();
            services.AddScoped<IIMDBRepository, IMDBRepository>();
            services.AddRazorPages();
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IMDBContext>();
        }
    }
}
