using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace API.Services.Classes
{
    public class RedisSubscriber : BackgroundService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisSubscriber(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();

            return subscriber.SubscribeAsync("messages", (channel, value) =>
            {
                System.Console.WriteLine($"The message content is : {value}");
            });
        }
    }
}
