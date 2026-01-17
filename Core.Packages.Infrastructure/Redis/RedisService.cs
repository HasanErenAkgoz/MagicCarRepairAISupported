using MagicCarRepairAISupported.Application.Common.Services.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCarRepairAISupported.Infrastructure.Redis
{
    public class RedisService : IRedisCacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer? _connectionMultiplexer;
        private readonly ILogger<RedisService> _logger;

        public RedisService(
            IDistributedCache distributedCache,
            IConfiguration configuration,
            ILogger<RedisService> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;

            // Initialize ConnectionMultiplexer for pattern matching
            try
            {
                var connectionString = configuration["Redis:ConnectionString"];
                if (!string.IsNullOrEmpty(connectionString))
                {
                    _connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to initialize Redis ConnectionMultiplexer. Pattern matching will not work.");
            }
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            await _distributedCache.SetStringAsync(key, value);
        }

        public async Task<string?> GetCacheValueAsync(string key)
        {
            return await _distributedCache.GetStringAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task RemoveByPatternAsync(string pattern)
        {
            if (_connectionMultiplexer == null)
            {
                _logger.LogWarning("ConnectionMultiplexer not available. Cannot remove cache by pattern: {Pattern}", pattern);
                return;
            }

            try
            {
                var database = _connectionMultiplexer.GetDatabase();
                var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());

                var keys = new List<RedisKey>();
                await foreach (var key in server.KeysAsync(pattern: pattern))
                {
                    keys.Add(key);
                }

                if (keys.Any())
                {
                    await database.KeyDeleteAsync(keys.ToArray());
                    _logger.LogInformation("Removed {Count} cache keys matching pattern: {Pattern}", keys.Count, pattern);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cache by pattern: {Pattern}", pattern);
                throw;
            }
        }
    }
}
