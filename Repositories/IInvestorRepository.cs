using FundAdministrationApi.Models;

namespace FundAdministrationApi.Repositories
{
    public interface IInvestorRepository
    {
        Task<IEnumerable<Investor>> GetAllAsync();
        Task<Investor?> GetByIdAsync(int id);
        Task<Investor> AddAsync(Investor investor);
        Task<Investor> UpdateAsync(Investor investor);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Transaction>> GetTransactionsByInvestorAsync(int investorId);
    }
}
