using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeRecarga.Business;
using SistemaDeRecarga.Model;

namespace SistemaDeRecarga.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserFilterAsync([FromQuery]int? id = null, int? idCourse = null, string username = null, string role = null)
        {
            var users = await _userBusiness.GetAllUserFilterAsync(id, idCourse, username, role);

            var userDto = users.Select(u => new UserDTO
            {
                Id = u.Id,
                IdCourse = u.IdCourse,
                Username = u.Username,
                Email = u.Email,
                RegistrationNumber = u.RegistrationNumber,
                Role = u.Role,
                Createdate = u.Createdate
            });

            return Ok(userDto);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            try
            {
                await _userBusiness.CreateUserAsync(user);

                var successResponse = new
                {
                    Success = true,
                    Message = "Usuário criado com sucesso",
                    Data = user
                };

                return StatusCode(StatusCodes.Status201Created, successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest("ID do usuário não corresponde.");
            }

            try
            {
                await _userBusiness.UpdateUserAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                await _userBusiness.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
