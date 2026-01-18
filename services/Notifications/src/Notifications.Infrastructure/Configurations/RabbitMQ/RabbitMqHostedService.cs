
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Notifications.Infrastructure.Configurations.RabbitMQ
{
    public class RabbitMqHostedService : IHostedService
    {
        private readonly RabbitMqSettings _settings;
        private readonly RabbitMqConnectionHolder _holder;

        public RabbitMqHostedService(IOptions<RabbitMqSettings> options, RabbitMqConnectionHolder holder)
        {
            _settings = options.Value;
            _holder = holder;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password,
                Port = _settings.Port
            };

            var connection = await factory.CreateConnectionAsync(cancellationToken);

            _holder.Set(connection);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _holder.Connection?.Dispose();
            return Task.CompletedTask;
        }
    }
}
