using SistemaDeRecarga.Model;

namespace SistemaDeRecarga.Business
{
    public class TransacaoBusiness : ITransacaoBusiness
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoBusiness(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<IEnumerable<Transacao>> GetAllTransactionAsync()
        {
            return await _transacaoRepository.GetAllTransactionAsync();
        }

        public async Task<IEnumerable<Transacao>> GetTransacaoByIdUserAsync(int idUser)
        {
            return await _transacaoRepository.GetTransacaoByIdUserAsync(idUser);
        }

        public async Task<IEnumerable<Transacao>> GetTransacaoByIdUserWithDatesAsync(int idUser, DateTime? startDate, DateTime? endDate)
        {
            return await _transacaoRepository.GetTransacaoByIdUserWithDatesAsync(idUser, startDate, endDate);
        }
    }
}
