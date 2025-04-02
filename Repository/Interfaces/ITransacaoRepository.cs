using SistemaDeRecarga.Model;

public interface ITransacaoRepository
{
    Task<IEnumerable<Transacao>> GetAllTransactionAsync();
    Task<IEnumerable<Transacao>> GetTransacaoByIdUserAsync(int idUser);
    Task<IEnumerable<Transacao>> GetTransacaoByIdUserWithDatesAsync(int idUser, DateTime? startDate, DateTime? endDate);
    Task CreateTransacaoAsync(Transacao transacao);
    Task<int> GetLastIdAsync();
}