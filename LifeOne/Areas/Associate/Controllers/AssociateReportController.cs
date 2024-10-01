using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Manager;
using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using AesEncryption;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.FranchiseManagement.FService;
using LifeOne.Models.Common;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using LifeOne.Models.API.DAL;
using LifeOne.Models;
using LifeOne.Models.AdminManagement.ADAL;
using DALCommon = LifeOne.Models.Common.DALCommon;
using System.Net;
using System.Configuration;
using Razorpay.Api;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Bibliography;

namespace LifeOne.Areas.Associate.Controllers
{

    [SessionTimeoutAttribute]

    public class AssociateReportController : Controller
    {
        string RazorPayLiveKey = ConfigurationManager.AppSettings["RazorKey"].ToString();
        string RazorPayLocalKey = ConfigurationManager.AppSettings["RazorKeyLocal"].ToString();
        string RazorPayLocalSecret = ConfigurationManager.AppSettings["RazorSecretLocaL"].ToString();
        string RazorPayLiveSecret = ConfigurationManager.AppSettings["RazorSecret"].ToString();
        string Aeskey = ConfigurationManager.AppSettings["Aeskey"].ToString();
        // GET: Associate/AssociateReport
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult PayoutReport1(int? Page, MPayoutReport obj)
        //{
        //    if (string.IsNullOrEmpty(SessionManager.LoginId))
        //    {
        //        return RedirectToAction("Login", "Home", new { area = "" });
        //    }
        //    else
        //    {
        //        obj.FK_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
        //    }
        //    obj = AdminReportsService.GetAssociatePayoutReport(Page, obj);
        //    ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
        //    Session["listPayout"] = obj.Objlist;

        //    return View(obj);
        //}

