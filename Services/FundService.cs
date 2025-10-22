using FundAdministrationApi.DTOs;
using FundAdministrationApi.Models;
using FundAdministrationApi.Repositories;

namespace FundAdministrationApi.Services
{
    public class FundService : IFundService
    {
        private readonly IFundRepository _repo;
        public FundService(IFundRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FundDto>> GetAllFundsAsync()
        {
            var funds = await _repo.GetAllAsync();
            return funds.Select(f => new FundDto
            {
                FundId = f.FundId,
                FundName = f.FundName,
                CurrencyCode = f.CurrencyCode,
                LaunchDate = f.LaunchDate
            });
        }

        public async Task<FundDto?> GetFundByIdAsync(int id)
        {
            var fund = await _repo.GetByIdAsync(id);
            if (fund == null) return null;
            return new FundDto
            {
                FundId = fund.FundId,
                FundName = fund.FundName,
                CurrencyCode = fund.CurrencyCode,
                LaunchDate = fund.LaunchDate
            };
        }

        public async Task<FundDto> CreateFundAsync(FundDto dto)
        {
            var entity = new Fund
            {
                FundName = dto.FundName,
                CurrencyCode = dto.CurrencyCode,
                LaunchDate = dto.LaunchDate
            };
            var result = await _repo.AddAsync(entity);
            dto.FundId = result.FundId;
            return dto;
        }

        public async Task<FundDto?> UpdateFundAsync(int id, FundDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.FundName = dto.FundName;
            existing.CurrencyCode = dto.CurrencyCode;
            existing.LaunchDate = dto.LaunchDate;

            await _repo.UpdateAsync(existing);
            return dto;
        }

        public async Task<bool> DeleteFundAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<FundSummaryDto>> GetFundSummariesAsync()
        {
            var results = await _repo.GetFundSummariesAsync();
            return results.Select(r => new FundSummaryDto
            {
                FundId = r.Fund.FundId,
                FundName = r.Fund.FundName,
                TotalSubscribed = r.Subscribed,
                TotalRedeemed = r.Redeemed,
                InvestorCount = r.InvestorCount
            });
        }
    }
}
