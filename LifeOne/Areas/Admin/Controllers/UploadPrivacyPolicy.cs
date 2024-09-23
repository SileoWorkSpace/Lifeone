using iTextSharp.text.pdf.qrcode;
using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using LifeOne.Models.API;
using System.Data;

namespace LifeOne.Areas.Admin.Controllers
{
  
    [SessionTimeoutAttributeAdmin]
    public class UploadPrivacyPolicyController : Controller
    {
        // GET: Admin/UploadPrivacyPolicy
        public ActionResult Index()
        {
            return View();
        }       
        public ActionResult UploadPrivacyPolicy(string id)
        {
            PrivacyPolicyService _objService = new PrivacyPolicyService();
            AdminPrivacyPolicy obj = new AdminPrivacyPolicy();
            if (!PermissionManager.IsActionPermit("Upload Privacy Policy", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            if (id != null)
            {
                obj.Pk_Id = id;
                try
                {
                    obj = _objService.PrivacyPolicyEditService(obj);                                  
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult UploadPrivacyPolicySave(AdminPrivacyPolicy model)
        {
            if (!PermissionManager.IsActionPermit("Upload Privacy Policy", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponsePrivacyPolicy _result = new ResponsePrivacyPolicy();
            try
            {
                PrivacyPolicyService _objService = new PrivacyPolicyService();              
                _result = _objService.SavePrivacyPolicyService(model);
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
            return Redirect("/UploadPrivacyPolicyList");
        }
        public ActionResult UploadPrivacyPolicyList()
        {
            PrivacyPolicyService _objService = new PrivacyPolicyService();
            AdminPrivacyPolicy obj = new AdminPrivacyPolicy();
            List<AdminPrivacyPolicy> objlst = new List<AdminPrivacyPolicy>();
            try
            {
                objlst = _objService.GetPrivacyPolicy(obj);
                if (objlst != null)
                {
                    obj.PrivacyPolicyList = objlst;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);
        }
        public ActionResult UploadPrivacyPolicyDelete(string id)
        {
            if (!PermissionManager.IsActionPermit("Product Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            PrivacyPolicyService _objService = new PrivacyPolicyService();
            ResponsePrivacyPolicy obj = new ResponsePrivacyPolicy();
            try
            {
                obj = _objService.PrivacyPolicyDeleteService(id);
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

            return Redirect("/UploadPrivacyPolicyList");
        }
    }
}