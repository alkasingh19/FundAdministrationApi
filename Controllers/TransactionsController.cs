using FundAdministrationApi.DTOs;
using FundAdministrationApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundAdministrationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;
        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        /// <summary>Registers a new transaction (Subscription or Redemption).</summary>
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDto dto)
        {
            var transaction = await _service.CreateTransactionAsync(dto);
            return CreatedAtAction(nameof(CreateTransaction), new { id = transaction.TransactionId }, transaction);
        }
    }
}
