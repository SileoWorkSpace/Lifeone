using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Associate.Controllers
{
    public class PremimumclubController : Controller
    {
        // GET: Associate/Premimumclub
        public ActionResult Index()
        {
            var list = PremiumClubMembersModel.GetPremiumClubMembers();
            ViewBag.PCMemberLst = list;
            if (list.Count > 0)
            {
                return View("premimumclubmember", list[0]);
            }
            else
            {
                return View("premimumclubmember");
            }
           // return View("premimumclubmember");
        }

        public ActionResult PremimumclubQualifier()
        {
            var list = PremiumClubMembersModel.GetPremiumClubQualifier();
            ViewBag.PCQualifierLst = list;
            if (list.Count > 0)
            {
                return View("PremimumclubQualifier", list[0]);
            }
            else
            {
                return View("PremimumclubQualifier");
            }
            // return View("premimumclubmember");
        }


        public JsonResult PremiumClubMembersList()
        {
            try
            {
                
                var list = PremiumClubMembersModel.GetPremiumClubMembers();
              //  return Json(list, JsonRequestBehavior.AllowGet);
                JsonResult json = Json(list, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}