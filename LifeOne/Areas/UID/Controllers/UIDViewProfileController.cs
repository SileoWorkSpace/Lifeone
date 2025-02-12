﻿using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.HomeManagement.HService;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Dynamic;
using LifeOne.Models.Common;
using LifeOne.Models.HomeManagement.HEntity;

namespace LifeOne.Areas.UID.Controllers
{
   // [SessionTimeoutAttribute]
    public class UIDViewProfileController : Controller
    {
        // GET: Associate/UIDViewProfile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewProfile()
        {
            try
            {
                AssociateProfileService _objService = new AssociateProfileService();
                AssociateProfile objres = new AssociateProfile();
                objres.Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId.ToString());
                if (objres.Fk_MemId == 0)
                {
                    return RedirectToAction("Login", "Home");
                }
                objres = AssociateProfileService.InvokeCustomerDetailsById(objres.Fk_MemId);
                objres.lstChild = AssociateProfileService.InvokeChildDetailsById(objres.Fk_MemId);
                return View(objres);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult Edit()
        {
            AssociateProfile objres = new AssociateProfile();
            objres.Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId.ToString());
            if (objres.Fk_MemId == 0)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            objres = AssociateProfileService.InvokeCustomerDetailsById(objres.Fk_MemId);
            objres.lstChild = AssociateProfileService.InvokeChildDetailsById(objres.Fk_MemId);
            return View(objres);
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
        public ActionResult Downline(string FK_MemId)
        {
            MMemberList model = new MMemberList();
            if (FK_MemId == null)
            {
                model.FK_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId.ToString());
            }
            else
            {
                model.FK_MemId = Convert.ToInt32(FK_MemId);
            }
            if (model.FK_MemId == 0)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
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

        public ActionResult DirectMembers()
        {
            MMemberList model = new MMemberList();
            model.FK_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId.ToString());
            if (model.FK_MemId == 0)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            MembersResponse data = new MembersResponse();
            model.Page = 1;
            int ProcId = 1;
            model.MemberListDetail = GetDirectMemberService.GetDirectMemberDetailsService(model.FK_MemId, ProcId, model.Page, 50);
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
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(MAdminProfile model)
        {
            ResponseAdminProfileModel _result = new ResponseAdminProfileModel();
            try
            {

                model.Fk_Memid = Convert.ToInt32(SessionManager.AssociateFk_MemId.ToString());
                if (model.Fk_Memid == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                AdminProfileService _objService = new AdminProfileService();

                model.LoginId = SessionManager.LoginId;
                model.NormalPass = model.NewPassword;
                model.OldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.OldPassword, "md5");
                model.NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.NewPassword, "md5");
                _result = _objService.UserChangePasswordService(model);
                if (_result != null)
                {

                    if (_result.Flag == 1)
                    {
                        TempData["code"] = _result.Flag;
                        TempData["msg"] = _result.Msg;
                        return RedirectToAction("Login", "Home", new { area = "" });
                    }
                    else
                    {
                        TempData["code"] = _result.Flag;
                        TempData["msg"] = _result.Msg;

                    }
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
            return RedirectToAction("ChangePassword", "UIDViewProfile");
        }

        public ActionResult KYC()
        {
            if(TempData["Code"]==null || TempData["Code"].ToString() == "")
            {
                TempData["Code"] = 2;
            }
            KYCDetails Obj = new KYCDetails();
            Obj.Fk_MemId = SessionManager.AssociateFk_MemId;
            if (Obj.Fk_MemId == 0)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            List<KYCDetails> lst = new List<KYCDetails>();
            KYCService _objService = new KYCService();
            Obj.lstKyc = _objService.GetKycInfo(Obj);
            return View(Obj);
        }

        [HttpPost]
        [ActionName("DocumentVerifyAction")]
        public ActionResult SaveKYCInfo(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.AssociateFk_MemId;
            Obj.Fk_MemId = SessionManager.AssociateFk_MemId;
            Obj.Action = "Profile";
            if (Request.Files[0].ContentLength > 0)
            {
                Obj.ProfilePic = Obj.profileimagepath;
             
            }
            KYCService _objService = new KYCService();
            KYCResponse objres = new KYCResponse();
            objres= _objService.SaveKYC(Obj);
            if (objres != null) { 
            if (objres.Flag == 1)
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = objres.Msg;
            }
            else
            {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] =objres.Msg;
            }
            }
            else
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = "Something went wrong";
            }
            return RedirectToAction("KYC", "/UIDViewProfile");
        }

