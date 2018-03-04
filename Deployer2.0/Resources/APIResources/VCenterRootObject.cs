using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deployer2._0.Resources.APIResources
{
    public class VCenterRootObject
    {
        [DeserializeAs(Name = "value")]
        public string value { get; set; }

    }
}