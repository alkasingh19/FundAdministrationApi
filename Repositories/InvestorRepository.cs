using FundAdministrationApi.Configuration;
using FundAdministrationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FundAdministrationApi.Repositories
{
    public class InvestorRepository : IInvestorRepository
    {
        private readonly AppDbContext _context;
        public InvestorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Investor>> GetAllAsync()
        {
            return await _context.Investors.Include(i => i.Fund).AsNoTracking().ToListAsync();
        }

        public async Task<Investor?> GetByIdAsync(int id)
        {
            return await _context.Investors.Include(i => i.Fund).FirstOrDefaultAsync(i => i.InvestorId == id);
        }

        public async Task<Investor> AddAsync(Investor investor)
        {
            _context.Investors.Add(investor);
            await _context.SaveChangesAsync();
            return investor;
        }

        public async Task<Investor> UpdateAsync(Investor investor)
        {
            _context.Investors.Update(investor);
            await _context.SaveChangesAsync();
            return investor;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var investor = await _context.Investors.FindAsync(id);
            if (investor == null) return false;
            _context.Investors.Remove(investor);
            await _context.SaveChangesAsync();
            return true;
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
