using iTextSharp.text.pdf.qrcode;
using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.API;
using LifeOne.Models.AssociateManagement.AssociateService;
using ClosedXML.Excel;
using System.IO;
using System.Data;
using DocumentFormat.OpenXml.EMMA;
using LifeOne.Models;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class WalletController : Controller
    {
        // GET: Admin/Wallet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult WalletRequest(MAdminWallet model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Wallet Request Approval", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            MAdminWallet _result = new MAdminWallet();
            try
            {

                WalletService _objService = new WalletService();

                _result.objList = _objService.GetWalletService(model);
                if (model.DisplayName != null)
                {
                    _result.DisplayName = model.DisplayName;
                }
                if (model.LoginId != null)
                {
                    _result.LoginId = model.LoginId;
                }
                if (model.TransactionNo != null)
                {
                    _result.TransactionNo = model.TransactionNo;
                }
                int totalRecords = 0;
                if (_result.objList.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_result.objList[0].TotalRecords);
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    _result.Pager = pager;
                }
                else
                {
                    _result.objList = new List<MAdminWallet>();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }

        public ActionResult ApproveRequest(int Id)
        {
            MAdminWallet model = new MAdminWallet();
            model.Pk_RequestId = Id;
            ResponseBankModel _result = new ResponseBankModel();
            try
            {
                WalletService _objService = new WalletService();
                _result = _objService.ApproveRequestService(model);
                if (_result != null)
                {
                    TempData["code"] = _result.Flag;
                    TempData["msg"] = _result.Msg;
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
            return Redirect("/WalletRequest");
        }

        public JsonResult DeclineRequest(MAdminWallet model)
        {
            ResponseBankModel _result = new ResponseBankModel();
            try
            {

                WalletService _objService = new WalletService();

                _result = _objService.DeclineRequestService(model);
                if (_result != null)
                {

                    TempData["code"] = _result.Flag;
                    TempData["msg"] = _result.Msg;
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
            return Json(_result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult FranchisePaymentRequest(MAdminWallet model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Franchise Payment Request Approval", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            MAdminWallet _result = new MAdminWallet();
            try
            {
                WalletService _objService = new WalletService();
                _result.objList = _objService.GetFranchisePaymentService(model);
                if (model.DisplayName != null)
                {
                    _result.DisplayName = model.DisplayName;
                }
                if (model.LoginId != null)
                {
                    _result.LoginId = model.LoginId;
                }
                if (model.TransactionNo != null)
                {
                    _result.TransactionNo = model.TransactionNo;
                }
                if (_result.objList.Count > 0)
                    _result.Pager = new Pager(Convert.ToInt32(_result.objList[0].TotalRecords), model.Page, SessionManager.Size);
                else
                {
                    _result.objList = new List<MAdminWallet>();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }

        public ActionResult ApproveFranchisePaymentRequest(int Id)
        {
            MAdminWallet model = new MAdminWallet();
            model.Pk_RequestId = Id;
            ResponseBankModel _result = new ResponseBankModel();
            try
            {
                WalletService _objService = new WalletService();
                NotificationService _objService1 = new NotificationService();
                _result = _objService.ApproveFranchisePaymentRequestService(model);

                if (_result != null)
                {
                    if (_result.Flag == 1)
                    {
                        string Message = string.Empty;
                        //add approval message here 

                        string message = SMS.ApprovePaymentMessage(_result.MemberName, _result.LoginID, _result.Password, _result.Pincode);
                        SMS.SendSMS(_result.Mobile, message);

                        Message = "Dear " + _result.MemberName + ", Your payment for pincode(" + _result.Pincode + ") Franchisee is approved by Karyon and LifeOne admin. your Login Id-" + _result.LoginID + "and Password-" + _result.Password + ". Login http://LifeOne.com/. Thanks";
                        string abc = SendNotification.SendNotificationForMobile(_result.DeviceId, "Franchise Payment Approved For Pincode-" + _result.Pincode, Message);
                        var xmldata = "<Notification>";
                        xmldata += "<Notificationdata><Tittle>" + "Franchise Payment Approved For Pincode-" + _result.Pincode + "</Tittle><Message>" + Message + "</Message><NotificationType>" + "Franchise Payment Approve" + "</NotificationType><Fk_MemId>" + _result.Fk_MemId + "</Fk_MemId><Fk_RequestId>" + Id + "</Fk_RequestId><CreatedBy>" + 1 + "</CreatedBy></Notificationdata>";
                        xmldata += "</Notification>";
                        _objService1.SaveNotification(xmldata);
                    }
                    TempData["code"] = _result.Flag;
                    TempData["msg"] = _result.Msg;

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
            return Redirect("/FranchisePaymentRequest");
        }

        public JsonResult DeclineFranchisePaymentRequest(MAdminWallet model)
        {
            ResponseBankModel _result = new ResponseBankModel();
            try
            {

                WalletService _objService = new WalletService();
                _result = _objService.DeclineFranchisePaymentRequestService(model);
                if (_result != null)
                {

                    TempData["code"] = _result.Flag;
                    TempData["msg"] = _result.Msg;
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
            return Json(_result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetFranchiseRequestPaymentHistory(MAdminWallet model)
        {
            MAdminWallet _result = new MAdminWallet();
            WalletService _objService = new WalletService();
            try
            {
                _result.objList = _objService.GetFranchiseRequestPaymentHistory(model);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(_result.objList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Proc_FranchisePaymentRequestApproveUpdateChild(ApproveWalletModel model)
        {
            ResponseBankModel _result = new ResponseBankModel();
            WalletService _objService = new WalletService();
            try
            {
                _result = _objService.Proc_FranchisePaymentRequestApproveUpdateChild(model);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(_result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult VirtualCompanionPaymentRequest(VirtualCompanion model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Virtual Companion Request", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            WalletService _objService = new WalletService();

            model.virtualCompanions = _objService.GetVirtualCompanionPaymentRequest(model);

            if (model.virtualCompanions.Count > 0)
                model.Pager = new Pager(Convert.ToInt32(model.virtualCompanions[0].TotalRecords), model.Page, SessionManager.Size);
            else
            {
                model.virtualCompanions = new List<VirtualCompanion>();
            }
            return View(model);
        }
        public JsonResult CompanionApproveDeclineRequest(ViretualCompanionPaymentRequest model)
        {
            ResponseBankModel _result = new ResponseBankModel();
            try
            {

                WalletService _objService = new WalletService();

                _result = _objService.CompanionApproveDeclineRequestService(model);
                if (_result != null)
                {
                    TempData["code"] = _result.Flag;
                    TempData["msg"] = _result.Msg;
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
            return Json(_result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult VirtualCompanionApproveDeclineDetails(VirtualCompanion model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Virtual Companion Approval History", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            WalletService _objService = new WalletService();

            model.virtualCompanions = _objService.GetVirtualCompanionApproveDecline(model);

            if (model.virtualCompanions.Count > 0)
                model.Pager = new Pager(Convert.ToInt32(model.virtualCompanions[0].TotalRecords), model.Page, SessionManager.Size);
            else
            {
                model.virtualCompanions = new List<VirtualCompanion>();
            }
            return View(model);
        }

        public ActionResult ExportToExcelVirtualCompanionApproveDeclineDetails(VirtualCompanion model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Virtual Companion Approval History", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            try
            {
                WalletService _objService = new WalletService();
                model.virtualCompanions = _objService.GetVirtualCompanionApproveDeclineForExcel(model);
            }
            catch (Exception)
            {
                throw;
            }
            var customers = model.virtualCompanions;
            DataTable dt = new DataTable("VirtualCompanion");
            dt.Columns.AddRange(new DataColumn[9] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Associate Name"),
                                                     new DataColumn("Franchise Name"),
                                                      new DataColumn("Mobile No."),
                                                     new DataColumn("No. Of Share"),
                                                     new DataColumn("Amount"),
                                                     new DataColumn("Payment Date"),
                                                     new DataColumn("Approved Date"),
                                                     new DataColumn("Status")
            });
            if (customers.Count > 0)
            {
                customers.ForEach(customer =>
                {
                    dt.Rows.Add(customer.AssociateId, customer.AssociateName, customer.FranchiseName, customer.MobileNo, customer.NoofShares, customer.Amount, customer.PaymentDate, customer.ApprovedDate, customer.Status);
                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VirtualCompanionApproveDecline.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("VirtualCompanionApproveDeclineDetails", "Wallet");
            }
            // return View();
        }

        [HttpPost]
        public JsonResult GetFranchiseDetailsByPincode(string Pincode)
        {
            var res = WalletService.GetFranchiseDetailByPincode(Pincode);
            return Json(res);
        }

        public ActionResult ExportToExcelFranchisePaymentRequestList(MAdminWallet model, int? Page)
        {

            if (!PermissionManager.IsActionPermit("Franchise Payment Request Approval", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            MAdminWallet _result = new MAdminWallet();
            try
            {
                WalletService _objService = new WalletService();
                _result.objList = _objService.GetFranchisePaymentForExcelService(model);
                if (model.DisplayName != null)
                {
                    _result.DisplayName = model.DisplayName;
                }
                if (model.LoginId != null)
                {
                    _result.LoginId = model.LoginId;
                }
                if (model.TransactionNo != null)
                {
                    _result.TransactionNo = model.TransactionNo;
                }
            }
            catch (Exception)
            {
                throw;
            }
            var customers = _result.objList;
            DataTable dt = new DataTable("FrnchisePaymentRequest");
            dt.Columns.AddRange(new DataColumn[8] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Associate Name"),
                                                     new DataColumn("Comapny Name"),
                                                      new DataColumn("Requested Amount"),
                                                     new DataColumn("Approved Amount"),
                                                     new DataColumn("No Of Payment"),
                                                     new DataColumn("PinCode"),
                                                     new DataColumn("Status")
            });

            if (customers.Count > 0)
            {
                customers.ForEach(customer =>
                {
                    dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.CompanyName, customer.RequestAmount, customer.Amount, customer.PaymentCount, customer.PinCode, customer.PaymentStatus);
                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PaymentRequestDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("FranchisePaymentRequest", "Wallet");
            }
            // return View();
        }

        public ActionResult EWalletLedger()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EWalletLedger(MAdminWallet model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("EWallet Ledger", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            MAdminWallet _result = new MAdminWallet();
            try
            {

                WalletService _objService = new WalletService();

                _result.objList = _objService.GetEWalletLedgerService(model);
                if (model.LoginId != null)
                {
                    _result.LoginId = model.LoginId;
                }
                if (model.IsExport == 1)
                {
                    
                    DataTable dt = new DataTable("EWallet Ledger");
                    dt.Columns.AddRange(new DataColumn[5] {
                                                     new DataColumn("Transaction Date"),
                                                     new DataColumn("Narration"),
                                                     new DataColumn("Credit"),
                                                     new DataColumn("Debit"),
                                                     new DataColumn("Balance")
                                                    

            });
                    if (_result.objList.Count > 0)
                    {
                        foreach (var customer in _result.objList)
                        {
                            dt.Rows.Add(customer.TransactionDate, customer.Narration, customer.Credit, customer.Debit, customer.Balance);

                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EWallet Ledger.xlsx");
                            }
                        }
                    }
                    else
                    {
                        TempData["alert"] = "Data Not Found";
                        return RedirectToAction("EWalletLedger", "Wallet");
                    }

                }
                int totalRecords = 0;
                if (_result.objList.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_result.objList[0].TotalRecords);
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    _result.Pager = pager;
                }
                else
                {
                    _result.objList = new List<MAdminWallet>();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }

    }
}