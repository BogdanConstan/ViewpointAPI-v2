using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ViewpointAPI.Models;
using ViewpointAPI.Repositories;
using ViewpointAPI.Exceptions;

namespace ViewpointAPI.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IIdsService _idsService;

        public HistoryService(IHistoryRepository historyRepository, IIdsService idsService)
        {
            _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
            _idsService = idsService ?? throw new ArgumentNullException(nameof(idsService));
        }

        public async Task<HistoryDataItems> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate)
        {
            var globalIdentifier = await _idsService.GetGlobalIdentifier(identifier);

            if (globalIdentifier == null) 
            {
                throw new IdNotFoundException("Local identifier not found");
            }

            return await _historyRepository.GetHistory(globalIdentifier, field, startDate, endDate);

        }
    }
}