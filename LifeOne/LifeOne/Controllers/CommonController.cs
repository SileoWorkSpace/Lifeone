using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Uploadfile(HttpPostedFileBase file, string subdir)
        {
            MFileUpload obj = new MFileUpload();
            if (file != null && file.ContentLength > 0)
            {
                var ImagePath = "~/ReadWriteData/" + subdir + "/";
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var filesize = Math.Round(Convert.ToDouble(file.ContentLength / 1024));
                if (filesize > 20)
                {
                    obj.Msg = "File is too Big. Please select a file less then 20mb !";
                    obj.Result = "warning";
                }
                if (extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPG" || extension.ToUpper() == ".JPEG" || extension.ToUpper() == ".BMP" || extension.ToUpper() == ".GIF" || extension.ToUpper() == ".PPT" || extension.ToUpper() == ".PPTX" || extension.ToUpper() == ".CGI" || extension.ToUpper() == ".XLX" || extension.ToUpper() == ".XLSX" || extension.ToUpper() == ".PDF" || extension.ToUpper() == ".EX4" || extension.ToUpper() == ".DOC" || extension.ToUpper() == ".DOCX" || extension.ToUpper() == ".TXT")
                {
                    if (!Directory.Exists(Server.MapPath(ImagePath)))
                    {
                        Directory.CreateDirectory((Server.MapPath(ImagePath)));
                    }
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;
                    file.SaveAs(Path.Combine(Server.MapPath(ImagePath), fileName));
                    obj.Msg = "file upload successfully!";
                    obj.Path = ImagePath + "" + fileName;
                    obj.Result = "success";
                    obj.FileName = fileName;
                }
                else
                {
                    obj.Msg = "Invalid file format !";
                    obj.Result = "warning";
                }
            }
            else
            {
                obj.Msg = "Please select file to upload!";
                obj.Result = "warning";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}