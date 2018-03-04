using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deployer2._0.Resources.APIResources
{
    public class Status
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public StatusCategory statusCategory { get; set; }

        public override string ToString()
        {
            return "Name: " + name + " ID: " + id + " description: " + description;
        }
    }
}