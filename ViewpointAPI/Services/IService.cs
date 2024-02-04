using ViewpointAPI.Models;

namespace ViewpointAPI.Services
{
    public interface IService
    {
        // not too sure how to implement this interface since the get methods for the three classes are different
        // as in they each take different arguments
        Task<SecurityData?> Get(string identifier, string field, DateTime? startDate, DateTime? endDate);
    }
}
