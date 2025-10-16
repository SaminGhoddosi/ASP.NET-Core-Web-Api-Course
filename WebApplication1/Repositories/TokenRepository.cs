using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using WebApplication1.Contracts;
using WebApplication1.Models.DTO;

namespace WebApplication1.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;//configuration do web application
        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<Roles> roles)
        {   
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            var rolesString = roles.Select(x => x.ToString());
            foreach (var role in rolesString)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }                                           //tradutor universal de texto
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//carimb
            var token = new JwtSecurityToken(
               _configuration["Jwt:Issuer"],//Payload
                _configuration["Jwt:Audience"],//Payload
                claims,//Payload
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }

}

