using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class ActorPatchDto
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
