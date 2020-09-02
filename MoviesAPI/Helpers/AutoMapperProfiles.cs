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
            CreateMap<ActorCreateDto, Actor>();

        }
    }
}
