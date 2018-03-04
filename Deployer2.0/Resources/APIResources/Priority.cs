using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deployer2._0.Resources.APIResources
{
    public class Priority
    {
        [DeserializeAs(Name = "self")]
        public string self { get; set; }
        [DeserializeAs(Name = "iconUrl")]
        public string iconUrl { get; set; }
        [DeserializeAs(Name = "name")]
        public string name { get; set; }
        [DeserializeAs(Name = "id")]
        public string id { get; set; }
    }
}