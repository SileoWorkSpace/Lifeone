using SelectPdf;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.AssociateManagement.AssociateEntity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    public class AllInvoiceController : Controller
    {
        AgricultureMasterService _Service = new AgricultureMasterService();
        // GET: Admin/AllInvoice
        public ActionResult Index()
        {
            return View();
        }



        [AllowAnonymous]
        public ActionResult Invoice(string OrderId)
        {
            try
            {
                //OrderId = DALCommon.GlobalDecrypText(OrderId);
                MInvoice obj = new MInvoice();
                //var key = AesEncryptDecrypt.DecryptString(Aeskey, OrderId);
                var list = LifeOne.Models.AdminManagement.ADAL.DBHelperDapper.GetInvocice(OrderId);
                obj = list._MemberDetail;
                obj.lst = list._OrderDetail;
                return View(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AllowAnonymous]
        public ActionResult Invoice_Karyon(string OrderId)
        {
            try
            {
                //OrderId = DALCommon.GlobalDecrypText(OrderId);
                MInvoice obj = new MInvoice();
                //var key = AesEncryptDecrypt.DecryptString(Aeskey, OrderId);
                var list = LifeOne.Models.AdminManagement.ADAL.DBHelperDapper.GetInvocice_Karyon(OrderId);
                obj = list._MemberDetail;
                obj.lst = list._OrderDetail;
                return View(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }



        [AllowAnonymous]
        public ActionResult InvoiceMobile(string OrderId)
        {

            if (OrderId != null || OrderId != "")
            {
                string msg = "Please see the attached invoice";
                HtmlToPdf htmlToPdf = new HtmlToPdf();
                //htmlToPdf.Options.MarginBottom = 5;
                //htmlToPdf.Options.MarginTop = 5;
                //htmlToPdf.Options.MarginLeft = 15;
                //htmlToPdf.Options.MarginRight = 15;
                //PdfDocument pdfDocument = htmlToPdf.ConvertUrl("https://localhost:44380" + "/Admin/ManageAssociateOrdered/Invoice?orderId=" + OrderId);
                PdfDocument pdfDocument = htmlToPdf.ConvertUrl("http://LifeOne.com" + "/Admin/AllInvoice/Invoice?orderId=" + OrderId);
                string guid = Guid.NewGuid().ToString().Substring(0, 7);
                pdfDocument.Save(HttpContext.ApplicationInstance.Response, false, "Invoice_" + guid + ".pdf");
                pdfDocument.Close();

            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult InvoiceMobile_Karyon(string OrderId)
        {

            if (OrderId != null || OrderId != "")
            {
                string msg = "Please see the attached invoice";
                HtmlToPdf htmlToPdf = new HtmlToPdf();
                //htmlToPdf.Options.MarginBottom = 5;
                //htmlToPdf.Options.MarginTop = 5;
                //htmlToPdf.Options.MarginLeft = 15;
                //htmlToPdf.Options.MarginRight = 15;
                PdfDocument pdfDocument = htmlToPdf.ConvertUrl("http://LifeOne.com" + "/Admin/AllInvoice/Invoice_Karyon?orderId=" + OrderId);
                //PdfDocument pdfDocument = htmlToPdf.ConvertUrl("http://LifeOne.com" + "/Admin/ManageAssociateOrdered/Invoice_Karyon?orderId=" + OrderId);
                string guid = Guid.NewGuid().ToString().Substring(0, 7);
                pdfDocument.Save(HttpContext.ApplicationInstance.Response, false, "Invoice_" + guid + ".pdf");
                pdfDocument.Close();

            }
            return View();
        }

        [AllowAnonymous]
        public JsonResult _GetCropFullDetais(string CropCode, int? FKMemid, int? PK_OrderId, string LanguageCode)
        {
            List<_AgricultureMasterModel> obj = _Service._GetCropDetailsByCropCode(CropCode, FKMemid, PK_OrderId ?? 0, LanguageCode);
            return Json(obj);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ViewCropDetails(string CropCode, int? FKMemid, int? PK_OrderId, string LanguageCode)
        {
            List<_AgricultureMasterModel> Obj = _Service._GetCropDetailsByCropCode(CropCode, FKMemid, PK_OrderId ?? 0, LanguageCode);
            return View(Obj);
        }
        [AllowAnonymous]
        public void CropDetailsPDF(string CropCode, int? FKMemid, int? PK_OrderId, string LanguageCode)
        {

            if (!string.IsNullOrEmpty(CropCode))
            {
                string msg = "Please see the attached invoice";
                HtmlToPdf htmlToPdf = new HtmlToPdf();
                //htmlToPdf.Options.MarginBottom = 5;
                //htmlToPdf.Options.MarginTop = 5;
                //htmlToPdf.Options.MarginLeft = 15;
                //htmlToPdf.Options.MarginRight = 15;
                //PdfDocument pdfDocument = htmlToPdf.ConvertUrl("https://localhost:44380" + "/_GetCropFullDetaisAdmin/ManageAssociateOrdered/Invoice?orderId=" + OrderId);
                PdfDocument pdfDocument = new PdfDocument();
                if (HttpContext.Request.Url.Host.ToLower() == "localhost")
                {
                    // pdfDocument = htmlToPdf.ConvertUrl("http://LifeOne.com/admin/AllInvoice/ViewCropDetails?CropCode=" + CropCode + "&FKMemid=" + FKMemid + "&PK_OrderId=" + PK_OrderId + "&LanguageCode=" + LanguageCode);
                    //var ss = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes("http://LifeOne.com/admin/AllInvoice/ViewCropDetails?CropCode=" + CropCode + "&FKMemid=" + FKMemid + "&PK_OrderId=" + PK_OrderId + "&LanguageCode=" + LanguageCode)));
                    var ss = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes("https://localhost:44380/admin/AllInvoice/ViewCropDetails?CropCode=" + CropCode + "&FKMemid=" + FKMemid + "&PK_OrderId=" + PK_OrderId + "&LanguageCode=" + LanguageCode)));
                    pdfDocument = htmlToPdf.ConvertUrl(ss);
                }
                else
                {
                    //pdfDocument = htmlToPdf.ConvertUrl("http://LifeOne.com" + "/admin/AllInvoice/ViewCropDetails?CropCode=" + CropCode + "&FKMemid=" + FKMemid + "&PK_OrderId=" + PK_OrderId + "&LanguageCode=" + LanguageCode);
                    var ss = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes("http://LifeOne.com/admin/AllInvoice/ViewCropDetails?CropCode=" + CropCode + "&FKMemid=" + FKMemid + "&PK_OrderId=" + PK_OrderId + "&LanguageCode=" + LanguageCode)));
                    // var ss = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes("https://localhost:44380/admin/AllInvoice/ViewCropDetails?CropCode=" + CropCode + "&FKMemid=" + FKMemid + "&PK_OrderId=" + PK_OrderId + "&LanguageCode=" + LanguageCode)));
                    pdfDocument = htmlToPdf.ConvertUrl(ss);
                }
                string guid = Guid.NewGuid().ToString().Substring(0, 7);
                pdfDocument.Save(HttpContext.ApplicationInstance.Response, false, "Crop" + CropCode + guid + ".pdf");
                pdfDocument.Close();
            }
        }
    }
}