using LifeOne.Filters;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;
using LifeOne.Models.Manager;
using LitJson;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LifeOne.Models;

namespace LifeOne.Areas.Franchise.Controllers
{
  [SessionTimeoutAttributeFranchise]
    public class FranchiseProfileController : Controller
    {
        // GET: Franchise/FranchiseProfile

        FranchiseMasterService _objService = new FranchiseMasterService();
        public ActionResult Index()
        {
            return View();
        }

        #region ChangePassword
        public ActionResult FranchiseChangePassword()
        {

            return View();
        }
        [HttpPost]
        public ActionResult FranchiseChangePassword(MFranchiseProfile model)
        {
            ResponseFranchiseProfileModel _result = new ResponseFranchiseProfileModel();
            try
            {
                FranchiseProfileService _objService = new FranchiseProfileService();

                model.PKFranchiseRegistrationId = SessionManager.FranchiseFk_MemId;
                model.Normalpassword = model.NewPassword;
                model.OldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.OldPassword, "md5");
                model.NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.NewPassword, "md5");
                _result = _objService.FranchiseChangePasswordService(model);
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
            return RedirectToAction("FranchiseChangePassword", "FranchiseProfile");
        }

        #endregion

        #region Profile

        public ActionResult FranchiseProfile(MFranchiseProfile model)
        {
            MFranchiseProfile _result = new MFranchiseProfile();
            try
            {

                FranchiseProfileService _objService = new FranchiseProfileService();
                model.LoginId = SessionManager.LoginId;
                _result.objList = _objService.GetFranchiseProfileService(model);

            }
            catch (Exception)
            {
                throw;
            }
            return View(_result);
        }
        #endregion

        public ActionResult FranchiseMaster()
        {
            MAdminFranchise obj = new MAdminFranchise();
            if (SessionManager.FranchiseFk_MemId > 0)
            {
                obj.PKFranchiseRegistrationId = SessionManager.FranchiseFk_MemId.ToString();
                try
                {
                    obj = _objService.FranchiseMasterEditService(obj);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(obj);
        }

        [HttpPost]
        public ActionResult FranchiseMaster(MAdminFranchise obj)
        {
            FranchiseResponseMaster _result = new FranchiseResponseMaster();
            try
            {

                _result = _objService.FranchiseMasterSaveService(obj);
                if (_result != null)
                {

                    TempData["code"] = _result.Flag.ToString();
                    TempData["msg"] = _result.Msg.ToString();
                    return RedirectToAction("FranchiseMaster");
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

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetLocationByPin(int pin)
        {

            PostOfficeResult postOfficeResult = null;
            List<PostOfficeResult> lists = new List<PostOfficeResult>();
            List<PostOffice> list = new List<PostOffice>();
            string userAuthenticationURI = "http://postalpincode.in/api/pincode/" + pin;

            if (!string.IsNullOrEmpty(userAuthenticationURI))
            {
                ListtoDataTable LDT = new ListtoDataTable();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userAuthenticationURI);
                request.Method = "GET";
                request.ContentType = "application/json";
                WebResponse response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var ApiStatus = reader.ReadToEnd();
                    JsonData data = JsonMapper.ToObject(ApiStatus);
                    string status = data["Status"].ToString();
                    if (status.ToLower() == "success")
                    {
                        postOfficeResult = JsonMapper.ToObject<PostOfficeResult>(ApiStatus);

                    }
                    if (postOfficeResult != null)
                    {
                        DataTable dt = LDT.ToDataTable(postOfficeResult.PostOffice);
                        foreach (DataRow row in dt.Rows)
                        {
                            list.Add(new PostOffice
                            {

                                District = row["District"].ToString(),
                                State = row["State"].ToString(),
                                Taluk = row["Taluk"].ToString(),
                                Circle = row["Circle"].ToString(),
                                Division = row["Division"].ToString(),
                                Region = row["Region"].ToString()
                            });
                        }
                    }
                    else
                    {
                        list.Add(new PostOffice
                        {
                            Responce = "No Data Found",
                        });
                    }
                }
            }


            var data1 = list.Distinct().GroupBy(y => new { y.District, y.State }).Select(y => new
            {
                State = y.FirstOrDefault().State,
                District = y.FirstOrDefault().District,
                DistrictDetail = y.ToList().Distinct().GroupBy(x => x.District).Select(x => new
                {
                    Taluka = x.FirstOrDefault().Taluk,
                    Circle = x.FirstOrDefault().Circle,
                    Zone = x.FirstOrDefault().Division,
                    Region = x.FirstOrDefault().Region
                }).ToList()
            }).Distinct().ToList();
            return Json(data1, JsonRequestBehavior.AllowGet);
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


        public ActionResult FranchiseeKYC(VerifyPanOrVoterid KYC)
        {
            Reports reports = new Reports();
            KYC.Fk_MemId = long.Parse(SessionManager.FranchiseFk_MemId.ToString());
            DataSet dataSet = reports.GetFranchiseeKYCDetails(KYC);

            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {

                    KYC.Name = dr["PersonName"].ToString();
                    KYC.LoginId = dr["LoginID"].ToString();
                    KYC.Mobile = dr["Contact"].ToString();
                    KYC.PanCard = dr["PanCard"].ToString();
                    KYC.PanImage = dr["PanImage"].ToString();
                    KYC.AadharImage = dr["AadharImage"].ToString();
                    KYC.AadharBackImage = dr["AadharBackImage"].ToString();
                    KYC.AadharNumber = dr["AadharNumber"].ToString();
                    KYC.BankImage = dr["BankDoc"].ToString();
                    KYC.BankName = dr["BankName"].ToString();
                    KYC.AccountHolder = dr["AccountHolder"].ToString();
                    KYC.AccountNumber = dr["AccountNumber"].ToString();
                    KYC.IfscCode = dr["IfscCode"].ToString();
                    KYC.Fk_MemId = long.Parse(dr["Fk_MemId"].ToString());
                    KYC.PanCardStatus = dr["PanStatus"].ToString();
                    KYC.BankStatus = dr["BankStatus"].ToString();
                    KYC.AddressProofBackStatus = dr["AadharBackStatus"].ToString();
                    KYC.AddressProofStatus = dr["AadharStatus"].ToString();
                    KYC.Flag = int.Parse(dr["Code"].ToString());
                }
            }
            return View(KYC);
        }
        public JsonResult UploadFranchiseeKyc(MAdminUploadKyc model)
        {
            Reports reports = new Reports();
            model.Fk_MemId = int.Parse(SessionManager.FranchiseFk_MemId.ToString());
            DataSet dataSet = reports.UploadFranchiseeKYC(model);
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    model.Code = int.Parse(dr["Code"].ToString());
                    model.Remark = dr["Remark"].ToString();
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}