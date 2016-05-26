using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NodeManagerClean.Helpers
{
    public class StringFilter
    {
        public static string[] FilterInput(string args)
        {
            string ContainerID = args.Split('-')[0];
            string Command = args.Split('-')[1];

            return new string[] { ContainerID, Command };
        }
    }
}