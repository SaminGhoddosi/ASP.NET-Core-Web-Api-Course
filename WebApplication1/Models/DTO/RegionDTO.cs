using Microsoft.Win32;
using System.Diagnostics;

namespace WebApplication1.Models.DTO
{
    public class RegionDtoV1
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
    


}