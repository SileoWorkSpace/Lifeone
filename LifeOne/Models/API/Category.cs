
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;

namespace LifeOne.Models.API
{
    public class Category:DBHelper
    {
        public DataSet GetDashboarddetaildata_V2()
        {

            DataSet dsResult = ExecuteQuery("GetDashBoardCategoryAllList");
            return dsResult;

        }

        public DataSet CommissiomDetails (int Fk_MemId)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_memId",Value=Fk_MemId},
            
                 };
            DataSet dsResult = ExecuteQuery("GetCommissionDetails", sqlparam);
            return dsResult;

        }

        public DataSet GetHoldUncleareBalance(int Fk_MemId,string Type)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_memId",Value=Fk_MemId},
                 new SqlParameter { ParameterName="@Type",Value=Type},

                 };
            DataSet dsResult = ExecuteQuery("GetHoldUnclearBalanceDetails", sqlparam);
            return dsResult;

        }

        public DataSet GetAssociateDashboardDetails(int Fk_MemId)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_memId",Value=Fk_MemId}, 

                 };
            DataSet dsResult = ExecuteQuery("AssociateDashboard_V2", sqlparam);
            return dsResult;

        }

        public DataSet GetIncentiveDetails(int Fk_MemId)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_memId",Value=Fk_MemId},

                 };
            DataSet dsResult = ExecuteQuery("IncentiveDetails", sqlparam);
            return dsResult;

        }

        public DataSet PayoutReportAPI(PayoutRequestAPI _model)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@FK_MemID",Value=_model.Fk_MemId},
                    new SqlParameter { ParameterName="@PayoutNo",Value=_model.PayoutNo},
                 new SqlParameter { ParameterName="@FromDate",Value=_model.FromDate},
                 new SqlParameter { ParameterName="@ToDate",Value=_model.ToDate}

                 };
            DataSet dsResult = ExecuteQuery("PayoutReportApi", sqlparam);
            return dsResult;

        }
        public DataSet TreeView(Genealogy _model)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@LoginId",Value=_model.LoginId},
                 new SqlParameter { ParameterName="@SearchLoginId",Value=_model.SearchLoginId}
                  

                 };
            DataSet dsResult = ExecuteQuery("TreeViewCommon", sqlparam);
            return dsResult;

        }
        public DataSet PayoutMasterList()
        {
            
            DataSet dsResult = ExecuteQuery("Proc_PayoutMasterList");
            return dsResult;

        }
        public DataSet SelectRepurchasePayoutMaster()
        {
            
            DataSet dsResult = ExecuteQuery("SelectRepurchasePayoutMaster");
            return dsResult;

        }
        public DataSet GetUidDashBoard(int Fk_MemId)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_memId",Value=Fk_MemId},

                 };
            DataSet dsResult = ExecuteQuery("dbo.UIDDashboardApI", sqlparam);
            return dsResult;

        }

        public DataSet CheckStatusForCompanion(int Fk_RequestId)
        {
            var sqlparam = new SqlParameter[] {
                 new SqlParameter { ParameterName="@Fk_RequestId",Value=Fk_RequestId},

                 };
            DataSet dsResult = ExecuteQuery("dbo.Proc_CheckStatusForCompanion", sqlparam);
            return dsResult;

        }

    }
}