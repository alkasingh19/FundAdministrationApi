using FundAdministrationApi.Configuration;
using FundAdministrationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FundAdministrationApi.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByInvestorAsync(int investorId)
        {
            return await _context.Transactions
                .Where(t => t.InvestorId == investorId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
    }
}
