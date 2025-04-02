using SistemaDeRecarga.Model;

public interface IBalanceBusiness
{
    Task<Balance> GetBalanceByIdUserAsync(int idUser);
    Task<Balance> AddBalanceAsync(int idUser, decimal valor, string description);
    Task<Balance> DeductBalanceAsync(int idUser, decimal valor, string description);
    Task<Transacao> TransferirSaldoAsync(int idRemetente, int idDestinatario, decimal valor, string description);
}