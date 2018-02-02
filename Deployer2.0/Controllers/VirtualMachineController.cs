using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deployer2._0.Models;



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
        public ViewResult CreationForm(VirtualMachineModel creationResponse)
        {

            return View("Thanks", creationResponse);
        }
        public ViewResult Users()
        {
            Console.WriteLine("in");
            DeployerEntities3 deployerEntities3 = new DeployerEntities3();
            return View(from customer in deployerEntities3.AspNetUsers.Take(10) select customer);
        }
        public ViewResult showServers()
        {

            VirtualMachineListModel virtualMachineListModel = new VirtualMachineListModel();
            return View();
        }
    }
}