using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
    public class AddWalkRequestDTO
    {
        [Required]
        [MinLength(4, ErrorMessage = "Name has to be a maximum of 3 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(3, ErrorMessage = "Description has to be a maximum of 3 characters")]
        [MinLength(3, ErrorMessage = "Description has to be a minimum of 3 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        


    }
}
