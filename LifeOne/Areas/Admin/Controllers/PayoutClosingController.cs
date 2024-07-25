using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Areas.Admin.Controllers
{
    [SessionTimeoutAttributeAdmin]
    public class PayoutClosingController : Controller
    {
        // GET: Admin/PayoutClosing
        public ActionResult PayoutCalculationStatus(PayoutCalculationStatusModel model, int? Page)
        {

            if (!PermissionManager.IsActionPermit("Payout Closing", ViewOptions.FormView))
            {
                return Redirect("/Home/adminlogin");
            }

            if (Page == null || Page == 0)
            {
                Page = 1;
            }
            model.page = Page;
            PayoutCalculationDAL repository = new PayoutCalculationDAL();
            PayoutCalculationStatusModel obj = new PayoutCalculationStatusModel();

            obj.PayoutCalculationStatuses = repository.GetPayoutCalculationStatus(model);
            int totalRecords = 0;
            if (obj.PayoutCalculationStatuses.Count > 0)
            {
                totalRecords = Convert.ToInt32(obj.PayoutCalculationStatuses[0].TotalRecord);
                var pager = new Pager(totalRecords, model.page, SessionManager.Size);
                obj.Pager = pager;
            }
            else
            {
                obj.PayoutCalculationStatuses = new List<PayoutCalculationStatusModel>();
            }
            return View(obj);
        }

        [HttpPost]
        public JsonResult UpdatePayoutCalculationStatus(string Pk_PayoutCalculationStatusId)
        {
            List<string> Msg = new List<string>();
            PayoutCalculationStatusModel statusModel = new PayoutCalculationStatusModel();
            statusModel.PK_PlanId = int.Parse(Pk_PayoutCalculationStatusId);
            PayoutCalculationDAL ac = new PayoutCalculationDAL();
            DataSet ds = ac.UpdatePayoutCalculationStatus(statusModel);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                {

                    Msg.Add(ds.Tables[0].Rows[0]["Code"].ToString());
                    Msg.Add(ds.Tables[0].Rows[0]["Remark"].ToString());
                }
                else
                {
                    Msg.Add("2");
                    Msg.Add("failed!!");
                }

            }
            else
            {
                Msg.Add("2");
                Msg.Add("failed!!");
            }
            return Json(Msg);
        }

        public ActionResult PayoutClosing()
        {
            PayoutCalculationDAL repository = new PayoutCalculationDAL();
            MembersToMakePaymentModel obj = new MembersToMakePaymentModel();
            obj.PayoutMembers = repository.GetMembersToMakePayment();
            return View(obj);
        }

        [HttpPost]
        public JsonResult MakePayments(string Fk_MemId, string ClosingDate)
        {
            PayoutCalculationDAL ac = new PayoutCalculationDAL();
            MembersToMakePaymentModel objSave = new MembersToMakePaymentModel();
            objSave.Fk_MemId = int.Parse(Fk_MemId);
            objSave.ClosingDate = DALCommon.ConvertToSystemDate(ClosingDate, "dd/MM/yyyy");
            DataSet ds = ac.MakePayments(objSave);
            DsResultModel payoutResultModel = DsResultModel.DsToResultModel(ds);
            return Json(payoutResultModel);
        }
    }
}