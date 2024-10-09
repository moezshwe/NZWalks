using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range (0, 50)]
        public double LengthInkm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyID { get; set; }
        [Required]
        public Guid RegionID { get; set; }
    }
}
