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
        public ActionResult UploadFAQ(MAdminUnit model)
        {
            if (!PermissionManager.IsActionPermit("FAQ Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminUnit _result = new MAdminUnit();
            try
            {
                model.ProcId = 1;
                UnitService _objService = new UnitService();

                _result.objList = _objService.GetUnitService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult FAQSave(MAdminUnit model)
        {
            if (!PermissionManager.IsActionPermit("FAQ Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            if (!PermissionManager.IsActionPermit("FAQ Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseUnitModel _result = new ResponseUnitModel();
            try
            {
                if (model.Pk_UnitId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 1; }
                UnitService _objService = new UnitService();

                _result = _objService.SaveUnitService(model);
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
            return Redirect("/FAQ");
        }

        
        //public ActionResult UnitDelete(int Id)
        //{
        //    if (!PermissionManager.IsActionPermit("Unit Master", ViewOptions.FormDelete))
        //    {
        //        return Redirect("/Home/adminlogin");
        //    }
        //    MAdminUnit model = new MAdminUnit();
        //    model.Pk_UnitId = Id;
        //    model.ProcId = 4;
        //    ResponseUnitModel _result = new ResponseUnitModel();
        //    try
        //    {

        //        UnitService _objService = new UnitService();

        //        _result = _objService.SaveUnitService(model);
        //        if (_result != null)
        //        {

        //            TempData["code"] = _result.Flag;
        //            TempData["msg"] = _result.Msg;
        //        }

        //        else
        //        {
        //            TempData["code"] = "0";
        //            TempData["msg"] = "Can not process the request";
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return Redirect("/Unit");
        //}
    }
}