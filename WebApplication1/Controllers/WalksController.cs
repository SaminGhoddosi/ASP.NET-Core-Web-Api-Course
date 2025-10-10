using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.CustomActionFilters;
using WebApplication1.Contracts;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            var walkDomain = _mapper.Map<Walk>(addWalkRequestDTO);
            await _walkRepository.CreateAsync(walkDomain);
            var walkDTO = _mapper.Map<WalkDTO>(walkDomain);
            return CreatedAtAction(nameof(GetById), new { id = walkDomain.id }, walkDTO);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomain = await _walkRepository.GetAllAsync();
            var walksDTO = _mapper.Map<List<WalkDTO>>(walksDomain); //não precisa criar Map de List, ele faz sozinho
            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.GetByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = _mapper.Map<WalkDTO>(walkDomain);
            return Ok(walkDTO);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkResponse)
        {
            var walkDomain = _mapper.Map<Walk>(updateWalkResponse);
            walkDomain = await _walkRepository.UpdateAsync(id, walkDomain);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = _mapper.Map<WalkDTO>(walkDomain);
            return Ok(walkDTO);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.DeleteAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = _mapper.Map<WalkDTO>(walkDomain);
            return Ok(walkDTO);
        }
    }
}
