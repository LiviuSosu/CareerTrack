using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.WebApi.HealthChecks
{
    public class RedisHealthCheck : IHealthCheck
    {
        public string RedisConnectionString { get; }

        public RedisHealthCheck(string redisConnectionString) 
        {
            RedisConnectionString = redisConnectionString;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisConnectionString);
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
            }

            return HealthCheckResult.Healthy();
        }
    }
}
