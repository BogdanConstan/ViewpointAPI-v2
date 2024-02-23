using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViewpointAPI.Models;

namespace ViewpointAPI.Repositories
{
    public interface IIdsRepository 
    {
        Task<string?> GetGlobalIdentifier(string identifier);
        Task<Dictionary<string, string>> GetAllIDs();
    }
}