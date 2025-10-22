using FundAdministrationApi.DTOs;

namespace FundAdministrationApi.Services
{
    public interface IInvestorService
    {
        Task<IEnumerable<InvestorDto>> GetAllInvestorsAsync();
        Task<InvestorDto?> GetInvestorByIdAsync(int id);
        Task<InvestorDto> CreateInvestorAsync(InvestorDto dto);
        Task<InvestorDto?> UpdateInvestorAsync(int id, InvestorDto dto);
        Task<bool> DeleteInvestorAsync(int id);
        Task<InvestorTransactionSummaryDto?> GetInvestorTransactionsAsync(int investorId);
    }
}
