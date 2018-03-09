using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deployer2._0.Models;
using System.Threading.Tasks;
using Deployer2._0.Resources.APIResources;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.IO;
using Deployer2._0.Models.PowerCLIHelper;
using System.Web.UI;
using System.Dynamic;

namespace Deployer2._0.Controllers
{
    [RoutePrefix("VirtualMachineController")]
    [Authorize]
    public class VirtualMachineController : Controller
    {

        // GET: VirtualMachine
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ViewResult CreationForm()
        {
            return View();
        }
        [HttpPost]
        public ViewResult CreationForm(VirtualMachineModel creationResponse)
        {
            //VirtualMachineCreation vmc = new VirtualMachineCreation(creationResponse.Name);
            //VirtualMachineRetrieval vmr = new VirtualMachineRetrieval(serverName: "10.0.88.11", userName: "administrator@vsphere.local", password: "Nu140859246!", skipServerVerification: true);
            return View("Thanks", creationResponse);
        }
        public ViewResult Users()
        {

            DeployerEntities3 deployerEntities3 = new DeployerEntities3();
            return View(from customer in deployerEntities3.AspNetUsers.Take(10) select customer);
        }
        public ViewResult showServers()
        {

            return View(new VirtualMachineListModel());
        }
        public void IsOverDue(List<Issue> issues, List<Issue> overDueIssues)
        {

            foreach (Issue issue in issues)
            {
                string dueDate = issue.fields.customfield_10007;
                DateTime parsedDueDate;
                if (DateTime.TryParse(dueDate, out parsedDueDate))
                {
                    if (DateTime.Compare(parsedDueDate, DateTime.Now) < 0)
                    {
                        overDueIssues.Add(issue);
                    }
                }

            }

        }
        public ViewResult Analytics()
        {
            ViewBag.Title = "Analytics";
            JiraAPIController jiraAPIController = new JiraAPIController();
            List<Issue> Allissues = jiraAPIController.getAllIssues().issues;
            List<Issue> overDueIssues = new List<Issue>();
            ViewBag.Content = Allissues;
            ViewBag.TotalIssues = Allissues.Count();
            ViewBag.OverDue = overDueIssues;

            int IssueIsDoneCount = 0;
            int idNumber = 0;

            foreach (Issue issue in Allissues)
            {
                Int32.TryParse(issue.fields.status.id, out idNumber);
                if (idNumber == 10001)
                {
                    IssueIsDoneCount++;
                }

            }
            IsOverDue(Allissues, overDueIssues);



            string progress = "Project Progress: " + (Allissues.Count() / IssueIsDoneCount) + "%    Issues Completed: ";
            ViewBag.Progress = progress;
            return View();
        }
        [HttpGet]
        public ViewResult VMs()
        {
            ViewBag.Title = "VMs";
            VMSuperModel environmentVM = new VMSuperModel();
            //dynamic dynamicModel = new ExpandoObject();
            //dynamicModel.DeployableEnvironmentVM = GetDeployableEnvironmentVM();

            return View("VMs", environmentVM);
        }
        [HttpPost]
        public ViewResult VMs(VMSuperModel environmentVM)
        {


            VMWare vmwarePs = new VMWare();
            vmwarePs.DeployCustomVM(environmentVM);
            //ViewBag.test = "The ting goes Boom, skidi dat";
            return View(environmentVM);
        }
        [HttpPost]
        public ViewResult DatabaseVM(VMSuperModel environmentVM)
        {
            VMWare vmwarePs = new VMWare();
            environmentVM.CPUNum = "";
            environmentVM.DiskSize = "";
            environmentVM.Memory = "";
            environmentVM.OperatingSystem = OS.Centos;
            vmwarePs.DeployDatabaseVM(environmentVM);

            return View("VMs", environmentVM);
        }
        [HttpPost]
        public ViewResult CustomVM(VMSuperModel environmentVM)
        {
            VMWare vmwarePs = new VMWare();
            vmwarePs.DeployCustomVM(environmentVM);

            return View("VMs", environmentVM);
        }
        [HttpPost]
        public ViewResult ftp(VMSuperModel environmentVM)
        {

            VMWare vmwarePs = new VMWare();
            environmentVM.CPUNum = "";
            environmentVM.DiskSize = "";
            environmentVM.Memory = "";
            environmentVM.OperatingSystem = OS.Centos;
            vmwarePs.DeployDatabaseVM(environmentVM);

            return View("VMs", environmentVM);
        }
        [HttpPost]
        public ViewResult web(VMSuperModel environmentVM)
        {
            VMWare vmwarePs = new VMWare();
            environmentVM.CPUNum = "";
            environmentVM.DiskSize = "";
            environmentVM.Memory = "";
            environmentVM.OperatingSystem = OS.Centos;
            vmwarePs.DeployDatabaseVM(environmentVM);
            return View("VMs");
        }


