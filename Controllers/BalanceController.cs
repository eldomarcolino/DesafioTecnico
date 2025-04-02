using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeRecarga.Model;

namespace SistemaDeRecarga.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceBusiness _balanceBusiness;

        public BalanceController(IBalanceBusiness balanceBusiness)
        {
            _balanceBusiness = balanceBusiness;
        }

        [HttpGet("usuario/{idUser}")]
        public async Task<IActionResult> GetBalanceByIdUserAsync(int idUser)
        {
            try
            {
                var saldo = await _balanceBusiness.GetBalanceByIdUserAsync(idUser);
                return Ok(saldo);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false, Message = ex.Message
                });
            }
        }


        [HttpPost("recarregar")]
        public async Task<IActionResult> AddBalanceAsync([FromBody] AddBalanceRequest request)
        {
            try
            {
                var balance = await _balanceBusiness.AddBalanceAsync(
                    request.IdUser,
                    request.Amount,
                    request.Description);

                var successResponse = new
                {
                    Success = true,
                    Message = "Recarga realizada com sucesso.",
                    Balance = balance
                };

                return StatusCode(StatusCodes.Status201Created, successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("debitar")]
        public async Task<IActionResult> DeductBalanceAsync([FromBody] AddBalanceRequest request)
        {
            try
            {
                var balance = await _balanceBusiness.DeductBalanceAsync(
                    request.IdUser,
                    request.Amount,
                    request.Description);

                var successResponse = new
                {
                    Success = true,
                    Message = "Compra realizada com sucesso.",
                    Balance = balance
                };

                return StatusCode(StatusCodes.Status201Created, successResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}
