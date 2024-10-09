using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.DTO;
using NZWalkAPI.Repository;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public ITokenRepository tokenRepository { get; }

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Role != null && registerRequestDto.Role.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Role);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }

                }
            }
            return BadRequest("Something went wrong.");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody ] LoginRequestDto  loginRequestDto )
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
               var checkPasswordResult= await userManager.CheckPasswordAsync(user,loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                      var jwtToken=  tokenRepository.CreateJwtToken(user,roles.ToList());
                        var response = new LoginResponseDto 
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(jwtToken);
                    }
                    
                   
                }
            }
            return BadRequest("Username or password incorrect.");
        }

    }
}
