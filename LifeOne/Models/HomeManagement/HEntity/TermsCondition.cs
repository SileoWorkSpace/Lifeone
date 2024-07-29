using DocumentFormat.OpenXml.Office2010.Excel;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.HomeManagement.HEntity
{
    public class TermsCondition
    {
        public string Pk_Id { get; set; }
        public string TermsandCondition { get; set; }
        public DataTable dtDetails { get; set; }


        public List<TermsCondition> lstTermsCondition { get; set; }

        public DataSet getTermsandCondition()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = {

                };
                ds = DBHelper.ExecuteQuery("TermsandConditionList", para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;


        }
    }
    public class TermsConditionResponse
    {
        public string Flag { get; set; }
        public string Msg { get; set; }
    }
}