        [HttpGet]
        public ViewResult VMForm()
        {


            return View();
        }
        [HttpPost]
        public async Task<ViewResult> VMForm(VMModel virtualMachineModel)
        {
            string Name = virtualMachineModel.Name;
            string Description = virtualMachineModel.Description;
            string IPAddress = virtualMachineModel.OperatingSystem;
            int Memory = virtualMachineModel.Memory;
            int CPU = virtualMachineModel.CPUs;
            int DiskSize = virtualMachineModel.DiskSize;

            VirtualMachineAPIController virtualMachineAPIController;
            virtualMachineAPIController = new VirtualMachineAPIController();
            ViewBag.test = await virtualMachineAPIController.Login();



            //await virtualMachineAPIController.CreateVM(virtualMachineModel);

            string link = System.Web.HttpContext.Current.Server.MapPath("~/ExhaustiveVm/CreateExhaustiveVm.exe");
            //link = "\" " + link + " \"";

            //Process.Start("cmd.exe", $" /k \"{link}\"  --server 10.0.88.11 --username administrator@vsphere.local --password Nu140859246! --skip-server-verification --cleardata --datacenter Datacenter --cluster VSANCluster --vmfolder APIVMs --datastore vsanDatastore --standardportgroup APINetwork --distributedportgroup API(DPG) --isodatastorepath [vsanDatastore] ISOImages --vmname " + virtualMachineModel.Name );
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $" /c \"{link}\"  --server 10.0.88.11 --username administrator@vsphere.local --password Nu140859246! --skip-server-verification --cleardata --datacenter Datacenter --cluster VSANCluster --vmfolder APIVMs --datastore vsanDatastore --standardportgroup APINetwork --distributedportgroup API(DPG) --isodatastorepath [vsanDatastore] ISOImages --vmname " + virtualMachineModel.Name,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            string pattern = @"( \'\w+\'\: \w+)";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            string output = "";
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();

                output = line;
            }
            Match match = regex.Match(output);
            string matchedGroup = "";
            if (match.Success)
            {
                matchedGroup = match.Groups[0].Value;

            }
            ViewBag.escapedLink = matchedGroup;

            return View("Thanks", virtualMachineModel);
        }

        public ViewResult Containers()
        {
            ViewBag.Title = "Containers";
            return View();
        }

        [HttpGet]
        public ViewResult ContainerForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult ContainerForm(ContainerModel containerModel)
        {
            string link = System.Web.HttpContext.Current.Server.MapPath("~/DeployScripts/DeployContainer.ps1");
            string scriptDir = $"{link}";
            string scriptText = System.IO.File.ReadAllText(scriptDir);
            PSDataCollection<ErrorRecord> errorRecord;
            using (PowerShell powerShellIstance = PowerShell.Create())
            {

                powerShellIstance.AddScript(scriptText);
                powerShellIstance.AddParameter("containerName", containerModel.Name);
                powerShellIstance.AddParameter("containerTemplate", containerModel.Distro);
                powerShellIstance.Invoke();
                //var results = powerShellIstance.Invoke();
                errorRecord = powerShellIstance.Streams.Error;

            }
            ViewBag.errors = errorRecord;
            return View("ThanksContainer", containerModel);
        }
        public ViewResult Environment()
        {

            VirtualMachineAPIController virtualMachineAPIController = new VirtualMachineAPIController();
            ViewBag.test = virtualMachineAPIController.Login("blah");
            VMWare vmScript = new VMWare();


            ViewBag.vmlist = vmScript.ConvertToObjectList(vmScript.Login());
            //VMRoot vmlist = virtualMachineAPIController.ListVMs();
            //ViewBag.vmlist = vmlist;
            ViewBag.Title = "Environment";
            return View();
        }


    }
}