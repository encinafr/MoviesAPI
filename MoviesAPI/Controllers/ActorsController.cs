using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
           _context = context;
           _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var actors = await _context.Actors.ToListAsync();  

            return _mapper.Map<List<ActorDTO>>(actors); ;
        }

        [HttpGet("{id}", Name = "getActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null)
                return NotFound();

            var actorDto = _mapper.Map<ActorDTO>(actor);

            return actorDto;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreateDto actorCreateDto)
        {
            var actor = _mapper.Map<Actor>(actorCreateDto);
            _context.Add(actor);
            await _context.SaveChangesAsync();
            var actorDto = _mapper.Map<ActorDTO>(actor);

            return new CreatedAtRouteResult("getActor", new { id = actor.Id }, actorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id ,[FromBody] ActorCreateDto actordto)
        {
            var actor = _mapper.Map<ActorDTO>(actordto);
            actor.Id = id;
            _context.Entry(actor).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _context.Actors.AnyAsync(g => g.Id == id);

            if (!exist)
                return NotFound();

            _context.Remove(new Actor { Id = id });
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
