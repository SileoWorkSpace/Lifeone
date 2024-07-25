using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.AdminManagement.AEntity;

namespace LifeOne.Controllers
{
    public class MigrationController : Controller
    {
        // GET: Migration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MigrateData()
        {
            MigrateDataViewModel objservice = new MigrateDataViewModel();
            var res = objservice.GetValuefromDB1();
            return View();
        }
    }
}