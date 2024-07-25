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
    public class Contact
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string DistributorID { get; set; }
        public string Grievance { get; set; }
        public string Message { get; set; }
        public string CompanyAddress1 { get; set; }
        public List<Contact> lstcontact { get; set; }

        public DataSet getInfgo()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = { 
                
                };
                ds = DBHelper.ExecuteQuery("Sp_GetCompanyInfo", para);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return ds;


        }
    }
    public class ContactResponse
    {
        public string Flag { get; set; }
        public string Msg { get; set; }
    }
}