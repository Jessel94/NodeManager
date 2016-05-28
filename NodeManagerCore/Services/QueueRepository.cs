using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace NodeManagerCore.Services
{
    public class QueueRepository
    {
        private const string CacheKey = "QueueStore";
        IMemoryCache memoryCache;

        public QueueRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            var ExistingQueues = new Models.Queue[] { };
            var Queues = new Models.Queue[]
            {
                new Models.Queue
                {
                    HostName = "http://145.24.222.140/",
                    QueueId = "1",
                    QueueName = "dockname",
                    QueuePass = "dockpass",
                },
                new Models.Queue
                {
                    HostName = "http://145.24.222.140/",
                    QueueId = "45",
                    QueueName = "swarmname",
                    QueuePass = "swarmpass",
                },
                new Models.Queue
                {
                    HostName = "http://145.24.222.140/",
                    QueueId = "80",
                    QueueName = "23872834902347283492034-20=423424",
                    QueuePass = "203984293042837823084375834583453",
                }
            };

            if (memoryCache.TryGetValue(CacheKey, out ExistingQueues))
            {
                var cachedQueues = ExistingQueues;
            }
            else
            {
                memoryCache.Set(CacheKey, Queues);
            }
        }


        public Models.Queue[] GetAllQueues()
        {
            var ExistingQueues = new Models.Queue[] { };

            if (memoryCache.TryGetValue(CacheKey, out ExistingQueues))
            {
                var cachedQueues = ExistingQueues;
                return cachedQueues;
            }
            else
            {
                return ExistingQueues;
            }
        }

        public bool SaveQueue(Models.Queue queue)
        {
            var ExistingQueues = new Models.Queue[] { };
            if (memoryCache.TryGetValue(CacheKey, out ExistingQueues))
            {
                var cachedQueues = ExistingQueues.ToList();
                cachedQueues.Add(queue);
                memoryCache.Set(CacheKey, cachedQueues.ToArray());

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
