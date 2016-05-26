using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using NodeManager.Helpers;
using NodeManager.Queue;

namespace NodeManager.Controllers
{
    public class CommandController : ApiController
    {
        //GET: api/Command
        public string Get()
        {
            string noInput = "No input";
            return noInput;
        }

        //GET: api/Command/5
        public string Get([FromUri]string id)
        {
            string[] input = StringFilter.FilterInput(id);

            string result = Send.Main(input[0], input[1]);

            return result;
        }
    }
}
