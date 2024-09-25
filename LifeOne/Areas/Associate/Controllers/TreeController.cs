using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Associate.Controllers
{
    public class TreeController : Controller
    {
        // GET: Associate/Tree/GeneologyTree
        public ActionResult Index()
        {
            return View();
        }

        //Associate/Tree/GeneologyTree

        [HttpGet]
        public ActionResult GeneologyTree()
        {

            return View();
        }



        public JsonResult RootTree(string Loginid)
        {

            TreeModel model = new TreeModel();
            TreeRepository obj = new TreeRepository();
            if (Loginid != null)
            {
                model.SearchLoginId = Loginid;
            }
            model.LoginId = SessionManager.Usertype == "3" ? "LIFEONE" : SessionManager.LoginId;
            List<TreeModel> TreeList = obj.TreeList(model);
            return Json(TreeList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindTree(string ParentId)
        {

            TreeModel model = new TreeModel();
            TreeRepository obj = new TreeRepository();
            model.LoginId = ParentId;
            List<TreeModel> SearchData = obj.TreeMembersChild(model);


            return Json(SearchData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchUpline(TreeModel model)
        {

            TreeRepository obj = new TreeRepository();
            TreeModel objmodel = obj.Upline(model);
            return Json(objmodel, JsonRequestBehavior.AllowGet);

        }
    }
}