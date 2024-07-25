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
    public class AchieverClubBonusMasterController : Controller
    {
        // GET: Admin/AchieverClubBonusMaster
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AchieverClubBonusMaster(MAchieverClubBonusMaster model)
        {
            if (!PermissionManager.IsActionPermit("Achiever Club Bonus Master", ViewOptions.FormView))
            {
                return RedirectToAction("adminlogin", "Home");
            }
            MAchieverClubBonusMaster _result = new MAchieverClubBonusMaster();
            try
            {
                model.ProcId = 1;
                AchieverClubBonusService _objService = new AchieverClubBonusService();
                ViewBag.DllPerformanceLevel = DALBindCommonDropdown.BindDropdown(12, 0);
                _result.objList = _objService.GetAchieverClubBonusService(model);
            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }

        [HttpPost]
        public ActionResult AchieverClubBonusMasterSave(MAchieverClubBonusMaster model)
        {
            if (!PermissionManager.IsActionPermit("Achiever Club Bonus Master", ViewOptions.FormSave))
            {
                return RedirectToAction("adminlogin", "Home");
            }
            AchieverClubBonusMasterResponse _result = new AchieverClubBonusMasterResponse();
            try
            {
                if (model.PK_LevelId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 3; }
                AchieverClubBonusService _objService = new AchieverClubBonusService();
                _result = _objService.SaveAchieverClubBonusService(model);
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
            ViewBag.DllPerformanceLevel = DALBindCommonDropdown.BindDropdown(12, 0);             
            return Redirect("/AchieverClubBonusMaster");
        }


        public ActionResult AchieverClubBonusMasterDelete(int Id)
        {
            if (!PermissionManager.IsActionPermit("Achiever Club Bonus Master", ViewOptions.FormDelete))
            {
                return RedirectToAction("adminlogin", "Home");
            }
            MAchieverClubBonusMaster model = new MAchieverClubBonusMaster();
            model.PK_LevelId = Id;
            model.ProcId = 4;
            AchieverClubBonusMasterResponse _result = new AchieverClubBonusMasterResponse();
            try
            {
                AchieverClubBonusService _objService = new AchieverClubBonusService();
                _result = _objService.SaveAchieverClubBonusService(model);
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
            return Redirect("/AchieverClubBonusMaster");
        }
    }
}