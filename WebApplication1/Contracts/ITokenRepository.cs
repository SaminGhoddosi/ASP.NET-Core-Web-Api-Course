using Microsoft.AspNetCore.Identity;
using WebApplication1.Models.DTO;

namespace WebApplication1.Contracts
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<Roles> roles);
    }
}
