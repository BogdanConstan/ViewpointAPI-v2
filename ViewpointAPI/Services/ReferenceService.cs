using System;
using System.Threading.Tasks;
using ViewpointAPI.Models;
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

        public async Task<ReferenceResponse> GetReference(string identifier, string field)
        {
            // Add any additional business logic here if needed
            var globalIdentifier = await _idsService.GetGlobalIdentifier(identifier);

            if (globalIdentifier == null) 
            {
                throw new CustomException("Local Identifier not found in database");
            }

            else if (globalIdentifier == "null")
            {
                throw new CustomException("This local identifier was already queried in the last 24 hours and is not present in the database");
            }

            var referenceData = await _referenceRepository.GetReference(globalIdentifier, field);

            var response = new ReferenceResponse
            {
                Identifier = identifier,
                Field = field,
                Value = referenceData?.Value
            };

            return response;
        }
    }
}
