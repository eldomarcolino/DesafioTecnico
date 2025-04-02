using SistemaDeRecarga.Model;

public interface ITransacaoBusiness
{
    Task<IEnumerable<Transacao>> GetAllTransactionAsync();
    Task<IEnumerable<Transacao>> GetTransacaoByIdUserAsync(int idUser);
    Task<IEnumerable<Transacao>> GetTransacaoByIdUserWithDatesAsync(int idUser, DateTime? startDate, DateTime? endDate);
}