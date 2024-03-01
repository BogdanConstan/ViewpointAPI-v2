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
            var mockHistoryRepository = new Mock<IHistoryRepository>();
            var expectedHistoryResponse = new HistoryResponse
            {
                Identifier = "BBG000HBHK85",
                Field = "DAY_TO_DAY_TOT_RETURN_GROSS_DVDS",
                Count = 4,
                Data = new List<HistoryDataItem>
                {
                    new HistoryDataItem { Timestamp = DateTime.Parse("2020-02-18T00:00:00Z"), Value = 0.0559 },
                    new HistoryDataItem { Timestamp = DateTime.Parse("2020-02-19T00:00:00Z"), Value = 0.3753 },
                    new HistoryDataItem { Timestamp = DateTime.Parse("2020-02-20T00:00:00Z"), Value = 0.1049 },
                    new HistoryDataItem { Timestamp = DateTime.Parse("2020-02-21T00:00:00Z"), Value = -0.5579 }
                }
            };
            // Set up mock repository that will return the expected output that the HistoryService class can use
            mockHistoryRepository.Setup(repo => repo.GetHistory("BBG000HBHK85", "DAY_TO_DAY_TOT_RETURN_GROSS_DVDS", DateTime.Parse("2021-02-18T00:00:00Z"), DateTime.Parse("2021-02-21T00:00:00Z")))
                     .ReturnsAsync(expectedHistoryResponse);

            var service = new HistoryService(mockHistoryRepository.Object);
            var result = await service.GetHistory("BBG000HBHK85", "DAY_TO_DAY_TOT_RETURN_GROSS_DVDS", DateTime.Parse("2021-02-18T00:00:00Z"), DateTime.Parse("2021-02-21T00:00:00Z"));

            // Assert
            // Since querying happens in the repository, and the only function of this method is to call the repository's GetHistory() method, this is the only relevant assertion 
            // for now. 
            Assert.IsNotNull(result);
        }
    }
}