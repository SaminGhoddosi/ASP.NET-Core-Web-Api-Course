using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public DifficultiesController(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var difficultiesModel = await _nZWalksDbContext.Difficulties.ToListAsync();
            if(difficultiesModel == null)
            {
                return NotFound();
            }
            var difficultiesDTO = new List<DifficultyDTO>();
            foreach(var difficultyModel in difficultiesModel)
            {
                difficultiesDTO.Add(new DifficultyDTO
                {
                    Name = difficultyModel.Name
                });
            }
            return Ok(difficultiesDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var difficultyDomain = await _nZWalksDbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if(difficultyDomain == null)
            {
                return NotFound();
            }
            var difficultyDTO = new DifficultyDTO
            {
                Name = difficultyDomain.Name
            };
            return Ok(difficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create( [FromBody] DifficultyDTO difficultyDTO)
        {
            if(difficultyDTO == null)
            {
                return NotFound();
            }
            var difficultyDomain = new Difficulty()
            {
                Name = difficultyDTO.Name
            };
            await _nZWalksDbContext.AddAsync(difficultyDTO);
            await _nZWalksDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(getById), new { id = difficultyDomain.Id }, difficultyDTO);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] DifficultyDTO difficultyDTO)
        {
            var difficultyDomain = await _nZWalksDbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if(difficultyDomain == null)
            {
                return NotFound();
            }
            difficultyDomain.Name = difficultyDTO.Name;
            await _nZWalksDbContext.SaveChangesAsync();
            return Ok(difficultyDTO);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var difficultyDomain = await _nZWalksDbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (difficultyDomain == null)
            {
                return NotFound();
            }
            _nZWalksDbContext.Remove(difficultyDomain);
            await _nZWalksDbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
