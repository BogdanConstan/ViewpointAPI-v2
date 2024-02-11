using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViewpointAPI.Models;

namespace ViewpointAPI.Repositories
{
    public interface IIdsRepository 
    {
        Task<string?> GetGlobalIdentifier(string identifier);
        Task<List<KeyValuePair<string, string>>> GetCacheInfo();


    }
}