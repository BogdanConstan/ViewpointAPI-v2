using ViewpointAPI.Models;
namespace ViewpointAPI.Repositories
{
    public interface IReferenceRepository 
    {
        Task<string?> GetReference(string identifier, string field);
    }
}