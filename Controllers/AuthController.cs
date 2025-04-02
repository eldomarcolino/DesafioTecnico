using Microsoft.AspNetCore.Mvc;
using SistemaDeRecarga.Auth;
using SistemaDeRecarga.Model;

namespace SistemaDeRecarga.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IUserBusiness _userBusiness;

        public AuthController(TokenService tokenService, IUserBusiness userBusiness)
        {
            _tokenService = tokenService;
            _userBusiness = userBusiness;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {

            //verifica se há usuários cadastrados
            var userExist = await _userBusiness.HasUserAsync();

            if (!userExist && loginRequest.Email == "admin@uea.edu.br" && loginRequest.Password == "admin123")
            {
                var adminUser = new User
                {
                    Id = 0,
                    Username = "admin",
                    Email = "admin@uea.edu.br",
                    Password = "admin123",
                    Role = "Admin"
                };

                var userToken = _tokenService.GenerateToken(adminUser);

                return Ok(new
                {
                    userToken,
                    user = new
                    {
                        id = adminUser.Id,
                        username = adminUser.Username,
                        email = adminUser.Email,
                        role = adminUser.Role
                    }
                });
            }

            var user = await _userBusiness.AuthenticateAsync(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized(new { Message = "Email ou senha inválidos"});
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    id = user.Id,
                    username = user.Username,
                    email = user.Email,
                    role = user.Role
                }
            });
        }
    }
}
