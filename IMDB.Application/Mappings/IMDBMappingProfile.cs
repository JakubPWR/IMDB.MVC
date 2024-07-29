using AutoMapper;
using IMDB.Application.ApplicationUser;
using IMDB.Application.DTOs;
using IMDB.Application.IMDB.Commands.AddRating;
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
                .ForMember(cd => cd.IsEditable, opt => opt.MapFrom(src => user != null && (src.CreatedById == user.Id || user.IsInRole("Admin"))))
                .ReverseMap();
            CreateMap<MovieDto, DeleteMovieCommand>();
            CreateMap<CreateMovieCommand, Movie>();
            CreateMap<MovieDto, EditMovieCommand>();
            CreateMap<RatingDto, Rating>()
                .ForMember(r => r.UserName, opt => opt.MapFrom(src => user.Email))
                .ForMember(r => r.UserId, opt => opt.MapFrom(src => user.Id));
            CreateMap<AddRatingCommand, Rating>()
                .ForMember(r => r.UserName, opt => opt.MapFrom(src => user.Email))
                .ForMember(r => r.UserId, opt => opt.MapFrom(src => user.Id));
            CreateMap<Rating, AddRatingCommand>();
        }
    }
}
