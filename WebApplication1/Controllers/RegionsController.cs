using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    { //readonly é usado para declarar campos que só podem ser atribuidos na inicialização do objeto(Constructor)
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllRegions()
        {
            //We are getting the data from DB - the domain model
            var regionsDomain = dbContext.Regions.ToList();

            var regionsDTO = new List<RegionDTO>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDTO.Add(new RegionDTO()
                {
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            return Ok(regionsDTO);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult getById([FromRoute] Guid id)
        {
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //We are creating a regionDTO using the regionDomain to send to the user
            var regionDTO = new RegionDTO()
            {
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDTO);
        }


        [HttpPost]
        public IActionResult Create([FromBody] RegionDTO regionDTO)
        {
            var regionDomain = new Region()
            {
                Code = regionDTO.Code,
                Name = regionDTO.Name,
                RegionImageUrl = regionDTO.RegionImageUrl
            };
            dbContext.Regions.Add(regionDomain);
            dbContext.SaveChanges(); //gera o id automaticamente, só o id
            return CreatedAtAction(nameof(getById), new { id = regionDomain.Id }, regionDTO);
        }
    }
}
