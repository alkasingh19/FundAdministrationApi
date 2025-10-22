using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace FundAdministrationApi.Models
{
    public class Investor
    {
        [Key]
        public int InvestorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [ForeignKey("Fund")]
        public int FundId { get; set; }

        public Fund? Fund { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }
    }
}