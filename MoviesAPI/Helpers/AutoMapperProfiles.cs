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
            CreateMap<CreateMovieDto, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MovieGenders, options => options.MapFrom(MapMovieGenders))
                .ForMember(x => x.MovieActors, options => options.MapFrom(MapMovieActors));
            CreateMap<MoviePatchDto, Movie>().ReverseMap();

        }

        private List<MovieGenders> MapMovieGenders(CreateMovieDto createMovieDto, Movie movie)
        {
            var result = new List<MovieGenders>();

            if (createMovieDto.GendersId == null)
                return result;

            foreach (var id in createMovieDto.GendersId)
            {
                result.Add(new MovieGenders() { GenderId = id });
            }

            return result;
        }

        private List<MovieActors> MapMovieActors(CreateMovieDto createMovieDto, Movie movie)
        {
            var result = new List<MovieActors>();

            if (createMovieDto.Actors == null)
                return result;

            foreach (var actor in createMovieDto.Actors)
            {
                result.Add(new MovieActors() { ActorId = actor.ActorId, Personage = actor.Personage });
            }

            return result;
        }
    }
}
