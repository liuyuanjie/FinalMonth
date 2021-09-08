using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using StackExchange.Redis;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _redis;

        public CacheController(IMemoryCache memoryCache, IDistributedCache redis)
        {
            _memoryCache = memoryCache;
            _redis = redis;
        }

        [HttpPost]
        [Route("set")]
        public async Task<IActionResult> SetValue(string key, string value)
        {
            _memoryCache.Set(key, value);
            await _redis.SetAsync(key, System.Text.Encoding.Default.GetBytes(value), new DistributedCacheEntryOptions());

            return Ok();
        }

        [HttpGet]
        [Route("")]
        public async Task<string> GetValue(string key, string value)
        {
            _memoryCache.TryGetValue(key, out string cacheValue);
            var redisCacheValue = await _redis.GetStringAsync(key);

            return $"{cacheValue};{redisCacheValue}";
        }
    }
}
