using AutoMapper;
using IMDB.Application.ApplicationUser;
using IMDB.Application.DTOs;
using IMDB.Application.IMDB.Commands;
using IMDB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Application.Mappings
{
    public class IMDBMappingProfile : Profile
    {
        public IMDBMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, DeleteMovieCommand>();
        }
    }
}
