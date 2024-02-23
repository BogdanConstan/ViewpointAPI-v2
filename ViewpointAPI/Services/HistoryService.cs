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

        public async Task<HistoryResponse> GetHistory(string identifier, string field, DateTime? startDate, DateTime? endDate)
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

            var historyData =  await _historyRepository.GetHistory(globalIdentifier, field, startDate, endDate);

            var historyDataItems = historyData.Select(x => new HistoryDataItem
            {
                Timestamp = x.Timestamp,
                Value = x.Value
            }).ToList();

            var response = new HistoryResponse
            {
                Identifier = identifier,
                Field = field,
                Data = historyDataItems
            };

            return response;
        }
    }
}