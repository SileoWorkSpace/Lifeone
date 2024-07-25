using LifeOne.Models.HomeManagement.HDAL;
using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LifeOne.Controllers
{
    public class ManageAdminController : Controller
    {
        // GET: ManageAdmin
        [HttpGet]
        public ActionResult Login()
        {
            TempData["Msg"] = "";
            Session["PageNo"] = 1;            
            return View();
        }
    }
}