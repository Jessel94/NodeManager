using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

using NodeManagerCore.Services;

namespace NodeManagerCore.Controllers
{
    [Route("api/[controller]")]
    public class QueuesController : Controller
    {
        private QueueRepository queueRepository;
        IMemoryCache memoryCache;

        public QueuesController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            this.queueRepository = new QueueRepository(memoryCache);
        }

        //GET: api/queues
        [HttpGet]
        public Models.Queue[] Get()
        {
            return queueRepository.GetAllQueues();
        }

        //GET: api/Containers/id
        [HttpGet("{id}")]
        public Models.Queue Get(string id)
        {
            var queues = queueRepository.GetAllQueues();
            return queues.FirstOrDefault((p) => p.QueueId == id);
        }

        ////Currently Unused Function
        //public HttpResponseMessage Post(Models.Queue queue)
        //{
        //    queue.HostName = "new hostname";
        //    queue.QueueId = "99";
        //    queue.QueueName = "new queuename";
        //    queue.QueuePass = "new queuepass";

        //    this.queueRepository.SaveQueue(queue);

        //    var response = Request.CreateResponse(HttpStatusCode.Created, queue);

        //    return response;
        //}
    }
}