        [HttpPost]
        public JsonResult IncomeReport(string LoginId, string PayoutNo)
        {
            MPayoutReport obj = new MPayoutReport();
            obj.LoginId = LoginId;
            obj.PayoutNo = Convert.ToInt16(PayoutNo);
            int? Page = 1;
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public JsonResult StatementDetails(string PayoutNo)
        {

            StatementDetails modelRequest = new StatementDetails();
            StatementResponse Objres = new StatementResponse();
            AdminReportsService _objService = new AdminReportsService();
            string FK_MemId = "";
            try
            {
                FK_MemId = SessionManager.AssociateFk_MemId.ToString();
                var res = _objService.GetStatementDetails(Convert.ToInt32(FK_MemId), PayoutNo);
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

        public JsonResult BindAssociateMessageHistory(int Fk_SupportId)
        {
            try
            {
                SupportService _objService = new SupportService();
                List<SupportHistoryModel> model = new List<SupportHistoryModel>();
                model = _objService.BindAssociateMessageHistory(Fk_SupportId);
                var Finalresult = new { data = model };
                return Json(Finalresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult AddAssociateMessageHistory(SupportHistoryModel model)
        {
            try
            {
                if (model.SupportId > 0 && !String.IsNullOrEmpty(model.Msg))
                {
                    model.MsgBy = 2;
                    model.Fk_MemId = SessionManager.AssociateFk_MemId;
                    model.Fk_MemLoginId = SessionManager.LoginId;
                    SupportService _objService = new SupportService();
                    model = _objService.AddAssociateMessageHistory(model);
                }
                var Finalresult = new { data = model };
                return Json(Finalresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
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
                return RedirectToAction("PayoutLeadger", "ManagePayout");
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

        [HttpGet]
        public ActionResult StatementDetails(string FK_MemId, string PayoutNo, int? Page)
        {
            string MemId = "";
            if (FK_MemId != null)
            {
                MemId = UrlEncodingDecoding.Decode(FK_MemId);
            }
            PayoutNo = UrlEncodingDecoding.Decode(PayoutNo);
            StatementDetails modelRequest = new StatementDetails();
            StatementResponse Objres = new StatementResponse();
            if (Page == null || Page == 0)
            {
                Page = 1;
            }

            Objres.Fk_MemId = Convert.ToInt32(MemId);
            Objres.PayoutNo = Convert.ToInt32(PayoutNo);
            Objres.Page = Page;
            Objres.Size = SessionManager.Size;

            AdminReportsService _objService = new AdminReportsService();
            try
            {
                int totalRecords = 0;
                var res = _objService.GetStatementDetailsForAssociate(Objres.Fk_MemId, Objres.PayoutNo, Convert.ToInt32(Objres.Page), Objres.Size);
                if (res != null)
                {
                    Objres.response = "success";
                    Objres.lstStatement = res;
                }
                else
                {
                    Objres.response = "error";

                }
                if (Objres.lstStatement.Count > 0)
                {

                    totalRecords = Convert.ToInt32(Objres.lstStatement[0].TotalRecords);
                    var pager = new Pager(totalRecords, Objres.Page, SessionManager.Size);
                    Objres.Pager = pager;
                }
                else
                {
                    Objres.lstStatement = new List<StatementDetails>();
                }
            }
            catch (Exception ex)
            {
                Objres.response = "error";
                Objres.Msg = ex.Message;
            }
            return View(Objres);
        }


        public ActionResult IncomeDetails(string LoginId, string PayoutNo)
        {
            string loginId = "";
            if (LoginId != null)
            {

                loginId = UrlEncodingDecoding.Decode(LoginId);
            }
            PayoutNo = UrlEncodingDecoding.Decode(PayoutNo);
            MPayoutReport obj = new MPayoutReport();
            obj.LoginId = loginId;
            obj.PayoutNo = Convert.ToInt16(PayoutNo);
            int? Page = 1;
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            return View(obj);
        }
        [HttpGet]
        public ActionResult AssociatePayoutReport(int? Page, string PlanName)
        {
            MPayoutReport obj = new MPayoutReport();
            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }

            else
            {
                obj.LoginId = SessionManager.LoginId;
                obj.FK_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
            }
            ViewBag.PlanId = PlanName;
            if (PlanName == "1")
            {
                ViewBag.PlanName = "Referral Income";
                obj.IncomeType = "1";
            }
            if (PlanName == "2")
            {
                ViewBag.PlanName = "Matching Income";
                obj.IncomeType = "2";
            }
            if (PlanName == "3")
            {
                ViewBag.PlanName = "Leadership Income";
                obj.IncomeType = "3";
            }
            obj = AdminReportsService.GetAssociatePayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayout"] = obj.Objlist;
            return View(obj);

        }
        [HttpPost]
        public ActionResult AssociatePayoutReport(int? Page, string PlanName, string PayoutNo, string FromDate, string ToDate)
        {
            MPayoutReport obj = new MPayoutReport();
            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                obj.LoginId = SessionManager.LoginId;
                obj.FK_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
            }
            ViewBag.PlanId = PlanName;
            if (PlanName == "1")
            {
                ViewBag.PlanName = "Referral Income";
                obj.IncomeType = "1";
            }
            if (PlanName == "2")
            {
                ViewBag.PlanName = "Matching Income";
                obj.IncomeType = "2";
            }
            if (PlanName == "3")
            {
                ViewBag.PlanName = "Leadership Income";
                obj.IncomeType = "3";
            }
            obj.PayountNo = PayoutNo;
            obj.FromDate = FromDate;
            obj.ToDate = ToDate;
            obj = AdminReportsService.GetAssociatePayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayout"] = obj.Objlist;
            return View(obj);

        }

        public ActionResult AssociateRepurchaseBonus(int? Page, MPayoutReport obj)
        {
            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                obj.LoginId = SessionManager.LoginId;
            }
            obj = AdminReportsService.RepurchasePayoutIncomeReport(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayout"] = obj.Objlist;
            return View(obj);
        }

        public ActionResult AssociateRepurchaseBonusIncome(int? Page, MPayoutReport obj, string PlanName)
        {
            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                obj.LoginId = SessionManager.LoginId;
            }
            if (PlanName == "RBonus")
            {

                ViewBag.IncomeType = "Repurchase Bonus";
                obj.IncomeType = "RBonus";
                ViewBag.planName = PlanName;
            }
            if (PlanName == "HMB")
            {

                ViewBag.IncomeType = "Hybrid Matching Bonus ";
                obj.IncomeType = "HMB";
                ViewBag.planName = PlanName;
            }
            if (PlanName == "LeadershipB")
            {

                ViewBag.IncomeType = "Leadership Bonus Repurchase";
                obj.IncomeType = "LeadershipB";
                ViewBag.planName = PlanName;
            }
            if (PlanName == "HouseFund")
            {

                ViewBag.IncomeType = "House Fund";
                obj.IncomeType = "HouseFund";
                ViewBag.planName = PlanName;
            }
            if (PlanName == "DirectorClub")
            {

                ViewBag.IncomeType = "Director Club";
                obj.IncomeType = "DirectorClub";
                ViewBag.planName = PlanName;
            }
            if (PlanName == "ChairmanClub")
            {

                ViewBag.IncomeType = "Chairman Club";
                obj.IncomeType = "ChairmanClub";
                ViewBag.planName = PlanName;
            }

            if (PlanName == "LAR")
            {

                ViewBag.IncomeType = "LAR Bonus";
                obj.IncomeType = "LAR";
                ViewBag.planName = PlanName;
            }
            if (PlanName == "YearlyBonus")
            {

                ViewBag.IncomeType = "Yearly Bonus";
                obj.IncomeType = "YearlyBonus";
                ViewBag.planName = PlanName;
            }
            obj.FK_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);

            obj = AdminReportsService.RepurchasePayoutIncomeReportType(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayout"] = obj.Objlist;

            return View(obj);
        }


        [HttpGet]
        public ActionResult AssociateUtility()
        {
            AffiliatedUtilityShopping model = new AffiliatedUtilityShopping();
            model.fk_MemId = SessionManager.AssociateFk_MemId;
            model.ActionType = "Recharg";

            List<AffiliatedUtilityShopping> modellist = AdminReportsService.UtilityAfiliatedShopping(model);

            return View(modellist);
        }

        [HttpPost]
        public ActionResult AssociateUtility(AffiliatedUtilityShopping model)
        {
            model.fk_MemId = SessionManager.AssociateFk_MemId;

            List<AffiliatedUtilityShopping> modellist = AdminReportsService.UtilityAfiliatedShopping(model);

            return View(modellist);
        }

        public ActionResult GetBonanzaReward(Reports reports)
        {
            reports.FK_MemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());
            DataSet dataSet = reports.GetBonanzaReward();
            reports.dtGetBonanzaReward = dataSet.Tables[0];
            return View(reports);
        }

        public ActionResult GetActiveMembers(Reports reports)
        {
            reports.LoginId = SessionManager.LoginId;
            DataSet dataSet = reports.GetActiveMembers();
            reports.dtGetActiveMembers = dataSet.Tables[0];
            return View(reports);
        }
        public ActionResult ActivatedCustomerTaxInvoice(string Pk_TransId)
        {
            ActivatedCustomerTaxInvoice orderConfirmation = new ActivatedCustomerTaxInvoice();
            List<InvoiceProduct> OrderItems = new List<InvoiceProduct>();
            OrderDAL orderDAL = new OrderDAL();
            DataSet dsOrder = orderDAL.ActivatedCustomerInvoiceById(Pk_TransId);
            Int32 TotalAmount = 0;
            if (dsOrder.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsOrder.Tables[0].Rows.Count; i++)
                {
                    orderConfirmation.LoginId = dsOrder.Tables[0].Rows[i]["LoginId"].ToString();
                    orderConfirmation.StateCode = dsOrder.Tables[0].Rows[i]["StateCode"].ToString();
                    orderConfirmation.DisplayName = dsOrder.Tables[0].Rows[i]["DisplayName"].ToString();
                    orderConfirmation.Amount = Convert.ToDecimal(dsOrder.Tables[0].Rows[i]["Amount"].ToString());
                    orderConfirmation.BV = Convert.ToDecimal(dsOrder.Tables[0].Rows[i]["BV"].ToString());
                    orderConfirmation.ActivationDate = dsOrder.Tables[0].Rows[i]["InvoiceDate"].ToString();
                    orderConfirmation.InnvoiceNo = dsOrder.Tables[0].Rows[i]["InnvoiceNo"].ToString();
                    orderConfirmation.CompanyName = dsOrder.Tables[0].Rows[i]["CompanyName"].ToString();
                    orderConfirmation.OfficeAddress = dsOrder.Tables[0].Rows[i]["OfficeAddress"].ToString();
                    orderConfirmation.OfficeEmailId = dsOrder.Tables[0].Rows[i]["OfficeEmailId"].ToString();
                    orderConfirmation.OfficeContactNo = dsOrder.Tables[0].Rows[i]["OfficeContactNo"].ToString();
                    orderConfirmation.StateCode = dsOrder.Tables[0].Rows[i]["StateCode"].ToString();
                    orderConfirmation.CompanyGST = dsOrder.Tables[0].Rows[i]["CompanyGST"].ToString();
                    orderConfirmation.Website = dsOrder.Tables[0].Rows[i]["Website"].ToString();
                    orderConfirmation.Pancard = dsOrder.Tables[0].Rows[i]["Pancard"].ToString();
                    orderConfirmation.HSNNo = dsOrder.Tables[1].Rows[i]["HSNNo"].ToString();


                    orderConfirmation.City = dsOrder.Tables[0].Rows[i]["City"].ToString();
                    orderConfirmation.State = dsOrder.Tables[0].Rows[i]["State"].ToString();
                    orderConfirmation.Pincode = dsOrder.Tables[0].Rows[i]["Pincode"].ToString();
                    orderConfirmation.custGSTNo = dsOrder.Tables[0].Rows[i]["custGSTNo"].ToString();

                    orderConfirmation.custAddress = dsOrder.Tables[0].Rows[i]["custAddress"].ToString();
                    orderConfirmation.custCity = dsOrder.Tables[0].Rows[i]["custCity"].ToString();
                    orderConfirmation.custPincode = dsOrder.Tables[0].Rows[i]["custPincode"].ToString();
                    orderConfirmation.custState = dsOrder.Tables[0].Rows[i]["custState"].ToString();
                    orderConfirmation.mobile = dsOrder.Tables[0].Rows[i]["mobile"].ToString();

                    for (i = 0; i < dsOrder.Tables[1].Rows.Count; i++)
                    {

                        OrderItems.Add(new InvoiceProduct
                        {
                            ProductName = dsOrder.Tables[1].Rows[i]["ProductName"].ToString(),
                            Qty = dsOrder.Tables[1].Rows[i]["Qty"].ToString(),
                            Rate = dsOrder.Tables[1].Rows[i]["Rate"].ToString(),
                            SGST = dsOrder.Tables[1].Rows[i]["SGST"].ToString(),
                            CGST = dsOrder.Tables[1].Rows[i]["CGST"].ToString(),
                            IGST = dsOrder.Tables[1].Rows[i]["IGST"].ToString(),
                            Tax = dsOrder.Tables[1].Rows[i]["Tax"].ToString(),
                            TaxableAmt = dsOrder.Tables[1].Rows[i]["TaxableAmt"].ToString(),
                            SGSTAmt = dsOrder.Tables[1].Rows[i]["SGSTAmt"].ToString(),
                            CGSTAmt = dsOrder.Tables[1].Rows[i]["CGSTAmt"].ToString(),
                            IGSTAmt = dsOrder.Tables[1].Rows[i]["IGSTAmt"].ToString(),
                            TotalTaxAmt = dsOrder.Tables[1].Rows[i]["TaxAmt"].ToString(),
                            TotAmount = dsOrder.Tables[1].Rows[i]["TotAmount"].ToString(),
                            HSNNo = dsOrder.Tables[1].Rows[i]["HSNNo"].ToString(),


                        });



                        TotalAmount += Convert.ToInt32(dsOrder.Tables[1].Rows[i]["TotAmount"]);
                    }


                    orderConfirmation.OrderItems = OrderItems;
                    if (TotalAmount == 0)
                    {
                        ViewBag.TotAmtInWord = "ZERO";
                    }
                    else
                    {
                        ViewBag.TotAmtInWord = DALCommon.ConvertNumbertoWords(TotalAmount);
                    }
                    ViewBag.BVAmountInWords = dsOrder.Tables[0].Rows[0]["BVAmountInWords"].ToString();
                    ViewBag.TotalBV = dsOrder.Tables[0].Rows[0]["TotalBV"].ToString();
                    ViewBag.PayMentMode = dsOrder.Tables[0].Rows[0]["PayMentMode"].ToString();
                    ViewBag.TransactionNo = dsOrder.Tables[0].Rows[0]["TransactionNo"].ToString();
                    ViewBag.BankName = dsOrder.Tables[0].Rows[0]["BankName"].ToString();
                    ViewBag.PaymentDate = dsOrder.Tables[0].Rows[0]["PaymentDate"].ToString();

                }
                return View(orderConfirmation);
            }
            return View(orderConfirmation);
        }

        public ActionResult GetBusinessStatistics(Reports reports)
        {

            reports.FK_MemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());
            DataSet dataSet = reports.GetBusinessSummaryForAssociate();
            reports.dtBusinessDetails = dataSet.Tables[0];
            reports.dtDirectBusiness = dataSet.Tables[1];
            return View(reports);
        }
        public ActionResult AssociateCartList()
        {

            Reports report = new Reports();
            report.OpCode = 1;

            report.FK_MemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());

            DataSet dataSet = report.GetCartList();
            report.dtCartList = dataSet.Tables[0];
           
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    Session["ShoppingPrice"] = double.Parse(dataSet.Tables[0].Compute("sum(price)", "").ToString()).ToString("0.00");
                }
          
           
            return View(report);
        }

        public ActionResult PaymentGatewayIntegration(Reports reports)
        {
            //TotalAmount = "1";
            //Reports reports = new Reports();
            reports.OpCode = 1;
            reports.FK_MemId=int.Parse(SessionManager.AssociateFk_MemId.ToString());
            Session["Pk_AddressId"] = reports.Pk_AddressId;
            Session["WalletAmount"] = reports.WalletAmount;
           // DataSet dataSet = reports.GetCartList();
            //string TotalAmount = double.Parse(dataSet.Tables[0].Compute("sum(price)", "").ToString()).ToString("0.00");
            if (reports.GatewayAmount>0)
            {
                Session["RazorPayTotalAmt"] = reports.GatewayAmount;
                
                string orderId = "";

                Razorpay.Api.Order objorder = null;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RazorpayClient client = null;
                client = new RazorpayClient(RazorPayLocalKey, RazorPayLocalSecret);
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", (reports.GatewayAmount) * 100);
                options.Add("receipt", "");
                options.Add("currency", "INR");
                options.Add("payment_capture", 1);
                objorder = client.Order.Create(options);
                orderId = objorder["id"].ToString();


                reports.OrderNo = orderId;
                reports.RazorPayAmount = ((reports.GatewayAmount) * 100).ToString();
            }
            else
            {
                return RedirectToAction("../AssociateReport/PlaceOrder");

            }
            return View(reports);

        }

        public ActionResult AssociateProductList()
        {
            Products report = new Products()
            {
                Pk_ProductId = 0
            };
            DataSet dataSet = report.GetAllProducts();
            report.DtDetails = dataSet.Tables[0];
            return View(report);
        }
        public ActionResult AssociateCartDelete(string id)
        {
            Reports report = new Reports();
            report.OpCode = 3;
            report.PK_Id = int.Parse(id);
            DataSet dataSet = report.GetCartList();

            return RedirectToAction("/AssociateCartList");
        }
        public ActionResult AssociateProductDetails(string id)
        {
            ProductsDetail productDetails = new ProductsDetail();
            Products products = new Products();
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Products");
            }
            DataTable dtImages = new DataTable();
            dtImages.Columns.Add("Images");
            //if (Session["Fk_MemId"] != null)
            //{
            //    products.Fk_MemId = Session["Fk_MemId"].ToString();
            //}

            products.Pk_ProductId = int.Parse(AesEncryption.Crypto.Decryption(Aeskey, id));
            DataSet dataSet = products.GetAllProducts();
            productDetails.CategoryName = dataSet.Tables[0].Rows[0]["CategoryName"].ToString();
            productDetails.ProductName = dataSet.Tables[0].Rows[0]["ProductName"].ToString();
            productDetails.MRP = dataSet.Tables[0].Rows[0]["MRP"].ToString();
            productDetails.OfferedPrice = dataSet.Tables[0].Rows[0]["OfferedPrice"].ToString();
            // productDetails.ProductImage = dataSet.Tables[0].Rows[0]["ProductImage"].ToString();
            productDetails.ProductDescription = dataSet.Tables[0].Rows[0]["ProductDescription"].ToString();
            productDetails.BalanceQuantity = dataSet.Tables[0].Rows[0]["BalanceQuantity"].ToString();
            productDetails.Pk_ProductId = int.Parse(dataSet.Tables[0].Rows[0]["Pk_ProductId"].ToString());
            productDetails.DP = decimal.Parse(dataSet.Tables[0].Rows[0]["DP"].ToString());

            string[] img = dataSet.Tables[0].Rows[0]["ProductImage"].ToString().Split(',');
            for (int i = 0; i < img.Length; i++)
            {
                dtImages.Rows.Add(img[i]);
            }
            productDetails.dtImages = dtImages;
            return View(productDetails);
        }
        [HttpPost]
        public ActionResult AssociateProductDetails(ProductsDetail productsDetail, string Add)
        {
            HttpCookie loginCookie123 = Request.Cookies["LoginData"];
            if (loginCookie123 != null)
            {
            }

            else
            {
                if (!string.IsNullOrEmpty(Add))
                {
                    Session["Quantity"] = productsDetail.Quantity;
                    Session["Pk_ProductId"] = productsDetail.Pk_ProductId;
                    if (SessionManager.AssociateFk_MemId.ToString() != "")
                    {
                        Reports report = new Reports();
                        report.OpCode = 2;
                        report.Pk_ProductId = int.Parse(Session["Pk_ProductId"].ToString());
                        report.FK_MemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());
                        report.Quantity = int.Parse(Session["Quantity"].ToString());
                        DataSet dataSet = report.GetCartList();
                        return RedirectToAction("../AssociateReport/AssociateCartList");
                    }

                }
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult GetAssociateAddress()
        {

            Reports report = new Reports();
            report.FK_MemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());
            DataSet dataSet = report.GetAssociateAddress();
            report.dtaddressdetails = dataSet.Tables[0];
            DataSet dataset1 = report.GetWalletAmount();
            report.WalletAmount = decimal.Parse(dataset1.Tables[0].Rows[0]["WalletAmount"].ToString());
            report.TotalAmount = decimal.Parse(Session["ShoppingPrice"].ToString());
            if (report.TotalAmount > report.WalletAmount)
            {
                report.GatewayAmount = report.TotalAmount - report.WalletAmount;
            }
            else
            {
                report.WalletAmount = report.TotalAmount;
                report.GatewayAmount = 0;
            }
            
            return View(report);  
        }
        public ActionResult PlaceOrder(string paymentid)
        {
            DataTable dtPaymentDetails = new DataTable();
            dtPaymentDetails.Columns.Add("PaymentMode");
            dtPaymentDetails.Columns.Add("Amount");

            Reports reports = new Reports();
            reports.paymentId = paymentid;
            reports.Token = SessionManager.TokenNo.ToString();
            reports.FK_MemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());
            reports.Pk_AddressId = Session["Pk_AddressId"].ToString();
            dtPaymentDetails.Rows.Add("Gateway", Session["RazorPayTotalAmt"].ToString());
           
