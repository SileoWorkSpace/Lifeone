
using DocumentFormat.OpenXml.Spreadsheet;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class OrderDAL : DBHelper
    {
        public DataSet GetOrderDetails(string OrderId)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@OrderId",OrderId)
            };
            DataSet dsresult = ExecuteQuery("OrderDetailsById", para);
            return dsresult;
        }

        public DataSet GetOrderDetailsByOrderId(string ReqId)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ReqId",ReqId)
            };
            DataSet dsresult = ExecuteQuery("OrderInvoice", para);
            return dsresult;
        }
        public DataSet GetOrderDetailsKaryon(string ReqId)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ReqId",ReqId)
            };
            DataSet dsresult = ExecuteQuery("OrderDetailsKaryon", para);
            return dsresult;
        }

        

        public DataSet GetCustomerInvoiceByOrderId(string ReqId)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ReqId",ReqId)
            };
            DataSet dsresult = ExecuteQuery("FranchiseCustomerInvoice", para);
            return dsresult;
        }
        public DataSet ActivatedCustomerInvoiceById(string ReqId)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_TransId",ReqId)
            };
            DataSet dsresult = ExecuteQuery("ActivatedMembersTaxInvoice", para);
            return dsresult;
        }
        public DataSet OpenOrderInvoiceByOrderId(string ReqId)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@ReqId",ReqId)
            };
            DataSet dsresult = ExecuteQuery("OpenOrderTaxInvoice", para);
            return dsresult;
        }
        public DataSet GetShoppingOrderInvoice(string id)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@fk_OrderId",id)
            };
            DataSet dsresult = ExecuteQuery("ShoppingOrderInvoice", para);
            return dsresult;
        }
        public DataSet GetTopupHistory(TopupHistory model)
        {
            try
            {
                SqlParameter[] para = {
               new SqlParameter("@Fk_MemId",model.Fk_MemId),
               new SqlParameter("@Page",model.Page),
               new SqlParameter("@Size",model.Size),
               new SqlParameter("@JoiningFormDate",model.JoiningFormDate),
               new SqlParameter("@JoiningToDate",model.JoiningToDate),
               new SqlParameter("@TopupFormDate",model.TopupFormDate),
               new SqlParameter("@TopupToDate",model.TopupToDate),
               new SqlParameter("@UTRNo",model.UTRNo),
               new SqlParameter("@PackageName",model.PackageName),
                };
                DataSet ds = DBHelper.ExecuteQuery("Topuphistory", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GettopupInvoice(string id)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Pk_TransId",id)
            };
            DataSet dsresult = ExecuteQuery("GetTopupInvoiceDetails", para);
            return dsresult;
        }
    }
}