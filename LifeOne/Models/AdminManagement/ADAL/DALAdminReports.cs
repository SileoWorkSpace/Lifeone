using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Security;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALAdminReports
    {
        public static List<MTDSReport> GetTDSReport(MTDSReport obj)
        {
            List<MTDSReport> _iresult = new List<MTDSReport>();
            var queryParameters = new DynamicParameters();
            //queryParameters.Add("@FinancealMonth", obj.FinancealMonth);
            //queryParameters.Add("@FinancealYear", obj.FinancealYear);
            //queryParameters.Add("@PanNo", obj.PanNo);
            //obj.PayoutNo = 1;
          //  queryParameters.Add("@PayoutNo", obj.PayoutNo=="null"?"1":obj.PayoutNo);
            SqlParameter[] param =
            {
                new SqlParameter("@PayoutNo",string.IsNullOrEmpty(obj.PayoutNo)?null:obj.PayoutNo),
                new SqlParameter("@LoginId",string.IsNullOrEmpty(obj.LoginId)?null:obj.LoginId),
                 new SqlParameter("@PanNo",string.IsNullOrEmpty(obj.PanNo)?null:obj.PanNo),
                 new SqlParameter("@FinancealMonth",string.IsNullOrEmpty(obj.FinancealMonth)?null:obj.FinancealMonth),
                 new SqlParameter("@FinancealYear",string.IsNullOrEmpty(obj.FinancealYear)?null:obj.FinancealYear),
                 new SqlParameter("@FromDate",string.IsNullOrEmpty(obj.FromDate)?null:obj.FromDate),
                 new SqlParameter("@ToDate",string.IsNullOrEmpty(obj.ToDate)?null:obj.ToDate),
                 new SqlParameter("@FromPaymentDate",string.IsNullOrEmpty(obj.FromPaymentDate)?null:obj.FromPaymentDate),
                 new SqlParameter("@ToPaymentDate",string.IsNullOrEmpty(obj.ToPaymentDate)?null:obj.ToPaymentDate),
                 new SqlParameter("@Page",obj.Page),
                 new SqlParameter("@Size",obj.Size),
                 new SqlParameter("@IsExport",obj.IsExport),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetTDSReport", param); 
            if(ds !=null && ds.Tables[0].Rows.Count>0)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    _iresult.Add(new MTDSReport
                    {
                        LoginId=dr["LoginId"].ToString(),
                        Name=dr["Name"].ToString(),
                        PayoutNo1 = dr["PayoutNo"].ToString(),
                        Mobile =dr["MobileNo"].ToString(),
                        PanNo=dr["PanNo"].ToString(),
                        GrossAmount=Convert.ToDecimal(dr["GrossAmount"].ToString()),
                        NetAmount= Convert.ToDecimal(dr["NetAmount"].ToString()),
                        TDS = Convert.ToDecimal(dr["TDS"].ToString()),
                        ClosingDate = dr["ClosingDate"].ToString(),
                        PaymentDate = dr["PaymentDate"].ToString(),
                        TotalRecords = int.Parse(dr["TotalRecords"].ToString()),

                    });

                }


            }
           
            return _iresult;
        }


        public static List<MTDSReport> GetPanCard_TDSReport(MTDSReport obj)
        {
            List<MTDSReport> _iresult = new List<MTDSReport>();
            var queryParameters = new DynamicParameters();
     
            SqlParameter[] param =
            {
                new SqlParameter("@PayoutNo",string.IsNullOrEmpty(obj.PayoutNo)?null:obj.PayoutNo),
                new SqlParameter("@LoginId",string.IsNullOrEmpty(obj.LoginId)?null:obj.LoginId),
                 new SqlParameter("@PanNo",string.IsNullOrEmpty(obj.PanNo)?null:obj.PanNo),
                 new SqlParameter("@FinancealMonth",string.IsNullOrEmpty(obj.FinancealMonth)?null:obj.FinancealMonth),
                 new SqlParameter("@FinancealYear",string.IsNullOrEmpty(obj.FinancealYear)?null:obj.FinancealYear),
                 new SqlParameter("@FromDate",string.IsNullOrEmpty(obj.FromDate)?null:obj.FromDate),
                 new SqlParameter("@ToDate",string.IsNullOrEmpty(obj.ToDate)?null:obj.ToDate),
                 new SqlParameter("@Page",obj.Page),
                 new SqlParameter("@Size" ,obj.Size),
                 new SqlParameter("@IsExport" ,obj.IsExport),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetTDSReport_Pancard", param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    _iresult.Add(new MTDSReport
                    {
                      
                        Name = dr["Name"].ToString(),
                      
                        Mobile = dr["MobileNo"].ToString(),
                        PanNo = dr["PanNo"].ToString(),
                        GrossAmount = Convert.ToDecimal(dr["GrossAmount"].ToString()),
                        NetAmount = Convert.ToDecimal(dr["NetAmount"].ToString()),
                        TDS = Convert.ToDecimal(dr["TDS"].ToString()),
                        TotalRecords = int.Parse(dr["TotalRecords"].ToString())

                    });

                }


            }

            return _iresult;
        }
        public static List<MPayoutReport> GetPayoutReport(int? Page ,MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", obj.LoginId);
            queryParameters.Add("@PayoutNo", obj.PayountNo);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
            queryParameters.Add("@IsExport", obj.IsExport);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("PayoutIncomeReport", queryParameters);
            return _iresult;
        }

        public static List<FranchiseBusinessModel> GetFranchiseBusiness(FranchiseBusinessModel obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size);
            queryParameters.Add("@IsExport", obj.IsExport);
            List<FranchiseBusinessModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<FranchiseBusinessModel>("GetFranchiseBusiness", queryParameters);
            return _iresult;
        }
        public static List<MPayoutReport> GetAssociatePayoutReportList(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FK_MemID", obj.FK_MemId);
            queryParameters.Add("@PayoutNo", obj.PayountNo);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("AssociatePayoutIncomeReport", queryParameters);
            return _iresult;
        }

        public static List<MPayoutReport> PaidUnPaidPayoutReport(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", obj.LoginId);
            queryParameters.Add("@PayoutNo", obj.PayountNo);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
            queryParameters.Add("@IsExport", obj.IsExport);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("UnPaidPayoutIncomeReport", queryParameters);
            return _iresult;
        }
    public static List<MPayoutReport> ReParchasePaidUnPaidPayoutReport(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", obj.LoginId);
            queryParameters.Add("@PayoutNo", obj.PayountNo);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
            queryParameters.Add("@IsExport", obj.IsExport);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("ReparchaseUnPaidPayoutIncomeReport", queryParameters);
            return _iresult;
        }
        public static List<MPayoutReport> RepurchasePayoutIncomeReport(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", obj.LoginId);
            queryParameters.Add("@PayoutNo", obj.PayountNo);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("RepurchasePayoutIncomeReport", queryParameters);
            return _iresult;
        }

        public static List<MPayoutReport> RepurchasePayoutIncomeReportType(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FK_MemID", obj.FK_MemId);
            queryParameters.Add("@PayoutNo", obj.PayountNo);
            queryParameters.Add("@IncomeType", obj.IncomeType);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("RepurchasePayoutIncomeReportType", queryParameters);
            return _iresult;
        }



        public static List<MPayoutReport> GetAssociatePayoutReport(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", obj.LoginId);
            queryParameters.Add("@PayoutNo", obj.PayountNo);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@IncomeType", obj.IncomeType);
            queryParameters.Add("@FK_MemID", obj.FK_MemId);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("PayoutIncomeReportType", queryParameters);
            return _iresult;
        }


        public static List<MPayoutReport> GetRepurchasePayoutReport(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", obj.LoginId);
            queryParameters.Add("@PayoutNo", obj.PayountNo);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
            queryParameters.Add("@IsExport", obj.IsExport);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("RepurchasePayoutIncomeReport", queryParameters);
            return _iresult;
        }


        public static List<MPayoutReport> GetRepurchaseIncomePayoutReport(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", obj.LoginId);
            queryParameters.Add("@FK_MemID", obj.FK_MemId);
            queryParameters.Add("@PayoutNo", obj.PayountNo);
            queryParameters.Add("@IncomeType", obj.IncomeType);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
            queryParameters.Add("@IsExport", obj.IsExport);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("RepurchasePayoutIncomeReportType", queryParameters);
            return _iresult;
        }

        public static List<MPayoutReport> GetDownlinePayoutReport(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", obj.LoginId);
            queryParameters.Add("@PayoutNo", obj.PayoutNo);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
            queryParameters.Add("@IsExport", obj.IsExport);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("PayoutIncomeReportByDownline", queryParameters);
            return _iresult;
        }

        public static List<MPayoutReport> GetPayoutReportForBank(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@fromDate", obj.fdate);
            queryParameters.Add("@toDate", obj.tdate);
            queryParameters.Add("@payoutNo", obj.PayoutNo==0?null:obj.PayountNo);            
            queryParameters.Add("@memberLoginId", obj.MemberLoginId);
            //queryParameters.Add("@KycStatus", obj.KycStatus);
            queryParameters.Add("@Iskyc", obj.IsKyc);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
            queryParameters.Add("@IsExport", obj.IsExport);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("PayoutReportForBank", queryParameters);
            return _iresult;
        }
  public static List<MPayoutReport> GetRepurchasePayoutReportForBank(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@fromDate", obj.fdate);
            queryParameters.Add("@toDate", obj.tdate);
            queryParameters.Add("@payoutNo", obj.PayoutNo);
            queryParameters.Add("@memberLoginId", obj.MemberLoginId);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
            queryParameters.Add("@IsExport", obj.IsExport);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("RepurchasePayoutReportForBank", queryParameters);
            return _iresult;
        }
        public static List<StatementDetails> GetStatementDetails(int FK_MemId,string PayoutNo)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FK_MemId", FK_MemId);
            queryParameters.Add("@PayoutNo", PayoutNo);
            List<StatementDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<StatementDetails>("StatementDetails", queryParameters);
            return _iresult;
        }

        public static List<StatementDetails> GetRepurchaseIncomeDetails(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FK_MemId", obj.FK_MemId);
            queryParameters.Add("@PayoutNo", obj.PayoutNo);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
            List<StatementDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<StatementDetails>("RepurchaseIncomeDetails", queryParameters);
            return _iresult;
        }
        public static List<StatementDetails> GetStatementDetailsByIncomeType(int FK_MemId,string IncomeType)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FK_MemId", FK_MemId);
            queryParameters.Add("@IncomeType", IncomeType);
            List<StatementDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<StatementDetails>("StatementDetailsByIncomeType", queryParameters);
            return _iresult;
        }

        public static List<StatementDetails> GetStatementDetailsForAssociate(int FK_MemId, int PayoutNo, int Page, int Size)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FK_MemId", FK_MemId);
            queryParameters.Add("@PayoutNo", PayoutNo);
            queryParameters.Add("@Page", Page);
            queryParameters.Add("@PageSize", Size);
            List<StatementDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<StatementDetails>("RepurchaseIncomeDetails", queryParameters);
            return _iresult;
        }
        public static List<MPayoutReport> GetPayoutPayment(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@fromDate", obj.fdate);
            queryParameters.Add("@toDate", obj.tdate);
            queryParameters.Add("@payoutNo", obj.PayoutNo);
            queryParameters.Add("@memberLoginId", obj.MemberLoginId);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", SessionManager.Size);

            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("PayoutDetailsForUpdatePayment", queryParameters);//PayoutReportGetAll
            return _iresult;
        }
      public static List<MPayoutReport> GetRepurchasePayoutPayment(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@fromDate", obj.fdate);
            queryParameters.Add("@toDate", obj.tdate);
            queryParameters.Add("@payoutNo", obj.PayoutNo);
            queryParameters.Add("@memberLoginId", obj.MemberLoginId);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", SessionManager.Size);

            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("RepurchasePayoutDetailsForUpdatePayment", queryParameters);//PayoutReportGetAll
            return _iresult;
        }
        public static List<AssociatePurchaseDetails> PurchaseDetailsForAssociate(AssociatePurchaseDetails obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_MemId", obj.Fk_MemId);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size);
            List<AssociatePurchaseDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<AssociatePurchaseDetails>("Proc_FSupplyOrderListForAssociate", queryParameters);
            return _iresult;
        }

        public static MPayoutReport SavePayoutIncomeDetail(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@PK_PaidMembersId", obj.PaidMemberId);
            queryParameters.Add("@DirectIncome", obj.DirectIncome);
            queryParameters.Add("@TeamBuildingBonus", obj.TeamBuildingBonus);
            queryParameters.Add("@BinaryIncome", obj.BinaryIncome);
            queryParameters.Add("@SponsorRoyalty", obj.SponsorRoyalty);
            queryParameters.Add("@NetAmount", obj.NetAmount);
            queryParameters.Add("@UpdatedBy", obj.UpdateBy);
            MPayoutReport _iresult = DBHelperDapper.DAAddAndReturnModel<MPayoutReport>("Proc_SavePayoutIncomeDetail", queryParameters);
            return _iresult;
        }


        public static List<AffiliatedUtilityShopping> UtilityAfiliatedShopping(AffiliatedUtilityShopping obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_MemId", obj.fk_MemId);
            queryParameters.Add("@RechargeType", obj.ActionType);

            List<AffiliatedUtilityShopping> _iresult = DBHelperDapper.DAAddAndReturnModelList<AffiliatedUtilityShopping>("GetUtilityShopping", queryParameters);
            return _iresult;
        }

        public static List<MPayoutReport> GetRepurchasePayoutReport_AdP(MPayoutReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", obj.LoginId);
            queryParameters.Add("@PayoutNo", obj.PayountNo);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Page", obj.Page);
            queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
            queryParameters.Add("@IsExport", obj.IsExport);
            List<MPayoutReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutReport>("RepurchasePayoutIncomeReport_AdP", queryParameters);
            return _iresult;
        }

    }
    public class DALMemberBankDetails
    {
        public static List<MAdminMemberBankDetails> GetMemberBankDetails(MAdminMemberBankDetails model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", model.LoginId);
            queryParameters.Add("@Mobile", model.Mobile);
            queryParameters.Add("@Name", model.Name);
            queryParameters.Add("@Page", model.Page);
            queryParameters.Add("@Size", model.Size > 0 ? model.Size : SessionManager.Size);
            List<MAdminMemberBankDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminMemberBankDetails>("GetMemberBankDetails", queryParameters);
            return _iresult;
        }
    }

    public class DALCommission
    {
        public static List<MAdminCommission> GetCommissionListofUser(MAdminCommission model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", model.LoginId);
            queryParameters.Add("@Mobile", model.Mobile);
            queryParameters.Add("@Name", model.Name);
            queryParameters.Add("@Page", model.Page);
            queryParameters.Add("@Size", model.Size > 0 ? model.Size : SessionManager.Size);
            List<MAdminCommission> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminCommission>("GetCommssionList", queryParameters);
            return _iresult;
        }
    }

    public class DALUtilityBiilpayment
    {
        public static List<MAdminUtilityBiilpayment> GetUtilityBiilpaymentListofUser(MAdminUtilityBiilpayment model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@procid", model.ProcId);
            queryParameters.Add("@LoginId", model.LoginId);
            queryParameters.Add("@Name", model.Name);
            queryParameters.Add("@Mobile", model.Mobile);
            queryParameters.Add("@TransactionAmount", model.TransactionAmount);
            queryParameters.Add("@Page", model.Page);
            queryParameters.Add("@Size", model.Size);
            queryParameters.Add("@IsExport", model.IsExport);
            List<MAdminUtilityBiilpayment> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminUtilityBiilpayment>("proc_AssociateTransactions", queryParameters);
            return _iresult;
        }
    }

    public class DALAllwalletdetails
    {
        public static List<MAdminAllwalletdetails> GetAllwalletdetailsList(MAdminAllwalletdetails model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", model.LoginId);
            queryParameters.Add("@Name", model.MemberName);
            queryParameters.Add("@Mobile", model.Mobile);
            queryParameters.Add("@FromDate", model.FromDate);
            queryParameters.Add("@ToDate", model.ToDate);
            queryParameters.Add("@page", model.page > 0 ? model.page : 1);
            queryParameters.Add("@size", model.size > 0 ? model.size : SessionManager.Size);
            List<MAdminAllwalletdetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminAllwalletdetails>("Proc_GetAllWalletBalance", queryParameters);
            return _iresult;
        }
    }

    public class DALRazorPaymentSearch
    {
        public static List<MAdminRazorPaymentSearch> GetRazorPaymentSearch(MAdminRazorPaymentSearch model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", model.LoginId);
            queryParameters.Add("@Name", model.MemberName);
            queryParameters.Add("@Mobile", model.Mobile);
            queryParameters.Add("@page", model.page > 0 ? model.page : 1);
            queryParameters.Add("@size", model.size > 0 ? model.size : SessionManager.Size);
            List<MAdminRazorPaymentSearch> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminRazorPaymentSearch>("Proc_GetRazorpaymentlist", queryParameters);
            return _iresult;
        }
    }

    public class DALMemberKYC
    {
        public static List<MAdminMemberKYC> GetActiveUnActiveMemberList(MAdminMemberKYC model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", model.LoginId);
            queryParameters.Add("@Name", model.Name);
            queryParameters.Add("@Mobile", model.Mobile);
            queryParameters.Add("@page", model.Page > 0 ? model.Page : 1);
            queryParameters.Add("@size", model.size > 0 ? model.size : SessionManager.Size);

            List<MAdminMemberKYC> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminMemberKYC>("GetActiveUnActiveMemberList", queryParameters);
            return _iresult;
        }
    }

    public class DALIncomeType
    {
        public static List<MAdminIncomeType> GetInComeTypeList(MAdminIncomeType model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@ProcId", model.ProcId);
            queryParameters.Add("@Name", model.Name);
            queryParameters.Add("@Mobile", model.Mobile);
            queryParameters.Add("@LoginId", model.LoginId);
            queryParameters.Add("@page", model.Page > 0 ? model.Page : 1);
            queryParameters.Add("@size", model.size > 0 ? model.size : SessionManager.Size);

            List<MAdminIncomeType> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminIncomeType>("GetIncomeTypeWiseCommission", queryParameters);
            return _iresult;
        }

        public static List<MAdminIncomeType> GetIncomeIdWise(long Fk_Memid)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_Memid", Fk_Memid);
            queryParameters.Add("@ProcId", 2);



            List<MAdminIncomeType> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminIncomeType>("GetRepurchaseIncomeIdWise", queryParameters);
            return _iresult;
        }

        public static List<MAdminIncomeType> GetRepurchaseIncomeIdWise(long Fk_Memid)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_Memid", Fk_Memid);
            queryParameters.Add("@ProcId", 1);



            List<MAdminIncomeType> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminIncomeType>("GetRepurchaseIncomeIdWise", queryParameters);
            return _iresult;
        }
        public static List<MAdminIncomeType> GetInrDealsTransactionList(MAdminIncomeType model)
        {
            var queryParameters = new DynamicParameters();


            queryParameters.Add("@Mobile", model.Mobile);
            queryParameters.Add("@Name", model.Name);
            queryParameters.Add("@LoginId", model.LoginId);
            queryParameters.Add("@page", model.Page > 0 ? model.Page : 1);
            queryParameters.Add("@size", model.size > 0 ? model.size : SessionManager.Size);

            List<MAdminIncomeType> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminIncomeType>("GetInrDealsTransactions", queryParameters);
            return _iresult;
        }
    }


    public class DALVendorDetails
    {
        public static List<MAdminVendorDetails> GetVendorDetailsList(MAdminVendorDetails model)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@Name", model.Name);
            queryParameters.Add("@Mobile", model.Mobile);
            queryParameters.Add("@page", model.Page > 0 ? model.Page : 1);
            queryParameters.Add("@Size", model.Size > 0 ? model.Size : SessionManager.Size);

            List<MAdminVendorDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminVendorDetails>("GetVendorDetails", queryParameters);
            return _iresult;
        }

        public static List<MAdminVendorDetails> GetVendorOrderList(MAdminVendorDetails model)
        {
            var queryParameters = new DynamicParameters();


            queryParameters.Add("@VendorId", model.Fk_VendorId);
            queryParameters.Add("@page", model.Page > 0 ? model.Page : 1);
            queryParameters.Add("@Size", model.Size > 0 ? model.Size : SessionManager.Size);

            List<MAdminVendorDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminVendorDetails>("GetVendorOrderList", queryParameters);
            return _iresult;
        }
    }


    public class DALAchieversDetails
    {
        public static List<MAdminAchieversDetails> GetAchieversDetails(MAdminAchieversDetails model)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@LoginId", model.LoginId);
            queryParameters.Add("@Name", model.Name);
            queryParameters.Add("@Mobile", model.Mobile);
            queryParameters.Add("@page", model.Page > 0 ? model.Page : 1);
            queryParameters.Add("@Size", model.Size > 0 ? model.Size : SessionManager.Size);


            List<MAdminAchieversDetails> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminAchieversDetails>("GetAchieversList", queryParameters);
            return _iresult;
        }
    }

    public class DALBlockUnblock
    {
        public static List<MAdminBlockUnblock> GetBlockUnblockMemberList(MAdminBlockUnblock model)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Name", model.Name);
                queryParameters.Add("@LoginId", model.LoginId);
                queryParameters.Add("@Mobile", model.Mobile);
                queryParameters.Add("@EmailId", model.Email);
                queryParameters.Add("@Status", model.Status);
                queryParameters.Add("@page", model.Page > 0 ? model.Page : 1);
                queryParameters.Add("@Size", model.Size > 0 ? model.Size : SessionManager.Size);
                queryParameters.Add("@IsExport", model.IsExport);
                if (model.Fk_MemId > 0)
                    queryParameters.Add("@FK_MemId", model.Fk_MemId);


                List<MAdminBlockUnblock> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminBlockUnblock>("BlockUnblockMemberList", queryParameters);
                return _iresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static BlockUnblockResponse BlockUnblockMembers(string id, long Createdby)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@fk_MemId", id);
                queryParameters.Add("@CreatedBy", Createdby);

                BlockUnblockResponse _iresult = DBHelperDapper.DAAddAndReturnModel<BlockUnblockResponse>("BlockUnblock", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static BlockUnblockResponse BlockUnblockMembersV2(long Fk_MemId, string Status,long CreatedBy, string Remark)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FkMemid", Fk_MemId);
                queryParameters.Add("@BlockType", Status);
                queryParameters.Add("@CreatedBy", CreatedBy);
                queryParameters.Add("@Remark", Remark);
                BlockUnblockResponse _iresult = DBHelperDapper.DAAddAndReturnModel<BlockUnblockResponse>("Proc_BlockMember", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static BlockUnblockResponse UnblockUser(long Fk_MemId, string Purpose, long CreatedBy)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@BlockType", Purpose);
                queryParameters.Add("@FkMemid", Fk_MemId);
                queryParameters.Add("@CreatedBy", CreatedBy);
                BlockUnblockResponse _iresult = DBHelperDapper.DAAddAndReturnModel<BlockUnblockResponse>("Proc_UnblockMember", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class DALGetMember
    {
        public static List<MMemberList> GetDirectMemberDetails(long Fk_Memid, int ProcId, int? Page, long Size)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_Memid", Fk_Memid);
            queryParameters.Add("@ProcId", ProcId);
            queryParameters.Add("@page", Page > 0 ? Page : 1);
            queryParameters.Add("@Size", Size > 0 ? Size : SessionManager.Size);
            List<MMemberList> _iresult = DBHelperDapper.DAAddAndReturnModelList<MMemberList>("GetDirectDownline", queryParameters);
            return _iresult;
        }
    }

    public class DALGetMemberDownline
    {
        public static List<MMemberList> GetMembersDownlineDetails(MMemberList _model)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_Memid", _model.FK_MemId);
            queryParameters.Add("@ProcId", 2);
            queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
            queryParameters.Add("@MemberName", _model.MemberName ?? string.Empty);
            queryParameters.Add("@Mobile", _model.Mobile ?? string.Empty);
            queryParameters.Add("@Page", 1);
            queryParameters.Add("@Size", 50);
            List<MMemberList> _iresult = DBHelperDapper.DAAddAndReturnModelList<MMemberList>("GetDirectDownline", queryParameters);
            return _iresult;
        }
    }


    public class DALMembers
    {
        public static List<MMemberList> GetMemberList(MMemberList _model)
        {
            try
            {
                List<MMemberList> _iresult = new List<MMemberList>();
                //string _qury = "GetMembers @LoginId=" + _model.LoginId ?? null + " ,@Mobile"+ _model.Mobile ?? null + ",@Name" + _model.MemberName ?? null + ",@Page" + _model.Page + ",@size" + 50 + ",@procid" + _model.pageurl + ",@City" + _model.City ?? null + ",@InvitedBy" + _model.InvitedBy ?? null + ",@InviteCode" + _model.InviteCode ?? null + ",@PinCode" + _model.PinCode ?? null + "";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
                queryParameters.Add("@Mobile", _model.Mobile ?? string.Empty);
                queryParameters.Add("@Name", _model.MemberName ?? string.Empty);
                queryParameters.Add("@Page", _model.Page);
                queryParameters.Add("@size", SessionManager.Size);
                queryParameters.Add("@procid", 1);
                queryParameters.Add("@City", _model.City ?? string.Empty);
                queryParameters.Add("@InvitedBy", _model.InvitedBy ?? string.Empty);
                queryParameters.Add("@InviteCode", _model.InviteCode ?? string.Empty);
                queryParameters.Add("@PinCode", _model.PinCode ?? string.Empty);
                queryParameters.Add("@FormDate", _model.FromDate ?? string.Empty);
                queryParameters.Add("@TODate", _model.ToDate ?? string.Empty);
                queryParameters.Add("@IsExport", _model.IsExport);
                _iresult = DBHelperDapper.DAAddAndReturnModelList<MMemberList>("GetMembers", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MMemberList> DownloadMemberList()
        {
            try
            {
                List<MMemberList> _iresult = new List<MMemberList>();
                //string _qury = "GetMembers @LoginId=" + _model.LoginId ?? null + " ,@Mobile"+ _model.Mobile ?? null + ",@Name" + _model.MemberName ?? null + ",@Page" + _model.Page + ",@size" + 50 + ",@procid" + _model.pageurl + ",@City" + _model.City ?? null + ",@InvitedBy" + _model.InvitedBy ?? null + ",@InviteCode" + _model.InviteCode ?? null + ",@PinCode" + _model.PinCode ?? null + "";
               
                _iresult = DBHelperDapper.DAGetDetailsInList<MMemberList>("DownloadMemberList");
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<MAdminMembers> GetTopTenTodaysReferals(MAdminMembers model)
        {
            var queryParameters = new DynamicParameters();
            List<MAdminMembers> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminMembers>("GetTopTenTodaysReferals", queryParameters);
            return _iresult;
        }

        public static MembersResponse MemberAssociateByAdmin(long FK_MemId, bool status)
        {
            try
            {
                MembersResponse _iresult = new MembersResponse();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", FK_MemId);
                queryParameters.Add("@status", status);
                _iresult = DBHelperDapper.DAAddAndReturnModel<MembersResponse>("MemberAssociateByAdmin", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MembersResponse DalBlockReffereal(long FK_MemId, bool Status, string remark)
        {
            try
            {
                MembersResponse _iresult = new MembersResponse();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", FK_MemId);
                queryParameters.Add("@remark", remark);
                queryParameters.Add("@status", Status);
                _iresult = DBHelperDapper.DAAddAndReturnModel<MembersResponse>("Proc_Blockreferral", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MMemberList> GetRecentMemberList(MMemberList _model)
        {
            try
            {
                List<MMemberList> _iresult = new List<MMemberList>();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
                queryParameters.Add("@Mobile", _model.Mobile ?? string.Empty);
                queryParameters.Add("@Name", _model.MemberName ?? string.Empty);
                queryParameters.Add("@Page", _model.Page);
                queryParameters.Add("@size", SessionManager.Size);
                queryParameters.Add("@procid", 1);
                queryParameters.Add("@City", _model.City ?? string.Empty);
                queryParameters.Add("@InvitedBy", _model.InvitedBy ?? string.Empty);
                queryParameters.Add("@InviteCode", _model.InviteCode ?? string.Empty);
                queryParameters.Add("@PinCode", _model.PinCode ?? string.Empty);
                _iresult = DBHelperDapper.DAAddAndReturnModelList<MMemberList>("GetRecentMembers", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MemberTestimonialModel> GetTestimonialDetail(MemberTestimonialModel _model)
        {
            try
            {
                List<MemberTestimonialModel> _iresult = new List<MemberTestimonialModel>();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@LoginId", _model.LoginId ?? null);
                queryParameters.Add("@Name", _model.Name ?? null);
                queryParameters.Add("@Mobile", _model.Mobile ?? null);
                queryParameters.Add("@PageNo", _model.Page ?? 1);
                queryParameters.Add("@PageSize", SessionManager.Size);
                //queryParameters.Add("@PageSize", 1);   

                _iresult = DBHelperDapper.DAAddAndReturnModelList<MemberTestimonialModel>("Proc_TestimonialDetail", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MemberTestimonialModel> ExportToExcelTestimonials(MemberTestimonialModel _model)
        {
            try
            {
                List<MemberTestimonialModel> _iresult = new List<MemberTestimonialModel>();
                var queryParameters = new DynamicParameters();
                _iresult = DBHelperDapper.DAAddAndReturnModelList<MemberTestimonialModel>("Proc_ExportToExcelTestimonialDetail", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static AddUpdateDeleteModel SaveTestimonialStatus(MemberTestimonialModel _model)
        {
            try
            {
                AddUpdateDeleteModel _iresult = new AddUpdateDeleteModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@UpdateBy", SessionManager.Fk_MemId);
                queryParameters.Add("@IsApproved", _model.IsApprove);
                queryParameters.Add("@IsDecline", _model.IsDecline);
                queryParameters.Add("@Remark", _model.Remark);
                queryParameters.Add("@SrNo", _model.Id);
                _iresult = DBHelperDapper.DAAddAndReturnModel<AddUpdateDeleteModel>("Proc_TestimonialStatus", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MembersResponse UpdateMemberTestimonialStatus(long FK_MemId, bool status)
        {
            try
            {
                MembersResponse _iresult = new MembersResponse();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", FK_MemId);
                queryParameters.Add("@status", status);
                queryParameters.Add("@CreatedBy", status);

                _iresult = DBHelperDapper.DAAddAndReturnModel<MembersResponse>("MemberTestimonialByAdmin", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<TestimonialPermissionViewModel> TestimonialPermissionLeaders(TestimonialPermissionViewModel _model)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
                queryParameters.Add("@Mobile", _model.Mobile ?? string.Empty);
                queryParameters.Add("@Name", _model.MemberName ?? string.Empty);
                queryParameters.Add("@Page", _model.Page == null ? 1 : _model.Page);
                queryParameters.Add("@size", SessionManager.Size);
                queryParameters.Add("@procid", 1);
                queryParameters.Add("@City", _model.City ?? string.Empty);
                queryParameters.Add("@InvitedBy", _model.InvitedBy ?? string.Empty);
                queryParameters.Add("@InviteCode", _model.InviteCode ?? string.Empty);
                queryParameters.Add("@PinCode", _model.PinCode ?? string.Empty);
                queryParameters.Add("@FormDate", _model.FromDate ?? string.Empty);
                queryParameters.Add("@TODate", _model.ToDate ?? string.Empty);
                queryParameters.Add("@IsAllowTestimonial", _model.IsAllowTestimonial);
                _model.List = DBHelperDapper.DAAddAndReturnModelList<TestimonialPermissionViewModel>("Proc_TestimonialLeaderPermission", queryParameters);
                return _model.List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<EmployeesViewModel> Employees(EmployeesViewModel _model)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", _model.FK_MemId);
                queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
                queryParameters.Add("@Mobile", _model.Mobile ?? string.Empty);
                queryParameters.Add("@Name", _model.MemberName ?? string.Empty);
                queryParameters.Add("@Page", _model.Page == null ? 1 : _model.Page);
                queryParameters.Add("@size", SessionManager.Size);
                queryParameters.Add("@FormDate", _model.FromDate ?? string.Empty);
                queryParameters.Add("@TODate", _model.ToDate ?? string.Empty);
                _model.EmployeeList = DBHelperDapper.DAAddAndReturnModelList<EmployeesViewModel>("Proc_EmployeeDetail", queryParameters);
                return _model.EmployeeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static EmployeeRegResponseModel DeleteEmployee(EmployeesViewModel _model)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", _model.FK_MemId);
                queryParameters.Add("@Procid", 1);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                var data = DBHelperDapper.DAAddAndReturnModel<EmployeeRegResponseModel>("Proc_EmployeeDetail", queryParameters);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static EmployeeRegResponseModel EmplReg(EmployeesViewModel _model)
        {
            try
            {
                string Password = new Random().Next(999999999).ToString();
                
                EmployeeRegResponseModel _response = new EmployeeRegResponseModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", _model.FK_MemId);
                queryParameters.Add("@FirstName", _model.FirstName);
                queryParameters.Add("@LastName", _model.LastName);
                queryParameters.Add("@Mobile", _model.Mobile);
                queryParameters.Add("@Email", _model.Email);
                queryParameters.Add("@DOB", _model.DOB);
                queryParameters.Add("@Gender", _model.Gender);
                queryParameters.Add("@Aadhar", _model.CustAadharNo);
                queryParameters.Add("@NormalPassword", Password);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                queryParameters.Add("@Password", FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "md5"));
                _response = DBHelperDapper.DAAddAndReturnModel<EmployeeRegResponseModel>("ProEmployeeRegistration", queryParameters);
                if (_response.Status)
                {
                    string Message = string.Empty;
                    Message = "Dear " + _model.FirstName + "" + _model.LastName + "\nYou have successfully registered with LifeOne.\nLoginId : " + _response.LoginId + "\nPassword : " + Password + "\nBest Regards\nFamilyN";
                    SMS.SendSMS(_model.Mobile, Message);
                }
                return _response;
            }
            catch (Exception e)
            {
                return new EmployeeRegResponseModel { Flag = 0, Status = false, Message = e.Message, response = "failed" };
            }
        }
    }

    public class DALAdminFranchiseSell
    {
        public static List<MAdminFranchiseSell> FranchiseSellReport(MAdminFranchiseSell model)
        {
            var queryParameters = new DynamicParameters();
            //model.Date = "01/11/2020 - 07/11/2020";
            queryParameters.Add("@fk_MemId", model.fk_MemId);
            if (model.Date != null)
            {
                queryParameters.Add("@Date", model.Date);
            }
            else
            {
                queryParameters.Add("@Date", model.Saledate);
            }
            queryParameters.Add("@InvoiceNo", model.InvoiceNo);
            queryParameters.Add("@ProcId", model.ProcId);
            List<MAdminFranchiseSell> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminFranchiseSell>("Proc_FranchiseSellReport", queryParameters);
            return _iresult;
        }
    }


    public class DALAdminFranchisePurchaseHistory
    {
        public static List<MAdminFranchisePurchaseHistory> FranchisePurchaseHistoryReport(MAdminFranchisePurchaseHistory model)
        {
            var queryParameters = new DynamicParameters();
            //model.Date = "01/11/2020 - 07/11/2020";
            queryParameters.Add("@fk_MemId", model.fk_MemId);
            queryParameters.Add("@Date", model.Date);
            queryParameters.Add("@InvoiceNo", model.InvoiceNo);
            queryParameters.Add("@ProcId", model.ProcId);
            List<MAdminFranchisePurchaseHistory> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminFranchisePurchaseHistory>("Proc_FranchisePurchaseHistory", queryParameters);
            return _iresult;
        }
    }
}