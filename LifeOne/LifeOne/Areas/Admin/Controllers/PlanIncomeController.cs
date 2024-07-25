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
using LifeOne.Models.AdminManagement.ADAL;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class PlanIncomeController : Controller
    {
        // GET: Admin/PlanIncome
        public ActionResult PlanIncome(MAdminPlanIncome model)
        {
            MAdminPlanIncome _result = new MAdminPlanIncome();
            try
            {
                model.ProcId = 1;
                PlanIncomeService _objService = new PlanIncomeService();
                int Proc_Id = 6;
                long value = 0;
                ViewBag.DllUtility = DALBindCommonDropdown.BindDropdown(Proc_Id, value);
                _result.objList = _objService.GetPlanIncomeService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult PlanIncomeSave(MAdminPlanIncome model)
        {
            ResponsePlanIncomeModel _result = new ResponsePlanIncomeModel();
            try
            {
                if (model.Pk_PlanIncomeId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 1; }
                model.CreatedBy = Convert.ToInt32(SessionManager.Fk_MemId);
                PlanIncomeService _objService = new PlanIncomeService();

                _result = _objService.SavePlanIncomeService(model);
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
            return Redirect("/PlanIncome");
        }


        public ActionResult PlanIncomeDelete(int Id)
        {
            MAdminPlanIncome model = new MAdminPlanIncome();
            model.Pk_PlanIncomeId = Id;
            model.ProcId = 4;
            ResponsePlanIncomeModel _result = new ResponsePlanIncomeModel();
            try
            {
                PlanIncomeService _objService = new PlanIncomeService();
                model.CreatedBy = Convert.ToInt32(SessionManager.Fk_MemId);
                _result = _objService.SavePlanIncomeService(model);
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
            return Redirect("/PlanIncome");
        }

        public ActionResult IncomeDetails(int? Page, string PlanId , string LoginId)
        {

            if (!PermissionManager.IsActionPermit("Income Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            AdminIncomeDetailsModel mdl = new AdminIncomeDetailsModel();   
            AdminIncomeDAL obj = new AdminIncomeDAL();

            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }

            mdl.PlanId = PlanId;
            mdl.LoginID = LoginId;
            mdl.Page = Page;
            ViewBag.PlanId = PlanId;

            mdl.AdminIncomeDetailsList = obj.GetIncomeDetails(mdl);

            foreach (var item in mdl.AdminIncomeDetailsList)
            {
                string IncomeType = item.PlanName;
                ViewBag.IncomeType = IncomeType;
                break;
            }

            return View(mdl);
        }

        public ActionResult ExportToExcelSelfIncomeReport(int? Page, string PlanId, string LoginId)
        {
            AdminIncomeDetailsModel mdl = new AdminIncomeDetailsModel();
            AdminIncomeDAL obj = new AdminIncomeDAL();
            mdl.PlanId = PlanId;
            mdl.LoginID = LoginId;
            mdl.Page = Page;
            ViewBag.PlanId = PlanId;

            mdl.AdminIncomeDetailsList = obj.GetIncomeDetails(mdl);
            DataTable dt = new DataTable("SelfIncomeReport");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("From ID"),
                                             new DataColumn("Display Name"),
                                            new DataColumn("Business Amount"),
                                            new DataColumn("Commission Percentage"),
                                            new DataColumn("Income Type"),
                                            new DataColumn("Closing Date"),
                                            new DataColumn("Amount")
            });
            if (mdl.AdminIncomeDetailsList.Count > 0)
            {
                foreach (var customer in mdl.AdminIncomeDetailsList)
                {
                    dt.Rows.Add(customer.LoginID,customer.DisplayName, customer.BusinessAmount, customer.CommissionPercentage, customer.PlanName, customer.ClosingDate, customer.NetAmount);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SelfIncomeReport.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("PayoutReport");
            }
        }
    }
}