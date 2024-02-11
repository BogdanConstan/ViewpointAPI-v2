using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViewpointAPI.Models;

namespace ViewpointAPI.Repositories
{
    public class IdsRepository : IIdsRepository
    {
        private readonly IMongoCollection<Ids> _idsCollection;

        public IdsRepository(IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
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
        public async Task<List<KeyValuePair<string, string>>> GetCacheInfo()
        {
            var cacheInfo = await _idsCollection.Find(_ => true)
                                                    .Project(x => new KeyValuePair<string, string>(x.Identifier, x.Id_bb_global))
                                                    .ToListAsync();
            return cacheInfo;
        }

    }
}
