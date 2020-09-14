using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MovieGenders
    {
        public int GenderId { get; set; }
        public int MovieId { get; set; }
        public Gender Gender { get; set; }
        public Movie Movie { get; set; }
    }
}
