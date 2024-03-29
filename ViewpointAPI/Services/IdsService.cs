using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ViewpointAPI.Models;
using ViewpointAPI.Repositories;

namespace ViewpointAPI.Services
{
    public class IdsService : IIdsService
    {
        private readonly IIdsRepository _idsRepository;
        private readonly IMemoryCache _cache;
        private readonly SemaphoreSlim _cacheLock = new SemaphoreSlim(1);

        public IdsService(IIdsRepository idsRepository, IMemoryCache memoryCache)
        {
            _idsRepository = idsRepository ?? throw new ArgumentNullException(nameof(idsRepository));
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<string?> GetGlobalIdentifier(string identifier)
        {
            identifier = identifier.Trim().ToUpper();

            string? globalIdentifier;

            if (!_cache.TryGetValue(identifier, out globalIdentifier))
            {
                await _cacheLock.WaitAsync();
                try
                {
                    if (!_cache.TryGetValue(identifier, out globalIdentifier))
                    {
                        globalIdentifier = await _idsRepository.GetGlobalIdentifier(identifier);
                        SetNewCacheEntry(identifier, globalIdentifier);
                    }
                }
                finally
                {
                    _cacheLock.Release(); // Release the lock
                }
            }
            return globalIdentifier;
        }

        private async Task SetNewCacheEntry(string identifier, string globalIdentifier) 
        {
            if (globalIdentifier == null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                _cache.Set(identifier, globalIdentifier, cacheEntryOptions);
            }
            else 
            {
                _cache.Set(identifier, globalIdentifier);
            }
        }

        public async Task PreloadCache()
        {
            var cacheInfo = await _idsRepository.GetAllIDs();

            foreach (var pair in cacheInfo)
            {
                _cache.Set(pair.Key, pair.Value);
            }
        }
    }
}