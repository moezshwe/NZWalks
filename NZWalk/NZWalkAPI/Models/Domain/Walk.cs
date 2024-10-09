namespace NZWalkAPI.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInkm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyID { get; set; }
        public Guid RegionID { get; set; }
        //Navigation Properties
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
