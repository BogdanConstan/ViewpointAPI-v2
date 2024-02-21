using System;
using System.Threading.Tasks;
using ViewpointAPI.Models;
using ViewpointAPI.Repositories;

namespace ViewpointAPI.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly IReferenceRepository _referenceRepository;
        private readonly IIdsService _idsService;
        public ReferenceService(IReferenceRepository referenceRepository, IIdsService idsService)
        {
            _referenceRepository = referenceRepository ?? throw new ArgumentNullException(nameof(referenceRepository));
            _idsService = idsService ?? throw new ArgumentNullException(nameof(idsService));
        }

        public async Task<ReferenceResponse> GetReference(string identifier, string field)
        {
            // Add any additional business logic here if needed
            var globalIdentifier = await _idsService.GetGlobalIdentifier(identifier);
            return await _referenceRepository.GetReference(globalIdentifier, field);
        }
    }
}
