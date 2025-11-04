using Microsoft.Extensions.Caching.Distributed;

namespace ByteBuy.Core.Caching.Abstractions;

public interface ICachingHelper
{
    Task<(bool Found, T? Value)> GetCachedObject<T>(string cacheKey, CancellationToken ct = default);
    Task CacheObject<T>(T obj, string cacheKey, DistributedCacheEntryOptions options, CancellationToken ct = default);
    Task InvalidateCache(string cacheKey, CancellationToken ct = default);
}
 