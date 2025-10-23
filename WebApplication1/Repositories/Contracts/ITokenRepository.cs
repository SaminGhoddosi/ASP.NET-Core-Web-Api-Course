using Microsoft.AspNetCore.Identity;
using WebApplication1.Models.DTO;

namespace NZWalks.Repositories.Contracts
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
