using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class TransactionService
    {

        public List<RequestDetails> RequestDetails(RequestDetails _model)
        {

            List<RequestDetails> result = new List<RequestDetails>();
            try
            {
                result = TransactionDAL.RequestDetails(_model);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public TransactionsResponse ApproveRequest(string type,string status, string Reamrk, long Fk_MemId)
        {

            TransactionsResponse result = new TransactionsResponse();
            try
            {
                result = TransactionDAL.ApproveRequest(type, status, Reamrk, Fk_MemId);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}