using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace LifeOne.Areas.Franchise.Controllers
{
    [SessionTimeoutAttributeFranchise]
    public class ManageFranchiseController : Controller
    {
        // GET: Franchise/ManageFranchise
        public ActionResult Index()
        {
            return View();
        }
        // GET: Franchise/ManageFranchise/OrderRequest
        public ActionResult OrderRequest()
        {
            try
            {
                ViewBag.ProductCategory = DALBindCommonDropdown.BindDropdown(2, 0);
                ViewBag.Product = DALBindCommonDropdown.BindDropdown(1, 0);
               
            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }

        public JsonResult GetProductDetails(MFranchiseorderRequest _data)
        {
            try
            {
                MFranchiseorderRequest obj = new DALFranchise().GetProductDetails(_data);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult AddProduct(MFranchiseorderRequest _data)
        {
            try
            {
                MSimpleString obj = FranchiseOrderService.AddProductService(_data);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult GetOrderTemp(MSimpleString _data)
        {
            try
            {
                MFranchiseorderRequest obj = new MFranchiseorderRequest();
                List<MFranchiseorderRequest> _objlst = DALFranchise.GetOrderTemp(_data);
                return Json(_objlst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult DeleteProductTemp(MFranchiseorderRequest _data)
        {
            try
            {
                MSimpleString _objlst = DALFranchise.DeleteProductTemp(_data);
                return Json(_objlst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult BindProduct(MFranchiseorderRequest _data)
        {
            try
            {
                List<SelectListItem> Lst = DALBindCommonDropdown.BindDropdown(1, _data.CategoryId);
                return Json(Lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult SaveProduct(MFranchiseorderRequest _data)
        {

            try
            {
                if (_data.PaymentMode == "Online" && !_data.isOnlinePayDone)
                {
                    Session["OrderSession"] = _data;
                    return Json(new { Response = "Online" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    MSimpleString result = FranchiseOrderService.SaveProductService(_data);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                throw;
            }
         
        }

        #region Payment Gateway
        public ActionResult CreateOrder()
        {
            MFranchiseorderRequest _data = new MFranchiseorderRequest();
            if (Session["OrderSession"] != null)
            {
                _data = (MFranchiseorderRequest)Session["OrderSession"];
                PaymentInitiateModel _requestData = new PaymentInitiateModel();
                Random randomObj = new Random();
                string transactionId = randomObj.Next(10000000, 100000000).ToString();
                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_live_nyCK5Vu6fq9JUq", "7snv6fZ93b3Z7h0xXdncSDX2");
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", (int)_data.TotalItemAmt * 100);  // Amount will in paise
                options.Add("receipt", transactionId);
                options.Add("currency", "INR");
                options.Add("payment_capture", "0");
                Razorpay.Api.Order orderResponse = client.Order.Create(options);
                string orderId = orderResponse["id"].ToString();
                // Create order model for return on view
                MOrderModel orderModel = FranchiseOrderService.FranchisePersonalDetails();
                orderModel.orderId = orderResponse.Attributes["id"];
                orderModel.razorpayKey = "rzp_test_umbrFAbVJ3slyJ";
                orderModel.amount = (int)_data.Amount * 100;
                orderModel.currency = "INR";
                orderModel.description = "Payment against order booking by " + Models.Manager.SessionManager.FranchiseFk_MemId.ToString() + " on " + DateTime.Now.ToString();
                return View("PaymentPage", orderModel);
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        [HttpPost]
        public ActionResult Complete()
        {
            string paymentId = Request.Params["rzp_paymentid"];
            string orderId = Request.Params["rzp_orderid"];
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_umbrFAbVJ3slyJ", "su9eXFaihGucMmKECVRcRk0Q");
            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];
            if (paymentCaptured.Attributes["status"] == "captured")
            {
                MFranchiseorderRequest _data = (MFranchiseorderRequest)Session["OrderSession"];
                _data.ChequeDDNo = paymentId;
                _data.isOnlinePayDone = true;
                ViewBag.paymentId = paymentId;
                ViewBag.Amount = amt;
                dynamic Response = SaveProduct(_data);
                if (Response.Data.Code == 1)
                    return RedirectToAction("PaymentSuccess");
                else
                    return RedirectToAction("OrderFailed");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        public ActionResult Failed()
        {
            return View();
        }
        public ActionResult OrderFailed()
        {
            return View();
        }
        public ActionResult PaymentSuccess()
        {
            return View();
        }
        #endregion

        #region Franchise to Child Franchise

        public ActionResult ViewNewRequestFranchise(int? page, MFAOrderRequest _param)
        {
            try
            {
                _param.ParentId = SessionManager.FranchiseFk_MemId;
                if (page == null)
                {
                    page = 1;
                }
                _param.Page = page;
                List<MFAOrderRequest> _obj = FOrderRequestService.GetNewOrderRequestService(_param);
                _param.MFAOrderRequestList = _obj;
                return View(_param);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult ViewNewRequestDetail(string RequestId)
        {
            List<MFAOrderRequestDetail> _obj = FOrderRequestService.GetNewOrderRequestItemService(long.Parse(RequestId));
            return Json(_obj, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ApproveNewRequestDetail(List<MFAOrderRequestDetail> productlist)
        {
            XmlModel obj = new XmlModel();
            var productDetail = "<ProductDetail>";
            if (productlist != null && productlist.Count > 0)
            {
                for (int i = 0; i < productlist.Count; i++)
                {

                    productDetail += "<ProductList><PrdId>" + productlist[i].FK_PrdId + "</PrdId>";
                    productDetail += "<ApproveQty>" + productlist[i].ApprovalQty + "</ApproveQty>";
                    productDetail += "<Remark>" + productlist[i].PrdWiseRmrk + "</Remark>";
                    productDetail += "<StatusId>" + productlist[i].PrdWiseStsId + "</StatusId>";
                    productDetail += "<FK_ReqId>" + productlist[i].Fk_RId + "</FK_ReqId>";
                    productDetail += "<FK_FId>" + productlist[i].FK_FId + "</FK_FId>";
                    productDetail += "<ParentId>" + SessionManager.FranchiseFk_MemId + "</ParentId>";
                    productDetail += "</ProductList>";
                }
                productDetail += "</ProductDetail>";

            }
            obj.XmlData = productDetail;
            var list = FOrderRequestService.ApprveProductServiceByFranchise(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ViewPaymentDetail(string RequestId)
        {
            List<MFAPaymentStatus> _obj = FOrderRequestService.GetFPaymentDetailService(long.Parse(RequestId));
            return Json(_obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApproveDeclinePaymentStatus(string ReqId, string status)
        {

            var list = FOrderRequestService.ApprveDeclinepaymentService(long.Parse(ReqId), Convert.ToInt32(status));
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public JsonResult BindUpdatedFigureAmt(string ReqId, string PK_KeyId, string Quantity, string TotalPv, string TotalAmt)
        {

            var list = DALFranchise.BindUpdatedFigureAmt(ReqId, long.Parse(PK_KeyId), Convert.ToInt32(Quantity), Convert.ToInt32(TotalPv), Convert.ToDecimal(TotalAmt));
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        #endregion

        public JsonResult BindOrbit()
        {
            try
            {
                List<SelectListItem> Lst = DALBindCommonDropdown.BindDropdown(7, 0);
                return Json(Lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult CheckProduct(string prdId)
        {
            try
            {
                MFranchiseorderRequest data = DALFranchise.CheckProduct(prdId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult GetFranchiseEwalletRequest(FranchiseEWalletRequest franchiseEWalletRequest)
        {
            franchiseEWalletRequest.AddedBy = SessionManager.FranchiseFk_MemId;
            DataSet dataSet = franchiseEWalletRequest.GetEwalletRequest();
            franchiseEWalletRequest.dtRequestDetails = dataSet.Tables[0];
            return View(franchiseEWalletRequest);
        }
        public ActionResult EwalletLedger(int? Page , FranchiseEWalletledger franchiseEWalletRequest)
        {
            if(Page==null)
            {
                Page = 1;
            }


            franchiseEWalletRequest.Page = Page ?? 1;
            franchiseEWalletRequest.Size = SessionManager.Size;
            franchiseEWalletRequest.FranchiseId = SessionManager.FranchiseFk_MemId;
            DataSet dataSet = franchiseEWalletRequest.GetEwalletLedger(franchiseEWalletRequest);
            franchiseEWalletRequest.dtRequestDetails = dataSet.Tables[0];
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0) {
                totalRecords = Convert.ToInt32(franchiseEWalletRequest.dtRequestDetails.Rows[0]["Totalrecord"].ToString());
                var pager = new Pager(totalRecords, franchiseEWalletRequest.Page, SessionManager.Size);
                franchiseEWalletRequest.Pager = pager;
            }
            return View(franchiseEWalletRequest);
        }

        public ActionResult GetActiveMembersFranchise(int? Page,FranchiseEWalletledger franchiseEWalletRequest)
        {


            if (Page == null)
            {
                Page = 1;
            }
            franchiseEWalletRequest.Page = Page?? 1;
            franchiseEWalletRequest.Size = SessionManager.Size;
            franchiseEWalletRequest.FranchiseId = SessionManager.FranchiseFk_MemId;
            DataSet dataSet = franchiseEWalletRequest.GetActiveMembersFranchise(franchiseEWalletRequest.LoginId, franchiseEWalletRequest.FromDate, franchiseEWalletRequest.ToDate,franchiseEWalletRequest.FranchiseId, franchiseEWalletRequest.FranchiseLoginID, franchiseEWalletRequest.MobileNo, franchiseEWalletRequest.FK_PackageId, franchiseEWalletRequest.Page, franchiseEWalletRequest.Size,franchiseEWalletRequest.PaymentMode, franchiseEWalletRequest.IsExport, franchiseEWalletRequest.InvoiceNo);
            franchiseEWalletRequest.dtRequestDetails = dataSet.Tables[0];
            int totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = Convert.ToInt32(dataSet.Tables[0].Rows[0]["TotalCount"]);
                var pager = new Pager(totalRecords, franchiseEWalletRequest.Page, franchiseEWalletRequest.Size);
                franchiseEWalletRequest.Pager = pager;
            }

            return View(franchiseEWalletRequest);
        }

    }
}