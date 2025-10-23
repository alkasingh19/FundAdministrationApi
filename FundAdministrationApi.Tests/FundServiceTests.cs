using FundAdministrationApi.DTOs;
using FundAdministrationApi.Models;
using FundAdministrationApi.Repositories;
using FundAdministrationApi.Services;
using Moq;
using Xunit;

namespace FundAdministrationApi.Tests
{
    public class FundServiceTests
    {
        private readonly Mock<IFundRepository> _fundRepoMock;
        private readonly FundService _fundService;

        public FundServiceTests()
        {
            _fundRepoMock = new Mock<IFundRepository>();
            _fundService = new FundService(_fundRepoMock.Object);
        }

        [Fact]
        public async Task GetAllFundsAsync_ReturnsListOfFunds()
        {
            // Arrange
            var funds = new List<Fund>
            {
                new Fund { FundId = 1, FundName = "Test Fund A", CurrencyCode = "USD", LaunchDate = DateTime.UtcNow },
                new Fund { FundId = 2, FundName = "Test Fund B", CurrencyCode = "EUR", LaunchDate = DateTime.UtcNow }
            };

            _fundRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(funds);

            // Act
            var result = await _fundService.GetAllFundsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, f => f.FundName == "Test Fund A");
        }

        [Fact]
        public async Task GetFundByIdAsync_ReturnsFund_WhenExists()
        {
            // Arrange
            var fund = new Fund { FundId = 1, FundName = "Sample Fund", CurrencyCode = "USD", LaunchDate = DateTime.UtcNow };
            _fundRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fund);

            // Act
            var result = await _fundService.GetFundByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Sample Fund", result!.FundName);
        }

        [Fact]
        public async Task CreateFundAsync_AddsNewFund()
        {
            // Arrange
            var dto = new FundDto { FundName = "New Fund", CurrencyCode = "USD", LaunchDate = DateTime.UtcNow };
            var entity = new Fund { FundId = 10, FundName = "New Fund", CurrencyCode = "USD", LaunchDate = dto.LaunchDate };

            _fundRepoMock.Setup(r => r.AddAsync(It.IsAny<Fund>())).ReturnsAsync(entity);

            // Act
            var result = await _fundService.CreateFundAsync(dto);

            // Assert
            Assert.Equal(10, result.FundId);
            _fundRepoMock.Verify(r => r.AddAsync(It.IsAny<Fund>()), Times.Once);
        }
    }
}
