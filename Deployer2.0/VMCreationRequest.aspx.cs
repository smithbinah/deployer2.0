using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deployer2._0.Models;
using System.Diagnostics;
using System.IO;
using vmware.samples.vcenter.vm;
using vmware.samples.vcenter.vm.create;
using System.Text.RegularExpressions;

namespace Deployer2._0
{
    public partial class VMCreationRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "ClosePage", "window.close();", true);

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            #region VMC
            //VirtualMachineCreation vmc = new VirtualMachineCreation(VMNameSubmission.Text.ToString(), nameLabel);
            //nameLabel.Text = VMNameSubmission.Text.ToString() + " is being created";
            //vmc.Run();


            //Process.Start("C:\\Users\\Binah Smith\\Documents\\GitHub\\Deployer2.0\\Deployer2.0\\Deployer2.0\\Resources\\CreateExhaustiveVm.exe");
            //ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe");
            //processStartInfo.Arguments = "ipconfig";
            //string str = System.IO.Path.GetFullPath("CreateExhaustiveVM.exe");
            //Process.Start("cmd.exe", "/k echo Your Location: " + str );
#endregion
            string link = HttpContext.Current.Server.MapPath("~/ExhaustiveVm/CreateExhaustiveVm.exe");
            string escapedLink = "\"" + link + "\"";
            Process.Start("cmd.exe", "/k"+ escapedLink+ " --server 10.0.88.11 --username administrator@vsphere.local --password Nu140859246! --skip-server-verification --cleardata --datacenter Datacenter --cluster VSANCluster --vmfolder APIVMs --datastore vsanDatastore --standardportgroup APINetwork --distributedportgroup API(DPG) --isodatastorepath [vsanDatastore] ISOImages");


            
            //nameImage.ImageUrl = Server.MapPath("~/Resources/plusButton.png");
            #region Async
            //nameLabel.Text = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            //    CreateExhaustiveVm cev = new CreateExhaustiveVm() {
            //    Server = "10.0.88.11",
            //    UserName = "administrator@vsphere.local",
            //    Password = "Nu140859246!",
            //    SkipServerVerification = true,
            //    ClearData = true,
            //    ExhaustiveVmName = nameLabel.Text,
            //    DatacenterName = "Datacenter",
            //    ClusterName = "VSANCluster",
            //    VmFolderName = "APIVMs",
            //    DatastoreName = "vsanDatastore",
            //    StandardPortgroupName = "APINetwork",
            //    DistributedPortgroupName = "API(DPG)",
            //    IsoDatastorePath = "[vsanDatastore] ISOImages"
            //};

            //    cev.Run();
            #endregion



        }
    }
}