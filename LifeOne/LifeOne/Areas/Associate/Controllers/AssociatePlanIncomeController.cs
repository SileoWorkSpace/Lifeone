using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Associate.Controllers
{

    [SessionTimeoutAttribute]
    public class AssociatePlanIncomeController : Controller
    {
        // GET: Associate/AssociatePlanIncome
        public ActionResult IncomeDetails(int? Page, string PlanId)
        {
            AssciateIncomeModel mdl = new AssciateIncomeModel();

            AssociateIncomeDAL obj = new AssociateIncomeDAL();

            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            if (Page == null || Page == 0)
                Page = 1;
            mdl.Page = Page;
            mdl.PlanId = PlanId;
            mdl.LoginID = SessionManager.LoginId;
            mdl.AssciateIncomeList = obj.GetIncomeDetails(mdl);

            foreach (var item in mdl.AssciateIncomeList)
            {
                string IncomeType = item.PlanName;
                ViewBag.IncomeType = IncomeType;
                break;
            }        
            int totalRecords = 0;
            if (mdl.AssciateIncomeList.Count > 0)
            {

                totalRecords = Convert.ToInt32(mdl.AssciateIncomeList[0].TotalRecords);
                var pager = new Pager(totalRecords, mdl.Page, SessionManager.Size);
                mdl.Pager = pager;
            }
            else
            {
                mdl.AssciateIncomeList = new List<AssciateIncomeModel>();
            }
            return View(mdl);
        }
    }
}