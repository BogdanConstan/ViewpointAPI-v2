using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViewpointAPI.Models;
using dotenv.net;


namespace ViewpointAPI.Services
{
    public class IdsService
    {
        private readonly IMongoCollection<Data> _idsCollection;

        public IdsService(
            IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                Environment.GetEnvironmentVariable("CONNECTIONSTRING"));

            var mongoDatabase = mongoClient.GetDatabase(
                SecurityDatabaseSettings.Value.DatabaseName);

            _idsCollection = mongoDatabase.GetCollection<Data>(
                SecurityDatabaseSettings.Value.IdsCollectionName);
        }

        public async Task<SecurityData?> Get(string identifier) 
        {
            throw new NotImplementedException();
            //Figure out what is needed from this query
        }

    }
}
