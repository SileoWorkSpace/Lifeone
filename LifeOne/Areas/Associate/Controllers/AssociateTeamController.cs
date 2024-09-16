using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
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

namespace LifeOne.Areas.Associate.Controllers
{

    [SessionTimeoutAttribute]
    public class AssociateTeamController : Controller
    {
        AssociateTeam team = new AssociateTeam();
        // GET: Associate/AssociateTeam
        public ActionResult MyDirect(int? Page, string FK_MemId, AssociateTeam team)
        {

            int TotalRecords = 0;
            int PageSize = SessionManager.Size;
            int Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
            if (Page == null)
            {
                Session["MemId"] = null;
                Page = 1;
            }
            if (string.IsNullOrEmpty(FK_MemId) == false && FK_MemId != "0")
            {
                Fk_MemId = Convert.ToInt32(UrlEncodingDecoding.Decode(FK_MemId));
                Session["MemId"] = Fk_MemId;
            }
            else if (Session["MemId"] != null)
            {
                Fk_MemId = Convert.ToInt32(Session["MemId"]);
            }
            else
            {
                Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
                Session["MemId"] = Fk_MemId;
            }
            if (SessionManager.SponsorFk_MemId == Fk_MemId)
            {
                Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
            }
            team.JoiningDate = String.IsNullOrEmpty(team.JoiningDate) ? null : Common.ConvertToSystemDate(team.JoiningDate, "dd/MM/yyyy");
            team.Directs = AssociateTeamService.GetDirectList(Fk_MemId, team.SearchLoginId, team.Status, team.JoiningDate);
           
            return View(team);
        }

