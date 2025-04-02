using Microsoft.EntityFrameworkCore;
using SistemaDeRecarga.Context;
using SistemaDeRecarga.Model;
using System.Transactions;

namespace SistemaDeRecarga.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly AppDbContext _context;

        public TransacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transacao>> GetAllTransactionAsync()
        {
            return await _context.Transaction
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> GetTransacaoByIdUserAsync(int idUser)
        {
            return await _context.Transaction
                .Where(t => t.IdUser == idUser)
                .OrderByDescending (t => t.TransactionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> GetTransacaoByIdUserWithDatesAsync(int idUser, DateTime? startDate, DateTime? endDate)
        {
            IQueryable<Transacao> query = _context.Transaction
                .Where(t => t.IdUser == idUser);

            if (startDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate <= endDate.Value);
            }

            return await query
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }

        public async Task CreateTransacaoAsync(Transacao transacao)
        {
            await _context.Transaction.AddAsync(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLastIdAsync()
        {
            if (!await _context.Transaction.AnyAsync())
            {
                return 0;
            }

            return await _context.Transaction.MaxAsync(x => x.Id);
        }
    }
}
