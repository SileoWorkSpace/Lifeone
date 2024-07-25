using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FamilyN.Controllers
{
    public class FranchiseController : Controller
    {
        // GET: Franchise


        [HandleError(ExceptionType =typeof(ArgumentOutOfRangeException),View ="Invalid Amount")]
        public ActionResult Index()
        {
            return View();
        }
    }
}