using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
    public class WalkDtoV1
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public DifficultyDtoV1 Difficulty { get; set; }
        public Guid RegionId { get; set; }
        public RegionDtoV1 Region { get; set; }
    }
}
