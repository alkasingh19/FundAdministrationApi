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
    public class FundsController : ControllerBase
    {
        private readonly IFundService _service;

        public FundsController(IFundService service)
        {
            _service = service;
        }

        /// <summary>Gets all funds.</summary>
        [HttpGet]
        public async Task<IActionResult> GetFunds()
        {
            var funds = await _service.GetAllFundsAsync();
            return Ok(funds);
        }

        /// <summary>Gets a fund by ID.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFund(int id)
        {
            var fund = await _service.GetFundByIdAsync(id);
            if (fund == null) return NotFound();
            return Ok(fund);
        }

        /// <summary>Creates a new fund.</summary>
        [HttpPost]
        public async Task<IActionResult> CreateFund([FromBody] FundDto dto)
        {
            var fund = await _service.CreateFundAsync(dto);
            return CreatedAtAction(nameof(GetFund), new { id = fund.FundId }, fund);
        }

        /// <summary>Updates a fund.</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFund(int id, [FromBody] FundDto dto)
        {
            var result = await _service.UpdateFundAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>Deletes a fund.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFund(int id)
        {
            var success = await _service.DeleteFundAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
