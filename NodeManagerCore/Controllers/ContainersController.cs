using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

using NodeManagerCore.Models;
using NodeManagerCore.Services;

namespace NodeManagerCore.Controllers
{
    [Route("api/[controller]")]
    public class ContainersController : Controller
    {
        private ContainerRepository containerRepository;
        IMemoryCache memoryCache;

        public ContainersController(IMemoryCache memoryCache)
        {
            this.containerRepository = new ContainerRepository(memoryCache);
        }

        //GET: api/Containers
        [HttpGet]
        public Container[] Get()
        {
            return containerRepository.GetAllContainers();
        }

        //GET: api/Containers/id
        [HttpGet("{id}")]
        public Container Get(int id)
        {
            var containers = containerRepository.GetAllContainers();
            return containers.FirstOrDefault((p) => p.Id == id);
        }

        ////Currently Unused Function
        //public HttpResponseMessage Post(Container container)
        //{
        //    container.Id = 4;
        //    container.QueueId = "90";
        //    container.LastChecked = DateTime.Today;

        //    this.containerRepository.SaveContainer(container);

        //    var response = Request.CreateResponse(HttpStatusCode.Created, container);

        //    return response;
        //}
    }
}