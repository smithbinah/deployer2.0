using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deployer2._0.Resources.APIResources
{
    public class VMValue
    {
        public int memory_size_MiB { get; set; }
        public string vm { get; set; }
        public string name { get; set; }
        public string power_state { get; set; }
        public int cpu_count { get; set; }
    }
}