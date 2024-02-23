using ViewpointAPI.Models;
namespace ViewpointAPI.Repositories
{
    public interface IHistoryRepository 
    {
        Task<List<History>> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate);
    }
}
