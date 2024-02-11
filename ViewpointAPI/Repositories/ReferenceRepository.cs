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
    public class ReferenceRepository : IReferenceRepository
    {
        private readonly IMongoCollection<Reference> _referenceCollection;
        private readonly ICache _memoryCache;

        public ReferenceRepository(
            IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings, ICache memoryCache)
        {
            var connectionString = SecurityDatabaseSettings.Value.ConnectionString;
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SecurityDatabaseSettings.Value.DatabaseName);

            _referenceCollection = mongoDatabase.GetCollection<Reference>(
                SecurityDatabaseSettings.Value.ReferenceCollectionName);

            _memoryCache = memoryCache;
        }

        public async Task<ReferenceResponse> GetReference(string identifier, string field)
        {
            string globalIdentifier = await _memoryCache.GetOrSetAsync(identifier, async () =>
            {
                return identifier;
            });

            var filterBuilder = Builders<Reference>.Filter;
            var filter = filterBuilder.Eq(x => x.Identifier, globalIdentifier) &
                        filterBuilder.Eq(x => x.Field, field);

            var referenceData = await _referenceCollection.Find(filter).FirstOrDefaultAsync();

            var response = new ReferenceResponse
            {
                Identifier = identifier,
                Field = field,
                Value = referenceData?.Value
            };

            return response;
        }
    }
}
