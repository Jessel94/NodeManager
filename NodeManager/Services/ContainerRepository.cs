using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using NodeManager.Helpers;
using NodeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NodeManager.Services
{
    public class ContainerRepository
    {
        private const string CacheKey = "ContainerStore";
        IMemoryCache memoryCache;

        public ContainerRepository(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            var ExistingContainers = new Container[] { };
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
            if (memoryCache.TryGetValue(CacheKey, out ExistingContainers))
            {
                var cachedContainers = ExistingContainers;
            }
            else
            {
                memoryCache.Set(CacheKey, Containers);
            }
        }

        public Container[] GetAllContainers()
        {
            var ExistingContainers = new Container[] { };
            if (memoryCache.TryGetValue(CacheKey, out ExistingContainers))
            {
                var cachedContainers = ExistingContainers;
                return cachedContainers;
            }
            else
            {
                return ExistingContainers;
            }
        }

        public bool SaveContainer(Container container)
        {
            var ExistingContainers = new Container[] { };
            if (memoryCache.TryGetValue(CacheKey, out ExistingContainers))
            {
                var cachedContainers = ExistingContainers.ToList();
                cachedContainers.Add(container);
                memoryCache.Set(CacheKey, cachedContainers.ToArray());

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
