using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.HomeManagement.HEntity
{
    public class MCustomerRegistration
    {
        public int Fk_MemId { get; set; }
        public string InviteCode { get; set; }

        public string PlaceUnderId { get; set; }
        
        public long Fk_SponserId { get; set; }
        public string SponserName { get; set; }
        public string AccountPurpose { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }

        public string RegType { get; set; }
        public string Gender { get; set; }
        public string OrgType { get; set; }
        public string DOB { get; set; }        
        public string CommencementDate { get; set; }
        public string MaritalStatus { get; set; }
        public string MarriageAnniversary { get; set; }
        public string Password { get; set; }
        public string ConPassword { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string AadharNo { get; set; }
        public string CustAadharImage { get; set; }
        public string CustAadharImageStatus { get; set; }
        public string SpouseAadharImagesStatus { get; set; }
        public string PanNo { get; set; }
        public string GSTNo { get; set; }
        public string Address1 { get; set; }
        public string husbandOrFatherNamesuffix { get; set; }
        public string FatherName { get; set; }
        public string Address2 { get; set; }
        public string LandMark { get; set; }
        public string Pincode { get; set; }
        public string VillageName { get; set; }
        public string Tehsil { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string City { get; set; }
        public string OTP { get; set; }
        public string CountryId { get; set; }
        public string SpouseName { get; set; }
        public string SpouseAadharNo { get; set; }
        public string SpousePanNo { get; set; }
        public string SpouseAadharImages { get; set; }
        public string NormalPassword { get; set; }
        public string ChildName { get; set; }
        public string Leg { get; set; }
        
        public string ChildRel { get; set; }
        public string ChildAadhar { get; set; }
        public string ChildPanNo { get; set; }
        public string ChildAadharImage { get; set; }
        public decimal TotalLand { get; set; }
        public string FruitImages { get; set; }
        public string FruitVegetablesImages { get; set; }
        public string LeafVegetablesImage { get; set; }
        public string GrainImages { get; set; }
        public string PulseCropImages { get; set; }
        public string profilepic { get; set; }
        public string Zone { get; set; }
        public int Flag { get; set; }
        public string Msg { get; set; }
        public List<ChildDetails> lstChild { get; set; }
        public List<MCustomerRegistration> lstCustomer { get; set; }

        public string AccountType { get; set; }
        public string JoiningDate { get; set; }
        public long MobileTeam { get; set; }
        public bool Iskyc { get; set; }
        //-------------Response----------------------

        //--------------Doc and Doc Type------------------------
        public string DocType { get; set; }
        public string DocNo { get; set; }
        public HttpPostedFileBase AttachmentFile { get; set; }
        public string FilePath { get; set; }

        public string BankAccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string NomineeName { get; set; }
        public string RelationWithNominee { get; set; }
        public decimal SelfBusiness { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal FamilyNWallet { get; set; }
        public decimal PayoutWallet { get; set; }
        public int BID { get; set; }
        public string LoginId { get; set; }
        public List<MTransactionLog> LstTransactionLog { get; set; }
        public Nullable<bool> IsApprovedBank { get; set; }
        public Nullable<bool> IsAdharApproved { get; set; }
        public Nullable<bool> IsApprovedPanCard { get; set; }
        public decimal TodayTransaction { get; set; }
        public decimal TotalSelfTransaction { get; set; }
        public long TotalTeam { get; set; }
        public decimal CashBackWallet { get; set; }
        public decimal TotalCommission { get; set; }
        public long Team { get; set; }
        public List<WalletDetailModel> WalletDetailList { get; set; }
        public List<WalletDetailModel> CashBackWalletDetailList { get; set; }
        public List<WalletDetailModel> CommissionDetailList { get; set; }
        public string PerformanceLevel { get; set; }
        public DataTable dtBusinessDetails { get;  set; }
        public DataTable dtDirectBusiness { get;  set; }
    }
    public class DistrictTehsilDetailsByPincode
    {
        public string Statename { get; set; }
        public int Fk_StateId { get; set; }
        public string Districtname { get; set; }
        public int DistrictId { get; set; }
        public string CityName { get; set; }
        public int Fk_cityId { get; set; }
        public Double pincode { get; set; }
        public int Code { get; set; }
        public string Remark { get; set; }


    }

    public class ChildDetails
    {
        public string ChildName { get; set; }
        public string Relation { get; set; }
        public string ChildAadhar { get; set; }
        public string ChildPanNo { get; set; }
        public string ChildAadharImage { get; set; }
    }
    public class WalletDetailModel
    {
        public int Fk_MemId { get; set; }
        public int TotalRecord { get; set; }
        public decimal CrAmount { get; set; }
        public decimal DrAmount { get; set; }
        public decimal TransactionAmount { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string UtilityType { get; set; }
        public string Remarks { get; set; }
        public string TransNo { get; set; }
        public string TransDate { get; set; }
        public string Status { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<WalletDetailModel> WalletDetailMList { get; set; }
        public Pager Pager { get; set; }
        public string FromDate { get; set; }


        public string ToDate { get; set; }
        public string TransactionType { get; set; }
    }




}