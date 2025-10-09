using AutoMapper;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

namespace WebApplication1.Mappings
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDTO>().ReverseMap();
            CreateMap<Walk, AddWalkRequestDTO>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDTO>().ReverseMap();
        }
    }
}
