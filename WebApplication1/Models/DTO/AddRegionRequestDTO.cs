using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
    public class AddRegionRequestDtoV1
    {
        [Required]//tanto o back quanto o front fazem essa validação
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "Name has to be a maximum of 3 characters")]
        [MinLength(3, ErrorMessage = "Name has to be a minimum of 3 characters")]
        public string Name { get; set; }
        
        public string? RegionImageUrl { get; set; }
    }
}
