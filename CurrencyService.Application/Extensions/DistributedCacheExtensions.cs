using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyService.Application.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);

            options.SlidingExpiration = unusedExpireTime;

            var jsonData = JsonSerializer.Serialize(data);

            try
            {
                await cache.SetStringAsync(recordId, jsonData, options);
            }
            catch (RedisConnectionException ex)
            {
            }
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            string jsonData;

            try
            {
                jsonData = await cache.GetStringAsync(recordId);
            }
            catch (RedisConnectionException ex)
            {
                return default(T);
            }

            if (jsonData is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
