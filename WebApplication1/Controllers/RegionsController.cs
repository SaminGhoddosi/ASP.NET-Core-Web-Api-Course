using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.CustomActionFilters;
using WebApplication1.Contracts;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {

            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        [Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("GetAllRegions Action Method was invoked");

                var regionsDomain = await _regionRepository.GetAllAsync();
                var regionsDTO = _mapper.Map<List<RegionDTO>>(regionsDomain);
                _logger.LogInformation($"Finished GetAllRegions with data: {JsonSerializer.Serialize(regionsDomain)}");
                return Ok(regionsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); //o erro vai aparecer só para mim
                throw;//encerra o programa
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer, Reader")]
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
        [Authorize(Roles = "Writer")]
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
        [Authorize(Roles = "Writer")]
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
        [Authorize(Roles = "Reader")]
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
