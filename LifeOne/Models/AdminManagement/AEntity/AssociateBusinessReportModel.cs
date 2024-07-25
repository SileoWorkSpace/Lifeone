using Dapper;
//using LifeOne.Models.AdminManagement.ADAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class AssociateBusinessReportModel
    {
        public string LoginId { get; set; }
        public string DispayName { get; set; }
        public decimal TotalBV { get; set; }
        public decimal SelfBV { get; set; }         
        public decimal TeamBV { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SelfBVFrom { get; set; }
        public string TeamBVFrom { get; set; }
        public string SelfBVTo { get; set; }
        public string TeamBVTo { get; set; }
    }

    public class AssociateBusinessReportRepository
    {

        public List<AssociateBusinessReportModel> ABusinessReport(AssociateBusinessReportModel model)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LoginId",string.IsNullOrEmpty(model.LoginId)?null: model.LoginId);
                param.Add("@FromDate",string.IsNullOrEmpty(model.FromDate)?null: model.FromDate);
                param.Add("@ToDate",string.IsNullOrEmpty(model.ToDate)?null: model.ToDate);
                param.Add("@SelfBVFrom", string.IsNullOrEmpty(model.SelfBVFrom) ? null : model.SelfBVFrom);
                param.Add("@TeamBVFrom", string.IsNullOrEmpty(model.TeamBVFrom) ? null : model.TeamBVFrom);
                param.Add("@SelfBVTo", string.IsNullOrEmpty(model.SelfBVTo) ? null : model.SelfBVTo);
                param.Add("@TeamBVTo", string.IsNullOrEmpty(model.TeamBVTo) ? null : model.TeamBVTo);

                List<AssociateBusinessReportModel> BusinessList = DBHelperDapper.DAAddAndReturnModelList<AssociateBusinessReportModel>("AssociateMonthlyBusinessReport", param);
                return BusinessList;


            }
            catch(Exception ex)
            {
                throw ex;

            }



        }

        public List<AssociateBusinessModel> AssociateBusinessReport(AssociateBusinessModel model)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LoginId", string.IsNullOrEmpty(model.LoginId) ? null : model.LoginId);
                param.Add("@Mobile", string.IsNullOrEmpty(model.Mobile) ? null : model.Mobile);
                param.Add("@FromDate", string.IsNullOrEmpty(model.FromDate) ? null : model.FromDate);
                param.Add("@ToDate", string.IsNullOrEmpty(model.ToDate) ? null : model.ToDate);
                param.Add("@NoOfSponsor", string.IsNullOrEmpty(model.NoOfSponsor) ? null : model.NoOfSponsor);
                param.Add("@FromBV", string.IsNullOrEmpty(model.FromPV) ? null : model.FromPV);
                param.Add("@ToBV", string.IsNullOrEmpty(model.ToPV) ? null : model.ToPV);
          
                param.Add("@Page", model.Page);
                param.Add("@Size", model.Size);
                param.Add("@IsExport", model.IsExport);


                List<AssociateBusinessModel> BusinessList = DBHelperDapper.DAAddAndReturnModelList<AssociateBusinessModel>("GetAssociateBusiness", param);
                return BusinessList;


            }
            catch (Exception ex)
            {
                throw ex;

            }



        }
        public List<AssociateBusinessModel> AssociateBusinessRepurchase(AssociateBusinessModel model)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LoginId", string.IsNullOrEmpty(model.LoginId) ? null : model.LoginId);
                param.Add("@Mobile", string.IsNullOrEmpty(model.Mobile) ? null : model.Mobile);
                param.Add("@FromDate", string.IsNullOrEmpty(model.FromDate) ? null : model.FromDate);
                param.Add("@ToDate", string.IsNullOrEmpty(model.ToDate) ? null : model.ToDate);

                param.Add("@FromPV", string.IsNullOrEmpty(model.FromPV) ? null : model.FromPV);
                param.Add("@ToPV", string.IsNullOrEmpty(model.ToPV) ? null : model.ToPV);

                param.Add("@Page", model.Page);
                param.Add("@Size", model.Size);
                param.Add("@IsExport", model.IsExport);


                List<AssociateBusinessModel> BusinessList = DBHelperDapper.DAAddAndReturnModelList<AssociateBusinessModel>("GetAssociateBusiness_Repurchase", param);
                return BusinessList;


            }
            catch (Exception ex)
            {
                throw ex;

            }



        }

    }




    public class AssociateBusinessModel: MPaging
    {
        public string LoginId { get; set; }

        public string DispayName { get; set; }
        public string Mobile { get; set; }

        public string NoOfSponsor { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalBV { get; set; }


        public decimal TotalSelfPv { get; set; }
        public decimal TotalSelfAmount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
            public string FromPV { get; set; }
            public string ToPV { get; set; }
        public int IsExport { get; set; }
        public int TotalRecord { get; set; }

        public List<AssociateBusinessModel> LstOrder { get; set; }
    }

   
}