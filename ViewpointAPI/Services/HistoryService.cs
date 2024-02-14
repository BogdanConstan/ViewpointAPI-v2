using System;
using System.Threading.Tasks;
using ViewpointAPI.Models;
using ViewpointAPI.Repositories;

namespace ViewpointAPI.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;


        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
        }

        public async Task<HistoryResponse> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate)
        {
            // Add any additional business logic here if needed
            return await _historyRepository.GetHistory(identifier, field, startDate, endDate);
        }
    }
}