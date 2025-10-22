using FundAdministrationApi.DTOs;
using FundAdministrationApi.Models;
using FundAdministrationApi.Repositories;

namespace FundAdministrationApi.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        private readonly IInvestorRepository _investorRepo;

        public TransactionService(ITransactionRepository repo, IInvestorRepository investorRepo)
        {
            _repo = repo;
            _investorRepo = investorRepo;
        }

        public async Task<TransactionDto> CreateTransactionAsync(TransactionDto dto)
        {
            var investor = await _investorRepo.GetByIdAsync(dto.InvestorId);
            if (investor == null)
                throw new ArgumentException("Invalid Investor ID");

            var transaction = new Transaction
            {
                InvestorId = dto.InvestorId,
                Type = dto.Type,
                Amount = dto.Amount,
                TransactionDate = dto.TransactionDate
            };

            var result = await _repo.AddAsync(transaction);
            dto.TransactionId = result.TransactionId;
            return dto;
        }
    }
}
