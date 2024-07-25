using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Associate.Controllers
{
    public class BuyPackageController : Controller
    {
        // GET: Associate/BuyPackage/purchaseNow
        public ActionResult purchaseNow()
        {
            PackageMaster para = new PackageMaster();
            ViewBag.ddlPackageList = BindPckageMaster.BindPackageMaster();
            return View();
        }

        public ActionResult PackagePurchaseHistory(PackagePurchaseHistory obj)
        {

            obj.Fk_MemId = Convert.ToString(SessionManager.AssociateFk_MemId);
            obj = obj.GetAssociatePackagePurchase(obj); 
            return View(obj);
        }

        //[HttpPost]
        //public ActionResult purchaseNow(MemberTopup obj)
        //{

        //    MemberTopup para = new MemberTopup();
        //    UpgradePackage returnData = new UpgradePackage();
        //    returnData.Fk_MemId = obj.FK_MemId;
        //    returnData.Fk_packageid = obj.FK_PackageId;
        //    returnData.Amount = obj.PaidAmount;
        //    returnData.TransactionNo = "Tansni";
        //    returnData.LoginId = "Tansni";
        //    returnData.PaymentMode = "Online";
        //    para.UpdatedBy = Session["Pk_MemId"].ToString();
        //    para.BankName = string.IsNullOrEmpty(obj.BankName) ? null : (obj.BankName);
        //    para.BankBranch = string.IsNullOrEmpty(obj.BankBranch) ? null : (obj.BankBranch);
        //    para.ChequeDDNo = string.IsNullOrEmpty(obj.ChequeDDNo) ? null : (obj.ChequeDDNo);
        //    // para.ChequeDDDate = string.IsNullOrEmpty(obj.ChequeDDDate) ? null : BLCommon.ConvertToSystemDate(obj.ChequeDDDate, "dd/MM/yyyy");
        //    para.ChequeDDDate = null;
        //    //para.TopupDate = string.IsNullOrEmpty(obj.TopupDate) ? null : BLCommon.ConvertToSystemDate(obj.TopupDate, "dd/MM/yyyy");
        //    para.PaymentMode = obj.PaymentMode;

        //    DataSet ds = null;
        //    ds = UpgradePackage.DalUpgradePackage(returnData);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
        //        {
        //            ViewBag.Alert = ds.Tables[0].Rows[0]["Remark"].ToString();
        //        }
        //        else if (ds.Tables[0].Rows[0]["Code"].ToString() == "0")
        //        {
        //            ViewBag.Alert = ds.Tables[0].Rows[0]["Remark"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Alert = "Unable To Process Request";
        //    }
        //    ViewBag.ddlPackageList = BindPckageMaster.BindPackageMaster();
        //    return View();
        //}

        [HttpPost]
        public ActionResult CreateOrder(MemberTopup obj)
        {
            Random randomObj = new Random();
            Session["SelectedPackageINformation"] = obj;
            string transactionId = randomObj.Next(10000000, 100000000).ToString();
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_live_y2pTaMvSx7H5Xa", "x8QzXrOlBIHFpA3d1ofsqHTS");
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", (int)obj.PaidAmount * 100);  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();
            // Create order model for return on view
            AssociateProfileService _ServiceAssociate = new AssociateProfileService();
            MOrderModel orderModel = _ServiceAssociate.AssociatePersonalDetails();
            orderModel.orderId = orderResponse.Attributes["id"];
            orderModel.razorpayKey = "rzp_live_y2pTaMvSx7H5Xa";
            orderModel.amount = (int)obj.PaidAmount * 100;
            orderModel.currency = "INR";
            orderModel.description = "Payment against Package booking by " + Models.Manager.SessionManager.AssociateFk_MemId.ToString() + " on " + DateTime.Now.ToString();
            return View("PaymentPage", orderModel);
        }
        [HttpPost]
        public ActionResult Complete()
        {
            string paymentId = Request.Params["rzp_paymentid"];
            string orderId = Request.Params["rzp_orderid"];
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_live_y2pTaMvSx7H5Xa", "x8QzXrOlBIHFpA3d1ofsqHTS");
            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];
            if (paymentCaptured.Attributes["status"] == "captured")
            {
                MemberTopup obj = (MemberTopup)Session["SelectedPackageINformation"];
                UpgradePackage returnData = new UpgradePackage();
                returnData.Fk_MemId = obj.FK_MemId;
                returnData.Fk_packageid = obj.FK_PackageId;
                returnData.Amount = obj.PaidAmount;
                returnData.TransactionNo = paymentId;
                returnData.LoginId = obj.LoginId;
                returnData.PaymentMode = "Online";
                DataSet ds = UpgradePackage.DalUpgradePackage(returnData);

                Session["paymentId"] = paymentId;
                Session["Amount"] = obj.PaidAmount;

                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                    {
                        return RedirectToAction("PaymentSuccess");
                    }
                    else
                    {
                        return RedirectToAction("OrderFailed");
                    }
                }
                else
                {
                    return RedirectToAction("OrderFailed");
                }

            }
            else
            {
                return RedirectToAction("Failed");
            }
        }
        public ActionResult GetMemberName(string LoginId)
        {
            UpgradePackage obj = new UpgradePackage();
            try
            {
                obj.LoginId = LoginId;
                DataSet ds = obj.GetMemberInfo();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() != "0")
                {
                    obj.DisplayName = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                    obj.Fk_MemId = Convert.ToInt64(ds.Tables[0].Rows[0]["Fk_memId"]);
                    obj.Result = "0";
                    obj.TempPermanent = ds.Tables[0].Rows[0]["TempPermanent"].ToString();
                }
                else { obj.Result = "1"; }
              
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMemberTopup(string LoginId)
        {
            UpgradePackage obj = new UpgradePackage();
            try
            {
                obj.LoginId = LoginId;
                DataSet ds = obj.GetMemberTopup();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() != "0")
                {
                    obj.DisplayName = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                    obj.Fk_MemId = Convert.ToInt64(ds.Tables[0].Rows[0]["Fk_memId"]);
                    obj.Result = "1";
                    TopupByFranchise obj1 = new TopupByFranchise();
                    obj1.FK_MemId = obj.Fk_MemId;
                    DataSet ds1 = obj1.ActivateMembersTemp();
                }
                else { obj.Result = "0"; }

            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPackageDetails(string Pk_PackageID)
        {
            try
            {
                PackageMaster para = new PackageMaster();
                para.Pk_PackageID = Pk_PackageID;
                var list = UpgradePackage.GetpackageDetails(para);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult GetAmountPackageId(string Pk_PackageID,string FK_MemID)
        {
            try
            {
                PackageMaster para = new PackageMaster();
                para.Pk_PackageID = Pk_PackageID;
                para.Fk_MemId = SessionManager.FranchiseFk_MemId.ToString();
                var list = UpgradePackage.GetAmountPackagebyId(para);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Failed()
        {
            return View();
        }
        public ActionResult PaymentSuccess()
        {
            return View();
        }
        public ActionResult OrderFailed()
        {
            return View();
        }
    }
}