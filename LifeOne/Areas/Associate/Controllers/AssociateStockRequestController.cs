using ClosedXML.Excel;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.API;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LifeOne.Areas.Associate.Controllers
{

    [SessionTimeoutAttribute]
    public class AssociateStockRequestController : Controller
    {
        string Aeskey = ConfigurationManager.AppSettings["Aeskey"].ToString();
        // GET: Associate/AssociateStockRequest/AddRequestForStock
        public ActionResult AddRequestForStock()
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
                MFranchiseorderRequest obj = new DALFranchise().GetProductDetailsAssociates(_data);
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

        public JsonResult AddProduct(MFranchiseorderRequest _data)
        {
            try
            {
                MSimpleString obj = AssoicateOrderService.AddAssociateProductService(_data);
                return Json(obj, JsonRequestBehavior.AllowGet);
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
                    MSimpleString result = AssoicateOrderService.SaveAssociateProductService(_data);
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
                RequestForAssociateFranchiseService _ServiceAssociate = new RequestForAssociateFranchiseService();
                MOrderModel orderModel = _ServiceAssociate.AssociatePersonalDetails();
                orderModel.orderId = orderResponse.Attributes["id"];
                orderModel.razorpayKey = "rzp_test_umbrFAbVJ3slyJ";
                orderModel.amount = (int)_data.Amount * 100;
                orderModel.currency = "INR";
                orderModel.description = "Payment against order booking by " + Models.Manager.SessionManager.AssociateFk_MemId.ToString() + " on " + DateTime.Now.ToString();
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

        // GET: Associate/AssociateStockRequest/TrackRequestedStock

        public ActionResult TrackPurchaseStock(int? page, MOrder obj)
        {
            //MOrder obj = new MOrder();
            try
            {
                //if (!String.IsNullOrEmpty(OrderNo))
                //    obj.OrderNo = OrderNo;

                //if (!String.IsNullOrEmpty(ApproveDate))
                //    obj.ApproveDate = ApproveDate;
                obj = AssoicateOrderService.RequestdOrderStatusService(page, obj);
                return View(obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult ExportToExcelTrackPurchaseStock(int? page, MOrder obj)
        {
            try
            {
                var customers = AssoicateOrderService.RequestdOrderStatusService(page, obj);
                DataTable dt = new DataTable("TrackPurchaseStock");
                dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Order No"),                                             
                                            new DataColumn("Qty"),
                                            new DataColumn("Total PV"),
                                            new DataColumn("Payment Type"),
                                            new DataColumn("Order Info."),
                                            new DataColumn("Payment Status")
                                            
            });
                if (customers.LstOrder.Count > 0)
                {
                    foreach (var customer in customers.LstOrder)
                    {
                        dt.Rows.Add(customer.OrderNo, customer.ItemQty, customer.TotalPV, customer.PaymentType, customer.OrderDate  +"\n"+  customer.Amount, customer.PaymentStatus);
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TrackPurchaseStock.xlsx");
                        }
                    }
                }
                else
                {
                    TempData["alert"] = "Data Not Found";
                    return Redirect("TrackPurchaseStock");
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public JsonResult GetShippingInformationForAssociate(string RequestId)
        {
            try
            {
                List<MShowSHippingInfoToAssociate> MemberList = AddShippingDetailsService.GetShippingInforForAssociateServie(RequestId);
                return Json(MemberList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


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
        public JsonResult BindUpdatedFigureAmt(string ReqId, string PK_KeyId, string Quantity, string TotalPv, string TotalAmt)
        {

            var list = DALFranchise.BindUpdatedFigureAmt(ReqId, long.Parse(PK_KeyId), Convert.ToInt32(Quantity), Convert.ToInt32(TotalPv), Convert.ToDecimal(TotalAmt));
            return Json(list, JsonRequestBehavior.AllowGet);

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

        /*Updated on 11/01/2021*/
        public JsonResult ViewRequestItemDetail(string RequestId)
        {
            try
            {
                List<MOrder> _obj = FranchiseOrderService.ViewRequestItemService(long.Parse(RequestId));
                return Json(_obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult ViewRequestItemDetailFranchiseWise(string RequestId, int FranchiseId)
        {
            try
            {
                List<MOrder> _obj = FranchiseOrderService.ViewRequestItemServiceFranchiseWise(FranchiseId, long.Parse(RequestId));
                return Json(_obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult MemberTaxInvoice(string OrderId)
        {
            try
            {
                OrderId = DALCommon.GlobalDecrypText(OrderId);
                MAssociateAdminInvoice obj = new MAssociateAdminInvoice();
                var key = AesEncryptDecrypt.DecryptString(Aeskey, OrderId);
                var list = LifeOne.Models.AdminManagement.ADAL.DBHelperDapper.GetMemberOrderInvoice(key);
                obj = list._MemberDetail;
                obj.MemberOrderDetailList = list._OrderDetail;
                return View(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}