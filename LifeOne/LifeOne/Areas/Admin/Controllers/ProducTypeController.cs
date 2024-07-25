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

namespace LifeOne.Areas.Admin.Controllers
{
  
    [SessionTimeoutAttributeAdmin]
    public class ProducTypeController : Controller
    {
        // GET: Admin/ProducType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductType(MAdminProductType model)
        {
            if (!PermissionManager.IsActionPermit("Product Type", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }


            MAdminProductType _result = new MAdminProductType();
            try
            {
                model.ProcId = 1;
                ProductTypeService _objService = new ProductTypeService();
               
                _result.objList = _objService.GetProductTypeService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult ProductTypeSave(MAdminProductType model)
        {
            if (!PermissionManager.IsActionPermit("Product Type", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseProductTypeModel _result = new ResponseProductTypeModel();
            try
            {
                if (model.Pk_ProductTypeId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 1; }
                ProductTypeService _objService = new ProductTypeService();
                if (model.file != null && model.file.ContentLength > 0)
                {
                    string ProductImage = model.file.FileName;
                    var ImagePath = "~/Images/ProductImages/";
                    var extension = Path.GetExtension(ProductImage);
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + ProductImage;
                    model.ProductTypeImage = ImagePath + fileName;

                    var filename = Path.Combine(Server.MapPath(ImagePath), fileName);
                    if (!Directory.Exists(Server.MapPath(ImagePath)))
                    {
                        Directory.CreateDirectory((Server.MapPath(ImagePath)));
                    }
                    model.file.SaveAs(filename);

                }
                _result = _objService.SaveProductTypeService(model);
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
            return Redirect("/ProductType");
        }

        //[HttpPost]
        public ActionResult ProductTypeDelete(int Id)
        {
            if (!PermissionManager.IsActionPermit("Product Type", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminProductType model = new MAdminProductType();
            model.Pk_ProductTypeId = Id;
            model.ProcId = 4;
            ResponseProductTypeModel _result = new ResponseProductTypeModel();
            try
            {

                ProductTypeService _objService = new ProductTypeService();

                _result = _objService.SaveProductTypeService(model);
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
            return Redirect("/ProductType");
        }

       
    }
}