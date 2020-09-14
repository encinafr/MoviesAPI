using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActors>().HasKey(x => new { x.ActorId, x.MovieId });
            modelBuilder.Entity<MovieGenders>().HasKey(x => new { x.GenderId, x.MovieId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActors> MovieActors { get; set; }
        public DbSet<MovieGenders> MovieGenders { get; set; }
    }
}
