using ClosedXML.Excel;
using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.Common;
using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.HomeManagement.HService;
using LifeOne.Models.Manager;
using LitJson;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Configuration;

namespace LifeOne.Areas.Admin.Controllers
{

    [SessionTimeoutAttributeAdmin]
    public class FranchiseMasterController : Controller
    {
        // GET: Admin/FranchiseMaster
        FranchiseMasterService _objService = new FranchiseMasterService();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult FranchiseMasterList(MAdminFranchise obj, int? Page)
        {
            if (!PermissionManager.IsActionPermit("Franchisees", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            obj.Page = Page;
            obj.Size = SessionManager.Size;
            List<MAdminFranchise> objlst = new List<MAdminFranchise>();
            try
            {

                objlst = _objService.FranchiseMasterGetService(obj);
                if (obj.Contact != null)
                {
                    obj.Contact = obj.Contact;
                }
                if (obj.PersonName != null)
                {
                    obj.PersonName = obj.PersonName;
                }
                if (obj.FranchiseType != null)
                {
                    obj.FranchiseType = obj.FranchiseType;
                }
                if (obj.P_PinCode != null)
                {
                    obj.P_PinCode = obj.P_PinCode;
                }
                if (objlst != null)
                {
                    obj.FranchiseMasterList = objlst;
                }
                if(obj.IsExport==1)
                {
                    DataTable dt = new DataTable("FranchiseMasterList");
                    dt.Columns.AddRange(new DataColumn[18] {

                                                     new DataColumn("Name"),
                                                     new DataColumn("FranchiseType"),
                                                     new DataColumn("LoginId"),
                                                     new DataColumn("Mobile"),
                                                     new DataColumn("Email"),
                                                     new DataColumn("Dateofbirth"),
                                                     new DataColumn("Address"),
                                                     new DataColumn("PinCode"),
                                                      new DataColumn("State"),
                                                      new DataColumn("City"),
                                                       new DataColumn("Aadhar Number"),
                                                       new DataColumn("Pan Number"),
                                                        new DataColumn("Company Name"),
                                                            new DataColumn("Account Holder"),
                                                         new DataColumn("Account Number"),
                                                          new DataColumn("Bank Name"),
                                                           new DataColumn("IFsc Code"),
                                                     new DataColumn("Status")


            });
                    if (obj.FranchiseMasterList.Count > 0)
                    {
                        foreach (var customer in obj.FranchiseMasterList)
                        {
                            dt.Rows.Add(customer.PersonName, customer.FranchiseType, customer.LoginID, customer.Contact, customer.EmailAddress, customer.DisplayDOB, customer.P_Address, customer.P_PinCode, customer.P_State, customer.P_City, customer.AadharNumber, customer.PanNumber, customer.CompanyName, customer.AccountHolder, customer.AccountNumber, customer.BankName, customer.IfscCode, customer.IsBlocked == "False" ? "unblock" : "Block");

                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Franchisee.xlsx");
                            }
                        }
                    }
                }
              
                    int totalRecords = 0;
                    if (obj.FranchiseMasterList.Count > 0)
                    {

                        totalRecords = Convert.ToInt32(obj.FranchiseMasterList[0].TotalRecord);
                        var pager = new Pager(totalRecords, obj.Page, SessionManager.Size);
                        obj.Pager = pager;
                    }
                
                
               

            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult FranchiseMaster(string id, string LoginID)
        {
            if (!PermissionManager.IsActionPermit("Franchisees", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminFranchise obj = new MAdminFranchise();
            if (id != null)
            {
                obj.PKFranchiseRegistrationId = id;
                try
                {
                    obj = _objService.FranchiseMasterEditService(obj);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            if (!string.IsNullOrEmpty(LoginID))
            {
                obj.LoginID = LoginID;
                TempData["BLoginId"] = obj.LoginID;
                TempData.Keep();
                obj = _objService.FranchiseGetDetailsByLoginId(obj);
                TempData["code"] = Convert.ToString(obj.Msg);
                TempData["msg"] = Convert.ToString(obj.Remark);

            }


            return View(obj);
        }

        [HttpPost]
        public ActionResult FranchiseMaster(MAdminFranchise obj)
        {
            
            if (!PermissionManager.IsActionPermit("Franchisees", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            FranchiseResponseMaster _result = new FranchiseResponseMaster();
            try
            {
                if (TempData["BLoginId"] != null)
                {
                    obj.LoginID = TempData["BLoginId"].ToString();
                }
               

                    _result = _objService.FranchiseMasterSaveService(obj);
                if (_result.Flag != null)
                {

                    /*updated on 18/12/2020*/
                    string message = SMS.PlotHolderRegistrationSMS(obj.PersonName, obj.Contact, "LifeOne123");
                    //SMS.SendSMSWitmtemp(obj.Contact, message,ConfigurationManager.AppSettings["TEMP-SMS-PLOTHOLDER-REGISTRATION"].ToString());

                    TempData["code"] = _result.Flag.ToString();
                    TempData["msg"] = _result.Msg.ToString();
                    return Redirect("/Franchise-Master-List");
                }

                else
                {
                    TempData["code"] = "";
                    TempData["msg"] = _result.Msg.ToString();

                }

            }
            catch (Exception)
            {
            }

            return View(obj);
        }

        [HttpGet]
        public ActionResult FranchiseMasterDelete(string id)
        {
            if (!PermissionManager.IsActionPermit("Franchisees", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            FranchiseResponseMaster obj = new FranchiseResponseMaster();
            try
            {
                obj = _objService.FranchiseMasterDeleteService(id);
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

            return RedirectToAction("FranchiseMasterList", "FranchiseMaster");

        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetLocationByPin(int? pin)
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


        [HttpGet]
        public ActionResult FranchiseMasterBlockUnblock(string id)
        {
            FranchiseResponseMaster obj = new FranchiseResponseMaster();
            try
            {
                obj = _objService.FranchiseMasterBlockUnblockService(id);
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

            return RedirectToAction("FranchiseMasterList", "FranchiseMaster");

        }

        [HttpGet]
        public ActionResult ChildFranchiseList(string Id)
        {
            MAdminFranchise obj = new MAdminFranchise();
            List<MAdminFranchise> objlst = new List<MAdminFranchise>();
            try
            {

                obj.PKFranchiseRegistrationId = Id;

                objlst = _objService.ChildFranchiseGetService(obj);
                if (objlst != null)
                {
                    obj.FranchiseMasterList = objlst;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);
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

        public ActionResult FranchiseStockList()
        {
            return View();
        }


      
        [HttpGet]
        public ActionResult FranchiseApproval(FranchiseApplicationFormReportFilterModel req, string t)
        {
            if (!PermissionManager.IsActionPermit("Franchisee Request", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            FranchiseApplicationFormReportModel model = new FranchiseApplicationFormReportModel();
            model.AssociateName = req.AssociateName;
            model.MobileNo = req.MobileNo;
            model.EmailId = req.EmailId;
            model.AppliedForPinCode = !string.IsNullOrEmpty(req.AppliedForPinCode) ? Convert.ToInt32(req.AppliedForPinCode) : 0;
            model.page = req.page ?? 1;
            model.LoginId = req.LoginId;
            if (model.AppliedForPinCode > 0)
            {
                model.page = 1;
            }
            model.procId = 1;
            model.Size = SessionManager.Size;
            FranchiseApplicationFormReportModel _data = CustomerRegService.BindFranchiseApplicationForm(model);
            return View(_data);
        }


        [HttpPost]
        public ActionResult FranchiseApproval(FranchiseApplicationFormReportFilterModel req)
        {
            if (!PermissionManager.IsActionPermit("Franchise Request", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            //  FranchiseApplicationFormReportModel model
            FranchiseApplicationFormReportModel model = new FranchiseApplicationFormReportModel();
            model.AssociateName = req.AssociateName;
            model.MobileNo = req.MobileNo;
            model.EmailId = req.EmailId;
            model.AppliedForPinCode = !string.IsNullOrEmpty(req.AppliedForPinCode) ? Convert.ToInt32(req.AppliedForPinCode) : 0;
            model.page = req.page ?? 1;
            model.LoginId = req.LoginId;
            if (model.AppliedForPinCode > 0)
            {
                model.page = 1;
            }
            
           
            model.procId = 1;
            model.Size = SessionManager.Size;
            FranchiseApplicationFormReportModel _data = CustomerRegService.BindFranchiseApplicationForm(model);
           
            return View(_data);
        }

        public JsonResult CheckFranchiseApprovalByPincode(int? pincode)
        {
            return Json(CustomerRegService.CheckFranchiseApprovalByPincode(pincode), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ApproveFranchiseApprovalbackup(int? KeyId, int? Status, string Remark, string DeviceId, string AssociateName, string ApproveStatus, string FK_MemId, string AppliedForPinCode)
        {
            string Message = string.Empty;
            NotificationService _objService = new NotificationService();
            Message = "Dear " + AssociateName + ", Your application for pincode(" + AppliedForPinCode + ") Franchisee is approved by Karyon and LifeOne admin. Please deposit payment in bank account AC name - LifeOne Pvt Ltd Bank - ICICI Bank Vimannagar Pune AC No - 777705006399 IFSC - ICIC0000915 Submit transaction details in LifeOne app franchise section upto 25th March 2021. More details please contact - 091752 68769. Thanks.";
            FranchiseApprovalResponseModel res = CustomerRegService.ApproveFranchiseApproval(KeyId, Status, Remark);
            string abc = SendNotification.SendNotificationForMobile(DeviceId, "Franchise Approved For Pincode-" + AppliedForPinCode, Message);
            var xmldata = "<Notification>";
            xmldata += "<Notificationdata><Tittle>" + "Franchise Approved For Pincode-" + AppliedForPinCode + "</Tittle><Message>" + Message + "</Message><NotificationType>" + ApproveStatus + "</NotificationType><Fk_MemId>" + FK_MemId + "</Fk_MemId><Fk_RequestId>" + KeyId + "</Fk_RequestId><CreatedBy>" + 1 + "</CreatedBy></Notificationdata>";
            xmldata += "</Notification>";
            _objService.SaveNotification(xmldata);
            return Json(res);
        }


        [HttpPost]
        public JsonResult ApproveFranchiseApproval(int? KeyId, int? Status, string Remark, string DeviceId, string AssociateName, string ApproveStatus, string FK_MemId, string AppliedForPinCode, string requesType)
        {
            string Message = string.Empty;
            string abc = string.Empty;
            NotificationService _objService = new NotificationService();
            FranchiseApprovalResponseModel res = CustomerRegService.ApproveFranchiseApproval(KeyId, Status, Remark);
            /*Notification*/
            if (res.Fkfrachnisetypeid != "" && res.Fkfrachnisetypeid != "0")
            {
                if (res.Fkfrachnisetypeid == "4") //pincode
                {
                    Message = "Dear " + AssociateName + ", Your application for pincode(" + AppliedForPinCode + ") Franchise is approved by Karyon and LifeOne admin. Please deposit payment in bank account AC name - LifeOne Pvt Ltd Bank - ICICI Bank Vimannagar Pune AC No - 777705006399 IFSC - ICIC0000915 Submit transaction details in LifeOne app franchise section upto 25th March 2021. More details please contact - 091752 68769. Thanks.";
                    abc = SendNotification.SendNotificationForMobile(DeviceId, "Franchise Approved For Pincode-" + AppliedForPinCode, Message);
                    var xmldata = "<Notification>";
                    xmldata += "<Notificationdata><Tittle>" + "Franchise Approved For Pincode-" + AppliedForPinCode + "</Tittle><Message>" + Message + "</Message><NotificationType>" + ApproveStatus + "</NotificationType><Fk_MemId>" + FK_MemId + "</Fk_MemId><Fk_RequestId>" + KeyId + "</Fk_RequestId><CreatedBy>" + 1 + "</CreatedBy></Notificationdata>";
                    xmldata += "</Notification>";
                    _objService.SaveNotification(xmldata);

                    string message = SMS.ApproveFranchise(AssociateName, res.PinCode);
                    SMS.SendSMS(res.Mobile, message);
                }
                else
                {
                    Message = "Dear " + AssociateName + ", Your application for district(" + res.District + ") Franchise is approved by Karyon and LifeOne admin. Please deposit payment in bank account AC name - LifeOne Pvt Ltd Bank - ICICI Bank Vimannagar Pune AC No - 777705006399 IFSC - ICIC0000915 Submit transaction details in LifeOne app franchise section upto 25th March 2021. More details please contact - 091752 68769. Thanks.";
                    abc = SendNotification.SendNotificationForMobile(DeviceId, "Franchise Approved For district-" + res.District, Message);
                    var xmldata = "<Notification>";
                    xmldata += "<Notificationdata><Tittle>" + "Franchise Approved For district-" + res.District + "</Tittle><Message>" + Message + "</Message><NotificationType>" + ApproveStatus + "</NotificationType><Fk_MemId>" + FK_MemId + "</Fk_MemId><Fk_RequestId>" + KeyId + "</Fk_RequestId><CreatedBy>" + 1 + "</CreatedBy></Notificationdata>";
                    xmldata += "</Notification>";
                    _objService.SaveNotification(xmldata);

                    string message = SMS.ApproveFranchiseDistrict(AssociateName, res.District);
                    SMS.SendSMS(res.Mobile, message);
                }
            }
            return Json(res);
        }


        public JsonResult GetOtherFranchiseApprovalDetails(int? KeyId)
        {
            return Json(CustomerRegService.GetOtherFranchiseApprovalDetails(KeyId ?? 0));
        }
        public ActionResult ApprovedFranchise(FranchiseApplicationFormReportFilterModel filterModel)
        {
            FranchiseApplicationFormReportModel _data = new FranchiseApplicationFormReportModel();
            if (filterModel.page == null)
                _data.page = 1;
            else
                _data.page = Convert.ToInt32(filterModel.page);

            if (filterModel.AppliedForPinCode != null && filterModel.AppliedForPinCode != "")
            {
                _data.AppliedForPinCode = Convert.ToInt32(filterModel.AppliedForPinCode);
                _data.page = 1;
            }

            _data.LoginId = filterModel.LoginId;
            _data.AssociateName = filterModel.AssociateName;
            _data.MobileNo = filterModel.MobileNo;
            _data.EmailId = filterModel.EmailId;
            _data.procId = 2;
            _data = CustomerRegService.BindFranchiseApplicationForm(_data);
            return View(_data);
        }

        public ActionResult DeclinedFranchise(FranchiseApplicationFormReportFilterModel filterModel)
        {
            FranchiseApplicationFormReportModel _data = new FranchiseApplicationFormReportModel();
            if (filterModel.page == null)
                _data.page = 1;
            else
                _data.page = Convert.ToInt32(filterModel.page);

            if (filterModel.AppliedForPinCode != null && filterModel.AppliedForPinCode != "")
            {
                _data.AppliedForPinCode = Convert.ToInt32(filterModel.AppliedForPinCode);
                _data.page = 1;
            }

            _data.LoginId = filterModel.LoginId;
            _data.AssociateName = filterModel.AssociateName;
            _data.MobileNo = filterModel.MobileNo;
            _data.EmailId = filterModel.EmailId;
            _data.procId = 3;
            _data = CustomerRegService.BindFranchiseApplicationForm(_data);
            return View(_data);
        }
        public ActionResult exsportforapproval(int? Page)
        {
            BlockUnblockService db = new BlockUnblockService();

            MembersResponse _objres = new MembersResponse();
            FranchiseApplicationFormReportModel _model = new FranchiseApplicationFormReportModel();
            _model.procId = 1;
            if (_model.AssociateName == "AssociateName")
            {
                _model.AssociateName = _model.AssociateName;
            }
            if (_model.MobileNo == "Mobile")
            {
                _model.MobileNo = _model.MobileNo;
            }
            if (_model.EmailId == "Name")
            {
                _model.EmailId = _model.EmailId;
            }


            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.page = 1;

            // var customers = MembersService.InvokeMemberDetails(_model);
            List<FranchiseApplicationFormReportModel> customers = LifeOne.Models.HomeManagement.HDAL.DALCustomerRegistration.BindFranchiseApplicationFormList(_model);
            DataTable dt = new DataTable("MemberkDetails");
            dt.Columns.AddRange(new DataColumn[11] {
                                                     new DataColumn("AssociateId"),
                                                     new DataColumn("AssociateName"),
                                                      new DataColumn("Franchise Name"),
                                                     new DataColumn("MobileNo"),
                                                     new DataColumn("EmailId"),
                                                     new DataColumn("AppliedForPinCode"),
                                                     new DataColumn("AreaName"),
                                                     new DataColumn("State"),
                                                     new DataColumn("District"),
                                                      new DataColumn("Taluka"),
                                                     new DataColumn("Status")

            });

            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.AssociateId, customer.AssociateName, customer.FranchiseName, customer.MobileNo, customer.EmailId, customer.AppliedForPinCode, customer.AreaName, customer.State, customer.District, customer.Taluka, customer.Status);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FranchiseApprovalList.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("FranchiseApproval");
            }
            // return View();
        }
        public ActionResult ExportForApproved(int? Page)
        {
            BlockUnblockService db = new BlockUnblockService();

            MembersResponse _objres = new MembersResponse();
            FranchiseApplicationFormReportModel _model = new FranchiseApplicationFormReportModel();
            _model.procId = 1;
            if (_model.AssociateName == "AssociateName")
            {
                _model.AssociateName = _model.AssociateName;
            }
            if (_model.MobileNo == "Mobile")
            {
                _model.MobileNo = _model.MobileNo;
            }
            if (_model.EmailId == "Name")
            {
                _model.EmailId = _model.EmailId;
            }


            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.page =1;
            _model.procId = 2;

            // var customers = MembersService.InvokeMemberDetails(_model);
            List<FranchiseApplicationFormReportModel> customers = LifeOne.Models.HomeManagement.HDAL.DALCustomerRegistration.BindFranchiseApplicationFormList(_model);
            DataTable dt = new DataTable("MemberkDetails");
            dt.Columns.AddRange(new DataColumn[7] {
                                                     new DataColumn("AssociateId"),
                                                     new DataColumn("AssociateName"),
                                                     new DataColumn("MobileNo"),
                                                     new DataColumn("EmailId"),
                                                     new DataColumn("AppliedForPinCode"),
                                                     new DataColumn("AreaName"),
                                                     new DataColumn("Status")

            });

            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.AssociateId, customer.AssociateName, customer.MobileNo, customer.EmailId, customer.AppliedForPinCode, customer.AreaName, customer.Status);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ApprovedFranchiseList.xlsx");
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
        public ActionResult ExportForDecline(int? Page)
        {
            BlockUnblockService db = new BlockUnblockService();

            MembersResponse _objres = new MembersResponse();
            FranchiseApplicationFormReportModel _model = new FranchiseApplicationFormReportModel();
            _model.procId = 1;
            if (_model.AssociateName == "AssociateName")
            {
                _model.AssociateName = _model.AssociateName;
            }
            if (_model.MobileNo == "Mobile")
            {
                _model.MobileNo = _model.MobileNo;
            }
            if (_model.EmailId == "Name")
            {
                _model.EmailId = _model.EmailId;
            }


            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            _model.page = 1;
            _model.procId = 3;

            // var customers = MembersService.InvokeMemberDetails(_model);
            List<FranchiseApplicationFormReportModel> customers = LifeOne.Models.HomeManagement.HDAL.DALCustomerRegistration.BindFranchiseApplicationFormList(_model);
            DataTable dt = new DataTable("MemberkDetails");
            dt.Columns.AddRange(new DataColumn[7] {
                                                     new DataColumn("AssociateId"),
                                                     new DataColumn("AssociateName"),
                                                     new DataColumn("MobileNo"),
                                                     new DataColumn("EmailId"),
                                                     new DataColumn("AppliedForPinCode"),
                                                     new DataColumn("AreaName"),
                                                     new DataColumn("Status")

            });

            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.AssociateId, customer.AssociateName, customer.MobileNo, customer.EmailId, customer.AppliedForPinCode, customer.AreaName, customer.Status);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DeclinedFranchiseList.xlsx");
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
    }
}

