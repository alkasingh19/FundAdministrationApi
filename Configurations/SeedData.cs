using FundAdministrationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FundAdministrationApi.Configuration
{
    public static class SeedData
    {
        public static void Initialize(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (context.Funds.Any()) return; // Already seeded

            var fund1 = new Fund { FundName = "Global Ventures Fund", CurrencyCode = "USD", LaunchDate = DateTime.UtcNow.AddYears(-5) };
            var fund2 = new Fund { FundName = "European Equity Fund", CurrencyCode = "EUR", LaunchDate = DateTime.UtcNow.AddYears(-3) };
            var fund3 = new Fund { FundName = "Emerging Markets Fund", CurrencyCode = "USD", LaunchDate = DateTime.UtcNow.AddYears(-2) };

            context.Funds.AddRange(fund1, fund2, fund3);
            context.SaveChanges();

            var investor1 = new Investor { FullName = "Alka Singh", Email = "alka@example.com", FundId = fund1.FundId };
            var investor2 = new Investor { FullName = "Bob Smith", Email = "bob@example.com", FundId = fund2.FundId };
            var investor3 = new Investor { FullName = "Sean Brown", Email = "sean@example.com", FundId = fund1.FundId };

            context.Investors.AddRange(investor1, investor2, investor3);
            context.SaveChanges();

            context.Transactions.AddRange(
                new Transaction { InvestorId = investor1.InvestorId, Type = TransactionType.Subscription, Amount = 10000, TransactionDate = DateTime.UtcNow.AddDays(-30) },
                new Transaction { InvestorId = investor1.InvestorId, Type = TransactionType.Redemption, Amount = 2000, TransactionDate = DateTime.UtcNow.AddDays(-5) },
                new Transaction { InvestorId = investor2.InvestorId, Type = TransactionType.Subscription, Amount = 5000, TransactionDate = DateTime.UtcNow.AddDays(-10) }
            );

            context.SaveChanges();
        }
    }
}
