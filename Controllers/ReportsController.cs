using FundAdministrationApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundAdministrationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IFundService _fundService;

        public ReportsController(IFundService fundService)
        {
            _fundService = fundService;
        }

        /// <summary>
        /// Returns net investment and investor count per fund.
        /// </summary>
        [HttpGet("net-investments")]
        public async Task<IActionResult> GetNetInvestments()
        {
            var report = await _fundService.GetFundSummariesAsync();
            return Ok(report);
        }
    }
}
