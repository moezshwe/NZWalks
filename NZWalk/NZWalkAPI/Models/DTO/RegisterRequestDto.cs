using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }
        public string[] Role { get; set; }
    }
}
