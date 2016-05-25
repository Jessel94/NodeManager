using NodeManagerClean.Models;
using NodeManagerClean.Services;
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

        private ContainerRepository containerRepository;

        public ContainersController()
        {
            this.containerRepository = new ContainerRepository();
        }

        public Container[] Get()
        {
            return containerRepository.GetAllContainers();
        }

        public IHttpActionResult GetContainer(int id)
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

            //Queue.Send.Main(container, "test1234");
            container.QueueId = Queue.Recieve.Main(container);

            this.containerRepository.SaveContainer(container);

            var response = Request.CreateResponse(HttpStatusCode.Created, container);

            return response;
        }
    }
}