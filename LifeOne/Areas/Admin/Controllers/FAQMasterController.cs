using iTextSharp.text.pdf.qrcode;
using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using LifeOne.Models;
using System.Data;
using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.API;

namespace LifeOne.Areas.Admin.Controllers
{ 
    [SessionTimeoutAttributeAdmin]
    public class FAQMasterController : Controller
    {
        // GET: Admin/FAQMaster
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadFAQ(MAdminFAQ model)
        {
            if (!PermissionManager.IsActionPermit("FAQ Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminFAQ _result = new MAdminFAQ();
            try
            {
                model.ProcId = 1;
                FAQService _objService = new FAQService();

                _result.objList = _objService.GetFAQService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult FAQSave(MAdminFAQ model)
        {
            if (!PermissionManager.IsActionPermit("FAQ Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            if (!PermissionManager.IsActionPermit("FAQ Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseFAQModel _result = new ResponseFAQModel();
            try
            {
                if (model.Pk_FAQId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 1; }               
                model.CreatedBy = SessionManager.Fk_MemId;
                FAQService _objService = new FAQService();

                _result = _objService.UploadFAQService(model);
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
            return Redirect("/UploadFAQ");
        }
        public ActionResult UnitDelete(int Id)
        {
            if (!PermissionManager.IsActionPermit("FAQ Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminFAQ model = new MAdminFAQ();
            model.Pk_FAQId = Id;
            model.ProcId = 4;
            ResponseFAQModel _result = new ResponseFAQModel();
            try
            {

                FAQService _objService = new FAQService();

                _result = _objService.UploadFAQService(model);
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
            return Redirect("/UploadFAQ");
        }
        [HttpPost]
        public JsonResult UpdateFAQ(string Pk_FAQId)
        {
            FAQModel obj = new FAQModel();
            try
            {
                List<FAQModel> FAQList1 = new List<FAQModel>();
                obj.Pk_FAQId = int.Parse(Pk_FAQId);
                DataSet ds = obj.getFAQ();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FAQModel FAQList = new FAQModel();
                        FAQList.Question = ds.Tables[0].Rows[i]["Question"].ToString();
                        FAQList.Answer = ds.Tables[0].Rows[i]["Answer"].ToString();
                        FAQList1.Add(FAQList);
                    }
                    obj.FAQList= FAQList1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(obj.FAQList, JsonRequestBehavior.AllowGet);
        }
    }
}