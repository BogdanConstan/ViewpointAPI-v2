using ViewpointAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ViewpointAPI.Services;

public class SecuritiesService
{
    private readonly IMongoCollection<Data> _securitiesCollection;

    public SecuritiesService(
        IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            SecurityDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            SecurityDatabaseSettings.Value.DatabaseName);

        _securitiesCollection = mongoDatabase.GetCollection<Data>(
            SecurityDatabaseSettings.Value.SecuritiesCollectionName);
    }

    public async Task<Data?> GetAsync(string id) =>
        await _securitiesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

}