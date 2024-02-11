using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ViewpointAPI.Cache
{
    public interface ICache
    {
        Task<string> GetOrSetAsync(string key, Func<Task<string>> valueFactory);
    }

}
