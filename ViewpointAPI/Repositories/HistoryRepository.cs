using ViewpointAPI.Models;
using ViewpointAPI.Cache;
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

        private readonly ICache _memoryCache;

        public HistoryRepository(
            IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings, ICache memoryCache)
        {
            var connectionString = SecurityDatabaseSettings.Value.ConnectionString;
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SecurityDatabaseSettings.Value.DatabaseName);

            _historyCollection = mongoDatabase.GetCollection<History>(
                SecurityDatabaseSettings.Value.HistoryCollectionName);

            _memoryCache = memoryCache;
        }

        public async Task<HistoryResponse> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate) 
        {
            startDate ??= defaultStartDate;
            endDate ??= defaultEndDate;
            
            string globalIdentifier = await _memoryCache.GetOrSetAsync(identifier, async () =>
            {
                return identifier;
            });

            var filterBuilder = Builders<History>.Filter;
            var filter = filterBuilder.Eq(x => x.Identifier, globalIdentifier) &
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
