using FundAdministrationApi.DTOs;

namespace FundAdministrationApi.Services
{
    public interface ITransactionService
    {
        Task<TransactionDto> CreateTransactionAsync(TransactionDto dto);
    }
}
