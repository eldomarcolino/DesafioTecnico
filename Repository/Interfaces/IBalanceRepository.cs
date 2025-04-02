using SistemaDeRecarga.Model;

public interface IBalanceRepository
{
    Task<Balance> GetBalanceByIdUserAsync(int idUser);
    Task CreateBalanceAsync(Balance balance);
    Task UpdateBalanceAsync(Balance balance);
}