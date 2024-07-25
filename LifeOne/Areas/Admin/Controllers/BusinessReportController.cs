using LifeOne.Filters;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
   
    [SessionTimeoutAttributeAdmin]
    public class BusinessReportController : Controller
    {
        // GET: Admin/BusinessReport
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CurrentBusinessReportDetail()
        {
            if (!PermissionManager.IsActionPermit("Franchise Business Summary", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            ViewBag.FranchiseDll = DALBindCommonDropdown.BindDropdown(11, 0);
            DALBusinessReport obj = new DALBusinessReport();
            AdminBusinessReportModel objmodel = new AdminBusinessReportModel();
            var result = obj.BusinessList(obj);
            objmodel.AdminBusinessReportModellst = result;
            return View(objmodel);
        }
        [HttpPost]
        public ActionResult CurrentBusinessReportDetail(DALBusinessReport model)
         {
            if (!PermissionManager.IsActionPermit("Franchise Business Summary", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.FranchiseDll = DALBindCommonDropdown.BindDropdown(11, 0);
            DALBusinessReport obj = new DALBusinessReport();
            AdminBusinessReportModel objmodel = new AdminBusinessReportModel();
            ViewBag.fromDate = model.Fromdate;
            ViewBag.ToDate = model.ToDate;
            ViewBag.LoginId = model.LoginId;
            var result = obj.BusinessList(model);
            objmodel.AdminBusinessReportModellst = result;
            return View(objmodel);
        }

        public JsonResult GetFranchiseOrderDetail(string InvoiceId)
        {
            DALBusinessReport obj = new DALBusinessReport();
            var result = obj.ProductDetail(InvoiceId);



            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TotalBusinessReport()
        {

            return View();
        }
    }
}