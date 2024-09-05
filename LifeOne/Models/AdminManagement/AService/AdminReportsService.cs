using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;
using ClosedXML.Excel;
using System.IO;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AdminReportsService
    {
        public static MTDSReport GetTDSReportService(MTDSReport _obj)
        {
            try
            {
                if (_obj.Page == null|| _obj.Page == 0)
                {
                    _obj.Page = 1;
                }
                _obj.Size = SessionManager.Size;
                _obj.Objlist = DALAdminReports.GetTDSReport(_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                else
                {
                    _obj.Objlist = new List<MTDSReport>();
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }

        public static MTDSReport GetPanCard_TDSReport( MTDSReport _obj)
        {
            try
            {
                if (_obj.Page == null || _obj.Page ==0)
                {
                    _obj.Page = 1;
                }
                _obj.Size = SessionManager.Size;
                _obj.Objlist = DALAdminReports.GetPanCard_TDSReport(_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                else
                {
                    _obj.Objlist = new List<MTDSReport>();
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }


        public static MPayoutReport GetPayoutReportService(int? Page,MPayoutReport _obj)
        {
            try
            {
                if(_obj.Page==null || _obj.Page == 0)
                {
                    _obj.Page = 1;
                }
                _obj.Size = SessionManager.Size;
                if (_obj.PayoutNo > 0)
                    _obj.PayountNo = _obj.PayoutNo.ToString();
                    _obj.Objlist = DALAdminReports.GetPayoutReport(Page,_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                else
                {
                    _obj.Objlist = new List<MPayoutReport>();
                }

                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        } 
        public static MPayoutReport GetRepurchasePayoutReport_AdP(int? page, MPayoutReport _obj)
        {
            try
            {
               
                if (_obj.PayoutNo > 0)
                    _obj.PayountNo = _obj.PayoutNo.ToString();
                _obj.Size = 30;
                _obj.Objlist = DALAdminReports.GetRepurchasePayoutReport_AdP(_obj);

               
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }
        public static FranchiseBusinessModel GetFranchiseBusiness(int? page, FranchiseBusinessModel _obj)
        {
            try
            {
                if(_obj.Page==null|| _obj.Page==0)
                {
                    _obj.Page = 1;
                }
                _obj.Size = SessionManager.Size;
                _obj.Objlist = DALAdminReports.GetFranchiseBusiness(_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }

        public static MPayoutReport GetAssociatePayoutReportService(int? page, MPayoutReport _obj)
        {
            try
            {

                if (_obj.PayoutNo > 0)
                    _obj.PayountNo = _obj.PayoutNo.ToString();
                _obj.Objlist = DALAdminReports.GetAssociatePayoutReport(_obj);

                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }




        public static MPayoutReport PaidUnPaidPayoutReport(int? page, MPayoutReport _obj)
        {
            try
            {
                if (_obj.Page == null || _obj.Page==0)
                {
                    _obj.Page = 1;
                }
                _obj.Size = SessionManager.Size;

                if (_obj.PayoutNo > 0)
                    _obj.PayountNo = _obj.PayoutNo.ToString();
                _obj.Objlist = DALAdminReports.PaidUnPaidPayoutReport(_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                else
                {
                    _obj.Objlist = new List<MPayoutReport>();
                }

                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }
  public static MPayoutReport ReParchasePaidUnPaidPayoutReport(int? page, MPayoutReport _obj)
        {
            try
            {
                if(_obj.Page==null || _obj.Page==0)
                {
                    _obj.Page=1;
                }
                _obj.Size = SessionManager.Size;
                if (_obj.PayoutNo > 0)
                    _obj.PayountNo = _obj.PayoutNo.ToString();
                _obj.Objlist = DALAdminReports.ReParchasePaidUnPaidPayoutReport(_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                else
                {
                    _obj.Objlist = new List<MPayoutReport>();
                }

                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }

        
        public static MPayoutReport RepurchasePayoutIncomeReport(int? page, MPayoutReport _obj)
        {
            try
            {

                if (_obj.PayoutNo > 0)
                    _obj.PayountNo = _obj.PayoutNo.ToString();
                _obj.Objlist = DALAdminReports.RepurchasePayoutIncomeReport(_obj);

                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }
        public static List<AffiliatedUtilityShopping> UtilityAfiliatedShopping(AffiliatedUtilityShopping model)
        {
            List<AffiliatedUtilityShopping> objlist = new List<AffiliatedUtilityShopping>();
            try
            {

             
                objlist = DALAdminReports.UtilityAfiliatedShopping(model);

                return objlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public static MPayoutReport RepurchasePayoutIncomeReportType(int? page, MPayoutReport _obj)
        {
            try
            {

                if (_obj.PayoutNo > 0)
                    _obj.PayountNo = _obj.PayoutNo.ToString();
                _obj.Objlist = DALAdminReports.RepurchasePayoutIncomeReportType(_obj);

                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }

        

        public static MPayoutReport GetAssociatePayoutReport(int? page, MPayoutReport _obj)
        {
            try
            {
                

                if (_obj.PayoutNo > 0)
                    _obj.PayountNo = _obj.PayoutNo.ToString();
                _obj.Objlist = DALAdminReports.GetAssociatePayoutReportList(_obj);

                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }

        
        public static MPayoutReport GetRepurchasePayoutReportService( MPayoutReport _obj)
        {
            try
            {

                if(_obj.Page==null|| _obj.Page == 0)
                {
                    _obj.Page = 1;
                }
                _obj.Size = SessionManager.Size;
                //if (_obj.PayoutNo > 0)
                //    _obj.PayountNo = _obj.PayoutNo.ToString();
                _obj.Objlist = DALAdminReports.GetRepurchasePayoutReport(_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                else
                {
                    _obj.Objlist = new List<MPayoutReport>();
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }
        public static MPayoutReport GetRepurchaseIncomePayoutReport(MPayoutReport _obj)
        
        {
            try
            {
                if( _obj.Page==null|| _obj.Page == 0)
                {
                    _obj.Page = 1;
                }
                _obj.Size=SessionManager.Size;
                //if (_obj.PayoutNo > 0)
                //    _obj.PayountNo = _obj.PayoutNo.ToString();

                _obj.Objlist = DALAdminReports.GetRepurchaseIncomePayoutReport(_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                else
                {
                    _obj.Objlist = new List<MPayoutReport>();
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }



        


        public static MPayoutReport GetDownlinePayoutReportService(int? page, MPayoutReport _obj)
        {
            try
            {
                if (page == null)
                {
                    _obj.Page = 1;
                }
                _obj.Objlist = DALAdminReports.GetDownlinePayoutReport(_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {

                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                else
                {
                    _obj.Objlist = new List<MPayoutReport>();
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }

        public static MPayoutReport GetPayoutReportForBankService(int? page, MPayoutReport _obj)
        {
            try
            {
                if (_obj.Page == null||_obj.Page==0)
                {
                    _obj.Page = 1;
                }
                _obj.SearchValue = "fadte=" + _obj.fdate + "&tdate=" + _obj.tdate + "&payoutNo=" + _obj.PayoutNo + "&MemberLoginId=" + _obj.MemberLoginId + "&KycStatus=" + _obj.KycStatus;
                _obj.Objlist = DALAdminReports.GetPayoutReportForBank(_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {
                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                else
                {
                    _obj.Objlist = new List<MPayoutReport>();
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }

        public static MPayoutReport GetRepurchasePayoutReportForBank(int? page, MPayoutReport _obj)
        {
            try
            {
                if (page == null || page==0)
                {
                    _obj.Page = 1;
                }
                _obj.SearchValue = "fadte=" + _obj.fdate + "&tdate=" + _obj.tdate + "&payoutNo=" + _obj.PayoutNo + "&MemberLoginId=" + _obj.MemberLoginId;
                _obj.Objlist = DALAdminReports.GetRepurchasePayoutReportForBank(_obj);
                int totalRecords = 0;
                if (_obj.Objlist.Count > 0)
                {
                    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecord);
                    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                    _obj.Pager = pager;
                }
                else
                {
                    _obj.Objlist = new List<MPayoutReport>();
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }
        public static MPayoutReport GetPayoutPayment(int? page, MPayoutReport _obj)
        {
            try
            {
                //if (page == null)
                //{
                //    _obj.Page = 1;
                //}
                _obj.Objlist = DALAdminReports.GetPayoutPayment(_obj);
                //int totalRecords = 0;
                //if (_obj.Objlist.Count > 0)
                //{
                //    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                //    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                //    _obj.Pager = pager;
                //}
                //else
                //{
                //    _obj.Objlist = new List<MPayoutReport>();
                //}
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }

        public static MPayoutReport GetRepurchasePayoutPayment(int? page, MPayoutReport _obj)
        {
            try
            {
                //if (page == null)
                //{
                //    _obj.Page = 1;
                //}
                _obj.Objlist = DALAdminReports.GetRepurchasePayoutPayment(_obj);
                //int totalRecords = 0;
                //if (_obj.Objlist.Count > 0)
                //{
                //    totalRecords = Convert.ToInt32(_obj.Objlist[0].TotalRecords);
                //    var pager = new Pager(totalRecords, _obj.Page, SessionManager.Size);
                //    _obj.Pager = pager;
                //}
                //else
                //{
                //    _obj.Objlist = new List<MPayoutReport>();
                //}
                return _obj;
            }
            catch (Exception ex)
            {
                return _obj;
            }
        }

        public List<StatementDetails> GetStatementDetails(int FK_MemId,string PayoutNo)
        {
            List<StatementDetails> _objResponseModel = new List<StatementDetails>();
            try
            {
                _objResponseModel = DALAdminReports.GetStatementDetails(FK_MemId, PayoutNo);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<StatementDetails> GetRepurchaseIncomeDetails(MPayoutReport obj)
        {
            List<StatementDetails> _objResponseModel = new List<StatementDetails>();
            try
            {
                _objResponseModel = DALAdminReports.GetRepurchaseIncomeDetails(obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<StatementDetails> GetStatementDetailsByIncomeType(int FK_MemId,string IncomeType)
        {
            List<StatementDetails> _objResponseModel = new List<StatementDetails>();
            try
            {
                _objResponseModel = DALAdminReports.GetStatementDetailsByIncomeType(FK_MemId, IncomeType);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public List<StatementDetails> GetStatementDetailsForAssociate(int FK_MemId,int PayoutNo,int Page,int Size)
        {
            List<StatementDetails> _objResponseModel = new List<StatementDetails>();
            try
            {
                _objResponseModel = DALAdminReports.GetStatementDetailsForAssociate(FK_MemId,PayoutNo, Page, Size);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public static DataSet UpdatePayoutPayment(long paidMemberId, Decimal paidAmount, string paymentModeId, string paymentModeNo, string paymentModeDate, int bankId, string branchname, long userId, string ipAddress)

        {

            SqlParameter[] Para = {
                new SqlParameter("@paidMemberId",paidMemberId),
                new SqlParameter("@paidAmount",paidAmount),
                new SqlParameter("@paymentModeNo",paymentModeNo),
                new SqlParameter("@paymentModeDate",paymentModeDate),
                new SqlParameter("@bankId",bankId),
                new SqlParameter("@branchname",branchname),
                new SqlParameter("@userId",userId),
                new SqlParameter("@ipAddress",ipAddress),
                new SqlParameter("@paymentMd",paymentModeId)

            };

            DataSet ds = DBHelper.ExecuteQuery("PayoutUpdatePaymentDetails", Para);
            return ds;

        } 
        
        public static DataSet RepurchaseUpdatePayoutPayment(long paidMemberId, Decimal paidAmount, string paymentModeId, string paymentModeNo, string paymentModeDate, int bankId, string branchname, long userId, string ipAddress)

        {

            SqlParameter[] Para = {
                new SqlParameter("@paidMemberId",paidMemberId),
                new SqlParameter("@paidAmount",paidAmount),
                new SqlParameter("@paymentModeNo",paymentModeNo),
                new SqlParameter("@paymentModeDate",paymentModeDate),
                new SqlParameter("@bankId",bankId),
                new SqlParameter("@branchname",branchname),
                new SqlParameter("@userId",userId),
                new SqlParameter("@ipAddress",ipAddress),
                new SqlParameter("@paymentMd",paymentModeId)

            };

            DataSet ds = DBHelper.ExecuteQuery("RepurchasePayoutUpdatePaymentDetails", Para);
            return ds;

        }

        public List<AssociatePurchaseDetails> PurchaseDetailsForAssociate(AssociatePurchaseDetails obj)
        {
            List<AssociatePurchaseDetails> _objResponseModel = new List<AssociatePurchaseDetails>();
            try
            {
                _objResponseModel = DALAdminReports.PurchaseDetailsForAssociate(obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public static MPayoutReport SavePayoutIncomeDetail(MPayoutReport model)
        {
            return DALAdminReports.SavePayoutIncomeDetail(model);
        }
    }


    public class MemberBankDetailsService
    {
        ~MemberBankDetailsService() { }

        public List<MAdminMemberBankDetails> GetMemberBankDetailsService(MAdminMemberBankDetails _obj)
        {
            List<MAdminMemberBankDetails> _objResponseModel = new List<MAdminMemberBankDetails>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALMemberBankDetails.GetMemberBankDetails(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }

    public class CommissionService
    {
        ~CommissionService() { }

        public List<MAdminCommission> GetCommisionService(MAdminCommission _obj)
        {
            List<MAdminCommission> _objResponseModel = new List<MAdminCommission>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALCommission.GetCommissionListofUser(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }

  
    public class UtilityBiilpaymentService
    {
        ~UtilityBiilpaymentService() { }

        public List<MAdminUtilityBiilpayment> GetUtilityBiilpaymentService(MAdminUtilityBiilpayment _obj)
        {
            List<MAdminUtilityBiilpayment> _objResponseModel = new List<MAdminUtilityBiilpayment>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALUtilityBiilpayment.GetUtilityBiilpaymentListofUser(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }

    public class AllwalletdetailsService
    {
        ~AllwalletdetailsService() { }

        public List<MAdminAllwalletdetails> GetAllwalletdetailsService(MAdminAllwalletdetails _obj)
        {
            List<MAdminAllwalletdetails> _objResponseModel = new List<MAdminAllwalletdetails>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAllwalletdetails.GetAllwalletdetailsList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }

    public class RazorPaymentSearchService
    {
        ~RazorPaymentSearchService() { }

        public List<MAdminRazorPaymentSearch> GetRazorPaymentSearchService(MAdminRazorPaymentSearch _obj)
        {
            List<MAdminRazorPaymentSearch> _objResponseModel = new List<MAdminRazorPaymentSearch>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALRazorPaymentSearch.GetRazorPaymentSearch(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }

    public class ActiveUnActiveMemberListService
    {
        ~ActiveUnActiveMemberListService() { }

        public List<MAdminMemberKYC> GetActiveUnActiveMemberListService(MAdminMemberKYC _obj)
        {
            List<MAdminMemberKYC> _objResponseModel = new List<MAdminMemberKYC>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALMemberKYC.GetActiveUnActiveMemberList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }

    public class GetInComeTypeListService
    {
        ~GetInComeTypeListService() { }

        public List<MAdminIncomeType> GetInComeTypeService(MAdminIncomeType _obj)
        {
            List<MAdminIncomeType> _objResponseModel = new List<MAdminIncomeType>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALIncomeType.GetInComeTypeList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<MAdminIncomeType> GetIncomeIdWise(long Fk_Memid)
        {
            List<MAdminIncomeType> _objResponseModel = new List<MAdminIncomeType>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALIncomeType.GetIncomeIdWise(Fk_Memid);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<MAdminIncomeType> GetRepurchaseIncomeIdWiseService(long Fk_Memid)
        {
            List<MAdminIncomeType> _objResponseModel = new List<MAdminIncomeType>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALIncomeType.GetRepurchaseIncomeIdWise(Fk_Memid);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }


    public class InrDealsTransactionListService
    {
        ~InrDealsTransactionListService() { }

        public List<MAdminIncomeType> GetInrDealsTransactionListServic(MAdminIncomeType _obj)
        {
            List<MAdminIncomeType> _objResponseModel = new List<MAdminIncomeType>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALIncomeType.GetInrDealsTransactionList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }


    public class VendorDetailsService
    {
        ~VendorDetailsService() { }

        public List<MAdminVendorDetails> GetVendorDetailsList(MAdminVendorDetails _obj)
        {
            List<MAdminVendorDetails> _objResponseModel = new List<MAdminVendorDetails>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALVendorDetails.GetVendorDetailsList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<MAdminVendorDetails> GetVendorOrderList(MAdminVendorDetails _obj)
        {
            List<MAdminVendorDetails> _objResponseModel = new List<MAdminVendorDetails>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALVendorDetails.GetVendorOrderList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

    }

    public class AchieversDetailsService
    {
        ~AchieversDetailsService() { }

        public List<MAdminAchieversDetails> GetAchieversDetailsService(MAdminAchieversDetails _obj)
        {
            List<MAdminAchieversDetails> _objResponseModel = new List<MAdminAchieversDetails>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAchieversDetails.GetAchieversDetails(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }


    public class BlockUnblockService
    {
        ~BlockUnblockService() { }

        public List<MAdminBlockUnblock> GetBlockUnblockService(MAdminBlockUnblock _obj)
        {
            List<MAdminBlockUnblock> _objResponseModel = new List<MAdminBlockUnblock>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALBlockUnblock.GetBlockUnblockMemberList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public BlockUnblockResponse BlockUnblockMemberService(string id)
        {

            BlockUnblockResponse _objResponseModel = new BlockUnblockResponse();
            try
            {

                //decrypt app team reponse
                long CreatedBY = SessionManager.Fk_MemId;
                _objResponseModel = DALBlockUnblock.BlockUnblockMembers(id, CreatedBY);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public BlockUnblockResponse BlockUnblockMemberServiceV2(long Fk_MemId, string Status, string Remark)
        {

            BlockUnblockResponse _objResponseModel = new BlockUnblockResponse();
            try
            {

                //decrypt app team reponse
                long CreatedBY = SessionManager.Fk_MemId;
                _objResponseModel = DALBlockUnblock.BlockUnblockMembersV2(Fk_MemId , Status,CreatedBY, Remark);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public BlockUnblockResponse UnblockUser(long Fk_MemId, string Purpose)
        {

            BlockUnblockResponse _objResponseModel = new BlockUnblockResponse();
            try
            {

                //decrypt app team reponse
                long CreatedBY = SessionManager.Fk_MemId;
                _objResponseModel = DALBlockUnblock.UnblockUser(Fk_MemId, Purpose, CreatedBY);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }


    public class TopTenTodaysReferalsService
    {
        ~TopTenTodaysReferalsService() { }

        public List<MAdminMembers> GetTopTenTodaysReferals(MAdminMembers _obj)
        {
            List<MAdminMembers> _objResponseModel = new List<MAdminMembers>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALMembers.GetTopTenTodaysReferals(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }

    public class GetDirectMemberService
    {
        ~GetDirectMemberService() { }

        public static List<MMemberList> GetDirectMemberDetailsService(long Fk_Memid, int ProcId, int? Page, long Size)
        {
            List<MMemberList> _objResponseModel = new List<MMemberList>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALGetMember.GetDirectMemberDetails(Fk_Memid, ProcId, Page, Size);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }

    public class GetMemberDownlineService
    {
        ~GetMemberDownlineService() { }

        public static List<MMemberList> GetMembersDownlineService(MMemberList model)
        {
            List<MMemberList> _objResponseModel = new List<MMemberList>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALGetMemberDownline.GetMembersDownlineDetails(model);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
    public class DsResultModel
    {
        public string Code { get; set; }
        public string Remark { get; set; }

        public static DsResultModel DsToResultModel(DataSet ds)
        {
            DsResultModel resultModel = new DsResultModel();
            resultModel.Code = "0";
            resultModel.Remark = "Unable to perform action";
            try
            {
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            resultModel.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                            resultModel.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultModel.Code = "0";
                resultModel.Remark = ex.Message;
            }
            return resultModel;
        }

        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            // DateTime Dt;

            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }
    }



    public class FranchiseSellService
    {
        ~FranchiseSellService() { }

        public static List<MAdminFranchiseSell> GetFranchiseSellService(MAdminFranchiseSell model)
        {
            List<MAdminFranchiseSell> _objResponseModel = new List<MAdminFranchiseSell>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAdminFranchiseSell.FranchiseSellReport(model);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }



    }

    public class FranchisePurchaseHistoryService
    {
        ~FranchisePurchaseHistoryService() { }

        public static List<MAdminFranchisePurchaseHistory> GetFranchisePurchaseHistoryService(MAdminFranchisePurchaseHistory model)
        {
            List<MAdminFranchisePurchaseHistory> _objResponseModel = new List<MAdminFranchisePurchaseHistory>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAdminFranchisePurchaseHistory.FranchisePurchaseHistoryReport(model);



                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }


    


}
