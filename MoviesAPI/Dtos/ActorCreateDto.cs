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
    public class ActorCreateDto
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        [PhotoWeightValidation(maxWeightMG: 100)]
        [FileTypeValidation(FileTypeGruopEnum.Image)]
        public IFormFile Photo { get; set; }
    }
}
