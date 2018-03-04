using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deployer2._0.Resources.APIResources
{
    public class Fields
    {
        public string customfield_10007 { get; set; }
        [DeserializeAs(Name = "priority")]
        public Priority priority { get; set; }
        [DeserializeAs(Name = "status")]
        public Status status { get; set; }
        
        public override string ToString()
        {
            return "priority: " + priority + "status: " + status;
        }
    }
}