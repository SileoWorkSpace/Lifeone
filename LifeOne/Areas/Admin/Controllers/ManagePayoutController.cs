using ClosedXML.Excel;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class ManagePayoutController : Controller
    {
        // GET: Admin/ManagePayoutController/ConsolidatedPayoutReport

        public ActionResult ConsolidatedPayoutReport(MConsolidatedPayoutModel _objParam)
        {
            try
            {

                ViewBag.ddlPayouts = ConsolidatedPayoutService.GetAndBindPayoutDDl();
                _objParam = ConsolidatedPayoutService.GetConsolidatedPayoutService(_objParam);
            }
            catch (Exception)
            {

                throw;
            }
            return View(_objParam);
        }

        [HttpGet]
        public ActionResult PayoutLeadger(int? Page, string LoginId)
        {
            if (!PermissionManager.IsActionPermit("Payout Ledger", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MPayoutLeadger _model = new MPayoutLeadger();
            _model.ProdId = 1;
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.Page = Page;
            _model.Size = SessionManager.Size;

            if (!string.IsNullOrEmpty(LoginId))
            {
                _model.LoginId = LoginId; // Login Id Access to View Profile by Admin
            }
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
                                                     new DataColumn("Transaction Date"),
                                                     new DataColumn("Narration"),
                                                     new DataColumn("Cr Amount"),
                                                     new DataColumn("Dr Amount"),
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


        [HttpGet]
        public ActionResult WalletDetails(int? Page)
        {
            if (!PermissionManager.IsActionPermit("Wallet Detail Ledger", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            CashbackWallet _model = new CashbackWallet();
            return View(_model);
        }

        [HttpPost]
        public ActionResult WalletDetails(int? Page, CashbackWallet _model)
        {
            if (!PermissionManager.IsActionPermit("Wallet Detail Ledger", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            try
            {
                if (Page == null || Page == 0)
                {
                    Page = 1;
                }
                _model.Page = Page;
                _model.Size = SessionManager.Size;
                _model.CashbackWallets = PayoutLeadgerService.GetWalletDetail(_model);
                int totalRecords = 0;
                if (_model.CashbackWallets.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_model.CashbackWallets[0].TotalRecord);
                    var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                    _model.Pager = pager;
                }
                else
                {
                    _model.CashbackWallets = new List<CashbackWallet>();
                }
                return View(_model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult ExportToExcelWalletLedger(int? Page)
        {
            CashbackWallet _model = new CashbackWallet();
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.Page = Page;
            _model.Size = SessionManager.Size;
            _model.CashbackWallets = PayoutLeadgerService.GetWalletDetail(_model);

            DataTable dt = new DataTable("Wallet Detail Ledger");
            dt.Columns.AddRange(new DataColumn[5] {
                                                     new DataColumn("Transaction Date"),
                                                     new DataColumn("Narration"),
                                                     new DataColumn("Cr Amount"),
                                                     new DataColumn("Dr Amount"),
                                                     new DataColumn("Balance")


            });
            if (_model.CashbackWallets.Count > 0)
            {
                foreach (var customer in _model.CashbackWallets)
                {
                    dt.Rows.Add(customer.TransactionDate, customer.Narration, customer.CrAmount, customer.DrAmount, customer.Balance);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "WalletDetailLedger.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/WalletDetails");
            }
        }


        [HttpGet]
        public ActionResult AssociateMonthlyBusinessReport()
        {
            AssociateBusinessReportModel model = new AssociateBusinessReportModel();
            AssociateBusinessReportRepository obj = new AssociateBusinessReportRepository();
           List<AssociateBusinessReportModel> ObjList=obj.ABusinessReport(model);



            return View(ObjList);
        }


        [HttpPost]
        public ActionResult AssociateMonthlyBusinessReport(AssociateBusinessReportModel model)
        {
           
            AssociateBusinessReportRepository obj = new AssociateBusinessReportRepository();
            List<AssociateBusinessReportModel> ObjList = obj.ABusinessReport(model);

            ViewBag.LoginId = model.LoginId;
            ViewBag.FromDate = model.FromDate;
            ViewBag.ToDate = model.ToDate;
            ViewBag.SelfBVFrom = model.SelfBVFrom;
            ViewBag.SelfBVTo = model.SelfBVTo;
            ViewBag.TeamBVFrom = model.TeamBVFrom;
            ViewBag.TeamBVTo = model.TeamBVTo;


            return View(ObjList);
        }



        public ActionResult AssociateBusinessReport(AssociateBusinessModel model)
        {
            if (!PermissionManager.IsActionPermit("DS Direct Business Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (model.Page == null || model.Page == 0)
            {
                model.Page = 1;
            }
            model.Size = SessionManager.Size;
            AssociateBusinessReportRepository obj = new AssociateBusinessReportRepository();



            if (model.IsExport == 1)
            {
                model.Page = 1;
                model.Size = SessionManager.Size;
         
                var customers = obj.AssociateBusinessReport(model);
                DataTable dt = new DataTable("Direct Sellers Direct Business");
                dt.Columns.AddRange(new DataColumn[6] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("No Of Sponsor"),
                                                     new DataColumn("Amount"),
                                                     new DataColumn("Total BV")
                                                   


            });

                if (customers.Count > 0)
                {
                    foreach (var customer in customers)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DispayName,  customer.Mobile, customer.NoOfSponsor, customer.Amount, customer.TotalBV);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Direct Sellers Direct Business.xlsx");
                        }
                    }
                }
                else
                {
                    TempData["alert"] = "Data Not Found";
                    return Redirect("/Members");
                }
            }
            else
            {






                model.LstOrder = obj.AssociateBusinessReport(model);
                int totalRecords = 0;
                if (model.LstOrder.Count > 0)
                {
                    totalRecords = Convert.ToInt32(model.LstOrder[0].TotalRecords);
                    var pager = new Pager(totalRecords, model.Page, model.Size);
                    model.Pager = pager;
                }
                ViewBag.LoginId = model.LoginId;
                ViewBag.FromDate = model.FromDate;
                ViewBag.ToDate = model.ToDate;



                return View(model);
            }
        }



        public ActionResult AssociateRepurchaseBusiness(AssociateBusinessModel model)
        {
            if (!PermissionManager.IsActionPermit("DS Repurchase Business Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (model.Page == null || model.Page == 0)
            {
                model.Page = 1;
            }
            model.Size = SessionManager.Size;
            AssociateBusinessReportRepository obj = new AssociateBusinessReportRepository();



            if (model.IsExport == 1)
            {
                model.Page = 1;
                model.Size = SessionManager.Size;

                var customers = obj.AssociateBusinessRepurchase(model);
                DataTable dt = new DataTable("Direct Sellers Repur. Business");
                dt.Columns.AddRange(new DataColumn[5] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     
                                                     new DataColumn("Self Amount"),
                                                     new DataColumn("Self PV")



            });

                if (customers.Count > 0)
                {
                    foreach (var customer in customers)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DispayName, customer.Mobile,  customer.TotalSelfAmount, customer.TotalSelfPv);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Direct Seller's Repurchase Business.xlsx");
                        }
                    }
                }
                else
                {
                    TempData["alert"] = "Data Not Found";
                    return Redirect("/Members");
                }
            }
            else
            {






                model.LstOrder = obj.AssociateBusinessRepurchase(model);
                int totalRecords = 0;
                if (model.LstOrder.Count > 0)
                {
                    totalRecords = Convert.ToInt32(model.LstOrder[0].TotalRecords);
                    var pager = new Pager(totalRecords, model.Page, model.Size);
                    model.Pager = pager;
                }
                ViewBag.LoginId = model.LoginId;
                ViewBag.FromDate = model.FromDate;
                ViewBag.ToDate = model.ToDate;



                return View(model);
            }
        }

        //*ConsolidatedPayoutReport*/
    }
}