using FundAdministrationApi.Models;

namespace FundAdministrationApi.Repositories
{
    public interface IFundRepository
    {
        Task<IEnumerable<Fund>> GetAllAsync();
        Task<Fund?> GetByIdAsync(int id);
        Task<Fund> AddAsync(Fund fund);
        Task<Fund> UpdateAsync(Fund fund);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<(Fund Fund, decimal Subscribed, decimal Redeemed, int InvestorCount)>> GetFundSummariesAsync();
    }
}
