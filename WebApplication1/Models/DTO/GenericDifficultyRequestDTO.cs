using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
    public class GenericDifficultyRequestDTO
    {
        [Required]
        [MinLength(1, ErrorMessage ="Name must has 1 character, at least")]
        public string Name { get; set; }
    }
}
