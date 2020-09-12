using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dtos;
using MoviesAPI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IFileStore _fileStore;
        private readonly string container = "actores";

        public ActorsController(ApplicationDbContext context, IMapper mapper, IFileStore fileStore)
        {
           _context = context;
           _mapper = mapper;
           _fileStore = fileStore;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginationDto pagination)
        {
            var queryable = _context.Actors.AsQueryable();
            await HttpContext.InsertParamsPagination(queryable, pagination.RecordsPerPage);
            var actors = await queryable.Paginate(pagination).ToListAsync();  

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

            if(actorCreateDto.Photo != null)
            {
                using(var memoryStream = new MemoryStream())
                {
                    await actorCreateDto.Photo.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreateDto.Photo.FileName);
                    actor.Photo = await _fileStore.SaveFile(content, extension, container,
                                                            actorCreateDto.Photo.ContentType);
                }
            }

            _context.Add(actor);
            await _context.SaveChangesAsync();
            var actorDto = _mapper.Map<ActorDTO>(actor);

            return new CreatedAtRouteResult("getActor", new { id = actor.Id }, actorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id ,[FromBody] ActorCreateDto actorDto)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);

            if (actor == null)
                return NotFound();

            actor = _mapper.Map(actorDto, actor);
            _context.Entry(actor).State = EntityState.Modified;

            if (actorDto.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorDto.Photo.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorDto.Photo.FileName);
                    actor.Photo = await _fileStore.EditFile(content, extension, container,
                                                           actor.Photo ,actorDto.Photo.ContentType);

                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);

            if (actor == null)
                return NotFound();

            var actorDto = _mapper.Map<ActorPatchDto>(actor);

            patchDocument.ApplyTo(actorDto, ModelState);

            bool isValid = TryValidateModel(actor);

            if (!isValid)
                return BadRequest(ModelState);

            _mapper.Map(actorDto, actor);

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
