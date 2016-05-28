using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

using NodeManagerCore.Helpers;
using NodeManagerCore.Queue;

namespace NodeManagerCore.Controllers
{
    [Route("api/[controller]")]
    public class CommandController : ApiController
    {
        // GET api/Command
        [HttpGet]
        public string Get()
        {
            return "No input";
        }

        //GET: api/Command/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            string[] input = StringFilter.FilterInput(id);

            string result = Send.Main(input[0], input[1]);

            return result;
        }
    }
}
