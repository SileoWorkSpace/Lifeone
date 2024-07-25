using LifeOne.Models.HomeManagement.HEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Associate.Controllers
{
    public class AssociateRegistrationController : Controller
    {
        // GET: Associate/AssociateRegistration
        public ActionResult AssociateRegistration()
        {
            MCustomerRegistration _obj = new MCustomerRegistration();
            return View(_obj);
        }

        public ActionResult AssociateCongratulations()
        {
            ViewBag.LoginId = Session["AssociateLoginId"];
            ViewBag.Password = Session["AssociatePassword"];
            ViewBag.Name = Session["AssociateName"];
            return View();
        }
    }
}