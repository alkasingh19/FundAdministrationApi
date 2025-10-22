using FundAdministrationApi.Configuration;
using FundAdministrationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FundAdministrationApi.Repositories
{
    public class FundRepository : IFundRepository
    {
        private readonly AppDbContext _context;
        public FundRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fund>> GetAllAsync()
        {
            return await _context.Funds.AsNoTracking().ToListAsync();
        }

        public async Task<Fund?> GetByIdAsync(int id)
        {
            return await _context.Funds.FindAsync(id);
        }

        public async Task<Fund> AddAsync(Fund fund)
        {
            _context.Funds.Add(fund);
            await _context.SaveChangesAsync();
            return fund;
        }

        public async Task<Fund> UpdateAsync(Fund fund)
        {
            _context.Funds.Update(fund);
            await _context.SaveChangesAsync();
            return fund;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fund = await _context.Funds.FindAsync(id);
            if (fund == null) return false;
            _context.Funds.Remove(fund);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<(Fund Fund, decimal Subscribed, decimal Redeemed, int InvestorCount)>> GetFundSummariesAsync()
        {
            var results = await _context.Funds
                .Include(f => f.Investors)
                .ThenInclude(i => i.Transactions)
                .ToListAsync();

            return results.Select(f =>
            {
                var transactions = f.Investors?.SelectMany(i => i.Transactions ?? new List<Transaction>()) ?? new List<Transaction>();
                var subscribed = transactions.Where(t => t.Type == TransactionType.Subscription).Sum(t => t.Amount);
                var redeemed = transactions.Where(t => t.Type == TransactionType.Redemption).Sum(t => t.Amount);
                var investorCount = f.Investors?.Count ?? 0;
                return (f, subscribed, redeemed, investorCount);
            });
        }
    }
}
