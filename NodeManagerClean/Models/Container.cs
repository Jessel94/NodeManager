using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NodeManagerClean.Models
{
    public class Container
    {
        public int Id { get; set; }
        public int QueueId { get; set; }
        public string Name { get; set; }
        public DateTime LastChecked { get; set; }
    }
}