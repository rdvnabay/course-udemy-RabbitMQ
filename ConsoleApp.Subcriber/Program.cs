using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsoleApp.Subcriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://hxymzwof:x8qckj2bB8qEjsN69sH8yLS_EzmZEhYV@clam.rmq.cloudamqp.com/hxymzwof");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("hello-queue", true, consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Gelen Mesaj: " + message);
            };
            Console.ReadLine();
        }
    }
}
