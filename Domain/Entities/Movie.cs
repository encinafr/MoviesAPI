using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Title { get; set; }
        public bool  InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public List<MovieActors> MovieActors { get; set; }
        public List<MovieGenders> MovieGenders { get; set; }
    }
}
