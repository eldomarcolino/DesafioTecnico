using SistemaDeRecarga.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SistemaDeRecarga.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoBusiness _cursoBusiness;

        public CursoController(ICursoBusiness cursoBusiness)
        {
            _cursoBusiness = cursoBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCursoAsync()
        {
            var cursos = await _cursoBusiness.GetAllCursoAsync();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCursoByIdAsync(int id)
        {
            var cursos = await _cursoBusiness.GetCursoByIdAsync(id);
            if (cursos == null)
            {
                return NotFound();
            }
            return Ok(cursos);
        }

        [HttpPost("CreateCurso")]
        public async Task<IActionResult> CreateCursoAsync([FromBody]Curso curso)
        {
            try
            {
                await _cursoBusiness.CreateCursoAsync(curso);

                var sucessResponse = new
                {
                    Success = true,
                    Message = "Curso Criado com sucesso",
                    Data = curso
                };

                return StatusCode(StatusCodes.Status201Created, sucessResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCursoAsync(int id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest("ID do curso não corresponde.");
            }

            try
            {
                await _cursoBusiness.UpdateCursoAsync(curso);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteCurso")]
        public async Task<IActionResult> DeleteCursoAsync(int id)
        {
            try
            {
                await _cursoBusiness.DeleteCursoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
