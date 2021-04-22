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
            //    var healthCheckResultHealthy = true;

            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            }
            catch (Exception ex)
            {
                // Discard PingExceptions and return false;
                return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
            }

            return HealthCheckResult.Healthy();
        }
    }
}
