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
    public class BankMasterController : Controller
    {
        // GET: Admin/BankMaster
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bank(MAdminBank model)
        {
            if (!PermissionManager.IsActionPermit("Bank Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminBank _result = new MAdminBank();
            try
            {
                model.ProcId = 1;
                BankService _objService = new BankService();

                _result.objList = _objService.GetBankService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult BankSave(MAdminBank model)
        {
            if (!PermissionManager.IsActionPermit("Bank Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseBankModel _result = new ResponseBankModel();
            try
            {
                if (model.Pk_BankId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 1; }
                BankService _objService = new BankService();

                _result = _objService.SaveBankService(model);
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
            return Redirect("/Bank");
        }

        public ActionResult BankDelete(int Id)
        {
            if (!PermissionManager.IsActionPermit("Bank Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            MAdminBank model = new MAdminBank();
            model.Pk_BankId = Id;
            model.ProcId = 4;
            ResponseBankModel _result = new ResponseBankModel();
            try
            {

                BankService _objService = new BankService();

                _result = _objService.SaveBankService(model);
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
            return Redirect("/Bank");
        }
    }
}