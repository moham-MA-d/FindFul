using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace API.Helpers.HealthChecks
{
    public class RedisHealthCheck : IHealthCheck
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisHealthCheck(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var db = _connectionMultiplexer.GetDatabase();
                db.StringGet("a value that doesn't exist in Redis");
                return await Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (System.Exception ex)
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy(ex.Message));
            }   
        }
    }
}
