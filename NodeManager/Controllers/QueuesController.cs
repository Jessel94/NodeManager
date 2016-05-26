using Microsoft.Extensions.Caching.Memory;
using NodeManager.Models;
using NodeManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NodeManager.Controllers
{
    public class QueuesController : ApiController
    {
        private QueueRepository queueRepository;
        IMemoryCache memoryCache;

        public QueuesController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            this.queueRepository = new QueueRepository(memoryCache);
        }

        public Models.Queue[] Get()
        {
            return queueRepository.GetAllQueues();
        }

        public Models.Queue GetQueue(string id)
        {
            var queues = queueRepository.GetAllQueues();
            var queue = queues.FirstOrDefault((p) => p.QueueId == id);
            if (queue == null)
            {
                return null;
            }
            return queue;
        }

        public HttpResponseMessage Post(Models.Queue queue)
        {
            queue.HostName = "new hostname";
            queue.QueueId = "99";
            queue.QueueName = "new queuename";
            queue.QueuePass = "new queuepass";

            this.queueRepository.SaveQueue(queue);

            var response = Request.CreateResponse(HttpStatusCode.Created, queue);

            return response;
        }
    }
}