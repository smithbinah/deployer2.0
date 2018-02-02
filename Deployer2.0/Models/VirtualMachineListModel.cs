using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using vmware.samples.common.authentication;
using vmware.samples.common;
using vmware.vapi.bindings;
using vmware.vcenter;
using System.IO;

namespace Deployer2._0.Models
{
    public class VirtualMachineListModel : SamplesBase
    {
        private vmware.vcenter.VM vmService;
        public List<VMTypes.Summary> vmList;
        public VirtualMachineListModel()
        {
            this.Execute(new string[] { "--server 10.0.88.11", "--username administrator@vsphere.local", "--password Nu140859246!", "--skip-server-verification", "--cleardata" });


        }

        public void login()
        {
            VapiAuthHelper = new VapiAuthenticationHelper();
            SessionStubConfiguration =
                            VapiAuthHelper.LoginByUsernameAndPassword(
                                "10.0.88.11", "administrator@vsphere.local", "Nu140859246!");
            //--skip - server - verification--cleardata
            this.vmService =
                VapiAuthHelper.StubFactory.CreateStub<vmware.vcenter.VM>(
                    SessionStubConfiguration);
            vmList =
                this.vmService.List(new VMTypes.FilterSpec());

            

            
        }
        

        public override void Run()
        {
            Environment.Exit(0);
            VapiAuthHelper = new VapiAuthenticationHelper();
            SessionStubConfiguration =
                            VapiAuthHelper.LoginByUsernameAndPassword(
                                Server, UserName, Password);
            //--skip - server - verification--cleardata
            this.vmService =
                VapiAuthHelper.StubFactory.CreateStub<vmware.vcenter.VM>(
                    SessionStubConfiguration);
            vmList =
                this.vmService.List(new VMTypes.FilterSpec());

            string fileLoc = @"c:\sample1.txt";
            FileStream fs = null;
            if (!File.Exists(fileLoc))
            {
                using (fs = File.Create(fileLoc))
                {

                }
            }
            string mydocpath =
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter outputFile = new StreamWriter(@"c:\sample1.txt"))
            {
                foreach (var line in vmList)
                    outputFile.WriteLine(line);
            }
        }

        public override void Cleanup()
        {
            VapiAuthHelper.Logout();
        }
    }
}