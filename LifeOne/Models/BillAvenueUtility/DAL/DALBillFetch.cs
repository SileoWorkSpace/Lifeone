using LifeOne.Models.BillAvenues.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.BillAvenueUtility.Entity;
using LifeOne.Models.Common;
using System.Data;
using System.Data.SqlClient;

namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALBillFetch
    {
        public DataSet SaveBillFetchRequest(MBillerFetchResponse _objDetails, MBillerFetchRequestAPP _objrequestdetails)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@RequestId", _objDetails.RequestId),
                                      new SqlParameter("@BillerId", _objrequestdetails.BillerId),
                                      new SqlParameter("@IPAdress", _objrequestdetails.IP),
                                      new SqlParameter("@AndroidId", _objrequestdetails.AndroidId),
                                      new SqlParameter("@DeviceId", _objrequestdetails.DeviceId),
                                      new SqlParameter("@CompanyId", "1"),
                                      new SqlParameter("@Fk_MemId", _objrequestdetails.Fk_MemID),
                                      new SqlParameter("@CreatedBy", _objrequestdetails.Fk_MemID),
                                      new SqlParameter("@ProcId", 1),
                                      new SqlParameter("@BillAmount", _objDetails.billerResponse.billAmount),
                                      new SqlParameter("@BillDate", _objDetails.billerResponse.billDate),
                                      new SqlParameter("@BillNumber", _objDetails.billerResponse.billNumber),
                                      new SqlParameter("@BillPeriod", _objDetails.billerResponse.billPeriod),
                                      new SqlParameter("@CustomerName", _objDetails.billerResponse.customerName),
                                      new SqlParameter("@DueDate", _objDetails.billerResponse.dueDate)

                                 };
            DataSet ds = DBHelper.ExecuteQuery(ProcedureName.BASaveAllTransactionDetails, para);

            return ds;
        }
        public DataSet SaveBillFetchResponseError(MBillerFetchResponse _objDetails, MBillerFetchRequestAPP _objrequestdetails)
        {

            SqlParameter[] para = {
                                         new SqlParameter("@RequestId", _objDetails.RequestId),
                                         new SqlParameter("@BillerId", _objrequestdetails.BillerId),
                                         new SqlParameter("@IPAdress", _objrequestdetails.IP),
                                         new SqlParameter("@AndroidId", _objrequestdetails.AndroidId),
                                         new SqlParameter("@DeviceId", _objrequestdetails.DeviceId),
                                         new SqlParameter("@CompanyId", "1"),
                                         new SqlParameter("@Fk_MemId", _objrequestdetails.Fk_MemID),
                                         new SqlParameter("@CreatedBy", _objrequestdetails.Fk_MemID),
                                         new SqlParameter("@ProcId", 1),
                                         new SqlParameter("@ErrorMsg", _objDetails.errorInfo.error.errorCode + ' ' + _objDetails.errorInfo.error.errorMessage)
                                       };
            DataSet ds = DBHelper.ExecuteQuery(ProcedureName.BASaveAllTransactionDetails, para);
            return ds;


        }
    }
}