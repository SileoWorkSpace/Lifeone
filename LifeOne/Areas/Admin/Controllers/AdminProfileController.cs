//using iTextSharp.text.pdf.qrcode;
using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LifeOne.Models.Common;
using System.IO;
//using DocumentFormat.OpenXml.EMMA;

namespace LifeOne.Areas.Admin.Controllers
{
   
    [SessionTimeoutAttributeAdmin]
    public class AdminProfileController : Controller
    {
        // GET: Admin/AdminProfile
        public ActionResult Index()
        {
            return View();
        }

        #region ChangePassword
        public ActionResult AdminChangePassword(int? FK_MemId )
        {
            
            MAdminProfile model = new MAdminProfile();
           
            if (FK_MemId != null)
            {
              
            model.Fk_Memid = Convert.ToInt64(FK_MemId.ToString());
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AdminChangePassword(MAdminProfile model)
        {
            ResponseAdminProfileModel _result = new ResponseAdminProfileModel();
            try
            {
                AdminProfileService _objService = new AdminProfileService();

                model.LoginId = SessionManager.LoginId;
                model.Fk_Memid = SessionManager.Fk_MemId;
                model.NormalPass = model.NewPassword;
                model.NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.NewPassword, "md5");
                _result = _objService.AdminChangePasswordService(model);
                if (_result != null)
                {

                    TempData["code"] = _result.Flag;
                    TempData["msg"] = _result.Msg;
                    return RedirectToAction("Login", "Home", new { area = "" });
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
            return RedirectToAction("AdminChangePassword", "AdminProfile");
        }
        public ActionResult FranchiseChangePassword(int? FK_MemId)
        {
            MAdminProfile model = new MAdminProfile();
            if (FK_MemId != null)
            {

                model.Fk_Memid = Convert.ToInt64(FK_MemId.ToString());
                ViewBag.FkMemId = model.Fk_Memid;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult FranchiseChangePassword(MAdminProfile model)
        {
            ResponseAdminProfileModel _result = new ResponseAdminProfileModel();
            try
            {
                AdminProfileService _objService = new AdminProfileService();

                //model.LoginId = SessionManager.LoginId;
                model.Fk_Memid = model.Fk_Memid;
                model.NormalPass = model.NewPassword;
                model.NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.NewPassword, "md5");
                _result = _objService.FranchiseChangePasswordService(model);
                if (_result != null)
                {

                    TempData["code"] = _result.Flag.ToString();
                    TempData["msg"] = _result.Msg;
                    return RedirectToAction("FranchiseMasterList", "FranchiseMaster", new { area = "Admin" });
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
            return RedirectToAction("FranchiseChangePassword", "AdminProfile");
        }

        #endregion

        #region ChangeProfilePicture

        public ActionResult AdminChangeProfilePicture()
        {
            //AdminProfileService _objService = new AdminProfileService();
            //ResponseAdminProfileModel _result = new ResponseAdminProfileModel();
            //model.Fk_Memid = SessionManager.Fk_MemId;
            //if (!string.IsNullOrEmpty(Update))
            //{
            //    model.NewPicture = Session["ProfilePic_NewPic"].ToString();

            //    model.OpCode = 1;
            //    _result = _objService.AdminChangeProfilePictureService(model);
            //    TempData["code"] = _result.Flag;
            //    TempData["msg"] = _result.Msg;
            //    if (_result.Flag == 1)
            //    {
            //        SessionManager.ProfilePic = Session["ProfilePic"].ToString();
            //    }
            //    return View(model);
            //}
            //else
            //{
                //model.OpCode = 2;
                //_result = _objService.AdminChangeProfilePictureService(model);
              //  Session["ProfilePic"] = SessionManager.ProfilePic;
                return View();
           // }
        }

        [HttpPost]
        public JsonResult Uploadfile(MAdminProfile model,HttpPostedFileBase file)
        {
            AdminProfileService _objService = new AdminProfileService();
            ResponseAdminProfileModel _result = new ResponseAdminProfileModel();
            model.Fk_Memid = SessionManager.Fk_MemId;
            
            if (file != null && file.ContentLength > 0)
            {
                var ImagePath = "/Content/AdminPicture/";
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".BMP" || extension.ToUpper() == ".PDF")
                {
                    if (!Directory.Exists(Server.MapPath(ImagePath)))
                    {
                        Directory.CreateDirectory((Server.MapPath(ImagePath)));
                    }

                    //Guid.NewGuid().ToString().Replace("_", "") + "_" + fileName;
                     fileName = fileName.Replace( fileName, SessionManager.Fk_MemId.ToString());
                    fileName = fileName + extension;
                    string fullPath = Request.MapPath(ImagePath+fileName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);

                    }
                    file.SaveAs(Path.Combine(Server.MapPath(ImagePath), fileName));

                    Session["ProfilePic_NewPic"] = ImagePath+fileName;
                    TempData["Src"] = ImagePath + fileName;

                 

                    model.NewPicture = Session["ProfilePic_NewPic"].ToString();

                    model.OpCode = 1;
                    _result = _objService.AdminChangeProfilePictureService(model);
                    TempData["code"] = _result.Flag;
                    TempData["msg"] = _result.Msg;
                    if (_result.Flag == 1)
                    {
                        SessionManager.ProfilePic = _result.OldPicture;
                    }
                   

                } 
            }
            
            return Json(_result, JsonRequestBehavior.AllowGet);
            }
            #endregion ChangeProfilePicture
        }
}