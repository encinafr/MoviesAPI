using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Helpers;
using MoviesAPI.Helpers.Enums;
using MoviesAPI.Helpers.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class CreateMovieDto
    {
        [Required]
        [MaxLength(300)]
        public string Title { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        [PhotoWeightValidation(maxWeightMG: 4)]
        [FileTypeValidation(FileTypeGruopEnum.Image)]
        public IFormFile Poster { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GendersId { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<CrateActorMovieDto>>))]
        public List<CrateActorMovieDto> Actors { get; set; }
    }
}
