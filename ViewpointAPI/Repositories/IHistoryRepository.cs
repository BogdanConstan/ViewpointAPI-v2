using ViewpointAPI.Models;
namespace ViewpointAPI.Repositories
{
    public interface IHistoryRepository 
    {
        Task<HistoryDataItems> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate);
    }
}
