using RabbitMQ.Client;

namespace Notifications.Infrastructure.Configurations.RabbitMQ
{
    public class RabbitMqConnectionHolder
    {
        public IConnection Connection { get; private set; } = null!;

        public void Set(IConnection connection)
        {
            Connection = connection;
        }

    }

}
