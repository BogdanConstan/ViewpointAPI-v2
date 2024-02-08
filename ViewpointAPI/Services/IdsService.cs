using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViewpointAPI.Models;
//using dotenv.net;


namespace ViewpointAPI.Services
{
    public class IdsService
    {
        private readonly IMongoCollection<Ids> _idsCollection;

        public IdsService(IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
        {

            var connectionString = SecurityDatabaseSettings.Value.ConnectionString;
            var mongoClient = new MongoClient(connectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SecurityDatabaseSettings.Value.DatabaseName);

            _idsCollection = mongoDatabase.GetCollection<Ids>(
                SecurityDatabaseSettings.Value.IdsCollectionName);
        }

        public async Task<string?> GetGlobalIdentifier(string identifier)
        {
            var id = await _idsCollection.Find(x => x.Identifier == identifier).FirstOrDefaultAsync();
            return id?.Id_bb_global;
        }

    }
}
