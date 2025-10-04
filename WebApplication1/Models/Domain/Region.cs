namespace WebApplication1.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
