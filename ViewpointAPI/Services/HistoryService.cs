using System;
using System.Threading.Tasks;
using ViewpointAPI.Models;
using ViewpointAPI.Repositories;

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

        public async Task<HistoryResponse> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate)
        {
            // Add any additional business logic here if needed
            var globalIdentifier = await _idsService.GetGlobalIdentifier(identifier);
            return await _historyRepository.GetHistory(globalIdentifier, field, startDate, endDate);
        }
    }
}