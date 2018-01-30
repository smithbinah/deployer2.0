using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deployer2._0.Models;

namespace Deployer2._0.Controllers
{
    public class ContainerController : Controller
    {
        // GET: Container
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
        public ViewResult CreationForm(ContainerModel creationResponse)
        {

            return View("Thanks",creationResponse);
        }
    }
}