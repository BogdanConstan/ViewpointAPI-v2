using ViewpointAPI.Models;

namespace ViewpointAPI.Services
{
    public interface IHistoryService
    {
        Task<HistoryResponse> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate);
    }
}