using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.HomeManagement.HEntity
{
    public class FranchiseApplicationFormModel : FranchiseRequestBankModel
    {

        public int FranchiseTypeId { get; set; }        
        public long KeyId { get; set; }
        public string AssociateId { get; set; }
        public string AssociateName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public int AppliedForPinCode { get; set; }
        public string Address { get; set; }
        public string AreaName { get; set; }
        public string Village { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        public int PinCode { get; set; }
        public string State { get; set; }
        public string FranchiseName { get; set; }
        //------------------Document----------------------------------
        public List<FranchiseRequestDocumentModel> FranchiseRequestDocumentList { get; set; }
        public List<FranchiseRequestBankModel> FranchiseRequestBankList { get; set; }
        //--------------XML Data------------------
        public string DocXml { get; set; }
        public string BankXML { get; set; }
        //-------------V29012021-----------------------
        public string DocType { get; set; }
        public string DocNo { get; set; } 
        public HttpPostedFileBase AttachmentFile { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }

    }
    public class FranchiseRequestDocumentModel
    {
        public long RDID { get; set; }
        public long FK_FranchiseId { get; set; }
        public string DocType { get; set; }
        public string DocNumber { get; set; }
        public string DocImage { get; set; }
        public HttpPostedFileBase DocFile { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }

    }
    public class FranchiseRequestBankModel
    {
        public long RBID { get; set; }
        public long FK_FranchiseId { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string IFSC_CODE { get; set; }
        public string GSTNo { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class MemberDetailsForFranchiseModel
    {
        public string LoginId { get; set; }
        public string PersonName { get; set; }
        public string Mobile { get; set; }
        public string EmailAddress { get; set; }
        public string Dateofbirth { get; set; }
        public string FatherName { get; set; }
        public string ProfilePic { get; set; }
        public string Cr_PinCode { get; set; }
        public string Cr_Address { get; set; }
        public string Cr_City { get; set; } 
        public string Cr_State { get; set; }
        public string Co_Tehsil { get; set; }
        public string Zone { get; set; }
        public string CustAadharNo { get; set; }
        public string CustPanNo { get; set; }
        public string District { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

    }


    public class FranchiseRegistrationResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public bool Iskyc { get; set; }
        public int Flag { get; set; }
        public string Remark { get; set; }

        public string AssociateName { get; set; }
        public string FranchiseName { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public int KeyId { get; set; }

    }

    public class FranchiseApplicationFormReportModel
    {

       
        
        
        public string LoginId { get; set; }
        public long KeyId { get; set; }
        public string AssociateId { get; set; }
        public string AssociateName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public int? AppliedForPinCode { get; set; }
        public string Address { get; set; }
        public string AreaName { get; set; }
        public string Village { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        public int PinCode { get; set; }
        public int ToatlRecords { get; set; }
        public int page { get; set; }
        public int Size { get; set; }
        public int procId { get; set; }
        public string State { get; set; }
        public string FranchiseName { get; set; }
        public string Status { get; set; }
        //-----------Other Details--------------------------
        public string PANNo { get; set; }
        public string PANDoc { get; set; }
        public string AadharNo { get; set; }
        public string AadharDoc { get; set; }
        public long FK_FranchiseId { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string IFSC_CODE { get; set; }
        public string GSTNo { get; set; }
        public bool ReturnStatus { get; set; }
        public string CompanyName { get; set; }
        public string AccountHolderName { get; set; }
        public string DeviceId { get; set; }
        public string ApplicationSubmitDate { get; set; }
        public string ApprovedDate { get; set; }
        public string DistrictName { get; set; }

        public int FK_MemId { get; set; }
        public Pager Pager { get; set; }
        public List<FranchiseApplicationFormReportModel> _DataList { get; set; }
    }


     
    public class FranchiseApplicationFormReportFilterModel
    {
      
        public string LoginId { get; set; }
        public string AssociateName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string AppliedForPinCode { get; set; }
        public int procId { get; set; }
        public int ? page { get; set; }
            
        
    }
    public class FranchiseApprovalResponseModel
    {
        public string PerosonName { get; set; }
        public string LoginId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public string FranchisePassword { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }

        public string State { get; set; }
        public string District { get; set; }
        public string Fkfrachnisetypeid { get; set; }


    }





}