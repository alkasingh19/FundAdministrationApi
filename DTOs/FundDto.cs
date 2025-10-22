using System;
using System.ComponentModel.DataAnnotations;

namespace FundAdministrationApi.DTOs
{
    public class FundDto
    {
        public int FundId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FundName { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string CurrencyCode { get; set; } = string.Empty;

        [Required]
        public DateTime LaunchDate { get; set; }
    }

    public class FundSummaryDto
    {
        public int FundId { get; set; }
        public string FundName { get; set; } = string.Empty;
        public decimal TotalSubscribed { get; set; }
        public decimal TotalRedeemed { get; set; }
        public decimal NetInvestment => TotalSubscribed - TotalRedeemed;
        public int InvestorCount { get; set; }
    }
}
