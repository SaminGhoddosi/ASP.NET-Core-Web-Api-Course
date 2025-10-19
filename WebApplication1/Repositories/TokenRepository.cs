using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Contracts;
using WebApplication1.Models.DTO;

namespace WebApplication1.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration; //configuration do web application
        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(//header(tipo de token)
                _configuration["Jwt:Issuer"],//Payload
                _configuration["Jwt:Audience"],//Payload
                claims,//Payload
                expires: DateTime.Now.AddMinutes(15),//Payload
                signingCredentials: credentials); //signature e header(tipo de assinatura)            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }                  //handler -> processa e valida tokens                                     
    }
}
