using DocumentFormat.OpenXml.EMMA;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpGet]
        public ActionResult Request()
        {
 
            Models.AssociateManagement.AssociateEntity.AssosiateRequest Entity = new Models.AssociateManagement.AssociateEntity.AssosiateRequest();
            Entity.LoginId = Convert.ToString(Session["LoginId"] ?? "0");
           
            Entity.Name = Convert.ToString(Session["Name"] ?? null);
            #region BankDll
            Models.AssociateManagement.AssociateDAL.RequestDAL para = new Models.AssociateManagement.AssociateDAL.RequestDAL();
            ViewBag.BankDDL = para.GetBank(Entity);
            #endregion

            return View(Entity);
            
        }
        [HttpPost]
        public ActionResult Request(Models.AssociateManagement.AssociateEntity.AssosiateRequest Entity)
        {
            try
            {
                Models.AssociateManagement.AssociateDAL.RequestDAL para = new Models.AssociateManagement.AssociateDAL.RequestDAL();
                #region BankDll
                Models.AssociateManagement.AssociateDAL.RequestDAL obj = new Models.AssociateManagement.AssociateDAL.RequestDAL();
                ViewBag.BankDDL = obj.GetBank(Entity);
                #endregion
                if (string.IsNullOrEmpty(Entity.Date) == false)
                {
                    Entity.Convert_date = DsResultModel.ConvertToSystemDate(Entity.Date, "dd/MM/yyyy");
                }
                Entity.AddedBy = Convert.ToInt32(Session["AssociateFk_MemId"] ?? "0");
                Entity.OpCode = 1;
                Models.AssociateManagement.AssociateEntity.AssosiateRequest Responce = para.RequestKaryonPoints(Entity);
                if (Responce.Code == "1")
                {
                    TempData["Message"] = "Request Save Successfuly.!";
                    TempData["Flag"] = "1";
                    return RedirectToAction("Request");
                }
                else
                {
                    TempData["Message"] = "Request not Saved.!";
                    TempData["Flag"] = "1";
                }
                return View(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

        }
    }
}