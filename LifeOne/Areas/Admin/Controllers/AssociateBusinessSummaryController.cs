using ClosedXML.Excel;
using LifeOne.Models.API.DAL;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.Common;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class AssociateBusinessSummaryController : Controller
    {
        // GET: Admin/AssociateBusinessSummary
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BusinessSummary(int? page, AssociateBusinessReport model)
        {
            if (!PermissionManager.IsActionPermit("Business Summary", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            int totalRecords = 0;
            AssociateBusinessReport obj = new AssociateBusinessReport();
            if (page == null || page == 0)
            {
                page = 1;
            }
            obj.Page = page;
           // obj.FK_MemId = null; //SessionManager.AssociateFk_MemId.ToString();
            obj.Size = SessionManager.Size;
            obj.FromDate = model.FromDate;
            obj.ToDate = model.ToDate;
            obj.LoginId = model.LoginId;
            obj.AssociateBusinessSummaryList = obj.AssociateBR(obj);
            ViewBag.LoginId = obj.AssociateBusinessSummaryList.Count>0? obj.AssociateBusinessSummaryList[0].LoginId:"0";
            if (obj.AssociateBusinessSummaryList.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.AssociateBusinessSummaryList[0].TotalRecords);
                var pager = new Models.Common.Pager(totalRecords, obj.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.AssociateBusinessSummaryList = new List<Models.API.AssociateBusinessSummaryModel>();
            }
            return View(obj);
            }
        public JsonResult BusinessSumaryDetails(string InvoceId)
        {
            AssociateBusinessReport obj = new AssociateBusinessReport();

            var BusinessList = obj.BusinessProductDetail(InvoceId);
            return Json(BusinessList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExportToExcelBusinessSummary(AssociateBusinessReport obj)
        {
            obj.FK_MemId = SessionManager.AssociateFk_MemId.ToString();
            DataTable dt = new DataTable("BusinessSummary");
            dt.Columns.AddRange(new DataColumn[8] {
                                                     new DataColumn("Invoice No."),
                                                     new DataColumn("LoginId"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("City"),
                                                     new DataColumn("State"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Paid Amount"),
                                                     new DataColumn("Total PV")
            });
            obj.Page = 1;
            obj.Size = SessionManager.Size;
            AssociateBusinessReport objService = new AssociateBusinessReport();
            var customers = objService.AssociateBR(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.InvoiceNo, customer.LoginId, customer.DisplayName, customer.City, customer.State, customer.Mobile, customer.TotalPaidAmount, customer.TotalPV);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BusinessSummary.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("BusinessSummary", "AssociateReport");
            }
        }
    }
   

}