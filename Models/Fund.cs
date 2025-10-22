using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundAdministrationApi.Models
{
    public class Fund
    {
        [Key]
        public int FundId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FundName { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string CurrencyCode { get; set; } = string.Empty;

        [Required]
        public DateTime LaunchDate { get; set; }

        public ICollection<Investor>? Investors { get; set; }
    }
}