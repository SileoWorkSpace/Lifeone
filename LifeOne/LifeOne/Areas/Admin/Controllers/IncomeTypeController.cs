using iTextSharp.text.pdf.qrcode;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Manager;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class IncomeTypeController : Controller
    {
        // GET: Admin/IncomeType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IncomeType(MAdminIncomeTypeMaster model)
        {
            if (!PermissionManager.IsActionPermit("Income Type", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            MAdminIncomeTypeMaster _result = new MAdminIncomeTypeMaster();
            try
            {
                model.ProcId = 1;
                IncomeTypeService _objService = new IncomeTypeService();               
                int Proc_Id = 5;
                long value = 0;
                ViewBag.DllUtility = DALBindCommonDropdown.BindDropdown(Proc_Id, value);
                _result.objList = _objService.GetIncomeTypeService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        
        public ActionResult IncomeTypeSave(MAdminIncomeTypeMaster model)
        {
            if (!PermissionManager.IsActionPermit("Income Type", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseIncomeTypeMasterModel _result = new ResponseIncomeTypeMasterModel();
            try
            {
                if (model.Pk_IncomeTypeId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 1; }
                model.CreatedBy = Convert.ToInt32(SessionManager.Fk_MemId);
                IncomeTypeService _objService = new IncomeTypeService();

                _result = _objService.SaveIncomeTypeService(model);
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
            return Redirect("/IncomeType");
        }

        public ActionResult IncomeTypeDelete(int Id)
        {
            if (!PermissionManager.IsActionPermit("Income Type", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminIncomeTypeMaster model = new MAdminIncomeTypeMaster();
            model.Pk_IncomeTypeId = Id;
            model.ProcId = 4;
            ResponseIncomeTypeMasterModel _result = new ResponseIncomeTypeMasterModel();
            try
            {

                IncomeTypeService _objService = new IncomeTypeService();
                model.CreatedBy = Convert.ToInt32(SessionManager.Fk_MemId);
                _result = _objService.SaveIncomeTypeService(model);
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
            return Redirect("/IncomeType");
        }


    }
}