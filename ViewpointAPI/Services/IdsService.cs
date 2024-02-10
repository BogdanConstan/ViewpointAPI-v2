using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViewpointAPI.Models;
using dotenv.net;


namespace ViewpointAPI.Services
{
    public class IdsService
    {
        private readonly IMongoCollection<History> _idsCollection;

        public IdsService(
            IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
        {

            var connectionString = SecurityDatabaseSettings.Value.ConnectionString;
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SecurityDatabaseSettings.Value.DatabaseName);

            _idsCollection = mongoDatabase.GetCollection<History>(
                SecurityDatabaseSettings.Value.IdsCollectionName);
        }

        public async Task<SecurityData?> Get(string identifier) 
        {
            throw new NotImplementedException();
            //Figure out what is needed from this query
        }

    }
}
