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

namespace LifeOne.Areas.UID.Controllers
{


    [SessionTimeoutAttribute]
    public class UIDDashboardController : Controller
    {
        //// GET: Associate/AssociateDashboard/Index
        //public ActionResult Index()
        //{
        //    return View();
        //}


        ////[RouteArea("Admin”)]
        ////[RoutePrefix(“menu”)]
        ////[Route(“{action}”)]
        //public ActionResult Dashboard()
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
        //    item = ObjR.AssociateDashboard(ObjM);
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



        //public ActionResult Statistics()
        //{
        //    try
        //    {
        //        AssociateDashBoard Value = new AssociateDashBoard();
        //        AssociateDashboardReq ObjM = new AssociateDashboardReq();
        //        if (SessionManager.AssociateFk_MemId == 0)
        //        {
        //            return RedirectToAction("Login", "Home", new { area = "" });
        //        }
        //        ObjM.Fk_MemId = SessionManager.AssociateFk_MemId;
        //        dynamic item = new ExpandoObject();
        //        AssociateDashboardService ObjR = new AssociateDashboardService();
        //        item = ObjR.AssociateDashboardStatistics(ObjM);
        //        if (item._AssociateDashBoardData != null)
        //        {
        //            Value = item._AssociateDashBoardData;
        //        }
        //        if (item._NewsDetails != null)
        //        {
        //            Value.lstNews = item._NewsDetails;
        //        }
        //        return View(Value);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}


        public ActionResult Dashboard()
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
            item = ObjR.UIDAssociateDashboard(ObjM);
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
            return View(Value);

        }
        public ActionResult Comingsoon()
        {
            return View();
        }
    }
}