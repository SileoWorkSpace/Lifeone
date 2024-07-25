using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Associate.Controllers
{
    public class AssociateRewardsController : Controller
    {
        // GET: Associate/AssociateRewards
        public ActionResult Rewards()
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();
            obj2.FkMemId = SessionManager.AssociateFk_MemId;
            obj2.Rewardslst = obj.Rewards(obj2);
            return View(obj2);
        }

        #region  Claim 
        [HttpPost]
        public JsonResult Claim(int rId)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();

            obj2.PK_RId = rId;
            obj2.Action = "Claim";
            obj2.FkMemId = SessionManager.AssociateFk_MemId;
            obj2 = obj.ClaimAndSkip(obj2);
            return Json(obj2, JsonRequestBehavior.AllowGet);
        }
      
        #endregion

        #region Skip 
        [HttpPost]
        public JsonResult Skip(int rId)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();

            obj2.PK_RId = rId;
            obj2.Action = "Skip";
            obj2.FkMemId = SessionManager.AssociateFk_MemId;
            obj2 = obj.ClaimAndSkip(obj2);
            return Json(obj2, JsonRequestBehavior.AllowGet);
        }

        #endregion

        [HttpGet]
        public ActionResult AssociateBonanza()
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();
            obj2.FkMemId = SessionManager.AssociateFk_MemId;
            obj2.Rewardslst = obj.AssociateBonaza(obj2);
            return View(obj2);
        }

        #region  Claim 
        [HttpPost]
        public JsonResult BonanzaClaim(int rId)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();

            obj2.PK_RId = rId;
            obj2.Action = "Claim";
            obj2.FkMemId = SessionManager.AssociateFk_MemId;
            obj2 = obj.BonanzaClaimAndSkip(obj2);
            return Json(obj2, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Skip 
        [HttpPost]
        public JsonResult BonanzaSkip(int rId)
        {
            AssociateRewardsDAL obj = new AssociateRewardsDAL();
            RewardsModel obj2 = new RewardsModel();

            obj2.PK_RId = rId;
            obj2.Action = "Skip";
            obj2.FkMemId = SessionManager.AssociateFk_MemId;
            obj2 = obj.BonanzaClaimAndSkip(obj2);
            return Json(obj2, JsonRequestBehavior.AllowGet);
        }

        #endregion



      

    }
}