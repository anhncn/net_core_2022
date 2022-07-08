using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace NNanh.Zolo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        // https://www.dotnetcoban.com/2019/09/redis-in-asp-dotnet-core.html
        private readonly IDistributedCache _distributedCache;

        public RedisController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public string Get()
        {
            var cacheKey = "TheTime";
            var currentTime = DateTime.Now.ToString();
            var cacheTime = _distributedCache.GetString(cacheKey);

            if (string.IsNullOrEmpty(cacheTime))
            {
                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(5));

                _distributedCache.SetString(cacheKey, currentTime, options);

                cacheTime = _distributedCache.GetString(cacheKey);
            }

            var result = $"Current Time : {currentTime} \n Cached Time: {cacheTime}";

            return result;
        }

    }
}
