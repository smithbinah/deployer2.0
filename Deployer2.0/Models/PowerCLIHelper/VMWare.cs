using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Deployer2._0.Models.PowerCLIHelper
{
    public class VMWare
    {
        public Collection<PSObject> Login()
        {
            string link = System.Web.HttpContext.Current.Server.MapPath("~/DeployScripts/GetVMInfo.ps1");
            string scriptDir = $"{link}";
            string scriptText = System.IO.File.ReadAllText(scriptDir);
            StringBuilder stringBuilder = new StringBuilder();
            PSDataCollection<ErrorRecord> errorRecord;

            Collection<PSObject> resultList;
            using (PowerShell powerShellIstance = PowerShell.Create())
            {

                powerShellIstance.AddScript(scriptText);
                resultList = powerShellIstance.Invoke();
                //var results = powerShellIstance.Invoke();
                errorRecord = powerShellIstance.Streams.Error;

            }
            //foreach (PSObject obj in resultList)
            //{

            //}
            return resultList;
        }
        public List<EnvironmentVM> ConvertToObjectList(Collection<PSObject> collection)
        {
            List<EnvironmentVM> EVMList = new List<EnvironmentVM>();
            List<string> list = new List<string>();
            foreach (object item in collection)
            {

            }
            foreach (PSObject listItem in collection)
            {
                object teststring2;
                List<object> values = new List<object>();
                List<string> names = new List<string>();
                List<string> combinedProp = new List<string>();
                List<object> reducedLines = new List<object>();

                foreach (PSMemberInfo prop in listItem.Members)
                {

                    names.Add(prop.Name);
                }
                foreach (PSMemberInfo prop in listItem.Members)
                {

                    values.Add(prop.Value);
                }
                //foreach (List<string> item in names)
                //{
                //    string lambdaItem = item.ToString();
                //    item += testString[names.FindIndex(items => items.ToString() == lambdaItem.ToString())];
                //}
                for (int i = 0; i < 10; i++)
                {

                    reducedLines.Add(names[i] + ": " + values[i]);
                }
                EVMList.Add(
                    new EnvironmentVM()
                    {
                        Name = reducedLines[0],
                        PowerState = reducedLines[1],
                        Guest = reducedLines[3],
                        CPUNum = reducedLines[4],
                        Cores = reducedLines[5],
                        Memory = reducedLines[6],
                        Host = reducedLines[9]
                    }
                    );
                


            }
                return EVMList;
        }
#region
        public void Refine(List<string> nameList, List<string> valueList)
        {
           
            
            List<string> Namepatterns = new List<string>()
            {
                @"(Name)",
                @"(PowerState)",
                @"(Notes)",
                @"(Guest)",
                @"(NumCpu)",
                @"(CoresPerSocket)",
                @"(MemoryMB)",
                @"(MemoryGB)",
                @"(VMHostId)",
                @"(VMHost)"
            };


            foreach (string pattern in Namepatterns)
            {
                MatchPattern(nameList, pattern);
            }
            foreach (string pattern in Namepatterns)
            {
                MatchPattern(valueList, pattern);
            }



        }
        public object MatchPattern(List<string> list, string pattern)
        {
            Regex regex = new Regex(pattern);
            object itemFound = null;
            foreach (string item in list)
            {
                Match match = regex.Match(item);
                if (match.Success)
                {
                    itemFound = match.Value;
                }
            }
            return itemFound;
        }
#endregion
        public void DeployCustomVM(VMSuperModel environmentVM)
        {
            string link = System.Web.HttpContext.Current.Server.MapPath("~/DeployScripts/CreateVM.ps1");
            string scriptDir = $"{link}";
            string scriptText = System.IO.File.ReadAllText(scriptDir);
            PSDataCollection<ErrorRecord> errorRecord;
            string GuestID = "";
            string ISOLocation = "";
            switch (environmentVM.OperatingSystem)
            {
                case OS.Centos:
                    GuestID = "centos7Guest";
                     ISOLocation = @"[VSANDatastore] ISO Images\CentOS-7x86_64-Everything-1511 (1).iso";
                    break;
                case OS.Ubuntu:
                    GuestID = "ubuntu64Guest";
                    ISOLocation = @"[VSANDatastore] ISO Images\ubuntu-16.04.3-server-amd64.iso";

                    break;
                case OS.kali:
                    GuestID = "debian10Guest";
                    ISOLocation = @"[VSANDatastore] ISO Images\kali-linux-2016.1-amd64.iso" ;
            break;
                case OS.archlinux:
                    GuestID = "otherLinuxGuest";
                    ISOLocation = @"[VSANDatastore] ISO Images\archlinux-2018.03.01-x86_64.iso";
                    break;
                case OS.Fedora:
                    GuestID = "fedoraGuest";
                    ISOLocation = @"[VSANDatastore] ISO Images\Fedora-Workstation-Live-x86_64-25-1.3.iso";
                    break;
                default:
                    break;
            }
            using (PowerShell powerShellIstance = PowerShell.Create())
            {

                powerShellIstance.AddScript(scriptText);
                powerShellIstance.AddParameter("VMName", environmentVM.Name.ToString());
                powerShellIstance.AddParameter("DiskSize", environmentVM.DiskSize.ToString());
                powerShellIstance.AddParameter("Memory", environmentVM.Memory.ToString());
                powerShellIstance.AddParameter("CPUNum", environmentVM.CPUNum.ToString());
                powerShellIstance.AddParameter("GuestId", GuestID);
                powerShellIstance.AddParameter("ISOLocation", ISOLocation);
                powerShellIstance.Invoke();
                //var results = powerShellIstance.Invoke();
                errorRecord = powerShellIstance.Streams.Error;

            }

        }
        public void DeployDatabaseVM(VMSuperModel preset)
        {
            string link = System.Web.HttpContext.Current.Server.MapPath("~/DeployScripts/CloneDatabaseVM.ps1");
            string scriptDir = $"{link}";
            string scriptText = System.IO.File.ReadAllText(scriptDir);
            PSDataCollection<ErrorRecord> errorRecord;
            using (PowerShell powerShellIstance = PowerShell.Create())
            {

                powerShellIstance.AddScript(scriptText);
                powerShellIstance.AddParameter("VMName", preset.Name.ToString());
                //powerShellIstance.Streams.Progress.DataAdded += (sender, eventargs) => {
                //    PSDataCollection<ProgressRecord> progressRecords = (PSDataCollection<ProgressRecord>)sender;
                //    VMInformation.DeploymentProgress = progressRecords[eventargs.Index].PercentComplete;
                //};
                powerShellIstance.Invoke();
                //var results = powerShellIstance.Invoke();
                errorRecord = powerShellIstance.Streams.Error;

            }
        }
    }
}