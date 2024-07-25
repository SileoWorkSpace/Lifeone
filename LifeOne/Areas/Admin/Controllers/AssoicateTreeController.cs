using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    public class AssoicateTreeController : Controller
    {
        // GET: Admin/AssoicateTree/ViewTree
        public ActionResult ViewTree()
        {
            if (!PermissionManager.IsActionPermit("Tree", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
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
            model.LoginId = "LifeOne";// SessionManager.Usertype == "3" ? "LifeoneWellness" : SessionManager.LoginId;
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