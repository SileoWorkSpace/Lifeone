using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class TopupByFranchise
    {
        public long FK_MemId { get; set; }
        public int FK_PackageId { get; set; }
        public long Fk_FranchiseId { get; set; }
        public decimal PaidAmount { get; set; }
        public string BankName { get; set; }
        public string ChequeDDNo { get; set; }
        public string ChequeDDDate { get; set; }
        public string IFSCCode { get; set; }
        public string BankBranch { get; set; }
        public string TopupDate { get; set; }
        public string Msg { get; set; }
        public string PaymentMode { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string ProductName { get; set; }
        public string MRP { get; set; }
        public string permanentdate { get; set; }
        public string TopupBy { get; set; }
        public string CalculationStatus { get; set; }
        public string ProductId { get; set; }
        public string Status { get; set; }
        public string PK_RequestId { get; set; }
        public string DeletedBy { get; set; }
        public string ApprovalStatus { get; set; }
        public string SearchString { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PayoutNo { get; set; }
        public string TopupRemark { get; set; }
        public string Quantity { get; set; }
        public string txtPaidAmount { get; set; }
        public string TotalBV { get; set; }
        public string txtLoginId { get; set; }
        public string WalletPayment { get; set; }
        public int txtFK_PackageId { get; set; }
        public DataTable dtDetails { get;  set; }

        public DataSet ActivateMembersTemp()
        {
            SqlParameter[] sp = new SqlParameter[] {
                        new SqlParameter("@Fk_ProductId",ProductId),
                        new SqlParameter("@Quantity",Quantity),
                        new SqlParameter("@Status",Status),
                        new SqlParameter("@Fk_MemId",FK_MemId),
                        new SqlParameter("@Addedby",Fk_FranchiseId),
            };
            DataSet dataSet = API.DAL.DBHelper.ExecuteQuery("ActivateMembersTemp",sp);
            return dataSet;
        }
        public DataSet DeleteActivatedMembersProductTemp()
        {
            SqlParameter[] sp = new SqlParameter[] {
                        new SqlParameter("@Fk_ProductId",ProductId),
                        new SqlParameter("@Fk_MemId",FK_MemId),
                        new SqlParameter("@Addedby",Fk_FranchiseId),
            };
            DataSet dataSet = API.DAL.DBHelper.ExecuteQuery("DeleteActivatedMembersProductTemp", sp);
            return dataSet;
        }

        public DataTable TopUpMemberByFranchise(TopupByFranchise obj)
        {

            SqlParameter[] para =
            {
            new SqlParameter("@FK_MemId",obj.FK_MemId),
            new SqlParameter("@FK_PackageId",obj.FK_PackageId),
            new SqlParameter("@Fk_FranchiseId",obj.Fk_FranchiseId),
            new SqlParameter("@PaidAmount",obj.PaidAmount),
            new SqlParameter("@WalletPayment",obj.WalletPayment)
            //new SqlParameter("@BankName",obj.BankName),
            //new SqlParameter("@ChequeDDNo",obj.ChequeDDNo),
            //new SqlParameter("@ChequeDDDate",obj.ChequeDDDate),
            //new SqlParameter("@BankBranch",obj.BankBranch),
            //new SqlParameter("@TopupDate",obj.TopupDate),
            //new SqlParameter("@PaymentMode",obj.PaymentMode),
            //new SqlParameter("@TopupRemark",obj.TopupRemark)
            };

            DataTable dt = API.DAL.DBHelper.ExecuteQuery("Sp_TopUpMemberByFranchise", para).Tables[0];
            return dt;
        }

        public DataSet GetActivateMembersTemp()
        {
            SqlParameter[] sp = new SqlParameter[] {
                        
                        new SqlParameter("@Fk_MemId",FK_MemId),
                        new SqlParameter("@Addedby",Fk_FranchiseId),
            };
            DataSet dataSet = API.DAL.DBHelper.ExecuteQuery("GetActivateMembersTemp", sp);
            return dataSet;
        }
        public DataSet DeleteActivateMembersTempbyId ()
        {
            SqlParameter[] sp = new SqlParameter[] {

                    
                        new SqlParameter("@Addedby",Fk_FranchiseId),
            };
            DataSet dataSet = API.DAL.DBHelper.ExecuteQuery("DeleteActivateMembersTempbyId", sp);
            return dataSet;
        }
    }

}