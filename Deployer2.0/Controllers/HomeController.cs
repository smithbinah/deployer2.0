using Deployer2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deployer2._0.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Servers()
        {
            ViewBag.Message = "Under Development";
            VirtualMachineModel testVirtualMachineModel = new VirtualMachineModel { Name="IIS Server", Description= "The Web server" , IPAddress = "10.0.88.55", CPUs = 2, Memory = 8, OperatingSystem = "Windows" };
            return View(testVirtualMachineModel);
        }
    }
}