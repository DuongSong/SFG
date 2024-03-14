using System;
using StackExchange.Redis;

namespace SFG.Core.Services.Interfaces
{
	public interface ICacheService
	{
		Task<string> GetCache(string key);
		Task<string> GetCache(string key, string field);
		Task<HashEntry[]> GetAllHash(string key);
        Task<bool> SetCache(string key, string value);
		Task SetCache(string key, HashEntry[] hashEntries);
        Task<bool> RefreshCache(string key);
        Task<bool> RemoveCache(string key);
	}
}

