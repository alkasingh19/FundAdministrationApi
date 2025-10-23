using FundAdministrationApi.Configuration;
using FundAdministrationApi.Models;
using FundAdministrationApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FundAdministrationApi.Tests
{
    public class FundRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly FundRepository _repository;

        public FundRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "FundRepoTestDb")
                .Options;

            _context = new AppDbContext(options);
            _repository = new FundRepository(_context);

            // Seed test data
            _context.Funds.AddRange(
                new Fund { FundId = 1, FundName = "Alpha Fund", CurrencyCode = "USD", LaunchDate = DateTime.UtcNow },
                new Fund { FundId = 2, FundName = "Beta Fund", CurrencyCode = "EUR", LaunchDate = DateTime.UtcNow }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllFunds()
        {
            // Act
            var funds = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(2, funds.Count());
        }

        [Fact]
        public async Task AddAsync_AddsFundSuccessfully()
        {
            // Arrange
            var fund = new Fund { FundName = "Alka Fund", CurrencyCode = "INR", LaunchDate = DateTime.UtcNow };

            // Act
            var result = await _repository.AddAsync(fund);

            // Assert
            Assert.True(result.FundId > 0);
            
        }

        [Fact]
        public async Task DeleteAsync_RemovesFund()
        {
            // Act
            var success = await _repository.DeleteAsync(1);

            // Assert
            Assert.True(success);
            Assert.Equal(1, _context.Funds.Count());
        }
    }
}
