using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class NewsMasterController : Controller
    {
        // GET: Admin/NewsMaster
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult News(MNews model)
        {
            if (!PermissionManager.IsActionPermit("News Master", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            MNews _result = new MNews();
            try
            {
                model.ProcId = 1;
                NewsService _objService = new NewsService();

                _result.objList = _objService.GetNewsService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        [HttpPost]
        public ActionResult NewsSave(MNews model)
        {
            if (!PermissionManager.IsActionPermit("News Master", ViewOptions.FormSave))
            {
                return Redirect("/Home/adminlogin");
            }
            ResponseNewsModel _result = new ResponseNewsModel();
            try
            {
                if (model.Pk_NewsId != 0)
                { model.ProcId = 2; }
                else
                { model.ProcId = 1; }
                NewsService _objService = new NewsService();
                if (model.AssignTo.Count > 0)
                    model.AssignToValue = String.Join(",", model.AssignTo);

                _result = _objService.SaveNewsService(model);
                if (_result != null)
                {

                    TempData["code"] = _result.Flag;
                    TempData["msg"] = _result.Msg;
                }

                else
                {
                    TempData["code"] = "0";
                    TempData["msg"] = "Can not process the request";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return Redirect("/News");
        }
        public ActionResult NewsDelete(int Id)
        {
            if (!PermissionManager.IsActionPermit("News Master", ViewOptions.FormDelete))
            {
                return Redirect("/Home/adminlogin");
            }
            MNews model = new MNews();
            model.Pk_NewsId = Id;
            model.ProcId = 4;
            ResponseNewsModel _result = new ResponseNewsModel();
            try
            {

                NewsService _objService = new NewsService();

                _result = _objService.SaveNewsService(model);
                if (_result != null)
                {

                    TempData["code"] = _result.Flag;
                    TempData["msg"] = _result.Msg;
                }

                else
                {
                    TempData["code"] = "0";
                    TempData["msg"] = "Can not process the request";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return Redirect("/News");
        }

        public JsonResult PerformanceLevelList()
        {
            return Json(DALBindCommonDropdown.BindDropdown(10, 0), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Uploadfile(HttpPostedFileBase file, string subdir)
        {
            var objres = (object)null;

            if (file != null && file.ContentLength > 0)
            {
                var ImagePath = "~/ReadWriteData/" + subdir + "/";
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".BMP" || extension.ToUpper() == ".PDF" || extension.ToUpper() == ".DOCX" || extension.ToUpper() == ".XLSX")
                {
                    if (!Directory.Exists(Server.MapPath(ImagePath)))
                    {
                        Directory.CreateDirectory((Server.MapPath(ImagePath)));
                    }
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;


                    file.SaveAs(Path.Combine(Server.MapPath(ImagePath), fileName));
                    objres = new { Msg = "file upload successfully!", Path = ImagePath + "" + fileName, result = "success" };
                }
                else
                {
                    objres = new { Msg = "Only jpg,jpeg,png,bmp,pdf,docx,xlsx files are allow !", result = "warning" };
                }
            }
            else
            {
                objres = new { Msg = "Please select file to upload!", result = "warning" };
            }
            return Json(objres, JsonRequestBehavior.AllowGet);
        }
    }
}