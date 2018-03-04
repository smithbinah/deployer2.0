using Deployer2._0.Models;
using Deployer2._0.Models.PowerCLIHelper;
using Deployer2._0.Resources.APIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Deployer2._0.Controllers
{
    
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
           
            return View();
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Simple Server Deployer Description ...";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us at ...";

            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Servers()
        {

            ViewBag.Message = "Under Development...";
            VirtualMachineModel testVirtualMachineModel = new VirtualMachineModel { Name="IIS Server", Description= "The Web server" , IPAddress = "10.0.88.55", CPUs = 2, Memory = 8, OperatingSystem = "Windows" };
            List<VirtualMachineModel> virtualMachines = new List<VirtualMachineModel>();
            //await Task.Factory.StartNew(() => { Thread.Sleep(3000); });
            VirtualMachineAPIController virtualMachineAPIController = new VirtualMachineAPIController();
            ViewBag.test = virtualMachineAPIController.Login("blah");
            VMWare vmScript = new VMWare();
            var VMList = vmScript.Login();
            ViewBag.vmlist = VMList;
            return View(testVirtualMachineModel);
        }
        
    }
}