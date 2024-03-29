using ViewpointAPI.Models;
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
        public ReferenceRepository(
            IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
        {
            var connectionString = SecurityDatabaseSettings.Value.ConnectionString;
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SecurityDatabaseSettings.Value.DatabaseName);

            _referenceCollection = mongoDatabase.GetCollection<Reference>(
                SecurityDatabaseSettings.Value.ReferenceCollectionName);
        }

        public async Task<string?> GetReference(string identifier, string field)
        {
            var filterBuilder = Builders<Reference>.Filter;
            var filter = filterBuilder.Eq(x => x.Identifier, identifier) &
                        filterBuilder.Eq(x => x.Field, field);

            var referenceData = await _referenceCollection.Find(filter).FirstOrDefaultAsync();

            return referenceData.Value;
        }
    }
}
