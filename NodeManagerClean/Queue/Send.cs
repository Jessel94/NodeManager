using NodeManagerClean.Controllers;
using NodeManagerClean.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NodeManagerClean.Queue
{
    public class Send
    {
        public static void Main(Container container, string message)
        {
            var factory = new ConnectionFactory();

            var QueueID = container.QueueId;
            var queuesController = new QueuesController();
            var queue = queuesController.GetQueue(QueueID);
            factory.HostName = queue.HostName;

            factory.Port = AmqpTcpEndpoint.UseDefaultPort;

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: container.QueueId,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: container.QueueId,
                                     basicProperties: properties,
                                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}