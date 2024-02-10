using ViewpointAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ViewpointAPI.Services;

public class HistoryService
{
    private readonly IMongoCollection<History> _dataCollection;
    private  DateTime defaultStartDate = DateTime.Today.AddYears(-1);
    private DateTime defaultEndDate = DateTime.Today;

    public HistoryService(
        IOptions<SecurityDatabaseSettings> SecurityDatabaseSettings)
    {

        var connectionString = SecurityDatabaseSettings.Value.ConnectionString;
        var mongoClient = new MongoClient(connectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            SecurityDatabaseSettings.Value.DatabaseName);

        _dataCollection = mongoDatabase.GetCollection<History>(
            SecurityDatabaseSettings.Value.DataCollectionName);
    }


    // Firgure out how to use abstract class 'SecurityData'
    public async Task<List<History?>> Get(string identifier, string field, DateTime? startDate, DateTime? endDate)
    {
        /** Queries from database given the identifier, field, startDate and endDate. If startDate or endDate are not provided,
         * default values are used.**/
        if (startDate is null)
        {
            startDate = defaultStartDate;
        }

        if (endDate is null)
        {
            endDate = defaultEndDate;
        }

        var builder = Builders<History>.Filter;
        var filter = builder.Eq(x => x.Identifier, identifier) & builder.Eq(x => x.Field, field) & builder.Gte(x => x.Timestamp, startDate.Value) & builder.Lte(x => x.Timestamp, endDate.Value);
        
        return await _dataCollection.Find(filter).ToListAsync();
    }
}