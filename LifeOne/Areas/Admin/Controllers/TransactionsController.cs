using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class TransactionsController : Controller
    {
        // GET: Admin/Transactions
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RequestDetails(RequestDetails _model)
        {
            if (_model.Page == 0)
            {
                _model.Page = 1;
            }
            RequestDetails request = new RequestDetails();
            TransactionService objservice = new TransactionService();
            request.lstRequest = objservice.RequestDetails(_model);
            int totalRecords = 0;
            if (request.lstRequest.Count > 0)
            {
                totalRecords = Convert.ToInt32(request.lstRequest[0].TotalRecord);
                var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                request.Pager = pager;
            }
            else
            {
                request.lstRequest = new List<RequestDetails>();
            }
            return View(request);
        }

        [HttpPost]
        public JsonResult ApproveRequest(string type, string status, string remark, long Fk_MemId)
        {


            TransactionsResponse response = new TransactionsResponse();
            try
            {
                TransactionService objService = new TransactionService();
                response = objService.ApproveRequest(type, status, remark, Fk_MemId);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportToExcelRequestDetails(RequestDetails _model)
        {
            if (_model.Page == 0)
                _model.Page = 1;
            TransactionService objservice = new TransactionService();
            var customers = objservice.RequestDetails(_model);


            DataTable dt = new DataTable("Request Details");
            dt.Columns.AddRange(new DataColumn[8] {
                                                     new DataColumn("LoginId"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Joining Date"),
                                                     new DataColumn("Request Date"),
                                                     new DataColumn("Associate Remark"),
                                                     new DataColumn("Franchise Remark")


            });
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(
                    customer.LoginId,
                    customer.Name,
                    customer.Email,
                    customer.Mobile,
                    customer.JoiningDate,
                    customer.RequestDate,
                    customer.AssociateRemark,
                    customer.FranchiseRemark
                    );

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RequestDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/RequestDetails");
            }
            // return View();
        }

    }
}