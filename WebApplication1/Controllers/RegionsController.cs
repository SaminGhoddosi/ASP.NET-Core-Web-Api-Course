using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.CustomActionFilters;
using WebApplication1.Contracts;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {

            _regionRepository = regionRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await _regionRepository.GetAllAsync();
            var regionsDTO = _mapper.Map<List<RegionDTO>>(regionsDomain);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            var regionDTO = _mapper.Map<RegionDTO>(regionDomain);
            return Ok(regionDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO regionRequestDTO)
        {
            var regionDomain = _mapper.Map<Region>(regionRequestDTO);
            await _regionRepository.CreateAsync(regionDomain);
            var regionDTO = _mapper.Map<RegionDTO>(regionDomain);
            return CreatedAtAction(nameof(GetById), new { id = regionDomain.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomain = _mapper.Map<Region>(updateRegionRequestDTO);
            if (regionDomain == null)
            {
                return NotFound();
            }
            regionDomain = await _regionRepository.UpdateAsync(id, regionDomain);
            var regionDTO = _mapper.Map<RegionDTO>(regionDomain);
            return Ok(regionDTO);

        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.DeleteAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            var regionDTO = _mapper.Map<RegionDTO>(regionDomain);
            return Ok(regionDTO);
        }
    }
}
