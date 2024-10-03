using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class AssosiateRequest
    {
        
            public string LoginId { get; set; }                    
            public string RequestId { get; set; }                    
            public string RequestDate { get; set; }                    
            public string Name { get; set; }                   
            public string Status { get; set; }                   
            public string Amount1 { get; set; }               
            public decimal Amount { get; set; }               
            public string PaymentMode { get; set; }           
            public string ChequeDD_No { get; set; }           
            public int? BankId { get; set; }   
            public HttpPostedFileBase File { get; set; }
            public string Image_url { get; set; }
        public string TransactionNo { get; set; }          
            public string ChequeDDNo { get; set; }          
            public string Date { get; set; }     
            public string Convert_date { get; set; }     
            public string UPI_Number  { get; set; }     
            public HttpPostedFileBase ImageFile { get; set; }            
            public string fileAttachment { get; set; }            
            public int OpCode { get; set; }            
            public string Code { get; set; }            
            public string Message { get; set; }            
            public string paymentid { get; set; }            
            public int Pk_BankId { get; set; }                   
            public int Fk_MemId { get; set; }                   
            public int AddedBy { get; set; }   
            public List<AssosiateRequest> lstAssosiateRequest { get; set; }
        public string orderId { get;  set; }
        public int Flag { get;  set; }

        public DataSet UpdateWalletByGateway()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@ChequeDDNo", ChequeDDNo),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@paymentid", paymentid),
                                     
                                  };
                DataSet ds = Connection.ExecuteQuery("UpdateWalletByGateway", para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}