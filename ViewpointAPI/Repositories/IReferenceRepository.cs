using ViewpointAPI.Models;
namespace ViewpointAPI.Repositories
{
    public interface IReferenceRepository 
    {
        Task<ReferenceResponse> GetReference(string identifier, string field);
    }
}