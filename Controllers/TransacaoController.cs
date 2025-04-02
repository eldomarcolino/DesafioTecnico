using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeRecarga.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoBusiness _transacaoBusiness;

        public TransacaoController(ITransacaoBusiness transacaoBusiness)
        {
            _transacaoBusiness = transacaoBusiness;
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
    }
}
