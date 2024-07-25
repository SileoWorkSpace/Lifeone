using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class CommissionMasterController : Controller
    {
        // GET: Admin/CommissionMaster
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Commission(MCommission model)
        {
            MCommission _result = new MCommission();
            try
            {
                model.ProcId = 1;
                CPCommissionService _objService = new CPCommissionService();

                _result.objList = _objService.GetCommissionService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult CommissionSave(MCommission model)
        {
            ResponseCommissionModel _result = new ResponseCommissionModel();
            try
            {
                if (model.Pk_CommonId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 1; }
                CPCommissionService _objService = new CPCommissionService();

                _result = _objService.SaveCommissionService(model);
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
            return Redirect("/Commission");
        }

        public ActionResult CommissionDelete(int Id)
        {
            MCommission model = new MCommission();
            model.Pk_CommonId = Id;
            model.ProcId = 4;
            ResponseCommissionModel _result = new ResponseCommissionModel();
            try
            {

                CPCommissionService _objService = new CPCommissionService();

                _result = _objService.SaveCommissionService(model);
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
            return Redirect("/Commission");
        }


    }
}