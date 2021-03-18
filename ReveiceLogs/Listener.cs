using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Receiver
{
    public class Listener
    {
        private IModel _channel;
        public void Listen(IModel channel)
        {
            _channel = channel;
            _channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            string queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName,
                              exchange: "logs",
                              routingKey: "");

            Console.WriteLine(" [*] Waiting for logs.");

            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] {0}", message);
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
