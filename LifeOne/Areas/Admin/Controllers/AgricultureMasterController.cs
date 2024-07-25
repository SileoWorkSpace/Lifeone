using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Filters;
using LifeOne.Models.Manager;
using SelectPdf;

namespace LifeOne.Areas.Admin.Controllers
{
    //[AuthorizeAdmin]
    //[MenuPermissionFilter]
    [SessionTimeoutAttributeAdmin]
    public class AgricultureMasterController : Controller
    {
        // GET: Admin/AgricultureMaster/AddMaster
        AgricultureMasterService _Service = new AgricultureMasterService();
        public ActionResult AddMaster()
        {
            MAgricultureMaster master = new MAgricultureMaster();
            ViewBag.CropCategory = DALBindCommonDropdown.BindDropdown(14, 0);
            return View(master);
        }

        // GET: Admin/AgricultureMaster/AddMaster
        [HttpPost]
        public ActionResult AddMaster(MAgricultureMaster model)
        {
            if (!PermissionManager.IsActionPermit("Agriculture Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            AddUpdateResponseResponseModel response = _Service.AddMaster(model);
            ViewBag.Status = response.Status;
            ViewBag.Message = response.Message;
            TempData["Status"] = response.Status;
            TempData["Message"] = response.Message;
            return Redirect("/Agriculture-Master-Report");
        }

        public ActionResult AgricultureMasterReport()
        {
            if (!PermissionManager.IsActionPermit("Agriculture Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }
        public JsonResult BindAgricultureMasterReport(AgricultureMasterFilter req)
        {
            return Json(_Service.BindAgricultureMasterReport(req));
        }
        [AllowAnonymous]
        public JsonResult GetCropFullDetais(string CropCode, string CropType)
        {
            return Json(_Service.GetCropDetailsByCropCodeReport(CropCode, CropType));
        }

        [HttpGet]
        public ActionResult EditMaster(string CropCode, string CropType)
        {
            if (!PermissionManager.IsActionPermit("Agriculture Master", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.CropCategory = DALBindCommonDropdown.BindDropdown(14, 0);
            MAgricultureMasterModel response = _Service.GetCropDetailsByCropCode(CropCode, CropType);
            ViewBag.ProductMasterList = _Service.BindProductMster();

            return View(response);
        }

        [HttpPost]
        public JsonResult DeleteCrop(string CropCode, string CropType)
        {
            return Json(_Service.DeleteCrop(CropCode, CropType));
        }

        [HttpPost]
        public ActionResult UpdateMaster(MAgricultureMaster model)
        {
            if (!PermissionManager.IsActionPermit("Agriculture Master", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            AddUpdateResponseResponseModel response = _Service.UpdateMaster(model);
            TempData["Status"] = response.Status;
            TempData["Message"] = response.Message;
            return Redirect("/Agriculture-Master-Report");
        }
        [AllowAnonymous]
        public JsonResult BindProductMster()
        {
            return Json(_Service.BindProductMster());
        }


        [AllowAnonymous]
        public JsonResult _GetCropFullDetais(string CropCode, int? FKMemid, int PK_OrderId, string LanguageCode)
        {
            return Json(_Service._GetCropDetailsByCropCode(CropCode, FKMemid, PK_OrderId, LanguageCode));
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ViewCropDetails(string CropCode, string CropType)
        {
            if (!PermissionManager.IsActionPermit("Agriculture Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }
        [AllowAnonymous]
        public void CropDetailsPDF(string CropCode, int? FKMemid, int PK_OrderId, string LanguageCode)
        {

            if (!string.IsNullOrEmpty(CropCode))
            {
                string msg = "Please see the attached invoice";
                HtmlToPdf htmlToPdf = new HtmlToPdf();
                //htmlToPdf.Options.MarginBottom = 5;
                //htmlToPdf.Options.MarginTop = 5;
                //htmlToPdf.Options.MarginLeft = 15;
                //htmlToPdf.Options.MarginRight = 15;
                //PdfDocument pdfDocument = htmlToPdf.ConvertUrl("https://localhost:44380" + "/Admin/ManageAssociateOrdered/Invoice?orderId=" + OrderId);
                PdfDocument pdfDocument = new PdfDocument();
                if (HttpContext.Request.Url.Host.ToLower() == "localhost")
                {
                    pdfDocument = htmlToPdf.ConvertUrl("https://localhost:44380/admin/AgricultureMaster/_GetCropFullDetais?CropCode=" + CropCode + "&FKMemid?=" + FKMemid + "&PK_OrderId?=" + PK_OrderId + "&LanguageCode?=" + LanguageCode);
                }
                else
                {
                    pdfDocument = htmlToPdf.ConvertUrl("http://LifeOne.com/" + "/admin/AgricultureMaster/_GetCropFullDetais?CropCode=" + CropCode + "&FKMemid?=" + FKMemid + "&PK_OrderId?=" + PK_OrderId + "&LanguageCode?=" + LanguageCode);
                }
                string guid = Guid.NewGuid().ToString().Substring(0, 7);
                pdfDocument.Save(HttpContext.ApplicationInstance.Response, false, "Crop" + CropCode + guid + ".pdf");
                pdfDocument.Close();
            }
        }
        public JsonResult GetCropDetail(string catid)
        {
            var list = DALBindCommonDropdown.BindDropdown(15, long.Parse(catid));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}