using NodeManagerClean.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NodeManagerClean.Services
{
    public class ContainerRepository
    {
        private const string CacheKey = "ContainerStore";

        public ContainerRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var Containers = new Container[]
                    {
                        new Container
                        {
                            Id = 1,
                            Name = "Dock 1",
                            HostName = "192.168.0.1",
                            QueueId = "1",
                            QueueName = "dockname",
                            QueuePass = "dockpass",
                            LastChecked = DateTime.Today.AddDays(-1)
                        },
                        new Container
                        {
                            Id = 2,
                            Name = "Swarm stuffs",
                            HostName = "192.168.0.2",
                            QueueId = "45",
                            QueueName = "swarmname",
                            QueuePass = "swarmpass",
                            LastChecked = DateTime.Today
                        },
                        new Container
                        {
                            Id = 3,
                            Name = "FBI Survailence container",
                            HostName = "192.168.0.3",
                            QueueId = "80",
                            QueueName = "23872834902347283492034-20=423424",
                            QueuePass = "203984293042837823084375834583453",
                            LastChecked = DateTime.Today.AddHours(-1)
                        }
                    };
                    ctx.Cache[CacheKey] = Containers;
                }
            }
        }


        public Container[] GetAllContainers()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Container[])ctx.Cache[CacheKey];
            }

            return new Container[]
                {
                    new Container
                    {
                        Id = 0,
                        Name = "Placeholder",
                        QueueId = "0",
                        LastChecked = DateTime.Today
                    }
                };
        }

        public bool SaveContainer(Container container)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Container[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(container);
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
