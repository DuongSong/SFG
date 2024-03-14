using System;
using SFG.Core.Services.Interfaces;
using StackExchange.Redis;

namespace SFG.Core.Services.Implementations
{
    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public DateTime Expired { get; set; }
    }

	public class CacheService : ICacheService
	{
        private IDatabase redisCache;
        ConfigurationOptions options = new ConfigurationOptions
        {
            EndPoints = {
                {"localhost", 6379 }
            },
            Password = "123"
        };

        public CacheService()
		{
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(options);
            redisCache = redis.GetDatabase();
        }

        public async Task<HashEntry[]> GetAllHash(string key)
        {
            return await redisCache.HashGetAllAsync(key);
        }

        public async Task<string> GetCache(string key)
        {
            return await redisCache.StringGetAsync(key);
        }

        public async Task<string> GetCache(string key, string field)
        {
            return await redisCache.HashGetAsync(key, field);
        }

        public async Task<bool> RefreshCache(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCache(string key)
        {
            return await redisCache.KeyDeleteAsync(key);
        }

        public async Task<bool> SetCache(string key, string value)
        {
            return await redisCache.StringSetAsync(key, value);
        }
        public async Task SetCache(string key, HashEntry[] hashEntries)
        {
            await redisCache.HashSetAsync(key, hashEntries);
        }
    }
}

