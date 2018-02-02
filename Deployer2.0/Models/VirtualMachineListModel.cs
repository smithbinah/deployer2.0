using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using vmware.samples.vcenter.vm.list;

namespace Deployer2._0.Models
{
    public class VirtualMachineListModel
    {
        public List<vmware.vcenter.VMTypes.Summary> VMs = new List<vmware.vcenter.VMTypes.Summary>();
        
        public VirtualMachineListModel()
        {
            ListVMs listVMs = new ListVMs();
            Process.Start(listVMs.returnPath() + "\\ListVMs.exe");
            VMs = ListVMs.vmList;
        }
    }
}