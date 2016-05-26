using Microsoft.AspNetCore.Mvc;
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
    public class ContainersController : ApiController
    {
        private ContainerRepository containerRepository;
        IMemoryCache memoryCache;

        public ContainersController(IMemoryCache memoryCache)
        {
            this.containerRepository = new ContainerRepository(memoryCache);
        }

        public Container[] Get()
        {
            return containerRepository.GetAllContainers();
        }

        public IActionResult GetContainer(int id)
        {
            var containers = containerRepository.GetAllContainers();
            var container = containers.FirstOrDefault((p) => p.Id == id);
            if (container == null)
            {
                return NotFound();
            }
            return Ok(container);
        }

        public HttpResponseMessage Post(Container container)
        {            
            container.Id = 4;
            container.QueueId = "90";
            container.LastChecked = DateTime.Today;

            this.containerRepository.SaveContainer(container);

            var response = Request.CreateResponse(HttpStatusCode.Created, container);

            return response;
        }
    }
}