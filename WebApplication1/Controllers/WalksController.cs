using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.CustomActionFilters;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using NZWalks.Repositories.Contracts;

namespace WebApplication1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
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
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDtoV1 addWalkRequestDTO)
        {
            var walkDomain = _mapper.Map<Walk>(addWalkRequestDTO);
            await _walkRepository.CreateAsync(walkDomain);
            var walkDTO = _mapper.Map<WalkDtoV1>(walkDomain);
            return CreatedAtAction(nameof(GetById), new { walkDomain.id }, walkDTO);

        }
        [HttpGet]
        [Route("{name}")]//por que não tem string? Porque não é uma constraint
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> FindByName([FromRoute]string name, [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var walks = await _walkRepository.GetByNameAsync(name, sortBy, isAscending, pageSize, pageNumber);
            var walksDTO = _mapper.Map<List<WalkDtoV1>>(walks);
            return Ok(walksDTO);
        }
        //GET: /api/walks?filterOn=Name&filterQuery=Park&SortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null, [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true,//não é null porque é true or false
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {//fromquery is to acess infomartions and frombody is to modify, create, etc.
            var walksDomain = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            var walksDTO = _mapper.Map<List<WalkDtoV1>>(walksDomain); //não precisa criar Map de List, ele faz sozinho
            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.GetByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = _mapper.Map<WalkDtoV1>(walkDomain);
            return Ok(walkDTO);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDtoV1 updateWalkResponse)
        {
            var walkDomain = _mapper.Map<Walk>(updateWalkResponse);
            walkDomain = await _walkRepository.UpdateAsync(id, walkDomain);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = _mapper.Map<WalkDtoV1>(walkDomain);
            return Ok(walkDTO);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.DeleteAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = _mapper.Map<WalkDtoV1>(walkDomain);
            return Ok(walkDTO);
        }
    }
}
