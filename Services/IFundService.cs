using FundAdministrationApi.DTOs;

namespace FundAdministrationApi.Services
{
    public interface IFundService
    {
        Task<IEnumerable<FundDto>> GetAllFundsAsync();
        Task<FundDto?> GetFundByIdAsync(int id);
        Task<FundDto> CreateFundAsync(FundDto dto);
        Task<FundDto?> UpdateFundAsync(int id, FundDto dto);
        Task<bool> DeleteFundAsync(int id);
        Task<IEnumerable<FundSummaryDto>> GetFundSummariesAsync();
    }
}
