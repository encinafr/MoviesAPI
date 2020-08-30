using AutoMapper;
using Core.Entities;
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
    [Route("api/gender")]
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
        public async Task<ActionResult<List<GenderDTO>>> GetAll()
        {
            var genders = await _dbContext.Genders.ToListAsync();
            var gendersToReturn = _mapper.Map<List<GenderDTO>>(genders);

            return gendersToReturn;
        }
    }
}
