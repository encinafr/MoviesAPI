using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/movie")]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStore _fileStore;
        private readonly string container = "movies";

        public MovieController(ApplicationDbContext dbContext, IMapper mapper, IFileStore fileStore)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileStore = fileStore;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDto>>> Get()
        {
            var movies = await _dbContext.Movies.ToListAsync();
            return _mapper.Map<List<MovieDto>>(movies);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public async Task<ActionResult<MovieDto>> Get(int id)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
                return NotFound();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return movieDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm]CreateMovieDto createMovieDto)
        {
            var movie = _mapper.Map<Movie>(createMovieDto);

            if (createMovieDto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await createMovieDto.Poster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(createMovieDto.Poster.FileName);
                    movie.Poster = await _fileStore.SaveFile(content, extension, container,
                                                           createMovieDto.Poster.ContentType);
                }
            }

            AssignActorsOrder(movie);
            _dbContext.Add(movie);
            await _dbContext.SaveChangesAsync();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return new CreatedAtRouteResult("GetMovie", new { id = movie.Id }, movieDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromForm] CreateMovieDto createMovieDto, int id)
        {
            var movie = await _dbContext.Movies.
                        Include(x => x.MovieActors)
                        .Include(x => x.MovieGenders)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
                return NotFound();

            movie = _mapper.Map(createMovieDto, movie);
            _dbContext.Entry(movie).State = EntityState.Modified;

            if (createMovieDto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await createMovieDto.Poster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(createMovieDto.Poster.FileName);
                    movie.Poster = await _fileStore.EditFile(content, extension, container,
                                                           movie.Poster, createMovieDto.Poster.ContentType);

                }
            }
            AssignActorsOrder(movie);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<MoviePatchDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var movie = await _dbContext.Actors.FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
                return NotFound();

            var movieDto = _mapper.Map<MoviePatchDto>(movie);

            patchDocument.ApplyTo(movieDto, ModelState);

            bool isValid = TryValidateModel(movie);

            if (!isValid)
                return BadRequest(ModelState);

            _mapper.Map(movieDto, movie);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _dbContext.Movies.AnyAsync(g => g.Id == id);

            if (!exist)
                return NotFound();

            _dbContext.Remove(new Movie { Id = id });
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        #region Private
        private void AssignActorsOrder(Movie movie)
        {
            if (movie.MovieActors != null)
            {
                for (int i = 0; i < movie.MovieActors.Count; i++)
                {
                    movie.MovieActors[i].Order = i;
                }
            }
        }
        #endregion
    }
}
