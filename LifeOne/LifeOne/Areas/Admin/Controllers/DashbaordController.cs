using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class DashbaordController : Controller
    {
        // GET: Admin/Dashbaord/Track
        public ActionResult Track()
        {
            return View();
        }
    }
}