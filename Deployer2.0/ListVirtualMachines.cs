using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deployer2._0.Models;
using vmware.samples.common;
using System.Web.UI.WebControls;

namespace Deployer2._0
{
    public class ListVirtualMachines : SamplesBase
    {
        BulletedList bulletedList;
        public ListVirtualMachines(BulletedList bulletedList)
        {
            this.bulletedList = bulletedList;
        }
       
public override void Run()
        {
            //VirtualMachineRetrieval vmr = new VirtualMachineRetrieval(serverName: "10.0.88.11", userName: "administrator@vsphere.local", password: "Nu140859246", skipServerVerification: true);
            //vmr.Run();
            //this.BulletedList1.DataSource = VMInformation.VirtualMachineList;
        }

        public override void Cleanup()
        {
            
        }
    }
}