        public ActionResult ExportToExcelMyDirect(int? Page, string FK_MemId, AssociateTeam team)
        {
            int TotalRecords = 0;
            int PageSize = SessionManager.Size;
            int Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
            if (Page == null)
            {
                Session["MemId"] = null;
                Page = 1;
            }
            if (string.IsNullOrEmpty(FK_MemId) == false && FK_MemId != "0")
            {
                Fk_MemId = Convert.ToInt32(UrlEncodingDecoding.Decode(FK_MemId));
                Session["MemId"] = Fk_MemId;
            }
            else if (Session["MemId"] != null)
            {
                Fk_MemId = Convert.ToInt32(Session["MemId"]);
            }
            else
            {
                Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
                Session["MemId"] = Fk_MemId;
            }


            var customers = AssociateTeamService.GetDirect(Convert.ToInt32(Fk_MemId), team.SearchLoginId, Page, PageSize);
            DataTable dt = new DataTable("MyDirect");
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Login Id"),
                                             new DataColumn("Member Name"),
                                            new DataColumn("Self Business"),
                                            new DataColumn("Team Business")

            });
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.MemberLoginId, customer.MemberName, customer.SelfBusiness, customer.TeamBusiness);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MyDirect.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("MyDirect");
            }
        }

        public ActionResult MyDownline(int? Page, string FK_MemId, AssociateTeam team,string Leg, string FromDate, string ToDate)
        {
            int TotalRecords = 0;
            int PageSize = SessionManager.Size;
            int Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
            if (Page == null)
            {
                Session["MemId"] = null;
                Page = 1;
            }
            if (string.IsNullOrEmpty(FK_MemId) == false && FK_MemId != "0")
            {
                Fk_MemId = Convert.ToInt32(UrlEncodingDecoding.Decode(FK_MemId));
                Session["MemId"] = Fk_MemId;
            }
            else if (Session["MemId"] != null)
            {
                Fk_MemId = Convert.ToInt32(Session["MemId"]);
            }
            else
            {
                Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
                Session["MemId"] = Fk_MemId;
            }
            ViewBag.PackageList = DALBindCommonDropdown.PackageBindDropdown(16, 0);
            team.FromDate = String.IsNullOrEmpty(team.FromDate) ? null : Common.ConvertToSystemDate(team.FromDate, "dd/MM/yyyy");
            team.ToDate = String.IsNullOrEmpty(team.ToDate) ? null : Common.ConvertToSystemDate(team.ToDate, "dd/MM/yyyy");            
            team.Downlines = AssociateTeamService.GetDownlineList(Fk_MemId, team.SearchLoginId,team.Status, team.FromDate, team.ToDate, Page, PageSize,Leg,team.Pk_PackageID);
            //ViewBag.Email = team.Downlines.Select(x => x.Email).ToList();
            ViewBag.Email = DALBindCommonDropdown.BindDropdown(13, Fk_MemId);
            if (team.Downlines.Count > 0)
            {
                TotalRecords = team.Downlines[0].TotalRecords;
                var pager = new Pager(TotalRecords, Page, SessionManager.Size);
                team.Pager = pager;
                Session["TotalRecords"] = TotalRecords;
            }
            if(Leg=="L")
            {
                ViewBag.Leg = "Leg";
            }
            if (Leg == "R")
            {
                ViewBag.Leg = "Right";
            }
           
            return View(team);
        }

        public ActionResult ExportToExcelMyDownline(int? Page, string FK_MemId, AssociateTeam team,string Leg)
        {
            int PageSize = Convert.ToInt32(Session["TotalRecords"]);
            int Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
            if (Page == null)
            {
                Session["MemId"] = null;
                Page = 1;
            }
            if (string.IsNullOrEmpty(FK_MemId) == false && FK_MemId != "0")
            {
                Fk_MemId = Convert.ToInt32(UrlEncodingDecoding.Decode(FK_MemId));
                Session["MemId"] = Fk_MemId;
            }
            else if (Session["MemId"] != null)
            {
                Fk_MemId = Convert.ToInt32(Session["MemId"]);
            }
            else
            {
                Fk_MemId = Convert.ToInt32(SessionManager.AssociateFk_MemId);
                Session["MemId"] = Fk_MemId;
            }
            var customers = AssociateTeamService.GetDownline(Fk_MemId, team.SearchLoginId, Page, PageSize);
            DataTable dt = new DataTable("MyDirect");
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Login Id"),
                                             new DataColumn("Member Name"),
                                                new DataColumn("Self Business"),
                                            new DataColumn("Team Business")                              
            });
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.FirstName + " " + customer.LastName, customer.SelfBusiness, customer.TeamBusiness);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Downline.xlsx");
                    }
                }
            }
            else
            {
                TempData["alert"] = "Data Not Found";
                return Redirect("MyDownline");
            }
        }
        public ActionResult DownlineDocumentVerification()
        {
            if (TempData["Code"] == null || TempData["Code"].ToString() == "")
            {
                TempData["Code"] = "2";
            }
            return View();
        }
        public ActionResult DownlineDocumentVerificationAction(string LoginId)
        {
            if (TempData["Code"] == null || TempData["Code"].ToString() == "")
            {
                TempData["Code"] = "2";
            }
            KYCDetails ObjRes = new KYCDetails();
            ObjRes.Fk_MemId = 0;
            MemberKYC Obj = new MemberKYC();
            Obj.Fk_MemId = SessionManager.AssociateFk_MemId;
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
            Obj.CreatedBy = SessionManager.AssociateFk_MemId;
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
            return RedirectToAction("DownlineDocumentVerificationAction", new RouteValueDictionary(new { controller = "AssociateTeam", action = "DownlineDocumentVerificationAction", LoginId = Obj.LoginId }));
        }

        [HttpPost]
        [ActionName("DocumentVerifyAction1")]
        public ActionResult SaveKYCInfo1(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.AssociateFk_MemId;
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
            return RedirectToAction("DownlineDocumentVerificationAction", new RouteValueDictionary(new { controller = "AssociateTeam", action = "DownlineDocumentVerificationAction", LoginId = Obj.LoginId }));
        }


        [HttpPost]
        [ActionName("DocumentVerifyAction2")]
        public ActionResult SaveKYCInfo2(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.AssociateFk_MemId;
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
            return RedirectToAction("DownlineDocumentVerificationAction", new RouteValueDictionary(new { controller = "AssociateTeam", action = "DownlineDocumentVerificationAction", LoginId = Obj.LoginId }));
        }
        [HttpPost]
        [ActionName("DocumentVerifyAction4")]
        public ActionResult SaveKYCInfo3(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.AssociateFk_MemId;
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
            return RedirectToAction("DownlineDocumentVerificationAction", new RouteValueDictionary(new { controller = "AssociateTeam", action = "DownlineDocumentVerificationAction", LoginId = Obj.LoginId }));
        }
        [HttpPost]
        [ActionName("DocumentVerifyAction3")]
        public ActionResult SaveKYCInfo4(KYCDetails Obj)
        {
            Obj.CreatedBy = SessionManager.AssociateFk_MemId;
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
            return RedirectToAction("DownlineDocumentVerificationAction", new RouteValueDictionary(new { controller = "AssociateTeam", action = "DownlineDocumentVerificationAction", LoginId = Obj.LoginId }));
        }


        [HttpPost]
        public JsonResult SaveNotification(MNotification modelRequest)
        {

            NotificationResponse objres = new NotificationResponse();
            NotificationService _objService = new NotificationService();
            //modelRequest.Title = Title;
            //modelRequest.Notificationmessage = Notificationmessage;
            modelRequest.CreatedBy = SessionManager.AssociateFk_MemId;
            //modelRequest.lstMemberId = MemberId;
            var xmldata = "<Notification>";
            for (var i = 0; i < modelRequest.LstMemberId.Count(); i++)
            {
                xmldata += "<Notificationdata><Tittle>" + modelRequest.Title + "</Tittle><Message>" + modelRequest.Notificationmessage + "</Message><NotificationType>" + "Downline" + "</NotificationType><Fk_MemId>" + modelRequest.LstMemberId[i] + "</Fk_MemId><Fk_RequestId>" + modelRequest.CreatedBy + "</Fk_RequestId><CreatedBy>" + modelRequest.CreatedBy + "</CreatedBy></Notificationdata>";
            }
            xmldata += "</Notification>";
            try
            {
                var res = _objService.SaveNotification(xmldata);
                if (res != null && res.Flag == 1)
                {
                    objres.response = "success";
                    objres.Flag = res.Flag;
                    objres.Msg = res.Msg;
                }
                else
                {
                    objres.response = "error";
                    objres.Flag = res.Flag;
                    objres.Msg = res.Msg;

                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.Msg = ex.Message;
            }
            return Json(objres, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SaveSMS(MSMS modelRequest)
        {

            SMSResponse objres = new SMSResponse();
            SMSService _objService = new SMSService();
            modelRequest.CreatedBy = SessionManager.AssociateFk_MemId;
            var xmldata = "<SMS>";
            for (var i = 0; i < modelRequest.LstMemberId.Count(); i++)
            {
                xmldata += "<SMSData><Tittle>" + modelRequest.Title + "</Tittle><Message>" + modelRequest.SMSContent + "</Message><SMSType>" + "Downline" + "</SMSType><Fk_MemId>" + modelRequest.LstMemberId[i] + "</Fk_MemId><CreatedBy>" + modelRequest.CreatedBy + "</CreatedBy></SMSData>";
            }
            xmldata += "</SMS>";
            try
            {
                var res = _objService.SaveSMS(xmldata);
                if (res != null && res.Flag == 1)
                {
                    objres.response = "success";
                    objres.Flag = res.Flag;
                    objres.Msg = res.Msg;
                }
                else
                {
                    objres.response = "error";
                    objres.Flag = res.Flag;
                    objres.Msg = res.Msg;

                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.Msg = ex.Message;
            }
            return Json(objres, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult SaveEMail(MEmail modelRequest)
        {
            EmailResponse objres = new EmailResponse();
            EmailSevice _objService = new EmailSevice();
            modelRequest.CreatedBy = SessionManager.AssociateFk_MemId;
            var xmldata = "<Email>";
            for (var i = 0; i < modelRequest.LstMemberId.Count(); i++)
            {
                xmldata += "<EmailData><Tittle>" + modelRequest.Title + "</Tittle><Message>" + modelRequest.EmailContect + "</Message><EmailType>" + "Downline" + "</EmailType><Fk_MemId>" + modelRequest.LstMemberId[i] + "</Fk_MemId><EmailTo>" + modelRequest.Email[i] + "</EmailTo><EmailCC>" + modelRequest.EmailCC + "</EmailCC><EmailBCC>" + modelRequest.EmailBCC + "</EmailBCC><EmailAttachmentPath>" + modelRequest.FilePath + "</EmailAttachmentPath><CreatedBy>" + modelRequest.CreatedBy + "</CreatedBy></EmailData>";
            }
            xmldata += "</Email>";
            try
            {
                var res = _objService.SaveEmail(xmldata);
                if (res != null && res.Flag == 1)
                {
                    objres.response = "success";
                    objres.Flag = res.Flag;
                    objres.Msg = res.Msg;
                }
                else
                {
                    objres.response = "error";
                    objres.Flag = res.Flag;
                    objres.Msg = res.Msg;
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.Msg = ex.Message;
            }
            return Json(objres, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FranchiseList(int? page)
        {
            try
            {
                FranchiseList _objdetail = new FranchiseList();
                _objdetail.LoginId = SessionManager.LoginId;
                _objdetail.Size = SessionManager.Size;
                _objdetail.Type = "Web";
                if (page == null || page == 0)
                {
                    _objdetail.Page = 1;

                }
                _objdetail.FranchiseListlst = AssociateTeamService.getFranchiseDetail(_objdetail);
                return View(_objdetail);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public ActionResult AssociateDetails()
        {
            DistributorDetails model = new DistributorDetails();
            model.Fk_memId = SessionManager.AssociateFk_MemId.ToString();
          ViewBag.lst= AssociateTeamService.DistributorDetails(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult AssociateDetails(DistributorDetails model)
        {
            
            model.Fk_memId = SessionManager.AssociateFk_MemId.ToString();
            ViewBag.lst = AssociateTeamService.DistributorDetails(model);

            return View(model);
        }


    }
}