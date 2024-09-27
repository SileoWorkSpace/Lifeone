
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
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
    }
}