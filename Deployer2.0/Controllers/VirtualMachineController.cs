using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deployer2._0.Models;
using System.Threading.Tasks;

namespace Deployer2._0.Controllers
{
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
        public async Task<ViewResult> CreationForm(VirtualMachineModel creationResponse)
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
        public ViewResult Analytics()
        {
            ViewBag.Title = "Analytics";
            return View();
        }
        public ViewResult VMs()
        {
            ViewBag.Title = "VMs";

            return View();
        }
        public ViewResult Containers()
        {
            ViewBag.Title = "Containers";
            return View();
        }
        public ViewResult Environment()
        {
            ViewBag.Title = "Environment";
            return View();
        }
    }
}