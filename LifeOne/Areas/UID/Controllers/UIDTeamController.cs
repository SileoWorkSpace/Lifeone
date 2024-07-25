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

namespace LifeOne.Areas.UID.Controllers
{

    [SessionTimeoutAttribute]
    public class UIDTeamController : Controller
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
            team.Directs = AssociateTeamService.GetDirect(Fk_MemId, team.SearchLoginId, Page, PageSize);
            if (team.Directs.Count > 0)
            {
                TotalRecords = team.Directs[0].TotalRecords;                
                var pager = new Pager(TotalRecords, Page, SessionManager.Size);
                team.Pager = pager;
            }
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
            dt.Columns.AddRange(new DataColumn[11] { new DataColumn("Login Id"),
                                             new DataColumn("Member Name"),
                                            new DataColumn("Type"),
                                            new DataColumn("Status"),
                                            new DataColumn("Direct Count(BID/UID)"),
                                            new DataColumn("Current PPV"),
                                            new DataColumn("Current UPV"),
                                            new DataColumn("Current GPV"),
                                            new DataColumn("Current QPPV"),
                                            new DataColumn("Current QUPV"),
                                            new DataColumn("Current QGPV"),

            });
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.MemberLoginId, customer.MemberName, customer.Type, customer.Status, customer.BIDCount + "/" + customer.UIDCount, customer.CurrPBV, customer.CurrTBV, customer.CurrGBV,
                        customer.TotalCurrPBV, customer.TotalCurrTBV, customer.TotalCurrGBV);
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

        public ActionResult MyDownline(int? Page, string FK_MemId, AssociateTeam team)
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
            team.Downlines = AssociateTeamService.GetDownline(Fk_MemId, team.SearchLoginId, Page, PageSize);
            if (team.Downlines.Count > 0)
            {
                TotalRecords = team.Downlines[0].TotalRecords;
                var pager = new Pager(Convert.ToInt32(TotalRecords), Page,SessionManager.Size);
                team.Pager = pager;
            }
            
            return View(team);
        }

        public ActionResult ExportToExcelMyDownline(int? Page, string FK_MemId, AssociateTeam team)
        {
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
            var customers = AssociateTeamService.GetDownline(Fk_MemId, team.SearchLoginId, Page, PageSize);
            DataTable dt = new DataTable("MyDirect");
            dt.Columns.AddRange(new DataColumn[11] { new DataColumn("Login Id"),
                                             new DataColumn("Member Name"),
                                                new DataColumn("Type"),
                                            new DataColumn("Status"),
                                            new DataColumn("Team Count(BID/UID)"),
                                            new DataColumn("Current PPV"),
                                            new DataColumn("Current UPV"),
                                            new DataColumn("Current GPV"),
                                            new DataColumn("Current QPPV"),
                                            new DataColumn("Current QUPV"),
                                            new DataColumn("Current QGPV")

            });
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    dt.Rows.Add(customer.LoginId, customer.FirstName + " " + customer.LastName, customer.Type, customer.Status, customer.BIDCount + "/" + customer.UIDCount, customer.CurrPBV, customer.CurrTBV, customer.CurrGBV,
                        customer.TotalCurrPBV, customer.TotalCurrTBV, customer.TotalCurrGBV);
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


    }
}