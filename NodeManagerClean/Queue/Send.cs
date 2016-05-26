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
        public static string Main(string QueueID, string Message)
        {
            try
            {
                var factory = new ConnectionFactory();

                var queuesController = new QueuesController();
                var queue = queuesController.GetQueue(QueueID);
            
                factory.HostName = "145.24.222.140";
                factory.UserName = "0885083";
                factory.Password = "awesomePassword23";
                factory.VirtualHost = "test";
                factory.Port = AmqpTcpEndpoint.UseDefaultPort;
                factory.Protocol = Protocols.AMQP_0_9_1;

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueID,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(Message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                         routingKey: QueueID,
                                         basicProperties: properties,
                                         body: body);

                    Console.WriteLine(" [x] Sent {0}", Message);
                    string response = "Message " + Message + " has been added to the queue.";
                    return response;
                }
            }
            catch(Exception e)
            {
                string Error = "Error sending message: " + e.ToString();
                return Error;
            }
        }
    }
}