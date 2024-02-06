using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViewpointAPI.Models;

namespace ViewpointAPI.Services
{
    public class ReferenceService
    {
        private readonly IMongoCollection<Data> _referenceCollection;

        public ReferenceService(
            IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
        {
            
            var connectionString = SecurityDatabaseSettings.Value.ConnectionString;
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SecurityDatabaseSettings.Value.DatabaseName);

            _referenceCollection = mongoDatabase.GetCollection<Data>(
                SecurityDatabaseSettings.Value.ReferenceCollectionName);
        }

        public async Task<SecurityData?> Get(string id)
        {
            throw new NotImplementedException();
            //Figure out what is needed from this query
        }
    }
}
