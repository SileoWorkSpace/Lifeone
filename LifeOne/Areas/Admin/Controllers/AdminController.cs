using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.HomeManagement.HService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using LifeOne.Filters;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System.Data;
using ClosedXML.Excel;
using System.Web.Security;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.API.DAL;
using LifeOne.Models.HomeManagement.HDAL;
using LifeOne.Models.API;
using System.Net.Http;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;
using LifeOne.Models;
using LifeOne.Models.FranchiseManagement.FDAL;
using Newtonsoft.Json;
using System.Configuration;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    [RouteArea("Admin")]
    public class AdminController : Controller
    {
        // GET: Admin/Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            if (!PermissionManager.IsTabPermit("Dashboard"))
                if (!PermissionManager.IsActionPermit("Dashboard", ViewOptions.FormView))
                {

                    return Redirect("/Home/adminlogin");
                }
            MAdminDashbord Value = new MAdminDashbord();
            dynamic item = new ExpandoObject();
            AdminDashboardService objservice = new AdminDashboardService();
            MAdminDashbord mAdminDashbord = new MAdminDashbord();
            DataSet dataSet = objservice.GetAdminDashboard();
            ViewBag.TotalAssocaite = dataSet.Tables[0].Rows[0]["TotalAssocaite"].ToString();
            ViewBag.PackageCount = dataSet.Tables[0].Rows[0]["PackageCount"].ToString();
            ViewBag.RepurchaseCount = dataSet.Tables[0].Rows[0]["RepurchaseCount"].ToString();
            ViewBag.TodaysJoining = dataSet.Tables[0].Rows[0]["TodaysJoining"].ToString();
            ViewBag.ActiveAssociate = dataSet.Tables[0].Rows[0]["ActiveAssociate"].ToString();
            ViewBag.DirectIncome = dataSet.Tables[0].Rows[0]["DirectIncome"].ToString();
            ViewBag.MatchingIncome = dataSet.Tables[0].Rows[0]["MatchingIncome"].ToString();
            ViewBag.FreePackageSale = dataSet.Tables[0].Rows[0]["FreePackageSale"].ToString();
            ViewBag.PaidPackageSale = dataSet.Tables[0].Rows[0]["PaidPackageSale"].ToString();
            ViewBag.TotalPackageSale = dataSet.Tables[0].Rows[0]["TotalPackageSale"].ToString();
            mAdminDashbord.dtPaidPackage = dataSet.Tables[1];
            mAdminDashbord.dtFreePackage = dataSet.Tables[2];

            return View(mAdminDashbord);
        }

        public ActionResult EmpDashboard()
        {
            return View();
        }

        public ActionResult Members(MembersResponse _objres, string FromDate, string ToDate, string City, string LoginId)
        {
            if (!PermissionManager.IsActionPermit("Direct Seller List", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            MMemberList _model = new MMemberList();

            _model.FromDate = FromDate;
            _model.ToDate = ToDate;
            _model.City = City;

            _model.LoginId = _objres.LoginId;

            _model.Mobile = _objres.Mobile;

            _model.MemberName = _objres.MemberName;

            _model.InvitedBy = _objres.InvitedBy;

            _model.InviteCode = _objres.InviteCode;


            if (_objres.Page == null || _objres.Page == 0)
            {
                _objres.Page = 1;
            }
            _model.IsExport = _objres.IsExport;

            _model.Page = _objres.Page;
            _objres.Size = SessionManager.Size;
            _objres.List = MembersService.InvokeMemberDetails(_model);
            if (_objres.List != null && _objres.List.Count > 0)
            {
                _objres.List[0].FromDate = FromDate ?? string.Empty;
                _objres.List[0].ToDate = ToDate ?? string.Empty;
                _objres.List[0].City = City ?? string.Empty;
                //_objres.Page = Page;
            }

            if (_model.Searchby != null)
            {
                _objres.Searchby = _model.Searchby;
            }
            if (_model.SearchParam != null)
            {
                _objres.SearchParam = _model.SearchParam;
            }
            if (_objres.IsExport == 1)
            {
                _objres.List = MembersService.InvokeMemberDetails(_model);
                DataTable dt = new DataTable("Direct Sellers List");
                dt.Columns.AddRange(new DataColumn[13] {
                                                     new DataColumn("Login Id"),
                                                      new DataColumn("Password"),
                                                     new DataColumn("Name"),
                                                    new  DataColumn("Mobile"),
                                                     new DataColumn("Sponsor's Id"),
                                                      new DataColumn("Sponsor's Name"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("SMobile"),
                                                     new DataColumn("State"),
                                                     new DataColumn("Join on"),
                                                     new DataColumn("Place Under Id"),
                                                     new DataColumn("Place Under Name"),
                                                     new DataColumn("Member Status")


            });
                if (_objres.List.Count > 0)
                {
                    foreach (var customer in _objres.List)
                    {
                        dt.Rows.Add(customer.LoginId, customer.Password, customer.MemberName, customer.Mobile, customer.SponsorId, customer.SponsorName, customer.Email, customer.SponsorMobile, customer.state, customer.joiningDate, customer.InviteCode, customer.InvitedBy, customer.Status);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Direct Sellers List.xlsx");
                        }
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (_objres.List.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_objres.List[0].ToatlRecord);
                    var pager = new Pager(totalRecords, _objres.Page, SessionManager.Size);
                    _objres.Pager = pager;
                }
            }

            return View(_objres);
        }
        //[HttpPost]
        //public ActionResult Members(MMemberList _model, MembersResponse _objres, int? Page)
        //{
        //    if (!PermissionManager.IsActionPermit("Direct Seller List", ViewOptions.FormView))
        //    {
        //        return Redirect("/Home/adminlogin");
        //    }

        //    if (_model.Searchby == "LoginId")
        //    {
        //        _model.LoginId = _model.SearchParam;
        //    }
        //    if (_model.Searchby == "Mobile")
        //    {
        //        _model.Mobile = _model.SearchParam;
        //    }
        //    if (_model.Searchby == "Name")
        //    {
        //        _model.MemberName = _model.SearchParam;
        //    }
        //    if (_model.Searchby == "InvitedBy")
        //    {
        //        _model.InvitedBy = _model.SearchParam;
        //    }
        //    if (_model.Searchby == "InviteCode")
        //    {
        //        _model.InviteCode = _model.SearchParam;
        //    }

        //    if (_objres.Page == null || _objres.Page == 0)
        //    {
        //        _objres.Page = 1;
        //    }
        //    _objres.Size = SessionManager.Size;
        //    //_model.Page = Page;
        //    _objres.List = MembersService.InvokeMemberDetails(_model);
        //    _objres.List[0].FromDate = _model.FromDate ?? string.Empty;
        //    _objres.List[0].ToDate = _model.ToDate ?? string.Empty;
        //    _objres.List[0].City = _model.City ?? string.Empty;
        //    _objres.Page = Page;
        //    if (_model.Searchby != null)
        //    {
        //        _objres.Searchby = _model.Searchby;
        //    }
        //    if (_model.SearchParam != null)
        //    {
        //        _objres.SearchParam = _model.SearchParam;
        //    }

        //    else
        //    {
        //        int totalRecords = 0;
        //        if (_objres.List.Count > 0)
        //        {

        //            totalRecords = Convert.ToInt32(_objres.List[0].ToatlRecord);
        //            var pager = new Pager(totalRecords, _objres.Page, SessionManager.Size);
        //            _objres.Pager = pager;
        //        }
        //    }


        //    return View(_objres);
        //}

        public ActionResult AllTransactionLog(int? Page, MTransactionLog _model)
        {
            if (!PermissionManager.IsActionPermit("All Transaction Log", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }


            //MTransactionLog _model = new MTransactionLog();
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.Page = Page;
            _model.Size = SessionManager.Size;
            _model.ProcId = 0;
            _model.LstTransactionLog = AllTransactionService.AllTransactionLog(_model);
            _model.ObjList = new List<SelectListItem>();
            _model.ObjList.Add(new SelectListItem { Text = "Select Utility", Value = "" });
            _model.ObjList.AddRange(LifeOne.Models.Common.DBHelperDapper.DAGetDetailsInList<SelectListItem>("exec Proc_BindDropDownForAllTrans"));
            ViewBag.ddlUtilityTypes = _model.ObjList;
            if (_model.IsExport == 1)
            {
                _model.LstTransactionLog = AllTransactionService.AllTransactionLog(_model);
                DataTable dt = new DataTable("All Transaction Log");
                dt.Columns.AddRange(new DataColumn[9] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Number"),
                                                     new DataColumn("Transaction Id/Action Type"),
                                                     new DataColumn("Session/Auth Code"),
                                                     new DataColumn("Payment Date"),
                                                     new DataColumn("Amount"),
                                                     new DataColumn("Status")


            });

                if (_model.LstTransactionLog.Count > 0)
                {
                    foreach (var customer in _model.LstTransactionLog)
                    {
                        dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.Mobile, customer.Number, "INR" + customer.TransactionId + "\n" + customer.ActionType, customer.Session + "\n" + customer.SessionAuthCode, customer.PaymentDate, customer.Amount, customer.Status);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "All Transaction Log.xlsx");
                        }
                    }
                }
            }
            else
            {
                int totalRecords = 0;
                if (_model.LstTransactionLog.Count > 0)
                {
                    totalRecords = Convert.ToInt32(_model.LstTransactionLog[0].TotalRecord);
                    var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                    _model.Pager = pager;
                }
            }


            return View(_model);
        }


        //[HttpPost]
        //public ActionResult AllTransactionLog(MTransactionLog _model)
        //{
        //    if (_model.Page == null || _model.Page == 0)
        //    {
        //        _model.Page = 1;
        //    }            
        //    _model.Size = SessionManager.Size;
        //    _model.ProcId = 0;
        //    _model.LstTransactionLog = AllTransactionService.AllTransactionLog(_model);
        //    return View(_model);
        //}

        public ActionResult ViewProfile(int Fk_MemId)
        {
            MTransactionLog _model1 = new MTransactionLog();
            CustomerRegService _objService = new CustomerRegService();
            MCustomerRegistration objres = new MCustomerRegistration();
            objres = CustomerRegService.InvokeCustomerDetailsById(Fk_MemId);
            _model1.LoginId = objres.LoginId;
            _model1.Page = 1;
            _model1.Size = 6;
            _model1.Fk_MemId = Fk_MemId;
            objres.WalletDetailList = AllTransactionService.WalletDetail(_model1);
            objres.CashBackWalletDetailList = AllTransactionService.CashBackWalletDetail(_model1);
            objres.CommissionDetailList = AllTransactionService.CommissionDetailList(_model1);

            Reports reports = new Reports();
            reports.FK_MemId = Fk_MemId;
            DataSet dataSet = reports.GetBusinessSummaryForAssociate();
            objres.dtBusinessDetails = dataSet.Tables[0];
            objres.dtDirectBusiness = dataSet.Tables[1];

            //objres.lstChild = CustomerRegService.InvokeChildDetailsById(Fk_MemId);
            return View(objres);
        }
        public ActionResult EditUserDetails(MCustomerRegistration objres, int Fk_MemId)
        {
            if (Fk_MemId != 0)
            {
                CustomerRegService _objService = new CustomerRegService();

                objres = CustomerRegService.InvokeCustomerDetailsById(Fk_MemId);
                TempData["GetGender"] = objres.Gender;
                TempData["GetMaritalStatus"] = objres.MaritalStatus;
                objres.lstChild = CustomerRegService.InvokeChildDetailsById(Fk_MemId);
                //return View(objres);
            }
            return View(objres);
        }

        [HttpPost]
        public JsonResult GetReferDetails(string Fk_SponserId)
        {
            CustomerRegService _objService = new CustomerRegService();
            var res = CustomerRegService.InvokeGetSponserDetails(Fk_SponserId);
            return Json(res);
        }


        [HttpPost]
        public JsonResult GetStateCityTehsilByPincode(string Pincode)
        {
            CustomerRegService _objService = new CustomerRegService();
            var res = CustomerRegService.InvokeGetDistrictDetails(Pincode);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Uploadfile(HttpPostedFileBase file, string subdir)
        {
            var objres = (object)null;
            if (file != null && file.ContentLength > 0)
            {
                var ImagePath = "~/ReadWriteData/" + subdir + "/";
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".BMP" || extension.ToUpper() == ".PDF" || extension.ToUpper() == ".DOCX" || extension.ToUpper() == ".XLSX")
                {
                    if (!Directory.Exists(Server.MapPath(ImagePath)))
                    {
                        Directory.CreateDirectory((Server.MapPath(ImagePath)));
                    }
                    //Guid.NewGuid().ToString().Replace("_", "") + "_" + InputFileName
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;


                    file.SaveAs(Path.Combine(Server.MapPath(ImagePath), fileName));
                    objres = new { Msg = "file upload successfully!", Path = ImagePath + "" + fileName, result = "success" };
                }
                else
                {
                    objres = new { Msg = "Only jpg,jpeg,png,bmp,pdf,docx,xlsx files are allow !", result = "warning" };
                }
            }
            else
            {
                objres = new { Msg = "Please select file to upload!", result = "warning" };
            }
            return Json(objres, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MemberAssociateByAdmin(long FK_MemId, bool Status)
        {
            MembersResponse obj = new MembersResponse();
            if (FK_MemId > 0)
            {


                obj = MembersService.MemberAssociateByAdmin(FK_MemId, Status);
                return Redirect("/Members");
            }
            else
            {
                obj.Response = "Please select Member";
            }
            return Redirect("/Members");
        }


        public JsonResult BlockReferral(long FK_MemId, bool Status, string Remark)
        {
            MembersResponse obj = new MembersResponse();
            if (FK_MemId > 0)
            {
                obj = MembersService.BlockRefereral(FK_MemId, Status, Remark);
            }
            return Json(0);
        }





        [HttpPost]
        public JsonResult CustomerPrimaryDetails(MCustomerRegistration Obj)
        {
            try
            {
                Obj.NormalPassword = Obj.Password;
                CustomerRegService _objService = new CustomerRegService();
                Obj.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Obj.Password, "md5");
                var res = CustomerRegService.InvokeUpdateCustomerPrimaryDetails(Obj);
                if (res.Code == 1)
                {
                    //string message = LifeOne.Models.API.SMS.PlotHolderRegistrationSMS(Obj.Name, Obj.MobileNo, Obj.NormalPassword);
                    //LifeOne.Models.API.SMS.SendSMS(Obj.MobileNo, message);   //open it on the development server           
                }
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost]
        public JsonResult CustomerAddressDetails(MCustomerRegistration Obj)
        {
            CustomerRegService _objService = new CustomerRegService();
            var res = CustomerRegService.InvokeCustomerAddressDetails(Obj);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CustomerFamilyDetails(string SpouseName, string SpouseAadharNo, string SpousePanNo, string SpouseAadharImages, string Fk_MemId, List<ChildDetails> data)
        {
            try
            {
                CustomerRegService _objService = new CustomerRegService();
                MCustomerRegistration Obj = new MCustomerRegistration();
                Obj.SpouseName = SpouseName;
                Obj.SpouseAadharNo = SpouseAadharNo;
                Obj.SpousePanNo = SpousePanNo;
                Obj.SpouseAadharImages = SpouseAadharImages;
                Obj.Fk_MemId = Convert.ToInt32(Fk_MemId);
                var xmlFamily = "<Result>";
                for (var i = 0; i < data.Count; i++)
                {
                    xmlFamily += "<data><ChildName>" + data[i].ChildName + "</ChildName><Relation>" + data[i].Relation + "</Relation><ChildAadhar>" + data[i].ChildAadhar + "</ChildAadhar><ChildAadharImage>" + data[i].ChildAadharImage + "</ChildAadharImage><ChildPanNo>" + data[i].ChildPanNo + "</ChildPanNo></data>";
                }
                xmlFamily += "</Result>";
                var res = CustomerRegService.InvokeCustomerFamilyDetails(Obj, xmlFamily);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult CustomerFarmDetails(MCustomerRegistration Obj)
        {
            try
            {

                CustomerRegService _objService = new CustomerRegService();
                var res = CustomerRegService.InvokeCustomerFarmDetails(Obj);
                return Json(res, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public JsonResult UtilityPaymentList()
        {
            try
            {
                AdminDashboardService objservice = new AdminDashboardService();
                var res = objservice.AdminDashboard();
                return Json(res, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult NotifyMembers(int? Page, MMemberList _model)
        {

            if (!PermissionManager.IsActionPermit("Notify Direct Sellers", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            MembersResponse _objres = new MembersResponse();
            if (!String.IsNullOrEmpty(_model.LoginId) || !String.IsNullOrEmpty(_model.MemberName) || !String.IsNullOrEmpty(_model.Mobile))
            {
                Page = 1;
            }


            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.Page = Page;

            _objres.List = MembersService.InvokeMemberDetails(_model);
            _objres.Page = Page;
            if (_model.Searchby != null)
            {
                _objres.Searchby = _model.Searchby;
            }
            if (_model.SearchParam != null)
            {
                _objres.SearchParam = _model.SearchParam;
            }
            int totalRecords = 0;
            if (_objres.List.Count > 0)
            {

                totalRecords = Convert.ToInt32(_objres.List[0].ToatlRecord);
                var pager = new Pager(totalRecords, _objres.Page, SessionManager.Size);
                _objres.Pager = pager;
            }
            else
            {
                _objres.List = new List<MMemberList>();
            }
            return View(_objres);
        }

        [HttpPost]
        public JsonResult SaveNotification(MNotification modelRequest)
        {

            MSendNotification _result = new MSendNotification();
            SendNotificationResponse res = new SendNotificationResponse();
            string abc = "";
            try
            {
                if (modelRequest != null && modelRequest.LstMemberId.Count > 0)
                {
                    for (var i = 0; i < modelRequest.LstMemberId.Count; i++)
                    {
                        string AndroidId = modelRequest.LstMemberId[i];
                        if (AndroidId != null && AndroidId != " ")
                            abc = SendNotification.SendNotificationForMobile(AndroidId, modelRequest.Title, modelRequest.Notificationmessage);
                        else
                            abc = "";

                        res.Flag = 1;
                        res.Msg = "Notification Send Successfully.";
                    }
                }
            }
            catch (Exception)
            {
                res.Msg = "Some error occured, Please contact admin.";
                throw;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MemberWalletDetail(int? Page, MTransactionLog _model)
        {
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            WalletDetailModel model = new WalletDetailModel();
            _model.Page = Page;
            _model.Size = SessionManager.Size;
            model.WalletDetailMList = AllTransactionService.WalletDetail(_model);
            int totalRecords = 0;
            if (model.WalletDetailMList.Count > 0)
            {

                totalRecords = Convert.ToInt32(model.WalletDetailMList[0].TotalRecord);
                var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                model.Pager = pager;
            }
            else
            {
                model.WalletDetailMList = new List<WalletDetailModel>();
            }
            model.Fk_MemId = _model.Fk_MemId;
            return View(model);
        }
        public ActionResult MemberCashBack(int? Page, MTransactionLog _model)
        {
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            WalletDetailModel model = new WalletDetailModel();
            _model.Page = Page;
            _model.Size = SessionManager.Size;
            _model.ProcId = 1;
            model.WalletDetailMList = AllTransactionService.CashBackWalletDetail(_model);
            int totalRecords = 0;
            if (model.WalletDetailMList.Count > 0)
            {

                totalRecords = Convert.ToInt32(model.WalletDetailMList[0].TotalRecord);
                var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                model.Pager = pager;
            }
            else
            {
                model.WalletDetailMList = new List<WalletDetailModel>();
            }
            model.Fk_MemId = _model.Fk_MemId;
            return View(model);
        }
        public ActionResult MemberCommision(int? Page, MTransactionLog _model)
        {
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            WalletDetailModel model = new WalletDetailModel();
            _model.Page = Page;
            _model.Size = SessionManager.Size;
            _model.ProcId = 1;
            model.WalletDetailMList = AllTransactionService.CommissionDetailList(_model);
            int totalRecords = 0;
            if (model.WalletDetailMList.Count > 0)
            {

                totalRecords = Convert.ToInt32(model.WalletDetailMList[0].TotalRecord);
                var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                model.Pager = pager;
            }
            else
            {
                model.WalletDetailMList = new List<WalletDetailModel>();
            }
            model.Fk_MemId = _model.Fk_MemId;
            return View(model);
        }
        public ActionResult RecentMembers(int? Page, MMemberList _model)
        {
            MembersResponse _objres = new MembersResponse();
            if (_model.Searchby == "LoginId")
            {
                _model.LoginId = _model.SearchParam;
            }
            if (_model.Searchby == "Mobile")
            {
                _model.Mobile = _model.SearchParam;
            }
            if (_model.Searchby == "Name")
            {
                _model.MemberName = _model.SearchParam;
            }
            if (_model.Searchby == "InvitedBy")
            {
                _model.InvitedBy = _model.SearchParam;
            }
            if (_model.Searchby == "InviteCode")
            {
                _model.InviteCode = _model.SearchParam;
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.Page = Page;

            _objres.List = MembersService.GetRecentMemberList(_model);
            _objres.Page = Page;
            if (_model.Searchby != null)
            {
                _objres.Searchby = _model.Searchby;
            }
            if (_model.SearchParam != null)
            {
                _objres.SearchParam = _model.SearchParam;
            }
            int totalRecords = 0;
            if (_objres.List.Count > 0)
            {

                totalRecords = Convert.ToInt32(_objres.List[0].ToatlRecord);
                var pager = new Pager(totalRecords, _objres.Page, SessionManager.Size);
                _objres.Pager = pager;
            }
            else
            {
                _objres.List = new List<MMemberList>();
            }
            return View(_objres);
        }
        public ActionResult ExportToExcelRecentMembers(int? Page)
        {
            BlockUnblockService db = new BlockUnblockService();

            MembersResponse _objres = new MembersResponse();
            MMemberList _model = new MMemberList();
            if (_model.Searchby == "LoginId")
            {
                _model.LoginId = _model.SearchParam;
            }
            if (_model.Searchby == "Mobile")
            {
                _model.Mobile = _model.SearchParam;
            }
            if (_model.Searchby == "Name")
            {
                _model.MemberName = _model.SearchParam;
            }
            if (_model.Searchby == "InvitedBy")
            {
                _model.InvitedBy = _model.SearchParam;
            }
            if (_model.Searchby == "InviteCode")
            {
                _model.InviteCode = _model.SearchParam;
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.Page = Page;

            var customers = MembersService.GetRecentMemberList(_model);
            DataTable dt = new DataTable("Today New Joining");
            dt.Columns.AddRange(new DataColumn[9] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Sponsor's ID/ Sponsor's Name"),
                                                     new DataColumn("E-mail"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Join on"),
                                                     new DataColumn("Place Under ID"),
                                                     new DataColumn("Place Under Name"),
                                                     new DataColumn("DS Status")


            });

            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.MemberName, customer.SponsorMobile + "\n" + customer.SponsorName, customer.Email, customer.state, customer.joiningDate, customer.InviteCode, customer.InvitedBy, (customer.IsBusinessId == true ? "BID" : "UID"));

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Today New Joining.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/Admin-Members");
            }
            // return View();
        }
        public ActionResult RecentTransactionLog(int? Page, MTransactionLog _model)
        {
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.Page = Page;
            _model.Size = SessionManager.Size;
            _model.ProcId = 0;
            _model.LstTransactionLog = AllTransactionService.AllRecentTransactionLog(_model);
            _model.ObjList = new List<SelectListItem>();
            _model.ObjList.Add(new SelectListItem { Text = "Select Utility", Value = "" });
            _model.ObjList.AddRange(LifeOne.Models.Common.DBHelperDapper.DAGetDetailsInList<SelectListItem>("exec Proc_BindDropDownForAllTrans"));
            ViewBag.ddlUtilityTypes = _model.ObjList;

            int totalRecords = 0;
            if (_model.LstTransactionLog.Count > 0)
            {

                totalRecords = Convert.ToInt32(_model.LstTransactionLog[0].TotalRecord);
                var pager = new Pager(totalRecords, _model.Page, SessionManager.Size);
                _model.Pager = pager;
            }
            else
            {
                _model.LstTransactionLog = new List<MTransactionLog>();
            }
            return View(_model);
        }
        public ActionResult ExportToExcelRecentTransactionLog(int? Page)
        {
            MTransactionLog _model = new MTransactionLog();
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.Page = Page;
            _model.Size = SessionManager.Size;
            _model.ProcId = 0;
            var customers = AllTransactionService.AllRecentTransactionLog(_model);
            DataTable dt = new DataTable("TransactionLogDetails");
            dt.Columns.AddRange(new DataColumn[9] {
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Number"),
                                                     new DataColumn("Transaction Id/Action Type"),
                                                     new DataColumn("Session/Auth Code"),
                                                     new DataColumn("Payment Date"),
                                                     new DataColumn("Amount"),
                                                     new DataColumn("Status")


            });

            if (customers.Count > 0)
            {
                customers.ForEach(customer =>
                {
                    dt.Rows.Add(customer.LoginId, customer.DisplayName, customer.Mobile, customer.Number, "INR" + customer.TransactionId + "\n" + customer.ActionType, customer.Session + "\n" + customer.SessionAuthCode, customer.PaymentDate, customer.Amount, customer.Status);

                });
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllTransactionLog.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/Admin-TransactionLog");
            }
            // return View();
        }


        public ActionResult TestiMonials(int? Page, MemberTestimonialModel _model)
        {
            if (!PermissionManager.IsActionPermit("Approve Testimonial", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MemberTestimonialModel model = new MemberTestimonialModel();
            model.MemberTestimonialList = MembersService.GetTestimonialDetail(_model);
            if (model.MemberTestimonialList.Count > 0)
            {
                model.Pager = new Pager(model.MemberTestimonialList[0].TotalRecords, _model.Page, SessionManager.Size);
                //model.Pager = new Pager(model.MemberTestimonialList[0].TotalRecords, _model.Page, 1);                
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult SaveTestimonialStatus(MemberTestimonialModel _model)
        {
            AddUpdateDeleteModel model = MembersService.SaveTestimonialStatus(_model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExportToExcelTestimonials()
        {
            MemberTestimonialModel model = new MemberTestimonialModel();
            var customers = MembersService.GetTestimonialDetail(model, 1);
            DataTable dt = new DataTable("MemberkDetails");
            dt.Columns.AddRange(new DataColumn[7] {
                                                     new DataColumn("Name"),
                                                     new DataColumn("LoginId"),
                                                     new DataColumn("CropCategory"),
                                                     new DataColumn("Crop"),
                                                     new DataColumn("Description"),
                                                     new DataColumn("File Path"),
                                                     new DataColumn("Status")
            });

            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.Name, customer.LoginId, customer.CropCategory, customer.Crop, customer.Description, customer.FilePath, customer.IsDecline == true ? "Declines" : "Approved");

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MemberkDetails.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/Members");
            }
            // return View();
        }
        public ActionResult UpdateMemberTestimonialStatus(long FK_MemId, bool Status)
        {
            MembersResponse obj = new MembersResponse();
            if (FK_MemId > 0)
            {
                obj = MembersService.UpdateMemberTestimonialStatus(FK_MemId, Status);
                TempData["code"] = "1";
                TempData["msg"] = "Updated Successfully";
                return Redirect("/Admin-TestimonialPermission");
            }
            else
            {
                obj.Response = "Please select Member";
            }
            return Redirect("/Admin-TestimonialPermission");
        }




        public ActionResult EmployeeRegistration()
        {


            return View();

        }

        public JsonResult GetEmployeeDetailBYID(Nullable<long> FK_MemId)
        {
            EmployeesViewModel _model = new EmployeesViewModel();
            if (FK_MemId != null && FK_MemId > 0)
            {
                _model.FK_MemId = FK_MemId;
                _model.EmployeeList = DALMembers.Employees(_model);
                return Json(_model.EmployeeList[0], JsonRequestBehavior.AllowGet);
            }
            else
                return Json(_model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EmpReg(EmployeesViewModel req)
        {
            try
            {
                return Json(DALMembers.EmplReg(req));
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Send Notification

        //[HttpPost]
        //public JsonResult SaveNotificationForApp(MNotification modelRequest)
        //{

        //    NotificationResponse objres = new NotificationResponse();
        //    NotificationService _objService = new NotificationService();
        //    //modelRequest.Title = Title;
        //    //modelRequest.Notificationmessage = Notificationmessage;
        //    modelRequest.CreatedBy = SessionManager.AssociateFk_MemId;
        //    //modelRequest.lstMemberId = MemberId;
        //    var xmldata = "<Notification>";
        //    for (var i = 0; i < modelRequest.LstMemberId.Count(); i++)
        //    {
        //        xmldata += "<Notificationdata><Tittle>" + modelRequest.Title + "</Tittle><Message>" + modelRequest.Notificationmessage + "</Message><NotificationType>" + "Notification" + "</NotificationType><Fk_MemId>" + modelRequest.LstMemberId[i] + "</Fk_MemId><Fk_RequestId>" + modelRequest.CreatedBy + "</Fk_RequestId><CreatedBy>" + modelRequest.CreatedBy + "</CreatedBy></Notificationdata>";
        //        string abc = SendNotification.SendNotificationForMobile(modelRequest.LstDeviceId[i], modelRequest.Title, modelRequest.Notificationmessage);
        //    }
        //    xmldata += "</Notification>";
        //    try
        //    {
        //        var res = _objService.SaveNotification(xmldata);
        //        if (res != null && res.Flag == 1)
        //        {
        //            objres.response = "success";
        //            objres.Flag = res.Flag;
        //            objres.Msg = res.Msg;
        //        }
        //        else
        //        {
        //            objres.response = "error";
        //            objres.Flag = res.Flag;
        //            objres.Msg = res.Msg;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objres.response = "error";
        //        objres.Msg = ex.Message;
        //    }
        //    return Json(objres, JsonRequestBehavior.AllowGet);

        //}

        [HttpGet]
        public ActionResult FranchiseViewProfile(MFranchiseViewProfile model)
        {
            DALFranchiseViewProfile obj = new DALFranchiseViewProfile();
            MFranchiseViewProfile objResult = obj.AdminViewFranchiseDetail(model);
            return View(objResult);
        }
        public ActionResult TestimonialPermission(TestimonialPermissionViewModel _model)
        {
            if (!PermissionManager.IsActionPermit("Testimonial Permission", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            TestimonialPermissionViewModel _objres = new TestimonialPermissionViewModel();
            //if (!String.IsNullOrEmpty(_model.LoginId) || !String.IsNullOrEmpty(_model.MemberName) || !String.IsNullOrEmpty(_model.Mobile) || !String.IsNullOrEmpty(_model.InvitedBy) || !String.IsNullOrEmpty(_model.City) || !String.IsNullOrEmpty(_model.FromDate) || !String.IsNullOrEmpty(_model.ToDate))
            //    _model.Page = 1;

            _model.List = MembersService.TestimonialPermissionLeaders(_model);
            if (_model.List.Count > 0)
                _model.Pager = new Pager(Convert.ToInt32(_model.List[0].ToatlRecord), _objres.Page, SessionManager.Size);
            else
                _model.List = new List<TestimonialPermissionViewModel>();

            return View(_model);
        }
        public ActionResult ExportToExcelTestimonialPermission()
        {
            TestimonialPermissionViewModel _objres = new TestimonialPermissionViewModel();
            _objres.List = MembersService.TestimonialPermissionLeaders(_objres);
            DataTable dt = new DataTable("TestimonialLeaderPermission");
            dt.Columns.AddRange(new DataColumn[7] {
                                                     new DataColumn("LoginId"),
                                                     new DataColumn("Name"),
                                                     new DataColumn("SponserID/Name"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("State"),
                                                     new DataColumn("Join on"),
                                                     new DataColumn("Member Status")


            });

            if (_objres.List.Count > 0)
            {
                foreach (var customer in _objres.List)
                {
                    dt.Rows.Add(customer.LoginId, customer.MemberName, customer.SponsorMobile + "\n" + customer.SponsorName, customer.Email, customer.state, customer.joiningDate, (customer.IsAllowTestimonial == true ? "Allow" : "Disallow"));

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TestimonialLeaderPermission.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/Admin-TestimonialPermission");
            }
        }

        public ActionResult AllowFranchiseRequest(FranchiseReqListResponse req)
        {
            req.FranchiseReqLists = DALCustomerRegistration.AllowFranchiseRegistrationList(req);
            var stateList = new List<SelectListItem>();
            stateList.Add(new SelectListItem { Text = "Select State", Value = "" });
            stateList.AddRange(LifeOne.Models.Common.DBHelperDapper.DAGetDetailsInList<SelectListItem>("exec Proc_BindStateListDropDown"));
            ViewBag.ddlStateList = stateList;
            return View(req);
        }

        public JsonResult GetDistrictList(int StateID)
        {
            CustomerDb db = new CustomerDb();
            DistrictMasterListModel _model = new DistrictMasterListModel();
            _model.StateId = StateID;
            return Json(db.DistrictMasterList(_model), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AllowFranchiseRequestDetail(FranchiseReqListResponse req)
        {
            if (req.KeyId > 0)
                req.ProcID = 4;
            else
                req.ProcID = 1;
            var data = DALCustomerRegistration.AllowFranchiseRegistrationrequest(req);
            TempData["code"] = data.Flag;
            TempData["msg"] = data.Msg;
            return Redirect("/Admin-AllowFranchiseRequest");
        }

        public JsonResult AllowFranchiseRegistrationDetailById(FranchiseReqListResponse req)
        {
            return Json(DALCustomerRegistration.AllowFranchiseRegistrationDetailById(req), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteFranchiseRequests(FranchiseReqListResponse req)
        {
            req.ProcID = 5;
            var data = DALCustomerRegistration.AllowFranchiseRegistrationrequest(req);
            TempData["code"] = data.Flag;
            TempData["msg"] = data.Msg;
            return Redirect("/Admin-AllowFranchiseRequest");
        }

        public ActionResult Employees(EmployeesViewModel _model)
        {
            if (!PermissionManager.IsActionPermit("Employees", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            _model.EmployeeList = DALMembers.Employees(_model);
            if (_model.EmployeeList.Count > 0)
                _model.Pager = new Pager(_model.EmployeeList[0].TotalRecord, _model.Page, SessionManager.Size);
            else
                _model.EmployeeList = new List<EmployeesViewModel>();
            return View(_model);
        }
        public ActionResult DeleteEmployee(Nullable<long> FK_MemId)
        {
            if (!PermissionManager.IsActionPermit("Employees", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            EmployeesViewModel _model = new EmployeesViewModel();
            _model.FK_MemId = FK_MemId;
            var res = DALMembers.DeleteEmployee(_model);
            TempData["msg"] = res.Message;
            TempData["code"] = res.Flag;
            return Redirect("/Admin-Employees");
        }
        //: Admin/Admin/BindVendorsInfromation
        public ActionResult BindVendorsInfromation()
        {
            MVendorViewModel vendorlist = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shop.LifeOne.com/api/");
                //HTTP GET
                var responseTask = client.GetAsync("vendor_list.php");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MVendorViewModel>();
                    readTask.Wait();
                    vendorlist = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    // vendorlist = Enumerable.Empty<MVendorViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View();
        }


        public JsonResult StatementDetails(string FK_MemId,string PayoutNo)
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



        public ActionResult GetFranchiseEwalletRequest(FranchiseEWalletRequest franchiseEWalletRequest)
        {
            if (!PermissionManager.IsActionPermit("E-Wallet Request", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            DataSet dataSet = franchiseEWalletRequest.GetEwalletRequest();
            franchiseEWalletRequest.dtRequestDetails = dataSet.Tables[0];
            return View(franchiseEWalletRequest);
        }
        public ActionResult ApproveFranchiseEwallet(string id, string Status)
        {
            FranchiseEWalletRequest franchiseEWalletRequest = new FranchiseEWalletRequest();
            franchiseEWalletRequest.AddedBy = SessionManager.Fk_MemId;
            franchiseEWalletRequest.Pk_RequestId = id;
            franchiseEWalletRequest.Status = Status;
            DataSet dataSet = franchiseEWalletRequest.ApproveDeclineFranchiseEwalletRequest();
            return RedirectToAction("GetFranchiseEwalletRequest");
        }
        [HttpGet]
        public ActionResult GetActiveMembersFranchise(int? Page)
        {
            if (!PermissionManager.IsActionPermit("Activated Direct Seller", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            List<FranchiseEWalletledger> objList = new List<FranchiseEWalletledger>();
            FranchiseEWalletledger franchiseEWalletRequest = new FranchiseEWalletledger();
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            franchiseEWalletRequest.Page = Convert.ToInt32(Page);
            franchiseEWalletRequest.Size = SessionManager.Size;
            ViewBag.ddlPackageList = Models.AssociateManagement.AssociateEntity.BindPckageMaster.GetPackageMaster();
            //franchiseEWalletRequest.FranchiseId = SessionManager.FranchiseFk_MemId;
            DataSet dataSet = franchiseEWalletRequest.GetActiveMembersFranchise(franchiseEWalletRequest.LoginId, franchiseEWalletRequest.FromDate,
                franchiseEWalletRequest.ToDate, franchiseEWalletRequest.FranchiseId, franchiseEWalletRequest.FranchiseLoginID, franchiseEWalletRequest.MobileNo,
                franchiseEWalletRequest.FK_PackageId, franchiseEWalletRequest.Page, franchiseEWalletRequest.Size, franchiseEWalletRequest.PaymentMode, franchiseEWalletRequest.IsExport, franchiseEWalletRequest.InvoiceNo);
            franchiseEWalletRequest.dtRequestDetails = dataSet.Tables[0];
            var empList = dataSet.Tables[0].AsEnumerable()
             .Select(dataRow => new FranchiseEWalletledger
             {
                 LoginId = dataRow.Field<string>("LoginId"),
                 DisplayName = dataRow.Field<string>("DisplayName"),
                 Amount = dataRow.Field<decimal>("Amount"),
                 BV = dataRow.Field<decimal>("BV"),
                 ActiveDate = dataRow.Field<string>("ActiveDate"),
                 FranchiseCode = dataRow.Field<string>("FranchiseCode"),
                 FranchiseName = dataRow.Field<string>("FranchiseName"),
                 PaymentMode = dataRow.Field<string>("PaymentMode"),
                 MobileNo = dataRow.Field<string>("MobileNo"),
                 PackageName = dataRow.Field<string>("PackageName"),
                 InvoiceNo = dataRow.Field<string>("InvoiceNo")
             }).ToList();
            Session["ActiveMembers"] = empList;
            int totalRecords = 0;
            if (empList.Count > 0)
            {
                totalRecords = Convert.ToInt32(dataSet.Tables[0].Rows[0]["TotalCount"].ToString());
                var pager = new Pager(totalRecords, franchiseEWalletRequest.Page, SessionManager.Size);
                franchiseEWalletRequest.Pager = pager;
            }
            else
            {
                empList = new List<FranchiseEWalletledger>();
            }


            ViewBag.TotalCount = totalRecords;
            ViewBag.LoginId = franchiseEWalletRequest.LoginId;
            ViewBag.Fromdate = franchiseEWalletRequest.FromDate;
            ViewBag.Todate = franchiseEWalletRequest.ToDate;
            ViewBag.MobileNo = franchiseEWalletRequest.MobileNo;
            //ViewBag.ddlPackageList = franchiseEWalletRequest.FK_PackageId;
            ViewBag.PaymentMode = franchiseEWalletRequest.PaymentMode;
            return View(franchiseEWalletRequest);
        }
        [HttpPost]
        public ActionResult GetActiveMembersFranchise(FranchiseEWalletledger obj, string LoginID, string Fromdate, string Todate, string FranchiseLoginID, string MobileNo, int FK_PackageId, string PaymentMode, string InvoiceNo, int IsExport, int Page)
        {
            ViewBag.ddlPackageList = Models.AssociateManagement.AssociateEntity.BindPckageMaster.GetPackageMaster();

            obj.FK_PackageId = FK_PackageId;
            obj.LoginId = LoginID;
            obj.FromDate = Fromdate;
            obj.ToDate = Todate;
            obj.FranchiseLoginID = FranchiseLoginID;
            obj.MobileNo = MobileNo;
            obj.InvoiceNo = InvoiceNo;
            obj.Page = Page;
            obj.IsExport = IsExport;
            if (obj.Page == null || obj.Page == 0)
            {
                obj.Page = 1;
            }
            obj.Size = SessionManager.Size;
            obj.PaymentMode = PaymentMode;
            //franchiseEWalletRequest.FranchiseId = SessionManager.FranchiseFk_MemId;
            DataSet dataSet = obj.GetActiveMembersFranchise(obj.LoginId, obj.FromDate, obj.ToDate, obj.FranchiseId, obj.FranchiseLoginID, obj.MobileNo, obj.FK_PackageId, obj.Page, obj.Size, obj.PaymentMode, obj.IsExport, obj.InvoiceNo);
            obj.dtRequestDetails = dataSet.Tables[0];
            var empList = dataSet.Tables[0].AsEnumerable()
            .Select(dataRow => new FranchiseEWalletledger
            {
                LoginId = dataRow.Field<string>("LoginId"),
                DisplayName = dataRow.Field<string>("DisplayName"),
                Amount = dataRow.Field<decimal>("Amount"),
                BV = dataRow.Field<decimal>("BV"),
                ActiveDate = dataRow.Field<string>("ActiveDate"),
                FranchiseCode = dataRow.Field<string>("FranchiseCode"),
                FranchiseName = dataRow.Field<string>("FranchiseName"),
                PaymentMode = dataRow.Field<string>("PaymentMode"),
                MobileNo = dataRow.Field<string>("MobileNo"),
                PackageName = dataRow.Field<string>("PackageName"),
                InvoiceNo = dataRow.Field<string>("InvoiceNo"),
                DDChequeNo = dataRow.Field<string>("DDChequeNo"),
            }).ToList();
            Session["ActiveMembers"] = empList;
            if (obj.IsExport == 1)
            {
                DataTable dt = new DataTable("Activated Direct Seller");// dataSet.Tables[0];


                dt.Columns.AddRange(new DataColumn[12] {
                                            new DataColumn("Franchisee Code"),
                                             new DataColumn("Franchisee Name"),
                                            new DataColumn("Package"),
                                            new DataColumn("Login Id"),
                                            new DataColumn("Invoice No"),
                                            new DataColumn("Direct Seller"),
                                            new DataColumn("Conatct"),
                                            new DataColumn("Amount"),
                                            new DataColumn("BV"),
                                            new DataColumn("Payment Mode"),
                                            new DataColumn("Transaction No"),
                                            new DataColumn("Active Date")

            });
                if (empList.Count > 0)
                {
                    foreach (var customer in empList)
                    {
                        dt.Rows.Add(customer.FranchiseCode, customer.FranchiseName, customer.PackageName,
                            customer.LoginId, customer.InvoiceNo, customer.DisplayName, customer.MobileNo, customer.Amount, customer.BV, customer.PaymentMode, customer.DDChequeNo, customer.ActiveDate

                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Franchisee Activation Details.xlsx");
                        }
                    }
                }
            }
            int totalRecords = 0;
            if (empList.Count > 0)
            {
                totalRecords = Convert.ToInt32(dataSet.Tables[0].Rows[0]["TotalCount"].ToString());
                var pager = new Pager(totalRecords, obj.Page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                empList = new List<FranchiseEWalletledger>();
            }
            ViewBag.TotalCount = totalRecords;
            ViewBag.TotalCount = totalRecords;
            ViewBag.LoginId = obj.LoginId;
            ViewBag.Fromdate = obj.FromDate;
            ViewBag.Todate = obj.ToDate;
            ViewBag.MobileNo = obj.MobileNo;
            //ViewBag.ddlPackageList = franchiseEWalletRequest.FK_PackageId;
            ViewBag.PaymentMode = obj.PaymentMode;
            ViewBag.InvoiceNo = obj.InvoiceNo;
            return View(obj);
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
                    orderConfirmation.custGSTNo = dsOrder.Tables[0].Rows[i]["CustGSTNo"].ToString();
                    orderConfirmation.HSNNo = dsOrder.Tables[1].Rows[i]["HSNNo"].ToString();


                    orderConfirmation.CompanyLogo = dsOrder.Tables[0].Rows[i]["CompanyLogo"].ToString();
                    orderConfirmation.City = dsOrder.Tables[0].Rows[i]["City"].ToString();
                    orderConfirmation.State = dsOrder.Tables[0].Rows[i]["State"].ToString();
                    orderConfirmation.Pincode = dsOrder.Tables[0].Rows[i]["Pincode"].ToString();

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
        //public ActionResult ExportToExcelActiveMembers(int? Page)
        //{

        //    FranchiseEWalletledger _model = new FranchiseEWalletledger();
        //    DataSet dataSet = _model.GetActiveMembersFranchise(_model.LoginId, _model.FromDate, _model.ToDate, _model.FranchiseId, _model.FranchiseLoginID, _model.MobileNo, _model.FK_PackageId, 1, 10000000, _model.PaymentMode);
        //    _model.dtRequestDetails = dataSet.Tables[0];
        //    var empList = dataSet.Tables[0].AsEnumerable()
        //    .Select(dataRow => new FranchiseEWalletledger
        //    {
        //        LoginId = dataRow.Field<string>("LoginId"),
        //        DisplayName = dataRow.Field<string>("DisplayName"),
        //        Amount = dataRow.Field<decimal>("Amount"),
        //        BV = dataRow.Field<decimal>("BV"),
        //        ActiveDate = dataRow.Field<string>("ActiveDate"),
        //        FranchiseCode = dataRow.Field<string>("FranchiseCode"),
        //        FranchiseName = dataRow.Field<string>("FranchiseName"),
        //        PaymentMode = dataRow.Field<string>("PaymentMode"),
        //        DDChequeNo = dataRow.Field<string>("DDChequeNo"),
        //        MobileNo = dataRow.Field<string>("MobileNo"),
        //        PackageName = dataRow.Field<string>("PackageName")
        //    }).ToList();
        //    Session["ActiveMembers"] = empList;
        //    //_model.Page = Convert.ToInt32(Page);
        //    //_model.Size = SessionManager.Size;
        //    //Session["ActiveMembers"] = dataSet.Tables[0];


        //    var customers = (List<FranchiseEWalletledger>)Session["ActiveMembers"];

        //    else
        //    {
        //        TempData["alert"] = "Data Not Found";
        //        return Redirect("GetActiveMembersFranchise");
        //    }
        //}

        //public ActionResult ViewFranchiseSupplyOrderStatus()
        //{
        //    MOrder obj = new MOrder();
        //    ViewBag.Franchise = DALBindCommonDropdown.BindFranchise();
        //    int? Page = 1;           
        //    obj = FranchiseOrderService.SupplyOrderStatusServiceForAdmin(Page, obj);

        //    return View(obj);
        //}
        //[HttpPost]
        public ActionResult FranchiseCustomerTaxInvoice(string OrderId)
        {
            OrderConfirmationModel orderConfirmation = new OrderConfirmationModel();
            List<ItemsModel> OrderItems = new List<ItemsModel>();
            OrderDAL orderDAL = new OrderDAL();
            DataSet dsOrder = orderDAL.GetCustomerInvoiceByOrderId(OrderId);
            if (dsOrder.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsOrder.Tables[0].Rows.Count; i++)
                {
                    orderConfirmation.BName = dsOrder.Tables[0].Rows[i]["BName"].ToString();
                    orderConfirmation.BGSTNo = dsOrder.Tables[0].Rows[i]["GSTNo"].ToString();
                    orderConfirmation.BAddress = dsOrder.Tables[0].Rows[i]["BAddress"].ToString();
                    orderConfirmation.BMobileNo = dsOrder.Tables[0].Rows[i]["BMobileNo"].ToString();
                    orderConfirmation.ProductName = dsOrder.Tables[1].Rows[i]["ProductName"].ToString();
                    orderConfirmation.HSNNo = dsOrder.Tables[1].Rows[i]["HSNNo"].ToString();
                    orderConfirmation.Total = dsOrder.Tables[0].Rows[i]["FinalAmt"].ToString();
                    orderConfirmation.TransportMode = dsOrder.Tables[0].Rows[i]["TransportMode"].ToString();
                    orderConfirmation.StateCode = dsOrder.Tables[0].Rows[i]["StateCode"].ToString();

                    orderConfirmation.OrderNo = dsOrder.Tables[0].Rows[i]["OrderNo"].ToString();
                    orderConfirmation.OrderDate = dsOrder.Tables[0].Rows[i]["OrderDate"].ToString();
                    orderConfirmation.InvoiceDate = dsOrder.Tables[1].Rows[i]["InvoiceDate"].ToString();
                    orderConfirmation.InvoiceNo = dsOrder.Tables[1].Rows[i]["InvoiceNo"].ToString();
                    orderConfirmation.CourierName = dsOrder.Tables[0].Rows[i]["CourierName"].ToString();

                    orderConfirmation.CompanyName = dsOrder.Tables[0].Rows[i]["CompanyName"].ToString();
                    orderConfirmation.OfficeAddress = dsOrder.Tables[0].Rows[i]["OfficeAddress"].ToString();
                    orderConfirmation.OfficeContactNo = dsOrder.Tables[0].Rows[i]["OfficeContactNo"].ToString();
                    orderConfirmation.GSTIN = dsOrder.Tables[0].Rows[i]["CompanyGST"].ToString();
                    orderConfirmation.Website = dsOrder.Tables[0].Rows[i]["Website"].ToString();
                    orderConfirmation.PAN = dsOrder.Tables[0].Rows[i]["Pancard"].ToString();
                    orderConfirmation.OfficeEmailId = dsOrder.Tables[0].Rows[i]["OfficeEmailId"].ToString();
                    orderConfirmation.LoginId = dsOrder.Tables[0].Rows[i]["LoginId"].ToString();
                    orderConfirmation.CompanyLogo = dsOrder.Tables[0].Rows[i]["CompanyLogo"].ToString();
                    ///orderConfirmation.StateCode = dsOrder.Tables[0].Rows[i]["StateCode"].ToString();

                    ItemsModel obj = new ItemsModel();

                    int qty = 0;
                    decimal TotalAmount = 0;

                    TotalAmount = Convert.ToDecimal(orderConfirmation.Total);

                    long finalamount = (long)Convert.ToDouble(TotalAmount);
                    orderConfirmation.AmountInWords = dsOrder.Tables[0].Rows[i]["AmountInWords"].ToString();

                    for (int j = 0; j < dsOrder.Tables[1].Rows.Count; j++)
                    {

                        OrderItems.Add(new ItemsModel
                        {
                            ProductUrl = "",
                            GSTNo = dsOrder.Tables[1].Rows[j]["GSTNo"].ToString(),
                            ProductName = dsOrder.Tables[1].Rows[j]["ProductName"].ToString(),
                            HSNNo = dsOrder.Tables[1].Rows[j]["HSNNo"].ToString(),
                            ProductAmt = dsOrder.Tables[1].Rows[j]["DealerPrice"].ToString(),
                            ProductQty = dsOrder.Tables[1].Rows[j]["ProductQty"].ToString(),
                            Discount = "0.00",
                            Tax = dsOrder.Tables[1].Rows[j]["DealerPrice"].ToString(),
                            CGST = dsOrder.Tables[1].Rows[j]["CGST"].ToString(),
                            SGST = dsOrder.Tables[1].Rows[j]["SGST"].ToString(),
                            IGST = dsOrder.Tables[1].Rows[j]["IGST"].ToString(),
                            CGSTAmount = dsOrder.Tables[1].Rows[j]["CGSTAmt"].ToString(),
                            SGSTAmount = dsOrder.Tables[1].Rows[j]["SGSTAmt"].ToString(),
                            IGSTAmount = dsOrder.Tables[1].Rows[j]["IGSTAmt"].ToString(),
                            //TotalProductAmt = dsOrder.Tables[1].Rows[j]["OrderAmount"].ToString(),
                            // BaseAmount = obj.BaseAmount,
                            TexableAmount = Convert.ToDecimal(dsOrder.Tables[1].Rows[j]["TaxableAmount"].ToString()),
                            TotalAmount = Convert.ToDecimal(orderConfirmation.Total),
                            SubTotal = Convert.ToDecimal(dsOrder.Tables[1].Rows[j]["SubTotal"].ToString()),
                            PointValue = Convert.ToDecimal(dsOrder.Tables[1].Rows[j]["PointValue"].ToString()),
                            AmountInWords = orderConfirmation.AmountInWords
                        });
                    }
                    qty += int.Parse(dsOrder.Tables[1].Rows[i]["ProductQty"].ToString());
                    int totalPointValue = Convert.ToInt32(dsOrder.Tables[1].Rows[i]["PV"].ToString());
                    if (totalPointValue == 0)
                    {
                        ViewBag.BVTotAmtInWord = "ZERO";
                    }
                    else
                    {
                        ViewBag.BVTotAmtInWord = DALCommon.ConvertNumbertoWords(totalPointValue);
                    }
                    orderConfirmation.TotalProductQty = qty.ToString();
                    orderConfirmation.OrderItems = OrderItems;
                    orderConfirmation.VendorName = OrderItems[0].VendorName;
                    ViewBag.PaymentMode = dsOrder.Tables[1].Rows[0]["PayMode"].ToString();
                    ViewBag.BankName = dsOrder.Tables[1].Rows[0]["BankName"].ToString();
                    ViewBag.TransactionNo = dsOrder.Tables[1].Rows[0]["TransactionNo"].ToString();
                    ViewBag.PaymentDate = dsOrder.Tables[1].Rows[0]["PaymentDate"].ToString();
                }
                return View(orderConfirmation);
            }
            return View(orderConfirmation);
        }
        public ActionResult ViewFranchiseSupplyOrderStatus(int? page, MOrder obj)
        {
            if (!PermissionManager.IsActionPermit("Customer Orders", ViewOptions.FormView))
            {
                return Redirect("/admin");
            }
            List<MOrder> ListFranchise = DALBindCommonDropdown.BindFranchise();

            if (obj.PKFranchiseRegistrationId != "0")
            {
                List<SelectListItem> Franlsts = (from c in ListFranchise
                                                 select new SelectListItem()
                                                 {
                                                     Selected = (c.PKFranchiseRegistrationId == obj.PKFranchiseRegistrationId),
                                                     Text = c.LoginID,
                                                     Value = c.PKFranchiseRegistrationId
                                                 }).ToList();
                ViewBag.Franchise = Franlsts;
                ViewBag.IsValue = obj.PKFranchiseRegistrationId;
            }
            else
            {
                List<SelectListItem> Franlsts = (from c in ListFranchise
                                                 select new SelectListItem()
                                                 {

                                                     Text = c.LoginID,
                                                     Value = c.PKFranchiseRegistrationId
                                                 }).ToList();
                ViewBag.Franchise = Franlsts;
                ViewBag.IsValue = 0;
            }
            ViewBag.PaymentMode = obj.PayMode;

            obj = FranchiseOrderService.SupplyOrderStatusServiceForAdmin(page, obj);
            obj.LstOrder = DALFranchise.GetAllSuppliedOrderList(obj);
            if (obj.IsExport == 1)
            {

                DataTable dt = new DataTable("Direct Seller Orders");
                dt.Columns.AddRange(new DataColumn[10] {
                                                     new DataColumn("Order No"),
                                                     new DataColumn("Direct Seller"),
                                                     new DataColumn("Login Id"),
                                                     new DataColumn("Date"),
                                                     new DataColumn("Payment Type"),
                                                     new DataColumn("Item Qty."),
                                                     new DataColumn("BV"),
                                                     new DataColumn("Order Amt.(₹)"),
                                                     new DataColumn("Discount Amt.(₹)"),
                                                     new DataColumn("Final Amt.(₹)")


            });
                if (obj.LstOrder.Count > 0)
                {
                    foreach (var customer in obj.LstOrder)
                    {
                        dt.Rows.Add(customer.OrderNo, customer.SupplyToName, customer.LoginID, customer.ApproveDate, customer.PaymentType, customer.ItemQty, customer.PointValue, customer.Amount, customer.TotalPV, customer.TotalAmt);

                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Direct Seller Orders.xlsx");
                        }
                    }
                }
            }
            return View(obj);
        }

        [HttpPost]
        public ActionResult DeleteCustomerOrder(string OrderNo)
        {

            MOrder _model = new MOrder();
            _model.OrderNo = OrderNo;
            var res = FranchiseOrderService.DeleteCustomerOrder(_model);
            TempData["msg"] = res.Message;
            TempData["code"] = res.Status;

            return Json(res, JsonRequestBehavior.AllowGet);
            // return Redirect("/CustomerOrder");
        }



        [HttpPost]
        public JsonResult RepurchaseStatementDetails(MPayoutReport obj)
        {

            StatementDetails modelRequest = new StatementDetails();
            StatementResponse Objres = new StatementResponse();
            AdminReportsService _objService = new AdminReportsService();

            try
            {


                var res = _objService.GetRepurchaseIncomeDetails(obj);
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

        public ActionResult GeneratePurchaseOrder()
        {
            if (!PermissionManager.IsActionPermit("Generate Purchase Order", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            ProductsDetail product = new ProductsDetail();
            DataSet dataSet = new DataSet();
            //#region Bind Product
            //List<SelectListItem> ddlProduct = new List<SelectListItem>();
            //dataSet = product.GetAllProducts();
            //if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            //{
            //    ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
            //    foreach (DataRow r in dataSet.Tables[0].Rows)
            //    {
            //        ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
            //    }
            //}
            //ViewBag.ddlProduct = ddlProduct;

            //#endregion
            #region BindSupplier
            List<SelectListItem> ddlSupplierDetails = new List<SelectListItem>();

            dataSet = product.GetSupplierDetails();

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                ddlSupplierDetails.Add(new SelectListItem { Text = "Select Vendor Name", Value = "0" });
                foreach (DataRow r in dataSet.Tables[0].Rows)
                {
                    ddlSupplierDetails.Add(new SelectListItem { Text = r["SupplierName"].ToString(), Value = r["Pk_SupplierId"].ToString() });
                }
            }
            ViewBag.ddlSupplierDetails = ddlSupplierDetails;
            #endregion

            //ViewBag.PaymentMode = product.PayMode;
            product.CreatedBy = SessionManager.Fk_MemId;
            dataSet = product.AddTempStock();
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                product.dtProductQuantity = dataSet.Tables[0];
                return View(product);
            }
            return View();

        }
        [HttpPost]
        public ActionResult GeneratePurchaseOrder(ProductsDetail products, string Add, string Save)
        {
            DataSet dataSet = new DataSet();

            if (!string.IsNullOrEmpty(Add))
            {
                products.CreatedBy = SessionManager.Fk_MemId;
                dataSet = products.AddTempStock();
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "0")
                    {
                        TempData["AddStockMessage"] = "Stock Already Added!!";
                    }
                    products.dtProductQuantity = dataSet.Tables[0];
                    products.Pk_ProductId = 0;
                }
            }
            if (!string.IsNullOrEmpty(Save))
            {
                products.CreatedBy = SessionManager.Fk_MemId;
                dataSet = products.AddAdminStock();
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["AddStockMessage"] = "Stock Added Successfully";
                    }
                    else
                    {
                        TempData["AddStockMessage"] = dataSet.Tables[0].Rows[0][1].ToString();
                    }
                }
                return RedirectToAction("GeneratePurchaseOrder");

            }
            #region Bind Product
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            dataSet = products.GetAllProducts();
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                foreach (DataRow r in dataSet.Tables[0].Rows)
                {
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                }
            }
            ViewBag.ddlProduct = ddlProduct;

            #endregion
            #region BindSupplier
            List<SelectListItem> ddlSupplierDetails = new List<SelectListItem>();

            dataSet = products.GetSupplierDetails();

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
            return View(products);

        }


        public ActionResult DeleteStock(string id)
        {
            ProductsDetail products = new ProductsDetail();
            products.PK_Id = id;
            DataSet dataSet1 = products.DeleteStock();
            return Redirect("~/Admin/admin/GeneratePurchaseOrder");
        }
        public ActionResult Add_Stock(int Pk_ProductId)
        {
            ProductsDetail products = new ProductsDetail();
            DataSet dataSet = products.GetProductGSTDetails(Pk_ProductId);
            products.IGST = dataSet.Tables[0].Rows[0]["IGST"].ToString();
            products.CGST = dataSet.Tables[0].Rows[0]["CGST"].ToString();
            products.SGST = dataSet.Tables[0].Rows[0]["SGST"].ToString();
            products.MRP = dataSet.Tables[0].Rows[0]["MRP"].ToString();
            return Json(products, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetSupplierDetails(string Pk_SupplierId)
        {
            ProductsDetail product = new ProductsDetail();
            product.OpCode = 4;

            product.Pk_SupplierId = Pk_SupplierId;
            //product.LstOrder = product.GetSupplierDetailsList();
            DataSet dataset = product.GetSupplierDetails();
            List<ProductsDetail> list = new List<ProductsDetail>();
            List<ProductsDetail> list1 = new List<ProductsDetail>();
            product.dtDetails = dataset.Tables[0];
            product.dtProductDetails = dataset.Tables[1];
            for (int j = 0; j <= dataset.Tables[0].Rows.Count - 1; j++)
            {
                ProductsDetail temproduct = new ProductsDetail();
                temproduct.Pk_SupplierId = dataset.Tables[0].Rows[j]["Pk_SupplierId"].ToString();
                temproduct.SupplierName = dataset.Tables[0].Rows[j]["SupplierName"].ToString();
                temproduct.Mobile = dataset.Tables[0].Rows[j]["Mobile"].ToString();
                temproduct.GSTNo = dataset.Tables[0].Rows[j]["GSTNo"].ToString();
                temproduct.Address = dataset.Tables[0].Rows[j]["Address"].ToString();
                temproduct.PinCode = dataset.Tables[0].Rows[j]["PinCode"].ToString();
                list.Add(temproduct);
            }
            for (int j = 0; j <= dataset.Tables[1].Rows.Count - 1; j++)
            {
                ProductsDetail temproduct = new ProductsDetail();
                temproduct.ProductName = dataset.Tables[1].Rows[j]["ProductName"].ToString();
                temproduct.Pk_ProductId = int.Parse(dataset.Tables[1].Rows[j]["Pk_ProductId"].ToString());
                list.Add(temproduct);
            }
            product.LstOrder = list;
            //product.Mobile = (dataset.Tables[0].Rows[0]["Mobile"].ToString());
            //product.Address = dataset.Tables[0].Rows[0]["Address"].ToString();
            //product.GSTNo = dataset.Tables[0].Rows[0]["GSTNo"].ToString();
            //product.ProductName = dataset.Tables[0].Rows[0]["ProductName"].ToString();
            return Json(product.LstOrder, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteActivatedMembersOrders(string Pk_TransId, string Fk_memId)
        {

            MOrder _model = new MOrder();
            _model.Fk_InvId = Pk_TransId;
            _model.Fk_MemId = Convert.ToInt64(Fk_memId);
            var res = FranchiseOrderService.CancelPackageSale(_model);
            TempData["msg"] = res.Message;
            TempData["code"] = res.Status;

            return Json(res, JsonRequestBehavior.AllowGet);
            // return Redirect("/CustomerOrder");
        }

        public ActionResult OpenOrderByAdmin()
        {
            try
            {
                if (!PermissionManager.IsActionPermit("Open Order", ViewOptions.FormView))
                {
                    return Redirect("/Home/adminlogin");
                }
                //ViewBag.ProductCategory = DALBindCommonDropdown.BindDropdown(2, 0);
                ViewBag.Product = DALBindCommonDropdown.BindDropdown(1, 0);
                //ViewBag.Product = DALBindCommonDropdown.BindFranchiseProductDropdown(1, 0, SessionManager.FranchiseFk_MemId);
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult AddProduct(MFranchiseorderRequest _data)
        {
            try
            {
                MSimpleString obj = FranchiseOrderService.AdminTempSaveOrder(_data);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetOrderTemp()
        {
            try
            {

                List<MFranchiseorderRequest> _objlst = DALFranchise.GetCustomerOrderTemp("2");
                return Json(_objlst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult SaveProduct(MFranchiseorderRequest _data)
        {
            try
            {
                MSimpleString result = FranchiseOrderService.SaveCustomerProductService(_data);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult DeleteProductTemp(MFranchiseorderRequest _data)
        {
            try
            {
                _data.Fk_Memid = SessionManager.Fk_MemId;
                _data.OpCode = 3;
                MSimpleString _objlst = DALFranchise.DeleteCustomerProductTemp(_data);
                return Json(_objlst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult OpenOrderList(Reports reports)
        {
            if (!PermissionManager.IsActionPermit("Open Order List", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (reports.Page == null || reports.Page == 0)
            {
                reports.Page = 1;
            }
            reports.Size = SessionManager.Size;
            reports.OpCode = 1;
            DataSet dataSet = reports.GetOpenOrderList();
            reports.dtDetails = dataSet.Tables[0];
            Session["OpenOrdersList"] = reports.dtDetails;
            if (reports.IsExport == 1)
            {
                DataTable dt = new DataTable("Open Order List");
                dt.Columns.AddRange(new DataColumn[9] {

                                             new DataColumn("Order No"),
                                             new DataColumn("Customer Name"),
                                            new DataColumn("Payment Type"),
                                            new DataColumn("Transaction No"),
                                            new DataColumn("Item Qty."),
                                            new DataColumn("BV"),
                                            new DataColumn("Order Amt.(₹)"),
                                            new DataColumn("Final Amt.(₹)"),
                                            new DataColumn("Ordered Dt."),


            });
                //List<OrderList> customers = Session["ProductStockReport"] as List<OrderList>;
                if (dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        dt.Rows.Add(row["OrderNo"].ToString(), row["Name"].ToString(), row["PaymentMode"].ToString(), row["TransactionNo"].ToString(), row["TotalQty"].ToString(), row["TotalBV"].ToString(), row["OrderAmount"].ToString(), row["OrderAmount"].ToString(), row["OrderDate"].ToString());
                    }
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Open Order List.xlsx");
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

            return View(reports);
        }
        public ActionResult DeleteOpenOrder(string id)
        {
            Reports reports = new Reports();
            reports.Pk_OrderId = id;
            reports.OpCode = 3;
            DataSet dataSet = reports.GetOpenOrderList();
            return Redirect("OpenOrderList");
        }
        public JsonResult ViewSupplyItemDetail(string OrderId)
        {
            List<MOrder> _obj = FranchiseOrderService.ViewOpenOrderDetails(long.Parse(OrderId));
            return Json(_obj, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult ExportToExcelOpenOrders()
        //{

        //    dt.Columns.Remove("ImageURL");

        //    return Redirect("OpenOrderList");
        //}
        public ActionResult OpenOrderTaxInvoice(string OrderId)
        {
            OrderConfirmationModel orderConfirmation = new OrderConfirmationModel();
            List<ItemsModel> OrderItems = new List<ItemsModel>();
            OrderDAL orderDAL = new OrderDAL();
            DataSet dsOrder = orderDAL.OpenOrderInvoiceByOrderId(OrderId);
            if (dsOrder.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsOrder.Tables[0].Rows.Count; i++)
                {
                    orderConfirmation.BName = dsOrder.Tables[0].Rows[i]["BName"].ToString();
                    orderConfirmation.BAddress = dsOrder.Tables[0].Rows[i]["BAddress"].ToString();
                    orderConfirmation.BMobileNo = dsOrder.Tables[0].Rows[i]["BMobileNo"].ToString();
                    orderConfirmation.ProductName = dsOrder.Tables[1].Rows[i]["ProductName"].ToString();
                    orderConfirmation.HSNNo = dsOrder.Tables[1].Rows[i]["HSNNo"].ToString();
                    orderConfirmation.Total = dsOrder.Tables[0].Rows[i]["FinalAmt"].ToString();
                    orderConfirmation.TransportMode = dsOrder.Tables[0].Rows[i]["TransportMode"].ToString();
                    orderConfirmation.BGSTNo = dsOrder.Tables[0].Rows[i]["GSTNo"].ToString();

                    orderConfirmation.OrderNo = dsOrder.Tables[0].Rows[i]["OrderNo"].ToString();
                    orderConfirmation.OrderDate = dsOrder.Tables[0].Rows[i]["OrderDate"].ToString();
                    orderConfirmation.InvoiceDate = dsOrder.Tables[1].Rows[i]["InvoiceDate"].ToString();
                    orderConfirmation.InvoiceNo = dsOrder.Tables[1].Rows[i]["InvoiceNo"].ToString();
                    orderConfirmation.CourierName = dsOrder.Tables[0].Rows[i]["CourierName"].ToString();

                    orderConfirmation.CompanyName = dsOrder.Tables[0].Rows[i]["CompanyName"].ToString();
                    orderConfirmation.OfficeAddress = dsOrder.Tables[0].Rows[i]["OfficeAddress"].ToString();
                    orderConfirmation.OfficeContactNo = dsOrder.Tables[0].Rows[i]["OfficeContactNo"].ToString();
                    orderConfirmation.GSTIN = dsOrder.Tables[0].Rows[i]["CompanyGST"].ToString();
                    orderConfirmation.Website = dsOrder.Tables[0].Rows[i]["Website"].ToString();
                    orderConfirmation.PAN = dsOrder.Tables[0].Rows[i]["Pancard"].ToString();
                    orderConfirmation.OfficeEmailId = dsOrder.Tables[0].Rows[i]["OfficeEmailId"].ToString();
                    orderConfirmation.CompanyLogo = dsOrder.Tables[0].Rows[i]["CompanyLogo"].ToString();

                    orderConfirmation.FreeAmount = dsOrder.Tables[0].Rows[i]["FreeAmount"].ToString();
                    ///orderConfirmation.StateCode = dsOrder.Tables[0].Rows[i]["StateCode"].ToString();

                    ItemsModel obj = new ItemsModel();

                    int qty = 0;
                    decimal TotalAmount = 0;

                    TotalAmount = Convert.ToDecimal(orderConfirmation.Total);

                    long finalamount = (long)Convert.ToDouble(TotalAmount);
                    orderConfirmation.AmountInWords = dsOrder.Tables[0].Rows[i]["AmountInWords"].ToString();

                    for (int j = 0; j < dsOrder.Tables[1].Rows.Count; j++)
                    {

                        OrderItems.Add(new ItemsModel
                        {
                            ProductUrl = "",
                            GSTNo = dsOrder.Tables[1].Rows[j]["GSTNo"].ToString(),
                            ProductName = dsOrder.Tables[1].Rows[j]["ProductName"].ToString(),
                            HSNNo = dsOrder.Tables[1].Rows[j]["HSNNo"].ToString(),
                            ProductAmt = dsOrder.Tables[1].Rows[j]["DealerPrice"].ToString(),
                            ProductQty = dsOrder.Tables[1].Rows[j]["ProductQty"].ToString(),
                            Discount = "0.00",
                            Tax = dsOrder.Tables[1].Rows[j]["DealerPrice"].ToString(),
                            CGST = dsOrder.Tables[1].Rows[j]["CGST"].ToString(),
                            SGST = dsOrder.Tables[1].Rows[j]["SGST"].ToString(),
                            IGST = dsOrder.Tables[1].Rows[j]["IGST"].ToString(),
                            CGSTAmount = dsOrder.Tables[1].Rows[j]["CGSTAmt"].ToString(),
                            SGSTAmount = dsOrder.Tables[1].Rows[j]["SGSTAmt"].ToString(),
                            IGSTAmount = dsOrder.Tables[1].Rows[j]["IGSTAmt"].ToString(),
                            //TotalProductAmt = dsOrder.Tables[1].Rows[j]["OrderAmount"].ToString(),
                            // BaseAmount = obj.BaseAmount,
                            TexableAmount = Convert.ToDecimal(dsOrder.Tables[1].Rows[j]["TaxableAmount"].ToString()),
                            TotalAmount = Convert.ToDecimal(orderConfirmation.Total),
                            SubTotal = Convert.ToDecimal(dsOrder.Tables[1].Rows[j]["SubTotal"].ToString()),
                            PointValue = Convert.ToDecimal(dsOrder.Tables[1].Rows[j]["PointValue"].ToString()),
                            AmountInWords = orderConfirmation.AmountInWords
                        });
                    }
                    qty += int.Parse(dsOrder.Tables[1].Rows[i]["ProductQty"].ToString());
                    int totalPointValue = Convert.ToInt32(dsOrder.Tables[1].Rows[i]["PV"].ToString());
                    if (totalPointValue == 0)
                    {
                        ViewBag.BVTotAmtInWord = "ZERO";
                    }
                    else
                    {
                        ViewBag.BVTotAmtInWord = DALCommon.ConvertNumbertoWords(totalPointValue);
                    }
                    orderConfirmation.TotalProductQty = qty.ToString();
                    orderConfirmation.OrderItems = OrderItems;
                    orderConfirmation.VendorName = OrderItems[0].VendorName;
                    ViewBag.PaymentMode = dsOrder.Tables[1].Rows[0]["PayMode"].ToString();
                    ViewBag.BankName = dsOrder.Tables[1].Rows[0]["BankName"].ToString();
                    ViewBag.TransactionNo = dsOrder.Tables[1].Rows[0]["TransactionNo"].ToString();
                    ViewBag.PaymentDate = dsOrder.Tables[1].Rows[0]["PaymentDate"].ToString();
                }
                return View(orderConfirmation);
            }
            return View(orderConfirmation);
        }


        public ActionResult AdminRagistration()
        {
            MCustomerRegistration _obj = new MCustomerRegistration();
            return View(_obj);
        }
        public JsonResult GetLocationByPinCodeAdmin(int? pin)
        {
            CustomerDb objcusdb = new CustomerDb();
            MPincode modelRequest = new MPincode();
            modelRequest.PinCode = pin.ToString();
            var res = objcusdb.GetPincodeDetais(modelRequest);
            var data1 = res.Distinct().GroupBy(y => new { y.DistrictName, y.StateName }).Select(y => new
            {
                State = y.FirstOrDefault().StateName,
                District = y.FirstOrDefault().DistrictName,
                DistrictDetail = y.ToList().Distinct().GroupBy(x => x.DistrictName).Select(x => new
                {
                    Taluka = x.FirstOrDefault().Taluka,
                    Circle = x.FirstOrDefault().CircleName,
                    Zone = x.FirstOrDefault().DivisionName
                    //Region = x.FirstOrDefault().Region
                }).ToList()
            }).Distinct().ToList();
            return Json(data1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGraphData()
        {
            List<AdminGraph> lstdata = new List<AdminGraph>();
            List<AdminGraph> lstdata1 = new List<AdminGraph>();

            try
            {
                AdminGraph adminGraph = new AdminGraph();
                DataSet dataSet = adminGraph.GetGraphData();
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                    {
                        AdminGraph adminGraph1 = new AdminGraph();
                        adminGraph1.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                        adminGraph1.TotalCount = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalCount"].ToString());
                        lstdata.Add(adminGraph1);
                    }
                    for (int i = 0; i <= dataSet.Tables[1].Rows.Count - 1; i++)
                    {
                        AdminGraph adminGraph1 = new AdminGraph();
                        adminGraph1.Name = dataSet.Tables[1].Rows[i]["Name"].ToString();
                        adminGraph1.TotalCount = decimal.Parse(dataSet.Tables[1].Rows[i]["TotalCount"].ToString());
                        lstdata1.Add(adminGraph1);
                    }

                }
                adminGraph.lstData = lstdata;
                adminGraph.lstData1 = lstdata1;
                return Json(adminGraph, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(lstdata);
            }
        }
        public JsonResult GetClosingTime()
        {
            AdminGraph adminGraph = new AdminGraph();
            try
            {
               
                DataSet dataSet = adminGraph.GetClosingTime();
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    adminGraph.ClosingDate = dataSet.Tables[0].Rows[0]["ClosingDate"].ToString();
                }
               
                return Json(adminGraph, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
               
            }
            return Json(adminGraph, JsonRequestBehavior.AllowGet);
        }



    }
}
