using FundAdministrationApi.DTOs;
using FundAdministrationApi.Models;
using FundAdministrationApi.Repositories;

namespace FundAdministrationApi.Services
{
    public class InvestorService : IInvestorService
    {
        private readonly IInvestorRepository _repo;
        private readonly IFundRepository _fundRepo;

        public InvestorService(IInvestorRepository repo, IFundRepository fundRepo)
        {
            _repo = repo;
            _fundRepo = fundRepo;
        }

        public async Task<IEnumerable<InvestorDto>> GetAllInvestorsAsync()
        {
            var investors = await _repo.GetAllAsync();
            return investors.Select(i => new InvestorDto
            {
                InvestorId = i.InvestorId,
                FullName = i.FullName,
                Email = i.Email,
                FundId = i.FundId,
                FundName = i.Fund?.FundName
            });
        }

        public async Task<InvestorDto?> GetInvestorByIdAsync(int id)
        {
            var investor = await _repo.GetByIdAsync(id);
            if (investor == null) return null;

            return new InvestorDto
            {
                InvestorId = investor.InvestorId,
                FullName = investor.FullName,
                Email = investor.Email,
                FundId = investor.FundId,
                FundName = investor.Fund?.FundName
            };
        }

        public async Task<InvestorDto> CreateInvestorAsync(InvestorDto dto)
        {
            var entity = new Investor
            {
                FullName = dto.FullName,
                Email = dto.Email,
                FundId = dto.FundId
            };
            var result = await _repo.AddAsync(entity);
            dto.InvestorId = result.InvestorId;
            return dto;
        }

        public async Task<InvestorDto?> UpdateInvestorAsync(int id, InvestorDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.FullName = dto.FullName;
            existing.Email = dto.Email;
            existing.FundId = dto.FundId;

            await _repo.UpdateAsync(existing);
            return dto;
        }

        public async Task<bool> DeleteInvestorAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<InvestorTransactionSummaryDto?> GetInvestorTransactionsAsync(int investorId)
        {
            var investor = await _repo.GetByIdAsync(investorId);
            if (investor == null) return null;

            var transactions = await _repo.GetTransactionsByInvestorAsync(investorId);
            return new InvestorTransactionSummaryDto
            {
                InvestorId = investor.InvestorId,
                InvestorName = investor.FullName,
                Transactions = transactions.Select(t => new TransactionDto
                {
                    TransactionId = t.TransactionId,
                    InvestorId = t.InvestorId,
                    Type = t.Type,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate
                }).ToList()
            };
        }
    }
}
