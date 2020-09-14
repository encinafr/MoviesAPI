using Microsoft.AspNetCore.Http;
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
    }
}
