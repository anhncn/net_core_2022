using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
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

        [HttpGet("person")]
        public async Task<Person> GetPerson()
        {
            var cacheKey = "TheTimePerson";
            var person = await _cacheService.GetAsync<Person>(cacheKey);

            if (person == null)
            {
                double time = (double)10 / 60;
                await _cacheService.SetAsync(cacheKey, time, new Person() { Name = DateTime.Now.ToString() });

                person = await _cacheService.GetAsync<Person>(cacheKey);
            }

            return person;
        }

    }

    public class Person
    {
        public string Name { get; set; }
    }
}
