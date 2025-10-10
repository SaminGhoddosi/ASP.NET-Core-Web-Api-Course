namespace WebApplication1.Models.Domain
{
    public class Walk
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //esse é para os Navigation Properties
        public Region region { get; set; }
        public Difficulty difficulty { get; set; }

    }
}
