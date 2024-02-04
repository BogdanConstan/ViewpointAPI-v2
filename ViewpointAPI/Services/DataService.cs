using ViewpointAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ViewpointAPI.Services;

public class DataService
{
    private readonly IMongoCollection<Data> _dataCollection;
    private  DateTime defaultStartDate = DateTime.Today.AddYears(-1);
    private DateTime defaultEndDate = DateTime.Today;

    public DataService(
        IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            Environment.GetEnvironmentVariable("CONNECTIONSTRING"));

        var mongoDatabase = mongoClient.GetDatabase(
            SecurityDatabaseSettings.Value.DatabaseName);

        _dataCollection = mongoDatabase.GetCollection<Data>(
            SecurityDatabaseSettings.Value.DataCollectionName);
    }


    // Firgure out how to use abstract class 'SecurityData'
    public async Task<List<Data?>> Get(string identifier, string field, DateTime? startDate, DateTime? endDate)
    {
        if (startDate is null)
        {
            startDate = defaultStartDate;
        }

        if (endDate is null)
        {
            endDate = defaultEndDate;
        }

        var builder = Builders<Data>.Filter;
        var filter = builder.Eq(x => x.Identifier, identifier) & builder.Eq(x => x.Field, field) & builder.Gte(x => x.Timestamp, startDate.Value) & builder.Lte(x => x.Timestamp, endDate.Value);
        
        return await _dataCollection.Find(filter).ToListAsync();
    }
}