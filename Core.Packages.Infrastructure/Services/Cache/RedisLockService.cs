using MagicCarRepairAISupported.Application.Common.Services.Cache;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace MagicCarRepairAISupported.Infrastructure.Services.Cache
{
   public class RedisLockService : IRedisLockService
{
    private readonly RedLockFactory _redLockFactory;

    public RedisLockService(string connectionString)
    {
        var redisMultiplexer = ConnectionMultiplexer.Connect(connectionString);
        _redLockFactory = RedLockFactory.Create(new List<RedLockMultiplexer> { redisMultiplexer });
    }

    /// <summary>
    /// Belirtilen anahtar i�in distributed lock al�r ve ger�ek sonucu d�nd�r�r.
    /// </summary>
    public async Task<TResponse> AcquireLockAsync<TResponse>(string resource, TimeSpan expiry, Func<Task<TResponse>> action)
    {
        using (var redLock = await _redLockFactory.CreateLockAsync(resource, expiry))
        {
            if (!redLock.IsAcquired) throw new Exception($"Lock al�namad�: {resource}");

            return await action();
        }
    }
}

}
