using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NodeManager.Models
{
    public class Queue
    {
        public string QueueId { get; set; }
        public string HostName { get; set; }
        public string QueueName { get; set; }
        public string QueuePass { get; set; }
    }
}