using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace NodeManagerClean.Queue
{
    public class Recieve
    {
        public static void Main(string hostname, string queueId)
        {
            var factory = new ConnectionFactory() { HostName = hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueId,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: queueId,
                                     noAck: false,
                                     consumer: consumer);
            }
        }
    }
}