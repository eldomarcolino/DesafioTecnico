using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeRecarga.Model;

namespace SistemaDeRecarga.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoBusiness _transacaoBusiness;
        private readonly IBalanceBusiness _balanceBusiness;

        public TransacaoController(ITransacaoBusiness transacaoBusiness, IBalanceBusiness balanceBusiness)
        {
            _transacaoBusiness = transacaoBusiness;
            _balanceBusiness = balanceBusiness;
        }

        [HttpGet("transacoes")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTransactionAsync()
        {
            try
            {
                var transacoes = await _transacaoBusiness.GetAllTransactionAsync();
                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("transacoes/usuario/{idUser}")]
        public async Task<IActionResult> GetTransacaoByIdUserAsync(int idUser)
        {
            try
            {
                var transacoes = await _transacaoBusiness.GetTransacaoByIdUserAsync(idUser);
                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("usuario/{idUser}")]
        public async Task<IActionResult> GetTransacaoByIdUserAsync(int idUser, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var transacoes = await _transacaoBusiness.GetTransacaoByIdUserWithDatesAsync(idUser, startDate, endDate);

                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}
