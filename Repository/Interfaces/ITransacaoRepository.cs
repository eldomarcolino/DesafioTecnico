using SistemaDeRecarga.Model;

public interface ITransacaoRepository
{
    Task<IEnumerable<Transacao>> GetAllTransactionAsync();
    Task<IEnumerable<Transacao>> GetTransacaoByIdUserAsync(int idUser);
    Task CreateTransacaoAsync(Transacao transacao);
    Task<int> GetLastIdAsync();
}