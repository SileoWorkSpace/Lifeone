using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using LifeOne.Models.AdminManagement.ADAL;
using Dapper;


namespace LifeOne.Models.API.DAL
{
    public class AssociateBusinessReport 
    {
        public string FK_MemId { get; set; }
        public string FromDate { get; set; }
        public string LoginId { get; set; }
        public string ToDate { get; set; }
        public string InvoiceId { get; set; }
        public string TeamType { get; set; }
        public int? Page { get; set; }
        public int Size { get; set; }
        public string SearchKey { get; set; }
        public string SearchValue { get; set; }
        public Models.Common.Pager Pager { get; set; }
        public int EndPage { get; private set; }
        public List<AssociateBusinessSummaryModel> AssociateBusinessSummaryList { get; set; }
        public List<AssociateBusinessSummaryModel> AssociateBR(AssociateBusinessReport model)
        {
          List<AssociateBusinessSummaryModel> TBRList = new List<AssociateBusinessSummaryModel>();
            try
            {
                var QuaryParameter = new DynamicParameters();
                QuaryParameter.Add("@Fk_MemId", model.FK_MemId);
                QuaryParameter.Add("@fromDate", model.FromDate == "" ? null : model.FromDate);
                QuaryParameter.Add("@ToDate", model.ToDate == "" ? null : model.ToDate);
                QuaryParameter.Add("@TeamType", model.TeamType == "" ? null : model.TeamType);
                QuaryParameter.Add("@LoginId", model.LoginId == "" ? null : model.LoginId);
                QuaryParameter.Add("@Page", model.Page);
                QuaryParameter.Add("@Size", model.Size);
               TBRList = DBHelperDapper.DAAddAndReturnModelList<AssociateBusinessSummaryModel>("AssociateBusinessSummaryReport", QuaryParameter);
                

            }
            catch(Exception ex)
            {

            }
            return TBRList;

        }
        public List<AssociateBusinessSummaryModel> AssociateBDetailsById(AssociateBusinessReport model)
        {
            List<AssociateBusinessSummaryModel> TBRList = new List<AssociateBusinessSummaryModel>();
            try
            {
                var QuaeryParameter = new DynamicParameters();
                QuaeryParameter.Add("@InvoiceId", model.InvoiceId);

                TBRList = DBHelperDapper.DAAddAndReturnModelList<AssociateBusinessSummaryModel>("AssociateBusinessSummaryByInvoiceId", QuaeryParameter);

              
            }
            catch (Exception ex)
            {

            }

            return TBRList;
        }
        public List<BusinessSummaryProductDetail> BusinessProductDetail(string InvoiceId)
        {
            List<BusinessSummaryProductDetail> BSPD = new List<BusinessSummaryProductDetail>();
            try
            {
                var QuaeryParameter = new DynamicParameters();
                QuaeryParameter.Add("@InvoiceId", InvoiceId);
                BSPD = DBHelperDapper.DAAddAndReturnModelList<BusinessSummaryProductDetail>("AssociateBusinessSummaryByInvoiceId", QuaeryParameter);
            }
            catch(Exception ex)
            {
            }
            return BSPD;
        }

    }
}