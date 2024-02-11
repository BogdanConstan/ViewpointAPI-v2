using System;
using System.Threading.Tasks;
using ViewpointAPI.Models;
using ViewpointAPI.Repositories;

namespace ViewpointAPI.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly IReferenceRepository _referenceRepository;


        public ReferenceService(IReferenceRepository referenceRepository)
        {
            _referenceRepository = referenceRepository ?? throw new ArgumentNullException(nameof(referenceRepository));
        }

        public async Task<ReferenceResponse> GetReference(string identifier, string field)
        {
            // Add any additional business logic here if needed
            return await _referenceRepository.GetReference(identifier, field);
        }
    }
}
