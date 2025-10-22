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
    public class InvestorsController : ControllerBase
    {
        private readonly IInvestorService _service;

        public InvestorsController(IInvestorService service)
        {
            _service = service;
        }

        /// <summary>Gets all investors.</summary>
        [HttpGet]
        public async Task<IActionResult> GetInvestors()
        {
            var investors = await _service.GetAllInvestorsAsync();
            return Ok(investors);
        }

        /// <summary>Gets investor by ID.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvestor(int id)
        {
            var investor = await _service.GetInvestorByIdAsync(id);
            if (investor == null) return NotFound();
            return Ok(investor);
        }

        /// <summary>Creates a new investor.</summary>
        [HttpPost]
        public async Task<IActionResult> CreateInvestor([FromBody] InvestorDto dto)
        {
            var investor = await _service.CreateInvestorAsync(dto);
            return CreatedAtAction(nameof(GetInvestor), new { id = investor.InvestorId }, investor);
        }

        /// <summary>Updates an investor.</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvestor(int id, [FromBody] InvestorDto dto)
        {
            var updated = await _service.UpdateInvestorAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>Deletes an investor.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestor(int id)
        {
            var success = await _service.DeleteInvestorAsync(id);
            return success ? NoContent() : NotFound();
        }

        /// <summary>Gets all transactions for a specific investor.</summary>
        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetInvestorTransactions(int id)
        {
            var summary = await _service.GetInvestorTransactionsAsync(id);
            if (summary == null) return NotFound();
            return Ok(summary);
        }
    }
}
