using ViewpointAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewpointAPI.Exceptions; 

namespace ViewpointAPI.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly IMongoCollection<History> _historyCollection;
        private DateTime defaultStartDate = DateTime.Today.AddYears(-1);
        private DateTime defaultEndDate = DateTime.Today;
        private readonly CustomException _customException;

        public HistoryRepository(
            IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings, CustomException customException)
        {
            var connectionString = SecurityDatabaseSettings.Value.ConnectionString;
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SecurityDatabaseSettings.Value.DatabaseName);

            _historyCollection = mongoDatabase.GetCollection<History>(
                SecurityDatabaseSettings.Value.HistoryCollectionName);

            _customException = customException ?? throw new ArgumentNullException(nameof(customException));

        }

        public async Task<List<History>> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate) 
        {
            startDate ??= defaultStartDate;
            endDate ??= defaultEndDate;
            
            var filterBuilder = Builders<History>.Filter;
            var filter = filterBuilder.Eq(x => x.Identifier, identifier) &
                        filterBuilder.Eq(x => x.Field, field) &
                        filterBuilder.Gte(x => x.Timestamp, startDate.Value) &
                        filterBuilder.Lte(x => x.Timestamp, endDate.Value);
                        
            var historyData = await _historyCollection.Find(filter).ToListAsync();

            if(historyData == null || historyData.Count == 0) 
            {
                throw new CustomException("History data not found for specified criteria");
            }

            return historyData;

        }
    }
}
