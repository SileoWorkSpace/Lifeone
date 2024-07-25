using LifeOne.Models;
using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
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
using static LifeOne.Models.ShoppingRequest;
using System.Data;

namespace LifeOne.Controllers
{
    public class WebHookApiTestingController : ApiController
    {
        [HttpPost]
        public ResultForCashFree TrasnsactionsUpdate()
        {
            CustomerDb objcusdb = new CustomerDb();
            ResultForCashFree objres = new ResultForCashFree();
            try
            {
                CashFreeWebHook objreq = new CashFreeWebHook();
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
                objreq = JsonConvert.DeserializeObject<CashFreeWebHook>(documentContents);

                var res = objcusdb.CheckTransaction(objreq);

                if (res != null && res.Flag == 1)
                {
                    var result1 = objcusdb.SaveWebHookString("", res.Fk_Memid, save.TransId);
                    if(res.Type=="Wallet")
                    {
                        PaypalToWallet paymodel = new PaypalToWallet();
                       
                        paymodel.Fk_MemId = res.Fk_Memid;
                        paymodel.Amount = res.Amount;
                        paymodel.OrderId = objreq.data.order.order_id;
                        paymodel.TransId = objreq.data.payment.cf_payment_id.ToString();
                        paymodel.Status = objreq.data.payment.payment_status;
                        var res1 = objcusdb.PaypalToWallet_V2(paymodel);
                    }
                    else if(res.Type=="Shopping")
                    {
                        UpdatePlaceOrderStatus updatePlaceOrderStatus = new UpdatePlaceOrderStatus();
                        if (objreq != null && (objreq.data.payment.payment_status == "SUCCESS"))
                        {
                            updatePlaceOrderStatus.Status = "Placed";
                            updatePlaceOrderStatus.paymentId = objreq.data.payment.cf_payment_id.ToString();

                        }
                        else if (objreq != null && objreq.data.payment.payment_status == "FAILED")
                        {
                            updatePlaceOrderStatus.Status = "Failed";
                            updatePlaceOrderStatus.paymentId = "";
                        }
                        updatePlaceOrderStatus.OpCode = 2;
                        updatePlaceOrderStatus.OrderID = res.Fk_OrderId;
                        DataSet dataSet = updatePlaceOrderStatus.UpdateStatus();
                    }

                    
                }
                objres.Msg = "Success";
            }
            catch (Exception ex)
            {
                objres.Msg = ex.Message;
            }
            return objres;
        }
    }
}