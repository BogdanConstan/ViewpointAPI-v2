using ViewpointAPI.Models;

namespace ViewpointAPI.Services
{
    public interface IReferenceService
    {
        Task<string?> GetReference(string identifier, string field);
    }
}