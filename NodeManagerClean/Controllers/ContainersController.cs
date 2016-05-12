using NodeManagerClean.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NodeManagerClean.Controllers
{
    public class ContainersController : ApiController
    {
        Container[] containers = new Container[]
        {
            new Container { Id = 1, Name = "Dock 1", QueueId = 1, LastChecked = DateTime.Today.AddDays(-1) },
            new Container { Id = 2, Name = "Swarm stuffs", QueueId = 45, LastChecked = DateTime.Today },
            new Container { Id = 3, Name = "FBI Survailence container", QueueId = 80, LastChecked = DateTime.Today.AddHours(-1) }
        };

        public IEnumerable<Container> GetAllContainers()
        {
            return containers;
        }

        public IHttpActionResult GetContainer(int id)
        {
            var container = containers.FirstOrDefault((p) => p.Id == id);
            if (container == null)
            {
                return NotFound();
            }
            return Ok(container);
        }
    }
}