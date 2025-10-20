using AutoMapper;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

namespace WebApplication1.Mappings
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDtoV1>().ReverseMap();
            CreateMap<AddRegionRequestDtoV1, Region>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDtoV1>().ReverseMap();
            CreateMap<Walk, AddWalkRequestDtoV1>().ReverseMap();
            CreateMap<Walk, WalkDtoV1>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDtoV1>().ReverseMap();
            CreateMap<Difficulty, DifficultyDtoV1>().ReverseMap();
            CreateMap<Difficulty, GenericDifficultyRequestDtoV1>().ReverseMap();
        }
    }
}
