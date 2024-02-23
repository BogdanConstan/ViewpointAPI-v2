using ViewpointAPI.Models;
namespace ViewpointAPI.Repositories
{
    public interface IReferenceRepository 
    {
        Task<Reference> GetReference(string identifier, string field);
    }
}