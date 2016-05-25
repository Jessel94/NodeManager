using NodeManagerClean.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NodeManagerClean.Services
{
    public class QueueRepository
    {
        private const string CacheKey = "QueueStore";

        public QueueRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var Queues = new Models.Queue[]
                    {
                        new Models.Queue
                        {
                            HostName = "localhost",
                            QueueId = "1",
                            QueueName = "dockname",
                            QueuePass = "dockpass",
                        },
                        new Models.Queue
                        {
                            HostName = "localhost",
                            QueueId = "45",
                            QueueName = "swarmname",
                            QueuePass = "swarmpass",
                        },
                        new Models.Queue
                        {
                            HostName = "localhost",
                            QueueId = "80",
                            QueueName = "23872834902347283492034-20=423424",
                            QueuePass = "203984293042837823084375834583453",
                        }
                };
                    ctx.Cache[CacheKey] = Queues;
                }
            }
        }


        public Models.Queue[] GetAllQueues()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Models.Queue[])ctx.Cache[CacheKey];
            }

            return new Models.Queue[]
                {
                    new Models.Queue
                    {
                        HostName = "temp",
                        QueueId = "0",
                        QueueName = "temp",
                        QueuePass = "temp"
                    }
                };
        }

        public bool SaveQueue(Models.Queue queue)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Models.Queue[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(queue);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }
    }
}
