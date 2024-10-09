using Microsoft.AspNetCore.Identity;

namespace NZWalkAPI.Repository
{
    public interface ITokenRepository
    {
      string  CreateJwtToken(IdentityUser user,List<string> roles);
    }
}
