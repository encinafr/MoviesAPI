using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MovieActors
    {
        public int ActorId { get; set; }
        public int MovieId { get; set; }
        public string Personage { get; set; }
        public int Order { get; set; }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
    }
}