        [HttpPost]
        [ActionName("DocumentVerifyAction1")]
        public ActionResult SaveKYCInfo1(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.AssociateFk_MemId;
            Obj.Fk_MemId = SessionManager.AssociateFk_MemId;
            Obj.Action = "PAN";
            if (Request.Files[0].ContentLength > 0)
            {
                Obj.PanImage = Obj.pancardimagepath;
            }
            KYCService _objService = new KYCService();
            KYCResponse objres = new KYCResponse();
            objres = _objService.SaveKYC(Obj);
            if (objres != null)
            {
                if (objres.Flag == 1)
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
                else
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
            }
            else
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = "Something went wrong";
            }
            return RedirectToAction("KYC", "/UIDViewProfile");
        }


        [HttpPost]
        [ActionName("DocumentVerifyAction2")]
        public ActionResult SaveKYCInfo2(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.AssociateFk_MemId;
            Obj.Fk_MemId = SessionManager.AssociateFk_MemId;
            Obj.Action = "AADHARFRONT";
            if (Request.Files[0].ContentLength > 0)
            {
                Obj.AdharImg = Obj.aadharfrontimagepath;
            }
            KYCService _objService = new KYCService();
            KYCResponse objres = new KYCResponse();
            objres = _objService.SaveKYC(Obj);
            if (objres != null)
            {
                if (objres.Flag == 1)
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
                else
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
            }
            else
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = "Something went wrong";
            }
            return RedirectToAction("KYC", "/UIDViewProfile");
        }
        [HttpPost]
        [ActionName("DocumentVerifyAction4")]
        public ActionResult SaveKYCInfo3(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.AssociateFk_MemId;
            Obj.Fk_MemId = SessionManager.AssociateFk_MemId;
            Obj.Action = "CHEQUE";
            if (Request.Files[0].ContentLength > 0)
            {
                Obj.ChequeBookImg = Obj.chequeimagepath;
            }
            KYCService _objService = new KYCService();
            KYCResponse objres = new KYCResponse();
            objres = _objService.SaveKYC(Obj);
            if (objres != null)
            {
                if (objres.Flag == 1)
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
                else
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
            }
            else
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = "Something went wrong";
            }
            return RedirectToAction("KYC", "/UIDViewProfile");
        }
        [HttpPost]
        [ActionName("DocumentVerifyAction3")]
        public ActionResult SaveKYCInfo4(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.AssociateFk_MemId;
            Obj.Fk_MemId = SessionManager.AssociateFk_MemId;
            Obj.Action = "AADHARBACK";
            if (Request.Files[0].ContentLength > 0)
            {
                Obj.addProofImg = Obj.aadharbackimagepath;
            }
            KYCService _objService = new KYCService();
            KYCResponse objres = new KYCResponse();
            objres = _objService.SaveKYC(Obj);
            if (objres != null)
            {
                if (objres.Flag == 1)
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
                else
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
            }
            else
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = "Something went wrong";
            }
            return RedirectToAction("KYC", "/UIDViewProfile");
        }

        public ActionResult LevelTree(TreeRequest model)
        {
            TreeModel Value = new TreeModel();
            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            if (model.LoginID == null)
            {
                model.LoginID = SessionManager.LoginId;
            }
            if (Session["RootId"] != null)
            {
                if (Session["RootId"].ToString() != "")
                {
                    model.LoginID = Session["RootId"].ToString();
                    Session["RootId"] = null;
                }
            }

            dynamic item = new ExpandoObject();
            AssociateTreeService ObjR = new AssociateTreeService();
            item = ObjR.GetTreeData(model);
            Value = item._papersetinformation;
            Value.TreeList = item._papersetHMLlevel;
            return View(Value);
        }

        [HttpPost]
        [ActionName("LevelTree")]
        public ActionResult SearchGenealogyTree(TreeRequest ObjM)
        {
            TreeModel Value = new TreeModel();
            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            dynamic item = new ExpandoObject();
            AssociateTreeService ObjR = new AssociateTreeService();
            item = ObjR.GetTreeData(ObjM);
            Value = item._papersetinformation;
            Value.TreeList = item._papersetHMLlevel;
            return RedirectToAction("LevelTree", Value);
        }

        public JsonResult GetRootId(string LoginId)
        {
            AssociateTreeService obj = new AssociateTreeService();
            LoginId = SessionManager.LoginId;
            var RootId = obj.AssociateRootId(LoginId);
            return Json(RootId, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUpId(string LoginId)
        {
            AssociateTreeService obj = new AssociateTreeService();
            var RootId = obj.AssociateGetUPId(LoginId);
            Session["RootId"] = RootId;
            return Json(RootId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WelcomeLetter()
        {
            long Fk_MemId = SessionManager.AssociateFk_MemId;
            WelcomeLetter obj = AssociateProfileService.WelcomeLetter(Fk_MemId);
            return View(obj);
        }

        public ActionResult RequestDetailAssociate()
        {
            if (SessionManager.AssociateFk_MemId == 0)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            RequestForAssociateFranchise _model = new RequestForAssociateFranchise();
            _model.Fk_MemId = SessionManager.AssociateFk_MemId;
            RequestForAssociateFranchiseService objService = new RequestForAssociateFranchiseService();
            _model.lstRequest = objService.RequestDetailsForAssociate(_model.Fk_MemId);
            return View(_model);
        }

        [HttpPost]
        public JsonResult RequestForFranchise(string type, string remark)
        {

            RequestForAssociateFranchise obj = new RequestForAssociateFranchise();
            ResponseforRequestForFranchise response = new ResponseforRequestForFranchise();
            try
            {
                obj.Fk_MemId = SessionManager.AssociateFk_MemId;
                RequestForAssociateFranchiseService objService = new RequestForAssociateFranchiseService();
                response = objService.RequestForAssociateFrnachise(type, obj.Fk_MemId, remark);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
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
            catch (Exception)
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
    }
}