using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    public class MemCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemCacheService(IMemoryCache memoryCache) => _memoryCache = memoryCache;

        public Task<TEntity> GetAsync<TEntity>(string cacheKey)
        {
            var entity = _memoryCache.Get<TEntity>(cacheKey);

            return Task.FromResult(entity);
        }

        public Task SetAsync<TEntity>(string cacheKey, int times, TEntity entity)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(times));

            _memoryCache.Set(cacheKey, entity, cacheEntryOptions);

            return Task.CompletedTask;
        }
    }
}
