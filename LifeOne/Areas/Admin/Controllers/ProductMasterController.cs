using ClosedXML.Excel;
using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
  
    [SessionTimeoutAttributeAdmin]
    public class ProductMasterController : Controller
    {
        // GET: Admin/ProductMaster
        AdminProductMasterService _objService = new AdminProductMasterService();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProductMasterList()
        {

            if (!PermissionManager.IsActionPermit("Product Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            MAdminProductMaster obj = new MAdminProductMaster();
            List<MAdminProductMaster> objlst = new List<MAdminProductMaster>();
            try
            {

                objlst = _objService.ProductMasterGetService(obj);
                if (objlst != null) 
                {
                    obj.ProductMasterList = objlst;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult ProductMaster(string id)
        {
            if (!PermissionManager.IsActionPermit("Product Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminProductMaster obj = new MAdminProductMaster();
            if (id != null)
            {
                obj.Pk_ProductId = id;
                try
                {
                    obj = _objService.ProductMasterEditService(obj);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
            ViewBag.ddProductType = DALBindCommonDropdown.BindDropdown(2, 0);
            ViewBag.ddUnit = DALBindCommonDropdown.BindDropdown(3, 0);
            ViewBag.ddStatus = DALBindCommonDropdown.BindDropdown(4, 0);
            ViewBag.SupplierName = DALBindCommonDropdown.BindDropdown(17, 0);
            return View(obj);
        }

        [HttpPost]
        public ActionResult ProductMaster(MAdminProductMaster obj)
        {
            if (!PermissionManager.IsActionPermit("Product Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseMaster _result = new ResponseMaster();
            try
            {
                if (obj.file != null && obj.file.ContentLength > 0)
                {
                    string ProductImage = obj.file.FileName;
                    var ImagePath = "~/Images/ProductImages/";
                    var extension = Path.GetExtension(ProductImage);
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + ProductImage;
                    obj.ProductImage = ImagePath + fileName;
                    
                    var filename = Path.Combine(Server.MapPath(ImagePath), fileName);
                    if (!Directory.Exists(Server.MapPath(ImagePath)))
                    {
                        Directory.CreateDirectory((Server.MapPath(ImagePath)));
                    }
                    obj.file.SaveAs(filename);
                    
                }
                //obj.ProductImage = Session["ProductImage"].ToString();
                _result = _objService.ProductMasterSaveService(obj);
                if (_result != null)
                {

                    TempData["code"] = _result.Code.ToString();
                    TempData["msg"] = _result.Msg.ToString();
                    return Redirect("/ProductMasterList");
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
            ViewBag.ddProductType = DALBindCommonDropdown.BindDropdown(2, 0);
            ViewBag.ddUnit = DALBindCommonDropdown.BindDropdown(3, 0);
            ViewBag.ddStatus = DALBindCommonDropdown.BindDropdown(4, 0);
            return View();
        }

         //[HttpPost]
        //public JsonResult Uploadfile(HttpPostedFileBase file, string subdir)
        //{
        //    var objres = (object)null;
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        var ImagePath = "~/ReadWriteData/" + subdir + "/";
        //        var fileName = Path.GetFileName(file.FileName);
        //        var extension = Path.GetExtension(file.FileName);
        //        if (extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".BMP" || extension.ToUpper() == ".PDF" )
        //        {
        //            if (!Directory.Exists(Server.MapPath(ImagePath)))
        //            {
        //                Directory.CreateDirectory((Server.MapPath(ImagePath)));
        //            }
        //            //Guid.NewGuid().ToString().Replace("_", "") + "_" + InputFileName
        //            fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;


        //            file.SaveAs(Path.Combine(Server.MapPath(ImagePath), fileName));
        //            objres = new { Msg = "file upload successfully!", Path = ImagePath + "" + fileName, result = "success" };
        //            Session["ProductImage"] = ImagePath+" "+ fileName;
        //        }
        //        else
        //        {
        //            objres = new { Msg = "Only jpg,jpeg,png,bmp,pdf,docx,xlsx files are allow !", result = "warning" };
        //        }
        //    }
        //    else
        //    {
        //        objres = new { Msg = "Please select file to upload!", result = "warning" };
        //    }
        //    return Json(objres, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public ActionResult ProductMasterDelete(string id)
        {
            if (!PermissionManager.IsActionPermit("Product Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseMaster obj = new ResponseMaster();  
            try
            {
                obj = _objService.ProductMasterDeleteService(id);
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
 
            return Redirect("/ProductMasterList");

        }

        public ActionResult ExportToExcelProductMaster(MAdminProductMaster obj)
        {
            List<MAdminProductMaster> objlst = new List<MAdminProductMaster>();
            DataTable dt = new DataTable("Product List");
            dt.Columns.AddRange(new DataColumn[10] {
                                                     new DataColumn("Product Name"),
                                                     new DataColumn("Category"),
                                                     new DataColumn("MRP"),
                                                     new DataColumn("Sale Price"),
                                                     new DataColumn("Single PV"),
                                                     new DataColumn("Box PV"),
                                                       new DataColumn("Box Qty"),
                                                         new DataColumn("Unit"),
                                                     new DataColumn("CGST/IGST/SGST"),
                                                       new DataColumn("Status")
            });
            var customers = _objService.ProductMasterGetService(obj);
            objlst = customers;
            if (objlst.Count > 0)
            {
                foreach (var customer in objlst)
                {
                    dt.Rows.Add(customer.ProductName, customer.ProductType, customer.MRP, customer.SalesPrice, customer.PV, customer.BoxPV, customer.BoxQty,customer.Unit ,customer.CGST+"/"+customer.IGST+"/"+ customer.SGST,customer.Status);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Product List.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/ProductMasterList");
            }
            // return View();
        }


    }
}