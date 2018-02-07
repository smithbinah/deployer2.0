using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vmware.samples.common;
using vmware.samples.common.authentication;
using vmware.vcenter;

namespace Deployer2._0.Models
{
    public class VirtualMachineRetrieval : SamplesBase
    {
        vmware.vcenter.VM vmService;
        //BulletedList bulletedList;
        public BulletedList bulletedList { get; set; }
        
        public VirtualMachineRetrieval(string serverName, string userName, string password, bool skipServerVerification)
        {
            this.Server = serverName;
            this.UserName = userName;
            this.Password = password;
            this.SkipServerVerification = skipServerVerification;
            this.ClearData = true;
            
        }
        public override void Cleanup()
        {
           // VapiAuthHelper.Logout();
        }
        
        public override async void Run()
        {
            SetupSslTrustForServer();

            this.VapiAuthHelper = new VapiAuthenticationHelper();
            
            this.SessionStubConfiguration =
                await this.VapiAuthHelper.LoginByUsernameAndPasswordAsync(
                                this.Server, this.UserName, this.Password);
            this.vmService =
                this.VapiAuthHelper.StubFactory.CreateStub<vmware.vcenter.VM>(this.SessionStubConfiguration);
            List<VMTypes.Summary> vmlist = await vmService.ListAsync(new VMTypes.FilterSpec());
            List<string> vmNamesList = new List<string>();
                VMInformation.VirtualMachineList.Clear();
            foreach (VMTypes.Summary summary in vmlist)
            {
                VMInformation.VirtualMachineList.Add(summary);
            }
            
            bulletedList.DataSource = VMInformation.VirtualMachineList;
            bulletedList.DataBind();
            
        }

        
    }
}