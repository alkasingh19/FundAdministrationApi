using System.ComponentModel.DataAnnotations;

namespace FundAdministrationApi.DTOs
{
    public class InvestorDto
    {
        public int InvestorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int FundId { get; set; }

        public string? FundName { get; set; }
    }
}
