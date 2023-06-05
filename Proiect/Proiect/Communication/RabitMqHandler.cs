using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Security.Policy;
using System.Text;

namespace Proiect.Communication
{
    public class RabitMqHandler : IRabitMqHandler
    {
        private readonly string queueName = Guid.NewGuid().ToString();
        private readonly IModel channel;

        public RabitMqHandler()
        {
            var connectionFactory = new ConnectionFactory();
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.ExchangeDeclare("fanout.exchange", ExchangeType.Fanout, true, false, null);
            channel.QueueBind(queue: queueName, exchange: "fanout.exchange", routingKey: "");
        }

        public void addCallback(Action callback)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                callback.Invoke();
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void sendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "fanout.exchange",
                     routingKey: queueName,
                     basicProperties: null,
                     body: body);
        }

    }
}
