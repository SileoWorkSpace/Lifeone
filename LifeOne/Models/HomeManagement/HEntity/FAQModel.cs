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
    public class FAQModel
    {
        public int Pk_FAQId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }             
        public DataTable dtDetails { get; set; }       
        public List<FAQModel> FAQList { get; set; }

        public DataSet getFAQ()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = {
                new SqlParameter("@Pk_FAQId",Pk_FAQId),
            };
                ds = DBHelper.ExecuteQuery("GetFAQDetails", para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }      
    }
    public class FAQModelResponse
    {
        public string Flag { get; set; }
        public string Msg { get; set; }
    }
}