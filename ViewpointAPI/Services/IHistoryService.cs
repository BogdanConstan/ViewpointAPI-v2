using ViewpointAPI.Models;

namespace ViewpointAPI.Services
{
    public interface IHistoryService
    {
        Task<HistoryDataItems> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate);
    }
}