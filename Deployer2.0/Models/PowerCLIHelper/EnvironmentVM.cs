using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deployer2._0.Models.PowerCLIHelper
{
    public class EnvironmentVM
    {
        public object Name { get; set; }
        public object PowerState { get; set; }
        public object Guest { get; set; }
        public object CPUNum { get; set; }
        public object Cores { get; set; }
        public object Memory { get; set; }
        public object Host { get; set; }
        public object DiskSize { get; set; }
        public OS OperatingSystem { get; set; }



    }
}