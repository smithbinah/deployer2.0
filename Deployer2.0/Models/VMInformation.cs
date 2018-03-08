using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vmware.vcenter;

namespace Deployer2._0.Models
{
    public static class VMInformation
    {
        public static int partialIndex = 0;
        public static List<VMTypes.Summary> VirtualMachineList = new List<VMTypes.Summary>();
        public static int DeployemntMethod = 0;
        public static object DeploymentProgress = 0;

    }
}