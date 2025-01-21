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
using LifeOne.Models.Common;

namespace LifeOne.Areas.Admin.Controllers
{
   
    [SessionTimeoutAttributeAdmin]
    public class ManageStockController : Controller
    {
        // GET: Admin/ManageStock
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageStock(MAdminManageStock model)
        {
            if (!PermissionManager.IsActionPermit("Manage Stock", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminManageStock _result = new MAdminManageStock();
            try
            {
               
                ManageStockService _objService = new ManageStockService();

                _result.objList = _objService.GetStockService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult CrDrStock(string Commond, string count)
        {   
            try
            {
                if (!PermissionManager.IsActionPermit("Manage Stock", ViewOptions.FormSave))
                {
                    return Redirect("/Home/adminlogin");
                }

                if (count == null || Convert.ToInt32(count) < 1)
                {

                    TempData["code"] = "0";
                    TempData["msg"] = "Product input must be required !";
                    return RedirectToAction("ManageStock", "ManageStock");

                }

                MAdminManageStock para = new MAdminManageStock();
                ResponseManageStock _result = new ResponseManageStock();
                ManageStockService _objService = new ManageStockService();
                int intCheck = 0;
                #region AddDebit Stock
                if (Commond == "Add")
                {
                    for (int i = 1; i <= int.Parse(count); i++)
                    {
                        if (Request["ProductQty_" + i] != null && Request["ProductQty_" + i] != "")
                        {

                            intCheck = 1;
                            para.ProductQty = long.Parse(Request["ProductQty_" + i]);
                            para.Fk_ProductID = long.Parse(Request["Fk_ProductID_" + i]);
                            para.Mode = "Add";
                            para.Fk_Memid = SessionManager.Fk_MemId;
                            para.Type = 1; //1 For Admin
                            if (para.Fk_ProductID != 0)
                            {
                                _result = _objService.CrDrStockService(para);
                            }

                        }
                    }
                    if (intCheck == 0)
                    {
                        TempData["code"] = "0";
                        TempData["msg"] = "Please enter at least one Models.Product quantity!";
                        return RedirectToAction("ManageStock", "ManageStock");


                    }

                    TempData["code"] = "1";
                    TempData["msg"] = "Quantity Added.!!";
                    return RedirectToAction("ManageStock", "ManageStock");

                }
                if (Commond == "Deduct")
                {
                    for (int i = 1; i <= int.Parse(count); i++)
                    {
                        if (Request["ProductQty_" + i] != null && Request["ProductQty_" + i] != "")
                        {

                            intCheck = 1;
                            string avQty = Request["hdnProductQty_" + i];
                            string txtQty = Request["ProductQty_" + i];
                            para.Fk_ProductID = long.Parse(Request["Fk_ProductID_" + i]);
                            if (Request["ProductQty_" + i] == "")
                            {
                                txtQty = "0";
                            }
                            if (Convert.ToInt32(txtQty) > Convert.ToInt32(avQty))
                            {
                                TempData["code"] = "0";
                                TempData["msg"] = "Invalid quantity to deduct!!";
                                return RedirectToAction("ManageStock", "ManageStock");
                            }
                            para.ProductQty = long.Parse(Request["ProductQty_" + i]);
                            para.Mode = "Deduct";
                            para.Fk_Memid = SessionManager.Fk_MemId;
                            para.Type = 1; //1 For Admin
                            if (para.Fk_ProductID != 0)
                            {
                                _result = _objService.CrDrStockService(para);
                            }

                        }
                    }
                    if (intCheck == 0)
                    {
                        TempData["code"] = "0";
                        TempData["msg"] = "Please enter at least one Models.Product quantity!";
                        return RedirectToAction("ManageStock", "ManageStock");


                    }
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

                #endregion
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("ManageStock", "ManageStock");
        }
    }
}