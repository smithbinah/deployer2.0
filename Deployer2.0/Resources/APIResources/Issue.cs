using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Deployer2._0.Resources.APIResources
{
    public class Issue
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        [DeserializeAs(Name ="fields")]
        public Fields fields { get; set; }
       

        public override string ToString()
        {
            return "ID: "+ id + 
                " Expand: " + expand + 
                " Self: " + self + 
                " Key: " + key +
                " Fields: " + fields
                ;
        }
    }
}