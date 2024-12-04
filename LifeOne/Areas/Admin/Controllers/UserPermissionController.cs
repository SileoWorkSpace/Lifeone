using LifeOne.Filters;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LifeOne.Models.AdminManagement.AModels;
using System.Web.Mvc;
namespace LifeOne.Areas.Admin.Controllers
{
	[AuthorizeAdmin]
    // [MenuPermissionFilter]
    public class UserPermissionController : Controller
    {
        // GET: Admin/UserPermission
        public ActionResult Index()
        {
            return View();
        }

        #region ----------Form Type----------

        [HttpGet]
        public ActionResult FormType()
        {
            if (!PermissionManager.IsActionPermit("Form Type", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }

        public ActionResult FormTypeList()
        {
            if (!PermissionManager.IsActionPermit("Form Type", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            FormTypeRepository obj = new FormTypeRepository();
            List<FormTypeModel> listoobj = obj.FormTypeList();
            return View(listoobj);
        }


        public ActionResult FormTypeDelete(string id)
        {
            if (!PermissionManager.IsActionPermit("Form Type", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            FormTypeModel obj = new FormTypeModel();
            string DeletedBy = SessionManager.Fk_MemId.ToString();
            FormTypeRepository objresps = new FormTypeRepository();
            DataSet ds = objresps.DeleteFormType(id, DeletedBy);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            return Redirect("/Form-Type-List");
        }

        [HttpPost]
        public ActionResult FormTypeInsert(FormTypeModel obj)
        {
            if (!PermissionManager.IsActionPermit("Form Type", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            obj.CreatedBy = SessionManager.Fk_MemId.ToString();
            FormTypeRepository objreps = new FormTypeRepository();
            DataSet ds = objreps.SaveFormType(obj);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                    return RedirectToAction("FormTypeList", "UserPermission");
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            return RedirectToAction("FormType", "UserPermission");
        }

        public ActionResult FormTypeFill(string id)
        {
            if (!PermissionManager.IsActionPermit("Form Type", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.ddlSite = FormTypeRepository.BindddlSite();
            FormTypeModel obj = new FormTypeModel();
            obj.PK_FormTypeId = id;
            FormTypeRepository objresps = new FormTypeRepository();
            FormTypeModel lstobj = objresps.EditFormType(id);
            return View("FormType", lstobj);
        }

        [HttpPost]
        [ActionName("FormTypeAction")]
        [OnAction(ButtonName = "Update")]
        public ActionResult FormTypeUpdate(FormTypeModel obj)
        {
            if (!PermissionManager.IsActionPermit("Form Type", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.ddlSite = FormTypeRepository.BindddlSite();
            FormTypeRepository objrpst = new FormTypeRepository();
            obj.UpdatedBy = SessionManager.Fk_MemId.ToString();
            DataSet ds = objrpst.UpdateFormType(obj);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                    return RedirectToAction("FormTypeList", "UserPermission");
                }
                else if (ds.Tables[0].Rows[0]["Code"].ToString() == "0")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                    return View("FormType", obj);
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            return View("FormType", obj);
        }

        #endregion

        #region ----------------Form Master--------------

        public ActionResult FormMaster()
        {
            if (!PermissionManager.IsActionPermit("Form Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.ddlFormType = FormMasterRepository.BindddlFormType();
            return View();
        }

        public ActionResult FormMasterList()
        {

            if (!PermissionManager.IsActionPermit("Form Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            FormMasterRepository obj = new FormMasterRepository();
            List<FormMasterModel> listoobj = obj.FormMasterList();
            return View(listoobj);
        }

        public ActionResult FormMasterDelete(string id)
        {
            if (!PermissionManager.IsActionPermit("Form Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            FormMasterModel obj = new FormMasterModel();
            string DeletedBy = SessionManager.Fk_MemId.ToString();
            FormMasterRepository objresps = new FormMasterRepository();
            DataSet ds = objresps.DeleteFormMaster(id, DeletedBy);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            return RedirectToAction("FormMasterList", "UserPermission");
        }

        [HttpPost]
        [ActionName("FormMasterAction")]
        [OnAction(ButtonName = "Save")]
        public ActionResult FormMasterInsert(FormMasterModel obj)
        {
            if (!PermissionManager.IsActionPermit("Form Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            obj.CreatedBy = SessionManager.Fk_MemId.ToString();
            FormMasterRepository objreps = new FormMasterRepository();
            DataSet ds = objreps.SaveFormMaster(obj);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                    return RedirectToAction("FormMasterList", "UserPermission");
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            return Redirect("/Form-Master");
        }

        public ActionResult FormMasterFill(string id)
        {
            if (!PermissionManager.IsActionPermit("Form Master", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.ddlFormType = FormMasterRepository.BindddlFormType();
            FormMasterModel obj = new FormMasterModel();
            obj.PK_FormId = id;
            FormMasterRepository objresps = new FormMasterRepository();
            FormMasterModel lstobj = objresps.EditFormMaster(id);
            return View("FormMaster", lstobj);
        }

        [HttpPost]
        [ActionName("FormMasterAction")]
        [OnAction(ButtonName = "Update")]
        public ActionResult FormMasterUpdate(FormMasterModel obj)
        {
            if (!PermissionManager.IsActionPermit("Form Master", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.ddlFormType = FormMasterRepository.BindddlFormType();
            FormMasterRepository objrpst = new FormMasterRepository();
            obj.UpdatedBy = SessionManager.Fk_MemId.ToString();
            DataSet ds = objrpst.UpdateFormMaster(obj);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                    return RedirectToAction("FormMasterList", "UserPermission");
                }
                else if (ds.Tables[0].Rows[0]["Code"].ToString() == "0")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                    return View("FormMaster", obj);
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            return View("FormMaster", obj);
        }

        #endregion FormMaster

        #region -------Role Permission---------------
        public ActionResult RolePermission()
        {
            if (!PermissionManager.IsActionPermit("Role Permission", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.ddlRoles = RoleMasterRepository.BindddlRoles();
            ViewBag.ddlFormType = RoleMasterRepository.BindddlFormType();
            ViewBag.RolePermissionFormList = RoleMasterRepository.BindRolePermissionFormList();
            return View();
        }


        public ActionResult RolePermissionList()
        {
            if (!PermissionManager.IsActionPermit("Role Permission", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            RolePermissionRepository obj = new RolePermissionRepository();
            List<RolePermissionModel> listoobj = obj.RolePermissionList().GroupBy(x => x.RoleName).Select(l => l.FirstOrDefault()).ToList();
            return View(listoobj);
        }                                                         


        public ActionResult RoleMaster()
        {
            if (!PermissionManager.IsActionPermit("Role Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            return View();
        }
        public ActionResult RoleMasterList()
        {

            if (!PermissionManager.IsActionPermit("Role Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            RoleMasterRepository obj = new RoleMasterRepository();
            List<RoleMasterModel> listoobj = obj.RoleMasterList();
            return View(listoobj);
        }


        [HttpPost]
        [ActionName("RoleMasterAction")]
        [OnAction(ButtonName = "Save")]
        public ActionResult RoleMasterInsert(RoleMasterModel obj)
        {
            if (!PermissionManager.IsActionPermit("Role Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            obj.CreatedBy = SessionManager.Fk_MemId.ToString();
            RoleMasterRepository objreps = new RoleMasterRepository();
            DataSet ds = objreps.SaveRoleMaster(obj);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                    return RedirectToAction("RoleMasterList", "UserPermission");
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            //            return RedirectToAction("RoleMaster", "UserPermission");
            return Redirect("/Role-Master");
        }


        [HttpPost]
        [ActionName("RoleMasterAction")]
        [OnAction(ButtonName = "Update")]
        public ActionResult RoleMasterUpdate(RoleMasterModel obj)
        {
            if (!PermissionManager.IsActionPermit("Role Master", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.ddlFormType = RoleMasterRepository.BindddlFormType();
            RoleMasterRepository objrpst = new RoleMasterRepository();
            obj.UpdatedBy = SessionManager.Fk_MemId.ToString();
            DataSet ds = objrpst.UpdateRoleMaster(obj);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                    return RedirectToAction("RoleMasterList", "UserPermission");
                }
                else if (ds.Tables[0].Rows[0]["Code"].ToString() == "0")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                    return View("RoleMaster", obj);
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            return View("RoleMaster", obj);
        }

        public ActionResult RoleMasterDelete(string id)
        {
            if (!PermissionManager.IsActionPermit("Role Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            RoleMasterModel obj = new RoleMasterModel();
            string DeletedBy = SessionManager.Fk_MemId.ToString();
            RoleMasterRepository objresps = new RoleMasterRepository();
            DataSet ds = objresps.DeleteRoleMaster(id, DeletedBy);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            //return RedirectToAction("RoleMasterList", "UserPermission");
            return Redirect("/Role-Master-List");
        }

        public ActionResult RoleMasterFill(string id)
        {
            if (!PermissionManager.IsActionPermit("Role Master", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }

            ViewBag.ddlFormType = RoleMasterRepository.BindddlFormType();
            RoleMasterModel obj = new RoleMasterModel();
            obj.PK_RoleId = id;
            RoleMasterRepository objresps = new RoleMasterRepository();
            RoleMasterModel lstobj = objresps.EditRoleMaster(id);
            return View("RoleMaster", lstobj);
        }

        [HttpPost]
        public JsonResult RolePermissionJsonInsert(RolePermissionModel roles)
        {

            if (roles != null)
            {

                //string Xml = string.Empty;
                //Xml += "<result>";
                //roles.RolePermissionList.ForEach(x =>
                //{
                //    if (x.FormView)
                //        Xml += "<data><FormTypeId>" + x.FK_FormTypeId + "</FormTypeId><FormId>" + x.FK_FormMasterId + "</FormId><FormView>" + x.FK_FormMasterId + "</FormView><FormSave>" + x.FK_FormMasterId + "</FormSave> <FormUpdate>" + x.FK_FormMasterId + "</FormUpdate> <FormDelete>" + x.FK_FormMasterId + "</FormDelete></data>  ";
                //});
                //Xml += "</result>";
                //LifeOne.Models.Common.AddUpdateResponseResponseModel response = RolePermissionRepository.SetMenuPermission(roles.RolePermissionList[0].FK_UserId ?? 0, roles.RolePermissionList[0].FK_RoleId ?? 0, SessionManager.Fk_MemId.ToString(), Xml);
                //if (response.Status)
                //    return Json(new { msg = response.Message, code = 1 });
                //else
                //    return Json(new { msg = response.Message, code = 0 });

                foreach (var obj in roles.RolePermissionList)
                {
                    var rpmObj = new RolePermissionModel();
                    rpmObj.CreatedBy = SessionManager.Fk_MemId.ToString();
                    rpmObj.FK_RoleId = obj.FK_RoleId;
                    rpmObj.FK_FormTypeId = obj.FK_FormTypeId;
                    rpmObj.FK_FormMasterId = obj.FK_FormMasterId;
                    rpmObj.FormSave = obj.FormSave;
                    rpmObj.FormUpdate = obj.FormUpdate;
                    rpmObj.FormDelete = obj.FormDelete;
                    rpmObj.FormView = obj.FormView;

                    RolePermissionRepository objreps = new RolePermissionRepository();
                    DataSet ds = objreps.SaveRolePermission(rpmObj);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                        {
                            TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                            TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                        }
                        else
                        {
                            TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                            TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                        }
                    }
                }
                //if (response.Status)
                //    return Json(new { msg = response.Message, code = 1 });
                //else
                //    return Json(new { msg = response.Message, code = 0 });
            }

            return Json(new { msg = TempData["msg"], code = TempData["code"] }); ;
        }

        public ActionResult RolePermissionFill(string roleId)
        {
           

            ViewBag.ddlRoles = RolePermissionRepository.BindddlRoles();
            ViewBag.ddlFormType = RolePermissionRepository.BindddlFormType();
            ViewBag.RolePermissionFormList = RolePermissionRepository.BindRolePermissionFormList();
            var obj = new RoleMasterModel();
            obj.PK_RoleId = roleId;
            var objresps = new RolePermissionRepository();
            var lstobj = objresps.EditRolePermission(roleId);
            return View("RolePermission", lstobj);
        }
        [HttpPost]
        public JsonResult RolePermissionJsonUpdate(RolePermissionModel roles)
        {
            if (roles != null)
            {
                foreach (var obj in roles.RolePermissionList)
                {
                    var rpmObj = new RolePermissionModel();
                    rpmObj.CreatedBy = SessionManager.Fk_MemId.ToString();
                    rpmObj.FK_RoleId = obj.FK_RoleId;
                    rpmObj.FK_FormTypeId = obj.FK_FormTypeId;
                    rpmObj.FK_FormMasterId = obj.FK_FormMasterId;
                    rpmObj.FormSave = obj.FormSave;
                    rpmObj.FormUpdate = obj.FormUpdate;
                    rpmObj.FormDelete = obj.FormDelete;
                    rpmObj.FormView = obj.FormView;
                    var objreps = new RolePermissionRepository();
                    DataSet ds = null;
                    if (string.IsNullOrEmpty(obj.PK_RolePermissionId))
                    {
                        ds = objreps.SaveRolePermission(rpmObj, true);
                    }
                    else
                    {
                        rpmObj.PK_RolePermissionId = obj.PK_RolePermissionId;
                        ds = objreps.UpdateRolePermission(rpmObj);
                    }

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                        {
                            TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                            TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                        }
                        else
                        {
                            TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                            TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                        }
                    }
                }
            }

            return Json(new { msg = TempData["msg"], code = TempData["code"] }); ;
        }
        #endregion

        #region ----------Set Menu Permission------------------
        public ActionResult SetPermission()
        {
            if (!PermissionManager.IsActionPermit("Set Permission", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.ddlRoles = FormPermissionRepository.BindddlRoles();
            ViewBag.ddlUsers = FormPermissionRepository.BindUserList();
            ViewBag.RolePermissionFormList = FormPermissionRepository.BindRolePermissionFormList();
            return View();
        }

        public ActionResult SetPermissionList()
        {
            if (!PermissionManager.IsActionPermit("Set Permission", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            FormPermissionRepository obj = new FormPermissionRepository();
            List<FormPermissionModel> listoobj = obj.FormPermissionList().GroupBy(x => x.FK_UserId).Select(l => l.FirstOrDefault()).ToList();
            return View(listoobj);
        }

        public ActionResult SetPermissionDelete(string id)
        {

            if (!PermissionManager.IsActionPermit("Set Permission", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            FormPermissionModel obj = new FormPermissionModel();
            string DeletedBy = SessionManager.Fk_MemId.ToString();
            FormPermissionRepository objresps = new FormPermissionRepository();
            DataSet ds = objresps.DeleteRolePermission(id, DeletedBy);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                }
            }
            return RedirectToAction("SetPermissionList", "UserPermission");
        }

        [HttpPost]
        public JsonResult SetPermissionJsonInsert(int userId, int roleId)
        {
            if (!PermissionManager.IsActionPermit("Set Permission", ViewOptions.FormSave))
            {
                return Json(new { msg = "Sorry you are not authorised!!!", code = "0" }); ;
            }

            if (userId != 0 && roleId != 0)
            {
                var rolePermissions = FormPermissionRepository.BindRolePermissionList(roleId);
                foreach (var obj in rolePermissions)
                {
                    var rpmObj = new FormPermissionModel();
                    rpmObj.CreatedBy = SessionManager.Fk_MemId.ToString();
                    rpmObj.FK_RoleId = obj.FK_RoleId;
                    rpmObj.FK_FormTypeId = obj.FK_FormTypeId;
                    rpmObj.FK_FormId = obj.FK_FormMasterId;
                    rpmObj.FK_UserId = userId;
                    rpmObj.FormSave = obj.FormSave;
                    rpmObj.FormUpdate = obj.FormUpdate;
                    rpmObj.FormDelete = obj.FormDelete;
                    rpmObj.FormView = obj.FormView;

                    FormPermissionRepository objreps = new FormPermissionRepository();
                    DataSet ds = objreps.SaveFormPermission(rpmObj);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                        {
                            TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                            TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                        }
                        else
                        {
                            TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                            TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                        }
                    }
                }
            }
            return Json(new { msg = TempData["msg"], code = TempData["code"] }); ;
        }

        [HttpPost]
        public JsonResult SetPermissionJsonUpdate(FormPermissionModel roles)
        {
            if (!PermissionManager.IsActionPermit("Set Permission", ViewOptions.FormUpdate))
            {
                return Json(new { msg = "Sorry you are not authorised!!!", code = "0" }); ;
            }
            if (roles != null && roles.FormPermissionList != null)
            {
                foreach (var obj in roles.FormPermissionList)
                {
                    var rpmObj = new FormPermissionModel();
                    rpmObj.CreatedBy = SessionManager.Fk_MemId.ToString();
                    rpmObj.FK_RoleId = obj.FK_RoleId;
                    rpmObj.FK_FormTypeId = obj.FK_FormTypeId;
                    rpmObj.FK_FormId = obj.FK_FormId;
                    rpmObj.FormSave = obj.FormSave;
                    rpmObj.FormUpdate = obj.FormUpdate;
                    rpmObj.FormDelete = obj.FormDelete;
                    rpmObj.FormView = obj.FormView;
                    rpmObj.FK_UserId = obj.FK_UserId;
                    FormPermissionRepository objreps = new FormPermissionRepository();
                    DataSet ds = null;
                    if (string.IsNullOrEmpty(obj.PK_PermissionId))
                    {
                        ds = objreps.SaveFormPermission(rpmObj, true);
                    }
                    else
                    {
                        rpmObj.PK_PermissionId = obj.PK_PermissionId;
                        ds = objreps.UpdateRolePermission(rpmObj);
                    }
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                        {
                            TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                            TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                        }
                        else
                        {
                            TempData["msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                            TempData["code"] = ds.Tables[0].Rows[0]["Code"].ToString();
                        }
                    }
                }
            }
            else
            {
                return Json(new { msg = "Record Updated Successfully", code = "1" }); ;
            }
            return Json(new { msg = TempData["msg"], code = TempData["code"] }); ;
        }

        public ActionResult SetPermissionFill(int userId, int roleId)
        {
            if (!PermissionManager.IsActionPermit("Set Permission", ViewOptions.FormUpdate))
            {
                return Redirect("/Home/adminlogin");
            }
            ViewBag.ddlRoles = FormPermissionRepository.BindddlRoles();
            ViewBag.ddlUsers = FormPermissionRepository.BindUserList();
            ViewBag.RolePermissionFormList = FormPermissionRepository.BindRolePermissionFormList(); 
            var obj = new FormPermissionModel();
            obj.FK_UserId = userId;
            var objresps = new FormPermissionRepository();
            var lstobj = objresps.EditFormPermission(userId);
            lstobj.FK_UserId = userId;
            lstobj.FK_RoleId = roleId;
            return View("SetPermission", lstobj);
        }

        public PartialViewResult GetPermissionTblList(int userId)
        {
            ViewBag.RolePermissionFormList = FormPermissionRepository.BindRolePermissionFormList();
            var objresps = new FormPermissionRepository();
            var lstobj = objresps.EditFormPermission(userId);
            lstobj.FK_UserId = userId;
            return PartialView("_GetFormTblPermission", lstobj);
        }

        public ActionResult SetUserPermission()
        {
            return View();
        }
        public JsonResult BindAllMenu()
        {
            return Json(FormPermissionRepository.BindAllMenu());
        }
        public JsonResult BindRole()
        {
            return Json(FormPermissionRepository.BindRoleMaster());
        }
        public JsonResult BindEmpRoleWise(int RoleId = 0)
        {
            return Json(FormPermissionRepository.BindEmpRoleIdWise(RoleId));
        }
        #endregion
    }
}