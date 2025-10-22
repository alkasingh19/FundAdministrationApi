using System;
using System.ComponentModel.DataAnnotations;
using FundAdministrationApi.Models;

namespace FundAdministrationApi.DTOs
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }

        [Required]
        public int InvestorId { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive.")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }
    }

    public class InvestorTransactionSummaryDto
    {
        public int InvestorId { get; set; }
        public string InvestorName { get; set; } = string.Empty;
        public List<TransactionDto> Transactions { get; set; } = new();
    }
}
