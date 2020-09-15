using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Dtos
{
    public class MoviePatchDto
    {
        [Required]
        [MaxLength(300)]
        public string Title { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}
