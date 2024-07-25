using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using LifeOne.Models.QUtility.DAL;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace LifeOne.Areas.Associate.Controllers
{

    [SessionTimeoutAttribute]
    public class EWalletRequestController : Controller
    {
        // GET: Associate/EWalletRequest
        public ActionResult EWalletRequestDetails(EwalletRequest obj, int? page)
        {
            EwalletRequest objewallet = new EwalletRequest();
            DALEwalletRequests objDALEwalletRequests = new DALEwalletRequests();
            obj.Fk_MemId = SessionManager.AssociateFk_MemId.ToString();
            obj.FromDate = (!string.IsNullOrEmpty(obj.FromDate)) ? DsResultModel.ConvertToSystemDate(obj.FromDate, "MM/dd/yyyy") : null;
            obj.ToDate = (!string.IsNullOrEmpty(obj.ToDate)) ? DsResultModel.ConvertToSystemDate(obj.ToDate, "MM/dd/yyyy") : null;
            List<EwalletRequest> lst = new List<EwalletRequest>();

            if (page == null)
            {
                DataSet ds = objDALEwalletRequests.GetEWalletRequest(obj);
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        EwalletRequest Objload = new EwalletRequest();
                        Objload.Amount = dr["Amount"].ToString();
                        Objload.RequestedDate = dr["RequestDate"].ToString();
                        Objload.PaymentMode = dr["PaymentMode"].ToString();
                        Objload.BankName = dr["BankName"].ToString();
                        Objload.DDChequeNo = dr["ChequeNo"].ToString();
                        Objload.DDChequeDate = dr["ChequeDate"].ToString();
                        Objload.BankBranch = dr["BankBranch"].ToString();
                        Objload.AdminApprovedStatus = dr["AdminApprovedStatus"].ToString();
                        Objload.PK_RequestID = dr["PK_RequestID"].ToString();
                        lst.Add(Objload);
                    }
                    TempData["EwalletRequest"] = lst;
                    objewallet.WalletRequestList = lst;
                }
            }
            else if (TempData["EwalletRequest"] != null)
            {
                lst = (List<EwalletRequest>)TempData["EwalletRequest"];
                TempData["EwalletRequest"] = lst;
            }
            
            return View(objewallet);
        }

        public ActionResult EWalletRequest(EwalletRequest obj)
        {
            obj.LoginId = SessionManager.LoginId;
            obj.Fk_MemId = SessionManager.AssociateFk_MemId.ToString();
            //obj.Mobile = Session["Mobile"].ToString();

            #region ddlpaymentmode
            List<PaymentModeListViewModel> ddlpaymentmode = DALBindCommonDropdown.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            ViewBag.BankList = DALBindCommonDropdown.BindDropdown(9, 0);
            #endregion

            return View(obj);
        }

        public ActionResult SaveEWalletRequest(string Fk_MemId, string Amount, string PaymentMode, string BankName, string BankBranch, string DDChequeNo, string DDChequeDate, string Document,int BankId)
        {
            EwalletRequest objpinmangement = new EwalletRequest();
            DALEwalletRequests objDALEwalletRequests = new DALEwalletRequests();
            try
            {
                objpinmangement.CreatedBy = SessionManager.AssociateFk_MemId.ToString();

                objpinmangement.Fk_MemId = Fk_MemId;

                //objpinmangement.Mobile = Session["MobileNo"].ToString();

                objpinmangement.Amount = Amount;
                objpinmangement.PaymentMode = PaymentMode;
                objpinmangement.BankName = BankName;
                objpinmangement.BankName = BankName;
                objpinmangement.DDChequeNo = DDChequeNo;
                objpinmangement.DDChequeDate = string.IsNullOrEmpty(DDChequeDate) ? null : DsResultModel.ConvertToSystemDate(DDChequeDate, "dd-MM-yyyy");

                objpinmangement.BankBranch = BankBranch;
                objpinmangement.BankId = BankId;
                objpinmangement.Document = Document;
                DataSet ds = objDALEwalletRequests.SaveEWalletRequest(objpinmangement);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        objpinmangement.Message = "Your Ewallet Request Submitted Successfully .";
                        ViewBag.Alert = ReturnAlert("success", "Your Ewallet Request Submitted Successfully .");

                    }
                    else
                    {
                        objpinmangement.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        ViewBag.Alert = ReturnAlert("warning", ds.Tables[0].Rows[0]["ErrorMessage"].ToString());


                    }
                }
            }
            catch (Exception ex)
            {
                objpinmangement.Message = ex.Message;

            }

            #region ddlpaymentmode
            List<PaymentModeListViewModel> ddlpaymentmode = DALBindCommonDropdown.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            return Json(objpinmangement.Message, JsonRequestBehavior.AllowGet);
        }

        public string ReturnAlert(string alert, string message)
        {
            var result = "";
            if (alert == "danger")
            {
                result = "<div class='alert alert-danger alert-dismissible show' role='alert'>" +
                         "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" +
                         "<strong>Alert! </strong>&nbsp;" + message + "</div>";
            }
            else if (alert == "success")
            {
                result = "<div class='alert alert-success alert-dismissible show' role='alert'>" +
                         "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" +
                         "<strong>Alert! </strong>&nbsp;" + message + "</div>";
            }
            else if (alert == "info")
            {
                result = "<div class='alert alert-info alert-dismissible show' role='alert'>" +
                         "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" +
                         "<strong>Alert! </strong>&nbsp;" + message + "</div>";
            }
            else if (alert == "warning")
            {
                result = "<div class='alert alert-warning alert-dismissible show' role='alert'>" +
                         "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" +
                        "<strong>Alert! </strong>&nbsp;" + message + "></div>";
            }
            else
            {
                result = "No Alert";
            }


            //  "$('#alertBoxx').fadeTo(2000,0).slideUp(500,function(){$('#alertBoxx').slideUp(500)};);";

            // " $('#alertBoxx .alert').fadeTo(2000, 0).slideUp(500, function() { $('#alertBoxx .alert').slideUp(500); });";

            //return "<script>$('#alertBoxx').html(" + result + ")+window.setTimeout(function() { $('#alertBoxx .alert').fadeTo(2000, 0).slideUp(5000, function() { $('#alertBoxx .alert').slideUp(5000); }); }, 2000)+</script>";

            // return "<script>$('#alertBoxx').html("+ result + ")</script>";

            // "$('#alertBoxx').html(ReturnAlert('danger', '" + DsResult.Tables[0].Rows[0]["ErrorMessage"].ToString() + "'));";
            //  $('#alertBoxx .alert').fadeTo(2000, 0).slideUp(500, function() { $('#alertBoxx .alert').slideUp(500); });

            //+window.setTimeout(function() { $('#alertBoxx .alert').fadeTo(2000, 0).slideUp(500, function() { $('#alertBoxx .alert').slideUp(500); }); }, 2000)+";

            return result;
        }

        public ActionResult EWalletRequestDetailsList(CashbackWallet obj, int? page)
        {
            CashbackWallet objewallet = new CashbackWallet();
            DALEwalletRequests objDALEwalletRequests = new DALEwalletRequests();
            obj.Fk_MemId = SessionManager.AssociateFk_MemId.ToString();
            obj.FromDate = (!string.IsNullOrEmpty(obj.FromDate)) ? DsResultModel.ConvertToSystemDate(obj.FromDate, "MM/dd/yyyy") : null;
            obj.ToDate = (!string.IsNullOrEmpty(obj.ToDate)) ? DsResultModel.ConvertToSystemDate(obj.ToDate, "MM/dd/yyyy") : null;
            List<CashbackWallet> lst = new List<CashbackWallet>();

            if (page == null)
            {
                DataSet ds = objDALEwalletRequests.GetEWalletLedger(obj);
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        CashbackWallet Objload = new CashbackWallet();
                        Objload.TransactionDate = dr["TransactionDate"].ToString();
                        Objload.Narration = dr["Narration"].ToString();
                        Objload.CrAmount = dr["CrAmount"].ToString();
                        Objload.DrAmount = dr["DrAmount"].ToString();
                        Objload.Balance = dr["Balance"].ToString();
                        lst.Add(Objload);
                    }
                    TempData["EWallerLedger"] = lst;
                    objewallet.CashbackWallets = lst;
                }
            }
            else if (TempData["EWallerLedger"] != null)
            {
                lst = (List<CashbackWallet>)TempData["EWallerLedger"];
                TempData["EWallerLedger"] = lst;
            }

            return View(objewallet);
        }
    }
}