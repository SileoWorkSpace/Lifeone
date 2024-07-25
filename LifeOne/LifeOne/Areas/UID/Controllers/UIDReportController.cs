using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Manager;
using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.FranchiseManagement.FService;
using LifeOne.Models.Common;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using LifeOne.Models.API.DAL;

namespace LifeOne.Areas.UID.Controllers
{

    [SessionTimeoutAttribute]
    public class UIDReportController : Controller
    {
        // GET: Associate/AssociateReport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PayoutReport(int? Page, MPayoutReport obj)
        {
            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                obj.LoginId = SessionManager.LoginId;
            }
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayout"] = obj.Objlist;

            return View(obj);
        }

        [HttpPost]
        public JsonResult IncomeReport(MPayoutReport obj)
        {
            int? Page = 1;
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult StatementDetails(int FK_MemId)
        {

            StatementDetails modelRequest = new StatementDetails();
            StatementResponse Objres = new StatementResponse();
            AdminReportsService _objService = new AdminReportsService();

            try
            {
                var res = _objService.GetStatementDetails(FK_MemId);
                if (res != null)
                {
                    Objres.response = "success";
                    Objres.lstStatement = res;
                }
                else
                {
                    Objres.response = "error";

                }
            }
            catch (Exception ex)
            {
                Objres.response = "error";
                Objres.Msg = ex.Message;
            }
            return Json(Objres, JsonRequestBehavior.AllowGet);


        }


        public ActionResult AssociateSupport()
        {
            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }

            ViewBag.Support = DALBindCommonDropdown.BindDropdown(8, 0);
            Support _model = new Support();
            _model.LoginId = SessionManager.LoginId;
            SupportService _objService = new SupportService();
            _model.SupportList = _objService.GetSupportList(_model);
            return View(_model);
        }

