using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using System.Web.Mvc;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class PerformanceLevelMasterController : Controller
    {
        // GET: Admin/PerformanceLevelMaster


        public ActionResult PerformanceLevelMaster(MPerformaneLevel model)
        {
            if (!PermissionManager.IsActionPermit("Performance Level Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            MPerformaneLevel _result = new MPerformaneLevel();
            try
            {
                model.ProcId = 1;
                PerformanceLevelService _objService = new PerformanceLevelService();
                ViewBag.DllPerformanceLevel = DALBindCommonDropdown.BindDropdown(10, 0);
                _result.objList = _objService.GetPerformanceLevelService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult PerformanceLevelMasterSave(MPerformaneLevel model)
        {
            if (!PermissionManager.IsActionPermit("Performance Level Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponsePerformaneLevelModel _result = new ResponsePerformaneLevelModel();
            try
            {
                if (model.PK_LevelId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 3; }
                PerformanceLevelService _objService = new PerformanceLevelService();

                _result = _objService.SavePerformanceLevelService(model);
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
            ViewBag.DllPerformanceLevel = DALBindCommonDropdown.BindDropdown(10, 0);
            return Redirect("/PerformanceLevelMaster");
        }


        public ActionResult PerformanceLevelMasterDelete(int Id)
        {
            if (!PermissionManager.IsActionPermit("Performance Level Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            MPerformaneLevel model = new MPerformaneLevel();
            model.PK_LevelId = Id;
            model.ProcId = 4;
            ResponsePerformaneLevelModel _result = new ResponsePerformaneLevelModel();
            try
            {

                PerformanceLevelService _objService = new PerformanceLevelService();

                _result = _objService.SavePerformanceLevelService(model);
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
            ViewBag.DllPerformanceLevel = DALBindCommonDropdown.BindDropdown(10, 0);
            return Redirect("/PerformanceLevelMaster");
        }


    }
}