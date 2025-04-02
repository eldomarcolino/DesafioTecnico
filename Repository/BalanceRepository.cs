using Microsoft.EntityFrameworkCore;
using SistemaDeRecarga.Context;
using SistemaDeRecarga.Model;

namespace SistemaDeRecarga.Repository
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly AppDbContext _context;

        public BalanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Balance> GetBalanceByIdUserAsync(int idUser)
        {
            return await _context.Balance.FirstOrDefaultAsync(b => b.IdUser == idUser);
        }

        public async Task CreateBalanceAsync(Balance balance)
        {
            await _context.Balance.AddAsync(balance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBalanceAsync(Balance balance)
        {
            _context.Balance.Update(balance);
            await _context.SaveChangesAsync();
        }
    }
}
