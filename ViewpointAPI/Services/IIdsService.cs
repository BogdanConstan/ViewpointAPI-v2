
namespace ViewpointAPI.Services
{
    public interface IIdsService
    {
        Task<string?> GetGlobalIdentifier(string identifier);
        Task PreloadCache();
    }
}