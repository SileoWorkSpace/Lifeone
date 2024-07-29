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
    public class UploadTermsandConditionController : Controller
    {
        // GET: Admin/UploadTermsandCondition
        public ActionResult Index()
        {
            return View();
        }       
        public ActionResult UploadTermsandCondition(string id)
        {
            TermsConditionService _objService = new TermsConditionService();
            AdminTermsCondition obj = new AdminTermsCondition();
            if (!PermissionManager.IsActionPermit("Upload Terms and Condition", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            if (id != null)
            {
                obj.Pk_Id = id;
                try
                {
                    obj = _objService.TermsConditionEditService(obj);

                    //obj.TermsandCondition = obj.Rows[0]["TermsandCondition"].ToString();                    

                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult UploadTermsandConditionSave(AdminTermsCondition model)
        {
            if (!PermissionManager.IsActionPermit("Upload Terms and Condition", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseTermsCondition _result = new ResponseTermsCondition();
            try
            {
                TermsConditionService _objService = new TermsConditionService();              
                _result = _objService.SaveTermsConditionService(model);
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
            return Redirect("/UploadTermsandConditionList");
        }
        public ActionResult UploadTermsandConditionList()
        {
            TermsConditionService _objService = new TermsConditionService();
            AdminTermsCondition obj = new AdminTermsCondition();
            List<AdminTermsCondition> objlst = new List<AdminTermsCondition>();
            try
            {

                objlst = _objService.GetTermsandCondition(obj);
                if (objlst != null)
                {
                    obj.TermsConditionList = objlst;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);
        }
        public ActionResult TermsandConditionDelete(string id)
        {
            if (!PermissionManager.IsActionPermit("Product Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            TermsConditionService _objService = new TermsConditionService();
            ResponseTermsCondition obj = new ResponseTermsCondition();
            try
            {
                obj = _objService.TermsConditionDeleteService(id);
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

            return Redirect("/UploadTermsandConditionList");

        }



    }
}