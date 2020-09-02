using AutoMapper;
using Core.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/genders")]
    public class GenderController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenderController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenderDTO>>> Get()
        {
            var genders = await _dbContext.Genders.ToListAsync();
            var gendersToReturn = _mapper.Map<List<GenderDTO>>(genders);

            return gendersToReturn;
        }

        [HttpGet("{id:int}", Name = "getGender")]
        public async Task<ActionResult<GenderDTO>> Get(int id)
        {
            var entity = await _dbContext.Genders.FirstOrDefaultAsync(g => g.Id == id);

            if (entity == null)
                return NotFound();

            var genderToReturn = _mapper.Map<GenderDTO>(entity);

            return genderToReturn;
        }

        [HttpPost]
        public async Task<ActionResult<GenderDTO>> Post([FromBody] CreateGenderDTO genderDTO)
        {
            var entity = _mapper.Map<Gender>(genderDTO);
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            var genderDto = _mapper.Map<GenderDTO>(entity);

            return new CreatedAtRouteResult("getGender", new { id = genderDto.Id }, genderDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateGenderDTO genderDTO)
        {
            var entity = _mapper.Map<Gender>(genderDTO);
            entity.Id = id;
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var exist = await _dbContext.Genders.AnyAsync(g => g.Id == id);

            if (!exist)
                return NotFound();

            _dbContext.Remove(new Gender { Id = id });
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
