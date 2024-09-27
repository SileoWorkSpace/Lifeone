using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.API.DAL;
using LifeOne.Models.API;
using System.Data;
using LifeOne.Models.Manager;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;

namespace LifeOne.Areas.Associate.Controllers
{
    public class AssociateOrderController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderByAssociate()
        {
            try
            {               
                ViewBag.Product = DALBindCommonDropdown.BindProductDropdown(0, 0);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        public JsonResult GetProductDetails(MFranchiseorderRequest _data)
        {
            try
            {
                MFranchiseorderRequest obj = new DALFranchise().GetAllProductDetails(_data);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult AddProduct(MFranchiseorderRequest _data)
        {
            try
            {
                MSimpleString obj = FranchiseOrderService.AddAssociateProduct(_data);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult GetOrderTemp(MSimpleString _data)
       {
            try
            {
                MFranchiseorderRequest obj = new MFranchiseorderRequest();
                _data.Fk_MemId  = Convert.ToInt32(SessionManager.AssociateFk_MemId);
                List<MFranchiseorderRequest> _objlst = DALFranchise.AssociateOrderTemp(_data);
                return Json(_objlst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult DeleteProductTemp(MFranchiseorderRequest _data)
        {
            try
            {
                MSimpleString _objlst = DALFranchise.DeleteAssociateProductTemp(_data);
                return Json(_objlst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult BindProduct(MFranchiseorderRequest _data)
        {
            try
            {
                List<SelectListItem> Lst = DALBindCommonDropdown.BindDropdown(1, _data.CategoryId);
                return Json(Lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult SaveProduct(MFranchiseorderRequest _data)
        {

            try
            {
                _data.Fk_Memid = SessionManager.AssociateFk_MemId;
                MSimpleString result = FranchiseOrderService.SaveAssociateProduct(_data);
                    return Json(result, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}