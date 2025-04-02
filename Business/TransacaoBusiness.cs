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
    }
}
