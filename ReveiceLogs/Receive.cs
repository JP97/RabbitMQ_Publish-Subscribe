using System;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Receiver;

namespace ReveiceLogs
{
    class Receive
    {
        static void Main(string[] args)
        {
            Listener listener = new Listener();

            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                listener.Listen(channel);

                Console.ReadLine();
            }
        }
    }
}
