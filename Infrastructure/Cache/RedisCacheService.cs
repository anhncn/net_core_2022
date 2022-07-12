using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    /// <summary>
    /// -v mount volume --appendonly để dữ liệu lưu vào ổ cứng khi lấy ra
    /// docker run -d --name redis -p 6379:6379 -v D:\docker\elk\redis:/data redis --appendonly yes
    /// </summary>
    public class RedisCacheService : ICacheService
    {

        private readonly IDistributedCache _distributedCache;

        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<TEntity> GetAsync<TEntity>(string cacheKey)
        {
            var bytes = await _distributedCache.GetAsync(cacheKey);
            if (bytes == null)
            {
                return default;
            }
            return System.Text.Json.JsonSerializer.Deserialize<TEntity>(bytes);
        }

        public Task SetAsync<TEntity>(string cacheKey, double timeMinutes, TEntity entity)
        {
            byte[] bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(entity);
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(timeMinutes));

            return _distributedCache.SetAsync(cacheKey, bytes, options);
        }


    }
}
