using Infrastruture.Worker.Constraint;
using Infrastruture.Worker.Interfaces;
using Infrastruture.Worker.Setting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastruture.Worker.Services
{
    public class RedisService : IRedisService
    {
        private readonly RedisSetting _redisSetting;
        private readonly IDistributedCache _cache;
        public RedisService(IDistributedCache cache, IOptions<RedisSetting> redisSetting)
        {
            _cache = cache;
            _redisSetting = redisSetting.Value;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _cache.GetStringAsync(key);

            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public async Task<T> SetAsync<T>(string key, T value, int expirationTime, string unit)
        {
            DistributedCacheEntryOptions timeOut = new DistributedCacheEntryOptions();

            if (unit == ExpirationTimeUnit.Days)
                timeOut.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(expirationTime);

            if (unit == ExpirationTimeUnit.Hours)
                timeOut.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(expirationTime);

            if (unit == ExpirationTimeUnit.Minutes)
                timeOut.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationTime);

            if (unit == ExpirationTimeUnit.Seconds)
                timeOut.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirationTime);

            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value), timeOut);

            return value;
        }
    }
}
