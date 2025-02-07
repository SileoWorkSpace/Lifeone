using iTextSharp.text.pdf.qrcode;
using ClosedXML.Excel;
using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Manager;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Globalization;
using System.Web.ModelBinding;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateDAL;
using PagedList;
using LifeOne.Models;
using DocumentFormat.OpenXml.Bibliography;
using LifeOne.Models.AdminManagement.ADAL;
using static LifeOne.Models.API.Common;
using Newtonsoft.Json;
using LifeOne.Models.FranchiseManagement.FService;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Runtime.Remoting;
using LifeOne.Models.API; 
using System.Web.UI.WebControls;
using MemberKYC = LifeOne.Models.AdminManagement.AEntity.MemberKYC;
using Support = LifeOne.Models.AdminManagement.AEntity.Support;
using SupportRequest = LifeOne.Models.AdminManagement.AEntity.SupportRequest;
using SupportResponse = LifeOne.Models.AdminManagement.AEntity.SupportResponse;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace LifeOne.Areas.Admin.Controllers
{

    [SessionTimeoutAttributeAdmin]
    public class AdminReportController : Controller
    {
        // GET: Admin/AdminReport
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult FranchiseStockGet(int Fk_Memid)
        {
            MAdminManageStock _result = new MAdminManageStock();
            try
            {

                ManageStockService _objService = new ManageStockService();

                _result.objList = _objService.GetFranchiseStockService(Fk_Memid);

            }
            catch (Exception)
            {
                throw;
            }
            return Json(_result.objList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MemberBankDetails(MAdminMemberBankDetails model, int? Page)
        {

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            MemberBankDetailsService db = new MemberBankDetailsService();
            MAdminMemberBankDetails obj = new MAdminMemberBankDetails();
            obj.Objlist = db.GetMemberBankDetailsService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            int totalRecords = 0;
            if (obj.Objlist.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist = new List<MAdminMemberBankDetails>();
            }
            return View(obj);

        }

        public ActionResult ExportToExcelMemberBankDetails(MAdminMemberBankDetails obj)
        {
            DataTable dt = new DataTable("BankDetails");
            dt.Columns.AddRange(new DataColumn[7] {
                                                     new DataColumn("LoginId"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("MemberAccNo"),
                                                     new DataColumn("MemberBankName"),
                                                     new DataColumn("IFSCCode"),
                                                     new DataColumn("MemberBranch")


            });
            MemberBankDetailsService db = new MemberBankDetailsService();

            //obj.Page = 1;
            obj.Size = SessionManager.Size;

            var customers = db.GetMemberBankDetailsService(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.MemberAccNo, customer.MemberBankName, customer.IFSCCode, customer.MemberBranch);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Member Bank Detail.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("MemberBankDetails", "AdminReport");
            }
            // return View();
        }

        public ActionResult DistributedCommissionToUser(MAdminCommission model, int? Page)
        {

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            CommissionService db = new CommissionService();
            MAdminCommission obj = new MAdminCommission();
            obj.Objlist = db.GetCommisionService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            int totalRecords = 0;
            if (obj.Objlist.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist = new List<MAdminCommission>();
            }
            return View(obj);

        }
        public ActionResult ExportToExcelDistributedCommissionToUser(MAdminCommission obj)
        {
            DataTable dt = new DataTable("Distributed Commission To User");
            dt.Columns.AddRange(new DataColumn[9] {
                                                     new DataColumn("LoginId"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Commission"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("City"),
                                                     new DataColumn("state"),
                                                     new DataColumn("Address"),
                                                      new DataColumn("joiningDate"),


            });
            CommissionService db = new CommissionService();

            // obj.page = 1;
            obj.Size = SessionManager.Size;

            var customers = db.GetCommisionService(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.Amount, customer.Email, customer.City, customer.State, customer.Address, customer.JoiningDate);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Distributed Commission To User.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("DistributedCommissionToUser", "AdminReport");
            }
            // return View();
        }


        public ActionResult UtilityBillPaymentReport(MAdminUtilityBiilpayment model, int? Page, int? FranchiseId, int? Pk_ProductId)
        {
            if (!PermissionManager.IsActionPermit("Recharge/Bill Payment", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (model.Searchby == "TransactionAmount")
            {
                model.TransactionAmount = Convert.ToDecimal(model.SearchParam);
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            model.Size = SessionManager.Size;
            UtilityBiilpaymentService db = new UtilityBiilpaymentService();
            MAdminUtilityBiilpayment obj = new MAdminUtilityBiilpayment();
            obj.Objlist = db.GetUtilityBiilpaymentService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            if (model.TransactionAmount != null || model.TransactionAmount != 0)
            {
                obj.TransactionAmount = model.TransactionAmount;
            }
            if (model.IsExport == 1)
            {
                DataTable dt = new DataTable("Recharge Bill Payment");
                dt.Columns.AddRange(new DataColumn[9] {
                                                     new DataColumn("LoginId"),
                                                     new DataColumn("Direct Seller"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Operator"),
                                                     new DataColumn("Session"),
                                                     new DataColumn("ActionType/OrderNo"),
                                                     new DataColumn("TransactionAmount"),
                                                     new DataColumn("TransactionDate"),
                                                     new DataColumn("Status")



            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.Operator, customer.Session, customer.ActionType + "/" + customer.OrderNo, customer.TransactionAmount, customer.TransactionDate, customer.Status);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Recharge Bill Payment.xlsx");
                        }
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    obj.Pager = pager;
                }
            }


            return View(obj);

        }


        public ActionResult Allwalletdetails(MAdminAllwalletdetails model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("All Wallet Details", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.page = Page;
            AllwalletdetailsService db = new AllwalletdetailsService();
            MAdminAllwalletdetails obj = new MAdminAllwalletdetails();
            obj.Objlist2 = db.GetAllwalletdetailsService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.MemberName != null)
            {
                obj.MemberName = model.MemberName;
            }
            int totalRecords = 0;
            if (obj.Objlist2.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist2[0].TotalRecord);
                var pager = new Pager(totalRecords, model.page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist2 = new List<MAdminAllwalletdetails>();
            }
            return View(obj);

        }
        public ActionResult ExportToExcelAllwalletdetails(MAdminAllwalletdetails obj)
        {
            DataTable dt = new DataTable("All wallet Details");
            dt.Columns.AddRange(new DataColumn[6] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Joining Date"),
                                                     new DataColumn("Bag"),
                                                     new DataColumn("Commission")


            });
            AllwalletdetailsService db = new AllwalletdetailsService();

            // obj.page = 1;
            obj.size = SessionManager.Size;

            var customers = db.GetAllwalletdetailsService(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.MemberName, customer.Mobile, customer.joiningDate, customer.Bag, customer.Commission);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Wallet Details .xlsx");
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
        public ActionResult RazorPaymentSearch(MAdminRazorPaymentSearch model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Razor Pay Payment Details", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.page = Page;
            RazorPaymentSearchService db = new RazorPaymentSearchService();
            MAdminRazorPaymentSearch obj = new MAdminRazorPaymentSearch();
            obj.Objlist2 = db.GetRazorPaymentSearchService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.MemberName != null)
            {
                obj.MemberName = model.MemberName;
            }
            int totalRecords = 0;
            if (obj.Objlist2.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist2[0].ToatlRecord);
                var pager = new Pager(totalRecords, model.page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist2 = new List<MAdminRazorPaymentSearch>();
            }
            return View(obj);

        }
        public ActionResult ExportToExcelRazorPaymentSearch(MAdminRazorPaymentSearch obj)
        {
            DataTable dt = new DataTable("Razor Payment Search");
            dt.Columns.AddRange(new DataColumn[6] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Payment Id"),
                                                     new DataColumn("Member Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Amount"),
                                                     new DataColumn("Date")


            });
            RazorPaymentSearchService db = new RazorPaymentSearchService();

            //obj.page = 1;
            obj.size = SessionManager.Size;

            var customers = db.GetRazorPaymentSearchService(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Payid, customer.MemberName, customer.Mobile, customer.Amount, customer.CreatedDate);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Razor Payment Search.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/RazorPaymentSearch");
            }
            // return View();
        }




        public ActionResult ActiveUnActiveMemberList(MAdminMemberKYC model, int? Page)
        {
            //if (model.Searchby == "LoginId")
            //{
            //    model.LoginId = model.SearchParam;
            //}
            //if (model.Searchby == "Mobile")
            //{
            //    model.Mobile = model.SearchParam;
            //}
            //if (model.Searchby == "Name")
            //{
            //    model.Name = model.SearchParam;
            //}
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            model.size = SessionManager.Size;
            ActiveUnActiveMemberListService db = new ActiveUnActiveMemberListService();
            MAdminMemberKYC obj = new MAdminMemberKYC();
            obj.Objlist = db.GetActiveUnActiveMemberListService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            int totalRecords = 0;
            if (obj.Objlist.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist = new List<MAdminMemberKYC>();
            }
            return View(obj);

        }
        public ActionResult ExportToExcelActiveUnActiveMember(MAdminMemberKYC obj)
        {
            DataTable dt = new DataTable("Active UnActive Member ");
            dt.Columns.AddRange(new DataColumn[5] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("joiningDate"),
                                                     new DataColumn("Status")


            });
            ActiveUnActiveMemberListService db = new ActiveUnActiveMemberListService();

            // obj.page = 1;
            obj.size = SessionManager.Size;

            var customers = db.GetActiveUnActiveMemberListService(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.JoiningDate, customer.Status);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Active UnActive Member.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("ActiveUnActiveMemberList", "AdminReport");
            }
            // return View();
        }

        public ActionResult MagicMoveCommissionList(MAdminIncomeType model, int? Page)
        {

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            GetInComeTypeListService db = new GetInComeTypeListService();
            MAdminIncomeType obj = new MAdminIncomeType();
            model.ProcId = 1;
            obj.Objlist = db.GetInComeTypeService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            int totalRecords = 0;
            if (obj.Objlist.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist = new List<MAdminIncomeType>();
            }
            return View(obj);

        }
        public ActionResult ExportToExcelMagicMoveCommissionList(MAdminIncomeType obj)
        {
            DataTable dt = new DataTable("Magic Move Commission");
            dt.Columns.AddRange(new DataColumn[5] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Member Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Amount"),
                                                     new DataColumn("Remark")


            });
            GetInComeTypeListService db = new GetInComeTypeListService();

            // obj.Page = 1;
            obj.size = SessionManager.Size;
            obj.ProcId = 1;
            var customers = db.GetInComeTypeService(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.Amount, customer.Remark);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Magic Move Commission.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("MagicMoveCommissionList", "AdminReport");
            }
            // return View();
        }

        public ActionResult GetMagicMoveIdWise(long Fk_Memid)
        {

            GetInComeTypeListService db = new GetInComeTypeListService();
            MAdminIncomeType obj = new MAdminIncomeType();

            obj.Objlist = db.GetIncomeIdWise(Fk_Memid);
            return View(obj);

        }

        public ActionResult ReferalIncomeList(MAdminIncomeType model, int? Page)
        {


            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            GetInComeTypeListService db = new GetInComeTypeListService();
            MAdminIncomeType obj = new MAdminIncomeType();
            model.ProcId = 2;
            obj.Objlist = db.GetInComeTypeService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            int totalRecords = 0;
            if (obj.Objlist.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist = new List<MAdminIncomeType>();
            }
            return View(obj);

        }
        public ActionResult ExportToExcelReferalIncomeList(MAdminIncomeType obj)
        {
            DataTable dt = new DataTable("Referal Income");
            dt.Columns.AddRange(new DataColumn[5] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Member Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Amount"),
                                                     new DataColumn("Remark")


            });
            GetInComeTypeListService db = new GetInComeTypeListService();

            // obj.Page = 1;
            obj.size = SessionManager.Size;
            obj.ProcId = 2;
            var customers = db.GetInComeTypeService(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.Amount, customer.Remark);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Referal Income.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("ReferalIncomeList", "AdminReport");
            }
            // return View();
        }

        public ActionResult ReferalIncomeIdWiseList(long Fk_Memid)
        {

            GetInComeTypeListService db = new GetInComeTypeListService();
            MAdminIncomeType obj = new MAdminIncomeType();


            obj.Objlist = db.GetIncomeIdWise(Fk_Memid);
            return View(obj);

        }

        public ActionResult RepurchaseIncomeList(MAdminIncomeType model, int? Page)
        {

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            GetInComeTypeListService db = new GetInComeTypeListService();
            MAdminIncomeType obj = new MAdminIncomeType();
            model.ProcId = 3;
            obj.Objlist = db.GetInComeTypeService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            int totalRecords = 0;
            if (obj.Objlist.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist = new List<MAdminIncomeType>();
            }
            return View(obj);

        }
        public ActionResult ExportToExcelRepurchaseIncomeList(MAdminIncomeType obj)
        {
            DataTable dt = new DataTable("Repurchase Income List");
            dt.Columns.AddRange(new DataColumn[5] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Member Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Amount"),
                                                     new DataColumn("Remark")


            });
            GetInComeTypeListService db = new GetInComeTypeListService();

            // obj.Page = 1;
            obj.size = SessionManager.Size;
            obj.ProcId = 3;
            var customers = db.GetInComeTypeService(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.Amount, customer.Remark);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Repurchase Income List.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("RepurchaseIncomeList", "AdminReport");
            }
            // return View();
        }
        public ActionResult GetRepurchaseIncomeIdWise(long Fk_Memid)
        {

            GetInComeTypeListService db = new GetInComeTypeListService();
            MAdminIncomeType obj = new MAdminIncomeType();

            obj.Objlist = db.GetRepurchaseIncomeIdWiseService(Fk_Memid);
            return View(obj);

        }

        //GetInrDealsTransactionList        
        public ActionResult OtherShopping(MAdminIncomeType model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Affiliate Shopping", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            InrDealsTransactionListService db = new InrDealsTransactionListService();
            MAdminIncomeType obj = new MAdminIncomeType();
            model.ProcId = 1;
            obj.Objlist = db.GetInrDealsTransactionListServic(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            int totalRecords = 0;
            if (obj.Objlist.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist = new List<MAdminIncomeType>();
            }
            return View(obj);

        }
        public ActionResult ExportToExcelOtherShopping(MAdminIncomeType obj)
        {
            DataTable dt = new DataTable("Other Shopping");
            dt.Columns.AddRange(new DataColumn[8] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Member Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("StoreName"),
                                                     new DataColumn("Amount"),
                                                     new DataColumn("TransactionId"),
                                                     new DataColumn("TransactionDate"),
                                                     new DataColumn("Status"),


            });
            InrDealsTransactionListService db = new InrDealsTransactionListService();

            // obj.Page = 1;           
            obj.size = SessionManager.Size;
            obj.ProcId = 3;
            var customers = db.GetInrDealsTransactionListServic(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.StoreName, customer.Amount, customer.TransactionId, customer.TransactionDate, customer.Status);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Other Shopping.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/OtherShopping");
            }
            // return View();
        }

        public ActionResult VendorDetails(MAdminVendorDetails model, int? Page)
        {

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            VendorDetailsService db = new VendorDetailsService();
            MAdminVendorDetails obj = new MAdminVendorDetails();

            obj.Objlist = db.GetVendorDetailsList(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            int totalRecords = 0;
            if (obj.Objlist.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist = new List<MAdminVendorDetails>();
            }
            return View(obj);

        }
        public ActionResult ExportToExcelVendorDetails(MAdminVendorDetails obj)
        {
            DataTable dt = new DataTable("Vendor Details");
            dt.Columns.AddRange(new DataColumn[4] {

                                                     new DataColumn("Member Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("TotalOrders"),
                                                     new DataColumn("Amount")



            });
            VendorDetailsService db = new VendorDetailsService();

            // obj.Page = 1;           
            obj.Size = SessionManager.Size;

            var customers = db.GetVendorDetailsList(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.Name, customer.Mobile, customer.TotalOrders, customer.TotalAmount);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Vendor Details.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("VendorDetails", "AdminReport");
            }
            // return View();
        }

        public ActionResult VendorOrderList(int Id)
        {

            VendorDetailsService db = new VendorDetailsService();
            MAdminVendorDetails obj = new MAdminVendorDetails();
            obj.Fk_VendorId = Id;
            if (obj.Page == 0)
            {
                obj.Page = 1;
            }
            obj.Objlist = db.GetVendorOrderList(obj);
            return View(obj);

        }


        public ActionResult AchieversDetails(MAdminAchieversDetails model, int? Page)
        {

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            AchieversDetailsService db = new AchieversDetailsService();
            MAdminAchieversDetails obj = new MAdminAchieversDetails();

            obj.Objlist = db.GetAchieversDetailsService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            int totalRecords = 0;
            if (obj.Objlist.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist[0].ToatlRecord);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist = new List<MAdminAchieversDetails>();
            }
            return View(obj);

        }
        public ActionResult ExportToExcelAchieversDetails(MAdminAchieversDetails obj)
        {
            DataTable dt = new DataTable("Achievers Details");
            dt.Columns.AddRange(new DataColumn[7] {

                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Member Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("EmailId"),
                                                     new DataColumn("Prize"),
                                                     new DataColumn("Achivement Date"),
                                                     new DataColumn("Status")



            });
            AchieversDetailsService db = new AchieversDetailsService();

            // obj.Page = 1;           
            obj.Size = SessionManager.Size;

            var customers = db.GetAchieversDetailsService(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.Email, customer.Prize, customer.AchievementDate, customer.Status);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Achievers Details.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("AchieversDetails", "AdminReport");
            }
            // return View();
        }

        public ActionResult BlockUnblockDetails(MAdminBlockUnblock model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Block/Unblock Direct Seller", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.Page = Page;
            BlockUnblockService db = new BlockUnblockService();
            MAdminBlockUnblock obj = new MAdminBlockUnblock();

            obj.Objlist = db.GetBlockUnblockService(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.Name != null)
            {
                obj.Name = model.Name;
            }
            if (model.Status != null)
            {
                obj.Status = model.Status;
            }
            if (model.IsExport == 1)
            {
                DataTable dt = new DataTable("Block Unblock Details");
                dt.Columns.AddRange(new DataColumn[5] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("Status")


            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.Email, customer.Status);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Block Unblock Details.xlsx");
                        }
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    obj.Pager = pager;
                }
            }

            return View(obj);

        }

        //public ActionResult ExportToExcelBlockUnblockDetails(MAdminBlockUnblock obj)
        //{

        //    BlockUnblockService db = new BlockUnblockService();

        //    //obj.Page = 1;
        //    obj.Size = SessionManager.Size;

        //    var customers = db.GetBlockUnblockService(obj);

        //    else
        //    {
        //        TempData["alert"] = "Data Not Found";
        //        return Redirect("/BlockUnblockDetails");
        //    }
        //    // return View();
        //}
        public ActionResult TopTenTodaysReferals(MAdminMembers model, int? Page)
        {

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.page = Page;
            TopTenTodaysReferalsService db = new TopTenTodaysReferalsService();
            MAdminMembers obj = new MAdminMembers();

            obj.Objlist = db.GetTopTenTodaysReferals(model);
            if (model.Mobile != null)
            {
                obj.Mobile = model.Mobile;
            }
            if (model.LoginId != null)
            {
                obj.LoginId = model.LoginId;
            }
            if (model.MemberName != null)
            {
                obj.MemberName = model.MemberName;
            }
            int totalRecords = 0;
            if (obj.Objlist.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.Objlist[0].ToatlRecord);
                var pager = new Pager(totalRecords, model.page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.Objlist = new List<MAdminMembers>();
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult BlockUnblockMember(string id)
        {
            BlockUnblockResponse obj = new BlockUnblockResponse();
            try
            {
                BlockUnblockService _objService = new BlockUnblockService();
                obj = _objService.BlockUnblockMemberService(id);
                if (obj != null)
                {

                    TempData["code"] = obj.Flag.ToString();
                    TempData["msg"] = obj.Msg.ToString();
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
            return RedirectToAction("BlockUnblockDetails", "AdminReport");
        }

        [HttpGet]
        public ActionResult GetMemberDirect(long Fk_Memid, int? Page)
        {
            int ProcId = 1;
            MMemberList model = new MMemberList();
            MembersResponse data = new MembersResponse();
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            data.Page = Page;
            model.MemberListDetail = GetDirectMemberService.GetDirectMemberDetailsService(Fk_Memid, ProcId, data.Page, SessionManager.Size);
            int totalRecords = 0;
            if (model.MemberListDetail.Count > 0)
            {

                totalRecords = Convert.ToInt32(model.MemberListDetail[0].ToatlRecord);
                var pager = new Pager(totalRecords, data.Page, SessionManager.Size);
                model.Pager = pager;
            }
            else
            {
                model.MemberListDetail = new List<MMemberList>();
            }
            if (model.MemberListDetail != null && model.MemberListDetail.Count > 0)
            {

                model.MemberListDetail = model.MemberListDetail;
            }
            else
            {

                model.MemberListDetail = model.MemberListDetail;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult GetMemberDownline(long Fk_Memid)
        {
            MMemberList model = new MMemberList();
            model.FK_MemId = Fk_Memid;
            MembersResponse data = new MembersResponse();

            model.MemberListDetail = GetMemberDownlineService.GetMembersDownlineService(model);
            if (model.MemberListDetail != null && model.MemberListDetail.Count > 0)
            {
                data.Response = "success";
                data.List = model.MemberListDetail;
            }
            else
            {
                data.Response = "error";
                data.List = model.MemberListDetail;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult GetMemberDownline(MMemberList model)
        {
            MembersResponse data = new MembersResponse();

            model.MemberListDetail = GetMemberDownlineService.GetMembersDownlineService(model);
            if (model.MemberListDetail != null && model.MemberListDetail.Count > 0)
            {
                data.Response = "success";
                data.List = model.MemberListDetail;
            }
            else
            {
                data.Response = "error";
                data.List = model.MemberListDetail;
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult TDSReport()
        {
            MTDSReport obj = new MTDSReport();
            if (!PermissionManager.IsActionPermit("TDS Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MTDSReport list = AdminReportsService.GetTDSReportService(obj);
            ViewBag.FinancealYear = DALBindCommonDropdown.BindFinancealYear();
            ViewBag.FinancealMonth = DALBindCommonDropdown.BindFinancealMonth();
            Session["TDSReport"] = list.Objlist;

            return View(list);
        }

        [HttpPost]
        public ActionResult TDSReport(MTDSReport model)
        {
            if (!PermissionManager.IsActionPermit("TDS Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
			if (model.Page == 0 || model.Page == null)
			{
				model.Page = 1;
			}
			model.Size = SessionManager.Size;
			MTDSReport list = AdminReportsService.GetTDSReportService(model);
            ViewBag.FinancealYear = DALBindCommonDropdown.BindFinancealYear();
            ViewBag.FinancealYear = DALBindCommonDropdown.BindFinancealYear();
            ViewBag.FinancealMonth = DALBindCommonDropdown.BindFinancealMonth();
            Session["TDSReport"] = model.Objlist;
            ViewBag.loginId = model.LoginId;
            ViewBag.PayoutNo = model.PayoutNo;
            if (model.IsExport == 1)
            {

                model.Objlist = DALAdminReports.GetTDSReport(model);
                DataTable dt = new DataTable("TDS Report");
                dt.Columns.AddRange(new DataColumn[10] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Pancard"),
                                                     new DataColumn("PayoutNo"),
                                                     new DataColumn("GrossAmount"),
                                                      new DataColumn("TdsAmount"),
                                                      new DataColumn("NetAmount"),
                                                       new DataColumn("PayoutDate"),
                                                      new DataColumn("PaidDate")


            });

                if (model.Objlist.Count > 0)
                {
                    foreach (var customer in model.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.Name, customer.Mobile, customer.PanNo, customer.PayoutNo1, customer.GrossAmount, customer.TDS, customer.NetAmount, customer.ClosingDate, customer.PaymentDate);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TDS Report.xlsx");
                        }
                    }
                }
                else
                {
                    TempData["alert"] = "Data Not Found";
                    return RedirectToAction("TDSReport", "AdminReport");
                }

            }

            return View(list);
        }

        [HttpGet]
        public ActionResult PayoutReport(int? Page)
        {
            MPayoutReport obj = new MPayoutReport();
            if (!PermissionManager.IsActionPermit("Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayoutdetails"] = obj.Objlist;
            return View(obj);
        }
        [HttpPost]
        public ActionResult PayoutReport(int? Page, MPayoutReport obj)
        {

            if (!PermissionManager.IsActionPermit("Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayoutdetails"] = obj.Objlist;
            if (obj.IsExport == 1)
            {
                obj.Objlist = DALAdminReports.GetPayoutReport(Page, obj);
                DataTable dt = new DataTable("Payout Income Report");
                dt.Columns.AddRange(new DataColumn[8] {
                new DataColumn("LoginId"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Payout No"),
                                            new DataColumn("Date"),
                                            new DataColumn("Gross Amount"),
                                            new DataColumn("Processing Fee"),
                                            new DataColumn("Net Amount"),
                                            new DataColumn("Payable Amount")

            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.ClosingDate, customer.GrossAmount, customer.ProcessingFee, customer.NetAmount,
                            customer.NetAmount
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Payout Income Report.xlsx");
                        }
                    }
                }
                else
                {
                    TempData["alert"] = "Data Not Found";
                    return Redirect("PayoutReport");
                }

            }
            return View(obj);
        }


        #region Franchise Business
        public ActionResult FranchiseBusiness(int? Page, FranchiseBusinessModel obj)
        {
            if (!PermissionManager.IsActionPermit("Franchisee Business", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetFranchiseBusiness(Page, obj);
            //Session["listFranchiseBusiness"] = obj.Objlist;
            if (obj.IsExport == 1)
            {
                obj.Objlist = DALAdminReports.GetFranchiseBusiness(obj);
                DataTable dt = new DataTable("Franchisee Business");
                dt.Columns.AddRange(new DataColumn[8] {
                                            new DataColumn("LoginId"),
                                             new DataColumn("FRanchise Name"),
                                            new DataColumn("Contact"),
                                            new DataColumn("City"),
                                            new DataColumn("State"),
                                            new DataColumn("Franchise TYpe"),
                                            new DataColumn("BV"),
                                            new DataColumn("Amount")

            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var Fbusinesss in obj.Objlist)
                    {
                        dt.Rows.Add(Fbusinesss.LoginId, Fbusinesss.FranchiseName, Fbusinesss.Contact, Fbusinesss.City, Fbusinesss.P_State, Fbusinesss.FranchiseType, Fbusinesss.BV,
                            Fbusinesss.Amount
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Franchisee Business.xlsx");
                        }
                    }
                }
            }
            return View(obj);
        }

        #endregion
        [HttpGet]
        public ActionResult PayoutReportForBank(int? Page)
        {
            MPayoutReport obj = new MPayoutReport();
            if (!PermissionManager.IsActionPermit("Payout Report For Bank", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            ViewBag.LoginId = obj.MemberLoginId;
            ViewBag.PayoutNo = obj.PayoutNo;
            ViewBag.Iskyc = obj.IsKyc;
            ViewBag.tdate = obj.tdate;
            ViewBag.fdate = obj.fdate;
            obj = AdminReportsService.GetPayoutReportForBankService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["PReportBank"] = obj.Objlist;
            return View(obj);
        }
        [HttpPost]
        public ActionResult PayoutReportForBank(MPayoutReport model, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Payout Report For Bank", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.LoginId = model.MemberLoginId;
            ViewBag.PayoutNo = model.PayoutNo;
            ViewBag.Iskyc = model.IsKyc;
            ViewBag.tdate = model.tdate;
            ViewBag.fdate = model.fdate;

            model = AdminReportsService.GetPayoutReportForBankService(Page, model);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["PReportBank"] = model.Objlist;
            if (model.IsExport == 1)
            {
                model.Objlist = DALAdminReports.GetPayoutReportForBank(model);
                DataTable dt = new DataTable("Payout Report For Bank");
                dt.Columns.AddRange(new DataColumn[11] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("KycStatus"),
                                                     new DataColumn("Mobile No"),
                                                     new DataColumn("Payout No"),
                                                     new DataColumn("Closing Date"),
                                                     new DataColumn("Bank Name"),
                                                     new DataColumn("IFSC Code"),
                                                     new DataColumn("Direct Seller Branch"),
                                                     new DataColumn("Direct Seller AccNo"),
                                                     new DataColumn("Net Income")
            });
                if (model.Objlist.Count > 0)
                {
                    foreach (var customer in model.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.Name, customer.KycStatus, customer.MobileNo, customer.PayountNo, customer.ClosingDate, customer.BankName, customer.IFSCCode, customer.MemberBranch, customer.MemberAccNo, customer.NetIncome);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Payout Report For Bank.xlsx");
                        }
                    }
                }
                else
                {
                    TempData["alert"] = "Data Not Found";
                    return RedirectToAction("PayoutReportForBank", "AdminReport");
                }

            }
            return View(model);
        }
        public ActionResult UpdatePayoutPayment(int? Page, MPayoutReport obj)
        {
            if (!PermissionManager.IsActionPermit("Update Payout Payment", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.PaymentModeList = DALBindCommonDropdown.BindPaymentMode();
            ViewBag.BankList = DALBindCommonDropdown.BindDropdown(9, 0);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();

            var res = AdminReportsService.GetPayoutPayment(Page, obj);
            return View(res);
        }
        public JsonResult UpdatePayoutPaymentDetails(long paidmemberId, Decimal paidAmount, string paymentModeId, string paymentNo = "", string paymentDate = "", int bankId = 0, string branchname = null)
        {
            //DateTime? PaymentDate=null;
            if (paymentModeId == "Cash")
            {
                paymentNo = "";
                paymentDate = null;
                bankId = 0;
                branchname = "";
            }
            if (string.IsNullOrEmpty(paymentDate) == false)
            {
                paymentDate = DsResultModel.ConvertToSystemDate(paymentDate, "dd/MM/yyyy");
            }
            else
            {
                paymentDate = null;
            }
            string ipAddress = "0";
            long userId = SessionManager.Fk_MemId;
            DataSet ds = AdminReportsService.UpdatePayoutPayment(paidmemberId, paidAmount, paymentModeId, paymentNo, paymentDate, bankId, branchname, userId, ipAddress);
            DsResultModel dsResult = DsResultModel.DsToResultModel(ds);
            return Json(dsResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KYCSection(MemberKYC _model)
        {

            if (!PermissionManager.IsActionPermit("KYC Approval", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (_model.Page == 0)
            {
                _model.Page = 1;
            }
            AdminKYCService _Objservice = new AdminKYCService();
            _model.List = _Objservice.GetMemberKYCList(_model);
            int totalRecords = 0;
            if (_model.List.Count > 0)
            {

                totalRecords = Convert.ToInt32(_model.List[0].TotalRecords);
                var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                _model.Pager = pager;
            }
            else
            {
                _model.List = new List<MemberKYC>();
            }

            return View(_model);
        }

        public ActionResult ExportToExcelKYC(MemberKYC obj)
        {
            DataTable dt = new DataTable("KYC List");
            dt.Columns.AddRange(new DataColumn[4] {
                                                     new DataColumn("Name"),
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Status")
            });


            obj.Page = 1;
            //obj.Size = SessionManager.Size;
            AdminKYCService _Objservice = new AdminKYCService();
            var customers = _Objservice.GetMemberKYCList(obj);
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.Name, customer.LoginId, customer.Mobile, customer.Status);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "KYC List.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("KYCSection", "AdminReport");
            }
            // return View();
        }
        public JsonResult ApprveDeclineKYC(string LoginId, string Status, string Remark, string PStatus)
        {
            MemberKYC _model = new MemberKYC();
            _model.LoginId = LoginId;
            _model.Status = PStatus;
            _model.ApproveKyc = Status == "Approve" ? true : false;
            _model.RejectKyc = Status == "Decline" ? true : false;
            _model.Remark = Remark;
            AdminKYCService _Objservice = new AdminKYCService();
            _model.List = _Objservice.ApprveDeclineKYC(_model);
            return Json(_model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KYCApproved()
        {
            MemberKYC _model = new MemberKYC();
            if (_model.Page == 0)
            {
                _model.Page = 1;
            }
            AdminKYCService _Objservice = new AdminKYCService();
            _model.Status = "Approved";
            _model.List = _Objservice.GetMemberKYCList(_model);
            return View(_model);
        }
        public ActionResult KYCDecline()
        {
            MemberKYC _model = new MemberKYC();
            if (_model.Page == 0)
            {
                _model.Page = 1;
            }
            AdminKYCService _Objservice = new AdminKYCService();
            _model.Status = "Rejected";
            _model.List = _Objservice.GetMemberKYCList(_model);
            return View(_model);
        }
        public ActionResult SupportRequest(int? Page, Support _model)
        {

            if (_model.Page == 0)
            {
                _model.Page = 1;
            }
            if (SessionManager.Usertype != "3")
            {
                _model.UserType = Convert.ToInt32(SessionManager.Usertype);
                _model.EmpFk_MemID = Convert.ToInt32(SessionManager.Fk_MemId);
            }
            _model.Size = SessionManager.Size;
            SupportService _objService = new SupportService();
            _model.SupportList = _objService.GetSupportList(_model);
            int totalRecords = 0;
            if (_model.SupportList.Count > 0)
            {

                totalRecords = Convert.ToInt32(_model.SupportList[0].TotalRecords);
                var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                _model.Pager = pager;
            }
            else
            {
                _model.SupportList = new List<Support>();
            }
            return View(_model);
        }

        public ActionResult SupportRequestByLoginId(string LoginId)
        {
            Session["LIds"] = LoginId;
            Support _model = new Support();
            _model.LoginId = LoginId;
            SupportService _objService = new SupportService();
            _model.SupportList = _objService.GetSupportList(_model);
            return View(_model);
        }

        public JsonResult BindSupportQueryByLoginId(string LoginId)
        {
            try
            {
                Support _model = new Support();
                _model.LoginId = LoginId;
                SupportService _objService = new SupportService();
                _model.SupportList = _objService.GetSupportList(_model);
                var Finalresult = new { data = _model.SupportList };
                return Json(Finalresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult CloseRequest(int SupportId)
        {
            try
            {
                SupportService _objService = new SupportService();
                SupportHistoryModel model = new SupportHistoryModel();
                model = _objService.CloseRequest(SupportId);
                var Finalresult = new { data = model };
                return Json(Finalresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
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
                    model.MsgBy = 1;
                    model.Fk_MemId = SessionManager.Fk_MemId;
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


        public ActionResult FranchiseSell(MAdminFranchiseSell obj, long? Id, int? Page)
        {
            //if (SessionManager.Usertype == "3")
            //{
            //    obj.fk_MemId = SessionManager.Fk_MemId;
            //}

            ViewBag.FranchiseDll = DALBindCommonDropdown.BindDropdown(11, 0);
            if (Id == null)
            {

                Id = obj.fk_MemId == null ? 0 : obj.fk_MemId;
            }
            else
            {
                obj.fk_MemId = Id;
            }
            //if (Page == null || Page == 0)
            //{
            //    Page = 1;
            //}
            //model.Page = Page;

            obj.ProcId = 1;
            obj.objList = FranchiseSellService.GetFranchiseSellService(obj);
            obj.fk_MemId = Id;
            //if (model.Mobile != null)
            //{
            //    obj.Mobile = model.Mobile;
            //}
            //if (model.LoginId != null)
            //{
            //    obj.LoginId = model.LoginId;
            //}
            //if (model.Name != null)
            //{
            //    obj.Name = model.Name;
            //}
            //int totalRecords = 0;
            //if (obj.Objlist.Count > 0)
            //{

            //    totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
            //    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
            //    obj.Pager = pager;
            //}
            //else
            //{
            //    obj.Objlist = new List<MAdminMemberBankDetails>();
            //}
            return View(obj);

        }

        public ActionResult FranchiseSellDateWise(MAdminFranchiseSell obj, long? Id, int? Page)
        {
            //if (SessionManager.Usertype == "3")
            //{
            //    obj.fk_MemId = SessionManager.Fk_MemId;
            //}
            if (Id == null)
            {

                Id = obj.fk_MemId == null ? 0 : obj.fk_MemId;
            }
            else
            {
                obj.fk_MemId = Id;
            }
            obj.ProcId = 2;
            //if (Page == null || Page == 0)
            //{
            //    Page = 1;
            //}
            //model.Page = Page;




            obj.objList = FranchiseSellService.GetFranchiseSellService(obj);
            //obj.fk_MemId = Id;
            //if (model.Mobile != null)
            //{
            //    obj.Mobile = model.Mobile;
            //}
            //if (model.LoginId != null)
            //{
            //    obj.LoginId = model.LoginId;
            //}
            //if (model.Name != null)
            //{
            //    obj.Name = model.Name;
            //}
            int totalRecords = 0;
            if (obj.objList.Count > 0)
            {

                totalRecords = Convert.ToInt32(obj.objList[0].TotalRecord);
                var pager = new Pager(totalRecords, obj.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            //else
            //{
            //    obj.Objlist = new List<MAdminMemberBankDetails>();
            //}
            return View(obj);

        }

        public JsonResult FranchiseSellDetailsByInvoiceNo(string Fk_Memid, string InvoiceNo)
        {

            MAdminFranchiseSell obj = new MAdminFranchiseSell();

            try
            {
                obj.fk_MemId = long.Parse(Fk_Memid);
                obj.InvoiceNo = InvoiceNo;

                obj.ProcId = 3;
                obj.objList = FranchiseSellService.GetFranchiseSellService(obj);

            }
            catch (Exception)
            {
                throw;
            }
            return Json(obj.objList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FranchisePurchaseHistory(MAdminFranchisePurchaseHistory obj, long? Id, int? Page)
        {
            ViewBag.FranchiseDll = DALBindCommonDropdown.BindDropdown(11, 0);
            if (SessionManager.Usertype == "3")
            {
                obj.fk_MemId = SessionManager.Fk_MemId;
            }
            if (Id == null)
            {

                Id = obj.fk_MemId == null ? 0 : obj.fk_MemId;
            }
            else
            {
                obj.fk_MemId = Id;
            }
            //if (Page == null || Page == 0)
            //{
            //    Page = 1;
            //}
            //model.Page = Page;



            obj.ProcId = 1;
            obj.objList = FranchisePurchaseHistoryService.GetFranchisePurchaseHistoryService(obj);
            obj.fk_MemId = Id;
            //if (model.Mobile != null)
            //{
            //    obj.Mobile = model.Mobile;
            //}
            //if (model.LoginId != null)
            //{
            //    obj.LoginId = model.LoginId;
            //}
            //if (model.Name != null)
            //{
            //    obj.Name = model.Name;
            //}
            //int totalRecords = 0;
            //if (obj.Objlist.Count > 0)
            //{

            //    totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
            //    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
            //    obj.Pager = pager;
            //}
            //else
            //{
            //    obj.Objlist = new List<MAdminMemberBankDetails>();
            //}
            return View(obj);

        }
        public ActionResult FranchisePurchaseHistoryByInvoice(MAdminFranchisePurchaseHistory obj, long? Id, int? Page)
        {
            if (SessionManager.Usertype != "3")
            {
                obj.fk_MemId = SessionManager.Fk_MemId;
            }
            if (Id == null)
            {

                Id = obj.fk_MemId == null ? 0 : obj.fk_MemId;
            }
            else
            {
                obj.fk_MemId = Id;
            }
            obj.ProcId = 2;
            //if (Page == null || Page == 0)
            //{
            //    Page = 1;
            //}
            //model.Page = Page;




            obj.objList = FranchisePurchaseHistoryService.GetFranchisePurchaseHistoryService(obj);
            obj.fk_MemId = Id;
            //if (model.Mobile != null)
            //{
            //    obj.Mobile = model.Mobile;
            //}
            //if (model.LoginId != null)
            //{
            //    obj.LoginId = model.LoginId;
            //}
            //if (model.Name != null)
            //{
            //    obj.Name = model.Name;
            //}
            //int totalRecords = 0;
            //if (obj.Objlist.Count > 0)
            //{

            //    totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecord);
            //    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
            //    obj.Pager = pager;
            //}
            //else
            //{
            //    obj.Objlist = new List<MAdminMemberBankDetails>();
            //}
            return View(obj);

        }
        [HttpGet]
        public ActionResult DownlinePayoutReport(int? Page)
        {
            MPayoutReport obj = new MPayoutReport();
            if (!PermissionManager.IsActionPermit("Downline Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetDownlinePayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayoutdetails"] = obj.Objlist;
            ViewBag.LoginId = obj.LoginId;
            return View(obj);
        }
        [HttpPost]
        public ActionResult DownlinePayoutReport(MPayoutReport obj, int Page)
        {
            if (!PermissionManager.IsActionPermit("Downline Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetDownlinePayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            obj.Objlist = DALAdminReports.GetDownlinePayoutReport(obj);
            Session["listPayoutdetails"] = obj.Objlist;
            ViewBag.LoginId = obj.LoginId;
            if (obj.IsExport == 1)
            {
                DataTable dt = new DataTable("Downline Payout Report");
                dt.Columns.AddRange(new DataColumn[10] { new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                             new DataColumn("Payout No"),
                                             new DataColumn("Direct Income"),
                                             new DataColumn("Gross Amount"),
                                             new DataColumn("Processing Fee"),
                                             new DataColumn("TDS Amount"),
                                             new DataColumn("Net Amount"),
                                             new DataColumn("Payable Amount"),
                                             new DataColumn("Date")

            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.DirectIncome,
                            customer.GrossAmount, customer.ProcessingFee, customer.TDSAmount, customer.NetAmount, customer.LeadershipBonus, customer.ClosingDate
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Downline Payout Report.xlsx");
                        }
                    }
                }
            }
            return View(obj);
        }


        [HttpPost]
        public JsonResult SaveSupportReply(string Message, int SupporPk_id, string Path)
        {
            SupportResponse objres = new SupportResponse();
            SupportRequest modelRequest = new SupportRequest();
            SupportService _objService = new SupportService();
            modelRequest.Message = Message;
            modelRequest.DocumentPath = Path;
            modelRequest.Fk_SupportId = SupporPk_id;
            modelRequest.Fk_MemId = SessionManager.Fk_MemId;

            try
            {
                var res = _objService.SaveSupportReply(Message, SupporPk_id, Path, modelRequest.Fk_MemId);
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

        //[HttpPost]
        //public JsonResult BlockUnblock(long Fk_MemId, string Purpose, string Remark)
        //{

        //    BlockUnblockResponse obj = new BlockUnblockResponse();
        //    try
        //    {
        //        BlockUnblockService _objService = new BlockUnblockService();
        //        obj = _objService.BlockUnblockMemberServiceV2(Fk_MemId, Purpose, Remark);

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return Json(obj, JsonRequestBehavior.AllowGet);

        //}

        [HttpPost]
        public JsonResult Unblockuser(long Fk_MemId, string Purpose)
        {

            BlockUnblockResponse obj = new BlockUnblockResponse();
            try
            {
                BlockUnblockService _objService = new BlockUnblockService();
                obj = _objService.UnblockUser(Fk_MemId, Purpose);

            }
            catch (Exception)
            {
                throw;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ExportToExcelSupportRequest(int? Page, Support _model)
        {
            if (_model.Page == 0)
            {
                _model.Page = 1;
            }
            _model.Size = SessionManager.Size;
            SupportService _objService = new SupportService();
            var customers = _objService.GetSupportList(_model);
            DataTable dt = new DataTable("Support Request List");
            dt.Columns.AddRange(new DataColumn[6] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Ticket Number"),
                                                     new DataColumn("Status"),
                                                     new DataColumn("Date"),
                                                     new DataColumn("Mobile")
            });

            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.TicketNumber, customer.Status, customer.Date, customer.Mobile);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Support Request List.xlsx");
                    }
                }
            }
            else
            {
                return Redirect("/Admin-SupportRequest");
            }
            // return View();
        }
        public JsonResult BindEmployeSupportList(string LoginId)
        {
            try
            {
                SupportService _objService = new SupportService();
                var res = _objService.BindEmployeSupportList(LoginId);
                var Finalresult = new { data = res };
                return Json(Finalresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult Assign_Employee(string LoginId, string EmployeeId, List<string> SupportRequestList)
        {
            try
            {
                SupportHistoryModel model = new SupportHistoryModel();
                if (!String.IsNullOrEmpty(EmployeeId) && !String.IsNullOrEmpty(EmployeeId) && SupportRequestList.Count > 0)
                {
                    SupportService _objService = new SupportService();
                    string xml = "<result>";
                    foreach (var item in SupportRequestList)
                    {
                        xml += "<data><EmployeeId>" + EmployeeId + "</EmployeeId><SID>" + item + "</SID></data>";
                    }
                    xml += "</result>";
                    model = _objService.Assign_Employee(xml);
                }
                var Finalresult = new { data = model };
                return Json(Finalresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult VerifyPanOrVoterIdDetails(VerifyPanOrVoterid _model)
        {

            if (!PermissionManager.IsActionPermit("Verify KYC", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (!string.IsNullOrEmpty(_model.Name) || !string.IsNullOrEmpty(_model.LoginId) || !string.IsNullOrEmpty(_model.Mobile) || !string.IsNullOrEmpty(_model.VoterIdNo) || !string.IsNullOrEmpty(_model.PanCard))
            {
                _model.Page = 1;
            }
            else
            {
                if (_model.Page == 0)
                {
                    _model.Page = 1;
                }
            }
            _model.size = 50;
            _model.VerifyStatus = "Uploaded";
            AdminKYCService _Objservice = new AdminKYCService();
            var allList = _Objservice.GetMemberPanOrVoterId(_model);
            // _model.List = _Objservice.GetMemberPanOrVoterId(_model);
            List<VerifyPanOrVoterid> allApprovedList = new List<VerifyPanOrVoterid>();


            allApprovedList = allList.ToList();



            var finalList = allApprovedList.ToList();
            _model.List = finalList.ToList();

            ViewBag.loginId = _model.LoginId;
            ViewBag.Mobile = _model.Mobile;
            ViewBag.Pan = _model.PanCard;
            int totalRecords = 0;
            if (_model.List.Count > 0)
            {
                totalRecords = Convert.ToInt32(_model.List[0].TotalRecords);
                //var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                var pager = new Pager(totalRecords, _model.Page, _model.size > 0 ? _model.size : SessionManager.Size);
                _model.Pager = pager;
            }
            else
            {
                _model.List = new List<VerifyPanOrVoterid>();
            }

            return View(_model);
        }
        public ActionResult ApprovePanOrVoterIdDetails(VerifyPanOrVoterid _model)
        {
            if (!PermissionManager.IsActionPermit("Verify KYC", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            if (!String.IsNullOrEmpty(_model.Name) || !String.IsNullOrEmpty(_model.LoginId) || !String.IsNullOrEmpty(_model.Mobile) || !String.IsNullOrEmpty(_model.VoterIdNo) || !String.IsNullOrEmpty(_model.PanCard))
            {
                _model.Page = 1;
            }
            else
            {
                if (_model.Page == 0)
                {
                    _model.Page = 1;
                }
            }
            _model.size = 50;
            _model.VerifyStatus = "Approve";
            AdminKYCService _Objservice = new AdminKYCService();
            _model.List = _Objservice.GetMemberPanOrVoterId(_model);
            ViewBag.loginId = _model.LoginId;
            ViewBag.Mobile = _model.Mobile;
            ViewBag.Pan = _model.PanCard;
            int totalRecords = 0;
            if (_model.List.Count > 0)
            {
                totalRecords = Convert.ToInt32(_model.List[0].TotalRecords);
                var pager = new Pager(totalRecords, _model.Page, _model.size > 0 ? _model.size : SessionManager.Size);
                _model.Pager = pager;
            }
            else
            {
                _model.List = new List<VerifyPanOrVoterid>();
            }

            return View(_model);
        }
        public ActionResult DeclinePanOrVoterIdDetails(VerifyPanOrVoterid _model)
        {
            if (!PermissionManager.IsActionPermit("Verify KYC", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            if (!String.IsNullOrEmpty(_model.Name) || !String.IsNullOrEmpty(_model.LoginId) || !String.IsNullOrEmpty(_model.Mobile) || !String.IsNullOrEmpty(_model.VoterIdNo) || !String.IsNullOrEmpty(_model.PanCard))
            {
                _model.Page = 1;
            }
            else
            {
                if (_model.Page == 0)
                {
                    _model.Page = 1;
                }
            }
            _model.size = 50;
            _model.VerifyStatus = "Decline";
            AdminKYCService _Objservice = new AdminKYCService();
            _model.List = _Objservice.GetMemberPanOrVoterId(_model);
            ViewBag.loginId = _model.LoginId;
            ViewBag.Mobile = _model.Mobile;
            ViewBag.Pan = _model.PanCard;
            int totalRecords = 0;
            if (_model.List.Count > 0)
            {
                totalRecords = Convert.ToInt32(_model.List[0].TotalRecords);
                var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                _model.Pager = pager;
            }
            else
            {
                _model.List = new List<VerifyPanOrVoterid>();
            }

            return View(_model);
        }

        [HttpPost]
        public JsonResult ApprovePanOrVoterId(string Fk_MemId, string Type, string Status, string Remark)
        {
            VerifyPanOrVoterid _result = new VerifyPanOrVoterid();
            try
            {
                _result.Fk_MemId = Convert.ToInt64(Fk_MemId);
                _result.Type = Type;
                _result.Status = Status;
                _result.Remark = Remark;
                AdminKYCService _objService = new AdminKYCService();
                _result = _objService.ApprovePanOrVoterId(_result);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(_result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AdminOrderList(OrderList model, string LoginID, string Fromdate, string Todate)
        {
            if (!PermissionManager.IsActionPermit("Ordered List", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (model.Page == null || model.Page == 0)
            {
                model.Page = 1;
            }
            model.Size = SessionManager.Size;
            model.LoginId = LoginID;
            model.FromDate = Fromdate;
            model.ToDate = Todate;
            ViewBag.LoginId = LoginID;
            ViewBag.Fromdate = Fromdate;
            ViewBag.Todate = Todate;
            AdminOorrderBilling obj = new AdminOorrderBilling();
            model.orderLists = obj.OrderList(model);
            ViewBag.orderList = model.orderLists;
            // Session["OrderList"] = model.orderLists;
            if (model.IsExport == 1)
            {
                DataTable dt = new DataTable("Ordered List");
                dt.Columns.AddRange(new DataColumn[10] {
                                            new DataColumn("Login Id"),
                                             new DataColumn("Member Name"),
                                            new DataColumn("Order No"),
                                            new DataColumn("TotalAmount"),
                                            new DataColumn("Total Qty"),
                                            new DataColumn("Total DP"),
                                            new DataColumn("Total BV"),
                                             new DataColumn("Status"),
                                            new DataColumn("Order Date"),
                                            new DataColumn("Remark")

            });

                if (model.orderLists.Count > 0)
                {
                    foreach (var customer in model.orderLists)
                    {
                        dt.Rows.Add(customer.LoginId, customer.Name, customer.OrderNo, customer.TotalAmount, customer.TotalQty, customer.DP,
                            customer.BV, customer.Status, customer.CreationDate, customer.Remark
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Ordered List.xlsx");
                        }
                    }
                }

            }
            else
            {
                int totalRecords = 0;
                if (model.orderLists.Count > 0)
                {

                    totalRecords = Convert.ToInt32(model.orderLists[0].TotalRecords);
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }

            return View(model);
        }
        //[HttpPost]
        //public ActionResult AdminOrderList(OrderList model)
        //{


        //    AdminOorrderBilling obj = new AdminOorrderBilling();

        //    List<OrderList> lst = obj.OrderList(model);
        //    ViewBag.orderList = lst;

        //    Session["OrderList"] = lst;


        //    else
        //    {


        //        //return View(model);
        //    }
        //     return View(model);


        //}

        //public ActionResult ExportToExcelOrderList(MPayoutReport obj)
        //{
        //    DataTable dt = new DataTable("Order List");
        //    dt.Columns.AddRange(new DataColumn[10] {
        //                                    new DataColumn("Login Id"),
        //                                     new DataColumn("Member Name"),
        //                                    new DataColumn("Order No"),
        //                                    new DataColumn("TotalAmount"),
        //                                    new DataColumn("Total Qty"),
        //                                    new DataColumn("Total DP"),
        //                                    new DataColumn("Total BV"),
        //                                     new DataColumn("Status"),
        //                                    new DataColumn("Order Date"),
        //                                    new DataColumn("Remark")

        //    });

        //    var customers = (List<OrderList>)Session["OrderList"];
        //    if (customers.Count > 0)
        //    {
        //        foreach (var customer in customers)
        //        {
        //            dt.Rows.Add(customer.LoginId, customer.Name, customer.OrderNo, customer.TotalAmount, customer.TotalQty, customer.DP,
        //                customer.BV, customer.Status, customer.CreationDate, customer.Remark
        //                   );
        //        }
        //        using (XLWorkbook wb = new XLWorkbook())
        //        {
        //            wb.Worksheets.Add(dt);
        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                wb.SaveAs(stream);
        //                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Order List.xlsx");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        TempData["alert"] = "Data Not Found";
        //        return Redirect("AdminOrderList");
        //    }
        //}
        public JsonResult DeleteOrder(string OrderNo)
        {
            AdminOorrderBilling obj1 = new AdminOorrderBilling();
            DeleteOrder obj = new DeleteOrder();

            obj.OrderNo = OrderNo.ToString();
            DeleteOrder model = obj1.DeleteOrder(obj);



            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StatementDetails(string FK_MemId, string PayoutNo)
        {

            StatementDetails modelRequest = new StatementDetails();
            StatementResponse Objres = new StatementResponse();
            AdminReportsService _objService = new AdminReportsService();

            try
            {
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


        public JsonResult StatementDetailsByIncomeType(string FK_MemId, string IncomeType)
        {

            StatementDetails modelRequest = new StatementDetails();
            StatementResponse Objres = new StatementResponse();
            AdminReportsService _objService = new AdminReportsService();

            try
            {
                var res = _objService.GetStatementDetailsByIncomeType(Convert.ToInt32(FK_MemId), IncomeType);
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
        [HttpGet]
        public ActionResult DirectIncome(int? Page)
        {
            MPayoutReport obj = new MPayoutReport();
            if (!PermissionManager.IsActionPermit("Direct Income Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayoutdetails"] = obj.Objlist;
            ViewBag.LoginId = obj.LoginId;
            return View(obj);
        }
        [HttpPost]
        public ActionResult DirectIncome(MPayoutReport obj, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Direct Income Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayoutdetails"] = obj.Objlist;
            ViewBag.LoginId = obj.LoginId;
            if (obj.IsExport == 1)
            {
                obj.Objlist = DALAdminReports.GetPayoutReport(Page, obj);
                DataTable dt = new DataTable("Direct Income");
                dt.Columns.AddRange(new DataColumn[10] {
                                             new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                             new DataColumn("Payout No"),
                                             new DataColumn("Direct Income"),
                                             new DataColumn("Gross Amount"),
                                             new DataColumn("Processing Fee"),
                                             new DataColumn("TDS Amount"),
                                             new DataColumn("Current Right"),
                                             new DataColumn("Net Amount"),
                                             new DataColumn("Payable Amount"),
                                             //new DataColumn("Date")

            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.DirectIncome,
                            customer.GrossAmount, customer.ProcessingFee, customer.TDSAmount, customer.NetAmount, customer.LeadershipBonus, customer.ClosingDate
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Direct Income.xlsx");
                        }
                    }
                }
            }
            return View(obj);
        }
        [HttpGet]
        public ActionResult MatchingtIncome(int? Page)
        {
            MPayoutReport obj = new MPayoutReport();
            if (!PermissionManager.IsActionPermit("Matching Income Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayoutdetails"] = obj.Objlist;
            ViewBag.LoginId = obj.LoginId;
            return View(obj);
        }
        [HttpPost]
        public ActionResult MatchingtIncome(MPayoutReport obj, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Matching Income Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayoutdetails"] = obj.Objlist;
            ViewBag.LoginId = obj.LoginId;
            if (obj.IsExport == 1)
            {
                DataTable dt = new DataTable("Matching Income");
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                             new DataColumn("Payout No"),
                                             new DataColumn("Matching Income"),

                                             new DataColumn("Prev Left"),
                                             new DataColumn("Prev Right"),
                                             new DataColumn("Current Left"),
                                             new DataColumn("Current Right"),
                                             new DataColumn("Bal. Left"),
                                             new DataColumn("Bal. Right"),
                                             new DataColumn("Date")

            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.MatchingBonus,
                            customer.PrevLeft, customer.PrevRight, customer.CurrLeft, customer.CurrRight, customer.BalLeft, customer.BalRight, customer.ClosingDate
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Matching Income.xlsx");
                        }
                    }
                }
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult LeaderShipIncome(int? Page)
        {
            MPayoutReport obj = new MPayoutReport();
            if (!PermissionManager.IsActionPermit("Leadership Income Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayoutdetails"] = obj.Objlist;
            ViewBag.LoginId = obj.LoginId;
            return View(obj);
        }
        [HttpPost]
        public ActionResult LeaderShipIncome(int? Page, MPayoutReport obj)
        {
            if (!PermissionManager.IsActionPermit("Leadership Income Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.GetPayoutReportService(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["listPayoutdetails"] = obj.Objlist;
            ViewBag.LoginId = obj.LoginId;
            if (obj.IsExport == 1)
            {
                obj.Objlist = DALAdminReports.GetPayoutReport(Page, obj);
                DataTable dt = new DataTable("Leadership Income");
                dt.Columns.AddRange(new DataColumn[10] { new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                             new DataColumn("Payout No"),
                                             new DataColumn("Leadership Income"),

                                             new DataColumn("Gross Amount"),
                                             new DataColumn("Processing Fee"),
                                             new DataColumn("TDS Amount"),
                                             new DataColumn("Net Amount"),
                                             new DataColumn("Payable Amount"),

                                             new DataColumn("Date")

            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.LeadershipBonus,
                            customer.GrossAmount, customer.ProcessingFee, customer.TDSAmount, customer.NetAmount, customer.PaidAmount, customer.ClosingDate
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Leadership Income.xlsx");
                        }
                    }
                }
            }
            return View(obj);
        }


        [HttpGet]
        public ActionResult UserDownlineReport(string loginId)
        {
            UserDownlineReport model = new UserDownlineReport();
            if (!PermissionManager.IsActionPermit("User Downline Report", ViewOptions.FormView))
            {
                return RedirectToAction("Index", "MainPage");
            }
            //List<UserDownlineReport> objlist = new List<UserDownlineReport>();
            UserDownlineList obj = new UserDownlineList();
            if (loginId != null)
            {


                //string dcdata = aesencryptdecrypt.Decrypt(loginId);
                model.LoginId = loginId;
                model.ObjList = obj.DownlineList(model);
                ViewBag.LoginId = loginId;
                Session["DownlineList"] = model.ObjList;
            }



            return View(model);
        }


        [HttpPost]
        public ActionResult UserDownlineReport(UserDownlineReport model, string loginId, string Place)
        {
            UserDownlineList obj = new UserDownlineList();
            //List<UserDownlineReport> objlist = obj.DownlineList(model);
            if (model.Page == null || model.Page == 0)
            {
                model.Page = 1;
            }
            if (loginId != null)
            {


                //string dcdata = aesencryptdecrypt.Decrypt(loginId);
                model.LoginId = loginId;
                model.ObjList = obj.DownlineList(model);
                ViewBag.LoginId = loginId;
                Session["DownlineList"] = model.ObjList;
            }

            model.Size = SessionManager.Size;
            // ViewBag.LoginId = model.LoginId;
            model.Place = Place;
            ViewBag.Place = model.Place;
            ViewBag.Status = model.TemPermanent;
            model.ObjList = obj.DownlineList(model);
            Session["DownLineList"] = model.ObjList;
            if (model.IsExport == 1)
            {
                model.ObjList = obj.DownlineList(model);
                DataTable dt = new DataTable("Downline Report");
                dt.Columns.AddRange(new DataColumn[10] {
                                            new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Placement"),
                                            new DataColumn("Sponsor Login Id"),
                                            new DataColumn("Sponsor Name"),
                                            new DataColumn("Under Place Id"),
                                            new DataColumn("Under Place Name"),
                                             new DataColumn("Date Of Joining"),
                                            new DataColumn("Date Of Activation"),
                                            new DataColumn("Status")

            });
                if (model.ObjList.Count > 0)
                {
                    foreach (var customer in model.ObjList)
                    {
                        dt.Rows.Add(customer.LoginId, customer.Name, customer.Place, customer.SponsorId, customer.SponsorName, customer.ParentLoginId,
                            customer.ParentName, customer.JoiningDate, customer.PermanentDate, customer.TemPermanent
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DownLine Report.xlsx");
                        }
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (model.ObjList.Count > 0)
                {

                    totalRecords = Convert.ToInt32(model.ObjList[0].TotalRecords);
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }


            return View(model);
        }



        [HttpGet]
        public ActionResult UpgradePackage()
        {
            MFranchiseorderRequest model = new MFranchiseorderRequest();
            ComboPackage obj = new ComboPackage();
            try
            {

                ViewBag.Product = obj.UpgradePackageList();
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpGet]
        public ActionResult UnPaidPayoutreport(int? Page)
        {
            MPayoutReport obj = new MPayoutReport();
            if (!PermissionManager.IsActionPermit("Pending Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.PaidUnPaidPayoutReport(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["PaidUnPaidlistPayoutdetails"] = obj.Objlist;
            return View(obj);

        }

        [HttpPost]
        public ActionResult UnPaidPayoutreport(MPayoutReport obj, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Pending Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.PaidUnPaidPayoutReport(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindPayoutMaster();
            Session["PaidUnPaidlistPayoutdetails"] = obj.Objlist;
            if (obj.IsExport == 1)
            {
                obj.Objlist = DALAdminReports.PaidUnPaidPayoutReport(obj);
                DataTable dt = new DataTable("Pending Payout Report");
                dt.Columns.AddRange(new DataColumn[9] {
                new DataColumn("LoginId"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Payout No"),
                                            new DataColumn("Date"),
                                            new DataColumn("Gross Amount"),
                                            new DataColumn("Processing Fee"),
                                            new DataColumn("Net Amount"),
                                            new DataColumn("Payable Amount"),
                                             new DataColumn("Status")

            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.ClosingDate, customer.GrossAmount, customer.ProcessingFee, customer.NetAmount,
                            customer.NetAmount, customer.PaidUnPaidStatus
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Pending Payout Report.xlsx");
                        }
                    }
                }
                else
                {
                    TempData["alert"] = "Data Not Found";
                    return Redirect("UnPaidPayoutreport");
                }
            }
            return View(obj);
        }


        [HttpGet]
        public ActionResult AdminRewardAchieverList()
        {


            return View();
        }

        [HttpPost]
        public ActionResult AdminRewardAchieverList(RewardsModel model)
        {

            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();
            obj2.Rewardslst = obj.AdminRewards(model);
            ViewBag.LoginId = model.LoginId;
            return View(obj2);
        }

        #region  Claim 
        [HttpPost]
        public JsonResult Claim(int rId, string LoginId)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();

            obj2.PK_RId = rId;
            obj2.Action = "Claim";
            obj2.LoginId = LoginId;
            obj2 = obj.AdminClaimAndSkip(obj2);
            return Json(obj2, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Skip 
        [HttpPost]
        public JsonResult Skip(int rId, string LoginId)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();

            obj2.PK_RId = rId;
            obj2.Action = "Skip";
            obj2.LoginId = LoginId;

            return Json(obj2, JsonRequestBehavior.AllowGet);
        }

        #endregion
        [HttpGet]
        public ActionResult AdminBonanzaAchieverList()
        {


            return View();
        }

        [HttpPost]
        public ActionResult AdminBonanzaAchieverList(RewardsModel model)
        {

            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();

            obj2.Rewardslst = obj.AdminBonanza(model);
            ViewBag.LoginId = model.LoginId;
            return View(obj2);
        }

        [HttpPost]
        public JsonResult BonanzaClaim(int rId, string LoginId)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();

            obj2.PK_RId = rId;
            obj2.Action = "Claim";
            obj2.LoginId = LoginId;
            obj2 = obj.BonanzaClaimAndSkip(obj2);
            return Json(obj2, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult BonanzaSkip(int rId, string LoginId)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();

            obj2.PK_RId = rId;
            obj2.Action = "Skip";
            obj2.LoginId = LoginId;
            obj2 = obj.BonanzaClaimAndSkip(obj2);
            return Json(obj2, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AdminUploadKyc(MAdminUploadKyc model)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();

            MAdminUploadKyc modelResult = obj.AdminUploadKyc(model);

            return Json(modelResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditKycDocumentByAdmin(string Fk_MemId, string type)
        {

            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            MAdminUploadKyc model = new MAdminUploadKyc();
            model.Fk_MemId = Convert.ToInt32(Fk_MemId);
            model.type = type;
            MAdminUploadKyc modelResult = obj.ViewEditUpdateProfile(model);



            return Json(modelResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateKycByAdmin(string Fk_MemId, string type, string PanNo, string AddressProofNo, string BankAccountNo, string AddressFrontUrl, string AddressBackUrl, string PanUrl, string BankUrl, string IfscCode)
        {

            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            MAdminUploadKyc model = new MAdminUploadKyc();
            model.Fk_MemId = Convert.ToInt32(Fk_MemId);
            model.type = type;
            model.PanCardNo = PanNo;
            model.AddressProofNo = AddressProofNo;
            model.BankACNo = BankAccountNo;
            model.AddressFrontUrl = AddressFrontUrl;
            model.AddressBackUrl = AddressBackUrl;
            model.PanUrl = PanUrl;
            model.BankUrl = BankUrl;
            model.IfscCode = IfscCode;
            MAdminUploadKyc modelResult = obj.ViewEditUpdateProfile(model);



            return Json(modelResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetKycDetails(string Fk_MemId)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            MAdminUploadKyc model = new MAdminUploadKyc();
            model.Fk_MemId = Convert.ToInt32(Fk_MemId);

            MAdminUploadKyc modelResult = obj.ViewKYCProfile(model);

            return Json(modelResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReKYC(string Fk_MemID, string ActionType)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            MAdminUploadKyc model = new MAdminUploadKyc();
            model.Fk_MemId = Convert.ToInt32(Fk_MemID);
            model.type = ActionType;

            MAdminUploadKyc objresult = obj.AdminReKYC(model);


            return Json(objresult, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Repurchase_PayoutReport(int? Page, MPayoutReport obj)
        {
            if (!PermissionManager.IsActionPermit("Repurchase Payout Income Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            obj.Page = Page;

            obj = AdminReportsService.GetRepurchasePayoutReport_AdP(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindRepurchasePayoutMaster();
            Session["listRepurchasePayoutdetails"] = obj.Objlist;
            if (obj.IsExport == 1)
            {
                DataTable dt = new DataTable("Repur. Payout Income Report");
                dt.Columns.AddRange(new DataColumn[11] {
                new DataColumn("LoginId"),
                                            new DataColumn("Login Id"),
                                            new DataColumn("Direct Seller"),
                                            new DataColumn("Payout No"),
                                            new DataColumn("Direct Income"),
                                            new DataColumn("Matching Income"),
                                            new DataColumn("Gross Amount"),
                                            new DataColumn("Processing Fee"),
                                            new DataColumn("TDS Amount"),
                                            new DataColumn("Net Amount"),
                                            new DataColumn("Date"),

            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.RepurchaseBonus, customer.HybridMatchingBonus, customer.GrossAmount, customer.ProcessingFee, customer.TDSAmount, customer.NetAmount,
                            customer.ClosingDate
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Repur. Payout Income Report.xlsx");
                        }
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, obj.Page, SessionManager.Size);
                    obj.Pager = pager;
                }
                else
                {
                    obj.Objlist = new List<MPayoutReport>();
                }
            }


            return View(obj);
        }
        //public ActionResult ExportToExcelRepurchase_PayoutReport(MPayoutReport obj)
        //{

        //    var customers = (List<MPayoutReport>)Session["listRepurchasePayoutdetails"];

        //    else
        //    {
        //        TempData["alert"] = "Data Not Found";
        //        return Redirect("Repurchase_PayoutReport");
        //    }
        //}
        public ActionResult Repurchase_UnPaidPayoutreport(int? Page, MPayoutReport obj)
        {
            if (!PermissionManager.IsActionPermit("Repurchase Pending Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            obj = AdminReportsService.ReParchasePaidUnPaidPayoutReport(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindRepurchasePayoutMaster();
            Session["RepurchasePaidUnPaidlistPayoutdetails"] = obj.Objlist;
            if (obj.IsExport == 1)
            {
                obj.Objlist = DALAdminReports.ReParchasePaidUnPaidPayoutReport(obj);
                DataTable dt = new DataTable("Repur. Pending Payout Report");
                dt.Columns.AddRange(new DataColumn[12] {
                new DataColumn("LoginId"),
                                            new DataColumn("Login Id"),
                                            new DataColumn("Direct Seller"),
                                            new DataColumn("Payout No"),
                                            new DataColumn("Direct Income"),
                                            new DataColumn("Matching Income"),
                                            new DataColumn("Gross Amount"),
                                            new DataColumn("Processing Fee"),
                                            new DataColumn("TDS Amount"),
                                            new DataColumn("Net Amount"),

                                             new DataColumn("Status"),
                                              new DataColumn("Date")

            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.PayoutNo, customer.DirectIncome, customer.MatchingBonus, customer.GrossAmount, customer.ProcessingFee, customer.TDSAmount, customer.NetAmount,
                             customer.PaidUnPaidStatus, customer.ClosingDate
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Repur. Pending Payout Report.xlsx");
                        }
                    }
                }
            }
            return View(obj);

        }
        public ActionResult Repurchase_PayoutReportForBank(int? Page, MPayoutReport obj)
        {

            if (!PermissionManager.IsActionPermit("Repurchase Payout Report For Bank", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            ViewBag.LoginId = obj.MemberLoginId;
            ViewBag.PayoutNo = obj.PayoutNo;
            ViewBag.tdate = obj.tdate;
            ViewBag.fdate = obj.fdate;
            obj = AdminReportsService.GetRepurchasePayoutReportForBank(Page, obj);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindRepurchasePayoutMaster();
            Session["RepurchasePReportBank"] = obj.Objlist;
            if (obj.IsExport == 1)
            {
                obj.Objlist = DALAdminReports.GetRepurchasePayoutReportForBank(obj);
                DataTable dt = new DataTable("Repurchase Payout For Bank");
                dt.Columns.AddRange(new DataColumn[10] {
                                                     new DataColumn("Direct Seller Id"),
                                                     new DataColumn("Direct Seller"),
                                                     new DataColumn("Mobile No"),
                                                     new DataColumn("Payout No"),
                                                     new DataColumn("Date"),
                                                     new DataColumn("Bank Name"),
                                                     new DataColumn("IFSC Code"),
                                                     new DataColumn("Bank Branch"),
                                                     new DataColumn("Account No"),
                                                     new DataColumn("Net Amount")
            });
                if (obj.Objlist.Count > 0)
                {
                    foreach (var customer in obj.Objlist)
                    {
                        dt.Rows.Add(customer.LoginId, customer.Name, customer.MobileNo, customer.PayountNo, customer.ClosingDate, customer.BankName, customer.IFSCCode, customer.MemberBranch, customer.MemberAccNo, customer.NetIncome);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Repurchase Payout For Bank.xlsx");
                        }
                    }
                }
            }
            return View(obj);
        }
        public ActionResult Repurchase_UpdatePayoutPayment(int? Page, MPayoutReport obj)
        {

            if (!PermissionManager.IsActionPermit("Repurchase Update Payout Payment", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.PaymentModeList = DALBindCommonDropdown.BindPaymentMode();
            ViewBag.BankList = DALBindCommonDropdown.BindDropdown(9, 0);
            ViewBag.PayoutMaster = DALBindCommonDropdown.BindRepurchasePayoutMaster();

            var res = AdminReportsService.GetRepurchasePayoutPayment(Page, obj);
            return View(res);
        }
        public JsonResult RepurchaseUpdatePayoutPayment(long paidmemberId, Decimal paidAmount, string paymentModeId, string paymentNo = "", string paymentDate = "", int bankId = 0, string branchname = null)
        {
            //DateTime? PaymentDate=null;
            if (paymentModeId == "Cash")
            {
                paymentNo = "";
                paymentDate = null;
                bankId = 0;
                branchname = "";
            }
            if (string.IsNullOrEmpty(paymentDate) == false)
            {
                paymentDate = DsResultModel.ConvertToSystemDate(paymentDate, "dd/MM/yyyy");
            }
            else
            {
                paymentDate = null;
            }
            string ipAddress = "0";
            long userId = SessionManager.Fk_MemId;
            DataSet ds = AdminReportsService.RepurchaseUpdatePayoutPayment(paidmemberId, paidAmount, paymentModeId, paymentNo, paymentDate, bankId, branchname, userId, ipAddress);
            DsResultModel dsResult = DsResultModel.DsToResultModel(ds);
            return Json(dsResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GSTReport(int? Page)
        {
            Reports reports = new Reports();
            try
            {
                if (!PermissionManager.IsActionPermit("GST Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                reports.OpCode = 1;

                reports = Reports.GetReports(Page, reports);
                Session["GSTList"] = reports.LstOrder;
                return View(reports);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost]
        public ActionResult GSTReport(Reports reports, int? Page)
        {
            if (!PermissionManager.IsActionPermit("GST Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            reports.OpCode = 1;

            reports = Reports.GetReports(Page, reports);
            Session["GSTList"] = reports.LstOrder;
            if (reports.IsExport == 1)
            {
                reports.LstOrder = DALReports.GetReports(reports);
                DataTable dt = new DataTable("GST Report");
                dt.Columns.AddRange(new DataColumn[9] {
                                                     new DataColumn("Order No"),
                                                     new DataColumn("Order Date"),
                                                     new DataColumn("Franchisee Id"),
                                                     new DataColumn("Franchisee Name"),
                                                     new DataColumn("Direct Seller Id"),
                                                     new DataColumn("Direct Seller Name"),
                                                     new DataColumn("Taxable Amt"),
                                                     new DataColumn("Tax Amt"),
                                                     new DataColumn("Total Amt"),

            });
                if (reports.LstOrder.Count > 0)
                {
                    foreach (var customer in reports.LstOrder)
                    {
                        dt.Rows.Add(customer.OrderNo, customer.OrderDate, customer.SellById, customer.SellByName, customer.SellToId, customer.SellToName, customer.TaxableAmount, customer.TaxAmt, customer.TotalAmt);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GST Report.xlsx");
                        }
                    }
                }
                else
                {
                    TempData["alert"] = "Data Not Found";
                    return RedirectToAction("GSTReport", "AdminReport");
                }

            }
            return View(reports);
        }

        public ActionResult GSTReportDetails(string OrderNo)
        {
            Reports reports = new Reports();
            reports.OrderNo = OrderNo;
            reports.OpCode = 1;

            reports = Reports.GetReports(0, reports);
            return Json(reports, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrderDetails(string OrderNo)
        {
            Reports reports = new Reports();
            reports.OrderNo = OrderNo;
            reports.OpCode = 2;
            reports = Reports.GetReports(0, reports);
            return Json(reports, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDirectBusiness(int? Page, Reports reports)
        {
            reports = Reports.GetDirectBusiness(Page, reports);
            return View(reports);
        }

        public ActionResult GetFranchiseSales(int? Page, Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Franchisee Sales Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }


            reports = Reports.GetFranchiseSales(Page, reports);
            return View(reports);
        }


        public ActionResult FranchiseSalesExport(Reports obj)
        {
            DataTable dt = new DataTable("Franchisee Sales Report");
            dt.Columns.AddRange(new DataColumn[7] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Person Name"),
                                                     new DataColumn("Contact"),
                                                     new DataColumn("CrAmount"),
                                                     new DataColumn("DrAmount"),
                                                     new DataColumn("Number Of Sales"),
                                                     new DataColumn("Total BV")


            });
            //FranchiseOrderService db = new FranchiseOrderService();


            obj.Page = 1;
            //obj.ProcId = 1;
            obj.Size = SessionManager.Size;
            //var customers = FranchiseOrderService.SupplyOrderStatusServiceForAdmin(obj);
            var customers = DALReports.GetFranchiseSales(obj);

            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.PersonName, customer.Contact, customer.CrAmount, customer.DrAmount, customer.NumberOfSales, customer.TotalBV);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Franchisee Sales Report.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("GetFranchiseSales", "AdminReport");
            }
            //return View();
        }
        [HttpGet]
        public ActionResult PanCard_TDSReport()
        {
            MTDSReport obj = new MTDSReport();
            if (!PermissionManager.IsActionPermit("PAN TDS Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }


            MTDSReport list = AdminReportsService.GetPanCard_TDSReport(obj);
            ViewBag.FinancealYear = DALBindCommonDropdown.BindFinancealYear();
            ViewBag.FinancealMonth = DALBindCommonDropdown.BindFinancealMonth();
            Session["PanCardTDSReport"] = obj.Objlist;
            ViewBag.loginId = obj.LoginId;
            ViewBag.PayoutNo = obj.PayoutNo;

            return View(list);
        }

        [HttpPost]
        public ActionResult PanCard_TDSReport(MTDSReport model)
        {
            if (!PermissionManager.IsActionPermit("PAN TDS Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MTDSReport list = AdminReportsService.GetPanCard_TDSReport(model);
            ViewBag.FinancealYear = DALBindCommonDropdown.BindFinancealYear();
            ViewBag.FinancealMonth = DALBindCommonDropdown.BindFinancealMonth();
            Session["PanCardTDSReport"] = model.Objlist;
            ViewBag.loginId = model.LoginId;
            ViewBag.PayoutNo = model.PayoutNo;
            if (model.IsExport == 1)
            {
                model.Objlist = DALAdminReports.GetPanCard_TDSReport(model);
                DataTable dt = new DataTable("PAN TDS Report");
                dt.Columns.AddRange(new DataColumn[6] {

                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("PAN Card"),

                                                     new DataColumn("Gross Amount"),
                                                      new DataColumn("TDS Amount"),
                                                      new DataColumn("Net Amount")



            });
                if (model.Objlist.Count > 0)
                {
                    foreach (var customer in model.Objlist)
                    {
                        dt.Rows.Add(customer.Name, customer.Mobile, customer.PanNo, customer.GrossAmount, customer.TDS, customer.NetAmount);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PAN TDS Report.xlsx");
                        }
                    }
                }
            }
            return View(list);
        }
        public ActionResult GetFranchiseWalletLedgerForAdmin()
        {
            if (!PermissionManager.IsActionPermit("Franchisee Wallet Ledger", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetFranchiseWalletLedgerForAdmin(int? Page, Reports reports)
        {


            reports = Reports.GetFranchiseWalletLedgerForAdmin(Page, reports);

            return View(reports);
        }

        public ActionResult FranchiseWalletLedgerForAdminExport(Reports obj)
        {
            DataTable dt = new DataTable("Franchise Wallet Ledger");
            dt.Columns.AddRange(new DataColumn[7] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("TransactionDate"),
                                                     new DataColumn("DrAmount"),
                                                     new DataColumn("Narration"),
                                                     new DataColumn("TotalAmt"),
                                                     new DataColumn("Balance"),
                                                     new DataColumn("Size")


            });
            //FranchiseOrderService db = new FranchiseOrderService();


            obj.Page = 1;
            //obj.ProcId = 1;
            obj.Size = SessionManager.Size;
            //var customers = FranchiseOrderService.SupplyOrderStatusServiceForAdmin(obj);
            var customers = DALReports.GetFranchiseWalletLedgerForAdmin(obj);

            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.TransactionDate, customer.DrAmount, customer.Narration, customer.TotalAmt, customer.Balance, customer.Size);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Franchise Wallet Ledger.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return RedirectToAction("GetFranchiseWalletLedgerForAdmin", "AdminReport");
            }
            //return View();
        }

        public ActionResult GetPackageGstReport(int? Page, Reports reports)
        {

            reports = Reports.GetPackageGstReport(Page, reports);
            return View(reports);
        }

        public ActionResult GetPackageSaleReport(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Package Sales Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            DataSet dataSet1 = reports.GetPackageSaleReport();
            if (dataSet1 != null && dataSet1.Tables[0].Rows.Count > 0)
            {
                reports.dtPaidActivation = dataSet1.Tables[0];
                reports.dtFreeActivation = dataSet1.Tables[1];
            }
            return View(reports);
        }
        public ActionResult GetBusinessStatistics(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Business Statistics Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            DataSet dataSet1 = reports.GetBusinessStatistics();
            if (dataSet1 != null && dataSet1.Tables[0].Rows.Count > 0)
            {
                reports.dtPaidActivation = dataSet1.Tables[0];
                reports.dtFreeActivation = dataSet1.Tables[1];
                reports.dtDirectBusinessValues0 = dataSet1.Tables[2];
                reports.dtDirectBusinessValues1 = dataSet1.Tables[3];
            }
            return View(reports);
        }


        public ActionResult GetFranchiseStockForAdmin(int? Page, AdminStock adminStock)
        {
            Reports reports = new Reports();
            if (!PermissionManager.IsActionPermit("Franchisee Stock Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            adminStock = AdminStock.GetFranchiseStockForAdmin(Page, adminStock);



            List<SelectListItem> ddlProduct = new List<SelectListItem>();

            DataSet dataSet = reports.GetAllProducts();

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)

            {
                ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });

                foreach (DataRow r in dataSet.Tables[0].Rows)
                {


                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });


                }
            }
            ViewBag.ddlProduct = ddlProduct;

            List<SelectListItem> ddlFranchiseDetails = new List<SelectListItem>();

            DataSet dataSet1 = reports.GetAllFranchiseDetail();

            if (dataSet1 != null && dataSet1.Tables.Count > 0 && dataSet1.Tables[0].Rows.Count > 0)

            {
                ddlFranchiseDetails.Add(new SelectListItem { Text = "Select Franchisee", Value = "0" });

                foreach (DataRow r in dataSet1.Tables[0].Rows)

                {


                    ddlFranchiseDetails.Add(new SelectListItem { Text = r["name"].ToString(), Value = r["PKFranchiseRegistrationId"].ToString() });


                }
            }
            ViewBag.ddlFranchiseDetails = ddlFranchiseDetails;
            return View(adminStock);
        }
        public ActionResult GetBonanzaRewardForAdmin(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Monthly Bonanza Reward", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            //Excel
            if (reports.Page == null || reports.Page == 0)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            DataSet dataSet = reports.GetBonanzaRewardForAdmin();
            reports.dtGetBonanzaRewardForAdmin = dataSet.Tables[0];
            if (reports.IsExport == 1)
            {

                DataTable dt = new DataTable("Monthly Bonanza Reward");
                dt.Columns.AddRange(new DataColumn[4] {

                                             new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Bonanza Name"),
                                            new DataColumn("Total BV"),


            });
                //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
                if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        dt.Rows.Add(row["LoginId"].ToString(), row["Name"].ToString(), row["BonanzaName"].ToString(), row["TotalBV"].ToString());
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Monthly Bonanza Reward.xlsx");
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (dataSet.Tables[0].Rows.Count > 0)
                {

                    totalRecords = Convert.ToInt32(reports.dtGetBonanzaRewardForAdmin.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                    reports.Pager = pager;
                }
            }

            return View(reports);

        }

        public ActionResult GetProductStockReport(Reports reports)
        {

            if (!PermissionManager.IsActionPermit("Product Stock Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            List<SelectListItem> ddlProduct = new List<SelectListItem>();

            DataSet dataSet = reports.GetAllProducts();

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)

            {
                ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });

                foreach (DataRow r in dataSet.Tables[0].Rows)

                {


                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });


                }
            }
            if (reports.Page == null || reports.Page == 0)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            ViewBag.ddlProduct = ddlProduct;
            DataSet dataset1 = reports.GetProductStockReport();
            reports.dtProductStockReport = dataset1.Tables[0];
            Session["ProductStockReport"] = dataset1.Tables[0];

            if (reports.IsExport == 1)
            {
                //reports.dataset1 = reports.GetProductStockReport();
                DataTable dt = new DataTable("Product Stock Report");
                dt.Columns.AddRange(new DataColumn[8] {

                                             new DataColumn("Product Name"),
                                             new DataColumn("Purchase"),
                                            new DataColumn("Sale in Repurchase"),
                                            new DataColumn("Sale in Package"),
                                            new DataColumn("Old Sale"),
                                            new DataColumn("Open Orders Sale"),
                                            new DataColumn("Franchise stock"),
                                            new DataColumn("Balance"),


            });
                if (dataset1.Tables[0] != null && dataset1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset1.Tables[0].Rows)
                    {
                        dt.Rows.Add(row["ProductName"].ToString(), row["Credit"].ToString(), row["Debit"].ToString(), row["SaleInPackage"].ToString(), row["OldSale"].ToString(), row["OpenOrders"].ToString(), row["FranchiseStock"].ToString(), row["Balance"].ToString());
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Product Stock Report.xlsx");
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (dataset1.Tables[0].Rows.Count > 0)
                {
                    totalRecords = Convert.ToInt32(reports.dtProductStockReport.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                    reports.Pager = pager;
                }
            }




            return View(reports);
        }
        //[HttpPost]
        //public ActionResult GetProductStockReport(Reports reports)
        //{
        //    if (!PermissionManager.IsActionPermit("Product Stock Report", ViewOptions.FormView))
        //    {
        //        return Redirect("/Home/adminlogin");
        //    }
        //    List<SelectListItem> ddlProduct = new List<SelectListItem>();

        //    DataSet dataSet = reports.GetAllProducts();

        //    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)

        //    {
        //        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });

        //        foreach (DataRow r in dataSet.Tables[0].Rows)

        //        {


        //            ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });


        //        }
        //    }

        //    ViewBag.ddlProduct = ddlProduct;
        //    DataSet dataset1 = reports.GetProductStockReport();
        //    reports.dtProductStockReport = dataset1.Tables[0];
        //    Session["ProductStockReport"] = dataset1.Tables[0];


        //    return View(reports);
        //}
        //public ActionResult GetProductGstReportExport()
        //{
        //    // dt = new DataTable("LifeTimeRewardList");


        //    DataTable dcustomers = (DataTable)Session["ProductStockReport"];

        //    List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;

        //    return null;
        //}
        public ActionResult GetProductGstReport(int? Page, Reports reports)
        {
            try
            {
                if (!PermissionManager.IsActionPermit("Product GST Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                reports.OpCode = 3;
                if (Page == null)
                {
                    Page = 1;
                }
                reports.Page = Page;
                reports.Size = SessionManager.Size;

                DataSet dataSet = reports.GetProductGstReport(reports);
                reports.dtGetProductGstReport = dataSet.Tables[0];
                if (reports.IsExport == 1)
                {
                    DataTable dt = new DataTable("Product GST Report");
                    dt.Columns.AddRange(new DataColumn[4] {

                                             new DataColumn("Product Name"),
                                             new DataColumn("Total Amount"),
                                            new DataColumn("Taxable Amount"),
                                            new DataColumn("Tax Amount"),


            });
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            dt.Rows.Add(row["ProductName"].ToString(), row["TotalAmt"].ToString(), row["TaxableAmount"].ToString(), row["TaxAmt"].ToString());
                        }
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Product GST Report.xlsx");
                        }
                    }
                }
                else
                {
                    int totalRecords = 0;
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        totalRecords = Convert.ToInt32(reports.dtGetProductGstReport.Rows[0]["TotalRecords"].ToString());
                        var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                        reports.Pager = pager;
                    }
                }


                return View(reports);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
      
        public ActionResult GetShoppingOrderDetails(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Shopping Order Details", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == 0 || reports.Page == null)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            DataSet dataSet = reports.GetShoppingOrderDetails();
            reports.dtGetShoppingOrderDetails = dataSet.Tables[0];
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = Convert.ToInt32(reports.dtGetShoppingOrderDetails.Rows[0]["TotalRecord"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            return View(reports);

        }
        [HttpPost]
        public JsonResult CancelShoppingOrder(string id,string remark)
        {
            string message = "";
            try
            {

                Reports reports = new Reports();
                reports.OrderNo = id;               
                reports.RemarkCancel = remark;
                reports.FK_MemId = int.Parse(SessionManager.Fk_MemId.ToString());
                DataSet dataSet = reports.CancelShoppingOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows[0]["Code"].ToString() == "1") {
                        message = dataSet.Tables[0].Rows[0]["Msg"].ToString();
                    }
                }
                else
                {
                    message = dataSet.Tables[0].Rows[0]["Msg"].ToString();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return Json(new { message = message });
        }


        //public ActionResult PremiumClubMember(Reports reports)
        //{
        //    if (!PermissionManager.IsActionPermit("Premium Direct Seller", ViewOptions.FormView))
        //    {
        //        return Redirect("/Home/adminlogin");
        //    }
        //    if (reports.Page == null || reports.Page == 0)
        //    {
        //        reports.Page = 1;
        //    }
        //    reports.Size = SessionManager.Size;
        //    DataSet dataSet = reports.GetPremiumClubMembers();
        //    reports.dtGetPremiumClubMembers = dataSet.Tables[0];
        //    if (reports.IsExport == 1)
        //    {

        //        DataTable dt = new DataTable("Premimum Club Direct Seller");
        //        dt.Columns.AddRange(new DataColumn[7] {

        //                                     new DataColumn("Login Id"),
        //                                     new DataColumn("Direct Seller"),
        //                                    new DataColumn("Mobile No"),
        //                                    new DataColumn("Joining Date"),
        //                                    new DataColumn("Activation Date"),
        //                                    new DataColumn("Package Name"),
        //                                    new DataColumn("No of Sponsoring"),


        //    });
        //        //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
        //        if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dataSet.Tables[0].Rows)
        //            {
        //                dt.Rows.Add(row["LoginId"].ToString(), row["DisplayName"].ToString(), row["Mobile"].ToString(), row["DOJ"].ToString(), row["DOA"].ToString(), row["PackageName"].ToString(), row["NoofSp"].ToString());
        //            }
        //        }

        //        using (XLWorkbook wb = new XLWorkbook())
        //        {
        //            wb.Worksheets.Add(dt);
        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                wb.SaveAs(stream);
        //                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Premimum Club Direct Seller.xlsx");
        //            }
        //        }
        //    }
        //    int totalRecords = 0;
        //    if (dataSet.Tables[0].Rows.Count > 0)
        //    {
        //        totalRecords = Convert.ToInt32(reports.dtGetPremiumClubMembers.Rows[0]["TotalRecords"].ToString());
        //        var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
        //        reports.Pager = pager;
        //    }

        //    return View(reports);

        //}

        public ActionResult PremiumClubQualifier(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Premium Club Qualifier", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            DataSet dataSet = reports.GetPremiumClubQualifier();
            reports.dtGetPremiumClubQualifier = dataSet.Tables[0];
            return View(reports);
        }

        public ActionResult ConsolidatedGstReport(int? Page, Reports reports)
        {
            try
            {
                if (!PermissionManager.IsActionPermit("Consolidated GST Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }

                // reports.OpCode = 1;

                if (Page == null)
                {
                    Page = 1;

                }
                reports.Page = Page;
                reports.Size = SessionManager.Size;
                // reports = Reports.GetConsolidatedGstReport(Page,reports);
                DataSet dataSet = reports.GetConsolidatedGstReport(reports);
                reports.dtGetConsolidatedGstReport = dataSet.Tables[0];
                Session["ConsolidatedGstReportList"] = dataSet.Tables[0];
                if (reports.IsExport == 1)
                {
                    DataTable dt = new DataTable("Consolidated GST Report");
                    dt.Columns.AddRange(new DataColumn[6] {

                                             new DataColumn("Place Of Supply"),
                                            new DataColumn("State Code"),
                                            new DataColumn("Taxable Amt"),
                                               new DataColumn("Tax Per"),
                                              new DataColumn("Tax Amt"),
                                            new DataColumn("Amount")

            });
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            //TextBox1.Text = row["ImagePath"].ToString();
                            dt.Rows.Add(row["PlaceOfSupply"].ToString(), row["StateCode"].ToString(), row["TaxableAmt"].ToString(), row["TaxPer"].ToString(), row["TaxAmt"].ToString(), row["Amount"].ToString());


                        }

                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Consolidated GST Report.xlsx");
                            }
                        }
                    }

                }
                else
                {
                    int totalRecords = 0;
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        totalRecords = Convert.ToInt32(reports.dtGetConsolidatedGstReport.Rows[0]["TotalRecords"].ToString());
                        var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                        reports.Pager = pager;
                    }
                }

                return View(reports);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult StatewiseGstReport(int? Page, Reports reports)
        {
            try
            {
                if (!PermissionManager.IsActionPermit("HSNCode GST Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }


                // reports.OpCode = 1;
                if (Page == null)
                {
                    Page = 1;
                }
                reports.Page = Page;
                reports.Size = SessionManager.Size;
                DataSet dataSet = reports.GetStatewiseGstReport(reports);
                reports.dtGetStatewiseGstReport = dataSet.Tables[0];
                Session["StatewiseGstReportList"] = dataSet.Tables[0];
                if (reports.IsExport == 1)
                {
                    DataTable dt = new DataTable("HSN Code GST Report");
                    dt.Columns.AddRange(new DataColumn[10] {

                                             new DataColumn("HSN Code"),
                                             new DataColumn("Place Of Supply"),
                                            new DataColumn("Qty"),
                                            new DataColumn("CGST"),
                                            new DataColumn("SGST"),
                                            new DataColumn("IGST"),
                                            new DataColumn("Taxablel Amt"),
                                             new DataColumn("Tax Per"),
                                              new DataColumn("Tax Amount"),
                                            new DataColumn("Total Amountt")

            });
                    if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            //TextBox1.Text = row["ImagePath"].ToString();
                            dt.Rows.Add(row["HSNCode"].ToString(), row["PlaceOfSupply"].ToString(), row["Qty"].ToString(), row["CGST"].ToString(), row["SGST"].ToString(), row["IGST"].ToString(),
                                   row["TaxableAmt"].ToString(), row["TaxPer"].ToString(), row["TaxAmount"].ToString(), row["TotalAmount"].ToString()
                                     );
                        }

                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HSN Code GST Report.xlsx");
                            }
                        }
                    }
                }
                else
                {
                    int totalRecords = 0;
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        totalRecords = Convert.ToInt32(reports.dtGetStatewiseGstReport.Rows[0]["TotalRecords"].ToString());
                        var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                        reports.Pager = pager;
                    }
                }

                return View(reports);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult GetInvoiceGSTReport(int? Page, Reports reports)
        {
            try
            {
                if (!PermissionManager.IsActionPermit("Invoice GST Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }

                if (Page == null)
                {
                    Page = 1;
                }
                reports.Page = Page;
                reports.Size = SessionManager.Size;
                DataSet dataSet = reports.GetInvoiceGSTReport(reports);
                reports.dtGetInvoiceGSTReport = dataSet.Tables[0];
                //Session["InvoiceGstReport"] = reports.dtGetInvoiceGSTReport;
                if (reports.IsExport == 1)
                {

                    DataTable dt = new DataTable("Invoice GST Report");
                    dt.Columns.AddRange(new DataColumn[8] {

                                            new DataColumn("Invoice No."),
                                            new DataColumn("Invoice Date"),
                                            new DataColumn("Customer Name"),
                                            new DataColumn("Mobile No."),
                                            new DataColumn("Total Amount"),
                                            new DataColumn("Taxable Amount"),
                                            new DataColumn("Tax Amount"),
                                            new DataColumn("Invoice Type"),

            });
                    if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            dt.Rows.Add(row["InvoiceNo"].ToString(), row["InvoiceDate"].ToString(), row["CustomerName"].ToString(), row["MobileNo"].ToString(), row["TotalAmount"].ToString(), row["TaxableAmt"].ToString(), row["TaxAmount"].ToString(), row["InvoiceType"].ToString());
                        }
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Invoice GST Report.xlsx");
                        }
                    }
                }
                else
                {
                    int totalRecords = 0;
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        totalRecords = Convert.ToInt32(reports.dtGetInvoiceGSTReport.Rows[0]["TotalRecords"].ToString());
                        var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                        reports.Pager = pager;
                    }
                }

                return View(reports);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ActionResult CancelInvoicRequest(int? Page, Reports reports)
        {
            try
            {
                if (!PermissionManager.IsActionPermit("Cancel Invoice Report", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                if (Page == null)
                {
                    Page = 1;
                }
                reports.Page = Page;
                reports.Size = SessionManager.Size;
                DataSet dataSet = reports.CancelInvoicRequest(reports);
                reports.dtCancelInvoicRequest = dataSet.Tables[0];
                Session["CancelInvoicRequestList"] = dataSet.Tables[0];
                if (reports.IsExport == 1)
                {
                    DataTable dt = new DataTable("Cancel Invoice Report");
                    dt.Columns.AddRange(new DataColumn[16] {

                                            new DataColumn("Login Id"),
                                            new DataColumn("Direct Seller "),
                                            new DataColumn("Items Qty"),
                                            new DataColumn("Order No"),
                                            new DataColumn("Transaction Id"),
                                            new DataColumn("Invoice No"),
                                            new DataColumn("Sell Date"),
                                            new DataColumn("Sell By"),
                                            new DataColumn("Sell To"),
                                            new DataColumn("Total BV"),
                                            new DataColumn("Total Amt"),
                                            new DataColumn("Discount Amt"),
                                            new DataColumn("Paid Amt "),
                                            new DataColumn("Due Amt "),
                                            new DataColumn("QPV"),
                                            new DataColumn("Delete Date"),

            });
                    if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            dt.Rows.Add(row["LoginID"].ToString(), row["PersonName"].ToString(), row["ItemsQty"].ToString(), row["OrderNo"].ToString(), row["TransactionId"].ToString(), row["InvoiceNo"].ToString(), row["SellDate"].ToString(), row["SellBy"].ToString(), row["SellTo"].ToString(), row["TotalPv"].ToString(), row["TotalAmt"].ToString(), row["DiscountAmt"].ToString(), row["PaidAmt"].ToString(), row["DueAmt"].ToString(), row["Qpv"].ToString(), row["DeleteDate"].ToString());
                        }
                    }


                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Cancel Invoice Report.xlsx");
                        }
                    }
                }
                else
                {
                    int totalRecords = 0;
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        totalRecords = Convert.ToInt32(reports.dtCancelInvoicRequest.Rows[0]["TotalRecords"].ToString());
                        var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                        reports.Pager = pager;
                    }
                }

                return View(reports);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ActionResult LifeTimeReward(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Life Time Reward", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == null || reports.Page == 0)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            DataSet dataSet = reports.LifeTimeReward();
            reports.dtLifeTimeReward = dataSet.Tables[0];
            //Session["LifeTimeRewardList"] = dataSet.Tables[0];
            if (reports.IsExport == 1)
            {
                DataTable dt = new DataTable("Lifetime Reward");
                dt.Columns.AddRange(new DataColumn[9] {

                                             new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Reward Name"),
                                            new DataColumn("Reward Amount"),
                                            new DataColumn("Left Point"),
                                            new DataColumn("Right Point"),
                                            new DataColumn("Matching Target Point Status"),
                                            new DataColumn("Achieved On"),
                                            new DataColumn("Claimed On"),


            });
                //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
                if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        dt.Rows.Add(row["LoginId"].ToString(), row["Name"].ToString(), row["Rewards"].ToString(), row["RewardAmount"].ToString(), row["LeftPoint"].ToString(), row["RightPoint"].ToString(), row["MatchingTargetPoint"].ToString(), row["AchievedDate"].ToString(), row["ClaimedDate"].ToString());
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Lifetime Reward.xlsx");
                    }
                }
            }
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                totalRecords = Convert.ToInt32(reports.dtLifeTimeReward.Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            return View(reports);
        }


        //public ActionResult ExportToExcelLifeTimeReward()
        //{
        //    // dt = new DataTable("LifeTimeRewardList");

        //    DataTable dt = (DataTable)Session["Lifetime Reward"];


        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt);
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Lifetime Reward.xlsx");
        //        }
        //    }
        //    return null;
        //}
        public ActionResult GetFranchisePayoutReport(Reports reports)
        {

            if (!PermissionManager.IsActionPermit("Franchisee Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == null)
            {
                reports.Page = 1;
            }


            reports.Size = SessionManager.Size;
            //reports.Size = SessionManager.Size;
            //Reports reports = new Reports();
            DataSet dataSet = reports.GetFranchisePayoutReport();
            reports.dtGetFranchisePayoutReport = dataSet.Tables[0];
            Session["FranchisePayoutReport"] = dataSet.Tables[0];
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                totalRecords = Convert.ToInt32(reports.dtGetFranchisePayoutReport.Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            return View(reports);
        }

        public ActionResult ExportToExcelFranchisePayoutReport()
        {
            DataTable dt = (DataTable)Session["FranchisePayoutReport"];
            //dt.Columns.Remove("ImageURL");
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Franchise Payout Report.xlsx");
                }
            }
            return Redirect("GetFranchisePayoutReport");
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
        public ActionResult FranchisePayoutPublishedReport(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Franchisee Payout Published Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == null || reports.Page == 0)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            DataSet dataSet = reports.GetFranchisePayoutReport();
            reports.dtGetFranchisePayoutReport = dataSet.Tables[0];
            //Session["FranchisePayoutPublishReport"] = dataSet.Tables[0];
            if (reports.IsExport == 1)
            {
                DataTable dt = new DataTable("Franchise Payout Publish Report");
                dt.Columns.AddRange(new DataColumn[13] {

                                             new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Payout No"),
                                            new DataColumn("Closing Date"),
                                            new DataColumn("Package Commission"),
                                            new DataColumn("Repurchase Commission	"),
                                            new DataColumn("Matching Target Point Status"),
                                            new DataColumn("Gross Amount"),
                                            new DataColumn("TDS Amount"),
                                            new DataColumn("Processing Fee"),
                                            new DataColumn("Advance"),
                                            new DataColumn("Adjusted Amount"),
                                            new DataColumn("Net Amount"),


            });
                //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
                if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        dt.Rows.Add(row["LoginId"].ToString(), row["PersonName"].ToString(), row["PayoutNo"].ToString(), row["ClosingDate"].ToString(), row["PackageComm"].ToString(), row["RepurchaseComm"].ToString(), row["GrossAmount"].ToString(), row["TDSAmount"].ToString(), row["ProcessingFee"].ToString(), row["Advance"].ToString(), row["AdjustedAmount"].ToString(), row["NetAmount"].ToString());
                    }
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Franchise Payout Publish Report.xlsx");
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (dataSet.Tables[0].Rows.Count > 0)
                {

                    totalRecords = Convert.ToInt32(reports.dtGetFranchisePayoutReport.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                    reports.Pager = pager;
                }
            }

            return View(reports);
        }



        public ActionResult UpdateFranchisePayout(string id)
        {
            Reports reports = new Reports();

            reports.PK_PaidId = id;
            DataSet ds = reports.UpdateFranchisePayout();


            return RedirectToAction("/FranchisePayoutPublishedReport");
        }


        public ActionResult GetPayPayoutReport(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Pay Payout Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == null || reports.Page == 0)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            DataSet dataSet = reports.GetPayPayoutReport();
            reports.dtGetPayPayoutReport = dataSet.Tables[0];
            //Session["PayoutReportList"] = dataSet.Tables[0];
            if (reports.IsExport == 1)
            {
                DataTable dt = new DataTable("Pay Payout Report");
                dt.Columns.AddRange(new DataColumn[5] {

                                             new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Payout No"),
                                            new DataColumn("Closing Date"),
                                            new DataColumn("Net Amount"),




            });
                //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
                if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        dt.Rows.Add(row["LoginId"].ToString(), row["PersonName"].ToString(), row["PayoutNo"].ToString(), row["ClosingDate"].ToString(), row["NetAmount"].ToString());
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Pay Payout Report.xlsx");
                    }
                }
            }
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                totalRecords = Convert.ToInt32(reports.dtGetPayPayoutReport.Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            return View(reports);
        }

        //public ActionResult ExportToExcelPayPayoutReport()
        //{
        //    //DataTable dt = new DataTable("CancelInvoicRequestList");





        //    return null;
        //}

        public JsonResult UpdateFranchisePayPayout(Reports _param)
        {
            try
            {

                int _iresult = _param.UpdateFranchisePayPayout(_param); ;
                return Json(_iresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult GetMembersRecognition(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Direct Seller Recognition", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == null || reports.Page == 0)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            ViewBag.BindRecog = DALBindCommonDropdown.BindRecognition();
            DataSet dataSet = reports.GetMembersRecognition();
            reports.dtGetMembersRecognition = dataSet.Tables[0];
            //Session["MembersRecognition"] = reports.dtGetMembersRecognition;
            if (reports.IsExport == 1)
            {

                DataTable dt = new DataTable("Direct Seller Recognition");
                dt.Columns.AddRange(new DataColumn[4] {

                                             new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Recognition"),
                                            new DataColumn("Achieved On"),


            });
                //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
                if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        dt.Rows.Add(row["LoginId"].ToString(), row["Name"].ToString(), row["RecognitionName"].ToString(), row["AchievedOn"].ToString());
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Direct Seller Recognition.xlsx");
                    }
                }
            }
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                totalRecords = Convert.ToInt32(reports.dtGetMembersRecognition.Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            return View(reports);
        }

        public ActionResult AllGetShoppingOrderDetails(Reports reports)
        {
            reports.OpCode = 1;
            DataSet dataSet = new DataSet();
            dataSet = reports.AllGetShoppingOrderDataDetails();
            reports.dtAllGetShoppingOrder = dataSet.Tables[0];


            return View(reports);
        }

        //public ActionResult CustomerInvoiceReports
        public ActionResult AllGetShoppingOrderDetailsInvoice(string Pk_OrderId)
        {
            Reports orderConfirmation = new Reports();
            List<Reports> OrderItems = new List<Reports>();
            Reports orderDAL = new Reports();
            DataSet dsOrder = orderDAL.ShoppingOrderInvoice(Pk_OrderId);
            Int32 TotalAmount = 0;
            if (dsOrder.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsOrder.Tables[0].Rows.Count; i++)
                {
                    orderConfirmation.LoginId = dsOrder.Tables[0].Rows[i]["LoginId"].ToString();
                    orderConfirmation.DisplayName = dsOrder.Tables[0].Rows[i]["DisplayName"].ToString();
                    orderConfirmation.ActivationDate = dsOrder.Tables[0].Rows[i]["OrderDate"].ToString();
                    orderConfirmation.InnvoiceNo = dsOrder.Tables[0].Rows[i]["OrderNo"].ToString();
                    orderConfirmation.CompanyName = dsOrder.Tables[0].Rows[i]["CompanyName"].ToString();
                    orderConfirmation.OfficeAddress = dsOrder.Tables[0].Rows[i]["OfficeAddress"].ToString();
                    orderConfirmation.OfficeEmailId = dsOrder.Tables[0].Rows[i]["OfficeEmailId"].ToString();
                    orderConfirmation.OfficeContactNo = dsOrder.Tables[0].Rows[i]["OfficeContactNo"].ToString();
                    orderConfirmation.StateCode = dsOrder.Tables[0].Rows[i]["StateCode"].ToString();
                    orderConfirmation.CompanyGST = dsOrder.Tables[0].Rows[i]["CompanyGST"].ToString();
                    orderConfirmation.Website = dsOrder.Tables[0].Rows[i]["Website"].ToString();
                    orderConfirmation.Pancard = dsOrder.Tables[0].Rows[i]["Pancard"].ToString();
                    orderConfirmation.custGSTNo = dsOrder.Tables[0].Rows[i]["CustGSTNo"].ToString();


                    orderConfirmation.City = dsOrder.Tables[0].Rows[i]["City"].ToString();
                    orderConfirmation.State = dsOrder.Tables[0].Rows[i]["State"].ToString();
                    orderConfirmation.PinCode = dsOrder.Tables[0].Rows[i]["Pincode"].ToString();

                    orderConfirmation.custAddress = dsOrder.Tables[0].Rows[i]["custAddress"].ToString();
                    orderConfirmation.custCity = dsOrder.Tables[0].Rows[i]["custCity"].ToString();
                    orderConfirmation.custPincode = dsOrder.Tables[0].Rows[i]["custPincode"].ToString();
                    orderConfirmation.custState = dsOrder.Tables[0].Rows[i]["custState"].ToString();
                    orderConfirmation.MobileNo = dsOrder.Tables[0].Rows[i]["mobile"].ToString();





                    for (i = 0; i < dsOrder.Tables[2].Rows.Count; i++)
                    {

                        OrderItems.Add(new Reports
                        {
                            ProductName = dsOrder.Tables[2].Rows[i]["ProductName"].ToString(),
                            Qty = dsOrder.Tables[2].Rows[i]["Quantity"].ToString(),
                            Amount = Convert.ToDecimal(dsOrder.Tables[2].Rows[i]["Amount"].ToString()),
                            SGST = Convert.ToDecimal(dsOrder.Tables[2].Rows[i]["SGST"].ToString()),
                            CGST = Convert.ToDecimal(dsOrder.Tables[2].Rows[i]["CGST"].ToString()),
                            IGST = Convert.ToDecimal(dsOrder.Tables[2].Rows[i]["IGST"].ToString()),
                            Tax = dsOrder.Tables[2].Rows[i]["Tax"].ToString(),
                            TaxableAmt = dsOrder.Tables[2].Rows[i]["TaxableAmt"].ToString(),
                            SGSTAmt = dsOrder.Tables[2].Rows[i]["SGSTAmt"].ToString(),
                            CGSTAmt = dsOrder.Tables[2].Rows[i]["CGSTAmt"].ToString(),
                            IGSTAmt = dsOrder.Tables[2].Rows[i]["IGSTAmt"].ToString(),
                            TotalTaxAmt = dsOrder.Tables[2].Rows[i]["TaxAmt"].ToString(),
                            TotAmount = dsOrder.Tables[2].Rows[i]["TotAmount"].ToString(),
                            HSNNo = dsOrder.Tables[2].Rows[i]["HSNCode"].ToString(),

                        });



                        TotalAmount += Convert.ToInt32(dsOrder.Tables[2].Rows[i]["TotAmount"]);
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


                    ViewBag.PayMentMode = dsOrder.Tables[1].Rows[0]["PayMentMode"].ToString();
                    //ViewBag.TransactionNo = dsOrder.Tables[0].Rows[0]["TransactionNo"].ToString();
                    ViewBag.Amount = dsOrder.Tables[1].Rows[0]["Amount"].ToString();
                    ViewBag.PaymentDate = dsOrder.Tables[1].Rows[0]["OrderDate"].ToString();

                }
                return View(orderConfirmation);
            }
            return View(orderConfirmation);
        }
        public JsonResult ViewOrderItemsDetail(string RequestId, Reports reports)
        {
            DataTable dt = new DataTable();
            reports.Pk_OrderId = RequestId;
            List<Reports> lst = new List<Reports>();
            DataSet dataSet = reports.AllGetShoppingOrderDataDetails();
            reports.dtDetails = dataSet.Tables[0];

            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                Reports reports1 = new Reports();
                reports1.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                reports1.Quantity = int.Parse(dataSet.Tables[0].Rows[i]["Quantity"].ToString());
                reports1.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["Amount"].ToString());
                reports1.FinalAmount = dataSet.Tables[0].Rows[i]["FinalAmount"].ToString();
                lst.Add(reports1);
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrderForShipping(string OrderNo, string Length, string Breadth, string Height, string Weight)
        {
            Reports reports = new Reports();
            List<OrderItems> lstOrder = new List<OrderItems>();
            reports.OrderNo = OrderNo;
            DataSet dataSet = reports.GetShoppingOrderDetails();

            string name = dataSet.Tables[0].Rows[0]["Name"].ToString();
            string orderdate = dataSet.Tables[0].Rows[0]["OrderDateForShipRocket"].ToString();
            string Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
            string City = dataSet.Tables[0].Rows[0]["City"].ToString();
            string PinCode = dataSet.Tables[0].Rows[0]["PinCode"].ToString();
            string State = dataSet.Tables[0].Rows[0]["State"].ToString();
            string Email = dataSet.Tables[0].Rows[0]["Email"].ToString();
            string MobileNo = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
            string Country = "India";

            DataSet dataSet1 = reports.GetShoppingOrderDetailsForOrderNo();
            for (int i = 0; i < dataSet1.Tables.Count; i++)
            {
                OrderItems orderItems = new OrderItems();
                orderItems.name = dataSet1.Tables[i].Rows[0]["ProductName"].ToString();
                orderItems.sku = dataSet1.Tables[i].Rows[0]["ProductName"].ToString();
                orderItems.units = dataSet1.Tables[i].Rows[0]["Quantity"].ToString();
                orderItems.selling_price = dataSet1.Tables[i].Rows[0]["FinalAmount"].ToString();
                orderItems.discount = "0";
                orderItems.tax = "0";
                orderItems.hsn = dataSet1.Tables[i].Rows[0]["HSNCode"].ToString();
                lstOrder.Add(orderItems);
            }
            string subtotal = dataSet1.Tables[0].Compute("Sum(FinalAmount)", "").ToString();

            string Aucthentication = Reports.HITAPIAUcthenication(APIURL.Authorization, "LifeoneWellness@gmail.com", "LifeoneWellnessadmin@108");
            AuthorizationResponse deserializedProduct1 = JsonConvert.DeserializeObject<AuthorizationResponse>(Aucthentication);


            string result = Reports.HITAPIForCreateOrder(APIURL.CreateOrderForShipping, OrderNo, orderdate, name, "", Address, "", City, PinCode, State, Country, Email, MobileNo, lstOrder, subtotal, Length, Breadth, Height, Weight, deserializedProduct1.token);
            ShippingResponse deserializedProduct = JsonConvert.DeserializeObject<ShippingResponse>(result);
            reports.ShippingId = deserializedProduct.shipment_id;
            reports.FK_OrderId = deserializedProduct.order_id;
            DataSet dataSet2 = reports.UpdateDeliveryDetaails();

            return Json(reports, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ViewOrderItemDetail(string OrderId)
        {
            Reports reports = new Reports();
            reports.FK_OrderId = OrderId;
            reports.OpCode = 7;
            List<Reports> lst = new List<Reports>();
            DataSet dataSet = reports.GetOrderitemDetails();
            reports.dtDetails = dataSet.Tables[0];

            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                Reports reports1 = new Reports();
                reports1.CategoryName = dataSet.Tables[0].Rows[i]["CategoryName"].ToString();
                reports1.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                reports1.Quantity = int.Parse(dataSet.Tables[0].Rows[i]["Quantity"].ToString());
                reports1.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["Amount"].ToString());
                reports1.FinalAmount = dataSet.Tables[0].Rows[i]["FinalAmount"].ToString();
                lst.Add(reports1);
            }


            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TallyGST(Reports reports, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Repurchase Tally GST Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            reports.Page = Page;
            reports.Size = SessionManager.Size;


            DataSet dataSet = reports.GetTallyGSTReport();
            reports.dtGetTallyGSTReport = dataSet.Tables[0];
            Session["TallyGSTReportList"] = dataSet.Tables[0];
            int totalRecords = 0;
            if (reports.dtGetTallyGSTReport.Rows.Count > 0)
            {

                totalRecords = Convert.ToInt32(reports.dtGetTallyGSTReport.Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            else
            {
            }
            return View(reports);
        }
        public ActionResult ExportToExcelGetTallyGSTReport()
        {
            Reports reports = new Reports();
            reports.Page = 1;
            reports.IsExport = 1;
            DataSet dataSet = reports.GetTallyGSTReport();
            reports.dtDetails = dataSet.Tables[0];
            reports.dtGetTallyGSTReport = dataSet.Tables[0];

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(reports.dtDetails);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Repur. Tally GST Report.xlsx");
                }
            }
            return null;
        }
        public ActionResult VerifyFranchiseeKYC(VerifyPanOrVoterid verifyPanOrVoterid, string Type, string Page)
        {
            if (!PermissionManager.IsActionPermit("Verify Franchisee KYC", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            Reports reports = new Reports();
            if (Page == "0" || Page == null)
            {
                Page = "1";
            }
            verifyPanOrVoterid.Page = int.Parse(Page);
            verifyPanOrVoterid.Type = Type;
            DataSet dataSet = reports.GetFranchiseeKYCDetails(verifyPanOrVoterid);
            List<VerifyPanOrVoterid> list = new List<VerifyPanOrVoterid>();
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    VerifyPanOrVoterid KYC = new VerifyPanOrVoterid();
                    KYC.Name = dr["PersonName"].ToString();
                    KYC.LoginId = dr["LoginID"].ToString();
                    KYC.Mobile = dr["Contact"].ToString();
                    KYC.PanCard = dr["PanCard"].ToString();
                    KYC.PanImage = dr["PanImage"].ToString();
                    KYC.AadharImage = dr["AadharImage"].ToString();
                    KYC.AadharBackImage = dr["AadharBackImage"].ToString();
                    KYC.AadharNumber = dr["AadharNumber"].ToString();
                    KYC.BankImage = dr["BankDoc"].ToString();
                    KYC.BankName = dr["BankName"].ToString();
                    KYC.AccountHolder = dr["AccountHolder"].ToString();
                    KYC.AccountNumber = dr["AccountNumber"].ToString();
                    KYC.IfscCode = dr["IfscCode"].ToString();
                    KYC.Fk_MemId = long.Parse(dr["Fk_MemId"].ToString());
                    KYC.PanCardStatus = dr["PanStatus"].ToString();
                    KYC.BankStatus = dr["BankStatus"].ToString();
                    KYC.AddressProofBackStatus = dr["AadharBackStatus"].ToString();
                    KYC.AddressProofStatus = dr["AadharStatus"].ToString();
                    KYC.TotalRecords = int.Parse(dr["TotalRecords"].ToString());
                    list.Add(KYC);
                }
                verifyPanOrVoterid.List = list;
                int totalRecords = 0;
                if (verifyPanOrVoterid.List[0].TotalRecords > 0)
                {

                    totalRecords = Convert.ToInt32(verifyPanOrVoterid.List[0].TotalRecords);
                    var pager = new Pager(totalRecords, verifyPanOrVoterid.Page, SessionManager.Size);
                    verifyPanOrVoterid.Pager = pager;
                }
                else
                {
                    verifyPanOrVoterid.List = new List<VerifyPanOrVoterid>();
                }
            }

            return View(verifyPanOrVoterid);
        }
        public ActionResult GetFranchiseeKycDetails(string Fk_MemId)
        {
            VerifyPanOrVoterid KYC = new VerifyPanOrVoterid();
            KYC.Fk_MemId = long.Parse(Fk_MemId);
            Reports reports = new Reports();
            DataSet dataSet = reports.GetFranchiseeKYCDetails(KYC);

            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {

                    KYC.Name = dr["PersonName"].ToString();
                    KYC.LoginId = dr["LoginID"].ToString();
                    KYC.Mobile = dr["Contact"].ToString();
                    KYC.PanCard = dr["PanCard"].ToString();
                    KYC.PanImage = dr["PanImage"].ToString();
                    KYC.AadharImage = dr["AadharImage"].ToString();
                    KYC.AadharBackImage = dr["AadharBackImage"].ToString();
                    KYC.AadharNumber = dr["AadharNumber"].ToString();
                    KYC.BankImage = dr["BankDoc"].ToString();
                    KYC.BankName = dr["BankName"].ToString();
                    KYC.AccountHolder = dr["AccountHolder"].ToString();
                    KYC.AccountNumber = dr["AccountNumber"].ToString();
                    KYC.IfscCode = dr["IfscCode"].ToString();
                    KYC.Fk_MemId = long.Parse(dr["Fk_MemId"].ToString());
                    KYC.PanCardStatus = dr["PanStatus"].ToString();
                    KYC.BankStatus = dr["BankStatus"].ToString();
                    KYC.AddressProofBackStatus = dr["AadharBackStatus"].ToString();
                    KYC.AddressProofStatus = dr["AadharStatus"].ToString();
                    KYC.Flag = int.Parse(dr["Code"].ToString());
                }
            }
            return Json(KYC, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApproveFranchiseeKYC(string Fk_MemId, string Status, string Remark, string IsPanApproved, string IsAddressApproved, string IsAddressBackApproved, string IsBankApproved)
        {
            VerifyPanOrVoterid obj = new VerifyPanOrVoterid();
            try
            {
                obj.IsPanApproved = IsPanApproved;
                obj.IsAadharApproved = IsAddressApproved;
                obj.IsAadharBackApproved = IsAddressBackApproved;
                obj.IsBankApproved = IsBankApproved;
                obj.Fk_MemId = Convert.ToInt64(Fk_MemId);
                obj.Status = Status;
                obj.Remark = Remark;
                obj.AddedBy = SessionManager.Fk_MemId;
                Reports reports = new Reports();
                DataSet dataSet = reports.ApproveDeclineFranchiseeKYC(obj);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        obj.Flag = int.Parse(dr["Flag"].ToString());
                        obj.Msg = dr["Message"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AdminUploadFranchiseeKyc(MAdminUploadKyc model)
        {
            Reports reports = new Reports();
            DataSet dataSet = reports.UploadFranchiseeKYC(model);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    model.Code = int.Parse(dr["Code"].ToString());
                    model.Remark = dr["Remark"].ToString();
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RefundAmount(int? Page, Member_RefundAmountModel _result)
        {
            if (!PermissionManager.IsActionPermit("Refund Amount", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }


            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _result.Page = Page;
            _result.Size = SessionManager.Size;

            _result.RefundAmountList = UtilityService.RefundAmount(_result);

            if (_result.IsExport == 1)
            {
                DataTable dt = new DataTable("Refund Amount");
                dt.Columns.AddRange(new DataColumn[7] {

                                             new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Mobile No"),
                                            new DataColumn("Transaction No"),
                                            new DataColumn("Transaction Date"),

                                            new DataColumn("Credit Amount"),
                                            new DataColumn("Remark"),


            });
                if (_result.RefundAmountList != null && _result.RefundAmountList.Count > 0)
                {
                    foreach (var row in _result.RefundAmountList)
                    {
                        dt.Rows.Add(row.LoginId, row.MemberName, row.Mobile, row.TransNo, row.TransDate, row.CreditAmount, row.Description);
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Refund Amount.xlsx");
                    }
                }

            }
            else
            {
                int totalRecords = 0;
                if (_result.RefundAmountList.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_result.RefundAmountList[0].TotalRecord);
                    var pager = new Pager(totalRecords, _result.Page, SessionManager.Size);
                    _result.Pager = pager;
                }
                else
                {
                    _result.RefundAmountList = new List<Member_RefundAmountModel>();
                }

                return View(_result);
            }

        }

        [HttpPost]
        public JsonResult RefundAmount(Member_RefundAmountModel model)
        {

            Member_RefundAmountModel _result = new Member_RefundAmountModel();
            _result = UtilityService.AddRefundAmount(model);
            return Json(_result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PackageWayActivation(Reports reports)
        {
            try
            {
                if (!PermissionManager.IsActionPermit("Package Way Activation", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                if (reports.Page == null || reports.Page == 0)
                {
                    reports.Page = 1;
                }
                reports.Size = SessionManager.Size;                
                DataSet dataSet = reports.GetPackagewayActvationReport();
                ViewBag.PackageList = DALBindCommonDropdown.PackageBindDropdown(16, 0);
                ViewBag.PaymentModeList = DALBindCommonDropdown.DropdownPaymentMode();
                reports.dtDetails = dataSet.Tables[0];
                if (reports.IsExport == 1)
                {

                    DataTable dt = new DataTable("Package Way Activation");
                    dt.Columns.AddRange(new DataColumn[10] {

                                             new DataColumn("Login Id"),
                                             new DataColumn("Display Name"),
                                            new DataColumn("Package Name"),
                                            new DataColumn("Mobile No"),
                                            new DataColumn("Date of Joining"),
                                            new DataColumn("Date of Activation"),
                                            new DataColumn("Payment Mode"),
                                            new DataColumn("Transaction Number"),
                                            new DataColumn("Check Date"),
                                            new DataColumn("Create Date"),


            });
                    //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
                    if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            dt.Rows.Add(row["LoginId"].ToString(), row["DisplayName"].ToString(), row["PackageName"].ToString(), row["mobile"].ToString(), row["DOJ"].ToString(), row["DOA"].ToString(), row["PaymentMode"].ToString(), row["TransNo"].ToString(), row["DDChequedate"].ToString(), row["CreateDate"].ToString());
                        }
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Package Way Activation.xlsx");
                        }
                    }
                }
                else
                {
                    
                    int totalRecords = 0;
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {

                        totalRecords = Convert.ToInt32(reports.dtDetails.Rows[0]["TotalRecords"].ToString());
                        var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                        reports.Pager = pager;
                    }
                }


            }
            catch (Exception)
            {
                throw;
            }
            return View(reports);
        }

        public ActionResult PurchaseOrdersList(Reports reports, string IsExport)
        {
            if (!PermissionManager.IsActionPermit("Purchase Order Details", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            DataSet dataSet = new DataSet();
            #region BindSupplier
            ProductsDetail product = new ProductsDetail();
            List<SelectListItem> ddlSupplierDetails = new List<SelectListItem>();
            dataSet = product.GetSupplierDetails();

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                ddlSupplierDetails.Add(new SelectListItem { Text = "Select Supplier Name", Value = "0" });
                foreach (DataRow r in dataSet.Tables[0].Rows)
                {
                    ddlSupplierDetails.Add(new SelectListItem { Text = r["SupplierName"].ToString(), Value = r["Pk_SupplierId"].ToString() });
                }
            }
            ViewBag.ddlSupplierDetails = ddlSupplierDetails;
            #endregion
            if (reports.Page == 0 || reports.Page == null)
            {
                reports.Page = 1;
            }
            reports.OpCode = 1;
            reports.Size = SessionManager.Size;
            dataSet = reports.GetPurchaseOrders();
            reports.dtDetails = dataSet.Tables[0];
            if (!string.IsNullOrEmpty(IsExport))
            {
                reports.Page = 1;
                reports.IsExport = 1;
                dataSet = reports.GetPurchaseOrders();
                reports.dtDetails = dataSet.Tables[0];
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(reports.dtDetails);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PurchaseOrders.xlsx");
                    }
                }

            }
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = Convert.ToInt32(reports.dtDetails.Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            return View(reports);
        }
        public ActionResult PurchaseOrdersDetails(string Fk_PurchaseOrderId)
        {
            Reports reports = new Reports();
            List<Reports> lst = new List<Reports>();
            reports.OpCode = 2;
            reports.Fk_PurchaseOrderId = Fk_PurchaseOrderId;
            DataSet dataSet = reports.GetPurchaseOrders();
            reports.dtDetails = dataSet.Tables[0];

            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                Reports reports1 = new Reports();
                reports1.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                reports1.Quantity = int.Parse(dataSet.Tables[0].Rows[i]["Quantity"].ToString());
                reports1.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["PurchasePrice"].ToString());
                reports1.FinalAmount = dataSet.Tables[0].Rows[i]["FinalAmount"].ToString();
                lst.Add(reports1);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ActivationTallyGST(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Activation Tally GST Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == null || reports.Page == 0)
            {
                reports.Page = 1;
            }

            reports.Size = SessionManager.Size;
            DataSet dataSet = reports.GetActivationTallyGSTReport();
            reports.dtGetTallyGSTReport = dataSet.Tables[0];
            Session["TallyGSTReportList"] = dataSet.Tables[0];
            if (reports.IsExport == 1)
            {

                DataTable dt = new DataTable("Activation Tally GST Report");
                dt.Columns.AddRange(new DataColumn[18] {

                                             new DataColumn("Date"),
                                             new DataColumn("Invoice No."),
                                            new DataColumn("Account Name"),
                                             new DataColumn("Address"),
                                            new DataColumn("GST No"),
                                            new DataColumn("HSN Code"),
                                            new DataColumn("Product Name"),
                                            new DataColumn("Unit Name"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("GST"),
                                            new DataColumn("Rate"),
                                            new DataColumn("Discount Amount"),
                                            new DataColumn("Taxable Amount"),
                                            new DataColumn("Tax Amount"),
                                            new DataColumn("CGST"),
                                            new DataColumn("SGST"),
                                            new DataColumn("IGST"),
                                            new DataColumn("State"),


            });
                //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
                if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        dt.Rows.Add(row["OrderDate"].ToString(), row["InvoiceNo"].ToString(), row["AccountName"].ToString(), row["Address"].ToString(), row["GSTNo"].ToString(), row["HSNCode"].ToString(), row["ProductName"].ToString(), row["UnitName"].ToString(), row["Quantity"].ToString(), row["GST"].ToString(), row["Rate"].ToString(), row["DiscountAmt"].ToString(), row["TaxableAmount"].ToString(), row["TaxAmount"].ToString(), row["CGST"].ToString(), row["SGST"].ToString(), row["IGST"].ToString(), row["State"].ToString());
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Activation Tally GST Report.xlsx");
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (reports.dtGetTallyGSTReport.Rows.Count > 0)
                {

                    totalRecords = Convert.ToInt32(reports.dtGetTallyGSTReport.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                    reports.Pager = pager;
                }
            }

            return View(reports);
        }
        public ActionResult ExportToExcelActivationTallyGSTReport()
        {
            Reports reports = new Reports();
            reports.Page = 1;
            reports.IsExport = 1;
            DataSet dataSet = reports.GetActivationTallyGSTReport();
            reports.dtDetails = dataSet.Tables[0];
            reports.dtGetTallyGSTReport = dataSet.Tables[0];

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(reports.dtDetails);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Activation Tally GST Report.xlsx");
                }
            }
            return null;
        }


        public ActionResult FranchiseeStockReturn(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Franchisee Stock Return", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == null || reports.Page == 0)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            DataSet dataSet = reports.GetPayPayoutReport();
            reports.dtGetPayPayoutReport = dataSet.Tables[0];
            //Session["PayoutReportList"] = dataSet.Tables[0];
            if (reports.IsExport == 1)
            {
                DataTable dt = new DataTable("Pay Payout Report");
                dt.Columns.AddRange(new DataColumn[5] {

                                             new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Payout No"),
                                            new DataColumn("Closing Date"),
                                            new DataColumn("Net Amount"),




            });
                //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
                if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        dt.Rows.Add(row["LoginId"].ToString(), row["PersonName"].ToString(), row["PayoutNo"].ToString(), row["ClosingDate"].ToString(), row["NetAmount"].ToString());
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Pay Payout Report.xlsx");
                    }
                }
            }
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                totalRecords = Convert.ToInt32(reports.dtGetPayPayoutReport.Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            return View();
        }
        public ActionResult PurchaseOrderList(Reports report)
        {
            if (!PermissionManager.IsActionPermit("Purchase Order Details", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (report.Page == null || report.Page == 0)
            {
                report.Page = 1;
            }
            report.Size = SessionManager.Size;
            DataSet dataset = report.GetPurchaseOrderList();
            report.dtDetails = dataset.Tables[0];
            if (report.IsExport == 1)
            {

                DataTable dt = new DataTable("Purchase Order List");
                dt.Columns.AddRange(new DataColumn[6] {

                                             new DataColumn("Bill Date"),
                                             new DataColumn("Bill No."),
                                            new DataColumn("Purchase Order No"),
                                            new DataColumn("Purchase Price"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Total Amount"),
            });
                //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
                if (dataset.Tables[0].Rows != null && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        dt.Rows.Add(row["BillDate"].ToString(), row["BillNo"].ToString(), row["PurchaseOrderNo"].ToString(), row["PurchasePrice"].ToString(), row["Quantity"].ToString(), row["FinalAmount"].ToString());
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Purchase Order List.xlsx");
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (dataset.Tables[0].Rows.Count > 0)
                {

                    totalRecords = Convert.ToInt32(report.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, report.Page, SessionManager.Size);
                    report.Pager = pager;
                }
            }

            return View(report);
        }
        public ActionResult RefunfBalance(string LoinId, string Balance, string Refundbal, string Fk_ProductId)
        {
            Reports obj = new Reports();
            List<Reports> list = new List<Reports>();
            try
            {
                obj.Fk_ProductId = int.Parse(Fk_ProductId);
                obj.LoginId = Convert.ToString(LoinId);
                obj.Refund_stock = Convert.ToString(Refundbal);
                obj.Current_stock = Convert.ToString(Balance);
                obj.AddedBy = Convert.ToInt32(SessionManager.Fk_MemId);


                DataSet ds = obj.Setrefundstock();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Reports listdata = new Reports();
                    listdata.flag = ds.Tables[0].Rows[0]["flag"].ToString();
                    listdata.message = ds.Tables[0].Rows[0]["message"].ToString();
                    list.Add(listdata);


                }
                obj.list = list;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(obj);
        }

        [HttpPost]
        public JsonResult BlockUnblock_Mem(long fk_memID, string Status, string remark)
        {
            BlockUnblockResponse obj = new BlockUnblockResponse();


            try
            {
                BlockUnblockService _objService = new BlockUnblockService();
                obj = _objService.BlockUnblockMemberServiceV2(fk_memID, Status, remark);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PrintPayoutReportForBank(int? Page)
        {
            MPayoutReport obj = new MPayoutReport();
            List<MPayoutReport> reportBankList = Session["PReportBank"] as List<MPayoutReport>;
            //ViewBag.LoginId = obj.MemberLoginId;
            //MPayoutReport reportFromSession = Session["PReportBank"] as MPayoutReport;
            //  obj = Session["PReportBank"];

            //return RedirectToAction("PayoutReportForBank", "AdminReport");
            obj.Objlist = reportBankList;
            return View(obj);

        }

        public ActionResult GeustShoppingOrderDetail(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Shopping Order Details", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == 0 || reports.Page == null)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            DataSet dataSet = reports.GetGeustShoppingOrderDetails();
            reports.dtGetShoppingOrderDetails = dataSet.Tables[0];
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = Convert.ToInt32(reports.dtGetShoppingOrderDetails.Rows[0]["TotalRecord"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            return View(reports);

        }
        public ActionResult ShoppingOrderInvoice(string id)
        {
            ShoppingOrderInvoiceModel orderInvoice = new ShoppingOrderInvoiceModel();
            OrderDAL orderDAL = new OrderDAL();
            DataSet dsOrder = orderDAL.GetShoppingOrderInvoice(id);
            orderInvoice.dtDetails = dsOrder.Tables[0];            
            return View(orderInvoice);
        }
        public ActionResult CancelOrderDetails(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Cancel Order Details", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == 0 || reports.Page == null)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            DataSet dataSet = reports.GetCancelOrderDetails();
            reports.dtGetShoppingOrderDetails = dataSet.Tables[0];
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = Convert.ToInt32(reports.dtGetShoppingOrderDetails.Rows[0]["TotalRecord"].ToString());
                var pager = new Pager(totalRecords, reports.Page, SessionManager.Size);
                reports.Pager = pager;
            }
            return View(reports);
        }
        public ActionResult RewardDetails(Rewards rewards)
        {
            if (!PermissionManager.IsActionPermit("Reward Details", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (rewards.Page == 0 || rewards.Page == null)
            {
                rewards.Page = 1;
            }
            ViewBag.RewardStatus = rewards.RewardStatus;
            rewards.Size = SessionManager.Size;
            DataSet dataSet = rewards.GetRewardDetails();
            rewards.getRewardDetails = dataSet.Tables[0];
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = Convert.ToInt32(rewards.getRewardDetails.Rows[0]["TotalRecord"].ToString());
                var pager = new Pager(totalRecords, rewards.Page, SessionManager.Size);
                rewards.Pager = pager;
            }
            return View(rewards);
        }
        public ActionResult TopupInvoice(string id)
        {
            TopupInvoiceModel topupInvoice = new TopupInvoiceModel();
            OrderDAL orderDAL = new OrderDAL();
            DataSet dsOrder = orderDAL.GettopupInvoice(id);
            topupInvoice.dtDetails = dsOrder.Tables[0];
            topupInvoice.dtDetails1 = dsOrder.Tables[1];
            return View(topupInvoice);
        }
    }
}