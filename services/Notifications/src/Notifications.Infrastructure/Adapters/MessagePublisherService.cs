using Notifications.Domain.Interfaces;
using Notifications.Infrastructure.Configurations.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Notifications.Infrastructure.Adapters
{
    public class MessagePublisherService : IMessagePublisherService
    {
        private IConnection _Connection { get;set; }

        public MessagePublisherService(RabbitMqConnectionHolder connectionHolder)
        {
            _Connection = connectionHolder.Connection;
        }


        public async Task PublishAsync<T>(string queueName, T command)
        {
            await using var channel = await _Connection.CreateChannelAsync();

            channel.BasicReturnAsync += OnMessageReturned;

            await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var body = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(command);

            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, mandatory: true, basicProperties: properties, body: body); 

            await channel.CloseAsync();
        }

        private Task OnMessageReturned(object sender, BasicReturnEventArgs args)
        {
            var message = Encoding.UTF8.GetString(args.Body.ToArray());
            Console.WriteLine("Mensagem NÃO roteada!");
            Console.WriteLine($"ReplyCode: {args.ReplyCode}");
            Console.WriteLine($"ReplyText: {args.ReplyText}");

            return Task.CompletedTask;
        }
    }
}
