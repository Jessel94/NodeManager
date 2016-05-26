using NodeManager.Models;
using NodeManager.Controllers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace NodeManager.Queue
{
    public class Recieve
    {
        public static string Main(Container container)
        {
            var factory = new ConnectionFactory();

            var QueueID = container.QueueId;
            var queuesController = new QueuesController();
            var queue = queuesController.GetQueue(QueueID);
            //factory.HostName = queue.HostName;
            factory.HostName = "localhost";

            factory.Port = AmqpTcpEndpoint.UseDefaultPort;

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: container.QueueId,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message;
                message = "NO DATA";

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: container.QueueId,
                                     noAck: false,
                                     consumer: consumer);
                return message;
            }
        }
    }
}