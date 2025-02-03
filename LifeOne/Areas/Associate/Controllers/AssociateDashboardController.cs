using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.Manager;
using LifeOne.Models.Common;
using LifeOne.Models.API.DAL;
using LifeOne.Models.API;
using LifeOne.Models.AssociateManagement.AssociateDAL;

namespace LifeOne.Areas.Associate.Controllers
{


    [SessionTimeoutAttribute]
    public class AssociateDashboardController : Controller
    {
        // GET: Associate/AssociateDashboard/Index
        public ActionResult Index()
        {
            return View();       
        }


        //[RouteArea("Admin”)]
        //[RoutePrefix(“menu”)]
        //[Route(“{action}”)]
        public ActionResult Dashboard()        
        {
            try
            {
                //AssociateDashBoard Value = new AssociateDashBoard();
                AssociateDashboardWeb Value = new AssociateDashboardWeb();
                AssociateDashboardReq ObjM = new AssociateDashboardReq();
                if (SessionManager.AssociateFk_MemId == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                ObjM.Fk_MemId = SessionManager.AssociateFk_MemId;
                dynamic item = new ExpandoObject();
                AssociateDashboardService ObjR = new AssociateDashboardService();
                item = ObjR.AssociateDashboard(ObjM);
                
                if (item._AssociateDashBoardData != null)
                {
                    Value = item._AssociateDashBoardData;
                }
                if (item._AssociateDetails != null)
                {
                    Value.lstAssociates = item._AssociateDetails;
                }
                if (item._NewsDetails != null)
                {
                    Value.lstNews = item._NewsDetails;
                }
                if (item._payoutStatements != null)
                {
                    Value.payoutStatements = item._payoutStatements;
                }
                if (item._payoutSummaries != null)
                {
                    Value.payoutSummaries = item._payoutSummaries;
                }
                if (item._rewarddetails != null)
                {
                    Value.rewarddetails = item._rewarddetails;
                }
                AssociateRewardsDAL obj = new AssociateRewardsDAL();
                RewardsModel obj2 = new RewardsModel();
                obj2.FkMemId = SessionManager.AssociateFk_MemId;
                Value.Rewardslst = obj.Rewards(obj2);                
                return View(Value);                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult Statistics()
        {
            try
            {
                AssociateDashBoard Value = new AssociateDashBoard();
                AssociateDashboardReq ObjM = new AssociateDashboardReq();
                if (SessionManager.AssociateFk_MemId == 0)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                ObjM.Fk_MemId = SessionManager.AssociateFk_MemId;
                dynamic item = new ExpandoObject();
                AssociateDashboardService ObjR = new AssociateDashboardService();
                item = ObjR.AssociateDashboardStatistics(ObjM);
                if (item._AssociateDashBoardData != null)
                {
                    Value = item._AssociateDashBoardData;
                }
                if (item._NewsDetails != null)
                {
                    Value.lstNews = item._NewsDetails;
                }
                return View(Value);
            }
            catch (Exception)
            {

                throw;
            }

        }


        //public ActionResult UIDDashboard()
        //{
        //    AssociateDashBoard Value = new AssociateDashBoard();
        //    AssociateDashboardReq ObjM = new AssociateDashboardReq();
        //    if (SessionManager.AssociateFk_MemId == 0)
        //    {
        //        return RedirectToAction("Login", "Home", new { area = "" });
        //    }
        //    ObjM.Fk_MemId = SessionManager.AssociateFk_MemId;
        //    dynamic item = new ExpandoObject();
        //    AssociateDashboardService ObjR = new AssociateDashboardService();
        //    item = ObjR.UIDAssociateDashboard(ObjM);
        //    if (item._AssociateDashBoardData != null)
        //    {
        //        Value = item._AssociateDashBoardData;
        //    }
        //    if (item._AssociateDetails != null)
        //    {
        //        Value.lstAssociates = item._AssociateDetails;
        //    }
        //    if (item._NewsDetails != null)
        //    {
        //        Value.lstNews = item._NewsDetails;
        //    }
        //    return View(Value);
        //}

        public ActionResult Comingsoon()
        {
            return View();
        }
        
    }
}