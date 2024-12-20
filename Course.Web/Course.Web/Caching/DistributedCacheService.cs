using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Udemy.Web.Caching
{
    public class DistributedCacheService(IDistributedCache distributedCache) : ICacheService
    {
        public async Task<T?> Get<T>(string key)
        {
           var valueAsJsonString = await distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(valueAsJsonString))
            {
                return default;
            }

            var value = JsonSerializer.Deserialize<T>(valueAsJsonString);
            return value;
        }

        public async Task Remove(string key)
        {
            await distributedCache.RemoveAsync(key);
        }

        public async Task Set<T>(string key, T value)
        {
            var valueAsJsonString = JsonSerializer.Serialize(value);

            await distributedCache.SetStringAsync(key, valueAsJsonString); 
        }
    }
}
