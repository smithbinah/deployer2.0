using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deployer2._0.Models;
using System.Threading;

namespace Deployer2._0
{
    public partial class AsyncForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VirtualMachineRetrieval vmr = new VirtualMachineRetrieval(serverName: "10.0.88.11", userName: "administrator@vsphere.local", password: "Nu140859246!", skipServerVerification: true);
            vmr.bulletedList = this.BulletedList1;
            vmr.Run();
            this.BulletedList1.DataSource = VMInformation.VirtualMachineList;
            
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "ClosePage", "window.close();", true);
        }

        public void GenneratorButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}