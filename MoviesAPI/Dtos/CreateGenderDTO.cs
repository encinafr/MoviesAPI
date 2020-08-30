using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class CreateGenderDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
