using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using System;      
using System.Collections.Generic;  
using System.Linq;       
using System.Web;
using System.Web.Mvc;
using LifeOne.Filters;  
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using ClosedXML.Excel;
using System.IO;
using System.Data;
using LifeOne.Models.Manager;

namespace LifeOne.Areas.Admin.Controllers
{
     [SessionTimeoutAttributeAdmin]
    public class AdvancePaymentController : Controller
    {
        // GET: Admin/AdvancePayment
        AdvancePaymentService _Obj = new AdvancePaymentService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdvancePaymentDetails()
        {
            if (!PermissionManager.IsActionPermit("Advance Payment Details", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }
        public JsonResult GetAllAdvancePaymentRequest(CommonRequestModel req)
        {
            return Json(_Obj.GetAllAdvancePaymentRequest(req));
        }
        public JsonResult ApproveRequest(string Action, string Narration, int ReqId = 0)
        {
            return Json(_Obj.ApproveRequest(Action, Narration, ReqId));
        }

        public JsonResult SaveAdvancePaymentDetails(AdvancePaymentApprovalDetails _data)
        {
            try
            {
                if (!PermissionManager.IsActionPermit("Advance Payment Details", ViewOptions.FormView))
                {
                    return Json(new MSimpleString { flag = 0, Message = "Sorry you are not authorised!!!" });
                }
                return Json(_Obj.SaveAdvancePaymentDetails(_data));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult ViewAdvancePaymenthistory(int ReqId = 0)
        {
            try
            {
                return Json(_Obj.ViewAdvancePaymenthistory(ReqId));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult ExportToExcelAdvancePaymentDetails(CommonRequestModel req)
        {
            DataTable dt = new DataTable("Advance Payment Details");
            dt.Columns.AddRange(new DataColumn[8] {
                                                     new DataColumn("Request No."),
                                                     new DataColumn("Associate Name / Id"),
                                                     new DataColumn("Request Amt."),
                                                     new DataColumn("Return Type"),
                                                     new DataColumn("EMI Option"),
                                                     new DataColumn("Month/Amount"),
                                                     new DataColumn("Request Dt."),
                                                     new DataColumn("Status")


            });
            req.Page = 1;
            var customers = _Obj.GetAllAdvancePaymentRequest(req);
            if (customers.DataList.Count > 0)
            {
                foreach (var customer in customers.DataList)
                {
                    dt.Rows.Add(customer.RequestId, customer.Name, customer.RequestAmt, customer.ReturnType, customer.EMIOption, customer.EMIOption == "Month" ? customer.Month : customer.Amount, customer.RequestDateStr, customer.Status);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AdvancePaymentDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/Allwalletdetails");
            }
            // return View();
        }
    }
}