        [HttpPost]
        public JsonResult SupportRequest(string Message, int SupporPk_id, string Path)
        {
            SupportResponse objres = new SupportResponse();
            SupportRequest modelRequest = new SupportRequest();
            SupportService _objService = new SupportService();
            modelRequest.Message = Message;
            modelRequest.DocumentPath = Path;
            modelRequest.Fk_SupportId = SupporPk_id;
            modelRequest.Fk_MemId = SessionManager.AssociateFk_MemId;

            try
            {
                var res = _objService.SupportRequest(modelRequest);
                if (res != null && res.Flag == 1)
                {
                    objres.response = "success";
                    objres.Flag = res.Flag;
                }
                else
                {
                    objres.response = "error";

                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.Msg = ex.Message;
            }
            return Json(objres, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PayoutReportExportExcel()
        {
            DataTable dt = new DataTable("PayoutPaymentStatement");
            dt.Columns.AddRange(new DataColumn[13] { new DataColumn("LoginId"),
                                             new DataColumn("Member Name"),
                                            new DataColumn("Payout No"),
                                             new DataColumn("Direct Income"),
                                            new DataColumn("Team Building Bonus(TBB)"),
                                            new DataColumn("Binary Income"),
                                            new DataColumn("Sponsor Royalty"),
                                            new DataColumn("Gross Amount"),
                                            new DataColumn("TDS Amount"),
                                            new DataColumn("Processing Fee"),
                                            new DataColumn("Advance"),
                                            new DataColumn("Adjusted Amount"),
                                            new DataColumn("Net Amount")

            });

            var customers = (List<MPayoutReport>)Session["listPayout"];
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.DirectIncome, customer.TeamBuildingBonus, customer.BinaryIncome, customer.SponsorRoyalty,
                        customer.GrossAmount, customer.TDSAmount, customer.ProcessingFee,
                        customer.Advance, customer.AdjustedAmount, customer.NetAmount
                           );
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Payoutlist.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("PayoutReport");
            }

        }

        public JsonResult BusinessSumaryDetails(string InvoceId)
        {
            AssociateBusinessReport obj = new AssociateBusinessReport();

            var BusinessList = obj.BusinessProductDetail(InvoceId);
            return Json(BusinessList, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult BusinessSummary(int? page, AssociateBusinessReport model)
        {
            int totalRecords = 0;
            AssociateBusinessReport obj = new AssociateBusinessReport();
            if (page == null || page == 0)
            {
                page = 1;
            }
            obj.Page = page;
            obj.FK_MemId = SessionManager.AssociateFk_MemId.ToString();
            obj.Size = SessionManager.Size;
            obj.FromDate = model.FromDate;
            obj.ToDate = model.ToDate;
            obj.LoginId = model.LoginId;
            obj.AssociateBusinessSummaryList = obj.AssociateBR(obj);
            if (obj.AssociateBusinessSummaryList.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.AssociateBusinessSummaryList[0].TotalRecords);
                var pager = new Pager(totalRecords, obj.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.AssociateBusinessSummaryList = new List<Models.API.AssociateBusinessSummaryModel>();
            }            
            return View(obj);
        }
        //[HttpPost]
        //public ActionResult BusinessSummary(AssociateBusinessReport model)
        //{
        //    AssociateBusinessReport obj = new AssociateBusinessReport();
        //    model.FK_MemId = SessionManager.Fk_MemId.ToString();
        //    ViewBag.fromDate = model.FromDate;
        //    ViewBag.ToDate = model.ToDate;
        //    ViewBag.LoginId = model.LoginId;
        //    var resp = obj.AssociateBR(model);
        //    return View(resp);
        //}

        public ActionResult ExportToExcelBusinessSummary(AssociateBusinessReport obj)
        {
            obj.FK_MemId = SessionManager.AssociateFk_MemId.ToString();
            obj.Size = SessionManager.Size;
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
            //obj.Page = 1;
            //obj.Size = SessionManager.Size;
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
                return RedirectToAction("BusinessSummary");
            }
        }

        [HttpGet]
        public ActionResult PayoutLeadger(int? Page)
        {
            MPayoutLeadger _model = new MPayoutLeadger();
            _model.ProdId = 1;
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.Page = Page;
            _model.Size = SessionManager.Size;
            if (SessionManager.Usertype.Trim() == "1")
            {
                _model.LoginId = SessionManager.LoginId;
            }
            _model._objPayoutList = PayoutLeadgerService.AllPayoutLeadger(_model);
            int totalRecords = 0;
            if (_model._objPayoutList.Count > 0)
            {

                totalRecords = Convert.ToInt32(_model._objPayoutList[0].TotalRecord);
                var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                _model.Pager = pager;
            }
            else
            {
                _model._objPayoutList = new List<MPayoutLeadger>();
            }
            return View(_model);
        }

        public ActionResult ExportToExcelPayoutLeadger(int? Page)
        {
            MPayoutLeadger _model = new MPayoutLeadger();
            _model.ProdId = 1;
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.Page = Page;
            _model.Size = SessionManager.Size;
            if (SessionManager.Usertype.Trim() == "1")
            {
                _model.LoginId = SessionManager.LoginId;
            }
            _model._objPayoutList = PayoutLeadgerService.AllPayoutLeadger(_model);

            DataTable dt = new DataTable("ActiveUnActiveMemberList");
            dt.Columns.AddRange(new DataColumn[5] {
                                                     new DataColumn("TransactionDate"),
                                                     new DataColumn("Narration"),
                                                     new DataColumn("CrAmount"),
                                                     new DataColumn("DrAmount"),
                                                     new DataColumn("Balance")


            });
            if (_model._objPayoutList.Count > 0)
            {
                foreach (var customer in _model._objPayoutList)
                {
                    dt.Rows.Add(customer.TransactionDate, customer.Narration, customer.CrAmount, customer.DrAmount, customer.Balance);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ActiveUnActiveMemberListExcel.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("PayoutLeadger");
            }
        }

        [HttpPost]
        public ActionResult PayoutLeadger(MPayoutLeadger _model)
        {
            try
            {
                _model.ProdId = 1;
                _model._objPayoutList = PayoutLeadgerService.AllPayoutLeadger(_model);
                return View(_model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult ViewSupplyItemDetail(string RequestId)
        {
            List<LifeOne.Models.FranchiseManagement.FEntity.MOrder> _obj = FranchiseOrderService.ViewSupplyItemService(long.Parse(RequestId));
            return Json(_obj, JsonRequestBehavior.AllowGet);
        }
    }
}