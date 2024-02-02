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

    public async Task<Data?> Get(string identifier, string field, DateTime? startDate, DateTime? endDate)
    {
        if (startDate is null)
        {
            startDate = defaultStartDate;
        }

        if (endDate is null)
        {
            endDate = defaultEndDate;
        }
        // find a way to return only data that is needed
        return await _dataCollection.Find(x => x.Id == identifier).FirstOrDefaultAsync();
    }
}