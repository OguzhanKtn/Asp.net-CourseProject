
using Microsoft.Extensions.Caching.Memory;

namespace Udemy.Web.Caching
{
    public class InMemoryCacheService(IMemoryCache memoryCache) : ICacheService
    {
        public Task<T?> Get<T>(string key)
        {
          return Task.FromResult(memoryCache.Get<T>(key));
        }

        public Task<string> Get(string key)
        {
            return Task.FromResult(memoryCache.Get(key).ToString());
        }

        public Task Remove(string key)
        {
            memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task Set<T>(string key, T value)
        {
            memoryCache.Set<T>(key, value);
            return Task.CompletedTask;
        }
    }
}
