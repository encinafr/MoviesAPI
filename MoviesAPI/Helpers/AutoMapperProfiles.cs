using AutoMapper;
using Core.Entities;
using Domain.Entities;
using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Gender, GenderDTO>().ReverseMap();
            CreateMap<CreateGenderDTO, Gender>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreateDto, Actor>().ForMember(x => x.Photo, options => options.Ignore());
            CreateMap<ActorPatchDto, Actor>().ReverseMap();

            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<CreateMovieDto, Movie>().ForMember(x => x.Poster, options => options.Ignore());
            CreateMap<MoviePatchDto, Movie>().ReverseMap();

        }
    }
}
