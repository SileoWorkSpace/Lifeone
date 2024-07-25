using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Controllers
{
    public class PayAddaTransactionController : Controller
    {
        // GET: PadAddaTransaction
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateRechargeStatus(string Status, string ClientTransactionId, string TransactionId, string OperatorTransactionId)
        {
            PayAddaResponse objres = new PayAddaResponse();
            try
            {
                CustomerDb objcusdb = new CustomerDb();
                objres = objcusdb.DALUpdateRechargePendingStatus(Status, ClientTransactionId, TransactionId, OperatorTransactionId);
                if (objres != null)
                {
                    objres.Flag = objres.Flag;
                    objres.Msg = objres.Msg;
                }
                else
                {
                    objres.Flag = 0;
                    objres.Msg = "Error";
                }
            }
            catch (Exception ex)
            {
                objres.Flag = 0;
                objres.Msg = ex.Message;
            }
            return View();
        }
    }
}