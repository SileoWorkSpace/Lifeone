using ClosedXML.Excel;
using LifeOne.Models.API;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class TeamController : Controller
    {
        // GET: Admin/Team
        AssociateTeam team = new AssociateTeam();
        [HttpGet]
        public ActionResult Direct(string loginId)
        {
            LifeOne.Models.AdminManagement.AEntity.UserDownlineReport model = new Models.AdminManagement.AEntity.UserDownlineReport();
            LifeOne.Models.AdminManagement.AEntity.UserDownlineList obj = new LifeOne.Models.AdminManagement.AEntity.UserDownlineList();
            if (!PermissionManager.IsActionPermit("Direct", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if(model.Page==null || model.Page==0)
            {
                model.Page = 1;
            }
            if (loginId != null)
            {


                //string dcdata = aesencryptdecrypt.Decrypt(loginId);
                model.LoginId = loginId;
                model.ObjList = obj.DownlineList(model);
                ViewBag.LoginId = loginId;
                Session["DownlineList"] = model.ObjList;
            }
            model.Size = SessionManager.Size;
          
            //List<LifeOne.Models.AdminManagement.AEntity.UserDownlineReport> objlist = obj.AdminDirectList(model);
            //model.ObjList = obj.AdminDirectList(model);
            //ViewBag.LoginId = model.LoginId;
            ViewBag.Place = model.Place;
            ViewBag.Status = model.TemPermanent;
            Session["DirectReport"] = model.ObjList;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Direct(LifeOne.Models.AdminManagement.AEntity.UserDownlineReport model, string loginId)
        {
            LifeOne.Models.AdminManagement.AEntity.UserDownlineList obj = new LifeOne.Models.AdminManagement.AEntity.UserDownlineList();
            if (!PermissionManager.IsActionPermit("Direct", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            if (model.Page == null || model.Page == 0)
            {
                model.Page = 1;
            }
            if (loginId != null)
            {


                //string dcdata = aesencryptdecrypt.Decrypt(loginId);
                model.LoginId = loginId;
                model.ObjList = obj.DownlineList(model);
                ViewBag.LoginId = loginId;
                Session["DownlineList"] = model.ObjList;
            }
            model.Size = SessionManager.Size;
         
            //List<LifeOne.Models.AdminManagement.AEntity.UserDownlineReport> objlist = obj.AdminDirectList(model);
            model.ObjList = obj.AdminDirectList(model);
            ViewBag.LoginId = model.LoginId;
            ViewBag.Place = model.Place;
            ViewBag.Status = model.TemPermanent;
            Session["DirectReport"] = model.ObjList;
            if (model.IsExport == 1)
            {
                model.ObjList = obj.AdminDirectList(model);
                DataTable dt = new DataTable("Direct Report");
                dt.Columns.AddRange(new DataColumn[10] {
                                            new DataColumn("Login Id"),
                                             new DataColumn("Direct Seller"),
                                            new DataColumn("Placement"),
                                            new DataColumn("Sponsor Login Id"),
                                            new DataColumn("Sponsor Name"),
                                            new DataColumn("Under Place Id"),
                                            new DataColumn("Under Place Name"),
                                             new DataColumn("Date Of Joining"),
                                            new DataColumn("Date Of Activation"),
                                            new DataColumn("Status")

            });
                if (model.ObjList.Count > 0)
                {
                    foreach (var customer in model.ObjList)
                    {
                        dt.Rows.Add(customer.LoginId, customer.Name, customer.Place, customer.SponsorId, customer.SponsorName, customer.ParentLoginId,
                            customer.ParentName, customer.JoiningDate, customer.PermanentDate, customer.TemPermanent
                               );
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Direct Report.xlsx");
                        }
                    }
                }
            }
            int totalRecords = 0;
            if (model.ObjList.Count > 0)
            {

                totalRecords = Convert.ToInt32(model.ObjList[0].TotalRecords);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                model.Pager = pager;
            }
            return View(model);
        }
        public ActionResult Downline(int? Page, string FK_MemId)
        {
            if (!PermissionManager.IsActionPermit("User Downline Report", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            int TotalRecords = 0;
            int PageSize = 10;
            int Fk_MemId = 0;
            if (Page == null)
            {
                Session["MemId"] = null;
                Page = 1;
            }
            if (string.IsNullOrEmpty(FK_MemId) == false && FK_MemId != "0")    
            {
                Fk_MemId = Convert.ToInt32(FK_MemId);
                Session["MemId"] = Fk_MemId;
            }
            else if (Session["MemId"] != null)
            {
                Fk_MemId = Convert.ToInt32(Session["MemId"]);
            }
            if (Fk_MemId > 0)
            {
                team.Downlines = AssociateTeamService.GetDownline(Fk_MemId, team.SearchLoginId, Page, PageSize);
                if (team.Downlines.Count > 0)
                {
                    TotalRecords = team.Downlines[0].TotalRecords;
                }
                var pager = new Pager(Convert.ToInt32(TotalRecords), Page);
                team.Pager = pager;
            }
            return View(team);
        }

        public JsonResult GetMemberById(string LoginId)
        {
            List<Member> members = new List<Member>();
            members = AssociateTeamService.GetMemberById(LoginId);
            if (members.Count > 0)
            {
                members[0].EncryptedId = UrlEncodingDecoding.Encode(members[0].FK_MemId.ToString());
            }
            return Json(members, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownlineDocumentVerification()
        {
            if (TempData["Code"] == null || TempData["Code"].ToString() == "")
            {
                TempData["Code"] = 2;
            }
            return View();
        }
        public ActionResult DownlineDocumentVerificationAction(string LoginId)
        {
            if (TempData["Code"] == null || TempData["Code"].ToString() == "")
            {
                TempData["Code"] = 2;
            }
            KYCDetails ObjRes = new KYCDetails();
            ObjRes.Fk_MemId = 0;
            MemberKYC Obj = new MemberKYC();
            Obj.Fk_MemId = SessionManager.Fk_MemId;
            Obj.LoginId = LoginId;
            if (!string.IsNullOrEmpty(LoginId))
            {
                ObjRes.lstKycDown = AssociateTeamService.GetMemberKYCListDownline(Obj);
                if (ObjRes.lstKycDown.Count == 0)
                {
                    TempData["msg"] = "No Data Found";
                    TempData["code"] = "0";
                }
                else
                {
                    ObjRes.Fk_MemId = ObjRes.lstKycDown[0].Fk_MemId;
                    TempData["msg"] = "Success";
                    TempData["code"] = "1";
                }
            }
            else
            {
                TempData["msg"] = "Please Enter Associate ID";
                TempData["code"] = "0";
            }
            return View("DownlineDocumentVerification", ObjRes);
        }

        [HttpPost]
        [ActionName("DocumentVerifyAction")]
        public ActionResult SaveKYCInfo(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.Fk_MemId;
            Obj.Action = "Profile";
            if (Request.Files[0].ContentLength > 0)
            {
                Obj.ProfilePic = Obj.profileimagepath;

            }
            KYCService _objService = new KYCService();
            KYCResponse objres = new KYCResponse();
            objres = _objService.SaveKYC(Obj);
            if (objres != null)
            {
                if (objres.Flag == 1)
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
                else
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
            }
            else
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = "Something went wrong";
            }
            return RedirectToAction("DownlineDocumentVerificationAction", new RouteValueDictionary(new { controller = "Team", action = "DownlineDocumentVerificationAction", LoginId = Obj.LoginId }));
        }

        [HttpPost]
        [ActionName("DocumentVerifyAction1")]
        public ActionResult SaveKYCInfo1(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.Fk_MemId;
            Obj.Action = "PAN";
            if (Request.Files[0].ContentLength > 0)
            {
                Obj.PanImage = Obj.pancardimagepath;
            }
            KYCService _objService = new KYCService();
            KYCResponse objres = new KYCResponse();
            objres = _objService.SaveKYC(Obj);
            if (objres != null)
            {
                if (objres.Flag == 1)
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
                else
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
            }
            else
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = "Something went wrong";
            }
            return RedirectToAction("DownlineDocumentVerificationAction", new RouteValueDictionary(new { controller = "Team", action = "DownlineDocumentVerificationAction", LoginId = Obj.LoginId }));
        }


        [HttpPost]
        [ActionName("DocumentVerifyAction2")]
        public ActionResult SaveKYCInfo2(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.Fk_MemId;
            Obj.Action = "AADHARFRONT";
            if (Request.Files[0].ContentLength > 0)
            {
                Obj.AdharImg = Obj.aadharfrontimagepath;
            }
            KYCService _objService = new KYCService();
            KYCResponse objres = new KYCResponse();
            objres = _objService.SaveKYC(Obj);
            if (objres != null)
            {
                if (objres.Flag == 1)
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
                else
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
            }
            else
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = "Something went wrong";
            }
            return RedirectToAction("DownlineDocumentVerificationAction", new RouteValueDictionary(new { controller = "Team", action = "DownlineDocumentVerificationAction", LoginId = Obj.LoginId }));
        }
        [HttpPost]
        [ActionName("DocumentVerifyAction4")]
        public ActionResult SaveKYCInfo3(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.Fk_MemId;
            Obj.Action = "CHEQUE";
            if (Request.Files[0].ContentLength > 0)
            {
                Obj.ChequeBookImg = Obj.chequeimagepath;
            }
            KYCService _objService = new KYCService();
            KYCResponse objres = new KYCResponse();
            objres = _objService.SaveKYC(Obj);
            if (objres != null)
            {
                if (objres.Flag == 1)
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
                else
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
            }
            else
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = "Something went wrong";
            }
            return RedirectToAction("DownlineDocumentVerificationAction", new RouteValueDictionary(new { controller = "Team", action = "DownlineDocumentVerificationAction", LoginId = Obj.LoginId }));
        }
        [HttpPost]
        [ActionName("DocumentVerifyAction3")]
        public ActionResult SaveKYCInfo4(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.Fk_MemId;
            Obj.Action = "AADHARBACK";
            if (Request.Files[0].ContentLength > 0)
            {
                Obj.addProofImg = Obj.aadharbackimagepath;
            }
            KYCService _objService = new KYCService();
            KYCResponse objres = new KYCResponse();
            objres = _objService.SaveKYC(Obj);
            if (objres != null)
            {
                if (objres.Flag == 1)
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
                else
                {
                    TempData["Code"] = objres.Flag;
                    TempData["Document"] = objres.Msg;
                }
            }
            else
            {
                TempData["Code"] = objres.Flag;
                TempData["Document"] = "Something went wrong";
            }
            return RedirectToAction("DownlineDocumentVerificationAction", new RouteValueDictionary(new { controller = "Team", action = "DownlineDocumentVerificationAction", LoginId = Obj.LoginId }));
        }
        public ActionResult Upline(LifeOne.Models.AdminManagement.AEntity.MUpline obj, int Page = 0)
        {
            if (!PermissionManager.IsActionPermit("UpLine", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }
            try
            {
                if (Page < 1)
                {
                    obj.Page = 1;
                }
                else
                {

                    obj.LoginId = obj.LoginId == null ? (string)TempData.Peek("LoginId") : obj.LoginId;
                    obj.Page = Page;
                }
                LifeOne.Models.AdminManagement.AService.UplineService objqq = new LifeOne.Models.AdminManagement.AService.UplineService();
                var list = objqq.uplinesDataService(obj);
                obj.MUplineList = list;
                TempData["LoginId"] = obj.LoginId;
                TempData.Keep("LoginId");
                obj.Pager = new Pager(Convert.ToInt32(list[0].TotalRecords), obj.Page, SessionManager.Size);

                return View(obj);

            }
            catch (Exception ex)
            {
            }
            return View();

        }
        public ActionResult ExportToExcelDirectReport()
        {

            DataTable dt = new DataTable("DirectReport");
            dt.Columns.AddRange(new DataColumn[10] {
                                            new DataColumn("LoginId"),
                                             new DataColumn("Member Name"),
                                            new DataColumn("Placement"),
                                            new DataColumn("SponsorLoginId"),
                                            new DataColumn("SponsorName"),
                                            new DataColumn("UnderPlaceId"),
                                            new DataColumn("UnderPlaceName"),
                                             new DataColumn("DOJ"),
                                            new DataColumn("DOA"),
                                            new DataColumn("Status")

            });

            var customers = (List<LifeOne.Models.AdminManagement.AEntity.UserDownlineReport>)Session["DirectReport"];
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.Name, customer.Place, customer.SponsorId, customer.SponsorName, customer.ParentLoginId,
                        customer.ParentName, customer.JoiningDate, customer.PermanentDate, customer.TemPermanent
                           );
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AdminDirectReport.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("/Direct");
            }
        }
    }
}