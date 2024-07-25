using LifeOne.Models.HomeManagement.HEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Controllers
{
    public class ManageFranchiseController : Controller
    {

        //ManageFranchise/OrderRequest
        public ActionResult Login()
        {
            Session["PageNo"] = 2;
            return View();
        }

        public ActionResult ForgotPassword(string _)
        {
            return View();
        }
    }
}