using System;

namespace NodeManagerCore.Models
{
    public class Container
    {
        public int Id { get; set; }
        public string QueueId { get; set; }
        public string Name { get; set; }
        public DateTime LastChecked { get; set; }
    }
}