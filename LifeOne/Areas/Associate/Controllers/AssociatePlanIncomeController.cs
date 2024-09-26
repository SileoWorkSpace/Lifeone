using DocumentFormat.OpenXml.EMMA;
using iTextSharp.text.pdf;
using LifeOne.Models;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.API;
using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.BillAvenueUtility.Entity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using Newtonsoft.Json;
using Razorpay.Api;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static LifeOne.Models.API.Common;

namespace LifeOne.Areas.Associate.Controllers
{

    [SessionTimeoutAttribute]
    public class AssociatePlanIncomeController : Controller
    {
        // GET: Associate/AssociatePlanIncome
        public ActionResult IncomeDetails(int? Page, string PlanId)
        {
            AssciateIncomeModel mdl = new AssciateIncomeModel();

            AssociateIncomeDAL obj = new AssociateIncomeDAL();

            if (string.IsNullOrEmpty(SessionManager.LoginId))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            if (Page == null || Page == 0)
                Page = 1;
            mdl.Page = Page;
            mdl.PlanId = PlanId;
            mdl.LoginID = SessionManager.LoginId;
            mdl.AssciateIncomeList = obj.GetIncomeDetails(mdl);

            foreach (var item in mdl.AssciateIncomeList)
            {
                string IncomeType = item.PlanName;
                ViewBag.IncomeType = IncomeType;
                break;
            }
            int totalRecords = 0;
            if (mdl.AssciateIncomeList.Count > 0)
            {

                totalRecords = Convert.ToInt32(mdl.AssciateIncomeList[0].TotalRecords);
                var pager = new Pager(totalRecords, mdl.Page, SessionManager.Size);
                mdl.Pager = pager;
            }
            else
            {
                mdl.AssciateIncomeList = new List<AssciateIncomeModel>();
            }
            return View(mdl);
        }
        [HttpGet]
        public ActionResult EWalletList()
        {

            Models.AssociateManagement.AssociateEntity.AssosiateRequest Entity = new Models.AssociateManagement.AssociateEntity.AssosiateRequest();
            Models.AssociateManagement.AssociateDAL.RequestDAL para = new Models.AssociateManagement.AssociateDAL.RequestDAL();
            Entity.LoginId = Convert.ToString(Session["AssociateFk_MemId"] ?? "0");

            Entity.lstAssosiateRequest = para.getRequest(Entity);
            return View(Entity);

        }
        public ActionResult EWalletRequest(Models.AssociateManagement.AssociateEntity.AssosiateRequest Entity, string Save)
        {
            try
            {
                Entity.LoginId = Convert.ToString(Session["LoginId"] ?? "0");
                Entity.Name = Convert.ToString(Session["Name"] ?? null);
                #region BankDll
                Models.AssociateManagement.AssociateDAL.RequestDAL obj = new Models.AssociateManagement.AssociateDAL.RequestDAL();
                ViewBag.BankDDL = obj.GetBank(Entity);
                #endregion


                return View(Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public ActionResult PaymentSuccess(string paymentid)
        {

            AssosiateRequest Entity = new AssosiateRequest();
            Entity.ChequeDDNo = Session["status"].ToString();
            Entity.AddedBy = Convert.ToInt32(Session["AssociateFk_MemId"] ?? "0");
            Entity.paymentid = paymentid;
            DataSet dataSet = Entity.UpdateWalletByGateway();

            return View(Entity);
        }
        public ActionResult RequestEwallet(string LoginId, int Amount, string PaymentMode, string ChequeDD_No, int BankId)
        {
            AssosiateRequest Entity = new AssosiateRequest();

            Models.AssociateManagement.AssociateDAL.RequestDAL para = new Models.AssociateManagement.AssociateDAL.RequestDAL();

            if (string.IsNullOrEmpty(Entity.Date) == false)
            {
                Entity.Convert_date = DsResultModel.ConvertToSystemDate(Entity.Date, "dd/MM/yyyy");
            }
            Entity.AddedBy = Convert.ToInt32(Session["AssociateFk_MemId"] ?? "0");
            Entity.OpCode = 1;
            Entity.LoginId = LoginId;

            Entity.Amount = Amount;
            Entity.PaymentMode = PaymentMode;
            Entity.ChequeDD_No = PaymentMode=="Gateway"?DateTime.Now.ToString("ddMMyyyyhhmmss"):ChequeDD_No;
            Entity.BankId = BankId;
            Entity.OpCode = 1;

            AssosiateRequest Responce = para.RequestwalletPoints(Entity);
            if (Responce.Code == "1")
            {
                if(PaymentMode=="Gateway")
                {
                    Razorpay.Api.Order objorder = null;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    RazorpayClient client = null;

                    client = new RazorpayClient(CommonRazorPay.Key, CommonRazorPay.SecretKey);



                    Dictionary<string, object> options = new Dictionary<string, object>();
                    options.Add("amount", Amount * 100);
                    options.Add("receipt", "");
                    options.Add("currency", "INR");
                    options.Add("payment_capture", 1);
                    objorder = client.Order.Create(options);
                    string orderId = objorder["id"].ToString();



                    Session["orderId"] = orderId;
                    Session["amount"] = Amount;
                    Session["status"] = Entity.ChequeDD_No;
                    Session["key"] = CommonRazorPay.Key;
                    
                }

                Entity.Flag = 1;

            }
            else
            {
                Entity.Message = Responce.Message;
                Entity.Flag = 0;
            }

            return Json(Entity);

        }


    }
}