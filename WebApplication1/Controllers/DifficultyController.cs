using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;
using WebApplication1.CustomActionFilters;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDifficultyRepository _difficultyRepository;
        public DifficultyController(IMapper mapper, IDifficultyRepository difficultyRepository)
        {
            _mapper = mapper;
            _difficultyRepository = difficultyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var difficultiesDomain = await _difficultyRepository.GetAllAsync();
            var difficultiesDTO = _mapper.Map<List<DifficultyDTO>>(difficultiesDomain);
            return Ok(difficultiesDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var difficultyDomain = await _difficultyRepository.GetByIdAsync(id);
            if (difficultyDomain == null)
            {
                return NotFound();
            }
            var difficultyDTO = _mapper.Map<DifficultyDTO>(difficultyDomain);
            return Ok(difficultyDTO);
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] GenericDifficultyRequestDTO genericDifficultyRequestDTO)
        {
            var difficultyDomain = _mapper.Map<Difficulty>(genericDifficultyRequestDTO);
            await _difficultyRepository.CreateAsync(difficultyDomain);
            var difficultyDTO = _mapper.Map<DifficultyDTO>(difficultyDomain);
            return CreatedAtAction(nameof(GetById), new { id = difficultyDomain.Id }, difficultyDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] GenericDifficultyRequestDTO genericDifficultyRequestDTO)
        {
            var difficultyDomain = _mapper.Map<Difficulty>(genericDifficultyRequestDTO);
            if(difficultyDomain == null)
            {
                return NotFound();
            }
            await _difficultyRepository.UpdateAsync(id, difficultyDomain);
            var difficultyDTO = _mapper.Map<DifficultyDTO>(difficultyDomain);
            return Ok(difficultyDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var difficultyDomain = await _difficultyRepository.DeleteAsync(id);
            if(difficultyDomain == null)
            {
                return NotFound();
            }
            var difficultyDTO = _mapper.Map<DifficultyDTO>(difficultyDomain);
            return Ok(difficultyDTO);
        }
    }
}
