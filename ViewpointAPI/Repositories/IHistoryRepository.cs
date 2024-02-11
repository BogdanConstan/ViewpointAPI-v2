using ViewpointAPI.Models;
namespace ViewpointAPI.Repositories
{
    public interface IHistoryRepository 
    {
        Task<HistoryResponse> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate);
    }
}
