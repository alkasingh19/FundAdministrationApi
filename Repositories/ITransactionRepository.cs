using FundAdministrationApi.Models;

namespace FundAdministrationApi.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByInvestorAsync(int investorId);
    }
}
