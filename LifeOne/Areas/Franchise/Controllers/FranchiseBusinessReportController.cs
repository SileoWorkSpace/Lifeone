using ClosedXML.Excel;
using LifeOne.Models.API.DAL;
using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.Common;
using LifeOne.Models;

namespace LifeOne.Areas.Franchise.Controllers
{

   [SessionTimeoutAttributeFranchise]
    public class FranchiseBusinessReportController : Controller
    {
        // GET: Franchise/FranchiseBusinessReport
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BusinessSummary()
        {
            DALFranchiseBusinessReport obj = new DALFranchiseBusinessReport();
            obj.Fk_FranchiseId = Convert.ToInt32(SessionManager.FranchiseFk_MemId);
            var FranchiseBusinessReportList = obj.BusinessReportList(obj);

            return View(FranchiseBusinessReportList);
        }
        public JsonResult BusinessSumaryDetails(string InvoceId)
        {
            AssociateBusinessReport obj = new AssociateBusinessReport();
            var BusinessList = obj.BusinessProductDetail(InvoceId);
            return Json(BusinessList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BusinessSummary(DALFranchiseBusinessReport model)
        {
            DALFranchiseBusinessReport obj = new DALFranchiseBusinessReport();
            ViewBag.fromDate = model.FromDate;
            ViewBag.ToDate = model.ToDate;
            ViewBag.LoginId = model.LoginId;
            obj.Fk_FranchiseId = Convert.ToInt32(SessionManager.FranchiseFk_MemId);
            var FranchiseBusinessReportList = obj.BusinessReportList(obj);

            return View(FranchiseBusinessReportList);
        }
        public ActionResult ExportToExcelBusinessSummaryDetail()
        {
            DALFranchiseBusinessReport obj = new DALFranchiseBusinessReport();
            obj.Fk_FranchiseId = Convert.ToInt32(SessionManager.FranchiseFk_MemId);
            var customers = obj.BusinessReportList(obj);
            DataTable dt = new DataTable("BusinessSummaryDetail");
            dt.Columns.AddRange(new DataColumn[7] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("City"),
                                                     new DataColumn("State"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Paid Amount"),
                                                     new DataColumn("Total PV")
            });

            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.City, customer.State, customer.MobileNo, customer.PaidAmount, customer.Pv);

                }
                dt.Rows.Add("", "", "Total", "", "", "" + customers.Sum(x => x.Amount) + "", "" + customers.Sum(x => x.Pv) + "");
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BusinessSummaryDetail.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("BusinessSummary", "FranchiseBusinessReport");
            }
        }

        public ActionResult GetFranchisePayoutReport(Reports reports)
        {
            reports.LoginId = SessionManager.LoginId.ToString();
            reports.IsPublished = "true";
            if (reports.Page == null)
            {
                reports.Page = 1;
            }
      
            reports.Size = SessionManager.Size;
            DataSet dataSet = reports.GetFranchisePayoutReport();
            reports.dtGetFranchisePayoutReport = dataSet.Tables[0];
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                totalRecords = Convert.ToInt32(reports.dtGetFranchisePayoutReport.Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            return View(reports);
        }





        public JsonResult GetFranchsisePayoutDetails(string FK_FranchiseId, string PayoutNo)
        {

            Reports reports = new Reports();
            try
            {
                reports.FranchiseId = FK_FranchiseId;
                reports.PayoutNo = PayoutNo;
                reports.list = reports.GetFranchsisePayoutDetails(reports);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(reports, JsonRequestBehavior.AllowGet);


        }


    }
}