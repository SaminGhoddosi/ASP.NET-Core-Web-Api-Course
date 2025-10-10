using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
    public class WalkDTO
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public DifficultyDTO Difficulty { get; set; }
        public Guid RegionId { get; set; }
        public RegionDTO Region { get; set; }
    }
}
