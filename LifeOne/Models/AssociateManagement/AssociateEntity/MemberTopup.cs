using LifeOne.Models.AdminManagement.ADAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{

    public class MemberTopup
    {

        public long FK_MemId { get; set; }
        public int FK_PackageId { get; set; }
        public string UpdatedBy { get; set; }
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
        public string UPINumber { get; set; }
        public long AddedBy {get; set; }

        public DataTable TopUpMemberByAdmin(MemberTopup obj)
        {
            
            SqlParameter[] para =
            {
            new SqlParameter("@FK_MemId",obj.FK_MemId),
            new SqlParameter("@FK_PackageId",obj.FK_PackageId),
            new SqlParameter("@UpdatedBy",obj.UpdatedBy),
            new SqlParameter("@PaidAmount",obj.PaidAmount),
            new SqlParameter("@BankName",obj.BankName),
            new SqlParameter("@ChequeDDNo",obj.ChequeDDNo),
            new SqlParameter("@ChequeDDDate",obj.ChequeDDDate),
            new SqlParameter("@BankBranch",obj.BankBranch),
            new SqlParameter("@TopupDate",obj.TopupDate),
            new SqlParameter("@PaymentMode",obj.PaymentMode),
            new SqlParameter("@TopupRemark",obj.TopupRemark)
            };

            DataTable  dt = API.DAL.DBHelper.ExecuteQuery("TopUpMemberByAdmin", para).Tables[0];
            return dt;
        }
        public DataTable TopUpMemberByAssociate(MemberTopup obj)
        {

            SqlParameter[] para =
            {
            new SqlParameter("@FK_MemId",obj.FK_MemId),
            new SqlParameter("@FK_PackageId",obj.FK_PackageId),
            new SqlParameter("@AddedBy",obj.AddedBy),            
            };

            DataTable dt = API.DAL.DBHelper.ExecuteQuery("TopUp", para).Tables[0];
            return dt;
        }
    }
    public class PackagePurchaseHistory
    {
        public string Fk_MemId { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string PackageName { get; set; }
        public string Amount { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNo { get; set; }
        public string PaymentDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OrderNo { get; set; }
        public int Pk_OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public decimal BV { get; set; }
        public decimal DP { get; set; }
        public int TotalQty { get; set; }






        public List<PackagePurchaseHistory> packagePurchaseHistories { get; set; }

        public PackagePurchaseHistory GetAssociatePackagePurchase(PackagePurchaseHistory obj)
        {
            List<PackagePurchaseHistory> lstpackagepurchase = new List<PackagePurchaseHistory>();

            string _qry = "AssociatePackageHistory @Fk_MemId='" + obj.Fk_MemId + "', @FromDate='" + obj.FromDate +"',@ToDate='"+ obj.ToDate+"'";
            lstpackagepurchase = DBHelperDapper.DAGetDetailsInList<PackagePurchaseHistory>(_qry);
            obj.packagePurchaseHistories = lstpackagepurchase;
            return obj;
        }

    }

}