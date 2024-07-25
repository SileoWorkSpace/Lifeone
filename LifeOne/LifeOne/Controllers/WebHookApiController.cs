using SGCareWeb.Models;
using SGCareWeb.Models.API;
using SGCareWeb.Models.API.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using static SGCareWeb.Models.ShoppingRequest;
using System.Data;

namespace SGCareWeb.Controllers
{
    public class WebHookApiController : ApiController
    {

        [HttpPost]
        public ResultSet TrasnsactionsUpdate()
        {
            CustomerDb objcusdb = new CustomerDb();
            ResultSet objres = new ResultSet();
            try
            {
                WebHookApiPara objreq = new WebHookApiPara();
                string documentContents;
                using (Stream receiveStream = HttpContext.Current.Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                var RazorpaySignature = HttpContext.Current.Request.Headers["X-Razorpay-Signature"];
                var save = objcusdb.SaveWebHookString(documentContents, 0, 0);
                objreq = JsonConvert.DeserializeObject<WebHookApiPara>(documentContents);

                var res = objcusdb.CheckTransaction(objreq);

                if (res != null && res.Flag == 1)
                {
                    var result1 = objcusdb.SaveWebHookString("", res.Fk_Memid, save.TransId);
                    if (res.Type == "Movie")
                    {
                        if (objreq != null && (objreq.payload.payment.entity.status == "captured" || objreq.payload.payment.entity.status == "paid" || objreq.payload.payment.entity.status == "authorized"))
                        {
                            var updatePayment = objcusdb.UpdatePaymentStatusAudition(objreq.payload.payment.entity.order_id, objreq.payload.payment.entity.id, objreq.payload.payment.entity.status, RazorpaySignature);
                            objres.Msg = "Success";
                        }
                        else if (objreq != null && objreq.payload.payment.entity.status == "failed")
                        {
                            var updatePayment = objcusdb.UpdatePaymentStatusAudition(objreq.payload.payment.entity.order_id, objreq.payload.payment.entity.id, objreq.payload.payment.entity.status, RazorpaySignature);
                            objres.Msg = "Due To some Error transaction failed amount will credited into your account";
                        }
                    }
                    else if (res.Type == "Shopping")
                    {
                        UpdatePlaceOrderStatus updatePlaceOrderStatus = new UpdatePlaceOrderStatus();
                        if (objreq != null && (objreq.payload.payment.entity.status == "captured"))
                        {
                            updatePlaceOrderStatus.Status = "Placed";
                            updatePlaceOrderStatus.paymentId = objreq.payload.payment.entity.id;

                        }
                        else if (objreq != null && objreq.Event == "payment.failed" && objreq.payload.payment.entity.status == "failed")
                        {
                            updatePlaceOrderStatus.Status = "Failed";
                            updatePlaceOrderStatus.paymentId = "";
                        }
                        updatePlaceOrderStatus.OpCode = 2;
                        updatePlaceOrderStatus.OrderID = res.Fk_OrderId;
                        DataSet dataSet = updatePlaceOrderStatus.UpdateStatus();
                    }
                    else
                    {
                        //if (objreq != null && objreq.Event == "order.paid" && objreq.payload.payment.entity.status == "captured")
                        if (objreq != null && objreq.Event == "order.paid" && objreq.payload.payment.entity.status == "captured")
                        {
                            PaypalToWallet paymodel = new PaypalToWallet();
                            paymodel.Amount = res.Amount;
                            paymodel.Fk_MemId = res.Fk_Memid;
                            paymodel.OrderId = objreq.payload.payment.entity.order_id;
                            paymodel.InvoiceNo = objreq.payload.payment.entity.id;
                            paymodel.Type = objreq.payload.payment.entity.method;
                            var res1 = objcusdb.PaypalToWallet_V2(paymodel);
                        }
                        //else if (objreq != null && objreq.Event == "payment.failed" && objreq.payload.payment.entity.status == "failed")
                        else if (objreq != null && objreq.Event == "payment.failed" && objreq.payload.payment.entity.status == "failed")
                        {
                            var res3 = objcusdb.UpdatePaymentStatus(objreq, objreq.payload.payment.entity.status);

                            objres.Msg = "Due To some Error transaction failed amount will credited into your account";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objres.Msg = ex.Message;
            }
            return objres;
        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public ResultSet GetAEPSResponse(string _aepsReponse)
        {
            ResultSet objres = new ResultSet();
            try
            {
                if (_aepsReponse=="")
                {
                    objres.Flag = 0;
                    objres.Msg = "AEPS response should not be blank.";
                    return objres;
                }
                objres.Flag = 1;
                CustomerDb objcusdb = new CustomerDb();
                var save = objcusdb.SaveWebHookString(_aepsReponse, 0, 0);
            }
            catch (Exception ex)
            {
                objres.Flag = 0;
                objres.Msg = "Due To some Error transaction failed amount will credited into your account";
            }
            return objres;
        }
    }
}
