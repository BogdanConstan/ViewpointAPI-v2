using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ViewpointAPI.Repositories;

namespace ViewpointAPI.Cache
{
    public class Cache : ICache
    {
        private readonly IMemoryCache _cache;
        private readonly IIdsRepository _idsRepository;
        private readonly TimeSpan _updateInterval = TimeSpan.FromHours(1); // Update interval set to 1 hour
        private Timer _timer;

        public Cache(IMemoryCache memoryCache, IIdsRepository idsRepository)
        {
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _idsRepository = idsRepository ?? throw new ArgumentNullException(nameof(idsRepository));

            // Preload values into cache during initialization
            PreloadCache().Wait(); // Wait synchronously for the preload operation

            // Set up a timer to update the cache periodically
            _timer = new Timer(UpdateCache, null, _updateInterval, _updateInterval);
        }

        public async Task<string> GetOrSetAsync(string key, Func<Task<string>> valueFactory)
        {
            if (!_cache.TryGetValue(key, out string result))
            {
                result = await valueFactory();

                if (result != null)
                {
                    _cache.Set(key, result);
                }
            }

            return result;
        }

        private async Task PreloadCache()
        {
            var cacheInfo = await _idsRepository.GetCacheInfo();

            foreach (var pair in cacheInfo)
            {
                _cache.Set(pair.Key, pair.Value);
            }
        }

        private async void UpdateCache(object state)
        {
            var cacheInfo = await _idsRepository.GetCacheInfo();

            foreach (var pair in cacheInfo)
            {
                _cache.Set(pair.Key, pair.Value);
            }
        }
    }
}
