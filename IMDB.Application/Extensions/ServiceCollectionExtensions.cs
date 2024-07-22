using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using IMDB.Application.ApplicationUser;
using IMDB.Application.IMDB.Commands.CreateMovie;
using IMDB.Application.IMDB.Queries.GetAllMovies;
using IMDB.Application.IMDB.Queries.GetMovieByName;
using IMDB.Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetAllMoviesQuery));
            services.AddMediatR(typeof(GetMovieByNameQuery));
            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new IMDBMappingProfile(userContext));
            }).CreateMapper()
            );

            services.AddValidatorsFromAssemblyContaining<CreateMovieCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
