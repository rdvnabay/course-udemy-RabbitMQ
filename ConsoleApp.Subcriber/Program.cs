using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

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

            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("hello-queue", false, consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Thread.Sleep(1500);
                Console.WriteLine("Gelen Mesaj: " + message);
                channel.BasicAck(e.DeliveryTag, false);
            };
            Console.ReadLine();
        }
    }
}
