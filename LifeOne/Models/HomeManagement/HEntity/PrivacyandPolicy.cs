﻿using DocumentFormat.OpenXml.Office2010.Excel;
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
    public class PrivacyandPolicy
    {
        public string Pk_Id { get; set; }
        public string PrivacyPolicy { get; set; }
        public DataTable dtDetails { get; set; }


        public List<PrivacyandPolicy> lstPrivacyandPolicy { get; set; }

        public DataSet getPrivacyPolicy()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = {

                };
                ds = DBHelper.ExecuteQuery("PrivacyPolicyList", para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
    public class PrivacyPolicyResponse
    {
        public string Flag { get; set; }
        public string Msg { get; set; }
    }
}