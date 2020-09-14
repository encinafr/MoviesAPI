using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class MoviePatchDto
    {
        [Required]
        [MaxLength(300)]
        public string Title { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<int> GendersId { get; set; }
    }
}
