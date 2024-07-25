using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class ManagePerformanceController : Controller
    {
        // GET: Admin/ManagePerformance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdatePerformanceLevel()
        {
            if (!PermissionManager.IsActionPermit("Update Member Performance", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }

        public JsonResult BindCustomerDetailByMobileNO(MFCoutomer obj)
        {
            try
            {
                var result = new DALFranchise().BindCustomerDetailByMobileNO(obj);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult PerfomanceMasterList()
        {
            try
            {
                var result = new DALFranchise().PerfomanceMasterList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult UpdatePerformanceLevel(MSimpleString model)
        {
            
            try
            {
                var result = new DALFranchise().UpdatePerformanceLevel(model);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}