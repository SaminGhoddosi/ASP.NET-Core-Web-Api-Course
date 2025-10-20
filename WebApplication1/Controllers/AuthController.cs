using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase //é uma classe que gerencia tudo relacionado a usuários
    {                           //foi registrado o UserManeger lá no identityCore
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository) //usa internamente o AuthDbContext
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDtoV1 registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Email,//por padrão, o Identity usa o username para auth, então deixa o email c
                Email = registerRequestDto.Email
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password); //tem que inserir a senha separadamente para ele criptografar
            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }
            if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
            {
                var rolesString = registerRequestDto.Roles.Select(x => x.ToString()).ToList();
                identityResult = await _userManager.AddToRolesAsync(identityUser, rolesString);
                if (!identityResult.Succeeded)
                {
                    return BadRequest(identityResult.Errors);
                }
            }
            return Ok("User was registered");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDtoV1 loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
            if (user != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPassword == true)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {//IList and list
                        var token = _tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDtoV1
                        {
                            JwtToken = token
                        };
                        return Ok(response);
                    }

                }
            }
            return BadRequest("Email or password incorrect"); //poderia ser específico, mostrando se é email ou senha errada, mas isso é menos seguro
        }
    }
}
