using ViewpointAPI.Models;

namespace ViewpointAPI.Services
{
    public interface IReferenceService
    {
        Task<ReferenceResponse> GetReference(string identifier, string field);
    }
}