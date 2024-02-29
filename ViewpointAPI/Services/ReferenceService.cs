using System;
using System.Threading.Tasks;
using ViewpointAPI.Repositories;
using ViewpointAPI.Exceptions;

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

        public async Task<string?> GetReference(string identifier, string field)
        {
            var globalIdentifier = await _idsService.GetGlobalIdentifier(identifier);

            if (globalIdentifier == null) 
            {
                throw new IdNotFoundException("Local Identifier not found");
            }

            return await _referenceRepository.GetReference(globalIdentifier, field);

        }
    }
}
