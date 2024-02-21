using ViewpointAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace ViewpointAPI.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly IMongoCollection<History> _historyCollection;
        private DateTime defaultStartDate = DateTime.Today.AddYears(-1);
        private DateTime defaultEndDate = DateTime.Today;

        public HistoryRepository(
            IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
        {
            var connectionString = SecurityDatabaseSettings.Value.ConnectionString;
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SecurityDatabaseSettings.Value.DatabaseName);

            _historyCollection = mongoDatabase.GetCollection<History>(
                SecurityDatabaseSettings.Value.HistoryCollectionName);
        }

        public async Task<HistoryResponse> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate) 
        {
            startDate ??= defaultStartDate;
            endDate ??= defaultEndDate;
            
            var filterBuilder = Builders<History>.Filter;
            var filter = filterBuilder.Eq(x => x.Identifier, identifier) &
                        filterBuilder.Eq(x => x.Field, field) &
                        filterBuilder.Gte(x => x.Timestamp, startDate.Value) &
                        filterBuilder.Lte(x => x.Timestamp, endDate.Value);

            var historyData = await _historyCollection.Find(filter).ToListAsync();

            var response = new HistoryResponse
            {
                Identifier = identifier,
                Field = field,
                Count = historyData.Count,
                Data = historyData.Select(x => new HistoryDataItem
                {
                    Timestamp = x.Timestamp,
                    Value = x.Value
                }).ToList()
            };

            return response;
        }
    }
}
