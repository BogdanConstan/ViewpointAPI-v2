using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViewpointAPI.Models;

namespace ViewpointAPI.Services
{
    public class ReferenceService
    {
        private readonly IMongoCollection<Reference> _referenceCollection;

        public ReferenceService(
            IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
        {
            
            var connectionString = SecurityDatabaseSettings.Value.ConnectionString;
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SecurityDatabaseSettings.Value.DatabaseName);

            _referenceCollection = mongoDatabase.GetCollection<Reference>(
                SecurityDatabaseSettings.Value.ReferenceCollectionName);
        }

        public IMongoCollection<Reference> Get_referenceCollection()
        {
            return _referenceCollection;
        }

        public async Task<List<Reference?>> Get(string identifier, string field)
        {
            var builder = Builders<Reference>.Filter;
            var filter = builder.Eq(x => x.Identifier, identifier) & builder.Eq(x => x.Field, field);
            return await _referenceCollection.Find(filter).ToListAsync();
        }
    }
}