            reports.dtPaymentDetails = dtPaymentDetails;
            DataSet dataSet = reports.PlaceOrderForWeb();
            if (dataSet != null)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        ViewBag.Msg = "Order Placed Successfully.";
                        reports.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
                        reports.paymentId = paymentid;
                    }
                    else
                    {
                        ViewBag.Msg = dataSet.Tables[0].Rows[0]["Msg"].ToString();
                        reports.paymentId = paymentid;
                    }
                }
            }
            return View(reports);
        }
        public ActionResult AssociateOrderDetails(Reports reports)
        {
            if(reports.Page==0 || reports.Page==null)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            reports.FK_MemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());
            DataSet dataSet = reports.GetShoppingOrderDetails();
            reports.dtGetShoppingOrderDetails = dataSet.Tables[0];
            return View(reports);

        }

        public ActionResult AssociateAddress(string id)
        {

            Reports report = new Reports();
            if (id != null)
            {
                report.Pk_AddressId = id;
                try
                {
                    report.FK_MemId = int.Parse(SessionManager.Fk_MemId.ToString());
                    DataSet dataSet = report.GetAssociateAddress();
                    report.dtaddressdetails = dataSet.Tables[0];
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return View(report);
        }

        [HttpPost]
        public ActionResult AssociateAddress(Reports report, string Save)
        {



            try
            {
                DataSet _result = new DataSet();

                report.FK_MemId = int.Parse(SessionManager.Fk_MemId.ToString());
                if (Save == "Save")
                {
                    _result = report.AssociateAddAddress(report);
                }
                else
                {
                    _result = report.AssociateAddressEdit(report);
                }
                if (_result != null)
                {
                    return Redirect("/Associate/AssociateReport/GetAssociateAddress");
                }

                else
                {
                    TempData["code"] = "0";
                    TempData["msg"] = "Can not process the request";

                }

            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }
        public ActionResult GetRecognition(Reports reports)
        {
            reports.FK_MemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());
            DataSet dataSet = reports.GetRecognition();
            reports.dtGetRecognition = dataSet.Tables[0];
            return View(reports);

        }

        public ActionResult Rewardspage(Modelreward obj)
        {
             
            List<Modelreward> listdata = new List<Modelreward>();
            try
            {


                obj.FkMemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());
                listdata = obj.RewardDetailsForAdmin();
                obj.Rewardslst = listdata;
            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);
        }

        public ActionResult PinRecognition(Modelreward obj)
        {
            List<Modelreward> listdata = new List<Modelreward>();
            try
            {
                obj.FkMemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());
                listdata = obj.GetPinRecognition();
                obj.Rewardslst = listdata;
            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);
        }

        public ActionResult MatchingBonus()
        {
            ModelMatchingBonus obj = new ModelMatchingBonus();
            obj.FK_MemID = SessionManager.AssociateFk_MemId.ToString();
            try
            {
            
                List<ModelMatchingBonus> listmaching = new List<ModelMatchingBonus>();
                DataSet ds = obj.GetMachingBonus();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // member id 36
                    ModelMatchingBonus databonus = new ModelMatchingBonus();
                    databonus.PayoutNo = dr["PayoutNo"].ToString();
                    databonus.MatchingBonus = dr["MatchingBonus"].ToString();
                    databonus.GrossAmount = dr["GrossAmount"].ToString();
                    databonus.TDSAmount = dr["TDSAmount"].ToString();
                    databonus.ProcessingCharges = dr["ProcessingFee"].ToString();
                    databonus.Advance = dr["Advance"].ToString();
                    databonus.AdjustedAmount = dr["AdjustedAmount"].ToString();
                    databonus.NetAmount = dr["NetAmount"].ToString();
                    //databonus.PerformanceBonus = Convert.ToDecimal(dr["performancebonus"].ToString());
                    databonus.ClosingDate = dr["closingdate"].ToString();
                    databonus.PreviousLeft = Convert.ToDecimal(dr["PreviousLeft"].ToString());
                    databonus.PreviousRight = Convert.ToDecimal(dr["PreviousRight"].ToString());
                    databonus.CurrentLeft = Convert.ToDecimal(dr["CurrentLeft"].ToString());
                    databonus.CurrentRight = Convert.ToDecimal(dr["CurrentRight"].ToString());
                    databonus.BalanceLeft = Convert.ToDecimal(dr["BalanceLeft"].ToString());
                    databonus.BalanceRight = Convert.ToDecimal(dr["BalanceRight"].ToString());
                    listmaching.Add(databonus);
                }
                obj.listMatchingBonus = listmaching;
            }
            catch (Exception)
            {
                throw;
            }
            return View(obj); 
        }

        public ActionResult PerformanceBonus()
        {
            ModelMatchingBonus obj = new ModelMatchingBonus();
            obj.FK_MemID = SessionManager.AssociateFk_MemId.ToString();
            try
            {
                                
                List<ModelMatchingBonus> listmaching = new List<ModelMatchingBonus>();
                DataSet ds = obj.GetPerformanceBonus();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ModelMatchingBonus objdata = new ModelMatchingBonus();
                    objdata.PayoutNo = dr["PayoutNo"].ToString();
                    objdata.MatchingBonus = dr["MatchingBonus"].ToString();
                    objdata.GrossAmount = dr["GrossAmount"].ToString();
                    objdata.TDSAmount = dr["TDSAmount"].ToString();
                    objdata.ProcessingCharges = dr["ProcessingFee"].ToString();
                    objdata.Advance = dr["Advance"].ToString();
                    objdata.AdjustedAmount = dr["AdjustedAmount"].ToString();
                    objdata.NetAmount = dr["NetAmount"].ToString();
                    objdata.PerformanceBonus = Convert.ToDecimal(dr["performancebonus"].ToString());
                    objdata.ClosingDate = dr["closingdate"].ToString();
                    objdata.PreviousLeft = Convert.ToDecimal(dr["PreviousLeft"].ToString());
                    objdata.PreviousRight = Convert.ToDecimal(dr["PreviousRight"].ToString());
                    objdata.CurrentLeft = Convert.ToDecimal(dr["CurrentLeft"].ToString());
                    objdata.CurrentRight = Convert.ToDecimal(dr["CurrentRight"].ToString());
                    objdata.BalanceLeft = Convert.ToDecimal(dr["BalanceLeft"].ToString());
                    objdata.BalanceRight = Convert.ToDecimal(dr["BalanceRight"].ToString());
                    listmaching.Add(objdata);

                }
                obj.listMatchingBonus = listmaching;

            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);
        }
        public ActionResult PayoutReport(int? Page, MPayoutReport obj)
        {
            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            else
            {
                obj.FK_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
            }

            List<MPayoutReport> listmPayout = new List<MPayoutReport>();
            DataSet ds = obj.GetPayoutReport();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                // member id 36
                MPayoutReport databonus = new MPayoutReport();
                databonus.PayoutNo = int.Parse(dr["PayoutNo"].ToString());
                databonus.MatchingBonus = decimal.Parse(dr["MatchingBonus"].ToString());
                databonus.GrossAmount = decimal.Parse(dr["GrossAmount"].ToString());
                databonus.TDSAmount = decimal.Parse(dr["TDSAmount"].ToString());
                databonus.ProcessingFee = decimal.Parse(dr["ProcessingFee"].ToString());
                databonus.Advance = decimal.Parse(dr["Advance"].ToString());
                databonus.AdjustedAmount = decimal.Parse(dr["AdjustedAmount"].ToString());
                databonus.NetAmount = decimal.Parse(dr["NetAmount"].ToString());
                //databonus.PerformanceBonus = Convert.ToDecimal(dr["performancebonus"].ToString());
                databonus.ClosingDate = dr["closingdate"].ToString();
                databonus.PayoutStatus = dr["PayoutStatus"].ToString();
                databonus.PrevLeft = Convert.ToDecimal(dr["PreviousLeft"].ToString());
                databonus.PrevRight = Convert.ToDecimal(dr["PreviousRight"].ToString());
                databonus.CurrLeft = Convert.ToDecimal(dr["CurrentLeft"].ToString());
                databonus.CurrRight = Convert.ToDecimal(dr["CurrentRight"].ToString());
                databonus.BalLeft = Convert.ToDecimal(dr["BalanceLeft"].ToString());
                databonus.BalRight = Convert.ToDecimal(dr["BalanceRight"].ToString());
                databonus.DirectIncome = Convert.ToDecimal(dr["DirectIncome"].ToString());
                listmPayout.Add(databonus);
            }
            obj.Objlist=listmPayout;
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
         
            Session["listPayout"] = obj.Objlist;

            return View(obj);
        }
        public ActionResult RewardsandRecogination(RewardsRecognition obj)
        {
            List<RewardsRecognition> listdata = new List<RewardsRecognition>();
            try
            {


                obj.FkMemId = int.Parse(SessionManager.AssociateFk_MemId.ToString());
                listdata = obj.RewardsandRecognitionDetails();
                obj.RewardsDetail = listdata;
            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);

            // return View();
        }
        public ActionResult ShoppingOrderTaxInvoice(string id)
        {
            ShoppingOrderInvoiceModel orderInvoice = new ShoppingOrderInvoiceModel();
            OrderDAL orderDAL = new OrderDAL();
            DataSet dsOrder = orderDAL.GetShoppingOrderInvoice(id);
            orderInvoice.dtDetails = dsOrder.Tables[0];
            return View(orderInvoice);
        }
    }
}



