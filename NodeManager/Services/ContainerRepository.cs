using NodeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace NodeManager.Services
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
                            QueueId = "1",
                            LastChecked = DateTime.Today.AddDays(-1)
                        },
                        new Container
                        {
                            Id = 2,
                            Name = "Swarm stuffs",
                            QueueId = "45",
                            LastChecked = DateTime.Today
                        },
                        new Container
                        {
                            Id = 3,
                            Name = "FBI Survailence container",
                            QueueId = "80",
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
