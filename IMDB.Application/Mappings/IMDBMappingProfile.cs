using AutoMapper;
using IMDB.Application.ApplicationUser;
using IMDB.Application.DTOs;
using IMDB.Application.IMDB.Commands.CreateMovie;
using IMDB.Application.IMDB.Commands.DeleteMovie;
using IMDB.Application.IMDB.Commands.Edit;
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
            CreateMap<Movie, MovieDto>()
                .ForMember(cd => cd.IsEditable, opt => opt.MapFrom(src => user != null && (src.CreatedById == user.Id || user.IsInRole("Admin"))));
            CreateMap<MovieDto, DeleteMovieCommand>();
            CreateMap<CreateMovieCommand, Movie>();
            CreateMap<MovieDto, EditMovieCommand>();
        }
    }
}
