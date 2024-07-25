using Dapper;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common ;

namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALBillInfo
    {
        public static int SaveBillInfoRequest(MbillInfoRequest _objDetails)
        {
            try
            {
                int _iresult = DBHelperDapper.DAAdd<MbillInfoRequest>("", _objDetails);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SaveBillInfoResponse(string xml)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@xmldocumentData", xml);

                int _iresult = DBHelperDapper.DAAdd("SaveBillerInfo", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<BillInfoDetails> GetBillerDetailsByBillerId(string BillerId)
        {
            try
            {
                List<BillInfoDetails> _iresult = new List<BillInfoDetails>();
                string _qury = "GetBillerDetailByBillerId " + BillerId + "";
                _iresult = DBHelperDapper.DAGetDetailsInList<BillInfoDetails>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static List<BillInfoDetailsByCategory> GetBillirIdFromCategory(string Category)
        {
            try
            {
                List<BillInfoDetailsByCategory> _iresult = new List<BillInfoDetailsByCategory>();
                string _qury = "GetBillerDetailByCategory '" + Category + "'";
                _iresult = DBHelperDapper.DAGetDetailsInList<BillInfoDetailsByCategory>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}