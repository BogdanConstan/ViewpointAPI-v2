using ViewpointAPI.Models;

namespace ViewpointAPI.Services
{
    public interface IService
    {
        // not too sure how to implement this interface since the get methods for the three classes are different
        Task<SecurityData?> Get(string identifier, string field, DateTime? startDate, DateTime? endDate);
    }
}
