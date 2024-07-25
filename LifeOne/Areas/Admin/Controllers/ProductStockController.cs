using LifeOne.Filters;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
  
    [SessionTimeoutAttributeAdmin]
    public class ProductStockController : Controller
    {
        // GET: Admin/ProductStock
        public ActionResult ManageProductStock()
        {
            return View();
        }
    }
}