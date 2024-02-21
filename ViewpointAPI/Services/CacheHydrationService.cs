using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ViewpointAPI.Services
{
    public class CacheHydrationService : IHostedService
    {
        private readonly IIdsService _idsService;

        public CacheHydrationService(IIdsService idsService)
        {
            _idsService = idsService ?? throw new ArgumentNullException(nameof(idsService));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Call the PreloadCache method of the IdsService
            await _idsService.PreloadCache();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Perform any cleanup logic if needed
            return Task.CompletedTask;
        }
    }
}
