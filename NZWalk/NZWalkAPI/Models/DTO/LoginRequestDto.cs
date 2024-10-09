using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace NZWalkAPI.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password )]
        public string Password { get; set; }    
    }
}
