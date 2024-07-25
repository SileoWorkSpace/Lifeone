using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class TransactionDAL
    {

        public static List<RequestDetails> RequestDetails(RequestDetails _model)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@LoginId", _model.LoginId);
                queryParameters.Add("@Mobile", _model.Mobile);
                queryParameters.Add("@Name", _model.Name);
                queryParameters.Add("@Page", _model.Page);
                queryParameters.Add("@size", _model.size > 0 ? _model.size : SessionManager.Size);
                queryParameters.Add("@Fk_MemId", 0);
                List<RequestDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<RequestDetails>("RequestDetailsForAssociateFranchise", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static TransactionsResponse ApproveRequest(string Type,string status , string Reamrk, long Fk_MemId)
        {
            try
            {
                TransactionsResponse _iresult = new TransactionsResponse();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_MemId", Fk_MemId);
                queryParameters.Add("@Type", Type ?? string.Empty);
                queryParameters.Add("@Remark", Reamrk?? string.Empty);
                queryParameters.Add("@status", status??string.Empty);
                _iresult = DBHelperDapper.DAAddAndReturnModel<TransactionsResponse>("ApproveDeclineRequestDetailsForAssociateFranchise", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}