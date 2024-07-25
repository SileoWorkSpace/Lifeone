using LifeOne.Filters;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{

    [SessionTimeoutAttributeAdmin]
    public class MemberParentController : Controller
    {
        // GET: Admin/MemberParent

        #region ChangeMemberSponser
        public ActionResult ChangeMemberParent()
        {
            if (!PermissionManager.IsActionPermit("Change Direct Seller Sponsor", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }

        public JsonResult BindMemberParentDetailByLoginID(MemberParent obj)
        {

            try
            {
                obj.PrdId = 1; // ProcType for geting Sponser Details
                var result = new MemberParentService().BindMemberParentDetailByLoginID(obj);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult UpdateMemberParentDetail(MemberParent obj)
        {
            try
            {
                obj.PrdId = 2;  // ProcType for updation of member parent
                var result = new MemberParentService().UpdateMemberParentDetail(obj);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region AddMiddleMember
        public ActionResult AddMiddleMember()
        {
            if (!PermissionManager.IsActionPermit("Add Middle Member", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }

        public JsonResult AddMiddleMemberByLoginID(MemberParent obj)
        {
            try
            {
                obj.PrdId = 2;  // ProcType for updation of member parent
                var result = new MemberParentService().AddMiddleMemberByLoginID(obj);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region  //Change Profile Add By Deepak Verma

        public JsonResult GetProfileDetails(string LoginId)
        {
            ChangeProfile para = new ChangeProfile();
            ChangeProfileRepository ar = new ChangeProfileRepository();
            ListtoDataTable ltdt = new ListtoDataTable();
            List<ChangeProfile> list = new List<ChangeProfile>();
            para.LoginId = LoginId;
            para.UpdatedBy = SessionManager.Fk_MemId.ToString();
            DataTable dt = ar.GetProfile(para);
            if (dt.Rows.Count > 0)
            {
                list = ltdt.ConvertDataTable<ChangeProfile>(dt);
            }
            return Json(list.FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeProfileList(ChangeProfile ObjNews)
        {

            if (!PermissionManager.IsActionPermit("Change Profile", ViewOptions.FormView))
            {
                return RedirectToAction("Index", "MainPage");
            }

            ChangeProfileRepository DB = new ChangeProfileRepository();
            // NewsMaster ObjNews = new NewsMaster();

            List<ChangeProfile> ChangeProfileList = new List<ChangeProfile>();

            DataSet Ds = DB.GetChangeProfileDetails(ObjNews);
            if (Ds != null && Ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Dr in Ds.Tables[0].Rows)
                {
                    ChangeProfile ObjData = new ChangeProfile();
                    ObjData.LoginId = Dr["LoginId"].ToString();
                    ObjData.NewName = Dr["NewName"].ToString();
                    ObjData.NewMobileNo = Dr["NewMobileNo"].ToString();
                    ObjData.NewEmailId = Dr["NewEmailId"].ToString();
                    ObjData.OldMobileNo = Dr["OldMobileNo"].ToString();
                    ObjData.OldEmailId = Dr["OldEmailId"].ToString();
                    ObjData.OldName = Dr["OldName"].ToString();
                    ObjData.Fk_MemId = Dr["Fk_MemId"].ToString();
                    ObjData.Pk_Id = Dr["Pk_Id"].ToString();

                    ChangeProfileList.Add(ObjData);
                }

                ObjNews.List = ChangeProfileList;
            }

            return View(ObjNews);
        }

        public ActionResult ChangeProfile(string id, string Pk_Id, ChangeProfileRepository DB)
        {

            if (!PermissionManager.IsActionPermit("ChangeProfile", ViewOptions.FormSave))
            {
                return RedirectToAction("Index", "MainPage");
            }
            
            ChangeProfile obj = new ChangeProfile();
            if (string.IsNullOrEmpty(id))
            {
                return View(obj);
            }
            obj.Fk_MemId = id;
            obj.Pk_Id = Pk_Id;
            //obj.NewsType = "O";
            DataSet Ds = DB.GetChangeProfileDetails(obj);
            if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                //obj.NewMobileNo = Ds.Tables[0].Rows[0]["MobileNo"].ToString();
                //obj.NewEmailId = Ds.Tables[0].Rows[0]["EmailId"].ToString();
                //obj.Fk_MemId = Ds.Tables[0].Rows[0]["ID"].ToString();
                obj.LoginId = Ds.Tables[0].Rows[0]["LoginId"].ToString();
                obj.OldName = Ds.Tables[0].Rows[0]["NewName"].ToString();
                obj.OldMobileNo = Ds.Tables[0].Rows[0]["NewMobileNo"].ToString();
                obj.OldEmailId = Ds.Tables[0].Rows[0]["NewEmailId"].ToString();
                obj.NewMobileNo = Ds.Tables[0].Rows[0]["OldMobileNo"].ToString();
                obj.NewEmailId = Ds.Tables[0].Rows[0]["OldEmailId"].ToString();
                obj.NewName = Ds.Tables[0].Rows[0]["OldName"].ToString();
                obj.Fk_MemId = Ds.Tables[0].Rows[0]["Fk_MemId"].ToString();
                //obj.CreatedDate = Ds.Tables[0].Rows[0]["CreatedDate"].ToString();

            }
            else
            {
                TempData["msg"] = "Problem while getting data";
                return RedirectToAction("ChangeProfileList", "MemberParent");
            }
            return View(obj);
        }

        [HttpPost]
        [ActionName("ChangeProfile")]
        [OnAction(ButtonName = "Save")]
        public ActionResult ChangeProfile(ChangeProfile obj, ChangeProfileRepository DB)
        {
            if (!PermissionManager.IsActionPermit("Change Profile", ViewOptions.FormSave))
            {
                return RedirectToAction("Index", "MainPage");
            }
            if (!ModelState.IsValid)
            {
                return View("ChangeProfile", obj);
            }
            obj.UpdatedBy = SessionManager.Fk_MemId.ToString();
            //obj.NewsType = "Associate";
            //obj.CreatedBy = "Ram";
            DataSet ds = DB.ChangeProfile(obj);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = "Saved Successfully";
                    return RedirectToAction("ChangeProfileList", "MemberParent");
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Remark"].ToString();
                    return RedirectToAction("ChangeProfile", "MemberParent");
                }
            }

            return View("ChangeProfileList", obj);
        }

        [ActionName("ChangeProfile")]
        [OnAction(ButtonName = "Update")]
        public ActionResult ChangeProfileUpdate(ChangeProfile obj, ChangeProfileRepository DB)
        {
            if (!PermissionManager.IsActionPermit("Change Profile", ViewOptions.FormUpdate))
            {
                return RedirectToAction("Index", "MainPage");
            }
            if (!ModelState.IsValid)
            {
                return View("Change Profile", obj);
            }
            obj.UpdatedBy = SessionManager.Fk_MemId.ToString();
            DataSet ds = DB.ChangeProfileUpdate(obj);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = "Profile Updated Successfully";
                    return RedirectToAction("ChangeProfileList", "MemberParent");
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Remark"].ToString();

                }

            }
            return RedirectToAction("ChangeProfileList", "MemberParent");
        }      
       
        #endregion

    }
}