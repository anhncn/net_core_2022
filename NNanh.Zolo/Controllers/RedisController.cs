using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers
{
    public class RedisController : ApiControllerBase
    {
        // https://www.dotnetcoban.com/2019/09/redis-in-asp-dotnet-core.html
        private readonly ICacheService _cacheService;

        public RedisController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var cacheKey = "TheTime";
            var currentTime = DateTime.Now.ToString();
            var cacheTime = await _cacheService.GetAsync<string>(cacheKey);

            if (string.IsNullOrEmpty(cacheTime))
            {
                double time = (double)10 / 60;
                await _cacheService.SetAsync(cacheKey, time, currentTime);

                cacheTime = await _cacheService.GetAsync<string>(cacheKey);
            }

            var result = $"Current Time : {currentTime} \n Cached Time: {cacheTime}";

            return result;
        }
    }
}
