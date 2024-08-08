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
using System.Data;
using ClosedXML.Excel;
using System.IO;
using LifeOne.Models.API.DAL;
using LifeOne.Models.API;
using LifeOne.Models.Manager;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models;
using static LifeOne.Models.ShoppingRequest;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.EMMA;

namespace LifeOne.Areas.Admin.Controllers
{

    [SessionTimeoutAttributeAdmin]
    public class MasterController : Controller
    {
        AdminProductMasterService _objService = new AdminProductMasterService();
        // GET: Admin/Master
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Utility(MAdminUntility model)
        {
            if (!PermissionManager.IsActionPermit("Utility Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminUntility _result = new MAdminUntility();
            try
            {
                model.ProcId = 1;
                UtilityService _objService = new UtilityService();

                _result.objList = _objService.GetUtilityService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult UtilitySave(MAdminUntility model)
        {
            if (!PermissionManager.IsActionPermit("Utility Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseUtilityModel _result = new ResponseUtilityModel();
            try
            {
                if (model.Pk_UtilityId != 0)
                { model.ProcId = 3; }
                else
                { model.ProcId = 2; }
                UtilityService _objService = new UtilityService();

                _result = _objService.SaveUtilityService(model);
                if (_result != null)
                {

                    TempData["code"] = _result.Flag.ToString();
                    TempData["msg"] = _result.Msg.ToString();
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
            return Redirect("/Utility");
        }
        public ActionResult UtilityDelete(string Id)
        {
            if (!PermissionManager.IsActionPermit("Utility Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminUntility model = new MAdminUntility();
            model.Pk_UtilityId = Convert.ToInt32(Id);
            model.ProcId = 4;
            ResponseUtilityModel _result = new ResponseUtilityModel();
            try
            {

                UtilityService _objService = new UtilityService();

                _result = _objService.SaveUtilityService(model);
                if (_result != null)
                {

                    TempData["code"] = _result.Flag.ToString();
                    TempData["msg"] = _result.Msg.ToString();
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
            return Redirect("/Utility");
        }
        public ActionResult UtilityMaster()
        {
            if (!PermissionManager.IsActionPermit("Manage App Dashboard", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }
        public JsonResult BindAllUtilityMaster()
        {
            return Json(UtilityService.BindAllUtilityMaster());
        }
        public JsonResult ChangeUtilityMasterStatus(DynamicUtilityModel req)
        {
            return Json(UtilityService.ChangeUtilityMasterStatus(req));
        }

        #region CategoryMaster

        public ActionResult Category()
        {
            return View();
        }

        public JsonResult BindCategoryList(CategoryModel model)
        {
            return Json(ProductService.BindCategoryList(model), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region KaryonProducts_Master

        [HttpGet]
        public ActionResult Karyon_ProductMaster(string id)
        {
            ViewBag.ddUnit = DALBindCommonDropdown.BindDropdown(3, 0);
            return View();
        }

        [HttpPost]
        public JsonResult Karyon_ProductMaster(SaveKaryonProductModel obj)
        {
            CustomerDb objcusdb = new CustomerDb();
            MemberLanguageResponse res = new MemberLanguageResponse();
            try
            {
                string xml = "<result>";
                if (obj.ProductOptionsList.Count > 0)
                {
                    foreach (var value in obj.ProductOptionsList)
                    {
                        xml += "<data>" +
                        "<MagentoProdId>" + obj.ProductDetails.Id + "</MagentoProdId>" +
                        "<ProductCode>" + obj.ProductDetails.ProductSKU + "</ProductCode>" +
                        "<ProductSKU>" + obj.ProductDetails.ProductSKU + "</ProductSKU>" +
                        "<ProductName>" + obj.ProductDetails.ProductName + "</ProductName>" +
                        "<ProductImage>" + obj.ProductDetails.ProductImage + "</ProductImage>" +
                        "<MRP>" + obj.ProductDetails.MRP + "</MRP>" +
                        "<SalePrice>" + obj.ProductDetails.SalesPrice + "</SalePrice>" +
                        "<PV>" + 0 + "</PV>" +
                        "<Unit>" + obj.ProductDetails.Unit + "</Unit>" +
                        "<OptionId>" + 0 + "</OptionId>" +
                        "<OptionTypeId>" + 0 + "</OptionTypeId>" +
                        "<Price>" + value.Price + "</Price>" +
                        "<size>" + value.Size + "</size>" +
                        "<CGST>" + obj.ProductDetails.GST / 2 + "</CGST>" +
                        "<SGST>" + obj.ProductDetails.GST / 2 + "</SGST>" +
                        "<IGST>" + obj.ProductDetails.GST + "</IGST>" +
                        "<Fk_OrbitType>" + obj.ProductDetails.FK_OrbitType + "</Fk_OrbitType>" +
                        "<DP> " + value.DP + " </DP> " +
                        "<BV>" + value.PV + "</BV>" +
                        "<Bottles>" + value.BoxSize + "</Bottles>" +
                        "<ProductDescription>" + obj.ProductDetails.ProductDescription + "</ProductDescription>";
                        xml += "</data>";
                    }
                }
                xml += "</result>";
                res = objcusdb.AddProductList(xml);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetKaryon_ProductDetail(string id)
        {
            CustomerDb objcusdb = new CustomerDb();
            MemberLanguageResponse res = new MemberLanguageResponse();
            MAdminProductMaster obj = new MAdminProductMaster();
            SaveKaryonProductModel model = new SaveKaryonProductModel();
            try
            {
                if (id != null)
                {
                    obj.Pk_ProductId = id;
                    try
                    {
                        List<ProductViewModel> obj1 = ProductService.Karyon_ProductMasterEditService(obj);
                        model = obj1.GroupBy(x => x.ProductId).Select(x => new SaveKaryonProductModel
                        {
                            ProductDetails = x.Select(y => new KaryonProductModel
                            {
                                Id = x.FirstOrDefault().ProductId,
                                ProductName = x.FirstOrDefault().ProductName,
                                ProductSKU = x.FirstOrDefault().ProductSKU,
                                ProductImage = x.FirstOrDefault().ProductImage,
                                MRP = x.FirstOrDefault().MRP,
                                GST = x.FirstOrDefault().GST,
                                ProductDescription = x.FirstOrDefault().ProductDescription,
                                FK_OrbitType = x.FirstOrDefault().FK_OrbitType,
                                SalesPrice = x.FirstOrDefault().SalesPrice,
                                Unit = x.FirstOrDefault().Units,
                            }).FirstOrDefault(),
                            ProductOptionsList = x.Select(z => new KaryonProductOptionModel
                            {
                                BoxSize = z.BoxSize,
                                DP = z.DP,
                                Price = z.Price,
                                Size = z.Size,
                                PV = z.PV

                            }).ToList()
                        }).FirstOrDefault();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Karyon_ProductMasterDelete(string id)
        {
            ResponseMaster obj = new ResponseMaster();
            try
            {
                obj = ProductService.Karyon_ProductMasterDeleteService(id);
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

            return Redirect("/Product-List");

        }

        public ActionResult ExportToExcelKaryon_ProductMaster(MAdminProductMaster obj)
        {
            List<MAdminProductMaster> objlst = new List<MAdminProductMaster>();
            DataTable dt = new DataTable("ProductList");
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
            var customers = ProductService.Karyon_ProductMasterGetService(obj);
            objlst = customers;
            if (objlst.Count > 0)
            {
                foreach (var customer in objlst)
                {
                    dt.Rows.Add(customer.ProductName, customer.ProductType, customer.MRP, customer.SalesPrice, customer.PV, customer.BoxPV, customer.BoxQty, customer.Unit, customer.CGST + "/" + customer.IGST + "/" + customer.SGST, customer.Status);

                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductList.xlsx");
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

        [HttpPost]
        public string UploadProduct(HttpPostedFileBase file)
        {
            string str_image = "";
            string pathToSave_100 = "";
            try
            {
                var fileContent = Request.Files["image"];
                file = fileContent;
                string fileName = file.FileName;
                fileName = SessionManager.Fk_MemId + DateTime.Now.Year.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                string fileExtension = file.ContentType;
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    fileExtension = Path.GetExtension(file.FileName);
                    str_image = "/ReadWriteData/ProductImages/" + fileName + fileExtension;
                    pathToSave_100 = Server.MapPath(str_image);
                    file.SaveAs(pathToSave_100);
                }
                return str_image;
            }
            catch (Exception ex)
            {
                return str_image;
            }
        }

        #endregion

        #region KaryonProductsCategory_Master

        public ActionResult CropCategory(CategoryModel model)
        {

            if (!PermissionManager.IsActionPermit("Crop Category", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminUnit _result = new MAdminUnit();
            CategoryModel _objService = new CategoryModel();
            try
            {
                var data = ProductService.BindCategoryList(model);
                if (data.Data != null)
                {
                    _objService.CategoryList = new List<CategoryModel>();
                    foreach (var item in data.Data)
                    {
                        _objService.CategoryList.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(_objService);
        }

        [HttpPost]
        public ActionResult CropCategorySave(CategoryModel model)
        {
            if (!PermissionManager.IsActionPermit("Crop Category", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseUnitModel _result = new ResponseUnitModel();
            try
            {
                if (model.Id != 0)
                { model.ProcID = 2; }
                else
                { model.ProcID = 1; }

                _result = ProductService.SaveCropCategorySave(model);
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
            return Redirect("/Admin-CropCategory");
        }


        public ActionResult CropCategoryDelete(int Id)
        {
            if (!PermissionManager.IsActionPermit("Crop Category", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            CategoryModel model = new CategoryModel();
            model.Id = Id;
            model.ProcID = 3;
            ResponseUnitModel _result = new ResponseUnitModel();
            try
            {
                _result = ProductService.SaveCropCategorySave(model);
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
            return Redirect("/Admin-CropCategory");
        }

        #endregion


        #region KaryonProducts_Master

        public ActionResult CropProduct(CropProductModel model)
        {
            List<SelectListItem> CategoryList = new List<SelectListItem>();
            try
            {
                var data = ProductService.BindCategoryList(new CategoryModel { });
                if (data.Data != null)
                {
                    foreach (var item in data.Data)
                    {
                        CategoryList.Add(new SelectListItem { Text = item.CropName, Value = Convert.ToString(item.Id) });
                    }
                    ViewBag.DllCategoryList = CategoryList;
                }
                model = ProductService.BindCropProduct(model);
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveCropProduct(CropProductModel model)
        {
            ResponseUnitModel _result = new ResponseUnitModel();
            List<SelectListItem> CategoryList = new List<SelectListItem>();
            try
            {
                if (model.Id != 0)
                { model.Crop_ProdId = 1; }
                else
                { model.Crop_ProdId = 2; }

                _result = ProductService.SaveCropProduct(model);
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
            var data = ProductService.BindCategoryList(new CategoryModel { });
            if (data.Data != null)
            {
                foreach (var item in data.Data)
                {
                    CategoryList.Add(new SelectListItem { Text = item.CropName, Value = Convert.ToString(item.Id) });
                }
                ViewBag.DllCategoryList = CategoryList;
            }
            return Redirect("/PerformanceLevelMaster");
        }


        public ActionResult DeleteCropProduct(int Id)
        {
            CropProductModel model = new CropProductModel();
            model.Id = Id;
            model.Crop_ProdId = 4;
            ResponseUnitModel _result = new ResponseUnitModel();
            try
            {
                _result = ProductService.DeleteCropProduct(model);
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
            return Redirect("/Admin-CropCategory");
        }

        #endregion


        public JsonResult ChangeCMDStartDateTime(DynamicUtilityModel req)
        {
            return Json(UtilityService.ChangeCMDStartDateTime(req));
        }    
        #region Crop Product SubCategory

        public ActionResult CropSubCategory()
        {

            if (!PermissionManager.IsActionPermit("Crop SubCategory", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            SubCategoryViewModel _objService = new SubCategoryViewModel();
            var data = ProductService.BindCropSubCategory(_objService);
            var ddCategoryList = new List<SelectListItem>();
            ddCategoryList.Add(new SelectListItem { Text = "Select Category", Value = "" });
            ddCategoryList.AddRange(LifeOne.Models.Common.DBHelperDapper.DAGetDetailsInList<SelectListItem>("exec Proc_BindDropDownCropCategory"));
            ViewBag.ddCategoryList = ddCategoryList;
            return View(data);
        }

        [HttpPost]
        public ActionResult CropSubCategory(SubCategoryViewModel model)
        {
            if (!PermissionManager.IsActionPermit("Crop SubCategory", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseUnitModel _result = new ResponseUnitModel();
            try
            {
                if (model.Id != null && model.Id > 0)
                    model.ProcID = 2;
                else
                    model.ProcID = 1;

                _result = ProductService.SaveCropSubCategorySave(model);
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
            return Redirect("/Admin-CropSubCategory");
        }
        public ActionResult CropSubCategoryDelete(int Id)
        {
            if (!PermissionManager.IsActionPermit("Crop SubCategory", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            SubCategoryViewModel model = new SubCategoryViewModel();
            model.Id = Id;
            model.ProcID = 3;
            ResponseUnitModel _result = new ResponseUnitModel();
            try
            {
                _result = ProductService.SaveCropSubCategorySave(model);
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
            return Redirect("/Admin-CropSubCategory");
        }
        #endregion



        public ActionResult TopupByAdmin()
        {
            if (!PermissionManager.IsActionPermit("Topup", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }   
            Models.AssociateManagement.AssociateEntity.PackageMaster para = new Models.AssociateManagement.AssociateEntity.PackageMaster();
            ViewBag.ddlPackageList = Models.AssociateManagement.AssociateEntity.BindPckageMaster.BindPackageMasterAdmin();
            return View();
        }

        [HttpPost]
        public ActionResult TopUpByAdmin(Models.AssociateManagement.AssociateEntity.MemberTopup obj)
        {

            Models.AssociateManagement.AssociateEntity.MemberTopup para = new Models.AssociateManagement.AssociateEntity.MemberTopup();

            para.FK_MemId = obj.FK_MemId;
            para.FK_PackageId = obj.FK_PackageId;
            para.PaidAmount = obj.PaidAmount;
            para.UpdatedBy = SessionManager.Fk_MemId.ToString();
            para.BankName = string.IsNullOrEmpty(obj.BankName) ? null : (obj.BankName);
            para.BankBranch = string.IsNullOrEmpty(obj.BankBranch) ? null : (obj.BankBranch);
            para.ChequeDDNo = string.IsNullOrEmpty(obj.ChequeDDNo) ? null : (obj.ChequeDDNo);
            para.ChequeDDDate = string.IsNullOrEmpty(obj.ChequeDDDate) ? null : DALCommon.ConvertToSystemDate(obj.ChequeDDDate, "dd/MM/yyyy");
            //para.TopupDate = string.IsNullOrEmpty(obj.TopupDate) ? null : BLCommon.ConvertToSystemDate(obj.TopupDate, "dd/MM/yyyy");
            para.PaymentMode = obj.PaymentMode;

            DataTable dt = obj.TopUpMemberByAdmin(para);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Msg"].ToString() == "1")
                {
                    ViewBag.Alert = "You are upgraded successfully";
                }
                else if (dt.Rows[0]["Msg"].ToString() == "0")
                {
                    ViewBag.Alert = dt.Rows[0]["Error"].ToString();
                }
                else
                {
                    ViewBag.Alert = "Error!!!";
                }
            }
            else
            {
                ViewBag.Alert = "Error!!!";
            }
            ViewBag.ddlPackageList = Models.AssociateManagement.AssociateEntity.BindPckageMaster.BindPackageMasterAdmin();

            return View();
        }
        [HttpGet]
        public ActionResult ChangeWebSitePopup()
        {
            if (!PermissionManager.IsActionPermit("Change WebSite Popup", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            WebSitePopup model = new WebSitePopup();
            model.ActiveType = "Select";
            ViewBag.WebSitePopup = ProductService.ChangeWebSitePopup(model);

            return View(model);
        }
        [HttpPost]
        public ActionResult ChangeWebSitePopup(WebSitePopup model, HttpPostedFileBase file)
        {
            if (!PermissionManager.IsActionPermit("Change WebSite Popup", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            Random rnd = new Random();
            string path = "";
            if (file != null)
            {
                string pic = rnd.Next(1000000).ToString() + System.IO.Path.GetFileName(file.FileName);

                path = System.IO.Path.Combine(
                 Server.MapPath("~/Images/Users/Popup"), pic);

                file.SaveAs(path);

                model.ImageUrl = "~/Images/Users/Popup/" + pic;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }

            model.ActiveType = "Update";
            WebSitePopup status = ProductService.UpdateWebSitePopup(model);

            return RedirectToAction("ChangeWebSitePopup");
        }


        [HttpGet]
        public ActionResult BonanzaMasterList()
        {
            if (!PermissionManager.IsActionPermit("Bonanza Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            ModelBonanzaMaster model = new ModelBonanzaMaster();
            List<ModelBonanzaMaster> objList = DALProductServices.BonanzaMasterDetailList(model);
            TempData["Code"] = "";
            TempData["Remark"] = "";
            return View(objList);
        }



        [HttpGet]
        public ActionResult AddNewBonanza()
        {
            if (!PermissionManager.IsActionPermit("Bonanza Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ModelBonanzaMaster model = new ModelBonanzaMaster();

            return View(model);
        }
        [HttpPost]
        public ActionResult AddNewBonanza(ModelBonanzaMaster model, HttpPostedFileBase file)
        {

            Random rnd = new Random();
            string path = "";
            if (file != null)
            {
                string pic = rnd.Next(1000000).ToString() + System.IO.Path.GetFileName(file.FileName);

                path = System.IO.Path.Combine(
                 Server.MapPath("~/Images/Users/Reward"), pic);

                file.SaveAs(path);
                model.Image = "~/Images/Users/Reward/" + pic;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            model.CreatedBy = SessionManager.Fk_MemId.ToString();
            ModelBonanzaMaster modelresult = DALProductServices.SaveBonanza(model);



            if (modelresult.Code == "1")
            {
                TempData["Code"] = modelresult.Code;
                TempData["Remark"] = modelresult.Remark;
                return RedirectToAction("BonanzaMasterList", "Master");
            }



            return View(model);
        }


        [HttpGet]
        public ActionResult UpdateBonanzamaster(string Id)
        {
            if (!PermissionManager.IsActionPermit("Bonanza Master", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            ModelBonanzaMaster model = new ModelBonanzaMaster();
            model.Pk_BonanzaId = Convert.ToInt32(Id);
            ModelBonanzaMaster objList = DALProductServices.BonanzaMasterDetailById(model);
            return View(objList);
        }


        [HttpPost]
        public ActionResult UpdateBonanzamaster(ModelBonanzaMaster model, HttpPostedFileBase file)
        {
            Random rnd = new Random();
            string path = "";
            if (file != null)
            {
                string pic = rnd.Next(1000000).ToString() + System.IO.Path.GetFileName(file.FileName);

                path = System.IO.Path.Combine(
                 Server.MapPath("~/Images/Users/Reward"), pic);

                file.SaveAs(path);

                model.Image = "~/Images/Users/Reward/" + pic;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            model.CreatedBy = SessionManager.Fk_MemId.ToString();

            ModelBonanzaMaster objList = DALProductServices.UpdateBonanzaMaster(model);

            return RedirectToAction("BonanzaMasterList", "master");
        }

        [HttpGet]
        public ActionResult DeleteBonanzaMaster(string Id)
        {
            if (!PermissionManager.IsActionPermit("Bonanza Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            ModelBonanzaMaster Bmodel = new ModelBonanzaMaster();
            Bmodel.Pk_BonanzaId = Convert.ToInt32(Id);

            ModelBonanzaMaster modelresult = DALProductServices.DeleteBonanzaMaster(Bmodel);
            if (modelresult.Code == "1")
            {
                TempData["Code"] = modelresult.Code;
                TempData["Remark"] = modelresult.Remark;
                return RedirectToAction("BonanzaMasterList", "Master");
            }

            return View(modelresult);
        }



        [HttpGet]
        public ActionResult BonanzaDetail()
        {
            if (!PermissionManager.IsActionPermit("Bonanza Detail", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            ModelBonanzaDetail model = new ModelBonanzaDetail();

            List<ModelBonanzaDetail> objList = DALProductServices.BonanzaDetailList(model);




            return View(objList);
        }
        [HttpGet]
        public ActionResult AddBonanzaDetail()
        {
            if (!PermissionManager.IsActionPermit("Bonanza Detail", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }

            ModelBonanzaDetail model = new ModelBonanzaDetail();
            ModelBonanzaMaster model1 = new ModelBonanzaMaster();
            ViewBag.BonanzaType = DALProductServices.BonanzaMasterDetailList(model1);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddBonanzaDetail(ModelBonanzaDetail model, HttpPostedFileBase file)
        {

            Random rnd = new Random();
            string path = "";
            if (file != null)
            {
                string pic = rnd.Next(1000000).ToString() + System.IO.Path.GetFileName(file.FileName);

                path = System.IO.Path.Combine(
                 Server.MapPath("~/Images/Users/Reward"), pic);

                file.SaveAs(path);

                model.BonanzaDetailImage = "~/Images/Users/Reward/" + pic;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            model.CreatedBy = SessionManager.Fk_MemId.ToString();
            ModelBonanzaDetail modelresult = DALProductServices.SaveBonanzaDetail(model);


            if (modelresult.Code == "1")
            {
                TempData["Code"] = modelresult.Code;
                TempData["Remark"] = modelresult.Remark;
                return RedirectToAction("BonanzaDetail", "Master");
            }

            ModelBonanzaMaster model1 = new ModelBonanzaMaster();
            ViewBag.BonanzaType = DALProductServices.BonanzaMasterDetailList(model1);

            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteBonanzaDetail(string Id)
        {
            if (!PermissionManager.IsActionPermit("Bonanza Detail", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }

            ModelBonanzaMaster Bmodel = new ModelBonanzaMaster();
            Bmodel.Pk_BonanzaId = Convert.ToInt32(Id);

            ModelBonanzaMaster modelresult = DALProductServices.DeleteBonanzaDetail(Bmodel);
            if (modelresult.Code == "1")
            {
                TempData["Code"] = modelresult.Code;
                TempData["Remark"] = modelresult.Remark;
                return RedirectToAction("BonanzaDetail", "Master");
            }


            return View(modelresult);
        }


        [HttpGet]
        public ActionResult UpdateBonanzaDetail(string Id)
        {
            if (!PermissionManager.IsActionPermit("Bonanza Detail", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }

            ModelBonanzaDetail model = new ModelBonanzaDetail();
            model.Pk_BonanzaId = Convert.ToInt32(Id);
            ModelBonanzaDetail objList = DALProductServices.BonanzaDetailListById(model);
            ModelBonanzaMaster model1 = new ModelBonanzaMaster();
            ViewBag.BonanzaType = DALProductServices.BonanzaMasterDetailList(model1);
            return View(objList);
        }


        [HttpPost]
        public ActionResult UpdateBonanzaDetail(ModelBonanzaDetail model, HttpPostedFileBase file)
        {
            Random rnd = new Random();
            string path = "";
            if (file != null)
            {
                string pic = rnd.Next(1000000).ToString() + System.IO.Path.GetFileName(file.FileName);

                path = System.IO.Path.Combine(
                 Server.MapPath("~/Images/Users/Reward"), pic);

                file.SaveAs(path);

                model.BonanzaDetailImage = "~/Images/Users/Reward/" + pic;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            model.CreatedBy = SessionManager.Fk_MemId.ToString();

            ModelBonanzaDetail objList = DALProductServices.UpdateBonanzaDetails(model);

            return RedirectToAction("BonanzaDetail", "master");
        }



        [HttpGet]
        public ActionResult RewardMaster()
        {
            if (!PermissionManager.IsActionPermit("Reward Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            ModelRewardMaster model = new ModelRewardMaster();
            List<ModelRewardMaster> objList = DALProductServices.RewardMasterDetailList(model);
            return View(objList);
        }

        [HttpGet]
        public ActionResult AddNewReward()
        {
            if (!PermissionManager.IsActionPermit("Reward Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ModelRewardMaster model = new ModelRewardMaster();

            return View(model);
        }
        [HttpPost]
        public ActionResult AddNewReward(ModelRewardMaster model, HttpPostedFileBase file)
        {

            Random rnd = new Random();
            string path = "";
            if (file != null)
            {
                string pic = rnd.Next(1000000).ToString() + System.IO.Path.GetFileName(file.FileName);

                path = System.IO.Path.Combine(
                 Server.MapPath("~/Images/Users/Reward"), pic);

                file.SaveAs(path);

                model.Image = "~/Images/Users/Reward/" + pic;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            model.CreatedBy = SessionManager.Fk_MemId.ToString();
            ModelRewardMaster modelresult = DALProductServices.Savereward(model);



            if (modelresult.Code == "1")
            {
                TempData["Code"] = modelresult.Code;
                TempData["Remark"] = modelresult.Remark;
                return RedirectToAction("RewardMaster", "Master");
            }



            return View(model);
        }

        [HttpGet]
        public ActionResult UpdateRewardMaster(string Id)
        {
            if (!PermissionManager.IsActionPermit("Reward Master", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }

            ModelRewardMaster model = new ModelRewardMaster();
            model.Pk_RewardId = Convert.ToInt32(Id);
            ModelRewardMaster objList = DALProductServices.RewardMasterDetailListByid(model);

            return View(objList);
        }


        [HttpPost]
        public ActionResult UpdateRewardMaster(ModelRewardMaster model, HttpPostedFileBase file)
        {
            Random rnd = new Random();
            string path = "";
            if (file != null)
            {
                string pic = rnd.Next(1000000).ToString() + System.IO.Path.GetFileName(file.FileName);

                path = System.IO.Path.Combine(
                 Server.MapPath("~/Images/Users/Reward"), pic);

                file.SaveAs(path);

                model.Image = "~/Images/Users/Reward/" + pic;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            model.CreatedBy = SessionManager.Fk_MemId.ToString();

            ModelRewardMaster objList = DALProductServices.Updatereward(model);

            return RedirectToAction("RewardMaster", "master");
        }


        [HttpGet]
        public ActionResult RewardDetail()
        {
            if (!PermissionManager.IsActionPermit("Reward Detail", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            ModelRewardDetail model = new ModelRewardDetail();

            List<ModelRewardDetail> objList = DALProductServices.RewardDetailList(model);




            return View(objList);
        }
        [HttpGet]
        public ActionResult AddRewardDetail()
        {
            if (!PermissionManager.IsActionPermit("Reward Detail", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ModelRewardDetail model = new ModelRewardDetail();
            ModelRewardMaster model1 = new ModelRewardMaster();
            ViewBag.RewardType = DALProductServices.RewardMasterDetailList(model1);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddRewardDetail(ModelRewardDetail model, HttpPostedFileBase file)
        {

            Random rnd = new Random();
            string path = "";
            if (file != null)
            {
                string pic = rnd.Next(1000000).ToString() + System.IO.Path.GetFileName(file.FileName);

                path = System.IO.Path.Combine(
                 Server.MapPath("~/Images/Users/Reward"), pic);

                file.SaveAs(path);

                model.RewardDetailImage = "~/Images/Users/Reward/" + pic;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            model.CreatedBy = SessionManager.Fk_MemId.ToString();
            ModelRewardDetail modelresult = DALProductServices.SaveRewardDetail(model);


            if (modelresult.Code == "1")
            {
                TempData["Code"] = modelresult.Code;
                TempData["Remark"] = modelresult.Remark;
                return RedirectToAction("RewardDetail", "Master");
            }

            ModelRewardMaster model1 = new ModelRewardMaster();
            ViewBag.RewardType = DALProductServices.RewardMasterDetailList(model1);

            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteRewardDetail(string Id)
        {
            if (!PermissionManager.IsActionPermit("Reward Detail", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            ModelRewardMaster Bmodel = new ModelRewardMaster();
            Bmodel.Pk_RewardId = Convert.ToInt32(Id);

            ModelRewardMaster modelresult = DALProductServices.DeleteRewardDetail(Bmodel);
            if (modelresult.Code == "1")
            {
                TempData["Code"] = modelresult.Code;
                TempData["Remark"] = modelresult.Remark;
                return RedirectToAction("RewardDetail", "Master");
            }


            return View(modelresult);
        }
        [HttpGet]
        public ActionResult DeleteRewardMaster(string Id)
        {
            if (!PermissionManager.IsActionPermit("Reward Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            ModelRewardMaster Bmodel = new ModelRewardMaster();
            Bmodel.Pk_RewardId = Convert.ToInt32(Id);

            ModelRewardMaster modelresult = DALProductServices.DeleteRewardMaster(Bmodel);
            if (modelresult.Code == "1")
            {
                TempData["Code"] = modelresult.Code;
                TempData["Remark"] = modelresult.Remark;
                return RedirectToAction("RewardMaster", "Master");
            }

            return View(modelresult);
        }
        [HttpGet]
        public ActionResult UpdateRewardDetail(string Id)
        {

            if (!PermissionManager.IsActionPermit("Reward Detail", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }

            ModelRewardDetail model = new ModelRewardDetail();
            model.Pk_RewardDetailId = Convert.ToInt32(Id);
            ModelRewardDetail objList = DALProductServices.RewardDetailListById(model);
            ModelRewardMaster model1 = new ModelRewardMaster();
            ViewBag.RewardType = DALProductServices.RewardMasterDetailList(model1);
            return View(objList);
        }


        [HttpPost]
        public ActionResult UpdateRewardDetail(ModelRewardDetail model, HttpPostedFileBase file)
        {
            Random rnd = new Random();
            string path = "";
            if (file != null)
            {
                string pic = rnd.Next(1000000).ToString() + System.IO.Path.GetFileName(file.FileName);

                path = System.IO.Path.Combine(
                 Server.MapPath("~/Images/Users/Reward"), pic);

                file.SaveAs(path);

                model.RewardDetailImage = "~/Images/Users/Reward/" + pic;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            model.CreatedBy = SessionManager.Fk_MemId.ToString();

            ModelRewardDetail objList = DALProductServices.UpdateRewardDetails(model);

            return RedirectToAction("RewardDetail", "master");
        }

        public ActionResult SupplierMasterList()
        {

            if (!PermissionManager.IsActionPermit("Supplier Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            try
            {
                ProductsDetail product = new ProductsDetail();
                product.OpCode = 4;
                DataSet dataset = product.GetSupplierDetails();
                product.dtDetails = dataset.Tables[0];
                
                return View(product);

            }
            catch (Exception)
            {
                throw;
            }

        }



        public ActionResult SupplierMaster(string id)
        {
            ProductsDetail product = new ProductsDetail();
            if (id != null)
            {

                product.Pk_SupplierId = id;
                try
                {
                    product.OpCode = 4;
                    DataSet dataset = product.GetSupplierDetails();
                    product.SupplierName = dataset.Tables[0].Rows[0]["SupplierName"].ToString();
                    product.Mobile = dataset.Tables[0].Rows[0]["Mobile"].ToString();
                    product.PinCode = dataset.Tables[0].Rows[0]["PinCode"].ToString();
                    product.Address = dataset.Tables[0].Rows[0]["Address"].ToString();
                    product.GSTNo = dataset.Tables[0].Rows[0]["GSTNo"].ToString();
                    product.dtDetails = dataset.Tables[0];
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult SupplierMaster(ProductsDetail productsDetail, string Save)
        {
            if (!PermissionManager.IsActionPermit("Supplier Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            if (Save == "Save")
            {
                productsDetail.OpCode = 1;
            }
            else
            {
                productsDetail.OpCode = 3;
            }
            productsDetail.Fk_MemId = long.Parse(SessionManager.Fk_MemId.ToString());
            DataSet dataset = productsDetail.GetSupplierDetails();
            if (dataset != null && dataset.Tables[0].Rows.Count > 0)
            {
                if (dataset.Tables[0].Rows[0]["Flag"].ToString() == "1")
                {
                    TempData["msg"] = dataset.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["Flag"] = dataset.Tables[0].Rows[0]["Flag"].ToString();
                }
                else
                {
                    TempData["msg"] = dataset.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["Flag"] = dataset.Tables[0].Rows[0]["Flag"].ToString();
                }

            }
            return RedirectToAction("SupplierMasterList");


        }
        public ActionResult SupplierMasterDelete(string id)
        {
            if (!PermissionManager.IsActionPermit("Supplier Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            ProductsDetail products = new ProductsDetail();
            products.Pk_SupplierId = id;
            products.OpCode = 2;
            products.Fk_MemId = long.Parse(SessionManager.Fk_MemId.ToString());
            DataSet dataSet1 = products.GetSupplierDetails();
            return RedirectToAction("SupplierMasterList");
        }



        public ActionResult PackageMasterList()
        {

            if (!PermissionManager.IsActionPermit("Package Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }


            try
            {
                Reports product = new Reports();
                product.OpCode = 4;
                DataSet dataset = product.PackageMaster();
                product.dtDetails = dataset.Tables[0];
                return View(product);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult PackageMaster(string id)
        {
            Reports product = new Reports();
            try
            {
                ProductsDetail ProductsDetail = new ProductsDetail();
                List<SelectListItem> ddlProduct = new List<SelectListItem>();

                DataSet dataSet = ProductsDetail.GetAllProducts();

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)

                {
                    ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    foreach (DataRow r in dataSet.Tables[0].Rows)
                    {

                        ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });

                    }

                }
                ViewBag.ddlProduct = ddlProduct;
               
           
                if (id != null)
                {
                product.Pk_PackageID = int.Parse(id);

                    product.OpCode = 4;
                    DataSet dataset = product.PackageMaster();
                    product.PackageName = dataset.Tables[0].Rows[0]["PackageName"].ToString();
                    product.PackageAmount = dataset.Tables[0].Rows[0]["PackageAmount"].ToString();
                    product.Price = dataset.Tables[0].Rows[0]["Price"].ToString();
                    product.CGST = decimal.Parse(dataset.Tables[0].Rows[0]["CGST"].ToString());
                    product.SGST = decimal.Parse(dataset.Tables[0].Rows[0]["SGST"].ToString());
                    product.IGST = decimal.Parse(dataset.Tables[0].Rows[0]["IGST"].ToString());
                    product.BP = dataset.Tables[0].Rows[0]["BP"].ToString();
                    product.DP = dataset.Tables[0].Rows[0]["DP"].ToString();
                    product.Rate = dataset.Tables[0].Rows[0]["Rate"].ToString();
                    product.Percentage = Convert.ToDecimal(dataset.Tables[0].Rows[0]["Percentage"].ToString());
                    product.CappingPoints = dataset.Tables[0].Rows[0]["CappingPoints"].ToString();
                    product.IsFixed = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsFixed"]);
                    product.IsDiscount = Convert.ToBoolean(dataset.Tables[0].Rows[0]["IsDiscount"]);
                    product.MinBv = dataset.Tables[0].Rows[0]["MinBV"].ToString();
                    product.dtDetails = dataset.Tables[0];

                }
              
                    product.AddedBy = int.Parse(SessionManager.Fk_MemId.ToString());
                    DataSet dataset1 = product.DeleteTempPackageProductById();
                
            }
            catch (Exception)
            {
                throw;
            }
            
            return View(product);
        }

        [HttpPost]
        public ActionResult PackageMaster(Reports reports, string Save)
        {
             if (!PermissionManager.IsActionPermit("Package Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            if (Save == "Save")
            {
                reports.OpCode = 1;
            }
            else
            {
                reports.OpCode = 3;
            }
            reports.AddedBy = int.Parse(SessionManager.Fk_MemId.ToString());
            DataSet dataset = reports.PackageMaster();
            if (dataset != null && dataset.Tables[0].Rows.Count > 0)
            {
                if (dataset.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["msg"] = dataset.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["Flag"] = dataset.Tables[0].Rows[0]["Flag"].ToString();
                }

            }
            return RedirectToAction("PackageMasterList");


        }

        public ActionResult PackageMasterDelete(string id)
        {
            if (!PermissionManager.IsActionPermit("Package Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            Reports products = new Reports();
            products.Pk_PackageID = int.Parse(id);
            products.OpCode = 2;
            products.AddedBy = int.Parse(SessionManager.Fk_MemId.ToString());
            DataSet dataset = products.PackageMaster();
            return RedirectToAction("PackageMasterList");
        }

        public ActionResult GetOfferDateMaster(ProductsDetail products)
        {
            if (!PermissionManager.IsActionPermit("Offer Date Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            DataSet dataSet = new DataSet();
            products.OpCode = 2;
            dataSet = products.InsertOfferDateMaster();
            products.dtDetails = dataSet.Tables[0];
            return View(products);
        }
        public ActionResult OfferDateMaster(ProductsDetail products, string id)
        {
            
            DataSet dataSet = new DataSet();
            if (id == null)
            {
                if (!PermissionManager.IsActionPermit("Offer Date Master", ViewOptions.FormSave))
                {
                    return Redirect("/Home/adminlogin");
                }
                ViewBag.Btn = "Save";
            }
            else
            {
                if (!PermissionManager.IsActionPermit("Offer Date Master", ViewOptions.FormUpdate))
                {
                    return Redirect("/Home/adminlogin");
                }
                products.OpCode = 3;
                ViewBag.Btn = "Update";
                products.PK_OfferMasterId = id;
                dataSet = products.InsertOfferDateMaster();
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    products.PK_OfferMasterId = dataSet.Tables[0].Rows[0]["PK_OfferMasterId"].ToString();
                    products.BV = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["BV"]);
                    DateTime dt = DateTime.Parse(dataSet.Tables[0].Rows[0]["FromDate"].ToString());
                    products.FromDate = dt.ToString("yyyy-MM-dd");
                    DateTime dt1 = DateTime.Parse(dataSet.Tables[0].Rows[0]["ToDate"].ToString());
                    products.ToDate = dt1.ToString("yyyy-MM-dd");
                    return View(products);
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult OfferDateMaster(ProductsDetail products, string Save, string id)
        {

            DataSet dataSet = new DataSet();
            if (Save == "Save")
            {
                products.OpCode = 1;
            }
            else
            {
                products.OpCode = 5;
            }
            products.CreatedBy = SessionManager.Fk_MemId;
            dataSet = products.InsertOfferDateMaster();
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Code"].ToString() == "1")
            {
                TempData["msg"] = dataSet.Tables[0].Rows[0]["Remark"].ToString();
                TempData["code"] = dataSet.Tables[0].Rows[0]["Code"].ToString();
                return RedirectToAction("GetOfferDateMaster");
            }
            else
            {
                TempData["msg"] = dataSet.Tables[0].Rows[0]["Remark"].ToString();
                TempData["code"] = dataSet.Tables[0].Rows[0]["Code"].ToString();
                return View(products);
            }
        }
        public ActionResult OfferDateMasterDelete(string id)
        {
            if (!PermissionManager.IsActionPermit("Offer Date Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            ProductsDetail products = new ProductsDetail();
            products.OpCode = 4;
            products.PK_OfferMasterId = id;
            products.CreatedBy = SessionManager.Fk_MemId;
            DataSet dataSet = products.InsertOfferDateMaster();
            return RedirectToAction("GetOfferDateMaster");
        }
        [HttpPost]
        public ActionResult AddPackageTemp(Reports product)
        {
            DataSet dataSet = new DataSet();
            product.AddedBy = int.Parse(SessionManager.Fk_MemId.ToString());
            dataSet = product.ADDTempPackageProduct();
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                if (dataSet.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    product.Code = "1";
                }
                else
                {
                    product.Code = "0";
                    product.Msg = dataSet.Tables[0].Rows[0]["msg"].ToString();

                }
            }
            return Json(product);
        }
        public ActionResult GetTempPackageProduct(int Pk_PackageID)
        {
            Reports product = new Reports();
           
            DataSet dataSet = new DataSet();
            product.Pk_PackageID = Pk_PackageID;
          
            product.AddedBy = int.Parse(SessionManager.Fk_MemId.ToString());
            if(Pk_PackageID==0)
            {
                dataSet = product.GetTempPackageProduct();
            }
            else
            {
                dataSet = product.EditTempPackageProduct();
            }
                List<Reports> list = new List<Reports>();
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    Reports varientData = new Reports();
                    varientData.Pk_PackageID = int.Parse(dataSet.Tables[0].Rows[i]["Fk_PackageId"].ToString());
                    varientData.Pk_ProductId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_ProductId"].ToString());
                    varientData.ProductQTY = dataSet.Tables[0].Rows[i]["QTY"].ToString();
                    varientData.ProductMRP =dataSet.Tables[0].Rows[i]["MRP"].ToString();
                    varientData.ProductBV = dataSet.Tables[0].Rows[i]["BV"].ToString();
                    varientData.ProductDP = dataSet.Tables[0].Rows[i]["DP"].ToString();
                    varientData.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                    varientData.Pk_PackageProductId = dataSet.Tables[0].Rows[i]["Pk_PackageProductId"].ToString();
                    list.Add(varientData);
                }
            }
            return Json(list);
        }
        [HttpPost]
        public ActionResult DeletePackageProductTemp(Reports product)
        {
            DataSet dataSet = new DataSet();
            product.AddedBy = int.Parse(SessionManager.Fk_MemId.ToString());
            dataSet = product.DeleteTempPackageProduct();
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                if (dataSet.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    product.Code = "1";
                }
                else
                {
                    product.Code = "0";
                    product.Msg = dataSet.Tables[0].Rows[0]["msge"].ToString();

                }
            }
            return Json(product);
        }

        public ActionResult GetPackageProduct(int Pk_PackageID)
        {
            Reports product = new Reports();
            //if (string.IsNullOrEmpty(Fk_ProductId))
            //    Fk_ProductId = "0";
            DataSet dataSet = new DataSet();
            product.Pk_PackageID = Pk_PackageID;
            //product.OpCode = 4;
            product.AddedBy = int.Parse(SessionManager.Fk_MemId.ToString());
           
            dataSet = product.GetPackageProduct();
           
            List<Reports> list = new List<Reports>();
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    Reports varientData = new Reports();
                    varientData.Pk_PackageID = int.Parse(dataSet.Tables[0].Rows[i]["Fk_ProductId"].ToString());
                    varientData.Pk_ProductId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_PackageId"].ToString());
                    varientData.ProductQTY = dataSet.Tables[0].Rows[i]["QTY"].ToString();
                    varientData.ProductMRP = dataSet.Tables[0].Rows[i]["MRP"].ToString();
                    varientData.ProductBV = dataSet.Tables[0].Rows[i]["BV"].ToString();
                    varientData.ProductDP = dataSet.Tables[0].Rows[i]["DP"].ToString();
                    varientData.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                    varientData.Pk_PackageProductId = dataSet.Tables[0].Rows[i]["Pk_PackageProductId"].ToString();
                    list.Add(varientData);
                }
            }
            return Json(list);
        }       
        public ActionResult UploadImage(MAdminProductMaster obj)
        {

            List<MAdminProductMaster> objlst = new List<MAdminProductMaster>();
            try
            {
                ResponseMaster _result = new ResponseMaster();
                objlst = _objService.ImageGetService(obj);
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
        [HttpPost]
        public ActionResult UploadImageSave(MAdminProductMaster obj)
        {           
            ResponseMaster _result = new ResponseMaster();
            try
            {

                if (obj.file != null && obj.file.ContentLength > 0)
                {
                    string ImageUrl = obj.file.FileName;
                    var ImagePath = "~/Images/ImageUrl/";
                    var extension = Path.GetExtension(ImageUrl);
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + ImageUrl;
                    obj.ImageUrl = ImagePath + fileName;

                    var filename = Path.Combine(Server.MapPath(ImagePath), fileName);
                    if (!Directory.Exists(Server.MapPath(ImagePath)))
                    {
                        Directory.CreateDirectory((Server.MapPath(ImagePath)));
                    }
                    obj.file.SaveAs(filename);

                }
                obj.OpCode = 1;
                _result = _objService.SaveImage(obj);
                if (_result != null)
                {

                    TempData["code"] = _result.Code.ToString();
                    TempData["msg"] = _result.Msg.ToString();
                    return Redirect("UploadImage");
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
            return Redirect("UploadImage");                      
        }        
        public ActionResult ImageDelete(int Id)
        {
            MAdminProductMaster obj = new MAdminProductMaster();
            obj.Id = Id.ToString();
            obj.OpCode = 2;
            try
            {
                ResponseMaster _result = new ResponseMaster();
                _result = _objService.DeleteImage(obj);
                if (_result != null)
                {
                    TempData["code"] = _result.Code.ToString();
                    TempData["msg"] = _result.Msg.ToString();
                    //return RedirectToAction("UploadImage", "Master");
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
            //return RedirectToAction("UploadImage", "Master");
            return Redirect("UploadImage");
        }
        [HttpGet]
        public ActionResult UploadVideo()
        {
            MAdminProductMaster obj = new MAdminProductMaster();
            List<MAdminProductMaster> objlst = new List<MAdminProductMaster>();
            try
            {

                objlst = _objService.GetVideoService(obj);
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
        [HttpPost]
        public ActionResult UploadVideo(MAdminProductMaster obj)
        {
            ResponseMaster _result = new ResponseMaster();
            try
            {
                 obj.OpCode = 1;
                _result = _objService.SaveVideo(obj);
                if (_result != null)
                {

                    TempData["code"] = _result.Code.ToString();
                    TempData["msg"] = _result.Msg.ToString();
                    return Redirect("UploadVideo");
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
            return Redirect("UploadVideo");
        }
        public ActionResult VideoDelete(int Id)
        {
            MAdminProductMaster obj = new MAdminProductMaster();
           // obj.Id = Id;
            obj.OpCode = 2;
            try
            {

                ResponseMaster _result = new ResponseMaster();

                _result = _objService.SaveVideo(obj);
                if (_result != null)
                {

                    TempData["code"] = _result.Code.ToString();
                    TempData["msg"] = _result.Msg.ToString();
                    return Redirect("UploadVideo");
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
            return Redirect("UploadVideo");
        }
    }
}
