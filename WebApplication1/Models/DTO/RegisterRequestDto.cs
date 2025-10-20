using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
    public class RegisterRequestDtoV1
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public List<Roles> Roles { get; set; }
    }
    public enum Roles
    {
        Reader,
        Writer
    }
}
