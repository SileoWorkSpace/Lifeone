using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class PayoutCalculationDAL
    {
        public List<PayoutCalculationStatusModel> GetPayoutCalculationStatus(PayoutCalculationStatusModel model)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@page", model.page > 0 ? model.page : 1);
                queryParameters.Add("@size", model.size > 0 ? model.size : SessionManager.Size);
                List<PayoutCalculationStatusModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<PayoutCalculationStatusModel>("Proc_GetPayoutCalculationStatus", queryParameters);


                //queryParameters.Add("@page", model.page > 0 ? model.page : 1);
                //queryParameters.Add("@size", model.size > 0 ? model.size : SessionManager.Size);

                //DataSet ds = DBHelper.ExecuteQuery("Proc_GetPayoutCalculationStatus");
                //List<PayoutCalculationStatusModel> objlist = new List<PayoutCalculationStatusModel>();
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in ds.Tables[0].Rows)
                //    {
                //        objlist.Add(new PayoutCalculationStatusModel
                //        {
                //            PK_PlanId = int.Parse(dr["PK_PlanId"].ToString()),
                //            PlanName = dr["PlanName"].ToString(),
                //            //IsCalculated = bool.Parse(dr["IsCalculated"].ToString()),
                //            CalculationDate = dr["CalculationDate"].ToString()
                //        });
                //    }
                //}

                return _iresult;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet UpdatePayoutCalculationStatus(PayoutCalculationStatusModel payout)
        {
            try
            {
                SqlParameter[] para ={
                new SqlParameter("@Pk_PayoutCalculationStatusId",payout.PK_PlanId)
            };
                DataSet ds = DBHelper.ExecuteQuery("Proc_UpdatePayoutCalculationStatus", para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MembersToMakePaymentModel> GetMembersToMakePayment()
        {
            try
            {
                List<MembersToMakePaymentModel> _iresult = new List<MembersToMakePaymentModel>();
                string _qury = "MembersToMakePayment";
                _iresult = DBHelperDapper.DAGetDetailsInList<MembersToMakePaymentModel>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet MakePayments(MembersToMakePaymentModel mem)
        {
            SqlParameter[] para =
                       {
                new SqlParameter("@Fk_Memid",mem.Fk_MemId),
                new SqlParameter("@ClosingDate",mem.ClosingDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("MakePayments", para);
            return ds;
        }

    }
}