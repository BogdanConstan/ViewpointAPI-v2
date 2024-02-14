using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using ViewpointAPI.Models;
using ViewpointAPI.Repositories;
using ViewpointAPI.Services;

namespace ViewpointAPITests
{
    [TestClass]
    public class HistoryServiceTests
    {
        [TestMethod]
        public async Task GetHistory_ReturnsExpectedHistory()
        {
            // Arrange
            var mockHistoryRepository = new Mock<IHistoryRepository>();
            var expectedHistoryResponse = new HistoryResponse
            {
                // Populate with expected data
            };
            mockHistoryRepository.Setup(repo => repo.GetHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                                 .ReturnsAsync(expectedHistoryResponse);

            var service = new HistoryService(mockHistoryRepository.Object);

            // Act
            var result = await service.GetHistory("identifier", "field", DateTime.Now.AddDays(-10), DateTime.Now);

            // Assert
            Assert.IsNotNull(result);
            // Add more detailed assertions based on the properties of HistoryResponse
            // For example, if HistoryResponse contains a property named Data, you might assert that it's not null or empty
            // Assert.AreEqual(expectedHistoryResponse.Data, result.Data);
        }
    }
}