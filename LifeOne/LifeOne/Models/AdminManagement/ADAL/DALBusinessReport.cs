using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALBusinessReport
    {
        public string Fromdate { get; set; }
        public string ToDate { get; set; }
        public string LoginId { get; set; }
        public string fk_MemId { get; set; }

        public List<AdminBusinessReportModel> BusinessList(DALBusinessReport model)
        {
            List<AdminBusinessReportModel> BList = new List<AdminBusinessReportModel>();
            try
            {
                var QuaeryParameter = new DynamicParameters();

                QuaeryParameter.Add("@FromDate", model.Fromdate == "" ? null : model.Fromdate);
                QuaeryParameter.Add("@ToDate", model.ToDate == "" ? null : model.ToDate);
                QuaeryParameter.Add("@LoginId", model.LoginId == "" ? null : model.LoginId);
                QuaeryParameter.Add("@fk_MemId", model.fk_MemId == "" ? null : model.fk_MemId);
                BList = DBHelperDapper.DAAddAndReturnModelList<AdminBusinessReportModel>("AdminBusinessReport", QuaeryParameter);
               
            }
            catch(Exception ex)
            {
            }

            return BList;
        }


        public List<BusinessDetail>  ProductDetail(string InvoiceId)
        {
            List<BusinessDetail> PList = new List<BusinessDetail>();
            try
            {
                var QuaeryParameter = new DynamicParameters();
                QuaeryParameter.Add("@InvoiceId", InvoiceId);
                PList= DBHelperDapper.DAAddAndReturnModelList<BusinessDetail>("GetFranchiseOrderDetails", QuaeryParameter);
               
            }
            catch(Exception ex)
            {


            }
            return PList;

        }

    }
}