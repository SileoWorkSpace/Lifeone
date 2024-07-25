using FamilyN.Models.AdminManagement.ADAL;
using FamilyN.Models.AdminManagement.AEntity;
using FamilyN.Models.API;
using FamilyN.Models.API.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FamilyN.Controllers
{
    public class WebHookTourPaymentController : ApiController
    {
        [HttpPost]
        public ResultSet TourOnlineTransaction()
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
                    else
                    {
                        //if (objreq != null && objreq.Event == "order.paid" && objreq.payload.payment.entity.status == "captured")
                        if (objreq != null && objreq.Event == "order.paid" && objreq.payload.payment.entity.status == "captured")
                        {
                 
                            TourOnlinePayment paymodel = new TourOnlinePayment();
                            paymodel.PaidAmount =Convert.ToDecimal(objreq.payload.payment.entity.amount);
                            paymodel.Fk_MemId =Convert.ToInt32(res.Fk_Memid);
                            paymodel.TourId =Convert.ToInt16(objreq.tourId);
                            paymodel.TransactionNo = objreq.payload.payment.entity.id;
                            paymodel.Status = "success";
                           // paymodel.PaymentDate = objreq.payload.payment.entity.order_id;
                            paymodel.PaymentId = objreq.payload.payment.entity.order_id;
                        
                            var res1 = DALTourDetail.TourOnlinePayment(paymodel);
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
                if (_aepsReponse == "")
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
