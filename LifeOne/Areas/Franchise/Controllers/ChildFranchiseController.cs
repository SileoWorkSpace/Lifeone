using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;
using LifeOne.Models.Common;
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
using LifeOne.Filters;

namespace LifeOne.Areas.Franchise.Controllers
{
    [AuthorizeAdmin]
    public class ChildFranchiseController : Controller
    {
        // GET: Franchise/ViewChildFranchise
        public ActionResult ViewChildFranchise()
        {
            return View();
        }

        // GET: Franchise/ChildFranchiseRegistration
      
        ChildFranchiseService _objService = new ChildFranchiseService();      

        [HttpGet]
        public ActionResult ChildFranchiseList()
        {
           
            MChildFranchise obj = new MChildFranchise();
            List<MChildFranchise> objlst = new List<MChildFranchise>();
            try
            {

                objlst = _objService.ChildFranchiseGetService(obj);
                if (objlst != null)
                {
                    obj.FranchiseMasterList = objlst;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult ChildFranchiseRegistration(string id)
        {
            MChildFranchise obj = new MChildFranchise();
            if (id != null)
            {
                obj.PKFranchiseRegistrationId = id;
                
                try
                {

                    obj = _objService.ChildFranchiseEditService(obj);



                }
                catch (Exception Ex)
                {
                    TempData["code"] = "0";
                    TempData["msg"] = Ex.Message.ToString();
                }
            }


            return View(obj);
        }

        [HttpPost]
        public ActionResult ChildFranchiseMaster(MChildFranchise obj)
        {
            ChildFranchiseResponseMaster _result = new ChildFranchiseResponseMaster();
            try
            {
                obj.FranchiseType = "Franchise";
                _result = _objService.ChildFranchiseSaveService(obj);
                if (_result != null)
                {

                    TempData["code"] = _result.Flag.ToString();
                    TempData["msg"] = _result.Msg.ToString();
                    return RedirectToAction("ChildFranchiseList", "ChildFranchise");
                }

                else
                {
                    TempData["code"] = "0";
                    TempData["msg"] = "Can not process the request";

                }

            }
            catch (Exception Ex)
            {
               
                TempData["code"] = "0";
                TempData["msg"] = Ex.Message.ToString();
            }

            return RedirectToAction("ChildFranchiseRegistration", "ChildFranchise");
        }

        [HttpGet]
        public ActionResult ChildFranchiseMasterDelete(string id)
        {
            ChildFranchiseResponseMaster obj = new ChildFranchiseResponseMaster();
            try
            {
                obj = _objService.ChildFranchiseDeleteService(id);
                if (obj != null)
                {

                    TempData["code"] = obj.Flag.ToString();
                    TempData["msg"] = obj.Msg.ToString();
                }

                else
                {
                    TempData["code"] = "0";
                    TempData["msg"] = "Can not process the request";
                }

            }
            catch (Exception Ex)
            {
                TempData["code"] = "0";
                TempData["msg"] = Ex.Message.ToString();
            }

            return RedirectToAction("ChildFranchiseList", "ChildFranchise");

        }

        [HttpGet]
        public ActionResult ChildFranchiseMasterBlockUnblock(string id)
        {
            ChildFranchiseResponseMaster obj = new ChildFranchiseResponseMaster();
            try
            {
                obj = _objService.ChildFranchiseBlockunblockService(id);
                if (obj != null)
                {

                    TempData["code"] = obj.Flag.ToString();
                    TempData["msg"] = obj.Msg.ToString();
                }

                else
                {
                    TempData["code"] = "0";
                    TempData["msg"] = "Can not process the request";
                }

            }
            catch (Exception Ex)
            {
                TempData["code"] = "0";
                TempData["msg"] = Ex.Message.ToString();
            }

            return RedirectToAction("ChildFranchiseList", "ChildFranchise");

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
                        // foreach (DataRow row in dt.Rows)
                        // {
                        list.Add(new PostOffice
                        {
                            District = dt.Rows[0]["District"].ToString(),
                            State = dt.Rows[0]["State"].ToString(),
                        });
                        // }
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
            return Json(list, JsonRequestBehavior.AllowGet);
            // return list;
        }
    }
}