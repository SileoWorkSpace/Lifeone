using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class SupportTypeMasterController : Controller
    {
        // GET: Admin/SupportTypeMaster
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SupportType(MSupportType model)
        {
            if (!PermissionManager.IsActionPermit("Support Type Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MSupportType _result = new MSupportType();
            try
            {
                model.ProcId = 1;
                SupportTypeService _objService = new SupportTypeService();
                _result.objList = _objService.GetSupportTypeService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult SupportTypeSave(MSupportType model)
        {
            if (!PermissionManager.IsActionPermit("Support Type Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseSupportTypeModel _result = new ResponseSupportTypeModel();
            try
            {
                if (model.PK_SupportId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 1; }
                SupportTypeService _objService = new SupportTypeService();

                _result = _objService.SaveSupportTypeService(model);
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
            return Redirect("/SupportType");
        }


        public ActionResult SupportTypeDelete(int Id)
        {
            if (!PermissionManager.IsActionPermit("Support Type Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            MSupportType model = new MSupportType();
            model.PK_SupportId = Id;
            model.ProcId = 4;
            ResponseSupportTypeModel _result = new ResponseSupportTypeModel();
            try
            {

                SupportTypeService _objService = new SupportTypeService();

                _result = _objService.SaveSupportTypeService(model);
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
            return Redirect("/SupportType");
        }
    }
}