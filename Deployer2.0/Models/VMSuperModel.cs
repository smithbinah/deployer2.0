using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deployer2._0.Models
{
    public class VMSuperModel
    {
        public string Name { get; set; }
        public string CPUNum { get; set; }
        public string Memory { get; set; }
        public string DiskSize { get; set; }
        public OS OperatingSystem { get; set; }
    }
}